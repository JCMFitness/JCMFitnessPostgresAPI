using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using JCMFitnessPostgresAPI.DataAccess;
using JCMFitnessPostgresAPI.Models;
using Microsoft.AspNetCore.Authorization;
using JCMFitnessPostgresAPI.Authentication;
using Microsoft.Extensions.Logging;

namespace JCMFitnessPostgresAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize]
    public class WorkoutExercisesController : ControllerBase
    {
        private readonly IDataRepository _dataRepository;
        ILogger<WorkoutExercisesController> _logger;

        public WorkoutExercisesController(IDataRepository workoutRepository, ILogger<WorkoutExercisesController> logger)
        {
            _dataRepository = workoutRepository;
            _logger = logger;
        }


        [HttpGet("getall")]
        [Authorize(Roles = UserRoles.Admin)]
        public async Task<IEnumerable<WorkoutExercises>> GetAllWorkoutExercises()
        {
            _logger.LogDebug("{Prefix}: Attempted to get all Workout Exercises", Prefixes.WORKEXER);
            return await _dataRepository.GetWorkoutExerciseListAsync();
        }

        [HttpGet]
        public async Task<IEnumerable<Exercise>> GetWorkoutExercises(string workoutid)
        {
            
            var workoutExercises = await _dataRepository.GetWorkoutExercisesAsync(workoutid);
            _logger.LogDebug("{Prefix}: Attempted to get Exercises for Workout with Id: {Id}", Prefixes.WORKEXER, workoutid);
            return workoutExercises;
        }


        [HttpPost]
        public async Task<IActionResult> AddWorkoutExercises([FromBody] Exercise exercise, string workoutid)
        {
            if (ModelState.IsValid)
            {
                //Guid obj = Guid.NewGuid();
                //user.UserID = obj.ToString("n");
                await _dataRepository.AddWorkoutExerciseAsync(workoutid, exercise);
                _logger.LogInformation("{Prefix}: Added association between Exercise with Id: {Id} and Workout with Id: {Id}", Prefixes.USERWORK, exercise.ExerciseID, workoutid);
                return Ok();
            }
            _logger.LogError("{Prefix}: Invalid Exercise model submitted: {exercise}", Prefixes.WORKEXER, exercise);
            return BadRequest();
        }



        [HttpDelete]
        public async Task<IActionResult> DeleteWorkoutExercises(string exerciseid, string workoutid)
        {
            await _dataRepository.DeleteWorkoutExerciseAsync(workoutid, exerciseid);
            _logger.LogInformation("{Prefix}: Deleted association between Exercise with Id: {Id}, and Workout with Id: {Id} ", Prefixes.USERWORK, exerciseid, workoutid);
            return Ok();
        }


        [HttpDelete("deleteall")]
        public async Task<IActionResult> DeleteWorkoutExercisesList(string workoutid)
        {
            await _dataRepository.DeleteWorkoutExerciseListAsync(workoutid);
            _logger.LogInformation("{Prefix}: Deleted all associations between Exercises and Workout with Id: {Id}", Prefixes.USERWORK, workoutid);
            return Ok();
        }

    }
}
