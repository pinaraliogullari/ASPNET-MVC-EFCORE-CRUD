using ASPNETMVC_EFCORE_CRUD.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace ASPNETMVC_EFCORE_CRUD.Data
{
    public class MvcDemoDbContext : DbContext
    {
        public MvcDemoDbContext(DbContextOptions options) : base(options)
        {
        }

     public DbSet<Employee> Employees { get; set; }
    }
}
