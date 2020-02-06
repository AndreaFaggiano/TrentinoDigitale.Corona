using iText.Kernel.Pdf;
using iText.Layout;
using iText.Layout.Element;
using iText.Layout.Properties;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TrentinoDigitale.Corona.Mvc.Logics.BusinessLayers;
using TrentinoDigitale.Corona.Mvc.Logics.Entities;
using TrentinoDigitale.Corona.Mvc.Models.Home;

namespace TrentinoDigitale.Corona.Mvc.Controllers
{
    public class HomeController : Controller
    {
        [Authorize]
        public ActionResult Index()
        {
            //Utente attualmente autenticato
            string authUserName = User.Identity.Name;

            //Recupero utente dal service layer
            AuthenticationLayer layer = new AuthenticationLayer();
            User user = layer.GetUserByUserName(authUserName);
            if (user == null)
                throw new InvalidProgramException($"L'utente con nome '{User.Identity.Name}' non è stato trovato!");

            //Se sono amministratore, visualizzo "Admin"
            //altrimenti metto "SimpleUser"
            var model = new IndexModel 
            { 
                Ottavio = user.IsAdministrator 
                    ? "Admin"
                    : "SimpleUser", 
                AuthenticatedUserName = user.UserName
            };

            return View(model);
        }

        public ActionResult Paperino()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        [HttpPost]
        public ActionResult GetDescriptionByCode(string code)
        {
            return Json(new { Description = "DESC" + code });
        }

        [HttpPost]
        public ActionResult FetchStructures() 
        {
            var list = new List<dynamic>();
            for (var i = 0; i < 1000; i++) 
            {
                list.Add(new 
                {
                    Codice = $"COD-{i}", 
                    Descrizione = $"Description {i}"
                });
            }
            return Json(list);
        }

        [HttpGet]
        public ActionResult GeneratePdf() 
        {
            string path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory,
                "App_Data", "documents");
            if (!Directory.Exists(path))
                Directory.CreateDirectory(path);

            //Generazione del nome del file
            string file = $"{DateTime.Now.ToString("yyyyMMdd-HHmmss")}.pdf";
            string fullPath = Path.Combine(path, file);

            PdfDocument pdfDoc = new PdfDocument(new PdfWriter(fullPath));
            Document doc = new Document(pdfDoc);

            Table table = new Table(UnitValue.CreatePercentArray(8))
                .UseAllAvailableWidth();

            for (int i = 0; i < 16; i++)
            {
                table.AddCell("hi");
            }

            doc.Add(table);

            doc.Close();

            //Ritorno il file
            return File(fullPath, "application/pdf");
        }
    }
}