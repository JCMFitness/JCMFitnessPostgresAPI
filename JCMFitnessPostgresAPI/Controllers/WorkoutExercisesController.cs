using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using JCMFitnessPostgresAPI.DataAccess;
using JCMFitnessPostgresAPI.Models;

namespace JCMFitnessPostgresAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WorkoutExercisesController : ControllerBase
    {
        private readonly IDataRepository _dataRepository;

        public WorkoutExercisesController(IDataRepository workoutRepository)
        {
            _dataRepository = workoutRepository;
        }


        [HttpGet("getall")]
        public async Task<IEnumerable<WorkoutExercises>> GetAllWorkoutExercises()
        {
            return await _dataRepository.GetWorkoutExercisesListAsync();
        }

        [HttpGet]
        public async Task<IEnumerable<Exercise>> GetWorkoutExercises(string workoutid)
        {
            var workoutExercises = await _dataRepository.GetWorkoutExercisesAsync(workoutid);

            return workoutExercises;
        }


        [HttpPost]
        public async Task<IActionResult> AddWorkoutExercises([FromBody] Exercise exercise, string workoutid)
        {
            if (ModelState.IsValid)
            {
                //Guid obj = Guid.NewGuid();
                //user.UserID = obj.ToString("n");
                await _dataRepository.AddWorkoutExercisesAsync(workoutid, exercise);
                return Ok();
            }
            return BadRequest();
        }



        [HttpDelete]
        public async Task<IActionResult> DeleteWorkoutExercises(string exerciseid, string workoutid)
        {
            await _dataRepository.DeleteWorkoutExercisesAsync(workoutid, exerciseid);


            return Ok();
        }


        [HttpDelete("deleteall")]
        public async Task<IActionResult> DeleteWorkoutExercisesList(string exerciseid)
        {
            await _dataRepository.DeleteWorkoutExercisesListAsync(exerciseid);


            return Ok();
        }

    }
}
