using ONLINE_SHOPPING.BUSINESS;
using ONLINE_SHOPPING.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ONLINE_SHOPPING.BUSINESS
{
    public static class CartService
    {

        static OnlineShoppingDB AccessDB = new ONLINE_SHOPPING.DAL.OnlineShoppingDB();

        public static void CreateOrder(string RecipientName, string Phone, string Address, int WardID, int UserID, string StrCartItems) {
            AccessDB.CreateOrder(RecipientName, Phone, Address, WardID, UserID, StrCartItems);
        }
        
    }
}
