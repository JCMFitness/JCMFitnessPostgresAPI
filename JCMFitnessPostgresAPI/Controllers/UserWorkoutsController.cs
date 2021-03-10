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


        [HttpGet]
        public async Task<IEnumerable<Workout>> GetUserWorkouts(string userID)
        {
            var category = await _dataRepository.GetUserWorkoutsAsync(userID);

            return category;
        }


        /* // GET: api/UserWorkouts
         [HttpGet]
         public async Task<ActionResult<IEnumerable<UserWorkout>>> GetUserWorkouts()
         {
             return await _context.UserWorkouts.ToListAsync();
         }

         // GET: api/UserWorkouts/5
         [HttpGet("{id}")]
         public async Task<ActionResult<UserWorkout>> GetUserWorkout(int id)
         {
             var userWorkout = await _context.UserWorkouts.FindAsync(id);

             if (userWorkout == null)
             {
                 return NotFound();
             }

             return userWorkout;
         }

         // PUT: api/UserWorkouts/5
         // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
         [HttpPut("{id}")]
         public async Task<IActionResult> PutUserWorkout(int id, UserWorkout userWorkout)
         {
             if (id != userWorkout.UserID)
             {
                 return BadRequest();
             }

             _context.Entry(userWorkout).State = EntityState.Modified;

             try
             {
                 await _context.SaveChangesAsync();
             }
             catch (DbUpdateConcurrencyException)
             {
                 if (!UserWorkoutExists(id))
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

         // POST: api/UserWorkouts
         // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
         [HttpPost]
         public async Task<ActionResult<UserWorkout>> PostUserWorkout(UserWorkout userWorkout)
         {
             _context.UserWorkouts.Add(userWorkout);
             try
             {
                 await _context.SaveChangesAsync();
             }
             catch (DbUpdateException)
             {
                 if (UserWorkoutExists(userWorkout.UserID))
                 {
                     return Conflict();
                 }
                 else
                 {
                     throw;
                 }
             }

             return CreatedAtAction("GetUserWorkout", new { id = userWorkout.UserID }, userWorkout);
         }

         // DELETE: api/UserWorkouts/5
         [HttpDelete("{id}")]
         public async Task<IActionResult> DeleteUserWorkout(int id)
         {
             var userWorkout = await _context.UserWorkouts.FindAsync(id);
             if (userWorkout == null)
             {
                 return NotFound();
             }

             _context.UserWorkouts.Remove(userWorkout);
             await _context.SaveChangesAsync();

             return NoContent();
         }

         private bool UserWorkoutExists(int id)
         {
             return _context.UserWorkouts.Any(e => e.UserID == id);
         }*/
    }
}
