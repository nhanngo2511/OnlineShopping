using ONLINE_SHOPPING.BUSINESS;
using ONLINE_SHOPPING.ENTITIES;
using ONLINE_SHOPPING.FO.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Providers.Entities;


namespace ONLINE_SHOPPING.FO.Commons
{
    public static class Cart
    {

        public static float UpdateTotalAmount(List<CartItem> CartItems)
        {
            float TotalAmount = 0;

            if (CartItems == null || CartItems.Count == 0)
            {
                return TotalAmount;
            }

            for (int i = 0; i < CartItems.Count; i++)
            {
                TotalAmount += CartItems[i].Quantity * CartItems[i].Product.Price;
            }

            return TotalAmount;
        }

        public static List<CartItem> AddItem(List<CartItem> cartitems, int ProductID, int Quantity)
        {

            Product Product = ProductService.GetProduct(ProductID);

            CartItem CartItem = new CartItem();
            CartItem.Product = Product;
            CartItem.Quantity = Quantity;

            if (cartitems == null)
            {
                cartitems = new List<CartItem>();
                cartitems.Add(CartItem);

                return cartitems;
            }

            if (!cartitems.Any(x => x.Product.ID == ProductID))
            {
                cartitems.Add(CartItem);

                return cartitems;
            }


            for (int i = 0; i < cartitems.Count; i++)
            {
                if ((cartitems[i].Product.ID == ProductID))
                {
                    cartitems[i].Quantity += Quantity;
                    break;
                }
            }
            return cartitems;


        }

        public static List<CartItem> RemoveItem(List<CartItem> cartitems, int ProductID)
        {
            for (int i = 0; i < cartitems.Count; i++)
            {
                if (cartitems[i].Product.ID == ProductID)
                {
                    cartitems.RemoveAt(i);
                    break;
                }
            }

            return cartitems;
        }

        public static List<CartItem> UpdateItem(List<CartItem> cartitems, int ProductID, int Quantity)
        {
            for (int i = 0; i < cartitems.Count; i++)
            {
                if ((cartitems[i].Product.ID == ProductID))
                {
                    cartitems[i].Quantity = Quantity;
                }
            }
            return cartitems;
        }
    }
}