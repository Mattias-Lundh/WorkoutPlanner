using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WorkoutPlanner.Data;
using WorkoutPlanner.Domain.Models;
using System.ComponentModel.DataAnnotations;
using WorkoutPlanner.App.Models;
using WorkoutPlanner.Domain.API;

namespace WorkoutPlanner.App.Controllers
{
    public class AdminController : Controller
    {
        // GET: Admin
        public ActionResult Index()
        {
            Database db = new Database();
            AchievementVM model = new AchievementVM();
            model.Achievements = db.GetAllAchievemntTypeAndDescription();
            return View(model);
        }

        public ActionResult AddAchievement(Achievement achievement)
        {
            Database db = new Database();
            db.AddAchievement(achievement);
            AchievementVM model = new AchievementVM();
            model.Achievements = db.GetAllAchievemntTypeAndDescription();
            return View("Index", model);
        }

        public ActionResult LoadDefaults()
        {
            Database db = new Database();
            if(0 == db.CountAchievements())
            {
                db.LoadDefaultAchievementData();
            }            

            return Redirect("Index");
        }
    }
}