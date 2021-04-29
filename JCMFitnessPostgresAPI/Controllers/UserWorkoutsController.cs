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
            //Logdebug for every call to this
            return await _dataRepository.GetUserWorkoutsListAsync();
        }

        [HttpGet]
        public async Task<IEnumerable<Workout>> GetUserWorkouts(string userid)
        {
            //Loginfo returning workouts for the userid {userid}
            var userWorkout = await _dataRepository.GetUserWorkoutsAsync(userid);

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
                //loginfo for successful addition of a workout of workoutid to userid
                return Ok();
            }
            //Logwarning "something went wrong"
            return BadRequest();
        }


        
        [HttpDelete]
        public async Task<IActionResult> DeleteUserWorkouts(string workoutid, string userid)
        {
            await _dataRepository.DeleteUserWorkoutAsync(workoutid, userid);
            //loginfo for successful deletion of workout of id: workoutid association from userid
            return Ok();
        }

  
        [HttpDelete("deleteall")]
        public async Task<IActionResult> DeleteUserWorkoutList(string userid)
        {
            await _dataRepository.DeleteUserWorkoutListAsync(userid);
            //Loginfo "successfully deleted all workouts associated with userid"
            return Ok();
        }

    }
}
