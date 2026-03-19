//using Backend.Model;
using System.ComponentModel.DataAnnotations;

namespace Backend.Dto
{
    public class SignUpDto
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public string Role { get; set; }
        public int EmployeeId { get; set; }
        public bool IsActive { get; set; }
    }
}