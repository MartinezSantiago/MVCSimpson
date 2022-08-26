using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using MVCPractica2.Context;
using MVCPractica2.Entities;
using MVCPractica2.Helper;
using MVCPractica2.Mapper;
using MVCPractica2.Mapper.DTO.UserDTO;
using MVCPractica2.Models;
using System.Security.Claims;

namespace MVCPractica2.Services
{
    public class UserService
    {
        private readonly AppDbContext appDbContext;
        private readonly AutoMapper mapper;
        private readonly Encrypt encrypt;
        private readonly ImageToDirectory imageToDirectory;
        public UserService(AppDbContext appDbContext, AutoMapper mapper, Encrypt encrypt, ImageToDirectory imageToDirectory)
        {
            this.appDbContext = appDbContext;
            this.mapper = mapper;
            this.encrypt = encrypt;
            this.imageToDirectory = imageToDirectory;
           
        }

        public async Task<UserResponse> Login(UserLoginDTO userLoginDTO)
        {
            var userResponse = new UserResponse();

            userLoginDTO.Password = encrypt.GetSHA256(userLoginDTO.Password);
            var user = await appDbContext.Users.Where(x => x.IsDeleted == false && x.Email == userLoginDTO.Email).FirstOrDefaultAsync();
            if (user == null)
            {
                userResponse.Success = false;
                userResponse.Message = "The user does not exist.";
            }
            else
            {
                if (user.Password == userLoginDTO.Password)
                {
                    userResponse.Success = true;
                    userResponse.Message = "The login was executed successfully";
                    userResponse.Role = user.Role;
                }
                else
                {
                    userResponse.Success = false;
                    userResponse.Message = "The email or password are incorrect.";
                 
                }
            }
            return userResponse;

        }
        public string GetRole(string Email)
        {
            var user =appDbContext.Users.Where(x => x.IsDeleted == false && x.Email == Email).FirstOrDefault();
            
            return user.Role;
        }
        public ClaimsIdentity GetClaims(string Email,string Role)
        {
            var claim = new List<Claim>{
                        new Claim(ClaimTypes.Email,Email),
                        new Claim(ClaimTypes.Role,Role)
                        
                    };
            var claims = new ClaimsIdentity(claim, CookieAuthenticationDefaults.AuthenticationScheme);
            return claims;
        }
        public async Task<User> SearchUser(string Email, string Password)
        {
            var user = await appDbContext.Users.Where(x => x.IsDeleted == false && x.Email == Email).FirstOrDefaultAsync();
            return user;
        }
        private async Task<bool> UserExists(string Email)
        {
            return await appDbContext.Users.Where(x => x.Email == Email && x.IsDeleted == false).AnyAsync();
        }
        public async Task<UserResponse> UserCreate(UserDTO userDTO, IWebHostEnvironment webHostEnvironment)
        {
            var userResponse = new UserResponse();
           
         if(!(await UserExists(userDTO.Email)))
            {

                userDTO.Password=encrypt.GetSHA256(userDTO.Password);
               

                await appDbContext.Users.AddAsync(mapper.UserDTOToUser(userDTO, imageToDirectory.UploadImageToDirectory(userDTO.Image, webHostEnvironment)));
                await appDbContext.SaveChangesAsync();
                userResponse.Success = true;
                userResponse.Message = "The user was created successfully";
                userResponse.Role = "User";
            }
            else
            {
                userResponse.Success = false;
                userResponse.Message = "The email is already used.";
         
            }
            return userResponse;
        }
    }
}
