using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using ContactWeb.Models;
using ContactWebLibrary;

namespace ContactWeb.Controllers
{
    public class FriendsController : Controller
    {


        [Authorize]
        public ActionResult Index(string username)
        {
            var contact =
                new ContactBUS().GetContacts().Where(
                    c => c.Username.ToLower() == username.ToLower()).SingleOrDefault();
            var friends = new List<Contact>();
            if (contact != null)
                friends = new FriendsBUS().GetFriends(contact);

            var model = new FriendsListViewModel {You = contact, YourFriends = friends};
            return View(model);
        }

        [Authorize]
        public ActionResult RemoveFriend(string username, int contactId)
        {
            var contact =
                new ContactBUS().GetContacts().Where(
                    c => c.Username.ToLower() == username.ToLower()).SingleOrDefault();
            var friends = new List<Contact>();
            if (contact != null)
            {
                friends = new FriendsBUS().GetFriends(contact);
                var contactToRemove = friends.Where(f => f.Id == contactId).Single();
                friends.Remove(contactToRemove);
                new FriendsBUS().SetFriends(contact, friends);
            }
            return RedirectToAction("Index", new {username = username});
        }

        [Authorize]
        public ActionResult AddFriend(string username, int contactId)
        {
            var contact =
                new ContactBUS().GetContacts().Where(
                    c => c.Username.ToLower() == username.ToLower()).SingleOrDefault();
            var friends = new List<Contact>();
            if (contact != null)
            {
                friends = new FriendsBUS().GetFriends(contact);
                friends.Add(new ContactBUS().GetContact(contactId));
                new FriendsBUS().SetFriends(contact, friends);
            }
            return RedirectToAction("Index", new { username = username });
        }

        [Authorize]
        public ActionResult Search(string username, string term)
        {
            var contacts = new ContactBUS().GetContacts(term);
            return new JsonResult()
                       {
                           JsonRequestBehavior = JsonRequestBehavior.AllowGet,
                           Data = contacts.Select(c=>new{id=c.Id, label=c.Username})
                       };
        }

    }
}
