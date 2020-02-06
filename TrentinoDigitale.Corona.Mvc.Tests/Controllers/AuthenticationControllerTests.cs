using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Web.Mvc;
using TrentinoDigitale.Corona.Mvc.Controllers;
using TrentinoDigitale.Corona.Mvc.Models.Authentication;

namespace TrentinoDigitale.Corona.Mvc.Tests.Controllers
{
    [TestClass]
    public class AuthenticationControllerTests
    {
        [TestMethod]
        public void ShouldSignInBeOkUsingValidCredentialsRedirectToHomePage() 
        {
            //BEHAVIOR: Date delle credenziali valide, mi aspetto che
            //la risposta sia Redirect (304) sulla pagina di home

            //1) SETUP
            //Definzione delle credenziali corrette
            AuthenticationController controller = new AuthenticationController();
            var request = new SignInModel
            {
                UserName = "mario", 
                Password = "password"
            };

            //2) EXECUTION
            //Invocazione del metodo sul controller
            ActionResult result = controller.SignIn(request);
            RedirectToRouteResult redirectResult = (RedirectToRouteResult)result;

            //3) ASSERT
            Assert.IsNotNull(redirectResult);
            Assert.AreEqual("Home", redirectResult.RouteValues["controller"]);
            Assert.AreEqual("Index", redirectResult.RouteValues["action"]);

            //4) CLEANUP
            //In questo caso niente perchè non abbiamo "sporcato"

        }

        [TestMethod]
        public void ShouldSignInBeUnauthorizedWithInvalidCredentials() 
        {
            //BEHAVIOR: In presenza di credenziali errate, devo ottenere
            //la stessa pagina con un messaggio di errore

            //1) SETUP
            AuthenticationController controller = new AuthenticationController();
            var request = new SignInModel
            {
                UserName = "mario",
                Password = "1234567890wrongpassword"
            };

            //2) EXECUTION
            //Invocazione del metodo sul controller
            ActionResult result = controller.SignIn(request);
            ViewResult viewResult = (ViewResult)result;
            SignInModel model = (SignInModel)viewResult.Model;

            //3) ASSERT
            Assert.IsNotNull(viewResult);
            Assert.IsNotNull(model);
            Assert.AreEqual("Le credenziali non sono valide", model.ErrorMessage);
        }
    }
}
