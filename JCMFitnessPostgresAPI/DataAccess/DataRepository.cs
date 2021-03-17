using JCMFitnessPostgresAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JCMFitnessPostgresAPI.DataAccess
{
    public class DataRepository : IDataRepository
    {
        private readonly ApiDBContext _context;

        public DataRepository(ApiDBContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        //Workout*******************************

        public async Task<IEnumerable<Workout>> GetWorkoutListAsync()
        {
            return await EntityFrameworkQueryableExtensions.ToListAsync(_context.Workouts);

        }

        public async Task AddWorkoutAsync(Workout workout)
        {

            _context.Workouts.Add(workout);
            await _context.SaveChangesAsync();
        }


        public async Task EditWorkoutAsync(Workout workout)
        {
            _context.Update(workout);
            await _context.SaveChangesAsync();
        }

        public async Task<Workout> GetWorkoutAsync(string workoutID)
        {
            return await _context.Workouts
                .FirstOrDefaultAsync(r => r.WorkoutID == workoutID);
        }

        public async Task DeleteWorkoutAsync(string workoutID)
        {
            var workout = await _context.Workouts.FindAsync(workoutID);

            _context.Workouts.Remove(workout);
            await _context.SaveChangesAsync();
        }



        //User *******************************
        public async Task<User> GetUserAsync(string userID)
        {
           /* return await _context.Users.Include(r => r.UserWorkouts)
               .Include(p => p.UserWorkouts)
               .ThenInclude(pc => pc.)
               .FirstOrDefaultAsync(r => r.ID == postID);*/

            return await Task.Run(() => _context.Users
            .First(r => r.UserID == userID));
        }

        public async Task AddUserAsync(User user)
        {

            _context.Users.Add(user);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<User>> GetUsersAsync()
        {
            return await _context.Users.ToListAsync();
        }

        public async Task EditUserAsync(User user)
        {

            _context.Update(user);
            await _context.SaveChangesAsync();

        }


        public async Task DeleteUserAsync(string userID)
        {
            var user = await _context.Users.FindAsync(userID);

            _context.Users.Remove(user);
            await _context.SaveChangesAsync();
        }

        //UserWorkout*******************************

        public async Task<IEnumerable<UserWorkout>> GetUserWorkoutsListAsync()
        {
            return await _context.UserWorkouts.ToListAsync();
        }

        public async Task AddUserWorkoutAsync(string workoutID, string userID)
        {

            var user = await EntityFrameworkQueryableExtensions.FirstOrDefaultAsync(_context.Users, c => c.UserID == userID);
            var workout = await EntityFrameworkQueryableExtensions.FirstOrDefaultAsync(_context.Workouts, c => c.WorkoutID == workoutID);

            //var workouts = await EntityFrameworkQueryableExtensions.ToListAsync(_context.Workouts);

            //var workout = workouts.FirstOrDefault(c => c.WorkoutID == workoutID);
         

            var newUserWorkout = new UserWorkout()
            {
                Id = Guid.NewGuid().ToString("n"),
                WorkoutID = workout.WorkoutID,
                UserID = userID,
                Workout = workout,
                User = user,
                IsPublic = true,
            };

            _context.UserWorkouts.Add(newUserWorkout);
            await _context.SaveChangesAsync();

        }


        public async Task<IEnumerable<Workout>> GetUserWorkoutsAsync(string userID)
        {
            //var workoutList = await EntityFrameworkQueryableExtensions.ToListAsync(_context.Workouts);
            //var userWorkoutList = await EntityFrameworkQueryableExtensions.ToListAsync(_context.UserWorkouts);

            //var usersList = await EntityFrameworkQueryableExtensions.ToListAsync(_context.Users);

            /*var userWorkouts = _context.UserWorkouts
                    .Where(m => m.UserID == userID)
                    .SelectMany(m => m.WorkoutID)
                    .ToList();

            return userWorkouts;*/

            return await Task.Run(() => _context.UserWorkouts
                    .Where(m => m.UserID == userID)
                    .Select(m => m.Workout)
                    .ToList());
        }

        public async Task DeleteUserWorkoutAsync(string workoutID, string userID)
        {

            //var workout = _context.UserWorkouts.Where(m => m.UserID == userID).Where(m => m.WorkoutID == workoutID).Select(m => m.Workout);

            var userWorkout = _context.UserWorkouts.Where(m => m.UserID == userID).Where(m => m.WorkoutID == workoutID).ToList();

            _context.UserWorkouts.Remove(userWorkout.FirstOrDefault(m => m.WorkoutID == workoutID));

            await _context.SaveChangesAsync();
        }

        public async Task DeleteUserWorkoutListAsync(string userID)
        {

            var userWorkout = _context.UserWorkouts.Where(m => m.UserID == userID).ToList();

            foreach(var i in userWorkout)
            {
                _context.UserWorkouts.Remove(i);
            }

            await _context.SaveChangesAsync();
        }
    }
}
