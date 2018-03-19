using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WorkoutPlanner.Data;
using WorkoutPlanner.Domain.Models;

namespace WorkoutPlanner.App.Controllers
{
    public class HomeController : Controller
    {
        private string dbKey = "";
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Track()
        {
            return View();
        }

        [HttpPost]
        public ActionResult SaveTrack(Track track, List<Location> locations)
        {
            return Redirect("Track");
        }

        [HttpPost]
        public ActionResult DbConnect(string connStr)
        {
            dbKey = connStr;
            return Redirect("Index");
        }
    }
}
