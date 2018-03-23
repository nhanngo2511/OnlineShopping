using ONLINE_SHOPPING.FO.Models;
using ONLINE_SHOPPING.ENTITIES;
using ONLINE_SHOPPING.BUSINESS;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ONLINE_SHOPPING.FO.Commons;


namespace ONLINE_SHOPPING.FO.Controllers
{

    //todo: DUNG AJAX TRA VE 2 HTML DUA VAO HAM RENDERVIEWTOSTRING DE UPDATE CART
    //DONE
    public class CartController : Controller
    {

        private string CartSessionName = "CartSession";

        public ActionResult ViewCart()
        {
            var CartSession = Session[CartSessionName];

            List<CartItem> CartItems = CartSession as List<CartItem>;

            float TotalAmount = Cart.UpdateTotalAmount(CartItems);
            ViewBag.TotalAmount = TotalAmount;

            return View(CartItems);
        }

        public ActionResult AddCartItem(int ProductID, int Quantity)
        {
                  
            var CartSession = Session[CartSessionName];

            List<CartItem> CartItems = new List<CartItem>();
            CartItems = CartSession as List<CartItem>;
            CartItems = Cart.AddItem(CartItems, ProductID, Quantity);          

            float TotalAmount = Cart.UpdateTotalAmount(CartItems);
            ViewBag.TotalAmount = TotalAmount;

            Session[CartSessionName] = CartItems;

            return Json(Helpers.ViewUltilities.RenderViewToString02(this, "~/Views/Shared/_CartPartial.cshtml", CartItems), JsonRequestBehavior.AllowGet);
        }

        public ActionResult CartItemList()
        {

            var CartSession = Session[CartSessionName];

            List<CartItem> CartItems = new List<CartItem>();
            CartItems = CartSession as List<CartItem>;

            float TotalAmount = Cart.UpdateTotalAmount(CartItems);
            ViewBag.TotalAmount = TotalAmount;     

            return PartialView("~/Views/Shared/_CartPartial.cshtml", CartItems);

        }

        public JsonResult RemoveCartItem(int ProductID)
        {

            var CartSession = Session[CartSessionName];

            List<CartItem> CartItems = CartSession as List<CartItem>;

            CartItems = Cart.RemoveItem(CartItems, ProductID);

            float TotalAmount = Cart.UpdateTotalAmount(CartItems);
            ViewBag.TotalAmount = TotalAmount;

            Session[CartSessionName] = CartItems;

            string CartHTML = Helpers.ViewUltilities.RenderViewToString02(this, "~/Views/Shared/_CartPartial.cshtml", CartItems);
            string ItemsCartHTML = Helpers.ViewUltilities.RenderViewToString02(this, "~/Views/Shared/_CartItemsPartial.cshtml", CartItems);

            return Json(new { isSuccess = true, CartHTML = CartHTML, ItemsCartHTML = ItemsCartHTML }, JsonRequestBehavior.AllowGet);

        }

        public JsonResult UpdateCartItem(int ProductID, int Quantity)
        {
            var CartSession = Session[CartSessionName];

            List<CartItem> CartItems = CartSession as List<CartItem>;

            CartItems = Cart.UpdateItem(CartItems, ProductID, Quantity);

            float TotalAmount = Cart.UpdateTotalAmount(CartItems);
            ViewBag.TotalAmount = TotalAmount;

            Session[CartSessionName] = CartItems;

            string CartHTML = Helpers.ViewUltilities.RenderViewToString02(this, "~/Views/Shared/_CartPartial.cshtml", CartItems);
            string ItemsCartHTML = Helpers.ViewUltilities.RenderViewToString02(this, "~/Views/Shared/_CartItemsPartial.cshtml", CartItems);

            return Json(new { isSuccess = true, CartHTML = CartHTML, ItemsCartHTML = ItemsCartHTML }, JsonRequestBehavior.AllowGet);
        }


        //public JsonResult GetDistricts(int CityID) {

        //    List<District> Districts = AddressService.GetDistricts(CityID);

        //    return Json(Districts, JsonRequestBehavior.AllowGet);
        //}

        //public JsonResult GetWards(int DistrictID)
        //{
        //    List<Ward> Wards = AddressService.GetWards(DistrictID);

        //    return Json(Wards, JsonRequestBehavior.AllowGet);
        //}
    }
}
