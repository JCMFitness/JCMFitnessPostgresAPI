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
    public class UserWorkoutsController : ControllerBase
    {
        private readonly IDataRepository _dataRepository;
        private readonly ILogger<UserWorkoutsController> _logger;
        public UserWorkoutsController(IDataRepository userRepository, ILogger<UserWorkoutsController> logger)
        {
            _dataRepository = userRepository;
            _logger = logger;
        }


        [HttpGet("getall")]
        [Authorize(Roles = UserRoles.Admin)]
        public async Task<IEnumerable<UserWorkout>> GetAllUserWorkouts()
        {
            _logger.LogDebug("{Prefix}: Attempted to get all User Workouts", Prefixes.USERWORK);
            return await _dataRepository.GetUserWorkoutsListAsync();
        }

        [HttpGet]
        public async Task<IEnumerable<Workout>> GetUserWorkouts(string userid)
        {
            
            var userWorkout = await _dataRepository.GetUserWorkoutsAsync(userid);
            _logger.LogDebug("{Prefix}: Attempted to get Workouts for User with Id: {Id}", Prefixes.USERWORK, userid);
            return userWorkout;
        }


        [HttpPost]
        public async Task<IActionResult> AddUserWorkout([FromBody] Workout workout, string userid)
        {
            if (ModelState.IsValid)
            {
                //Guid obj = Guid.NewGuid();
                //user.UserID = obj.ToString("n");
                await _dataRepository.AddUserWorkoutAsync(workout, userid);
                _logger.LogInformation("{Prefix}: Added association between Workout with Id: {Id} and User with Id: {Id}", Prefixes.USERWORK,workout.WorkoutID,userid);
                return Ok();
            }
            _logger.LogError("{Prefix}: Invalid Workout model submitted: {workout}",Prefixes.USERWORK, workout);
            return BadRequest();
        }


        
        [HttpDelete]
        public async Task<IActionResult> DeleteUserWorkouts(string workoutid, string userid)
        {
            await _dataRepository.DeleteUserWorkoutAsync(workoutid, userid);
            _logger.LogInformation("{Prefix}: Deleted association between Workout with Id: {Id}, and User with Id: {Id} ",Prefixes.USERWORK, workoutid,userid);
            return Ok();
        }

  
        [HttpDelete("deleteall")]
        public async Task<IActionResult> DeleteUserWorkoutList(string userid)
        {
            await _dataRepository.DeleteUserWorkoutListAsync(userid);
            _logger.LogInformation("{Prefix}: Deleted all associations between Workouts and User with Id: {Id}",Prefixes.USERWORK,userid);
            return Ok();
        }

    }
}
