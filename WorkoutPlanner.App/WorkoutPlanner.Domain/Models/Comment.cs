using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkoutPlanner.Domain.Models
{
    public class Comment
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public Track Track { get; set; }
        public int TrackId { get; set; }
        public User Author { get; set; }
        public int AuthorId { get; set; }
    }
}
