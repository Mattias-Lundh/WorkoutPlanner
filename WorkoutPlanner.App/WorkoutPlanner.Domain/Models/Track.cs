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
        public string Name { get; set; }
        public List<Location> Locations { get; set; }
        [Required]
        public int Meters { get; set; }
        [Required]
        public int Creator { get; set; }
        public List<Comment> Comments { get; set; }
    }
}