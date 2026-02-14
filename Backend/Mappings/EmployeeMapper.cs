using Backend.Dto;
using Backend.Model;

namespace Backend.Mappings
{
    public static class EmployeeMapper
    {
        public static EmployeeDto EntityToEmployeeDto(Employee employee)
        {
            return new EmployeeDto
            {
                Id = employee.Id,
                FirstName = employee.FirstName,
                LastName = employee.LastName,
                Email = employee.Email,
                Department = employee.Department
            };

        }

        public static Employee EmployeeDtoToEntity(EmployeeDto dto)
        {
            return new Employee
            {
                FirstName = dto.FirstName,
                LastName = dto.LastName,
                Email = dto.Email,
                Department = dto.Department
            };
        }


        public static Employee CreateEmployeeDtotoEntity(CreateEmployeeDto dto) 
        {
            return new Employee
            {
                FirstName = dto.FirstName,
                LastName = dto.LastName,
                Email = dto.Email,
                Department = dto.Department
            };
        }
    }
}
