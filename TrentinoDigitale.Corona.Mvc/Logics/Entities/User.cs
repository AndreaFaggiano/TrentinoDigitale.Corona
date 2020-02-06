using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Web;

namespace TrentinoDigitale.Corona.Mvc.Logics.Entities
{
    public class User
    {
        public string UserName { get; set; }

        public string PasswordHash { get; set; }

        public string FirstName { get; set; }

        public string Surname { get; set; }

        public string Email { get; set; }
        
        public bool IsEnabled { get;  set; }

        public bool IsAdministrator { get; set; }
    }
}