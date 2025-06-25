using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TestProject.Models
{
    public class Inventory
    {
        public int ItemID { get; set; }
        public string ItemName { get; set; }
        public int Quantity { get; set; }
        public int SafetyStock { get; set; }
        public DateTime? LastUpdateDate { get; set; }
        public string ProductDesc { get; set; }
        public string Category { get; set; }
        public string Supplier { get; set; }
        public string ProductName { get; set; }

        public ICollection<OrderDetail> OrderDetails { get; set; }
    }
}
