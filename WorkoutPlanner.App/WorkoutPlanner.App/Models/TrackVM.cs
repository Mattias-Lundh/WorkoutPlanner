using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WorkoutPlanner.Domain.Models;

namespace WorkoutPlanner.App.Models
{
    public class TrackVM
    {
        public TrackVM()
        {
            TrackSearchResult = new List<Track>();
        }

        public List<Track> TrackSearchResult { get; set; }
    }
}