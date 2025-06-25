using Microsoft.EntityFrameworkCore;
using TestProject.Models;

namespace TestProject.Data // 关键：命名空间必须为 TestProject.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        // 实体集定义（根据 Models 补充完整）
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        // 其他实体集...

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // 配置外键关系（根据需求补充）
            modelBuilder.Entity<User>()
                .HasMany(u => u.Orders)
                .WithOne(o => o.User)
                .HasForeignKey(o => o.CustomerID);
        }
    }
}

