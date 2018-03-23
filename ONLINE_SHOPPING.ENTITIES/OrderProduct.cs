using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ONLINE_SHOPPING.ENTITIES
{
    public class OrderProduct
    {
        public Order Order { set; get; }
        public Product Product { set; get; }
        public int Quantity { set; get; }
    }
}
