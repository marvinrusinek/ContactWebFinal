using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

namespace ContactWebLibrary
{
    public class XMLFriendsDAL : IFriendsDAL
    {

        XDocument FriendDoc
        {
            get { return XDocument.Load(System.Web.HttpContext.Current.Server.MapPath("~/App_Data/Friends.xml")); }
        }


        public List<Contact> GetFriends(Contact contact)
        {
            var doc = this.FriendDoc;
            var results = from friend in doc.Descendants("Friend")
                          where (int)friend == contact.Id
                          select friend.Parent.Elements("Friend").Where(f => (int)f != contact.Id).Single();
            List<Contact> contacts = new List<Contact>();
            foreach (XElement elem in results)
                contacts.Add(new ContactBUS().GetContact(int.Parse(elem.Value)));
            return contacts;

        }

        public void SetFriends(Contact contact, List<Contact> friends)
        {
            var doc = this.FriendDoc;
            var results = from friend in doc.Descendants("Friend")
                          where (int)friend == contact.Id
                          select friend.Parent;
            results.Remove();
            foreach (var friend in friends)
            {
                var elem = new XElement("FriendPair",
                                            new XElement("Friend", contact.Id),
                                            new XElement("Friend", friend.Id));
                doc.Descendants("Friends").Single().Add(elem);
            }
            doc.Save(System.Web.HttpContext.Current.Server.MapPath("~/App_Data/Friends.xml"));

        }

        public void SetFriends(List<Contact> contacts)
        {
            var doc = this.FriendDoc;

            foreach (var contact in contacts)
            {

                var results = from friend in doc.Descendants("Friend")
                              where (int)friend == contact.Id
                              select friend.Parent;
                results.Remove();
                foreach (var friend in contact.Friends)
                {
                    var elem = new XElement("FriendPair",
                                            new XElement("Friend", contact.Id),
                                            new XElement("Friend", friend.Id));
                    doc.Descendants("Friends").Single().Add(elem);
                }
            }

            doc.Save(System.Web.HttpContext.Current.Server.MapPath("~/App_Data/Friends.xml"));

        }


        public void Clear()
        {
            var doc = this.FriendDoc;
            doc.Descendants("FriendPair").Remove();
            doc.Save(System.Web.HttpContext.Current.Server.MapPath("~/App_Data/Friends.xml"));

        }



        public Dictionary<int, int> FriendsCountMap()
        {
            var doc = this.FriendDoc;
            var ids = doc.Descendants("Friend").Select(node => (int) node).Distinct();
            Dictionary<int, int> friendCount  = new Dictionary<int, int>();
            foreach (var id in ids)
                friendCount[id] = doc.Descendants("Friend").Where(node => (int) node == id).Count();
            return friendCount;
        }
    }
}