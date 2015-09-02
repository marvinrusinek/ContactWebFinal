using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ContactWeb.Models;
using ContactWebLibrary;

namespace ContactWeb.Controllers
{
    public class AddMultipleContactController : Controller
    {

        public ActionResult Knockout()
        {
            var model =  new AddMultipleViewModel { Contacts = new List<Contact> { new Contact { } } };
            return View(model);
        }

        [HttpPost]
        public ActionResult Knockout(AddMultipleViewModel model)
        {

            var errors = new Dictionary<string, ModelErrorCollection>();
            foreach(var key in this.ModelState.Keys)
                errors.Add(key, this.ModelState[key].Errors);

            this.ViewData["Errors"] = errors;

            if(this.ModelState.IsValid)
            {
                var logic = new ContactBUS();
                foreach(var contact in model.Contacts)
                    logic.CreateContact(contact);
                TempData["model"] = model;
                return RedirectToAction("Confirmation");
            }
            return View(model);
        }

        public ActionResult AddMultiple()
        {
            var model = TempData["model"] ?? new AddMultipleViewModel { Contacts = new List<Contact> { new Contact { } } };
            return View(model);
        }

        [HttpPost]
        public ActionResult AddMore(AddMultipleViewModel model)
        {
            ModelState.Clear();
            model.Contacts.Add(new Contact() { });
            TempData["model"] = model;
            return RedirectToAction("AddMultiple");
        }

        [HttpPost]
        public ActionResult AddMultipleRemoveOne(AddMultipleViewModel model, int index)
        {
            ModelState.Clear();
            model.Contacts.RemoveAt(index);
            TempData["model"] = model;
            return RedirectToAction("AddMultiple");
        }

        [HttpPost]
        public ActionResult AddMultiple(AddMultipleViewModel model)
        {
            

            if (this.ModelState.IsValid)
            {
                var logic = new ContactBUS();
                foreach (var contact in model.Contacts)
                {
                    logic.CreateContact(contact);
                }
                TempData["model"] = model;
                return RedirectToAction("Confirmation");
            }
            else
            {
                return View(model);
            }
        }

        public ActionResult Confirmation()
        {
            var model = TempData["model"] ?? new AddMultipleViewModel { Contacts = new List<Contact> { } };
            return View(model);
        }

    }
}
