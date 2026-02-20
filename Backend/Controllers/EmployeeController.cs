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

        private EmployeeService employeeService;

        public EmployeeController(EmployeeService employeeService ) {
            this.employeeService = employeeService;
        }

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
            //Console.WriteLine(emp);
            return Ok(emp);
        }

        [HttpGet("list")]
        public IActionResult GetEmployees([FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10)
        {
            var employees = employeeService.FindAllEmployees(pageNumber, pageSize);
            return Ok(employees);
        }

        [HttpPut("update/{id}")]
        public IActionResult UpdateEmployee(int id, [FromBody] CreateEmployeeDto dto)
        {
            var updatedEmp = employeeService.UpdateEmployee(id, dto);

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
            //Console.WriteLine("ksdfj");

            if (!deleted) return NotFound($"employee with id: {id} not found");

            return Ok(new {message = $"Deleted employee id: {id}"});

        }

    }
}
