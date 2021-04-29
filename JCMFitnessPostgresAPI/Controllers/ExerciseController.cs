using JCMFitnessPostgresAPI.DataAccess;
using JCMFitnessPostgresAPI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
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
        private readonly ILogger<ExerciseController> _logger;
        public ExerciseController(IDataRepository userRepository, ILogger<ExerciseController> logger)
        {
            _dataRepository = userRepository;
            _logger = logger;
        }

        [HttpGet("getall")]
        public async Task<IEnumerable<Exercise>> GetAllExercises()
        {
            //logdebug for every call
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
                    //Add exercise if datarepo doesnt contain exercise
                    await _dataRepository.AddExerciseAsync(exercise);
                    return Ok();
                }
                else
                {
                    //logwarning because exercise exists
                    return BadRequest("Exercise already exists");
                }
            }
            //Logerror because exercise object is invalid
            return BadRequest("Workout Object is not valid");
        }

        [HttpPut]
        public async Task<IActionResult> EditExercise([FromBody] Exercise exercise)
        {
            if (ModelState.IsValid)
            {
                //loginfo exercise with id {id} edited
                await _dataRepository.EditExerciseAsync(exercise);
                return Ok();
            }
            //logerror edit failed for exercise
            return BadRequest();
        }

        [HttpGet]
        public async Task<ActionResult<Exercise>> GetExerciseByID(string exerciseid)
        {

            if (_dataRepository.ExerciseExists(exerciseid))
            {
                //Loginfo exercise returned from existing 
                return await _dataRepository.GetExerciseAsync(exerciseid);
            }
            else
            {
                //logwarning attempted to return an exercise that doesnt exist
                return BadRequest("Exercise id does not exist");
            }
        }



        [HttpDelete]
        public async Task<IActionResult> DeleteExercise(string exerciseid)
        {
            if (_dataRepository.ExerciseExists(exerciseid))
            {   //loginfo deleted an exercise 
                await _dataRepository.DeleteExerciseAsync(exerciseid);
                return Ok();
            }
            else
            {
                //logwarning attempted to delete an exercise that doesnt exist
                return BadRequest("Exercise id does not exist");
            }

        }

    }
}
