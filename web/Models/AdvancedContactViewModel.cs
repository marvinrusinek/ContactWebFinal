using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ContactWebLibrary;

namespace ContactWeb.Models
{
    public class AdvancedContactViewModel
    {
        public Contact Contact { get; set; }
        public List<PhoneNumberEntry> PhoneNumbers { get; set; }
        public bool? HasAccount { get; set; }
    }
}