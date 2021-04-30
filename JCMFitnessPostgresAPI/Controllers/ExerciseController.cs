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
            _logger.LogDebug("{Prefix}: Attempted to get all exercises",Prefixes.EXER);
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
                    _logger.LogInformation("{Prefix}: Added Exercise with Id: {Id} to the Data Repo", Prefixes.EXER, exercise.ExerciseID);
                    return Ok();
                }
                else
                {
                    _logger.LogWarning("{Prefix}: Attempted to add with existing Id: {Id}", Prefixes.EXER,exercise.ExerciseID);
                    return BadRequest("Exercise already exists");
                }
            }
            _logger.LogError("{Prefix}: Invalid exercise model submitted: {exercise}", Prefixes.EXER, exercise);
            return BadRequest("Workout Object is not valid");
        }

        [HttpPut]
        public async Task<IActionResult> EditExercise([FromBody] Exercise exercise)
        {
            if (ModelState.IsValid)
            {
                
                await _dataRepository.EditExerciseAsync(exercise);
                _logger.LogInformation("{Prefix}: Edited Exercise with Id: {Id}", Prefixes.EXER, exercise.ExerciseID);
                return Ok();
            }
            _logger.LogError("{Prefix}: Invalid edit submitted for: {Id}", Prefixes.EXER, exercise.ExerciseID);
            return BadRequest();
        }

        [HttpGet]
        public async Task<ActionResult<Exercise>> GetExerciseByID(string exerciseid)
        {

            if (_dataRepository.ExerciseExists(exerciseid))
            {
                _logger.LogDebug("{Prefix}: Attempted to get an exercise with Id: {Id}",Prefixes.EXER, exerciseid);
                return await _dataRepository.GetExerciseAsync(exerciseid);
            }
            else
            {
                _logger.LogWarning("{Prefix}: Attempted to get an Id: {Id} that does not exist",Prefixes.EXER,exerciseid);
                return BadRequest("Exercise id does not exist");
            }
        }



        [HttpDelete]
        public async Task<IActionResult> DeleteExercise(string exerciseid)
        {
            if (_dataRepository.ExerciseExists(exerciseid))
            {  
                await _dataRepository.DeleteExerciseAsync(exerciseid);
                _logger.LogInformation("{Prefix}: Deleted Exercise with Id: {Id}", Prefixes.EXER, exerciseid);
                return Ok();
            }
            else
            {
                _logger.LogWarning("{Prefix}: Unable to delete exercise with Id: {Id}, Id does not exist", Prefixes.EXER, exerciseid);
                return BadRequest("Exercise id does not exist");
            }

        }

    }
}
