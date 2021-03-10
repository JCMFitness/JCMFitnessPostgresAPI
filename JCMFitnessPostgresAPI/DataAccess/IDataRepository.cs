using JCMFitnessPostgresAPI.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JCMFitnessPostgresAPI.DataAccess
{
    public interface IDataRepository
    {
        //Workout
        public Task<IEnumerable<Workout>> GetWorkoutListAsync();
        public Task AddWorkoutAsync(Workout workout);
        public Task<Workout> GetWorkoutAsync(string id);
        public Task EditWorkoutAsync(Workout workout);

        //User
        public Task<IEnumerable<User>> GetUsersAsync();
        public Task AddUserAsync(User User);
        public Task<User> GetUserAsync(string id);
        public Task EditUserAsync(User user);


        //UserWorkout
        public Task<IEnumerable<UserWorkout>> GetUserWorkoutsListAsync();
        public Task AddUserWorkoutAsync(string workoutID, string userID);
        public Task<IEnumerable<Workout>> GetUserWorkoutsAsync(string userID);

    }

}
