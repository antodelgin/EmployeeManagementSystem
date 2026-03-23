using Backend.Dto;
using Backend.Mappings;
using Backend.Model;
using Microsoft.AspNetCore.Http.HttpResults;
using Backend.Exceptions;


//using Backend.Exceptions;
//using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
//using Backend.Repository;

namespace Backend.Service
{
    public class AuthService
    {
        private readonly AppDbContext appContext;

        public AuthService(AppDbContext context)
        {
            appContext = context;
        }


        public async Task<User?> ValidateUser(string email, string password)
        {
            return await appContext.Users
                .FirstOrDefaultAsync(u => u.Email == email && u.Password == password);
        }

        public async Task<User> Create(User user)
        {
            var employee = await appContext.Employees
                .FirstOrDefaultAsync(e => e.Id == user.EmployeeId);

            if (employee == null)
                throw new ApplicationException("Employee not found");

            if (employee.Email != user.Email)
                throw new ApplicationException("Employee email does not match");

            var existingUser = await appContext.Users
                .AnyAsync(u => u.EmployeeId == user.EmployeeId);

            if (existingUser) throw new EmailAlreadyExistsException();
                //throw new ApplicationException("User already exists for this employee");

            user.Employee = employee;

            appContext.Users.Add(user);
            await appContext.SaveChangesAsync();

            return user;
        }

        public async Task<User> GetUserById(int id)
        {
            return await appContext.Users.FirstOrDefaultAsync(u => u.Id == id);
        }
    }
}