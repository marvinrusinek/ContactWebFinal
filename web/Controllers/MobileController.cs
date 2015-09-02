using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ContactWeb.Models;
using ContactWebLibrary;

namespace ContactWeb.Controllers
{
    public class MobileController : Controller
    {
        private ContactBUS _logic;
        private FriendsBUS _friendsLogic;
        
        public MobileController()
        {
            _logic = new ContactBUS();
            _friendsLogic = new FriendsBUS();
        }

        public ActionResult Index()
        {
            var data = (
                           from c in _logic.Contacts
                           group c by c.LastName.Substring(0, 1)
                       ).ToDictionary(gr => gr.Key, gr => gr.ToList());
            var model = new MobileContactsViewModel
                            {
                                Contacts = data,
                                FriendsCount = _friendsLogic.FriendsCount()
                            };
            return View(model);
        }

        public ActionResult Detail(int id)
        {
            var contact = _logic.GetContact(id);
            var model = new MobileContactViewModel
                            {
                                Contact = contact,
                                FriendsCount = _friendsLogic.FriendsCount()
                            };
            return View(model);
        }

    }
}
