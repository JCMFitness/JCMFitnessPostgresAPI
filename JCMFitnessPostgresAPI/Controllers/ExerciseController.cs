using JCMFitnessPostgresAPI.DataAccess;
using JCMFitnessPostgresAPI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JCMFitnessPostgresAPI.Controllers
{
    [Microsoft.AspNetCore.Mvc.Route("api/[controller]")]
    [ApiController]
    //[Authorize]
    public class ExerciseController : ControllerBase
    {
        private readonly IDataRepository _dataRepository;

        public ExerciseController(IDataRepository userRepository)
        {
            _dataRepository = userRepository;
        }

        [HttpGet("getall")]
        public async Task<IEnumerable<Exercise>> GetAllExercises()
        {
            return await _dataRepository.GetExerciseListAsync();
        }

        [HttpPost]
        public async Task<IActionResult> AddExercise([FromBody] Exercise exercise)
        {

            if (ModelState.IsValid)
            {
                //Guid obj = Guid.NewGuid();
                //user.UserID = obj.ToString("n");
                if (!_dataRepository.ExerciseExists(exercise.ExerciseID))
                {
                    await _dataRepository.AddExerciseAsync(exercise);
                    return Ok();
                }
                else
                {
                    return BadRequest("Exercise already exists");
                }
            }
            return BadRequest("Workout Object is not valid");
        }

        [HttpPut]
        public async Task<IActionResult> EditExercise([FromBody] Exercise exercise)
        {
            if (ModelState.IsValid)
            {
                await _dataRepository.EditExerciseAsync(exercise);
                return Ok();
            }
            return BadRequest();
        }

        [HttpGet]
        public async Task<ActionResult<Exercise>> GetExerciseByID(string exerciseid)
        {

            if (_dataRepository.ExerciseExists(exerciseid))
            {
                return await _dataRepository.GetExerciseAsync(exerciseid);
            }
            else
            {
                return BadRequest("Exercise id does not exist");
            }
        }



        [HttpDelete]
        public async Task<IActionResult> DeleteExercise(string exerciseid)
        {
            if (_dataRepository.ExerciseExists(exerciseid))
            {
                await _dataRepository.DeleteExerciseAsync(exerciseid);
                return Ok();
            }
            else
            {
                return BadRequest("Exercise id does not exist");
            }

        }

        [HttpPost("sync")]
        public async Task SyncAllExercises(string workoutID, List<Exercise> exercises)
        {
            await _dataRepository.SyncExercisesAsync(workoutID, exercises);
        }

    }
}
