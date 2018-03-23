using ONLINE_SHOPPING.DAL;
using ONLINE_SHOPPING.ENTITIES;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ONLINE_SHOPPING.BUSINESS
{
   public static class UserService
    {
       static OnlineShoppingDB AccessDB = new ONLINE_SHOPPING.DAL.OnlineShoppingDB();

       public static bool FindUser (User user, out int ID){

           
           bool result = AccessDB.FindUser(user.UserName, user.Password, out ID);
           user.ID = ID;

           return result;
        }

       public static User GetUser(int ID) {
           User us = AccessDB.GetUser(ID);
           return us;
       }

       public static void CreateUser(string UserName, string Password, string FullName, int Age, bool Gender, string PhoneNumber, string Email, string Address, int WardID, int RoleID)
       {
           AccessDB.CreateUser( UserName, Password, FullName, Age, Gender, PhoneNumber, Email, Address, WardID, RoleID);
       }

       public static bool CheckExistUserName(string username) {
           bool result = AccessDB.CheckExistUserName(username);
           return result;
       }

       public static List<User> GetShipper()
       {
           List<User> Shippers = AccessDB.GetShippers();

           return Shippers;

       }

    }
}
