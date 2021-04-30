using JCMFitnessPostgresAPI.Authentication;
using JCMFitnessPostgresAPI.Models;
using JCMFitnessPostgresAPI.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace JCMFitnessPostgresAPI.DataAccess
{
    public class DataRepository : IDataRepository
    {
        private readonly ApiDBContext _context;

        //private DateTimeWithZone dateTimeWithZone = new DateTimeWithZone();

        public DataRepository(ApiDBContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        //Workout*******************************
        public bool WorkoutExists(string workoutID)
        {
            return _context.Workouts.Any(e => e.WorkoutID == workoutID);
        }

        public async Task<IEnumerable<Workout>> GetWorkoutListAsync()
        {
            return await EntityFrameworkQueryableExtensions.ToListAsync(_context.Workouts);

        }

        public async Task AddWorkoutAsync(Workout workout)
        {
            if (!WorkoutExists(workout.WorkoutID))
            {
                workout.LastUpdated = DateTimeWithZone.LocalTime(DateTime.Now);
                workout.IsDeleted = false;

                _context.Workouts.Add(workout);
                await _context.SaveChangesAsync();
            }
            else
            {
                throw new InvalidOperationException("Workout already exists!");
            }

        }


        public async Task EditWorkoutAsync(Workout workout)
        {
            workout.LastUpdated = DateTimeWithZone.LocalTime(DateTime.Now);

            _context.Update(workout);
            await _context.SaveChangesAsync();
        }

        public async Task<Workout> GetWorkoutAsync(string workoutID)
        {
            /*return await _context.Workouts.AsNoTracking()
                .FirstOrDefaultAsync(r => r.WorkoutID == workoutID);*/

            return await _context.Workouts.Include(r => r.WorkoutExercises).AsNoTracking()
             .FirstOrDefaultAsync(r => r.WorkoutID == workoutID);
        }

        public async Task DeleteWorkoutAsync(string workoutID)
        {
            var workout = await _context.Workouts.FindAsync(workoutID);

            _context.Workouts.Remove(workout);
            await _context.SaveChangesAsync();
        }

        


        //User *******************************
        public bool UserExists(string userID)
        {
            return _context.Users.Any(e => e.Id == userID);
        }

        public async Task<ApiUser> GetUserAsync(string userID)
        {
            /* return await _context.Users.Include(r => r.UserWorkouts)
                .Include(p => p.UserWorkouts)
                .ThenInclude(pc => pc.)
                .FirstOrDefaultAsync(r => r.ID == postID);*/

                return await Task.Run(() => _context.Users.AsNoTracking().First(r => r.Id == userID));
        }



        public async Task<IEnumerable<ApiUser>> GetUsersAsync()
        {
            return await _context.Users.ToListAsync();
        }

        public async Task EditUserAsync(ApiUser user)
        {
            user.LastUpdated = DateTimeWithZone.LocalTime(DateTime.Now);
            _context.Update(user);
            await _context.SaveChangesAsync();

        }


        public async Task DeleteUserAsync(string userID)
        {
            var user = await _context.Users.FindAsync(userID);

            await DeleteUserWorkoutListAsync(user.Id);
            _context.Users.Remove(user);
            await _context.SaveChangesAsync();
        }

        public async Task SyncUserAsync(ApiUser user)
        {
            var dbUser = await GetUserAsync(user.Id);
          
            if (user.LastUpdated > dbUser.LastUpdated)
            {
                await EditUserAsync(user);
            }
        }

        //UserWorkout*******************************

        public bool UserWorkoutExists(string userWorkoutID)
        {
            return _context.UserWorkouts.Any(e => e.Id == userWorkoutID);
        }
        public async Task<IEnumerable<UserWorkout>> GetUserWorkoutsListAsync()
        {
            return await _context.UserWorkouts.ToListAsync();
        }

        public async Task AddUserWorkoutAsync(Workout workout, string userID)
        {

            var user = await EntityFrameworkQueryableExtensions.FirstOrDefaultAsync(_context.Users, c => c.Id == userID);


            var ExistingWorkout = await EntityFrameworkQueryableExtensions.FirstOrDefaultAsync(_context.Workouts, c => c.WorkoutID == workout.WorkoutID);

            if(ExistingWorkout == null)
            {
                workout.LastUpdated = DateTimeWithZone.LocalTime(DateTime.Now);
                workout.IsDeleted = false;

                _context.Workouts.Add(workout);
                await _context.SaveChangesAsync();

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
            else
            {
                var newUserWorkout = new UserWorkout()
                {
                    Id = Guid.NewGuid().ToString("n"),
                    WorkoutID = ExistingWorkout.WorkoutID,
                    UserID = userID,
                    Workout = ExistingWorkout,
                    User = user,
                    IsPublic = false,
                };

                _context.UserWorkouts.Add(newUserWorkout);
                await _context.SaveChangesAsync();
            }

        }


        public async Task<IEnumerable<Workout>> GetUserWorkoutsAsync(string userID)
        {
            return await Task.Run(() => _context.UserWorkouts
             .Include(e => e.Workout)
             .Where(m => m.UserID == userID)
             .Select(d => d.Workout));


        }

        public async Task DeleteUserWorkoutAsync(string workoutID, string userID)
        {

            var ExistingWorkout = await EntityFrameworkQueryableExtensions.FirstOrDefaultAsync(_context.Workouts, c => c.WorkoutID == workoutID);

            var userWorkout = _context.UserWorkouts.Where(m => m.UserID == userID).Where(m => m.WorkoutID == workoutID).ToList();

           

            if (ExistingWorkout != null && ExistingWorkout.IsPublic == false)
            {
                await DeleteWorkoutExerciseListAsync(ExistingWorkout.WorkoutID);

                _context.UserWorkouts.Remove(userWorkout.FirstOrDefault(m => m.WorkoutID == workoutID));
                await DeleteWorkoutAsync(ExistingWorkout.WorkoutID);
            }

            

            await _context.SaveChangesAsync();
        }

        public async Task DeleteUserWorkoutListAsync(string userID)
        {

            var userWorkout = _context.UserWorkouts.Where(m => m.UserID == userID).ToList();

            foreach(var i in userWorkout)
            {
                _context.UserWorkouts.Remove(i);

              
                await DeleteWorkoutExerciseListAsync(i.WorkoutID);


                await DeleteWorkoutAsync(i.WorkoutID);
            }

            await _context.SaveChangesAsync();
        }

        public async Task SyncWorkoutsAsync(string userID, List<Workout> workouts)
        {
            // Pull sync is just getting all records that have changed since that date.
            foreach (var w in workouts)
                if (!WorkoutExists(w.WorkoutID)) // Does not exist, hence insert 
                    await AddUserWorkoutAsync(w, userID);
                else if (w.IsDeleted)
                    await DeleteUserWorkoutAsync(w.WorkoutID, userID);
                else
                {
                    var w1 = await GetWorkoutAsync(w.WorkoutID);

                    if (w.LastUpdated > w1.LastUpdated)
                    {
                        await EditWorkoutAsync(w);
                    }
                }

        }

        /*WorkOutExercises**********************************************************************************************************************************************/

        public async Task<IEnumerable<WorkoutExercises>> GetWorkoutExerciseListAsync()
        {
            return await _context.WorkoutExercises.ToListAsync();
        }

        public async Task AddWorkoutExerciseAsync(string workoutid, Exercise exercise)
        {
            var Workout = await EntityFrameworkQueryableExtensions.FirstOrDefaultAsync(_context.Workouts, c => c.WorkoutID == workoutid);

            var ExistingExercise = await EntityFrameworkQueryableExtensions.FirstOrDefaultAsync(_context.Exercises, c => c.ExerciseID == exercise.ExerciseID);

            if (ExistingExercise == null)
            {
                exercise.LastUpdated = DateTimeWithZone.LocalTime(DateTime.Now);
                exercise.IsDeleted = false;
                _context.Exercises.Add(exercise);
                await _context.SaveChangesAsync();

                var newWorkoutExercise = new WorkoutExercises()
                {
                    Id = Guid.NewGuid().ToString("n"),
                    ExerciseID = exercise.ExerciseID,
                    WorkoutID = workoutid,
                    Exercise = exercise,
                    Workout = Workout,
                    IsPublic = false,
                };

                _context.WorkoutExercises.Add(newWorkoutExercise);
                await _context.SaveChangesAsync();

            }
            else
            {
                var newWorkoutExercise = new WorkoutExercises()
                {
                    Id = Guid.NewGuid().ToString("n"),
                    ExerciseID = ExistingExercise.ExerciseID,
                    WorkoutID = workoutid,
                    Exercise = ExistingExercise,
                    Workout = Workout,
                    IsPublic = false,
                };

                _context.WorkoutExercises.Add(newWorkoutExercise);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<Exercise>> GetWorkoutExercisesAsync(string workoutID)
        {
            return await Task.Run(() => _context.WorkoutExercises
                       .Where(m => m.WorkoutID == workoutID)
                       .Select(m => m.Exercise)
                       .ToList());
        }


        public async Task DeleteWorkoutExerciseAsync(string workoutID, string exerciseID)
        {

            var ExistingExercise = await EntityFrameworkQueryableExtensions.FirstOrDefaultAsync(_context.Exercises, c => c.ExerciseID == exerciseID);

            var workoutExercises = _context.WorkoutExercises.Where(m => m.ExerciseID == exerciseID).Where(m => m.WorkoutID == workoutID).ToList();

            

            if (ExistingExercise != null && ExistingExercise.IsPublic == false)
            {
               
                _context.WorkoutExercises.Remove(workoutExercises.FirstOrDefault(m => m.WorkoutID == workoutID));
                await DeleteExerciseAsync(ExistingExercise.ExerciseID);
            }


            await _context.SaveChangesAsync();
        }


 
        public async Task DeleteWorkoutExerciseListAsync(string workoutID)
        {
            var workoutExercises = _context.WorkoutExercises.Where(m => m.WorkoutID == workoutID).ToList();

            foreach (var i in workoutExercises)
            {
                await DeleteWorkoutExerciseAsync(i.WorkoutID, i.ExerciseID);
                //_context.WorkoutExercises.Remove(i);
            }

            await _context.SaveChangesAsync();
        }


        public bool WorkoutExerciseExists(string Id)
        {
            return _context.WorkoutExercises.Any(e => e.Id == Id);
        }



        public async Task SyncExercisesAsync(string workoutID, List<Exercise> exercises)
        {
            // Pull sync is just getting all records that have changed since that date.
            foreach (var w in exercises)
                if (!ExerciseExists(w.ExerciseID)) // Does not exist, hence insert 
                    await AddWorkoutExerciseAsync(workoutID, w);
                else if (w.IsDeleted)
                    await DeleteWorkoutExerciseAsync(workoutID, w.ExerciseID);
                else
                {
                    var w1 = await GetExerciseAsync(w.ExerciseID);

                    if (w.LastUpdated > w1.LastUpdated)
                    {
                        await EditExerciseAsync(w);
                    }
                }

        }

        /*Exercises*****************************************************************************************************************************/
        public async Task<IEnumerable<Exercise>> GetExerciseListAsync()
        {
            return await EntityFrameworkQueryableExtensions.ToListAsync(_context.Exercises);
        }
        public async Task AddExerciseAsync(Exercise exercise)
        {
            if (!ExerciseExists(exercise.ExerciseID))
            {
                exercise.LastUpdated = DateTimeWithZone.LocalTime(DateTime.Now);
                exercise.IsDeleted = false;
                _context.Exercises.Add(exercise);
                await _context.SaveChangesAsync();
            }
            else
            {
                throw new InvalidOperationException("Exercise already exists!");
            }
        }
        public async Task<Exercise> GetExerciseAsync(string exerciseid)
        {
            return await _context.Exercises.AsNoTracking()
                .FirstOrDefaultAsync(r => r.ExerciseID == exerciseid);
        }
        public async Task EditExerciseAsync(Exercise exercise)
        {
            exercise.LastUpdated = DateTimeWithZone.LocalTime(DateTime.Now);
            _context.Update(exercise);
            await _context.SaveChangesAsync();
        }
        public async Task DeleteExerciseAsync(string exerciseid)
        {
            var exercise = await _context.Exercises.FindAsync(exerciseid);
            _context.Exercises.Remove(exercise);
            await _context.SaveChangesAsync();
        }
        public bool ExerciseExists(string exerciseid)
        {
            return _context.Exercises.Any(e => e.ExerciseID == exerciseid);
        }

       

    }

}
