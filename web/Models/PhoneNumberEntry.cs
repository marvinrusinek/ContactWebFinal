using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ContactWeb.Models
{
    public class PhoneNumberEntry
    {
        [Required(ErrorMessage = "Required")]
        public string PhoneNumber { get; set; }

        public string PhoneNumberType { get; set; }
        
        public List<string> AvailableTypes { get; set; }
    }
}