using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TrentinoDigitale.Corona.Mvc.Models.Authentication
{
    public class SignInModel
    {
        [Required]
        [StringLength(255)]
        //[RegularExpression("[a-zA-Z0-9]")]
        public string UserName { get; set; }

        [Required]
        [StringLength(255)]
        public string Password { get; set; }

        /// <summary>
        /// Errore lato client
        /// </summary>
        public string ErrorMessage { get; set; }
    }
}