using System.ComponentModel.DataAnnotations;

namespace TestProject.Models
{
    public class OrderDetail
    {
        public int DetailID { get; set; }
        public int? OrderID { get; set; }
        public int? ItemID { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }

        public Order Order { get; set; }
        public Inventory InventoryItem { get; set; }
    }
}
