using JCMFitnessPostgresAPI.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JCMFitnessPostgresAPI.DataAccess
{
    public interface IUserRepository
    {
        public Task<IEnumerable<Workout>> GetWorkoutListAsync();
        public Task AddWorkoutAsync(Workout workout, string userID);
        public Task<Workout> GetWorkoutAsync(string id);


        public Task<IEnumerable<User>> GetUsersAsync();
        public Task AddUserAsync(User User);
        public Task<User> GetUserAsync(string id);


        public Task EditWorkoutAsync(Workout workout);
        public Task EditUserAsync(User user);

        public Task<IEnumerable<Workout>> GetUserWorkoutsAsync(string userID);

    }

}
