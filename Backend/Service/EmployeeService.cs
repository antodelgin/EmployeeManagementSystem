using Backend.Dto;
using Backend.Mappings;
using Backend.Model;

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

        public Employee FindEmployeeByID(int id)
        {
            Employee emp = appDbContext.Employees.Find(id);
            return emp;
        }

        public List<Employee> FindAllEmployees()
        {
            List<Employee> employees = appDbContext.Employees.ToList();
            return employees;
        }

        public Employee? UpdateEmployee(int id, Employee employee)
        {
            var existingData = appDbContext.Employees.Find(id);

            if (existingData == null)
                return null;

            existingData.FirstName = employee.FirstName;
            existingData.LastName = employee.LastName;
            existingData.Email = employee.Email;
            existingData.Department = employee.Department;

            appDbContext.SaveChanges();

            return existingData;
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
