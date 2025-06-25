using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TestProject.Models
{
    public class User
    {
       
        public int Id { get; set; }
        public string UserName { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public string Role { get; set; } = "User";
        public string CompanyName { get; set; } = string.Empty;
        public string ContactInfo { get; set; } = string.Empty;
        public DateTime CreatedDate { get; set; } = DateTime.UtcNow;
        public string Department { get; set; } = string.Empty;
        public bool? IsApproved { get; set; } = false;

   
        public ICollection<Order> Orders { get; set; } = new List<Order>();
        public ICollection<Requirement> CreatedRequirements { get; set; } = new List<Requirement>();
        public ICollection<Requirement> AssignedRequirements { get; set; } = new List<Requirement>();
        public ICollection<CustomerRequirement> CustomerRequirements { get; set; } = new List<CustomerRequirement>();
        public ICollection<UserRoleMapping> UserRoleMappings { get; set; } = new List<UserRoleMapping>();
    }
}