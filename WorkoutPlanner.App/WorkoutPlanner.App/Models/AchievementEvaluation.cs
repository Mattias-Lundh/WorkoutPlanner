using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WorkoutPlanner.Domain.Models;
using WorkoutPlanner.Data;

namespace WorkoutPlanner.App.Models
{
    public class AchievementEvaluation
    {
        public AchievementEvaluation()
        {
            _qualifiedAchievements = new List<Achievement>();
            _db = new Database();

        }
        private List<Achievement> _qualifiedAchievements;
        private Database _db;

        public void EvaluateWorkouts(IEnumerable<Workout> workouts)
        {
            List<Achievement> result = new List<Achievement>();
            List<Achievement> allAchievements = _db.GetAllAchievements();
            int maxDistance = 0;
            int totalDistance = 0;
            int totalDuration = 0;

            foreach (var workout in workouts)
            {
                maxDistance = Math.Max(workout.Track.Meters/1000, maxDistance);
                totalDistance += workout.Track.Meters/1000;
                totalDuration += workout.Minutes/60;
            }
            result.AddRange(_db.GetAllAccomplishmentAchievementsByQualification(maxDistance));
            result.AddRange(_db.GetAllDistanceAchievementsByQualification(totalDistance));
            result.AddRange(_db.GetAllDurationAchievementsByQualification(totalDuration));
            _qualifiedAchievements.AddRange(result);
        }

        public void EvaluateWorkout(Workout workout)
        {
            List<Achievement> result = new List<Achievement>();
            result.AddRange(_db.GetAllAccomplishmentAchievementsByQualification(workout.Track.Meters));
            _qualifiedAchievements.AddRange(result);
        }

        public void GrantAwardsToUser(int userId)
        {
            List<UserAchievement> previouslyAwarded = _db.GetUserAchievementsForUser(userId);
            foreach (var achievement in _qualifiedAchievements)
            {
                UserAchievement uA = new UserAchievement { UserId = userId, AchievementId = achievement.Id };
                if (null == previouslyAwarded
                    .Where(ua => ua.UserId == uA.UserId && ua.AchievementId == uA.AchievementId)
                    .FirstOrDefault())
                {
                    _db.AddUserAchievement(uA);
                }
            }
        }
    }
}