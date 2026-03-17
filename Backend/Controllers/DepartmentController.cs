using Backend.Dto;
using Backend.Model;
using Backend.Service;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using System.Runtime.CompilerServices;

namespace Backend.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class DepartmentController : ControllerBase
    {
        private DepartmentService departmentService;
        public DepartmentController(DepartmentService departmentService)
        {
            this.departmentService = departmentService;
        }

        [HttpPost("create")]
        public IActionResult AddDepartment([FromBody] CreateDepartmentDto dto)
        {
            var dept = departmentService.CreateDepartment(dto);
            return Ok(dept);
        }

        [HttpGet("{id}")]
        public IActionResult FindDepartment([FromRoute] int id)
        {
            var dept = departmentService.FindDepartmentByID(id);
            return Ok(dept);
        }

        [HttpGet("list")]
        public IActionResult GetDepartments()
        {
            var departments = departmentService.FindAllDepartments();
            return Ok(departments);
        }
    }

}