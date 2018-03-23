using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ONLINE_SHOPPING.ENTITIES
{ // TODO: tang Entities chi de mo phong lai du lieu duoi database
    // Con khong phai nhu vay thi o tang model theo project do
    public class Image
    {
        public int ID { set; get; }
        public int ProductID { set; get; }

        public string Name { set; get; }
        
    }
}
