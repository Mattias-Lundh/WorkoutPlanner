using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkoutPlanner.Domain.Models;

namespace WorkoutPlanner.Data
{
    public class Database
    {
        private WorkoutPlannerContext context;

        Database()
        {
            context = new WorkoutPlannerContext();
        }

        #region Track
        public void AddTrack(Track track)
        {
            context.Add(track);
            context.SaveChanges();
        }

        public void AddTracks(ICollection<Track> tracks)
        {
            context.AddRange(tracks);
            context.SaveChanges();
        }

        public List<Track> GetTrackByNameSearch(string searchStr)
        {
            return context.Tracks.Where(t => t.Name.Contains(searchStr)).ToList();
        }

        public List<Track> GetTrackByUserId(int userId)
        {
            List<Track> result = new List<Track>();
            var users = context.Users
                .Where(u => u.Id == userId)
                .Include(u => u.Workouts)
                    .ThenInclude(w => w.Track)
                .ToList();

            users.ForEach(u => u.Workouts.ForEach(w => result.Add(w.Track)));
            return result;
        }
        #endregion

        #region User
        public User GetUserById(int id)
        {
            return context.Users.Find(id);
        }

        public List<User> GetUsersByTrackId(int id)
        {
            List<User> result = new List<User>();
            var users = context.Users
                                    .Include(u => u.Workouts)
                                        .ThenInclude(w => w.Track)
                                    .ToList();

            foreach (User user in users)
            {
                foreach(Workout workout in user.Workouts)
                {
                    if(workout.Track.Id == id)
                    {
                        result.Add(user);
                    }
                }
            }
            return result;
        }
        #endregion
    }
}
