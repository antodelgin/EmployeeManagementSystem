using Backend.Dto;
using Backend.Mappings;
using Backend.Model;
using Backend.Exceptions;
//using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Backend.Repository;


namespace Backend.Service
{
    public class EmployeeService
    {
        //private readonly AppDbContext appDbContext;

        //public EmployeeService(AppDbContext appDbContext) {
        //    this.appDbContext = appDbContext;
        //}
        private EmployeeRepository employeeRepository;

        public EmployeeService(EmployeeRepository employeeRepository)
        {
            this.employeeRepository = employeeRepository;
        }

        //public List<Employee> FindEmployeeInSameDepartment(int departmentId, int pageNumber, int pageSize)
        //{
        //    var emp = this.employeeRepository.FindEmployeeInSameDpt(departmentId);

        //    //if (emp == null) return null;

        //    return emp;

        //}

        //public object FindEmployeeInSameDepartment(int departmentId, int pageNumber, int pageSize)
        //{
        //    var query = appDbContext.Employees
        //        .Where(e => e.DepartmentId == departmentId);

        //    var totalCount = query.Count();

        //    var data = query
        //        .Skip((pageNumber - 1) * pageSize)
        //        .Take(pageSize)
        //        .ToList();

        //    var totalPages = (int)Math.Ceiling((double)totalCount / pageSize);

        //    return new
        //    {
        //        data,
        //        totalPages
        //    };
        //}

        public object FindEmployeeInSameDepartment(int departmentId, int pageNumber, int pageSize)
        {
            var (employees, totalRecords) = employeeRepository.FindEmployeeInSameDpt(departmentId, pageNumber, pageSize);

            int totalPages = (int)Math.Ceiling((double)totalRecords / pageSize);

            var data = employees.Select(e => new
            {
                id = e.Id,
                firstName = e.FirstName,
                lastName = e.LastName,
                email = e.Email,
                departmentName = e.Department.Name
            });

            return new
            {
                data,
                totalPages
            };
        }

        //public List<Employee> SearchEmployees(string name)
        //{

        //    var employee = this.employeeRepository.SearchEmployeesByName(name);

        //    return employee;

        //}

        public object SearchEmployees(string name, int departmentId, int pageNumber, int pageSize)
        {
            var (employees, totalRecords) =
                employeeRepository.SearchEmployeesByName(name, departmentId, pageNumber, pageSize);

            int totalPages = (int)Math.Ceiling((double)totalRecords / pageSize);

            var data = employees.Select(e => new
            {
                id = e.Id,
                firstName = e.FirstName,
                lastName = e.LastName,
                email = e.Email,
                departmentName = e.Department.Name
            });

            return new
            {
                data,
                totalPages
            };
        }

        public EmployeeDto CreateEmployee(CreateEmployeeDto dto)
        {
            var employee = EmployeeMapper.CreateEmployeeDtotoEntity(dto);

            //bool emailExists = appDbContext.Employees.Any(e => e.Email == employee.Email);
            //if (emailExists) return null;
            //this.employeeRepository()
            if (this.employeeRepository.EmailExists(employee.Email))
                throw new EmailAlreadyExistsException();

            //if (emailExists)
            //    throw new EmailAlreadyExistsException();

            this.employeeRepository.AddEmployee(employee);

            //appDbContext.Employees.Add(employee);
            //appDbContext.SaveChanges();

            var employeeDto = EmployeeMapper.EntityToEmployeeDto(employee);
            Console.WriteLine(employeeDto);
            return employeeDto;
        }


        public EmployeeDto FindEmployeeByID(int id)
        {
            //var employee = appDbContext.Employees.Find(id);
            //var employee = appDbContext.Employees
            //    .Include(e => e.Department)
            //    .FirstOrDefault(e => e.Id == id);


            var employee = this.employeeRepository.FindEmployeeById(id);

            if (employee == null)
                throw new EmployeeNotFoundException(id);

            return EmployeeMapper.EntityToEmployeeDto(employee);
        }


        public PagedResultDto<EmployeeDto> FindAllEmployees(int pageNumber, int pageSize)
        {
            if (pageNumber < 1) pageNumber = 1;
            if (pageSize < 1) pageSize = 10;

            //var query = appDbContext.Employees.AsQueryable();
            //var query = appDbContext.Employees.Include(e => e.Department).AsQueryable();

            //int totalRecords = query.Count();

            //var employees = query.OrderByDescending(e => e.Id)    
            //    .Skip((pageNumber - 1) * pageSize)
            //    .Take(pageSize)
            //    .ToList();

            var result = this.employeeRepository.FindAllEmployees(pageNumber, pageSize);
            var employees = result.Item1;
            var totalRecords = result.Item2;

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


        public EmployeeDto UpdateEmployee(int id, CreateEmployeeDto dto)
        {
            //var existingData = appDbContext.Employees.Find(id);

            var existingData = this.employeeRepository.FindEmployeeById(id);

            if (existingData == null)
            {
                //Console.WriteLine("akjfkjfjoi");
                throw new EmployeeNotFoundException(id);

            }

            existingData.FirstName = dto.FirstName;
            existingData.LastName = dto.LastName;
            existingData.Email = dto.Email;
            existingData.DepartmentId = dto.DepartmentId;

            //appDbContext.SaveChanges();
            this.employeeRepository.Save();

            //var updatedData = appDbContext.Employees.Find(id);
            //var updatedData = appDbContext.Employees
            //    .Include(e => e.Department)
            //    .FirstOrDefault(e => e.Id == id);
            var updatedData = this.employeeRepository.FindEmployeeById(id);

            return EmployeeMapper.EntityToEmployeeDto(updatedData);;
        }


        public bool DeleteEmployee(int id)
        {
            //var employee = appDbContext.Employees.Find(id);
            var employee = this.employeeRepository.FindEmployeeById(id);


            if (employee == null)
                return false;

            //appDbContext.Employees.Remove(employee);
            //appDbContext.SaveChanges();
            this.employeeRepository.DeleteEmployee(employee);

            return true;
        }
    }
}
