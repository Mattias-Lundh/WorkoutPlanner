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
        #region Track
        public void AddTrack(Track track)
        {
            using (WorkoutPlannerContext context = new WorkoutPlannerContext())
            {
                context.Tracks.Add(track);
                context.SaveChanges();
            }
        }

        public void AddTracks(ICollection<Track> tracks)
        {
            using (WorkoutPlannerContext context = new WorkoutPlannerContext())
            {
                context.Tracks.AddRange(tracks);
                context.SaveChanges();
            }
        }

        public Track GetTrackByNameSearch(string searchStr)
        {
            using (WorkoutPlannerContext context = new WorkoutPlannerContext())
            {
                return context.Tracks.Where(t => t.Name.Contains(searchStr)).FirstOrDefault();
            }
        }

        public List<Track> GetTracksByNameSearch(string searchStr)
        {
            using (WorkoutPlannerContext context = new WorkoutPlannerContext())
            {
                return context.Tracks
                    .Where(t => t.Name.Contains(searchStr))
                    .Include(t => t.Locations)
                    .ToList();
            }
        }

        public List<Track> GetTrackByUserId(int userId)
        {
            List<Track> result = new List<Track>();
            using (WorkoutPlannerContext context = new WorkoutPlannerContext())
            {
                var users = context.Users
                .Where(u => u.Id == userId)
                .Include(u => u.Workouts)
                    .ThenInclude(w => w.Track)
                .ToList();

                users.ForEach(u => u.Workouts.ForEach(w => result.Add(w.Track)));
            }
            return result;
        }
        #endregion

        #region User
        public void AddUser(User user)
        {
            using (WorkoutPlannerContext context = new WorkoutPlannerContext())
            {
                context.Users.Add(user);
                context.SaveChanges();
            }
        }

        public User GetUserById(int id)
        {
            using (WorkoutPlannerContext context = new WorkoutPlannerContext())
            {
                return context.Users.Find(id);
            }
        }

        public User GetUserByEmail(string email)
        {
            using (WorkoutPlannerContext context = new WorkoutPlannerContext())
            {
                return context.Users.Where(u => u.Email == email).FirstOrDefault();
            }
        }

        public List<User> GetUsersByTrackId(int id)
        {
            List<User> result = new List<User>();

            using (WorkoutPlannerContext context = new WorkoutPlannerContext())
            {
                var users = context.Users
                      .Include(u => u.Workouts)
                         .ThenInclude(w => w.Track)
                      .ToList();

                foreach (User user in users)
                {
                    foreach (Workout workout in user.Workouts)
                    {
                        if (workout.Track.Id == id)
                        {
                            result.Add(user);
                        }
                    }
                }
            }

            return result;
        }
        #endregion
    }
}
