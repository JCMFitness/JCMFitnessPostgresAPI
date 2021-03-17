using JCMFitnessPostgresAPI.DataAccess;
using JCMFitnessPostgresAPI.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JCMFitnessPostgresAPI.Controllers
{
    [Microsoft.AspNetCore.Mvc.Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IDataRepository _dataRepository;

        public UserController(IDataRepository userRepository)
        {
            _dataRepository = userRepository;
        }

        [HttpGet]
        public async Task<IEnumerable<User>> GetAllUsers()
        {
            return await _dataRepository.GetUsersAsync();
        }


        [HttpPost]
        public async Task<IActionResult> AddUser([FromBody] User user)
        {
            if (ModelState.IsValid)
            {
                //Guid obj = Guid.NewGuid();
                //user.UserID = obj.ToString("n");

                try
                {
                    await _dataRepository.AddUserAsync(user);
                    return Ok();
                }
                catch(InvalidOperationException e)
                {
                    return BadRequest(e);
                }
               
            }
            return BadRequest();
        }

        [HttpGet("{id}")]
        public async Task<User> GetUserByID(string id)
        {
            return await _dataRepository.GetUserAsync(id);
        }

        [HttpPut]
        public async  Task<IActionResult> UpdateUser([FromBody] User user)
        {
            if (ModelState.IsValid)
            {
                await _dataRepository.EditUserAsync(user);
                return Ok();
            }
            return BadRequest();
        }

        // DELETE: api/Users/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(string userID)
        {
            await _dataRepository.DeleteUserAsync(userID);
          

            return Ok();
        }
    }
}
