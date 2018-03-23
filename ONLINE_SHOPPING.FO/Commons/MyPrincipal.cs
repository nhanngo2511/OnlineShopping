using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Web;

namespace ONLINE_SHOPPING.FO.Commons
{
    public interface ICustomPincipal : IPrincipal
    {
        int ID { set; get; }
        string FullName { set; get; }

        string[] Roles { set; get; }
    }

    public class MyPrincipal : ICustomPincipal
    {
        public IIdentity Identity { get; private set; }

        public bool IsInRole(string role)
        {
            foreach (string r in Roles)
            {
                if (string.Compare(r, role, true) == 0)
                {
                    return true;
                }
            }
            return false;
        }

        public MyPrincipal(string UserName)
        {
            this.Identity = new GenericIdentity(UserName);
        }

        public int ID { set; get; }

        public string FullName { set; get; }

        public string[] Roles { set; get; }

    }

    public class CustomPrincipalSerializedModel
    {
        public int ID { set; get; }
        public string FullName { set; get; }
        public string[] Roles { set; get; }
    }
}