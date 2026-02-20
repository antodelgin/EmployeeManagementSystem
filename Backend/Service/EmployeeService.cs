using Backend.Dto;
using Backend.Mappings;
using Backend.Model;
//using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;


namespace Backend.Service
{
    public class EmployeeService
    {
        private readonly AppDbContext appDbContext;

        public EmployeeService(AppDbContext appDbContext) {
            this.appDbContext = appDbContext;
        }

        public EmployeeDto? CreateEmployee(CreateEmployeeDto dto)
        {
            var employee = EmployeeMapper.CreateEmployeeDtotoEntity(dto);

            bool emailExists = appDbContext.Employees.Any(e => e.Email == employee.Email);
            if (emailExists) return null;
            
            appDbContext.Employees.Add(employee);
            appDbContext.SaveChanges();

            var employeeDto = EmployeeMapper.EntityToEmployeeDto(employee);
            Console.WriteLine(employeeDto);
            return employeeDto;
        }


        public EmployeeDto? FindEmployeeByID(int id)
        {
            //var employee = appDbContext.Employees.Find(id);
            var employee = appDbContext.Employees
                .Include(e => e.Department)
                .FirstOrDefault(e => e.Id == id);

            if (employee == null)
                return null;

            return EmployeeMapper.EntityToEmployeeDto(employee);
        }


        public PagedResultDto<EmployeeDto> FindAllEmployees(int pageNumber, int pageSize)
        {
            if (pageNumber < 1) pageNumber = 1;
            if (pageSize < 1) pageSize = 10;

            //var query = appDbContext.Employees.AsQueryable();
            var query = appDbContext.Employees.Include(e => e.Department).AsQueryable();

            int totalRecords = query.Count();

            var employees = query.OrderByDescending(e => e.Id)    
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToList();

            var employeeDtos = employees
                .Select(e => EmployeeMapper.EntityToEmployeeDto(e))
                .ToList();

            var emp = new PagedResultDto<EmployeeDto>
            {
                Data = employeeDtos,
                TotalRecords = totalRecords,
                PageNumber = pageNumber,
                PageSize = pageSize,
                TotalPages = (int)Math.Ceiling(totalRecords / (double)pageSize)
            };

            //Console.WriteLine(emp);

            return emp;
        }


        public EmployeeDto? UpdateEmployee(int id, CreateEmployeeDto dto)
        {
            var existingData = appDbContext.Employees.Find(id);

            if (existingData == null)
                return null;

            existingData.FirstName = dto.FirstName;
            existingData.LastName = dto.LastName;
            existingData.Email = dto.Email;
            existingData.DepartmentId = dto.DepartmentId;

            appDbContext.SaveChanges();

            //var updatedData = appDbContext.Employees.Find(id);
            var updatedData = appDbContext.Employees
                .Include(e => e.Department)
                .FirstOrDefault(e => e.Id == id);

            return EmployeeMapper.EntityToEmployeeDto(updatedData);;
        }


        public bool DeleteEmployee(int id)
        {
            var employee = appDbContext.Employees.Find(id);

            if (employee == null)
                return false;

            appDbContext.Employees.Remove(employee);
            appDbContext.SaveChanges();
            return true;
        }
    }
}
