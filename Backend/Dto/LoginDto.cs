//namespace Backend.Dto
//{
//    public class LoginDto
//    {
//        public string Email { get; set; }
//        public string Password { get; set; }
//    }
//}

using Backend.Model;
using System.ComponentModel.DataAnnotations;

namespace Backend.Dto
{
    public class LoginDto
    {
        [Required]
        public string? Email { get; set; }

        [Required]
        public string? Password { get; set; }

    }
}