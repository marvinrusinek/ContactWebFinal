using System.Collections.Generic;
using ContactWebLibrary;

namespace ContactWeb.Models
{
    public class ContactUser
    {
        public Contact Contact { get; set; }
        public List<string> AllRoles { get; set; }
        public bool IsUser { get; set; }
        public List<string> UserRoles { get; set; }
        public bool IsAdmin { get; set; }
    }
}