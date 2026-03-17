using System.ComponentModel.DataAnnotations;

namespace Backend.Model
{
    public class User
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Username { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }

        [Required]
        public UserRole Role { get; set; }


        //public List<Employee> Employees { get; set; }
    }

    public enum UserRole
    {
        Admin,
        User
    }
}