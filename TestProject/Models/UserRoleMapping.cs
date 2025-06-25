using System.ComponentModel.DataAnnotations;

namespace TestProject.Models
{
    public class UserRoleMapping
    {
        public int UserID { get; set; }
        public int RoleID { get; set; }

        public User User { get; set; }

        public Role Role { get; set; }
    }
}
