using System.ComponentModel.DataAnnotations;

namespace Backend.Model
{
    public class Employee
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        [Required]
        public string Email { get; set; }

        //[Required]
        //public string Department { get; set; }

        [Required]
        public int DepartmentId { get; set; }

        public Department Department { get; set; }
    }
}
