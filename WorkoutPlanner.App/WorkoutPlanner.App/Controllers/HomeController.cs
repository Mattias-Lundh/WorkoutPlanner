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

        public ActionResult Workout()
        {
            Database db = new Database();
            WorkoutVM model = new WorkoutVM();
            if (null != Session["UserId"])
            {
                model.UserTracks = db.GetTrackByCreator((int)Session["UserId"]);
                model.Workouts = db.GetWorkoutsByUserId((int)Session["UserId"]);
            }
            return View(model);
        }

        public ActionResult SaveWorkout(Workout workout, string trackName)
        {
            if (null != Session["UserId"])
            {
                Database db = new Database();
                Track track = db.GetTrackByNameSearch(trackName);
                workout.TrackId = track.Id;
                workout.UserId = (int)Session["UserId"];
                db.AddWorkout(workout);

                //Award achievements
                AchievementEvaluation achievementTool = new AchievementEvaluation();
                List<Workout> workouts = db.GetWorkoutsByUserId((int)Session["UserId"]);
                achievementTool.EvaluateWorkouts(workouts);
                achievementTool.GrantAwardsToUser((int)Session["UserId"]);
            }
            return Redirect("Workout");
        }

        public ActionResult Achievements()
        {
            AchievementVM model = new AchievementVM();
            if (null != Session["UserId"])
            {
                Database db = new Database();
                model.Achievements = db.GetAchievementsByUserId((int)Session["UserId"]);
            }
            return View(model);
        }

        public ActionResult Schedule()
        {
            ScheduleVM model = new ScheduleVM();
            if (null != Session["UserId"])
            {
                Database db = new Database();                
                model.ScheduleItems = db.GetSessionsByUserId((int)Session["UserId"]);
            }
            return View(model);
        }

        public ActionResult SaveSession()
        {
            return Redirect("Schedule");
        }
    }
}