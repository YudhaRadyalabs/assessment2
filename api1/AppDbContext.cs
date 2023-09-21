using api1.Entities;
using Microsoft.EntityFrameworkCore;

namespace api1
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public virtual DbSet<UserEntity> Users { get; set; }
    }
}
