using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TrentinoDigitale.Corona.Mvc.Models.Rilevazioni
{
    public class IndexModel
    {
        [Required]
        public DateTime FromDate { get; set; }

        [Required]
        public DateTime ToDate { get; set; }

        [Required]
        public string UserName { get; set; }

        [Required]
        public string SelectedCode { get; set; }

        public bool IsMonday { get; set; }

        public bool IsTuesday { get; set; }

        public bool IsWednesday { get; set; }

        public bool IsThurstday { get; set; }

        public bool IsFriday { get; set; }

        public IList<SelectListItem> AllCodes { get; set; }

        public string SelectedCodeDescription { get; set; }
    }
}