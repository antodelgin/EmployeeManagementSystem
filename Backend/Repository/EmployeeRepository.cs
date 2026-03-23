using Backend.Model;
using Microsoft.EntityFrameworkCore;


namespace Backend.Repository
{

    public class EmployeeRepository
    {
        private readonly AppDbContext appDbContext;

        public EmployeeRepository(AppDbContext appDbContext)
        {
            this.appDbContext = appDbContext;
        }

        //public List<Employee> SearchEmployeesByName(string name)
        //{

        //    var emp = appDbContext.Employees.Where(e => e.FirstName.Contains(name) || e.LastName.Contains(name)).ToList();
        //    return emp;

        //}

        //public List<Employee> SearchEmployeesByName(string name)
        //{
        //    //if (string.IsNullOrWhiteSpace(name))
        //    //    return new List<Employee>();

        //    return appDbContext.Employees
        //        .Where(e => e.FirstName.Contains(name) || e.LastName.Contains(name))
        //        .ToList();
        //}


        public List<Employee> FindEmployeeInSameDpt(int departmentId)
        {
            var emp = appDbContext.Employees.Where(e => e.DepartmentId == departmentId).AsNoTracking().ToList();
            return emp;
        }


        public void AddEmployee(Employee employee)
        {
            appDbContext.Employees.Add(employee);
            appDbContext.SaveChanges();
        }

        public bool EmailExists(string email)
        {
            return appDbContext.Employees.Any(e => e.Email == email);
        }

        public Employee FindEmployeeById(int id)
        {
            var employee = appDbContext.Employees
                .Include(e => e.Department)
                .FirstOrDefault(e => e.Id == id);

            return employee;
        }

        public void DeleteEmployee(Employee employee)
        {

            appDbContext.Employees.Remove(employee);
            appDbContext.SaveChanges();
            
        }

        public (List<Employee>, int) FindAllEmployees(int pageNumber, int pageSize)
        {
            var query = appDbContext.Employees
                        .Include(e => e.Department)
                        .AsQueryable();

            int totalRecords = query.Count();

            var employees = query
                .OrderBy(e => e.Id)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToList();

            return (employees, totalRecords);
        }

        public void Save()
        {
            appDbContext.SaveChanges();
        }


        public (List<Employee>, int) FindEmployeeInSameDpt(int departmentId, int pageNumber, int pageSize)
        {
            var query = appDbContext.Employees
                        .Include(e => e.Department)
                        .Where(e => e.DepartmentId == departmentId)
                        .AsQueryable();

            int totalRecords = query.Count();

            var employees = query
                .OrderByDescending(e => e.Id)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToList();

            return (employees, totalRecords);
        }


        //public (List<Employee>, int) SearchEmployeesByName(string name, int departmentId, int pageNumber, int pageSize)
        //{
        //    var query = appDbContext.Employees
        //        .Include(e => e.Department)
        //        .Where(e => (e.FirstName.Contains(name) || e.LastName.Contains(name)) && e.DepartmentId == departmentId)
        //        .AsQueryable();

        //    int totalRecords = query.Count();

        //    var employees = query
        //        .OrderByDescending(e => e.Id)
        //        .Skip((pageNumber - 1) * pageSize)
        //        .Take(pageSize)
        //        .ToList();

        //    return (employees, totalRecords);
        //}

        public (List<Employee>, int) SearchEmployeesByName(string name, int departmentId, int pageNumber, int pageSize)
        {
            var query = appDbContext.Employees
                .Include(e => e.Department)
                .AsQueryable();

            if (!string.IsNullOrWhiteSpace(name))
            {
                query = query.Where(e => e.FirstName.Contains(name) || e.LastName.Contains(name));
            }

            if (departmentId > 0)
            {
                query = query.Where(e => e.DepartmentId == departmentId);
            }

            int totalRecords = query.Count();

            var employees = query
                .OrderByDescending(e => e.Id)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToList();

            return (employees, totalRecords);
        }
    }
}