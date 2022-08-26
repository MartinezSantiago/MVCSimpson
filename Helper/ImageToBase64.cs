using System.Drawing;
using System.Text;

namespace MVCPractica2.Helper
{
    public class ImageToBase64
    {
     
        public byte[] ConvertImageToBase64(IFormFile formFile)
        {


            

                using (var ms = new MemoryStream())
                {
                    formFile.CopyTo(ms);
                    var fileBytes = ms.ToArray();
                    return fileBytes;
                }

           
            

        }
        public static Image LoadBase64(byte[] bytes)
        {
           
            Image image;
            using (MemoryStream ms = new MemoryStream(bytes))
            {
                image = Image.FromStream(ms);
            }
            return image;

            
        }
    }
}
