using Backend.Dto;
using Backend.Model;

namespace Backend.Mappings
{

    public static class DepartmentMapper
{
    public static DepartmentDto EntityToDto(Department department)
    {
        return new DepartmentDto
        {
            Id = department.Id,
            Name = department.Name
        };
    }

    public static Department DtoToEntity(CreateDepartmentDto dto)
    {
        return new Department
        {
            Name = dto.Name
        };
    }
}
}