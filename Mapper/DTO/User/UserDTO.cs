using MVCPractica2.Helper;
using System.ComponentModel.DataAnnotations;

namespace MVCPractica2.Mapper.DTO.UserDTO
{
    public class UserDTO
    {
        [Required]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        [Required]
        [DataType(DataType.Password)]
        [MinLength(6, ErrorMessage = "The minimun lenght is 6")]
        [MaxLength(30, ErrorMessage = "The maximun lenght is 30")]
        public string Password { get; set; }

        
        [AllowedExtensions(new string[] { ".jpg", ".jpeg", ".png" })]
        public IFormFile Image { get; set; }
        [Required]
        [MinLength(4, ErrorMessage = "The minimun lenght is 2")]
        [MaxLength(50, ErrorMessage = "The maximun lenght is 50")]
        public string UserName { get; set; }



    }
    
}
