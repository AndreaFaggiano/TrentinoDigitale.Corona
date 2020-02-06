using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TrentinoDigitale.Corona.Mvc.Filters
{
    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
    public class DurationFilterAttribute: ActionFilterAttribute
    {
        private DateTime Start { get; set; }
        private TimeSpan Duration { get; set; }

        /// <summary>
        /// Questo viene invocato PRIMA che la action sia eseguita
        /// </summary>
        /// <param name="filterContext"></param>
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            //Imposto la data attuale
            Start = DateTime.UtcNow;

            base.OnActionExecuting(filterContext);
        }

        /// <summary>
        /// Invocato prima che il risultato sia generato
        /// </summary>
        /// <param name="filterContext"></param>
        public override void OnResultExecuting(ResultExecutingContext filterContext)
        {
            base.OnResultExecuting(filterContext);
        }

        /// <summary>
        /// Invocato dopo che il risultato è stato generato
        /// </summary>
        /// <param name="filterContext"></param>
        public override void OnResultExecuted(ResultExecutedContext filterContext)
        {
            base.OnResultExecuted(filterContext);
        }

        /// <summary>
        /// Invocato dopo che la action è stata completata
        /// </summary>
        /// <param name="filterContext"></param>
        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            //Calcolo la durata trascorsa tra inizio e fine azione
            Duration = DateTime.UtcNow.Subtract(Start);

            //Nome della action e del controller
            string actionName = filterContext.ActionDescriptor.ActionName;
            string controllerName = filterContext.ActionDescriptor.ControllerDescriptor.ControllerName;

            //Scrivo sul file di testo
            string path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory,
                "App_Data", "durations", DateTime.Now.ToString("yyyy-MM-dd"));
            
            //Se non esiste, crea directory
            if (!Directory.Exists(path))
                Directory.CreateDirectory(path);

            //Nome file e full pash 
            string filename = $"{controllerName}-{actionName}.trace";
            string fullPath = Path.Combine(path, filename);

            ////Se il file non esiste, lo creo
            //if (!File.Exists(fullPath)) 
            //{
            //    File.CreateText(fullPath);
            //}

            //Composizione del contenuto
            string message = $"{DateTime.Now.ToString("HH:mm:ss")} " + 
                $"=> {Duration.TotalMilliseconds} ms\n";

            //Append sul file esistente
            File.AppendAllText(fullPath, message);

            base.OnActionExecuted(filterContext);
        }
    }
}