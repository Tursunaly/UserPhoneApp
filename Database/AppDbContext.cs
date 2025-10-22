using Microsoft.EntityFrameworkCore;
using UserPhoneApp.Models;

namespace UserPhoneApp.Database
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }
        public DbSet<User> Users { get; set; }
        public DbSet<Phone> Phones { get; set; }
    }
}
