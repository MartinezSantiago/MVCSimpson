using MVCPractica2.Helper;
using MVCPractica2.Mapper.DTO.UserDTO;
using System.ComponentModel.DataAnnotations;

namespace MVCPractica2.Mapper.DTO.Simpson
{
    public class SimpsonCreateDTO
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public string Lastname { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public int Age { get; set; }

        [Required]
        public string Nationality { get; set; }

        [Required]
        public string Breed { get; set; }

        public string Religion { get; set; }

        [Required]

        [AllowedExtensions(new string[] { ".jpg", ".jpeg", ".png" })]
        public IFormFile Image { get; set; }
    }
}
