using System.Web;
using System.Web.Mvc;

namespace ONLINE_SHOPPING.FO
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}