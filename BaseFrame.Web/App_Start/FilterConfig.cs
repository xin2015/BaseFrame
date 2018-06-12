using BaseFrame.Web.Attributes;
using System.Web;
using System.Web.Mvc;

namespace BaseFrame.Web
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new SuncereHandleErrorAttribute());
        }
    }
}
