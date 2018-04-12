using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WorkoutPlanner.Domain.Models;
using WorkoutPlanner.Data;

namespace WorkoutPlanner.App.Models
{
    public class ScheduleService
    {
        private Random rng = new Random();

        public List<Session> GenerateShedule(int userId)
        {
            List<Session> result = new List<Session>();
            Database db = new Database();
            List<Track> tracks = new List<Track>();
            tracks = db.GetAllTracks();

            if (tracks.Count < 6)
            {
                for (int i = 0; i < tracks.Count ; i++)
                {
                    result.Add(new Session { UserId = userId, Date = DateTime.Now.AddDays(1 + 2 * i), TrackId = tracks[rng.Next(tracks.Count)].Id });
                }
                return result;
            }

            result.AddRange(new List<Session>{ 
                new Session { UserId = userId, Date = DateTime.Now.AddDays(1), TrackId = tracks[rng.Next(tracks.Count)].Id},
                new Session { UserId = userId, Date = DateTime.Now.AddDays(3), TrackId = tracks[rng.Next(tracks.Count)].Id},
                new Session { UserId = userId, Date = DateTime.Now.AddDays(5), TrackId = tracks[rng.Next(tracks.Count)].Id},
                new Session { UserId = userId, Date = DateTime.Now.AddDays(7), TrackId = tracks[rng.Next(tracks.Count)].Id},
                new Session { UserId = userId, Date = DateTime.Now.AddDays(9), TrackId = tracks[rng.Next(tracks.Count)].Id},
                new Session { UserId = userId, Date = DateTime.Now.AddDays(11), TrackId = tracks[rng.Next(tracks.Count)].Id},
                });
            return result;
        }
    }
}