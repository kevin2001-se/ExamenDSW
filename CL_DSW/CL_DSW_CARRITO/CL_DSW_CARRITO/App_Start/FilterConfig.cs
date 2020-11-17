using System.Web;
using System.Web.Mvc;

namespace CL_DSW_CARRITO
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
