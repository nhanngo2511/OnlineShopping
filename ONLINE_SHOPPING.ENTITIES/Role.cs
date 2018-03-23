using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ONLINE_SHOPPING.ENTITIES
{
    public class Role
    {
        public int ID { get; set; }
        public string Name { get; set; }

        public List<Permission> Permissions { get; set; }
    }
}
