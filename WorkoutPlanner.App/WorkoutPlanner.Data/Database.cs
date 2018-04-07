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

        public List<Track> GetWorkoutTracksByUserId(int userId)
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

        public List<Track> GetTrackByCreator(int creator)
        {
            using (WorkoutPlannerContext context = new WorkoutPlannerContext())
            {
                var tracks = context.Tracks
                    .Where(t => t.Creator == creator).ToList();

                return tracks;
            }
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

        #region Workout
        public void AddWorkout(Workout workout)
        {
            using (WorkoutPlannerContext context = new WorkoutPlannerContext())
            {
                context.Workouts.Add(workout);
                context.SaveChanges();
            }
        }

        public List<Workout> GetWorkoutsByUserId(int userId)
        {
            List<Workout> result = new List<Workout>();
            using (WorkoutPlannerContext context = new WorkoutPlannerContext())
            {
                var user = context.Users
                    .Where(u => u.Id == userId)
                    .Include(u => u.Workouts)
                        .ThenInclude(w => w.Track)
                    .FirstOrDefault();
                user.Workouts.ForEach(w => result.Add(w));
            }
            return result;
        }
        #endregion

        #region Achievement
        public List<Achievement> GetAllAchievements()
        {
            using (WorkoutPlannerContext context = new WorkoutPlannerContext())
            {
                return context.Achievements.ToList();
            }
        }

        public List<Achievement> GetAchievementsByUserId(int userId)
        {
            using (WorkoutPlannerContext context = new WorkoutPlannerContext())
            {
                List<Achievement> result = new List<Achievement>();
                User user = context.Users
                    .Where(u => u.Id == userId)
                    .Include(u => u.UserAchievements)
                        .ThenInclude(ua => ua.Achievement)
                    .FirstOrDefault();

                user.UserAchievements.ForEach(ua => result.Add(ua.Achievement));
                return result;
            }
        }

        public List<Achievement> GetAllAccomplishmentAchievementsByQualification(int distanceQualification)
        {
            using (WorkoutPlannerContext context = new WorkoutPlannerContext())
            {
                return context.Achievements
                    .Where(a => a.Type == "Accomplishment" && a.Value < distanceQualification)
                    .ToList();
            }
        }

        public List<Achievement> GetAllDistanceAchievementsByQualification(int distanceQualification)
        {
            using (WorkoutPlannerContext context = new WorkoutPlannerContext())
            {
                return context.Achievements
                    .Where(a => a.Type == "Distance" && a.Value < distanceQualification)
                    .ToList();
            }
        }

        public List<Achievement> GetAllDurationAchievementsByQualification(int durationQualification)
        {
            using (WorkoutPlannerContext context = new WorkoutPlannerContext())
            {
                return context.Achievements
                    .Where(a => a.Type == "Duration" && a.Value < durationQualification)
                    .ToList();
            }
        }

        public void AddAchievement(Achievement achievement)
        {
            using (WorkoutPlannerContext context = new WorkoutPlannerContext())
            {
                context.Achievements.Add(achievement);
                context.SaveChanges();
            }
        }
        #endregion

        #region UserAchievement
        public void AddUserAchievement(UserAchievement userAchievement)
        {
            using (WorkoutPlannerContext context = new WorkoutPlannerContext())
            {
                context.UserAchievements.Add(userAchievement);
                context.SaveChanges();
            }
        }

        public List<UserAchievement> GetUserAchievementsForUser(int userId)
        {
            using (WorkoutPlannerContext context = new WorkoutPlannerContext())
            {
                return context.UserAchievements
                    .Where(ua => ua.UserId == userId)
                    .ToList();
            }
        }
        #endregion

        #region Session
        public List<Session> GetSessionsByUserId(int userId)
        {
            using (WorkoutPlannerContext context = new WorkoutPlannerContext())
            {
                return context.Sessions
                    .Where(s => s.UserId == userId)
                    .Include(s => s.Track)
                    .ToList();
            }
        }
        #endregion
    }
}
