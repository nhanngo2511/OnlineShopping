using ONLINE_SHOPPING.ENTITIES;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


namespace ONLINE_SHOPPING.BO.Commons
{
    public static class UserLogin
    {
        public static bool LoggedIn(User us)
        {
            var session = (User)Session[Commons.Constant.UserSession];

            if (Session[Commons.Constant.UserSession] != null)
            {
                
            }

        }


    }
}