using System.ComponentModel.DataAnnotations;

namespace MVCPractica2.Mapper.DTO.Simpson
{
    public class SimpsonViewDTO
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }

        [Required]
        public string Lastname { get; set; }

        [Required]
        public int Age { get; set; }

        [Required]
        public string Nationality { get; set; }

        [Required]
        public string ImagePath { get; set; }
      

    }
}
