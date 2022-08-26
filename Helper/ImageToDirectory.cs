using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace MVCPractica2.Helper
{
    public class ImageToDirectory
    {
        public string UploadImageToDirectory(IFormFile formfile, IWebHostEnvironment webHostEnvironment)
        {
            string? uniqueFileName = null;

            if (formfile != null)
            {
                string uploadsFolder = Path.Combine(webHostEnvironment.WebRootPath, "images");
                uniqueFileName = Guid.NewGuid().ToString() + "_" + formfile.FileName;
                string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    formfile.CopyTo(fileStream);
                }
            }
            return uniqueFileName;

        }
    }
}
