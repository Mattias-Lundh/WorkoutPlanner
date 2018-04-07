using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WorkoutPlanner.Domain.Models;

namespace WorkoutPlanner.App.Models
{
    public class ScheduleVM
    {
        public ScheduleVM()
        {
            ScheduleItems = new List<Session>();
            UserTracks = new List<Track>();
        }
        public List<Session> ScheduleItems { get; set; }
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