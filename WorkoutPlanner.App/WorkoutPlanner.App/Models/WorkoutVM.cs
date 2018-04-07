using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WorkoutPlanner.Domain.Models;

namespace WorkoutPlanner.App.Models
{
    public class WorkoutVM
    {
        public WorkoutVM()
        {
            Workouts = new List<Workout>();
            UserTracks = new List<Track>();
        }
        public List<Workout> Workouts { get; set; }
        public List<Track> UserTracks { get; set; }
        public Track Selectedtrack { get; set; }
        public List<Track> AllTracks
        {
            get
            {
                List<Track> result = new List<Track>();
                result.AddRange(UserTracks);
                if (null != Selectedtrack)
                {
                    result.Add(Selectedtrack);
                }
                return result;
            }
        }
    }
}