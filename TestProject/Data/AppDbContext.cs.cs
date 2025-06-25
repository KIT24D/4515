using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using TestProject.Models;  

namespace TestProject.Data  
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
        public DbSet<User> Users { get; set; }
    }

}

