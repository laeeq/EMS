using Microsoft.EntityFrameworkCore;
using WebApi.Models;

namespace WebApi.Database
{
    public class DatabaseContext : DbContext
    {
        //public DatabaseContext(string connectionString) : base(connectionString) { }
        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options) { }
        public DbSet<Employee> Employees { get; set; }
    }
}
