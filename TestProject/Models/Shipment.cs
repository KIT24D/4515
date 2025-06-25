using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TestProject.Models
{
    public class Shipment
    {
        public int ShipmentID { get; set; }
        public int OrderID { get; set; }
        public string TrackingNumber { get; set; }
        public DateTime ShipmentDate { get; set; }
        public string Carrier { get; set; }
        public string ShipmentStatus { get; set; }

        public Order Order { get; set; }
    }
}