using Backend.Dto;
using Backend.Model;
using Backend.Service;
using Microsoft.AspNetCore.Mvc;
using System.Runtime.CompilerServices;

namespace Backend.Controllers
{

    [ApiController]
    [Route("[controller]")]
    public class EmployeeController : ControllerBase
    {
        //public IActionResult Index()
        //{
        //    return View();
        //}

        private EmployeeService employeeService;

        public EmployeeController(EmployeeService employeeService ) {
            this.employeeService = employeeService;
        }

        //[HttpPost("create")]
        //public IActionResult AddEmployee([FromBody] Employee employee)
        //{
        //    var emp = employeeService.CreateEmployee(employee);
        //    if (emp == null)
        //        return BadRequest(new { message = "Email already exists" });

        //    return Ok(emp);
        //}

        [HttpPost("create")]
        public IActionResult AddEmployee([FromBody] CreateEmployeeDto dto)
        {
            var emp = employeeService.CreateEmployee(dto);
            if (emp == null)
                return BadRequest(new { message = "Email already exists" });

            return Ok(emp);
        }

        [HttpGet("{id}")]
        public IActionResult FindEmployee([FromRoute] int id)
        {
            var emp = employeeService.FindEmployeeByID(id);
            return Ok(emp);
        }

        [HttpGet("list")]
        public IActionResult GetEmployees()
        {
            var employees = employeeService.FindAllEmployees();
            return Ok(employees);
        }

        [HttpPut("update/{id}")]
        public IActionResult UpdateEmployee(int id, [FromBody] Employee employee)
        {
            var updatedEmp = employeeService.UpdateEmployee(id, employee);

            if (updatedEmp == null)
                return NotFound($"Employee with id {id} not found");

            return Ok(new
            {
                message = $"Updated employee id: {id}",
                updatedEmp
            });
        }


        [HttpDelete("delete/{id}")]
        public IActionResult DeleteEmployee(int id)
        {
            var deleted = employeeService.DeleteEmployee(id);
            //Console.WriteLine("fkjhakjfhk");

            if (!deleted) return NotFound($"employee with id: {id} not found");

            return Ok(new {message = $"Deleted employee id: {id}"});



        }




    }
}
