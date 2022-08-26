using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MVCPractica2.Context;
using MVCPractica2.Mapper;
using MVCPractica2.Mapper.DTO.Simpson;
using MVCPractica2.Models;
using MVCPractica2.Services;

namespace MVCPractica2.Controllers
{
    public class SimpsonController : Controller
    {
     
      
        private readonly SimpsonService simpsonService;
        private readonly UserService userService;
        private readonly IWebHostEnvironment webHostEnvironment;

        public  SimpsonController(AppDbContext context, AutoMapper mapper, SimpsonService simpsonService, UserService userService, IWebHostEnvironment webHostEnvironment)
        {

            this.webHostEnvironment = webHostEnvironment;
            this.simpsonService = simpsonService;
            this.userService = userService;
          
        }
        [Authorize]
        // GET: Simpson
        public async Task<IActionResult> Index()
        {
           var role= (User.Identity as ClaimsIdentity).Claims.Where(c => c.Type == ClaimTypes.Role).FirstOrDefault();
            ViewBag.Role = role.Value;

            return View(await simpsonService.GetSimpsons());
        }
        [Authorize]
        // GET: Simpson/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            var role = (User.Identity as ClaimsIdentity).Claims.Where(c => c.Type == ClaimTypes.Role).FirstOrDefault();
            ViewBag.Role = role.Value;
            if (id == null || await simpsonService.GetSimpsons() == null)
            {
                return NotFound();
            }

          
            var simpson = await simpsonService.GetDetailsSimpson(id);



            if (simpson == null)
            {
                return NotFound();
            }
            ViewBag.ImagePath = simpson.ImagePath;
            return View(simpson);
        }

        [Authorize(Roles ="Admin")]
        // GET: Simpson/Create
        public IActionResult Create()
        {

            return View();
        }

        // POST: Simpson/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(SimpsonCreateDTO simpsonDTO)
        {
            if (ModelState.IsValid)
            {
               await simpsonService.Create(simpsonDTO, webHostEnvironment);
                return RedirectToAction(nameof(Index));
            }
            return View(simpsonDTO);
        }

        // GET: Simpson/Edit/5
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || await simpsonService.GetSimpsons() == null)
            {
                return NotFound();
            }

            var simpson = await simpsonService.GetSimpsonsEdit(id);
            if (simpson == null)
            {
                return NotFound();
            }
            return View(simpson);
        }
        
        // POST: Simpson/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(SimpsonEditDTO simpson)
        {
            if (simpson.Id==0)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {



                try
                {
                    await simpsonService.Update(simpson, webHostEnvironment);
                    return RedirectToAction(nameof(Index));
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!await simpsonService.SimpsonExists(simpson.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
            }


            simpson.ImagePath = simpsonService.GetImagePath(simpson.Id);
            return View(simpson);
        }

        // GET: Simpson/Delete/5
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || await simpsonService.GetSimpsons() == null)
            {
                return NotFound();
            }

            var simpsons = await simpsonService.GetSimpsons();
            var simpson=  simpsons.FirstOrDefault(m => m.Id == id);
            if (simpson == null)
            {
                return NotFound();
            }

            return View(simpson);
        }

       
        // POST: Simpson/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (simpsonService.GetSimpsons() == null)
            {
                return Problem("Entity set 'AppDbContext.Simpsons'  is null.");
            }
           await simpsonService.Delete(id);
            return RedirectToAction(nameof(Index));
        }

       
    }
}
