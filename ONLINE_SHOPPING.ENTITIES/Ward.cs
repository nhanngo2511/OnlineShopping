using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ONLINE_SHOPPING.ENTITIES
{
    public class Ward
    {
        [DisplayName("WardID")]
        public int ID { set; get; }
        public string Name { set; get; }
        public District District { set; get; }
    }
}
