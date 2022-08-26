using MVCPractica2.Helper;
using System.ComponentModel.DataAnnotations;

namespace MVCPractica2.Mapper.DTO.Simpson
{
    public class SimpsonEditDTO
    {
        [Required]
        public int Id { get; set; }
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

        public string? Religion { get; set; }
        [AllowedExtensions(new string[] { ".jpg", ".jpeg", ".png" })]
        public IFormFile? Image { get; set; }
        public string? ImagePath { get; set; }

        
    }
}
