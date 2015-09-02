using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using ContactWeb.Models;
using ContactWebLibrary;

namespace ContactWeb.Controllers
{
    public class FacetContactController : Controller
    {
        public ActionResult Index(FacetContactViewModel model)
        {
            if(model.ContactSearchParams == null)
                model.ContactSearchParams = new ContactSearchParams();
            var logic = new ContactBUS();
            var usernames = Membership.GetAllUsers().Cast<MembershipUser>().Select(m=>m.UserName).ToList();

            model.UsernameMap = logic.UsernameMap(usernames, model.ContactSearchParams);

            model.UserAccountMap = logic.UserAccountMap(usernames, model.ContactSearchParams);

            model.FriendsMap = logic.FriendsMap(usernames, model.ContactSearchParams);

            model.FriendsCount = new FriendsBUS().FriendsCount();

            var results = from c in logic.Contacts
                          where
                              (model.ContactSearchParams.SelectedUserAccountStatus.Count() == 0 ||
                              (usernames.Contains(c.Username) &&
                               model.ContactSearchParams.SelectedUserAccountStatus.Contains(ContactUserAccountStatus.UserAccount)) ||
                              (!usernames.Contains(c.Username) &&
                               model.ContactSearchParams.SelectedUserAccountStatus.Contains(ContactUserAccountStatus.NoUserAccount)))
                               &&
                               (
                               model.ContactSearchParams.SelectedUsernameLetters.Count() == 0 || model.ContactSearchParams.SelectedUsernameLetters.Contains(c.Username.Substring(0, 1))
                               )
                               &&
                                        (model.ContactSearchParams.SelectedFriendsStatus.Count() == 0 || (c.Friends.Count > 0 && model.ContactSearchParams.SelectedFriendsStatus.Contains(ContactFriendsStatus.HasFriends)) || (c.Friends.Count == 0 && model.ContactSearchParams.SelectedFriendsStatus.Contains(ContactFriendsStatus.NoFriends)))
                          select c;
            model.Contacts = results.ToList();
            return View(model);
        }

    }
}
