using System.Linq;
using System.Web.Mvc;
using System.Web.Security;
using ContactWeb.Models;

namespace ContactWeb.Controllers
{
    public class RoleController : Controller
    {

        public ActionResult Index()
        {
            var roles = Roles.GetAllRoles();
            var model = new RoleListViewModel {Roles = roles.ToList()};
            return View(model);
        }

        [HttpPost]
        public ActionResult CreateRole(string role)
        {
            if(!Roles.RoleExists(role))
                Roles.CreateRole(role);
            return RedirectToAction("Index");
        }

    }
}
