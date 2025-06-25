using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TestProject.Models
{
    public class Requirement
    {
        public int ReqID { get; set; }
        public string ReqTitle { get; set; }
        public string ReqDescription { get; set; }
        public string Status { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public int? AssignedTo { get; set; }
        public DateTime? Deadline { get; set; }

        public User CreatedByUser { get; set; }

        public User AssignedToUser { get; set; }

        public ICollection<Order> Orders { get; set; }
    }
}