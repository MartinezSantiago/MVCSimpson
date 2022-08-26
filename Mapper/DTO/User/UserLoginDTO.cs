using System.ComponentModel.DataAnnotations;

namespace MVCPractica2.Mapper.DTO.UserDTO
{
    public class UserLoginDTO
    {
       
        [Required]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
