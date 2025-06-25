using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TestProject.Models
{
    public class Permission
    {
        public int PermissionID { get; set; }
        public string PermissionName { get; set; }
        public ICollection<RolePermissionMapping> RolePermissionMappings { get; set; }
    }
}
