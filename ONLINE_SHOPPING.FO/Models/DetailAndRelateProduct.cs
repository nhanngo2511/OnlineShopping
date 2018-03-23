using ONLINE_SHOPPING.ENTITIES;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ONLINE_SHOPPING.FO.Models
{
    public class DetailAndRelateProduct
    {
        public Product ProductDetails { set; get; }

        public List<Product> RelateProducts { set; get; }
    }
}
