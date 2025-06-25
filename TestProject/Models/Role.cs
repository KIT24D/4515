using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TestProject.Models
{
    public class Role
    {
        public int RoleID { get; set; }
        public string RoleName { get; set; }


        public ICollection<RolePermissionMapping> RolePermissionMappings { get; set; }
            = new List<RolePermissionMapping>();
    }
}
