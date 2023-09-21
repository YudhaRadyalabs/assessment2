using api2.Entities;
using Microsoft.EntityFrameworkCore;

namespace api2
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public virtual DbSet<UserEntity> Users { get; set; }
    }
}
