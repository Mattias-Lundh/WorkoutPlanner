using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkoutPlanner.Domain.Models
{
    public class Comment //not part of initial migration
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public User Author { get; set; }
    }
}
