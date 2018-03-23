using ONLINE_SHOPPING.BUSINESS;
using ONLINE_SHOPPING.ENTITIES;
using ONLINE_SHOPPING.FO.Commons;
using ONLINE_SHOPPING.FO.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ONLINE_SHOPPING.FO.Controllers
{
    public class SaleController : Controller
    {
        private string CartSessionName = "CartSession";

        [Authorize(Roles = "User")]
        [HttpGet]
        public ActionResult Payment()
        {

            int UserID = (User as MyPrincipal).ID;
            User us = UserService.GetUser(UserID);

            Order order = new Order();
            order.Address = us.Address;
            order.RecipientName = us.FullName;
            order.Phone = us.PhoneNumber;
            order.Ward = us.Ward;

            //List<City> Cities = AddressService.GetCities();
            //List<District> Districts = AddressService.GetDistricts(us.Ward.District.City.ID);
            //List<Ward> Wards = AddressService.GetWards(us.Ward.District.ID);

            List<City> Cities = Cache.CacheGetCities();
            List<District> Districts = Cache.CacheGetDistricts(us.Ward.District.City.ID);
            List<Ward> Wards = Cache.CacheGetWards(us.Ward.District.ID);

            ViewBag.Cities = new SelectList(Cities, "ID", "Name", us.Ward.District.City.ID);
            ViewBag.Districts = new SelectList(Districts, "ID", "Name", us.Ward.District.ID);
            ViewBag.Wards = new SelectList(Wards, "ID", "Name", us.Ward.ID);

            return View(order);
        }

        [HttpPost]
        public ActionResult Payment(Order order)
        {
            int UserID = (User as MyPrincipal).ID;

            if (!ModelState.IsValid)
            {
                List<City> Cities = AddressService.GetCities();
                List<District> Districts = AddressService.GetDistricts(order.Ward.District.City.ID);
                List<Ward> Wards = AddressService.GetWards(order.Ward.District.ID);


                ViewBag.Cities = new SelectList(Cities, "ID", "Name", order.Ward.District.City.ID);
                ViewBag.Districts = new SelectList(Districts, "ID", "Name", order.Ward.District.ID);
                ViewBag.Wards = new SelectList(Wards, "ID", "Name", order.Ward.ID);

                return View(order);
            }

            var CartSession = Session[CartSessionName];
            List<CartItem> CartItems = CartSession as List<CartItem>;

            // get total amount
            float TotalAmount = Cart.UpdateTotalAmount(CartItems);

            string StrCartItems = "";
            for (int i = 0; i < CartItems.Count; i++)
            {               
                StrCartItems += CartItems[i].Product.ID + "," + CartItems[i].Quantity + "|";
            }

            CartService.CreateOrder(order.RecipientName, order.Phone, order.Address, order.Ward.ID, UserID, StrCartItems);
           
            Session[CartSessionName] = null;

            return RedirectToAction("Thankyou", "Sale");
        }

        [HttpGet]
        [Authorize(Roles = "User")]
        public ActionResult OrderList(int? Page = 1)
        {
            int TotalRecords = 0;
            float TotalAmount = 0;

            int UserID = (User as MyPrincipal).ID;

            List<Order> Orders = SaleService.GetOrders(null, null, null, UserID, null, null, null, null, null, null, null, out TotalRecords, out TotalAmount, 6, Page.Value);

            ViewBag.TotalRecords = TotalRecords;
            ViewBag.TotalAmount = TotalAmount;

            return View(Orders);
        }

        [HttpPost]
        public JsonResult CancelOrder(int OrderID)
        {
            SaleService.SetCancelOrder(OrderID);

            Order order = SaleService.GetOrderDetails(OrderID);

            for (int i = 0; i < order.OrderProducts.Count; i++)
            {
                ProductService.UpdateQuantityProduct(OrderID, (order.OrderProducts[i].Quantity * (-1)));
            }

            string OrderHTML = Helpers.ViewUltilities.RenderViewToString02(this, "~/Views/Shared/_OrderAfterCancel.cshtml", order);

            return Json(new { isSuccess = true, OrderHTML = OrderHTML }, JsonRequestBehavior.AllowGet);
        }

        [Authorize(Roles = "User")]
        public ActionResult OrderDetails(int OrderID)
        {
            Order order = SaleService.GetOrderDetails(OrderID);

            return View(order);
        }

        [Authorize(Roles = "User")]
        public ActionResult Thankyou()
        {
            return View();
        }
    }
}
