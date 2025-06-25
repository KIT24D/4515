using System.ComponentModel.DataAnnotations;
using TestProject.Models; // 关键：引用 Role/Permission 所在命名空间

namespace TestProject.Models
{
    public class RolePermissionMapping
    {
        public int RoleID { get; set; }
        public int PermissionID { get; set; }

        public Role Role { get; set; } 
        public Permission Permission { get; set; } 
    }
}