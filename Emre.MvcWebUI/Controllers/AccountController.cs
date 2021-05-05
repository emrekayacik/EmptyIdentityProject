using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Emre.MvcWebUI.Identity;
using Emre.MvcWebUI.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin.Security;

namespace Emre.MvcWebUI.Controllers
{
    public class AccountController : Controller
    {
        private UserManager<ApplicationUser> UserManager;

        private RoleManager<ApplicationRole> RoleManager;

        public AccountController()
        {
            var userStore = new UserStore<ApplicationUser>(new IdentityDataContext());
            UserManager = new UserManager<ApplicationUser>(userStore);
            var roleStore = new RoleStore<ApplicationRole>(new IdentityDataContext());
            RoleManager = new RoleManager<ApplicationRole>(roleStore);
        }

        [Authorize]
        public ActionResult Index()
        {
            return View();
        }
        [Authorize]
        public ActionResult Details(int id)
        {
            return View();
        }
        // GET: Account
        public ActionResult Register()
        {
            if (User.Identity.IsAuthenticated)
            {
                return View("Error");
            }
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register(Register model)
        {
            if (ModelState.IsValid)
            {
                var applicationUser = new ApplicationUser();
                applicationUser.Name = model.Name;
                applicationUser.Surname = model.Surname;
                applicationUser.Email = model.Email;
                applicationUser.UserName = model.Username;
                var result = UserManager.Create(applicationUser, model.Password);
                if (result.Succeeded)
                {
                    //Kullanıcı oluştu ve kullanıcıyı bir role atayabiliriz
                    if (RoleManager.RoleExists("user"))
                    {
                        UserManager.AddToRole(applicationUser.Id, "user");
                    }

                    return RedirectToAction("Login", "Account");

                }
                else
                {
                    ModelState.AddModelError("RegisterError","Kullanıcı oluşturma sırasında hata.");
                }
            }
            return View(model);
        }
        public ActionResult Login(string ReturnUrl)
        {
            if (User.Identity.IsAuthenticated)
            {
                return View("Error");
            }
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(Login model, string ReturnUrl)
        {
            
            if (ModelState.IsValid)
            {
                var user = UserManager.Find(model.Username, model.Password); // kullanıcıyı bul
                if (user!=null)
                {
                    //var olan kullanıcıyı sisteme dahil et
                    //applicationcookie oluşturup bunu sisteme bırak(beni hatırla dediyse)
                    var authManager = HttpContext.GetOwinContext().Authentication;

                    var identityClaims = UserManager.CreateIdentity(user, "ApplicationCookie");
                    var authProperties = new AuthenticationProperties();
                    authProperties.IsPersistent = model.RememberMe;
                    authManager.SignIn(authProperties,identityClaims);
                    if (!string.IsNullOrEmpty(ReturnUrl))
                    {
                        return Redirect(ReturnUrl);
                    }
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ModelState.AddModelError("LoginError", "Böyle bir kullanıcı yok.");
                }
            }
            return View(model);
        }
        public ActionResult Logout()
        {
            if (!User.Identity.IsAuthenticated)
            {
                return View("Error");
            }
            var authManager = HttpContext.GetOwinContext().Authentication;
            authManager.SignOut();
            return RedirectToAction("Index","Home");
        }
        public ActionResult Error()
        {
            return View();
        }
    }
}