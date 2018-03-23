using ONLINE_SHOPPING.ENTITIES;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ONLINE_SHOPPING.FO.Models
{
   public class Footer
    {
       public Footer() {
           Brands = new List<Brand>();
           Categories = new List<Category>();
       }
        public List<Brand> Brands { set; get; }

        public List<Category> Categories { set; get; }
    }
}
