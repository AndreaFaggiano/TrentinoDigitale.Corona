using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TrentinoDigitale.Corona.Mvc.Models.Rilevazioni;

namespace TrentinoDigitale.Corona.Mvc.Controllers
{
    public class RilevazioniController : Controller
    {        
        [HttpGet]
        public ActionResult Index()
        {
            IndexModel model = new IndexModel
            {
                FromDate = DateTime.Now.AddDays(-30),
                ToDate = DateTime.Now.AddDays(30),
                UserName = "mauro",
                AllCodes = new List<SelectListItem>()
            };
            model.AllCodes = GenerateItems();
            return View(model);
        }

        private IList<SelectListItem> GenerateItems() 
        {
            var all = new List<SelectListItem>();
            for (int i = 0; i < 10; i++)
            {
                var item = new SelectListItem();
                item.Value = i.ToString();
                item.Text = "CODE " + i;
                all.Add(item);
            }
            return all;
        }


        [HttpPost]
        public ActionResult Index(IndexModel model) 
        {
            //Se ho una selezione del codice
            if (!string.IsNullOrEmpty(model.SelectedCode)) 
            {
                model.SelectedCodeDescription = "Descri " + model.SelectedCode;
                model.AllCodes = GenerateItems();
                return View(model);
            }

            model.AllCodes = GenerateItems();
            return View(model);

        }

        [HttpPost]
        public ActionResult CompilaCodice(IndexModel model) 
        {
            //Se ho una selezione del codice
            if (!string.IsNullOrEmpty(model.SelectedCode))
            {
                model.SelectedCodeDescription = "Descri " + model.SelectedCode;
                model.AllCodes = GenerateItems();
                return View(model);
            }

            model.AllCodes = GenerateItems();
            return View(model);
        }
    }
}