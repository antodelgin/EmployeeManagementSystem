using Backend.Model;
using Microsoft.EntityFrameworkCore;

namespace Backend
{
    public class AppDbContext : DbContext
    {

        public AppDbContext(DbContextOptions<AppDbContext> options): base(options)
        {}
        public DbSet<Employee> Employees { get; set; }
    }
}
