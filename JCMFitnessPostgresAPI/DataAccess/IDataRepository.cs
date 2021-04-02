using JCMFitnessPostgresAPI.Authentication;
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
        public Task DeleteWorkoutAsync(string id);
        public bool WorkoutExists(string workoutID);

        //User
        public Task<IEnumerable<ApiUser>> GetUsersAsync();
        public Task AddUserAsync(ApiUser User);
        public Task<ApiUser> GetUserAsync(string id);
        public Task EditUserAsync(ApiUser user);
        public Task DeleteUserAsync(string id);

        public bool UserExists(string userID);
        public Task<ApiUser> LoginUserAsync(string userName, string password);


        //UserWorkout
        public Task<IEnumerable<UserWorkout>> GetUserWorkoutsListAsync();
        public Task AddUserWorkoutAsync(Workout workout, string userID);
        public Task<IEnumerable<Workout>> GetUserWorkoutsAsync(string userID);
        public Task DeleteUserWorkoutAsync(string workoutID, string userID);
        public Task DeleteUserWorkoutListAsync(string userID);

        public bool UserWorkoutExists(string userWorkoutID);

        //Exercises
        public Task<IEnumerable<Exercise>> GetExerciseListAsync();
        public Task AddExerciseAsync(Exercise exercise);
        public Task<Exercise> GetExerciseAsync(string exerciseID);
        public Task EditExerciseAsync(Exercise exercise);
        public Task DeleteExerciseAsync(string exerciseID);
        public bool ExerciseExists(string exerciseID);

        //WorkoutExercises
        public Task<IEnumerable<WorkoutExercises>> GetWorkoutExerciseListAsync();
        public Task AddWorkoutExerciseAsync(string workoutid, Exercise exercise);
        public Task<IEnumerable<Exercise>> GetWorkoutExercisesAsync(string workoutID);
        public Task DeleteWorkoutExerciseAsync(string workoutID, string ExerciseID);
        public Task DeleteWorkoutExerciseListAsync(string ExerciseID);
        public bool WorkoutExerciseExists(string Id);

    }

}
