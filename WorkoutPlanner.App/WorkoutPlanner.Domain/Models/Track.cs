using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkoutPlanner.Domain.Models
{
    public class Track
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; } //not part of init migration
        public List<Location> Locations { get; set; }
        public int Meters { get; set; } //not part of init migration
        public User Creator { get; set; } //not part of init migration
        public List<Comment> Comments { get; set; } //not part of init migration
    }
}