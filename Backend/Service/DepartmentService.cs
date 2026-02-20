using Backend.Dto;
using Backend.Mappings;
using Backend.Model;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Backend.Service
{ 
    public class DepartmentService
    {
        private readonly AppDbContext appDbContext;
        public DepartmentService(AppDbContext appDbContext) {
            this.appDbContext = appDbContext;
        }
        public DepartmentDto CreateDepartment(CreateDepartmentDto dto)
        {
            var department = DepartmentMapper.DtoToEntity(dto);
            appDbContext.Departments.Add(department);
            appDbContext.SaveChanges();
            var departmentDto = DepartmentMapper.EntityToDto(department);
            return departmentDto;
        }

        public Department FindDepartmentByID(int id)
        {
            Department dept = appDbContext.Departments.Find(id);
            return dept;
        }

        public List<Department> FindAllDepartments()
        {
            List<Department> departments = appDbContext.Departments.ToList();
            return departments;
        }
    }
}