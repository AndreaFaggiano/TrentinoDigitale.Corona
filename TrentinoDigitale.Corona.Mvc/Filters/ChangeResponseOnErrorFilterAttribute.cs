using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TrentinoDigitale.Corona.Mvc.Filters
{
    public class ChangeResponseOnErrorFilterAttribute : ActionFilterAttribute
    {
        /// <summary>
        /// Invocato dopo che la action è stata completata
        /// </summary>
        /// <param name="filterContext"></param>
        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            //Scrivo solo se ho eccezione
            if (filterContext.Exception == null)
            {
                base.OnActionExecuted(filterContext);
                return;
            }

            var defaultResultOnError = new HttpStatusCodeResult(504, "Eccezione server!");

            filterContext.Result = defaultResultOnError;

            base.OnActionExecuted(filterContext);

            filterContext.HttpContext.Response.Flush();
        }

    }
}