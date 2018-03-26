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
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Track()
        {
            return View(new TrackVM());
        }

        public ActionResult SearchTrack(string searchString)
        {
            Database db = new Database();
            TrackVM model = new TrackVM();
            try
            {
                model.TrackSearchResult = db.GetTracksByNameSearch(searchString);
            }
            catch (NullReferenceException)
            {

            }
            return View("Track", model);
        }

        [HttpPost]
        public ActionResult SaveTrack(Track track, List<Location> locations)
        {
            if (ModelState.IsValid && null != Session["UserId"])
            {
                Database db = new Database();
                track.Creator = (int)Session["UserId"];
                track.Locations = locations;
                db.AddTrack(track);
                ViewBag.Message = "Track '" + track.Name + "' Saved";
                return Redirect("Track");
            }

            return Redirect("Track");
        }
    }
}