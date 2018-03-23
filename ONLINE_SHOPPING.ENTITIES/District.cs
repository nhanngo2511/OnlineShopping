using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ONLINE_SHOPPING.ENTITIES
{
    public class District
    {
     
        [DisplayName("DistrictID")]
        public int ID { set; get; }
        public string Name { set; get; }

        public City City { set; get; }
    }
}
