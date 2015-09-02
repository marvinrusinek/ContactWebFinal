using System;
using System.Collections.Generic;
using System.Linq;

namespace ContactWeb.Models
{
    public class PhoneNumberEntryViewModel
    {
        private List<PhoneNumberEntry> _phoneNumberEntries = null;
        public List<PhoneNumberEntry> PhoneNumberEntries
        {
            set { this._phoneNumberEntries = value; }
            get
            {
                if(this._phoneNumberEntries == null)
                    this.PhoneNumberEntries = new List<PhoneNumberEntry>();
                return this._phoneNumberEntries;
            }
        }
        public List<string> Types
        {
            get
            {
                return Enum.GetNames(typeof(PhoneNumberTypes)).ToList();
            }
        }
    }
}