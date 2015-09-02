using System.Collections.Generic;
using ContactWebLibrary;


namespace ContactWeb.Models
{
    public class ContactListViewModel
    {
        public List<Contact> Contacts { get; set; }
        public int CurrentPage { get; set; }
        public int MaxPages { get; set; }
        private Dictionary<string, List<string>> _userNameRoles;
        public Dictionary<string, List<string>> UserNameRoles
        {
            get
            {
                if (this._userNameRoles == null)
                    UserNameRoles = new Dictionary<string, List<string>>();
                return this._userNameRoles;
            }
            set { this._userNameRoles = value; }
        }

        private List<string> _allRoles;

        public List<string> AllRoles
        {
            set { this._allRoles = value; }
            get
            {
                if(this._allRoles == null)
                    this.AllRoles = new List<string>();
                return this._allRoles;
            }
        }

        public bool IsAdmin { get; set; }

    }
}