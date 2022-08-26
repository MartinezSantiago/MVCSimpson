using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MVCPractica2.Context;
using MVCPractica2.Entities;
using MVCPractica2.Mapper.DTO.UserDTO;
using MVCPractica2.Models;
using MVCPractica2.Services;

namespace MVCPractica2.Controllers
{
    public class UserController : Controller
    {
        private readonly UserService userService;
        private readonly IWebHostEnvironment webHostEnvironment;
     
        public UserController(AppDbContext context, UserService userService, IWebHostEnvironment webHostEnvironment)
        {


            this.userService = userService;
            this.webHostEnvironment = webHostEnvironment;
        
           
        }

        public IActionResult Login()
        {
            return View();
        }
        [HttpPost("[controller]/Login")]
        public async Task<IActionResult> Login(UserLoginDTO userLoginDTO)
        {
            if (!(ModelState.IsValid))
            {
                ViewBag.Message = "Please check your entered values.";
                return View(userLoginDTO); }

            var response = await userService.Login(userLoginDTO);
            if (!response.Success)
            {
                ViewBag.Message = response.Message;


            }
            else
            {
                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(userService.GetClaims(userLoginDTO.Email,response.Role)), new AuthenticationProperties
                {

                    IsPersistent = true,AllowRefresh = true

                });
              
                return RedirectToAction("Index", "Home");
            }


            return View(userLoginDTO);
        }


        // GET: Login/Create
        public IActionResult Register()
        {
            return View();
        }

        // POST: Login/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost("[controller]/Register")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(UserDTO userDTO)
        {
            if (!(ModelState.IsValid))
            {
                ViewBag.Message = "Please check your entered values.";
                return View(userDTO); }

            var response = await userService.UserCreate(userDTO, webHostEnvironment);
            if (!(response.Success))
            {
                ViewBag.Message = response.Message;
            }
            else
            {

                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(userService.GetClaims(userDTO.Email,response.Role)), new AuthenticationProperties
                {

                    IsPersistent = true,AllowRefresh=true

                });
            
                 
                return RedirectToAction("Index", "Home");
            }
            return View(userDTO);
        }
         
        
        public async Task<ActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return View("Login");
            
        }
    }
        
    }

     
   