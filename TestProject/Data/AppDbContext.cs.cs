using Microsoft.EntityFrameworkCore;
using TestProject.Models;

namespace TestProject.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        // 实体类映射 - 对应数据库表
        public DbSet<User> Users { get; set; }
        public DbSet<Inventory> Inventory { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderDetail> OrderDetails { get; set; }
        public DbSet<Requirement> Requirements { get; set; }
        public DbSet<CustomerRequirement> CustomerRequirements { get; set; }
        public DbSet<Shipment> Shipments { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Permission> Permissions { get; set; }
        public DbSet<RolePermissionMapping> RolePermissionMappings { get; set; }
        public DbSet<UserRoleMapping> UserRoleMappings { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // 配置主键和表关系
            modelBuilder.Entity<User>()
                .HasKey(u => u.UserID);

            modelBuilder.Entity<Inventory>()
                .HasKey(i => i.ItemID);

            modelBuilder.Entity<Order>()
                .HasKey(o => o.OrderID);

            modelBuilder.Entity<OrderDetail>()
                .HasKey(od => od.DetailID);

            modelBuilder.Entity<Requirement>()
                .HasKey(r => r.ReqID);

            modelBuilder.Entity<CustomerRequirement>()
                .HasKey(cr => cr.RequirementID);

            modelBuilder.Entity<Shipment>()
                .HasKey(s => s.ShipmentID);

            modelBuilder.Entity<Role>()
                .HasKey(r => r.RoleID);

            modelBuilder.Entity<Permission>()
                .HasKey(p => p.PermissionID);

            modelBuilder.Entity<RolePermissionMapping>()
                .HasKey(rpm => new { rpm.RoleID, rpm.PermissionID });

            modelBuilder.Entity<UserRoleMapping>()
                .HasKey(urm => new { urm.UserID, urm.RoleID });

            // 配置外键关系（基于导航属性）
            modelBuilder.Entity<Order>()
                .HasOne(o => o.User)
                .WithMany(u => u.Orders)
                .HasForeignKey(o => o.CustomerID);

            modelBuilder.Entity<OrderDetail>()
                .HasOne(od => od.Order)
                .WithMany(o => o.OrderDetails)
                .HasForeignKey(od => od.OrderID);

            modelBuilder.Entity<OrderDetail>()
                .HasOne(od => od.InventoryItem)
                .WithMany(i => i.OrderDetails)
                .HasForeignKey(od => od.ItemID);

            modelBuilder.Entity<Order>()
                .HasOne(o => o.Requirement)
                .WithMany(r => r.Orders)
                .HasForeignKey(o => o.RequirementID);

            modelBuilder.Entity<Requirement>()
                .HasOne(r => r.CreatedByUser)
                .WithMany(u => u.CreatedRequirements)
                .HasForeignKey(r => r.CreatedBy);

            modelBuilder.Entity<Requirement>()
                .HasOne(r => r.AssignedToUser)
                .WithMany(u => u.AssignedRequirements)
                .HasForeignKey(r => r.AssignedTo);

            modelBuilder.Entity<CustomerRequirement>()
                .HasOne(cr => cr.User)
                .WithMany(u => u.CustomerRequirements)
                .HasForeignKey(cr => cr.UserID);

            modelBuilder.Entity<Shipment>()
                .HasOne(s => s.Order)
                .WithMany(o => o.Shipments)
                .HasForeignKey(s => s.OrderID);

            modelBuilder.Entity<RolePermissionMapping>()
                .HasOne(rpm => rpm.Role)
                .WithMany(r => r.RolePermissionMappings)
                .HasForeignKey(rpm => rpm.RoleID);

            modelBuilder.Entity<RolePermissionMapping>()
                .HasOne(rpm => rpm.Permission)
                .WithMany(p => p.RolePermissionMappings)
                .HasForeignKey(rpm => rpm.PermissionID);

            modelBuilder.Entity<UserRoleMapping>()
                .HasOne(urm => urm.User)
                .WithMany(u => u.UserRoleMappings)
                .HasForeignKey(urm => urm.UserID);

            modelBuilder.Entity<UserRoleMapping>()
                .HasOne(urm => urm.Role)
                .WithMany(r => r.UserRoleMappings)
                .HasForeignKey(urm => urm.RoleID);

            base.OnModelCreating(modelBuilder);
        }
    }
}

