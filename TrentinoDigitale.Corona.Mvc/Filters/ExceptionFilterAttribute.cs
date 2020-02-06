using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TrentinoDigitale.Corona.Mvc.Filters
{
    public class ExceptionFilterAttribute: ActionFilterAttribute
    {
        public string ActionName { get; set; }

        public string ControllerName { get; set; }

        public string MethodName { get; set; }

        public string AuthenticatedUserName { get; set; }

        public bool IsAuthenticated { get; set; }

        public IDictionary<string, object> ActionParams { get; set; }

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            ActionName = filterContext.ActionDescriptor.ActionName;
            ControllerName  = filterContext.ActionDescriptor.ControllerDescriptor.ControllerName;
            ActionParams = filterContext.ActionParameters;
            AuthenticatedUserName = filterContext.HttpContext.User.Identity.Name;
            IsAuthenticated = filterContext.HttpContext.User.Identity.IsAuthenticated;

            base.OnActionExecuting(filterContext);
        }
        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            //Scrivo solo se ho eccezione
            if (filterContext.Exception == null) 
            {
                base.OnActionExecuted(filterContext);
                return;
            }

            //Struttura per conservare i dati
            var holder = new
            {
                Controller = ControllerName,
                Action = ActionName,
                Params = ActionParams,
                User = IsAuthenticated 
                    ? AuthenticatedUserName
                    : "Anonymous",
                ExceptionMessage = filterContext.Exception.Message, 
                Stacktrace = filterContext.Exception.ToString()
            };

            //Conversione in json
            JsonSerializerSettings settings = new JsonSerializerSettings();
            settings.ContractResolver = new CamelCasePropertyNamesContractResolver();

            //Conversione in json
            var json = JsonConvert.SerializeObject(
                holder, Formatting.Indented, settings);

            //Scrivo sul file di testo
            string path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory,
                "App_Data", "errors", DateTime.Now.ToString("yyyy-MM-dd"));

            //Se non esiste, crea directory
            if (!Directory.Exists(path))
                Directory.CreateDirectory(path);

            //Nome file e full pash 
            string filename = $"{DateTime.Now.ToString("HH-mm-ss-sss")}.json";
            string fullPath = Path.Combine(path, filename);
            File.WriteAllText(fullPath, json);

            //Esecuzione base
            base.OnActionExecuted(filterContext);
        }
    }
}