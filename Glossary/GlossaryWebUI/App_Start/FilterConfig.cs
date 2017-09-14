using System.Web;
using System.Web.Mvc;

namespace GlossaryWebUI
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new GlossaryWebUI.Filters.ExceptionHandlingFilter());
            filters.Add(new HandleErrorAttribute());
        }
    }
}
