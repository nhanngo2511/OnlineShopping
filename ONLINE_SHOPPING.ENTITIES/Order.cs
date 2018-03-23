using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ONLINE_SHOPPING.ENTITIES
{
    public class Order
    {
        public int ID { set; get; }

        [Required]
        public string RecipientName { set; get; }
        [Required]
        public string Phone { set; get; }
        [Required]
        public string Address { set; get; }
        [Required]
        public float TotalAmount { set; get; }
        [Required]
        public Ward Ward { set; get; }

        public OrderStatus OrderStatus { set; get; }
        public User Shipper { set; get; }
        public DateTime? ShipmentTime { set; get; }
        public DateTime? ShippedTime { set; get; }
        public User User { set; get; }
        public DateTime CreationTime { set; get; }

        public List<OrderProduct> OrderProducts { set; get; }
    }
}
