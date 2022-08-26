using MVCPractica2.Helper;
using MVCPractica2.Mapper.DTO.Simpson;

using MVCPractica2.Mapper.DTO.UserDTO;
using MVCPractica2.Models;

namespace MVCPractica2.Mapper
{
    public class AutoMapper
    {
        private readonly ImageToBase64 imageToBase64;

        public AutoMapper(ImageToBase64 imageToBase64)
        {
            this.imageToBase64 = imageToBase64;
        }

        public User UserDTOToUser(UserDTO userDTO,string path)
        {var user = new User { IsDeleted = false,Email = userDTO.Email, LastUpdate = DateTime.Now, UserName = userDTO.UserName, Password = userDTO.Password,Role="User" };
            ;
            if (!string.IsNullOrEmpty(path))
            {
                
                user.Image = path;
            }
            return user;

        }

        public Simpson SimpsonDTOToSimspon(SimpsonCreateDTO simpsonDTO,string path)
        {
            var simpson = new Simpson
            {
                Age = simpsonDTO.Age,
                Breed = simpsonDTO.Breed,
                Description = simpsonDTO.Description,
                IsDeleted = false,
                Lastname = simpsonDTO.Lastname,
                Name = simpsonDTO.Name,
                LastUpdate = DateTime.Now,
                Nationality = simpsonDTO.Nationality,
                Religion = simpsonDTO.Religion

            };
            
            if (!string.IsNullOrEmpty(path))
            {

                simpson.ImagePath = path;
            }
            return simpson;
        }
        
        public SimpsonViewDTO SimpsonToSimpsonViewDTO(Simpson simpson)
        {
            return new SimpsonViewDTO
            {
                Age = simpson.Age,
                Id = simpson.Id,
                Lastname = simpson.Lastname,
                Name = simpson.Name,
                Nationality = simpson.Nationality,
                ImagePath= simpson.ImagePath
            };
        }
        public SimpsonDetailsDTO SimpsonToSimpsonDetailsDTO(Simpson simpson)
        {
            return new SimpsonDetailsDTO
            {Id = simpson.Id,ImagePath=simpson.ImagePath,
                Age = simpson.Age,
                Breed = simpson.Breed,
                Description = simpson.Description,
                Lastname = simpson.Lastname,
                Name = simpson.Name,
                Nationality = simpson.Nationality,
                Religion = simpson.Religion
            };
        }
        public SimpsonEditDTO SimpsonToSimpsonEditDTO(Simpson simpson)
        {
          

            return new SimpsonEditDTO
            {
                Id = simpson.Id,
                ImagePath = simpson.ImagePath,
                Age = simpson.Age,
                Breed = simpson.Breed,
                Description = simpson.Description,
                Lastname = simpson.Lastname,
                Name = simpson.Name,
                Nationality = simpson.Nationality,
                Religion = simpson.Religion
            };
        }
        public Simpson SimpsonEditDTOToSimspon(SimpsonEditDTO simpsonDTO,string? path)
        {
            var simpson = new Simpson
            {
                Age = simpsonDTO.Age,
                Breed = simpsonDTO.Breed,
                Description = simpsonDTO.Description,
                IsDeleted = false,
                Lastname = simpsonDTO.Lastname,
                Name = simpsonDTO.Name,
                LastUpdate = DateTime.Now,
                Nationality = simpsonDTO.Nationality,
                Religion = simpsonDTO.Religion,
                Id = simpsonDTO.Id,
                ImagePath=simpsonDTO.ImagePath
                
            };
            
            if (!string.IsNullOrEmpty(path))
            {
                
                simpson.ImagePath = path;
            }
            return simpson;
        }

    }
}
