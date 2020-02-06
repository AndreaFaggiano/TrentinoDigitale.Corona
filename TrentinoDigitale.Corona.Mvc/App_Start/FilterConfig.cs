using System.Web;
using System.Web.Mvc;
using TrentinoDigitale.Corona.Mvc.Filters;

namespace TrentinoDigitale.Corona.Mvc
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
            filters.Add(new DurationFilterAttribute());
            filters.Add(new ExceptionFilterAttribute());
            filters.Add(new ChangeResponseOnErrorFilterAttribute());
        }
    }
}
