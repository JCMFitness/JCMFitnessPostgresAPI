using JCMFitnessPostgresAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JCMFitnessPostgresAPI.DataAccess
{
    public class ApiDBContext : DbContext
    {
        public ApiDBContext(DbContextOptions<ApiDBContext> options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Workout> Workouts { get; set; }
        public DbSet<UserWorkout> UserWorkouts { get; set; }
        public DbSet<Exercise> Exercises { get; set; }
        public DbSet<WorkoutExercises> WorkoutExercises { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
           /* modelBuilder.Entity<UserWorkout>()
                .HasKey(t => new { t.UserID, t.WorkoutID });*/

            modelBuilder.Entity<UserWorkout>()
                .HasOne(pt => pt.Workout)
                .WithMany(p => p.UserWorkouts)
                .HasForeignKey(pt => pt.WorkoutID);

            modelBuilder.Entity<UserWorkout>()
                .HasOne(pt => pt.User)
                .WithMany(t => t.UserWorkouts)
                .HasForeignKey(pt => pt.UserID);

            modelBuilder.Entity<WorkoutExercises>()
                .HasOne(pt => pt.Workout)
                .WithMany(t => t.WorkoutExercises)
                .HasForeignKey(pt => pt.WorkoutID);

            modelBuilder.Entity<WorkoutExercises>()
                .HasOne(pt => pt.Exercise)
                .WithMany(p => p.WorkoutExercises)
                .HasForeignKey(pt => pt.ExerciseID);
        }
    }
}
