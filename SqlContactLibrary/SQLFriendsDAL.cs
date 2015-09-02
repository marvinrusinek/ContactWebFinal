using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ContactWebLibrary;

namespace SqlContactLibrary
{
    public class SQLFriendsDAL : IFriendsDAL
    {
        ContactDBDataContext _dataContext
        {
            get
            {
                return new ContactDBDataContext(System.Configuration.ConfigurationManager.ConnectionStrings["ContactDB"].ConnectionString);
            }
        }

        public List<ContactWebLibrary.Contact> GetFriends(ContactWebLibrary.Contact contact)
        {
            var db = this._dataContext;
            var friendIds = (from f in db.Friends
                          where f.ContactId1 == contact.Id || f.ContactId2 == contact.Id
                          select f.ContactId1 == contact.Id ? f.ContactId2 : f.ContactId1).ToList();
            var contacts = from c in db.Contacts
                           where friendIds.Contains(c.Id)
                           select new ContactWebLibrary.Contact()
                                      {
                                          Id = c.Id,
                                          Email = c.Email,
                                          FirstName = c.FirstName,
                                          LastName = c.LastName,
                                          Username = c.Username
                                      };
            return contacts.ToList();
        }

        public void SetFriends(ContactWebLibrary.Contact contact, List<ContactWebLibrary.Contact> contacts)
        {
            var db = this._dataContext;
            var currentFriendIds = this.GetFriends(contact).Select(c=>c.Id);
            var newFriendsIds = contacts.Select(c => c.Id).ToList();
            foreach(var newFriend in contacts)
            {
                if(!currentFriendIds.Contains(newFriend.Id))
                {
                    db.Friends.InsertOnSubmit(new SqlContactLibrary.Friend{ContactId1 = contact.Id,ContactId2 = newFriend.Id});
                }
            }

            foreach(var id in currentFriendIds)
                if(!newFriendsIds.Contains(id))
                    db.Friends.DeleteOnSubmit(db.Friends.Where(f => (f.ContactId1 == id && f.ContactId2 == contact.Id) || (f.ContactId2 == id && f.ContactId1 == contact.Id)).SingleOrDefault());

            db.SubmitChanges();

        }

        public void SetFriends(List<ContactWebLibrary.Contact> contacts)
        {
            foreach(var contact in contacts)
                this.SetFriends(contact, contact.Friends);
        }
        public void Clear()
        {
            var db = _dataContext;
            db.Friends.DeleteAllOnSubmit(db.Friends);
            db.SubmitChanges();
        }


        public Dictionary<int, int> FriendsCountMap()
        {
            var doc = _dataContext;
            var ids = doc.Friends.Select(f => f.ContactId1).Union(doc.Friends.Select(f => f.ContactId2)).Distinct();
            Dictionary<int, int> friendCount = new Dictionary<int, int>();
            foreach (var id in ids)
                friendCount[id] = doc.Friends.Where(f => f.ContactId1 == id || f.ContactId2 == id).Count();
            return friendCount;           
        }
    }
}
