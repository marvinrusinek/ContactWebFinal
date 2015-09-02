using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.Security;
using ContactWeb.Models;
using ContactWebLibrary;
using ekCommonLibs.IOC;

namespace ContactWeb
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801


    public class MvcApplication : System.Web.HttpApplication
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            //routes.MapRoute(
            //    "Grid", // Route name
            //    "{controller}/{action}/Page{page}/{Column}/{Direction}", // URL with parameters
            //    new { controller = "Home", action = "Index", page = 1, Column = "FirstName", Direction = "Ascending" }, // Parameter defaults
            //    new { page = @"\d+" }// Parameter defaults
            //);

            routes.MapRoute(
                    "PagedContacts", // Route name
                     "Contact/Page{page}", // URL with parameters
                    new { controller = "Contact", action = "List", page = UrlParameter.Optional },
                    new { page = @"\d+" }// Parameter defaults
            );

            routes.MapRoute(
    "AlphaList", // Route name
     "Contact/Page{alpha}", // URL with parameters
    new { controller = "AlphaContact", action = "Index", alpha="A" },
    new { alpha = @"[A-Z]{1}" }// Parameter defaults
);

            routes.MapRoute(
                "Default", // Route name
                "{controller}/{action}/{id}", // URL with parameters
                new { controller = "Home", action = "Index", id = UrlParameter.Optional } // Parameter defaults
            );

        }


        protected void Application_Start()
        {
 
            ConfigIoC.Instance.Init();
            //new ContactGenerator().Populate();

            //var exists = Roles.RoleExists("Admin");
            //if (!exists)
            //    Roles.CreateRole("Admin");

            //exists = Roles.RoleExists("SooperDooperUser");
            //if (!exists)
            //    Roles.CreateRole("SooperDooperUser");

            //var aUser = new ContactBUS().GetContacts()[0];
            //if (Membership.GetUser(aUser.Username) == null)
            //{
            //    MembershipCreateStatus status;
            //    Membership.CreateUser(aUser.Username, "Password", aUser.Email, null, null, true,
            //                          out status);

            //    if (status == MembershipCreateStatus.Success)
            //        Roles.AddUserToRole(aUser.Username, "Admin");
            //    else
            //    {
            //        throw new Exception(status.ToString());
            //    }
            //}

            //var bUser = new ContactBUS().GetContacts()[1];
            //if (Membership.GetUser(bUser.Username) == null)
            //{
            //    MembershipCreateStatus status;
            //    Membership.CreateUser(bUser.Username, "Password", bUser.Email, null, null, true,
            //                          out status);

            //    if (status != MembershipCreateStatus.Success)
            //        throw new Exception(status.ToString());
            //}


            //var friendsBUS = new FriendsBUS();
            //friendsBUS.SetFriends(aUser, new List<Contact> { bUser });

            //var cUser = new ContactBUS().GetContacts()[5];
            //friendsBUS.SetFriends(cUser, new List<Contact> { aUser, bUser });

            AreaRegistration.RegisterAllAreas();
            RegisterRoutes(RouteTable.Routes);
        }
    }
}
