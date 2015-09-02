using System.Web.Mvc;
using System.Web.Security;
using ContactWeb.Models;
using ekCommonLibs.Providers;

namespace ContactWeb.Controllers
{
    public class AccountController : Controller
    {
        private MembershipService Membership
        {
            get
            {
                return new MembershipService();
            }
        }

        public ActionResult Index()
        {
            var simpleUser = new SimpleUser();
            if (User.Identity.IsAuthenticated)
                simpleUser.UserName = User.Identity.Name;
            return View(simpleUser);
        }

        public ActionResult Logon()
        {
            var user = new SimpleUser {};
            return View(user);
        }

        [HttpPost]
        public ActionResult Logon(SimpleUser user)
        {
            var result = Membership.Authenticate(user.UserName, user.Password);
            if (result)
            {
                FormsAuthentication.RedirectFromLoginPage(user.UserName, false);
            }
            return View(user);
        }

 
        public ActionResult Logout()
        {
                FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Home");
        }

    }
}
