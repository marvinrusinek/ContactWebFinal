using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ContactWeb.Models;
using ContactWebLibrary;

namespace ContactWeb.Controllers
{
    public class AdvancedContactController : Controller
    {
        //
        // GET: /AdvancedContact/

        public ActionResult Index()
        {
            var model = new AdvancedContactViewModel()
                            {
                            };
            return View(model);
        }

    }
}
