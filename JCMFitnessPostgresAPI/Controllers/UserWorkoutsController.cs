using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using JCMFitnessPostgresAPI.DataAccess;
using JCMFitnessPostgresAPI.Models;

namespace JCMFitnessPostgresAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserWorkoutsController : ControllerBase
    {
        private readonly IDataRepository _dataRepository;

        public UserWorkoutsController(IDataRepository userRepository)
        {
            _dataRepository = userRepository;
        }


        [HttpGet("getall")]
        public async Task<IEnumerable<UserWorkout>> GetAllUserWorkouts()
        {
            return await _dataRepository.GetUserWorkoutsListAsync();
        }

        [HttpGet]
        public async Task<IEnumerable<Workout>> GetUserWorkouts(string userID)
        {
            var userWorkout = await _dataRepository.GetUserWorkoutsAsync(userID);

            return userWorkout;
        }


        [HttpPost]
        public async Task<IActionResult> AddUserWorkout([FromBody] Workout workout, string userID)
        {
            if (ModelState.IsValid)
            {
                //Guid obj = Guid.NewGuid();
                //user.UserID = obj.ToString("n");
                await _dataRepository.AddUserWorkoutAsync(workout, userID);
                return Ok();
            }
            return BadRequest();
        }


        
        [HttpDelete]
        public async Task<IActionResult> DeleteUserWorkouts(string workoutID, string userID)
        {
            await _dataRepository.DeleteUserWorkoutAsync(workoutID, userID);


            return Ok();
        }

  
        [HttpDelete("deleteall")]
        public async Task<IActionResult> DeleteUserWorkoutList(string userID)
        {
            await _dataRepository.DeleteUserWorkoutListAsync(userID);


            return Ok();
        }

    }
}
