using System;
using System.Collections.Generic;
using System.Linq;
using ekCommonLibs;
using ekCommonLibs.IOC;


namespace ContactWebLibrary
{
    public class ContactBUS
    {
        private IContactDAL _dal = null;

        public ContactBUS(IContactDAL dal)
        {
            _dal = dal;
        }

        public ContactBUS() : this(ConfigIoC.Instance.Resolve<IContactDAL>())
        {

        }

        public Dictionary<string, int> UsernameMap(List<string> usernames, ContactSearchParams contactSearchParams)
        {
            var friendsMap = new FriendsBUS().FriendsCount();
            var usernameMapResults = from c in Contacts
                                     where (contactSearchParams.SelectedUserAccountStatus.Count() == 0 || (usernames.Contains(c.Username) && contactSearchParams.SelectedUserAccountStatus.Contains(ContactUserAccountStatus.UserAccount)) || (!usernames.Contains(c.Username) && contactSearchParams.SelectedUserAccountStatus.Contains(ContactUserAccountStatus.NoUserAccount)))
                                     &&
                                        (contactSearchParams.SelectedFriendsStatus.Count() == 0 || (friendsMap.Keys.Contains(c.Id) && contactSearchParams.SelectedFriendsStatus.Contains(ContactFriendsStatus.HasFriends)) || (!friendsMap.Keys.Contains(c.Id) && contactSearchParams.SelectedFriendsStatus.Contains(ContactFriendsStatus.NoFriends)))
                                     group c by c.Username.Substring(0, 1).ToUpper()
                                         into g
                                         select new { Letter = g.Key, Count = g.Count() };

            var usernameMap = usernameMapResults.ToDictionary(g => g.Letter, g => g.Count);
            return usernameMap;
        }

        public Dictionary<ContactUserAccountStatus, int> UserAccountMap(List<string> usernames, ContactSearchParams contactSearchParams)
        {
            var friendsMap = new FriendsBUS().FriendsCount();
            var userAccountMapResults = from c in Contacts
                                        where (contactSearchParams.SelectedUsernameLetters.Count() == 0 || contactSearchParams.SelectedUsernameLetters.Contains(c.Username.Substring(0, 1)))
                                                                             &&
                                        (contactSearchParams.SelectedFriendsStatus.Count() == 0 || (friendsMap.Keys.Contains(c.Id)  && contactSearchParams.SelectedFriendsStatus.Contains(ContactFriendsStatus.HasFriends)) || (!friendsMap.Keys.Contains(c.Id) && contactSearchParams.SelectedFriendsStatus.Contains(ContactFriendsStatus.NoFriends)))
                                        group c by usernames.Contains(c.Username)
                                            into g
                                            select new { HasAccount = g.Key, Count = g.Count() };

            var userAccountMap = userAccountMapResults.ToDictionary(g => g.HasAccount ? ContactUserAccountStatus.UserAccount : ContactUserAccountStatus.NoUserAccount, g => g.Count);
            return userAccountMap;
        }

        public Dictionary<ContactFriendsStatus, int> FriendsMap(List<string> usernames, ContactSearchParams contactSearchParams)
        {
            var friendsMap = new FriendsBUS().FriendsCount();
            var mapQuery = from c in Contacts
                                        where (contactSearchParams.SelectedUsernameLetters.Count() == 0 || contactSearchParams.SelectedUsernameLetters.Contains(c.Username.Substring(0, 1)))
                                        &&
                                        (contactSearchParams.SelectedUserAccountStatus.Count() == 0 || (usernames.Contains(c.Username) && contactSearchParams.SelectedUserAccountStatus.Contains(ContactUserAccountStatus.UserAccount)) || (!usernames.Contains(c.Username) && contactSearchParams.SelectedUserAccountStatus.Contains(ContactUserAccountStatus.NoUserAccount)))
                                        group c by friendsMap.ContainsKey(c.Id)
                                            into g
                                            select new { HasFriends = g.Key, Count = g.Count() };

            var list = mapQuery.ToList();

            var map = mapQuery.ToDictionary(g => g.HasFriends ? ContactFriendsStatus.HasFriends : ContactFriendsStatus.NoFriends, g => g.Count);
            return map;
        }

        public IQueryable<Contact> Contacts
        {
            get { return _dal.Contacts(); }
        }

        public int GetContactsCount()
        {
            return _dal.Contacts().Count();
        }

        public List<Contact> GetContacts()
        {
            return _dal.Contacts().ToList();
        }

        public List<Contact> GetContacts(string searchTerm)
        {
            var results = from contact in _dal.Contacts()
                          where contact.Username.ToLower().StartsWith(searchTerm)
                          select contact;
            return results.ToList();
        }

        public void EditContact(Contact contact)
        {
            _dal.EditContact(contact);

        }

        public void CreateContact(Contact contact)
        {
            _dal.CreateContact(contact);
        }

        public void CreateContact(List<Contact> contacts)
        {
            _dal.CreateContact(contacts);
        }

        public void DeleteContact(Contact contact)
        {
            var friendsService = new FriendsBUS();
            friendsService.SetFriends(contact, new List<Contact>());
            _dal.DeleteContact(contact);
        }

        public Contact GetContact(int id)
        {
            var results = from contact in _dal.Contacts()
                          where contact.Id == id
                          select contact;
            return results.SingleOrDefault();
        }
        public bool UsernameExists(string username)
        {
            var results = from contact in _dal.Contacts()
                          where contact.Username.ToLower() == username.ToLower()
                          select contact;
            return results.Any();
        }

        public bool EmailExists(Contact contact)
        {
            var results = from c in _dal.Contacts()
                          where c.Email.ToLower() == contact.Email.ToLower()
                          select contact;
            return results.Any();
        }

        public void Clear()
        {
            _dal.Clear();
        }

    }
}