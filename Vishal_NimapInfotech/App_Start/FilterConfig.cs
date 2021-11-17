using System.Web;
using System.Web.Mvc;

namespace Vishal_NimapInfotech
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
