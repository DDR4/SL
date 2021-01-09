using System.Web;
using System.Web.Mvc;

namespace WebSL
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
            filters.Add(new Core.Filter.CustomAuthorizeAttribute());
        }
    }
}
