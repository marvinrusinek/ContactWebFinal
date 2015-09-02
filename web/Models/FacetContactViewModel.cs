using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ContactWebLibrary;

namespace ContactWeb.Models
{

    public class FacetContactViewModel
    {
        public List<Contact> Contacts { get; set; }

        public Dictionary<string, int> UsernameMap { get; set; }

        public Dictionary<ContactUserAccountStatus, int> UserAccountMap { get; set; }

        public Dictionary<ContactFriendsStatus, int> FriendsMap { get; set; }

        public ContactSearchParams ContactSearchParams { get; set; }

        public Dictionary<int, int> FriendsCount { get; set; }

    }
}