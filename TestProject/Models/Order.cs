using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TestProject.Models
{
    public class Order
    {
        public int OrderID { get; set; }
        public int? CustomerID { get; set; }
        public string OrderStatus { get; set; }
        public decimal TotalAmount { get; set; }
        public DateTime? OrderDate { get; set; }
        public string DeliveryAddress { get; set; }
        public int? RequirementID { get; set; }

        // 导航属性：关联的用户
        public User User { get; set; }
        // 导航属性：关联的订单详情
        public ICollection<OrderDetail> OrderDetails { get; set; }
        // 导航属性：关联的需求
        public Requirement Requirement { get; set; }
        // 导航属性：关联的发货单
        public ICollection<Shipment> Shipments { get; set; }
    }
}