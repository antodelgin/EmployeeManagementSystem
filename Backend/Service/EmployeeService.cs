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
        private EmployeeRepository employeeRepository;

        public EmployeeService(EmployeeRepository employeeRepository)
        {
            this.employeeRepository = employeeRepository;
        }

        private PagedResultDto<TDto> BuildPagedResult<TEntity, TDto>(
            List<TEntity> entities,
            int totalRecords,
            int pageNumber,
            int pageSize,
            Func<TEntity, TDto> mapFunc)
        {
            return new PagedResultDto<TDto>
            {
                Data = entities.Select(mapFunc).ToList(),
                TotalRecords = totalRecords,
                PageNumber = pageNumber,
                PageSize = pageSize,
                TotalPages = (int)Math.Ceiling(totalRecords / (double)pageSize)
            };
        }

        public PagedResultDto<EmployeeDto> FindEmployeeInSameDepartment(int departmentId, int pageNumber, int pageSize)
        {
            var (employees, totalRecords) =
                employeeRepository.FindEmployeeInSameDpt(departmentId, pageNumber, pageSize);

            return BuildPagedResult(
                employees,
                totalRecords,
                pageNumber,
                pageSize,
                EmployeeMapper.EntityToEmployeeDto
            );
        }

        public PagedResultDto<EmployeeDto> SearchEmployees(string name, int departmentId, int pageNumber, int pageSize)
        {
            var (employees, totalRecords) =
                employeeRepository.SearchEmployeesByName(name, departmentId, pageNumber, pageSize);

            return BuildPagedResult(
                employees,
                totalRecords,
                pageNumber,
                pageSize,
                EmployeeMapper.EntityToEmployeeDto
            );
        }

        public PagedResultDto<EmployeeDto> FindAllEmployees(int pageNumber, int pageSize)
        {
            pageNumber = pageNumber < 1 ? 1 : pageNumber;
            pageSize = pageSize < 1 ? 10 : pageSize;

            var (employees, totalRecords) =
                employeeRepository.FindAllEmployees(pageNumber, pageSize);

            return BuildPagedResult(
                employees,
                totalRecords,
                pageNumber,
                pageSize,
                EmployeeMapper.EntityToEmployeeDto
            );
        }

        //public object FindEmployeeInSameDepartment(int departmentId, int pageNumber, int pageSize)
        //{
        //    var (employees, totalRecords) = employeeRepository.FindEmployeeInSameDpt(departmentId, pageNumber, pageSize);

        //    int totalPages = (int)Math.Ceiling((double)totalRecords / pageSize);

        //    var data = employees.Select(e => new
        //    {
        //        id = e.Id,
        //        firstName = e.FirstName,
        //        lastName = e.LastName,
        //        email = e.Email,
        //        departmentName = e.Department.Name
        //    });

        //    return new
        //    {
        //        data,
        //        totalPages
        //    };
        //}


        //public object SearchEmployees(string name, int departmentId, int pageNumber, int pageSize)
        //{
        //    var (employees, totalRecords) =
        //        employeeRepository.SearchEmployeesByName(name, departmentId, pageNumber, pageSize);

        //    int totalPages = (int)Math.Ceiling((double)totalRecords / pageSize);

        //    var data = employees.Select(e => new
        //    {
        //        id = e.Id,
        //        firstName = e.FirstName,
        //        lastName = e.LastName,
        //        email = e.Email,
        //        departmentName = e.Department.Name
        //    });

        //    return new
        //    {
        //        data,
        //        totalPages
        //    };
        //}

        public EmployeeDto CreateEmployee(CreateEmployeeDto dto)
        {
            var employee = EmployeeMapper.CreateEmployeeDtotoEntity(dto);

            if (this.employeeRepository.EmailExists(employee.Email))
                throw new EmailAlreadyExistsException();

            this.employeeRepository.AddEmployee(employee);

            var employeeDto = EmployeeMapper.EntityToEmployeeDto(employee);
            Console.WriteLine(employeeDto);
            return employeeDto;
        }


        public EmployeeDto FindEmployeeByID(int id)
        {
            var employee = this.employeeRepository.FindEmployeeById(id);

            if (employee == null)
                throw new EmployeeNotFoundException(id);

            return EmployeeMapper.EntityToEmployeeDto(employee);
        }


        //public PagedResultDto<EmployeeDto> FindAllEmployees(int pageNumber, int pageSize)
        //{
        //    if (pageNumber < 1) pageNumber = 1;
        //    if (pageSize < 1) pageSize = 10;

        //    var result = this.employeeRepository.FindAllEmployees(pageNumber, pageSize);
        //    var employees = result.Item1;
        //    var totalRecords = result.Item2;

        //    var employeeDtos = employees
        //        .Select(e => EmployeeMapper.EntityToEmployeeDto(e))
        //        .ToList();

        //    var emp = new PagedResultDto<EmployeeDto>
        //    {
        //        Data = employeeDtos,
        //        TotalRecords = totalRecords,
        //        PageNumber = pageNumber,
        //        PageSize = pageSize,
        //        TotalPages = (int)Math.Ceiling(totalRecords / (double)pageSize)
        //    };

        //    return emp;
        //}


        public EmployeeDto UpdateEmployee(int id, CreateEmployeeDto dto)
        {
            var existingData = this.employeeRepository.FindEmployeeById(id);

            if (existingData == null)
            {
                throw new EmployeeNotFoundException(id);
            }

            existingData.FirstName = dto.FirstName;
            existingData.LastName = dto.LastName;
            existingData.Email = dto.Email;
            existingData.DepartmentId = dto.DepartmentId;

            this.employeeRepository.Save();

            var updatedData = this.employeeRepository.FindEmployeeById(id);

            return EmployeeMapper.EntityToEmployeeDto(updatedData);;
        }


        public bool DeleteEmployee(int id)
        {
            var employee = this.employeeRepository.FindEmployeeById(id);


            if (employee == null)
                return false;

            this.employeeRepository.DeleteEmployee(employee);

            return true;
        }
    }
}
