using System.ComponentModel.DataAnnotations;

namespace Backend.Model
{
    public class Department
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int Code { get; set; }

        [Required]
        public string Name { get; set; }

        public List<Employee> Employees { get; set; } 
    }
}