using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ContactWeb.Models;
using ContactWebLibrary;
using MvcContrib.UI.Grid;
using MvcContrib.Sorting;
using MvcContrib.Pagination;
using ekCommonLibs.Providers;

namespace ContactWeb.Controllers
{
    public class ContactController : Controller
    {

        private RoleService Roles
        {
            get
            {
                return new RoleService();
            }
        }

        private MembershipService Membership
        {
            get
            {
                return new MembershipService();
            }
        }

        public ActionResult List(int page = 1)
        {
            var logic = new ContactBUS();
            var pageSize = 50;

            var contacts = logic.GetContacts().Skip((page - 1)*pageSize).Take(pageSize).ToList();
            var model = new ContactListViewModel();
            model.AllRoles = Roles.GetRoles();
            model.IsAdmin = Roles.UserHasRole("Admin");
            foreach(var contact in contacts)
            {
                if(Membership.UserExists(contact.Username))
                    model.UserNameRoles.Add(contact.Username.ToLower(), Roles.GetRolesForUser(contact.Username).ToList());

            }

            model.Contacts = contacts;
            model.CurrentPage = page;
            model.MaxPages = (int) Math.Ceiling(logic.GetContactsCount()/(double) pageSize);

            return View(model);
        }

        public ActionResult Search()
        {
            return View();
        }

        public ActionResult Grid(GridSortOptions gridSortOptions, int? page)
        {
            var logic = new ContactBUS();
            var contacts = logic.GetContacts().AsQueryable();
            if (string.IsNullOrEmpty(gridSortOptions.Column))
                gridSortOptions.Column = "FirstName";

            var contactPagedList = contacts
                .OrderBy(gridSortOptions.Column, gridSortOptions.Direction)
                .AsPagination(page ?? 1, 10);

            var model = new ContactGridModel
                            {
                                ContactPagedList = contactPagedList,
                                GridSortOptions = gridSortOptions
                            };
           
            return View(model);
        }


        [Authorize(Roles = "Admin")]
        [HttpPost]
        public ActionResult CreateAccount(int id)
        {
            var logic = new ContactBUS();
            UserHelper.Create().CreateUser(id);
            var contact = logic.GetContact(id);
            TempData["message"] = String.Format("User Account for {0} has been created.", contact.Username);
            var model = new ContactUser
                            {
                                AllRoles = Roles.GetRoles(),
                                Contact = contact, 
                                IsAdmin = true,
                                IsUser = true,
                                UserRoles = new List<string>()
                            };
            return View("ListMembershipControls", model);
            //return RedirectToAction("List", new { page = page });
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public ActionResult AddUserToRole(int id, string role)
        {
            var logic = new ContactBUS();
            UserHelper.Create().AddUserToRole(id, role);
            var contact = logic.GetContact(id);
            TempData["message"] = String.Format("User Account for {0} has been added to {1} Role", contact.Username, role);
            var model = new ContactUser
            {
                AllRoles = Roles.GetRoles(),
                Contact = contact,
                IsAdmin = true,
                IsUser = true,
                UserRoles = Roles.GetRolesForUser(contact.Username).ToList()
            };
            return View("ListMembershipControls", model);           
            //return RedirectToAction("List", new { page = page });
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public ActionResult RemoveUserFromRole(int id, string role)
        {
            var logic = new ContactBUS();
            UserHelper.Create().RemoveUserFromRole(id, role);
            var contact = logic.GetContact(id);
            TempData["message"] = String.Format("User Account for {0} has been removed from {1} Role", contact.Username, role);
            var model = new ContactUser
            {
                AllRoles = Roles.GetRoles(),
                Contact = contact,
                IsAdmin = true,
                IsUser = true,
                UserRoles = Roles.GetRolesForUser(contact.Username).ToList()
            };
            return View("ListMembershipControls", model); 
            //return RedirectToAction("List", new { page = page });
        }


        [Authorize(Roles = "Admin")]
        public ActionResult Create()
        {
            var model = new ContactUser
                            {
                                AllRoles = Roles.GetRoles(),
                                Contact = new Contact() {},
                                IsUser = false,
                                UserRoles = new List<string>()
                            };
            return View(model);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public ActionResult Create(ContactUser m)
        {
            m.AllRoles = Roles.GetRoles();
            if (m.UserRoles == null)
                m.UserRoles = new List<string>();
            var logic = new ContactBUS();
            if (ModelState.IsValid)
            {

                logic.CreateContact(m.Contact);
                if (m.IsUser)
                {
                    var userHelper = UserHelper.Create();
                    var contact =
                        logic.GetContacts().Where(c => c.Username == m.Contact.Username).SingleOrDefault();
                    userHelper.CreateUser(contact.Id);
                    if (m.UserRoles.Count() > 0)
                    {
                        foreach (var role in m.UserRoles)
                            userHelper.AddUserToRole(contact.Id, role);
                    }

                }
                return RedirectToAction("List");
            }
            else
                return View(m);
        }

        [Authorize(Roles = "Admin")]
        public ActionResult Edit(int id)
        {
            var logic = new ContactBUS();
            var contact = logic.GetContact(id);
            var exists = Membership.UserExists(contact.Username);
            var model = new ContactUser
            {
                AllRoles = Roles.GetRoles().ToList(),
                Contact = contact,
                IsUser = exists,
                UserRoles = !exists ? new List<string>() :Roles.GetRolesForUser(contact.Username) .ToList()
            };
            return View(model);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public ActionResult Edit(ContactUser m)
        {
            m.AllRoles = Roles.GetRoles().ToList();
            if(m.UserRoles == null)
                m.UserRoles = new List<string>();
            var logic = new ContactBUS();
            var exists = Membership.UserExists(m.Contact.Username);
            if (ModelState.IsValid)
            {

                logic.EditContact(m.Contact);
                var userHelper = UserHelper.Create();
                if (m.IsUser && !exists)  
                    userHelper.CreateUser(m.Contact.Id);
                else if(!m.IsUser && exists)
                    userHelper.DeleteUser(m.Contact.Id);
                
                if(m.IsUser)
                {
                    foreach(var role in m.AllRoles)
                    {
                        if(m.UserRoles.Contains(role))
                        {
                            if(!Roles.UserHasRole(m.Contact.Username, role))
                                userHelper.AddUserToRole(m.Contact.Id, role);
                        }
                        if (!m.UserRoles.Contains(role))
                        {
                            if(Roles.UserHasRole(m.Contact.Username, role))
                                userHelper.RemoveUserFromRole(m.Contact.Id, role);
                        }

                    }
                }
                return RedirectToAction("List");
            }
            else
                return View(m);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public ActionResult Delete(Contact contact)
        {
            var logic = new ContactBUS();
            logic.DeleteContact(contact);
            return RedirectToAction("List");
        }

        public ActionResult Form(Contact contact)
        {
            return View("Form", contact ?? new Contact());
        }

    }
}
