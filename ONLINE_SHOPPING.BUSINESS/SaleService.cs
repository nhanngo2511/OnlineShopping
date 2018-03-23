using ONLINE_SHOPPING.BUSINESS;
using ONLINE_SHOPPING.DAL;
using ONLINE_SHOPPING.ENTITIES;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ONLINE_SHOPPING.BUSINESS
{
   public static class SaleService
    {
       static OnlineShoppingDB AccessDB = new ONLINE_SHOPPING.DAL.OnlineShoppingDB();

       public static List<Order> GetOrders(int? ID, string RecipientName, string Phone, int? UserID, int? StatusID, DateTime? StartDate, DateTime? EndDate, int? WardID, int? ShipperID, int? ProductID, int? CategoryID, out int TotalRecords, out float TotalAmount, int PageSize, int Page)
       {
           List<Order> Orders = AccessDB.GetOrders(ID, RecipientName, Phone, UserID, StatusID, StartDate, EndDate, WardID, ShipperID, ProductID, CategoryID, out TotalRecords, out TotalAmount, PageSize, Page);
           return Orders;
       }

       public static Order GetOrderDetails(int OrderID) {
           Order order = AccessDB.GetOrderDetails(OrderID);
           return order;
       }

       public static void SetCancelOrder(int OrderID) {
           AccessDB.SetCancelOrder(OrderID);
       }

       public static void SetShipmentOrder(int OrderID, int ShipperID)
       {
           AccessDB.SetShippingOrder(OrderID, ShipperID);
       }

       public static void SetShippedOrder(int OrderID)
       {
           AccessDB.SetShippedOrder(OrderID);
       }
    }
}
