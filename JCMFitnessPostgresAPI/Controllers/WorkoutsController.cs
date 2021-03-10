﻿using System;
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
        public async Task<IActionResult> AddUser([FromBody] Workout workout)
        {
            if (ModelState.IsValid)
            {
                //Guid obj = Guid.NewGuid();
                //user.UserID = obj.ToString("n");
                await _dataRepository.AddWorkoutAsync(workout);
                return Ok();
            }
            return BadRequest();
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
        public async Task<Workout> GetWorkoutByID(string workoutID)
        {
            return await _dataRepository.GetWorkoutAsync(workoutID);
        }

        /* // GET: api/Workouts
         [HttpGet]
         public async Task<ActionResult<IEnumerable<Workout>>> GetWorkouts()
         {
             return await _context.Workouts.ToListAsync();
         }

         // GET: api/Workouts/5
         [HttpGet("{id}")]
         public async Task<ActionResult<Workout>> GetWorkout(long id)
         {
             var workout = await _context.Workouts.FindAsync(id);

             if (workout == null)
             {
                 return NotFound();
             }

             return workout;
         }

         // PUT: api/Workouts/5
         // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
         [HttpPut("{id}")]
         public async Task<IActionResult> PutWorkout(long id, Workout workout)
         {
             if (id != workout.WorkoutID)
             {
                 return BadRequest();
             }

             _context.Entry(workout).State = EntityState.Modified;

             try
             {
                 await _context.SaveChangesAsync();
             }
             catch (DbUpdateConcurrencyException)
             {
                 if (!WorkoutExists(id))
                 {
                     return NotFound();
                 }
                 else
                 {
                     throw;
                 }
             }

             return NoContent();
         }

         // POST: api/Workouts
         // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
         [HttpPost]
         public async Task<ActionResult<Workout>> PostWorkout(Workout workout)
         {
             _context.Workouts.Add(workout);
             await _context.SaveChangesAsync();

             return CreatedAtAction("GetWorkout", new { id = workout.WorkoutID }, workout);
         }

         // DELETE: api/Workouts/5
         [HttpDelete("{id}")]
         public async Task<IActionResult> DeleteWorkout(long id)
         {
             var workout = await _context.Workouts.FindAsync(id);
             if (workout == null)
             {
                 return NotFound();
             }

             _context.Workouts.Remove(workout);
             await _context.SaveChangesAsync();

             return NoContent();
         }

         private bool WorkoutExists(long id)
         {
             return _context.Workouts.Any(e => e.WorkoutID == id);
         }*/
    }
}
