using ONLINE_SHOPPING.BO.Commons;
using ONLINE_SHOPPING.BUSINESS;
using ONLINE_SHOPPING.ENTITIES;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using System.Web.Security;


namespace ONLINE_SHOPPING.BO.Controllers
{

    public class AccountController : Controller
    {
        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

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

            //-----------------------------------

            return Redirect(returnURL ?? Url.Action("GetProducts", "Product"));

        }

        [Authorize]
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Login", "Account");
        }

        public ActionResult CreateAccount()
        {
            return View();
        }

        [HttpPost]      
        public ActionResult CreateAccount(User us, int WardID, int RoleID)
        {
            if (!ModelState.IsValid)
            {
                return View(User);
            }

            UserService.CreateUser(us.UserName, us.Password, us.FullName, us.Age, us.Gender, us.PhoneNumber, us.Email, us.Address, WardID, RoleID);
            return RedirectToAction("Login", "Account", us);
        }
      
    }
}
