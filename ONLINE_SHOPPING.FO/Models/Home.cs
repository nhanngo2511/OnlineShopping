using ONLINE_SHOPPING.ENTITIES;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ONLINE_SHOPPING.FO.Models
{
    public class Home
    {

        public List<Product> NewProducts { set; get; }

        public List<Product> TopSellerProducts { set; get; }

        public List<Product> PantProducts { set; get; }

        public List<Product> ShirtProducts { set; get; }

        public List<Product> DryFoods { set; get; }

        public List<Product> WetFoods { set; get; }
    }
}
