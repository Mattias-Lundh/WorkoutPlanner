using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WorkoutPlanner.Domain.Models;

namespace WorkoutPlanner.App.Models
{
    public class AchievementVM
    {
        public AchievementVM()
        {
            Achievements = new List<object>();
        }
        public List<object> Achievements { get; set; }        
    }
}