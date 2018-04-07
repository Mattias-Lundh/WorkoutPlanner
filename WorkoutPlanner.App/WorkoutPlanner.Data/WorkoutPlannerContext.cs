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
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Session> Sessions { get; set; }
        public DbSet<Achievement> Achievements { get; set; }
        public DbSet<UserAchievement> UserAchievements { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().HasIndex(m => m.Email).IsUnique();
            modelBuilder.Entity<UserAchievement>().HasKey(ua => new { ua.AchievementId, ua.UserId });
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //server connection for Azure live version
            //optionsBuilder.UseSqlServer(String.Format("Server = tcp:workoutplanner.database.windows.net, 1433; Initial Catalog = WorkoutPlanner; Persist Security Info = False; User ID = mattias.lundh; Password ={0}; MultipleActiveResultSets = False; Encrypt = True; TrustServerCertificate = False; Connection Timeout = 30;", Environment.GetEnvironmentVariable("SQLAZURECONNSTR_WorkoutPlanner") ));

            //server connection for local development testing
            optionsBuilder.UseSqlServer("Server = (localdb)\\mssqllocaldb; Database = WorkoutPlannerDB; Trusted_Connection = True;");
        }
    }
}
