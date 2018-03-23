using Facebook;
using ONLINE_SHOPPING.BUSINESS;
using ONLINE_SHOPPING.ENTITIES;
using ONLINE_SHOPPING.FO.Commons;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using System.Web.Security;
using System.Web.UI;

namespace ONLINE_SHOPPING.FO.Controllers
{
    public class UserController : Controller
    {

        private Uri RedirectUri
        {
            get
            {
                var uriBuilder = new UriBuilder(Request.Url);
                uriBuilder.Query = null;
                uriBuilder.Fragment = null;
                uriBuilder.Path = Url.Action("FacebookCallback");
                return uriBuilder.Uri;
            }
        }

        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        public ActionResult LoginFacebook() {
            var fb = new FacebookClient();
            var loginUrl = fb.GetLoginUrl(new {
                client_id = ConfigurationManager.AppSettings["FbAppID"],
                client_secrect = ConfigurationManager.AppSettings["FbAppSecret"],
                redirec_uri = RedirectUri.AbsoluteUri,
                response_type = "code",
                scope = "email",
            });

            return Redirect(loginUrl.AbsoluteUri);
        }

        //[HttpPost]
        //public ActionResult LoginFacebook()
        //{
        //    var fb = new FacebookClient();
        //    var loginURL = fb.GetLoginUrl(new { 
        //        client_id = ConfigurationManager.AppSettings["FbAppID"],
        //        clien_secret = ConfigurationManager.AppSettings["FbAppSecret"],
        //        response_type = "code",
        //        scope = "email"
        //    });

        //    return Redirect(loginURL.AbsoluteUri);

        //}

        [HttpPost]
        public ActionResult Login(string UserName, string Password, string returnURL)
        {

            User user = new User();
            user.UserName = UserName;
            user.Password = Password;

            int UserID;
            bool isExistUser = UserService.FindUser(user, out UserID);

            if (!isExistUser)
            {
                ModelState.AddModelError("", "UserName or Password was wrong");
                return View(user);
            }

            User us = UserService.GetUser(UserID);
            //string roles = string.Join(",", us.Roles.ToArray());

            //--------------My Principal--------------------

            CustomPrincipalSerializedModel serializeModel = new CustomPrincipalSerializedModel();
            serializeModel.ID = us.ID;
            serializeModel.FullName = us.FullName;
            serializeModel.Roles = us.Roles.ToArray();      

            JavaScriptSerializer selialier = new JavaScriptSerializer();

            string UserData = selialier.Serialize(serializeModel);

            //----------------------------------

            FormsAuthenticationTicket authTicket = new FormsAuthenticationTicket(
              1,
              us.ID.ToString(),
              DateTime.Now,
              DateTime.Now.AddMinutes(20),
              false,
              UserData,
              "/");

            //----------My Principal---------------

            string encTiket = FormsAuthentication.Encrypt(authTicket);
            HttpCookie faCookie = new HttpCookie(FormsAuthentication.FormsCookieName, encTiket);
            Response.Cookies.Add(faCookie);

            return Redirect(returnURL ?? Url.Action("Home", "ViewSite"));

        }

        [Authorize]
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            Session["CartSession"] = null;
            return RedirectToAction("Login", "User");
        }

        public JsonResult CheckExistUserName(string username) {
            bool IsExist = UserService.CheckExistUserName(username);
          
            return Json(new { result = IsExist });
        }

        [HttpGet]
        public ActionResult Register()
        {

           // List<City> Cities = AddressService.GetCities();
           // List<District> Districts = AddressService.GetDistricts(Cities[0].ID);
           // List<Ward> Wards = AddressService.GetWards(Districts[0].ID);

            List<City> Cities = Cache.CacheGetCities();
            List<District> Districts = Cache.CacheGetDistricts(Cities[0].ID);
            List<Ward> Wards = Cache.CacheGetWards(Districts[0].ID);

            ViewBag.Cities = new SelectList(Cities, "ID", "Name", 0);
            ViewBag.Districts = new SelectList(Districts, "ID", "Name", 0);
            ViewBag.Wards = new SelectList(Wards, "ID", "Name", 0);

            return View();
        }

        [HttpPost]
        public ActionResult Register(User us)
        {
            if (!ModelState.IsValid)
            {

                List<City> Cities = AddressService.GetCities();
                List<District> Districts = AddressService.GetDistricts(us.Ward.District.City.ID);
                List<Ward> Wards = AddressService.GetWards(us.Ward.District.ID);

                ViewBag.Cities = new SelectList(Cities, "ID", "Name");
                ViewBag.Districts = new SelectList(Districts, "ID", "Name");
                ViewBag.Wards = new SelectList(Wards, "ID", "Name");

                return View(us);
            }

            UserService.CreateUser(us.UserName, us.Password, us.FullName, us.Age, us.Gender, us.PhoneNumber, us.Email, us.Address, us.Ward.ID, 3);

            return RedirectToAction("Login", "User");
        }

        [OutputCache(Duration = 60, Location=System.Web.UI.OutputCacheLocation.Client, VaryByParam = "none", NoStore = true)]
        public ActionResult ViewProfile()
        {
            int UserID = (User as MyPrincipal).ID;

            User us = UserService.GetUser(UserID);

            return View(us);
        }

        public JsonResult GetDistricts(int CityID)
        {

            //List<District> Districts = AddressService.GetDistricts(CityID);

            List<District> Districts = Cache.CacheGetDistricts(CityID);

            return Json(Districts, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetWards(int DistrictID)
        {
            //List<Ward> Wards = AddressService.GetWards(DistrictID);

            List<Ward> Wards = Cache.CacheGetWards(DistrictID);

            return Json(Wards, JsonRequestBehavior.AllowGet);
        }


        [OutputCache(Duration = 60, Location = System.Web.UI.OutputCacheLocation.Client, VaryByParam = "none", NoStore = true)]
        public ActionResult Demo(){
            return Content("123");
        }

    }
}
