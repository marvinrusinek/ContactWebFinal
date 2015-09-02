using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ContactWeb.Models;

namespace ContactWeb.Controllers
{
    public class PhoneNumberEntryController : Controller
    {

        private void ProcessModel(PhoneNumberEntryViewModel model)
        {
            var usedNumberTypes = model.PhoneNumberEntries.Select(i => i.PhoneNumberType).ToList();
            var allNumbers = Enum.GetNames(typeof(PhoneNumberTypes)).ToList();
            foreach (var item in model.PhoneNumberEntries)
            {
                item.AvailableTypes = allNumbers.Where(t => t == item.PhoneNumberType || !usedNumberTypes.Contains(t)).ToList();
            }
        }

        public ActionResult Index(PhoneNumberEntryViewModel model)
        {
            //simulate database

            //var items = Session["PhoneNumbers"] != null ? (List<PhoneNumberEntry>)Session["PhoneNumbers"] : new List<PhoneNumberEntry>
            //                {
            //                    new PhoneNumberEntry{PhoneNumber = "917-301-1539", PhoneNumberType = PhoneNumberTypes.Mobile.ToString()},
            //                    new PhoneNumberEntry{PhoneNumber ="212-555-1212", PhoneNumberType = PhoneNumberTypes.Business.ToString()}
            //                };
            //var model = new PhoneNumberEntryViewModel()
            //{
            //    PhoneNumberEntries = items
            //};
            if(model == null)
                model = new PhoneNumberEntryViewModel();
            ProcessModel(model);

            return View("Form", model);
        }


        [HttpPost]
        public ActionResult Change(PhoneNumberEntryViewModel model)
        {
            ProcessModel(model);
            return View("Form", model);
        }

        [HttpPost]
        public ActionResult Delete(PhoneNumberEntryViewModel model, int index)
        {
            model.PhoneNumberEntries.RemoveAt(index);
            ProcessModel(model);
            return View("Form", model);
        }

        [HttpPost]
        public ActionResult Add(PhoneNumberEntryViewModel model)
        {
            var types = model.PhoneNumberEntries.Select(e => e.PhoneNumberType);
            var type = model.Types.Where(t => !types.Contains(t)).First();
            model.PhoneNumberEntries.Add(new PhoneNumberEntry()
                                             {
                                                    PhoneNumberType = type
                                             });
            ProcessModel(model);
            return View("Form", model);
        }

        //[HttpPost]
        //public ActionResult Save(PhoneNumberEntryViewModel model)
        //{
        //    ProcessModel(model);
        //    if (ModelState.IsValid)
        //    {
        //        Session["PhoneNumbers"] = model.PhoneNumberEntries;
        //        TempData["message"] = "Phone Numbers have been changed.";
        //    }
        //    return View("Index", model);
        //}

    }
}
