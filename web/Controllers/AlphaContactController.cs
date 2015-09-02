using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ContactWeb.Models;
using ContactWebLibrary;

namespace ContactWeb.Controllers
{
    public class AlphaContactController : Controller
    {
        //
        // GET: /AlphaContact/

        public ActionResult Index(string alpha)
        {

            var logic = new ContactBUS();
            var model = new ContactListAlphaViewModel();
            var dictionary = logic.GetContacts().GroupBy(g => g.Username.Substring(0, 1).ToUpper()).ToDictionary(g => g.Key, g => g.Count());
            foreach (var key in dictionary.Keys)
                model.LetterDictionary[key] = dictionary[key];
            model.Contacts = logic.GetContacts().Where(c => c.Username.ToUpper().StartsWith(alpha)).ToList();
            model.SelectedLetter = alpha;
            return View(model);
        }

    }
}
