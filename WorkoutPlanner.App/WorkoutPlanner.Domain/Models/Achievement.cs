using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkoutPlanner.Domain.Models
{
    public class Achievement // new model
    {
        public int Id { get; set; }
        // Accomplishmemt, Distance, Duration
        public string Type { get; set; }
        public string Description { get; set; } // new property
        public int Value { get; set; }
        public List<UserAchievement> UserAchievements { get; set; }
    }
}
