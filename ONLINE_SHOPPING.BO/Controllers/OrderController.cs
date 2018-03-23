using ONLINE_SHOPPING.BO.Commons;
using ONLINE_SHOPPING.BUSINESS;
using ONLINE_SHOPPING.ENTITIES;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ONLINE_SHOPPING.BO.Controllers
{
    public class OrderController : Controller
    {

        [HttpGet]
        [Authorize]
        public ActionResult GetOrders(int? ID, string RecipientName, string Phone, DateTime? StartDate, DateTime? EndDate, int? UserID, int? StatusID, int? WardID, int? ShipperID, int? ProductID, int? CategoryID, int? Page = 1)
        {
            int TotalRecords = 0;
            float TotalAmount = 0;

            List<User> ShipperList = new List<User>();
            ShipperList = UserService.GetShipper();
            
            ViewBag.Shippers = new SelectList(ShipperList, "ID", "FullName");            

            List<Order> Orders = SaleService.GetOrders(ID, RecipientName, Phone, null, StatusID, StartDate, EndDate, WardID, ShipperID, ProductID, CategoryID, out TotalRecords, out TotalAmount, 10, Page.Value);

            ViewBag.TotalRecords = TotalRecords;
            ViewBag.TotalAmount = TotalAmount;
            
            return View(Orders);
        }

        [HttpPost]
        public ActionResult GetOrders(int? ID, string RecipientName, string Phone, DateTime? StartDate, DateTime? EndDate, int? ShipperID, int? ProductID)
        {
            return RedirectToAction("GetOrders", "Order", new { ID = ID, RecipientName = RecipientName, Phone = Phone, StartDate = StartDate, EndDate = EndDate, ShipperID = ShipperID, ProductID = ProductID });
        }

        [Authorize]
        public ActionResult GetOrdersNeedToShipped(int? Page = 1)
        {
            int TotalRecords = 0;
            float TotalAmount = 0;

            List<Order> Orders = SaleService.GetOrders(null, null, null, null, 1, null, null, null, null, null, null, out TotalRecords, out TotalAmount, 10, Page.Value);

            ViewBag.TotalRecords = TotalRecords;
            ViewBag.TotalAmount = TotalAmount;

            return View(Orders);
        }

        public JsonResult CancelOrder(int OrderID)
        {
            if ((User as MyPrincipal).IsInRole("Shipper"))
            {
                return Json(new { isSuccess = false, Message = "Bạn KHÔNG có quyền thao tác HỦY ĐƠN HÀNG lên đơn hàng này" }, JsonRequestBehavior.AllowGet);
            }

            SaleService.SetCancelOrder(OrderID);

            Order order = SaleService.GetOrderDetails(OrderID);

            for (int i = 0; i < order.OrderProducts.Count; i++)
            {
                ProductService.UpdateQuantityProduct(OrderID, (order.OrderProducts[i].Quantity * (-1)));
            }

            string OrderHTML = Helpers.ViewUltilities.RenderViewToString02(this, "~/Views/Shared/_OrderDetailsPartial.cshtml", order);

            return Json(new { isSuccess = true, OrderHTML = OrderHTML }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult SetShipmentOrder(int OrderID)
        {
            if (!(User as MyPrincipal).IsInRole("Shipper"))
            {
                return Json(new { isSuccess = false, Message = "Bạn kHÔNG có quyền thao tác CHO ĐI GIAO lên đơn hàng này" }, JsonRequestBehavior.AllowGet);
            }

            int CurrentShipperID = (User as MyPrincipal).ID;
            SaleService.SetShipmentOrder(OrderID, CurrentShipperID);

            Order order = SaleService.GetOrderDetails(OrderID);

            string OrderHTML = Helpers.ViewUltilities.RenderViewToString02(this, "~/Views/Shared/_OrderDetailsPartial.cshtml", order);

            return Json(new { isSuccess = true, OrderHTML = OrderHTML }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult SetShippedOrder(int OrderID)
        {
            int CurrentShipperID = (User as MyPrincipal).ID;

            Order order = SaleService.GetOrderDetails(OrderID);

            if (!(order.Shipper.ID == CurrentShipperID))
            {
                return Json(new { isSuccess = false, Message = "Bạn KHÔNG có quyền thao tác ĐÃ GIAO lên đơn hàng này" }, JsonRequestBehavior.AllowGet);
            }

            SaleService.SetShippedOrder(OrderID);

            order = SaleService.GetOrderDetails(OrderID);

            string OrderHTML = Helpers.ViewUltilities.RenderViewToString02(this, "~/Views/Shared/_OrderDetailsPartial.cshtml", order);

            return Json(new { isSuccess = true, OrderHTML = OrderHTML }, JsonRequestBehavior.AllowGet);
        }

    }
}
