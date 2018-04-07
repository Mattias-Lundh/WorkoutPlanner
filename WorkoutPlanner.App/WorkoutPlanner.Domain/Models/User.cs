using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace WorkoutPlanner.Domain.Models
{
    public class User
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Email { get; set; } 
        [Required]
        public string Password { get; set; } 
        public string Salt { get; set; } 
        public List<Workout> Workouts { get; set; }
        public List<Session> Sessions { get; set; }
        public List<UserAchievement> UserAchievements { get; set; }
    }
}
