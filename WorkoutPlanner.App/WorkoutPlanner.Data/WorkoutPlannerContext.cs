using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkoutPlanner.Domain.Models;
using WorkoutPlanner.Domain.API;


namespace WorkoutPlanner.Data
{
    public class WorkoutPlannerContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Location> Locations { get; set; }
        public DbSet<Track> Tracks { get; set; }
        public DbSet<Workout> Workouts { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            Client client = new Client();
            optionsBuilder.UseSqlServer(String.Format("Server = tcp:workoutplanner.database.windows.net, 1433; Initial Catalog = WorkoutPlanner; Persist Security Info = False; User ID = mattias.lundh; Password ={0}; MultipleActiveResultSets = False; Encrypt = True; TrustServerCertificate = False; Connection Timeout = 30;", Environment.GetEnvironmentVariable("SQLAZURECONNSTR_WorkoutPlanner")));
        }
    }
}
