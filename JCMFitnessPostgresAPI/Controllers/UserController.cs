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
                catch
                {
                    return BadRequest("User already exists");
                }

            }
            return BadRequest("User Object is not valid");
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<User>> GetUserByID(string id)
        {
            try
            {
                return await _dataRepository.GetUserAsync(id);
            }
            catch
            {
                return BadRequest("User id does not exist");
            }

        }

        [HttpGet("loginuser")]
        public async Task<ActionResult<User>> GetUserByUsernameAndPassword(string userName, string password)
        {
            try
            {
                return await _dataRepository.LoginUserAsync(userName, password);
            }
            catch(InvalidOperationException)
            {
                return BadRequest("User does not exist");
            }
            catch(InvalidProgramException)
            {
                return BadRequest("Password did not match");
            }

        }

        [HttpPut]
        public async  Task<IActionResult> UpdateUser([FromBody] User user)
        {
            if (ModelState.IsValid)
            {
                await _dataRepository.EditUserAsync(user);
                return Ok();
            }
            return BadRequest("User object is not valid");
        }

        // DELETE: api/Users/5
        [HttpDelete]
        public async Task<IActionResult> DeleteUser(string userID)
        {
            try
            {
                await _dataRepository.DeleteUserAsync(userID);
                return Ok();
            }
            catch
            {
                return BadRequest("User Id does not exist");
            }


            //return Ok();
        }
    }
}
