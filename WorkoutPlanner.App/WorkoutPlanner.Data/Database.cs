using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using WorkoutPlanner.Domain.Models;

namespace WorkoutPlanner.Data
{
    public class Database
    {
        #region Track
        /// <summary>
        /// Single object add
        /// </summary>
        /// <param name="track"> The track object to be added to database</param>
        public void AddTrack(Track track)
        {
            using (WorkoutPlannerContext context = new WorkoutPlannerContext())
            {
                context.Tracks.Add(track);
                context.SaveChanges();
            }
        }
        /// <summary>
        /// multiple object add
        /// </summary>
        /// <param name="tracks">the track collection to be added to the databse</param>
        public void AddTracks(ICollection<Track> tracks)
        {
            using (WorkoutPlannerContext context = new WorkoutPlannerContext())
            {
                context.Tracks.AddRange(tracks);
                context.SaveChanges();
            }
        }

        /// <summary>
        /// multiple object get
        /// </summary>
        /// <returns>all tracks</returns>
        public List<Track> GetAllTracks()
        {
            using (WorkoutPlannerContext context = new WorkoutPlannerContext())
            {
                return context.Tracks.ToList();
            }
        }

        /// <summary>
        /// single object get,
        /// using track id as selection parameter
        /// </summary>
        /// <param name="trackId">value that the query tries to match</param>
        /// <returns>a single track mactched by input parameter</returns>
        public Track GetTrackById(int trackId)
        {
            using (WorkoutPlannerContext context = new WorkoutPlannerContext())
            {
                return context.Tracks.Find(trackId);
            }
        }

        /// <summary>
        /// single object get,
        /// using track name as selection parameter,
        /// "LIKE" matching,
        /// includes locations,
        /// includes comments
        /// </summary>
        /// <param name="searchStr">the string that the query tries to match</param>
        /// <returns>a single track matched by the input parameter</returns>
        public Track GetTrackByNameSearch(string searchStr)
        {
            using (WorkoutPlannerContext context = new WorkoutPlannerContext())
            {
                return context.Tracks.FromSql("SELECT * FROM Tracks WHERE Name LIKE '%@searchStr%'", searchStr)
                    .Include(t => t.Locations)
                    .Include(t => t.Comments)
                    .FirstOrDefault();
            }
        }

        /// <summary>
        /// multiple object get,
        /// using track name as selection parameter,
        /// "LIKE" matching,
        /// includes locations,
        /// includes comments
        /// </summary>
        /// <param name="searchStr">the string that the query tries to match</param>
        /// <returns>all tracks matched by the input parameter</returns>
        public List<Track> GetTracksByNameSearch(string searchStr)
        {
            using (WorkoutPlannerContext context = new WorkoutPlannerContext())
            {
                return context.Tracks.FromSql("SELECT * FROM Tracks WHERE Name LIKE '%@searchStr%'", searchStr)
                    .Include(t => t.Comments)
                    .Include(t => t.Locations)
                    .ToList();
            }
        }

        /// <summary>
        /// multiple object get,
        /// finds all tracks belonging to workouts belonging to a user
        /// </summary>
        /// <param name="userId">value that the query tries to match</param>
        /// <returns>all tracks matched by the input parameter</returns>
        public List<Track> GetWorkoutTracksByUserId(int userId)
        {
            List<Track> result = new List<Track>();
            using (WorkoutPlannerContext context = new WorkoutPlannerContext())
            {
                var users = context.Users
                .Where(u => u.Id == userId)
                .Include(u => u.Workouts)
                    .ThenInclude(w => w.Track)
                        .ThenInclude(t => t.Comments)
                .Include(u => u.Workouts)
                    .ThenInclude(w => w.Track)
                        .ThenInclude(t => t.Locations)
                .ToList();

                users.ForEach(u => u.Workouts.ForEach(w => result.Add(w.Track)));
            }
            return result;
        }

        /// <summary>
        /// multiple object get,
        /// finds all tracks based on the creator of the track
        /// </summary>
        /// <param name="creator">the id of the creator</param>
        /// <returns>all tracks matched by the input parameter</returns>
        public List<Track> GetTrackByCreator(int creator)
        {
            using (WorkoutPlannerContext context = new WorkoutPlannerContext())
            {
                return context.Tracks
                    .Where(t => t.Creator == creator)
                    .Include(t => t.Locations)
                    .Include(t => t.Comments)
                    .ToList();
            }
        }
        #endregion

        #region User
        /// <summary>
        /// single object add
        /// </summary>
        /// <param name="user">User to be added</param>
        public void AddUser(User user)
        {
            using (WorkoutPlannerContext context = new WorkoutPlannerContext())
            {
                context.Users.Add(user);
                context.SaveChanges();
            }
        }
        /// <summary>
        /// single object get
        /// </summary>
        /// <param name="id">id of User to be found</param>
        /// <returns>User object</returns>
        public User GetUserById(int id)
        {
            using (WorkoutPlannerContext context = new WorkoutPlannerContext())
            {
                return context.Users.Find(id);
            }
        }
        /// <summary>
        /// single object get
        /// </summary>
        /// <param name="email">email of user to be found</param>
        /// <returns>User object</returns>
        public User GetUserByEmail(string email)
        {
            using (WorkoutPlannerContext context = new WorkoutPlannerContext())
            {
                return context.Users.Where(u => u.Email == email).FirstOrDefault();
            }
        }
        /// <summary>
        /// multiple object get
        /// </summary>
        /// <param name="id">if of Track to be matched</param>
        /// <returns>A list of Users</returns>
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
        /// <summary>
        /// single object add
        /// </summary>
        /// <param name="workout">Workout to be added</param>
        public void AddWorkout(Workout workout)
        {
            using (WorkoutPlannerContext context = new WorkoutPlannerContext())
            {
                context.Workouts.Add(workout);
                context.SaveChanges();
            }
        }
        /// <summary>
        /// multiple object get
        /// </summary>
        /// <param name="userId">User id to be matched</param>
        /// <returns>A list of workouts</returns>
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
        /// <summary>
        /// single object update
        /// </summary>
        /// <param name="workout"></param>
        public void UpdateWorkoutTime(Workout workout)
        {
            using (WorkoutPlannerContext context = new WorkoutPlannerContext())
            {
                var targetWorkout = context.Workouts.Find(workout.Id);
                targetWorkout.Minutes = workout.Minutes;
                context.Workouts.Update(targetWorkout);
                context.SaveChanges();
            }
        }
        #endregion

        #region Achievement
        /// <summary>
        /// multiple object get
        /// </summary>
        /// <returns>a list of all Achievements</returns>
        public List<Achievement> GetAllAchievements()
        {
            using (WorkoutPlannerContext context = new WorkoutPlannerContext())
            {
                return context.Achievements.ToList();
            }
        }

        public List<object> GetAllAchievemntTypeAndDescription()
        {
            using (WorkoutPlannerContext context = new WorkoutPlannerContext())
            {
                List<object> result = new List<object>();
                var achievements = context.Achievements
                    .Select(a => new { a.Type, a.Description })                    
                    .ToList();
                achievements.ForEach(o => result.Add(o));
                return result;
            }
        }

        /// <summary>
        /// multiple object get
        /// </summary>
        /// <param name="userId">user id to be matched</param>
        /// <returns>a list of achievements</returns>
        public List<object> GetAchievementsTypeAndDescriptionByUserId(int userId)
        {
            using (WorkoutPlannerContext context = new WorkoutPlannerContext())
            {
                List<object> result = new List<object>();
                User user = context.Users
                    .Where(u => u.Id == userId)
                    .Include(u => u.UserAchievements)
                        .ThenInclude(ua => ua.Achievement)
                    .FirstOrDefault();

                user.UserAchievements.ForEach(ua => result.Add(new { ua.Achievement.Type, ua.Achievement.Description }));
                return result;
            }
        }
        /// <summary>
        /// multiple object get,
        /// </summary>
        /// <param name="distanceQualification"></param>
        /// <returns>a list of achievements of the accomplishment type</returns>
        public List<Achievement> GetAllAccomplishmentAchievementsByQualification(int distanceQualification)
        {
            using (WorkoutPlannerContext context = new WorkoutPlannerContext())
            {
                return context.Achievements
                    .Where(a => a.Type == "Accomplishment" && a.Value < distanceQualification)
                    .ToList();
            }
        }
        /// <summary>
        /// multiple object get
        /// </summary>
        /// <param name="distanceQualification"></param>
        /// <returns>a list of accomplishments of the distance type</returns>
        public List<Achievement> GetAllDistanceAchievementsByQualification(int distanceQualification)
        {
            using (WorkoutPlannerContext context = new WorkoutPlannerContext())
            {
                return context.Achievements
                    .Where(a => a.Type == "Distance" && a.Value < distanceQualification)
                    .ToList();
            }
        }
        /// <summary>
        /// multiple object get
        /// </summary>
        /// <param name="durationQualification"></param>
        /// <returns>a list of achievements of the duration type</returns>
        public List<Achievement> GetAllDurationAchievementsByQualification(int durationQualification)
        {
            using (WorkoutPlannerContext context = new WorkoutPlannerContext())
            {
                return context.Achievements
                    .Where(a => a.Type == "Duration" && a.Value < durationQualification)
                    .ToList();
            }
        }
        /// <summary>
        /// single object add
        /// </summary>
        /// <param name="achievement">the Achievement to be added</param>
        public void AddAchievement(Achievement achievement)
        {
            using (WorkoutPlannerContext context = new WorkoutPlannerContext())
            {
                context.Achievements.Add(achievement);
                context.SaveChanges();
            }
        }

        /// <summary>
        /// counts all records in the achievement table
        /// </summary>
        /// <returns>an int representing the number of records</returns>
        public int CountAchievements()
        {
            using (WorkoutPlannerContext context = new WorkoutPlannerContext())
            {
                return context.Achievements.Count();
            }
        }
        /// <summary>
        /// run database stored procedure,
        /// adds default Achievement data
        /// </summary>
        public void LoadDefaultAchievementData()
        {
            using (WorkoutPlannerContext context = new WorkoutPlannerContext())
            {
                context.Database.ExecuteSqlCommand("EXEC dbo.LoadAchievementDefaults");
            }
        }
        #endregion

        #region UserAchievement
        /// <summary>
        /// single object add
        /// </summary>
        /// <param name="userAchievement">UserAchievement to be added</param>
        public void AddUserAchievement(UserAchievement userAchievement)
        {
            using (WorkoutPlannerContext context = new WorkoutPlannerContext())
            {
                context.UserAchievements.Add(userAchievement);
                context.SaveChanges();
            }
        }
        /// <summary>
        /// multiple object get
        /// </summary>
        /// <param name="userId">id to be matched</param>
        /// <returns>a list of user achievements</returns>
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
        /// <summary>
        /// single object add
        /// </summary>
        /// <param name="session">the session to be added</param>
        public void AddSession(Session session)
        {
            using (WorkoutPlannerContext context = new WorkoutPlannerContext())
            {
                context.Sessions.Add(session);
                context.SaveChanges();
            }
        }
        /// <summary>
        /// multiple object get
        /// </summary>
        /// <param name="userId">id to be matched</param>
        /// <returns>a list of sessions belonging to a user</returns>
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
        /// <summary>
        /// single object delete
        /// </summary>
        /// <param name="sessionId">id of session to be deleted</param>
        public void RemoveSessionById(int sessionId)
        {
            using (WorkoutPlannerContext context = new WorkoutPlannerContext())
            {
                Session session = context.Sessions.Find(sessionId);
                context.Sessions.Remove(session);
                context.SaveChanges();
            }
        }
        /// <summary>
        /// multiple object delete,
        /// deletes all sessions from a user
        /// </summary>
        /// <param name="userId">user id to be matched</param>
        public void RemoveSessionsByUserId(int userId)
        {
            using (WorkoutPlannerContext context = new WorkoutPlannerContext())
            {
                var sessions = context.Sessions
                    .Where(s => s.UserId == userId)
                    .ToList();
                context.Sessions.RemoveRange(sessions);
                context.SaveChanges();
            }
        }
        /// <summary>
        /// single object add,
        /// async,
        /// </summary>
        /// <param name="sessions">collection of sessions to be added</param>
        /// <returns></returns>
        public async Task<int> AddSessionsAsync(List<Session> sessions)
        {
            using (WorkoutPlannerContext context = new WorkoutPlannerContext())
            {
                context.Sessions.AddRange(sessions);
                Thread.Sleep(2000);
                return await context.SaveChangesAsync();
            }
        }

        #endregion

        #region Comments
        /// <summary>
        /// single object add
        /// </summary>
        /// <param name="comment">comment to be added</param>
        public void AddComment(Comment comment)
        {
            using (WorkoutPlannerContext context = new WorkoutPlannerContext())
            {
                context.Comments.Add(comment);
                context.SaveChanges();
            }
        }
        #endregion
    }
}
