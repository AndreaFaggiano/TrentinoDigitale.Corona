using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Threading;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using TrentinoDigitale.Corona.Mvc.Filters;
using TrentinoDigitale.Corona.Mvc.Logics.BusinessLayers;
using TrentinoDigitale.Corona.Mvc.Logics.Entities;
using TrentinoDigitale.Corona.Mvc.Models.Authentication;

namespace TrentinoDigitale.Corona.Mvc.Controllers
{
    //Authentication/SignIn
    //Acceso/Login

    public class AuthenticationController : Controller
    {        
        [HttpGet]
        public ActionResult SignIn() 
        {
            SignInModel model = new SignInModel 
            {
            };

            return View(model);
        }

        [HttpGet]
        public ActionResult Autocomplete() 
        {
            SignInModel model = new SignInModel
            {
                UserName = "mauro", 
                Password = "password"
            };

            return View("SignIn", model);
        }

        [HttpPost]
        public ActionResult SignIn(SignInModel model) 
        {
            //Validazione dell'input
            if (model == null)
            {
                model.ErrorMessage = "Devi mandarmi un body!";
                return View(model);
            }                

            //Se ci sono dei dati, gestisco i valori
            if (!ModelState.IsValid)
                return View(ModelState);

            //Qui sono sicuro che username e password
            AuthenticationLayer layer = new AuthenticationLayer();
            User user = layer.SignIn(model.UserName, model.Password);

            //Se non ho l'utente rimando la stessa pagina
            if (user == null)
            {
                model.ErrorMessage = "Le credenziali non sono valide";
                return View(model);
            }

            FormsAuthentication.SetAuthCookie(user.UserName, false);

            //GenericIdentity identity = new GenericIdentity(user.UserName);
            //GenericPrincipal principal = new GenericPrincipal(identity, new string[] { });
            //Thread.CurrentPrincipal = principal;
                
            //Altrimenti ridirigo alla home
            return RedirectToAction("Index", "Home");            
        }
    }
}