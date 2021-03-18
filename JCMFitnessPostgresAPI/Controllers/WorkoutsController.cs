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
    public class WorkoutsController : ControllerBase
    {
        private readonly IDataRepository _dataRepository;

        public WorkoutsController(IDataRepository userRepository)
        {
            _dataRepository = userRepository;
        }

        [HttpGet]
        public async Task<IEnumerable<Workout>> GetAllWorkouts()
        {
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
                    return Ok();
                }
                else
                {
                    return BadRequest("Workout already exists");
                }
            }
            return BadRequest("User Object is not valid");
        }

        [HttpPut]
        public  async Task<IActionResult> EditWorkout([FromBody] Workout workout)
        {
            if (ModelState.IsValid)
            {
                await _dataRepository.EditWorkoutAsync(workout);
                return Ok();
            }
            return BadRequest();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Workout>> GetWorkoutByID(string workoutID)
        {

            if (_dataRepository.WorkoutExists(workoutID))
            {
                return await _dataRepository.GetWorkoutAsync(workoutID);
            }
            else
            {
                return BadRequest("Workout id does not exist");
            }
        }


        // DELETE: api/Workouts/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteWorkout(string workoutID)
        {
            if (_dataRepository.WorkoutExists(workoutID))
            {
                 await _dataRepository.DeleteWorkoutAsync(workoutID);
                return Ok();
            }
            else
            {
                return BadRequest("Workout id does not exist");
            }
            
        }

    }
}
