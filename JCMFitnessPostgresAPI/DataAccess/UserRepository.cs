using JCMFitnessPostgresAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JCMFitnessPostgresAPI.DataAccess
{
    public class UserRepository : IUserRepository
    {
        private readonly ApiDBContext _context;

        public UserRepository(ApiDBContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Workout>> GetWorkoutListAsync()
        {
            return await EntityFrameworkQueryableExtensions.ToListAsync(_context.Workouts);

        }

        public async Task AddWorkoutAsync(Workout workout, string userID)
        {

            var tempWorkout = await EntityFrameworkQueryableExtensions.FirstOrDefaultAsync(_context.Workouts, c => c.WorkoutID == workout.WorkoutID);


            if (tempWorkout == null)
            {
                tempWorkout = new Workout { 
                    WorkoutID = workout.WorkoutID, Name = workout.Name, Description = workout.Description, Category = workout.Category, IsPublic = workout.IsPublic };
                _context.Workouts.Add(tempWorkout);
                await _context.SaveChangesAsync();
            }

            var newUserWorkout = new UserWorkout()
            {
                WorkoutID = tempWorkout.WorkoutID,
                UserID = userID
            };

            _context.UserWorkouts.Add(newUserWorkout);
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
                .Include(p => p.UserWorkouts)
                .ThenInclude(pc => pc.User)
                .FirstOrDefaultAsync(r => r.WorkoutID == workoutID);
        }

        public async Task<User> GetUserAsync(string userID)
        {
            return await Task.Run(() => _context.Users
            .Include(c => c.UserWorkouts)
            .ThenInclude(pc => pc.Workout)
            .First(r => r.UserID == userID));
        }

        public async Task AddUserAsync(User user)
        {

            _context.Users.Add(user);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<User>> GetUsersAsync()
        {
            return await _context.Users.Include(c => c.UserWorkouts).ThenInclude(r => r.Workout).ToListAsync();
        }

        public async Task EditUserAsync(User user)
        {

            _context.Update(user);
            await _context.SaveChangesAsync();

        }

        public async Task<IEnumerable<Workout>> GetUserWorkoutsAsync(string userID)
        {
            //var workoutList = await EntityFrameworkQueryableExtensions.ToListAsync(_context.Workouts);
            //var userWorkoutList = await EntityFrameworkQueryableExtensions.ToListAsync(_context.UserWorkouts);

            //var usersList = await EntityFrameworkQueryableExtensions.ToListAsync(_context.Users);

            var userWorkouts = _context.Users
                    .Where(m => m.UserID == userID)
                    .SelectMany(m => m.UserWorkouts.Select(mc => mc.Workout))
                    .ToList();

            return userWorkouts;
        }

        /*public void AddUser(User user)
        {
            _context.Users.Add(user);
            _context.SaveChanges();
        }

        public void UpdateUser(User user)
        {
            _context.Users.Update(user);
            _context.SaveChanges();
        }

        public void DeleteUser(string id)
        {
            var entity = _context.Users.FirstOrDefault(t => t.UserID == id);
            _context.Users.Remove(entity);
            _context.SaveChanges();
        }

        public User GetUserByID(string id)
        {
            return _context.Users.FirstOrDefault(t => t.UserID == id);
        }

        public List<User> GetAllUsers()
        {
            return _context.Users.ToList();
        }*/
    }
}
