using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TestProject.Models
{
    public class User
    {
        public int UserID { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Role { get; set; }
        public string CompanyName { get; set; }
        public string ContactInfo { get; set; }
        public DateTime CreatedDate { get; set; }
        public string Department { get; set; }
        public bool? IsApproved { get; set; }


        public ICollection<Order> Orders { get; set; }
        public ICollection<Requirement> CreatedRequirements { get; set; }
        public ICollection<Requirement> AssignedRequirements { get; set; }
        public ICollection<CustomerRequirement> CustomerRequirements { get; set; }
        public ICollection<UserRoleMapping> UserRoleMappings { get; set; }
    }
}
