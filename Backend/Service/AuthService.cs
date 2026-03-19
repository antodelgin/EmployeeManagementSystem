using Backend.Dto;
using Backend.Mappings;
using Backend.Model;
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

        //public async Task<LoginDto?> ValidateUser(string email, string password )
        //{
        //    var user = await appContext.Users
        //        .FirstOrDefault(u => u.Email == email && u.Password == password );

        //    if (user == null)
        //        return null;

        //    return new LoginDto
        //    {
        //        Email = user.Email
        //    };
        //}

        public async Task<User?> ValidateUser(string email, string password)
        {
            return await appContext.Users
                .FirstOrDefaultAsync(u => u.Email == email && u.Password == password);
        }

        public User Create(User user)
        {
            appContext.Users.Add(user);
            appContext.SaveChanges();
            return user;
        }

        public async Task<User> GetUserById(int id)
        {
            return await appContext.Users.FirstOrDefaultAsync(u => u.Id == id);
        }
    }
}