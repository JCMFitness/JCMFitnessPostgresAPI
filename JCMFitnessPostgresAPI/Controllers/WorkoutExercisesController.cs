using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using JCMFitnessPostgresAPI.DataAccess;
using JCMFitnessPostgresAPI.Models;
using Microsoft.AspNetCore.Authorization;
using JCMFitnessPostgresAPI.Authentication;

namespace JCMFitnessPostgresAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize]
    public class WorkoutExercisesController : ControllerBase
    {
        private readonly IDataRepository _dataRepository;

        public WorkoutExercisesController(IDataRepository workoutRepository)
        {
            _dataRepository = workoutRepository;
        }


        [HttpGet("getall")]
        //[Authorize(Roles = UserRoles.Admin)]
        public async Task<IEnumerable<WorkoutExercises>> GetAllWorkoutExercises()
        {
            return await _dataRepository.GetWorkoutExerciseListAsync();
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
            
            //Guid obj = Guid.NewGuid();
            //user.UserID = obj.ToString("n");
            await _dataRepository.AddWorkoutExerciseAsync(workoutid, exercise);
            return Ok();
            
            //return BadRequest();
        }



        [HttpDelete]
        public async Task<IActionResult> DeleteWorkoutExercises(string exerciseid, string workoutid)
        {
            await _dataRepository.DeleteWorkoutExerciseAsync(workoutid, exerciseid);


            return Ok();
        }


        [HttpDelete("deleteall")]
        public async Task<IActionResult> DeleteWorkoutExercisesList(string workoutid)
        {
            await _dataRepository.DeleteWorkoutExerciseListAsync(workoutid);


            return Ok();
        }

    }
}
