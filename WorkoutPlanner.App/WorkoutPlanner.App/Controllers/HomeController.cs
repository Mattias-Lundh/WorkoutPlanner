using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WorkoutPlanner.Data;
using WorkoutPlanner.Domain.Models;
using System.ComponentModel.DataAnnotations;
using WorkoutPlanner.App.Models;

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
            return View(new TrackVM());
        }

        [HttpPost]
        public ActionResult SaveTrack(Track track, List<Location> locations)
        {
            if (ModelState.IsValid)
            {
                track.Locations = locations;
                Database db = new Database();
                db.AddTrack(track);
                ViewBag.Message = "Track '" + track.Name + "' Saved";
                return Redirect("Track");
            }

            TrackVM model = new TrackVM { TrackNameError = "Track needs a name" };
            return Redirect("Track"); // need to add the model here
        }

        [HttpPost]
        public ActionResult DbConnect(string connStr)
        {
            dbKey = connStr;
            return Redirect("Index");
        }
    }
}
