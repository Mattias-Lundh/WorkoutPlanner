using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WorkoutPlanner.Data;
using WorkoutPlanner.Domain.Models;
using WorkoutPlanner.App.Models;
using System.ComponentModel.DataAnnotations;
using System.Web.Security;

namespace WorkoutPlanner.App.Controllers
{
    public class AccountController : Controller
    {
        private static string returnDestination;
        // GET: Authentification
        public ActionResult Index(string returnUrl)
        {
            returnDestination = returnUrl;
            return View();
        }

        public ActionResult Login(string email, string password)
        {
            Security security = new Security();
            Database db = new Database();
            User user = db.GetUserByEmail(email);
            if (security.ComparePassword(user.Password, password, user.Salt))
            {
                Session["UserId"] = user.Id;
                Session["Email"] = user.Email;
                Session["Username"] = user.Name;
                return RedirectToAction("Index", returnDestination);
            }
            return RedirectToAction("Index", "Account", returnDestination);
        }

        [HttpGet]
        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Register(User user)
        {
            Database db = new Database();
            Security security = new Security();
            security.GeneratePasswordData(user.Password);
            string rawPassword = user.Password;
            user.Password = security.HashedPassword;
            user.Salt = security.Salt;

            if (ModelState.IsValid)
            {
                db.AddUser(user);
                Login(user.Email, rawPassword);
            }

            return RedirectToAction("Index", "Account", returnDestination);
        }
    }
}