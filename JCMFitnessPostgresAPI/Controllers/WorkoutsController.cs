using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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
    public class WorkoutsController : ControllerBase
    {
        private readonly IDataRepository _dataRepository;
        private readonly ILogger<WorkoutsController> _logger;

        public WorkoutsController(IDataRepository userRepository, ILogger<WorkoutsController> logger)
        {
            _dataRepository = userRepository;
            _logger = logger;
        }

        [HttpGet("getall")]
        [Authorize(Roles = UserRoles.Admin)]
        public async Task<IEnumerable<Workout>> GetAllWorkouts()
        {
            _logger.LogDebug("{Prefix}: Attempted to get all workouts", Prefixes.WORKOUT);
            return await _dataRepository.GetWorkoutListAsync();
        }

        [HttpPost]
        public async Task<IActionResult> AddWorkout([FromBody] Workout workout)
        {

            if (ModelState.IsValid)
            {
                //Guid obj = Guid.NewGuid();
                //user.UserID = obj.ToString("n");
                if (!_dataRepository.WorkoutExists(workout.WorkoutID))
                {
                    await _dataRepository.AddWorkoutAsync(workout);
                    _logger.LogInformation("{Prefix}: Added Workout with Id: {Id} to the Data Repo", Prefixes.WORKOUT, workout.WorkoutID);
                    return Ok();
                }
                else
                {
                    _logger.LogWarning("{Prefix}: Attempted to add Workout with existing Id: {Id}", Prefixes.WORKOUT, workout.WorkoutID);
                    return BadRequest("Workout already exists");
                }
            }
            _logger.LogError("{Prefix}: Invalid Workout model submitted: {workout}", Prefixes.WORKOUT, workout);
            return BadRequest("Workout Object is not valid");
        }

        [HttpPut]
        public  async Task<IActionResult> EditWorkout([FromBody] Workout workout)
        {
            if (ModelState.IsValid)
            {
                await _dataRepository.EditWorkoutAsync(workout);
                _logger.LogInformation("{Prefix}: Edited Workout with Id: {Id}", Prefixes.WORKOUT, workout.WorkoutID);
                return Ok();
            }
            _logger.LogError("{Prefix}: Invalid edit submitted for: {Id}", Prefixes.WORKOUT, workout.WorkoutID);
            return BadRequest();
        }

        [HttpGet]
        public async Task<ActionResult<Workout>> GetWorkoutByID(string workoutid)
        {

            if (_dataRepository.WorkoutExists(workoutid))
            {
                _logger.LogDebug("{Prefix}: Attempted to get an workout with Id: {Id}", Prefixes.WORKOUT, workoutid);
                return await _dataRepository.GetWorkoutAsync(workoutid);
            }
            else
            {
                _logger.LogWarning("{Prefix}: Attempted to get an Id: {Id} that does not exist", Prefixes.WORKOUT, workoutid);
                return BadRequest("Workout id does not exist");
            }
        }



        [HttpDelete]
        public async Task<IActionResult> DeleteWorkout(string workoutid)
        {
            if (_dataRepository.WorkoutExists(workoutid))
            {
                 await _dataRepository.DeleteWorkoutAsync(workoutid);
                _logger.LogInformation("{Prefix}: Deleted Workout with Id: {Id}", Prefixes.WORKOUT, workoutid);
                return Ok();
            }
            else
            {
                _logger.LogWarning("{Prefix}: Unable to delete Workout with Id: {Id}, Id does not exist", Prefixes.WORKOUT, workoutid);
                return BadRequest("Workout id does not exist");
            }
            
        }

    }
}
