using Emre.MvcWebUI.Identity;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Web.Mvc;

namespace Emre.MvcWebUI.Controllers
{
    [Authorize(Roles = "admin")]
    public class AdminController : Controller
    {
        // GET: Admin

        private UserManager<ApplicationUser> userManager;

        public AdminController()
        {
            var userStore = new UserStore<ApplicationUser>(new IdentityDataContext());
            userManager = new UserManager<ApplicationUser>(userStore);

        }
        // GET: Admin
        public ActionResult Index()
        {
            return View(userManager.Users);
        }
    }
}