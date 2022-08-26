using Microsoft.EntityFrameworkCore;
using MVCPractica2.Context;
using MVCPractica2.Helper;
using MVCPractica2.Mapper;
using MVCPractica2.Mapper.DTO.Simpson;

using MVCPractica2.Models;

namespace MVCPractica2.Services
{
    public class SimpsonService
    {
        private readonly AppDbContext appDbContext;
        private readonly AutoMapper mapper;

        private readonly ImageToDirectory imageToDirectory;

        public SimpsonService(AppDbContext appDbContext, AutoMapper mapper, ImageToDirectory imageToDirectory)
        {
            this.appDbContext = appDbContext;
            this.mapper = mapper;

            this.imageToDirectory = imageToDirectory;
        }


        public async Task Create(SimpsonCreateDTO simpsonDTO, IWebHostEnvironment webHostEnvironment)
        {

            var simpson = new Simpson();


            simpson = mapper.SimpsonDTOToSimspon(simpsonDTO, imageToDirectory.UploadImageToDirectory(simpsonDTO.Image, webHostEnvironment));
           await appDbContext.AddAsync(simpson);
            await appDbContext.SaveChangesAsync();
        }

        public async Task<List<SimpsonViewDTO>> GetSimpsons()
        {
            var simpsons=await appDbContext.Simpsons.Where(x=>x.IsDeleted==false).ToListAsync();
            var simpsonsView = new List<SimpsonViewDTO>();
            foreach(var simpson in simpsons)
            {
                simpsonsView.Add(mapper.SimpsonToSimpsonViewDTO(simpson));
            }

            return simpsonsView;
        }
        public async Task<SimpsonDetailsDTO?> GetDetailsSimpson(int? id)
        {
            var simpson = await appDbContext.Simpsons
            .FirstOrDefaultAsync(x => x.Id == id && x.IsDeleted==false);
            if (simpson == null)
            {
                return null;
            }


            return mapper.SimpsonToSimpsonDetailsDTO(simpson);

        }
      public async Task<SimpsonEditDTO?> GetSimpsonsEdit(int? id){
            var simpson = await appDbContext.Simpsons
           .FirstOrDefaultAsync(x => x.Id == id && x.IsDeleted == false);
            if (simpson == null)
            {
                return null;
            }


            return mapper.SimpsonToSimpsonEditDTO(simpson);

        }

        public async Task Update(SimpsonEditDTO simpsonEditDTO,IWebHostEnvironment webHostEnvironment)
        {
            string path;
            if (simpsonEditDTO.Image != null)
            {
              path = imageToDirectory.UploadImageToDirectory(simpsonEditDTO.Image, webHostEnvironment);
                
                appDbContext.Update(mapper.SimpsonEditDTOToSimspon(simpsonEditDTO, path));

            }
            else
            {
                appDbContext.Update(mapper.SimpsonEditDTOToSimspon(simpsonEditDTO, null));

                
            }
            await appDbContext.SaveChangesAsync();
        } 
        public async Task<bool> SimpsonExists(int id)
        {
            return await appDbContext.Simpsons.Where(x => x.Id == id && x.IsDeleted == false).AnyAsync();
        }
        public async Task Delete(int id)
        {

            var simpson=await appDbContext.Simpsons.Where(x=>x.Id==id && x.IsDeleted==false).FirstOrDefaultAsync();
            if (simpson != null)
            {
                simpson.IsDeleted = true;
                simpson.LastUpdate = DateTime.Now;
           
            }
          await  appDbContext.SaveChangesAsync();

        }
        public string? GetImagePath(int id)
        {
           return appDbContext.Simpsons.Where(x=> x.Id == id && x.IsDeleted==false).FirstOrDefault().ImagePath;
        }
    }

}