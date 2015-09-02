using System.Collections.Generic;


namespace ContactWebLibrary
{
    public class ContactSearchParams
    {
        public List<Contact> Contacts { get; set; }

        public Dictionary<string, int> UsernameMap { get; set; }
        private List<string> _selectedUsernameLetters = null;
        public List<string> SelectedUsernameLetters
        {
            set { this._selectedUsernameLetters = value; }
            get
            {
                if (this._selectedUsernameLetters == null)
                    this.SelectedUsernameLetters = new List<string>();
                return _selectedUsernameLetters;
            }
        }

        public Dictionary<ContactUserAccountStatus, int> UserAccountMap { get; set; }

        private List<ContactUserAccountStatus> _selectedUserAccountStatus = null;
        public List<ContactUserAccountStatus> SelectedUserAccountStatus
        {
            set { this._selectedUserAccountStatus = value; }
            get
            {
                if (this._selectedUserAccountStatus == null)
                    this.SelectedUserAccountStatus = new List<ContactUserAccountStatus>();
                return this._selectedUserAccountStatus;
            }
        }

        public Dictionary<ContactFriendsStatus, int> FriendsMap { get; set; }
        private List<ContactFriendsStatus> _selectedFriendsStatus = null;
        public List<ContactFriendsStatus> SelectedFriendsStatus
        {
            set { this._selectedFriendsStatus = value; }
            get
            {
                if(this._selectedFriendsStatus == null)
                    this.SelectedFriendsStatus = new List<ContactFriendsStatus>();
                return this._selectedFriendsStatus;
            }
        }
    }
}
