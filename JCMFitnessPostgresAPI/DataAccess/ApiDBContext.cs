using JCMFitnessPostgresAPI.Authentication;
using JCMFitnessPostgresAPI.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JCMFitnessPostgresAPI.DataAccess
{
    public class ApiDBContext : IdentityDbContext<ApiUser>
    {
        public ApiDBContext(DbContextOptions<ApiDBContext> options) : base(options)
        {
        }

        //public DbSet<User> Users { get; set; }
        public DbSet<Workout> Workouts { get; set; }
        public DbSet<UserWorkout> UserWorkouts { get; set; }
        public DbSet<Exercise> Exercises { get; set; }
        public DbSet<WorkoutExercises> WorkoutExercises { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            /* modelBuilder.Entity<UserWorkout>()
                 .HasKey(t => new { t.UserID, t.WorkoutID });*/

            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<UserWorkout>()
                .HasOne(pt => pt.Workout)
                .WithMany(p => p.UserWorkouts)
                .HasForeignKey(pt => pt.WorkoutID)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<UserWorkout>()
                .HasOne(pt => pt.User)
                .WithMany(t => t.UserWorkouts)
                .HasForeignKey(pt => pt.UserID)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<WorkoutExercises>()
                .HasOne(pt => pt.Workout)
                .WithMany(t => t.WorkoutExercises)
                .HasForeignKey(pt => pt.WorkoutID)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<WorkoutExercises>()
                .HasOne(pt => pt.Exercise)
                .WithMany(p => p.WorkoutExercises)
                .HasForeignKey(pt => pt.ExerciseID)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
