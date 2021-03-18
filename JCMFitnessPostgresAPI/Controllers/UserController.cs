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

        [HttpGet("getall")]
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
                if (!_dataRepository.UserExists(user.UserID))
                {
                    await _dataRepository.AddUserAsync(user);
                    return Ok();
                }
                else
                {

                    return BadRequest("User already exists");
                }
            }
            return BadRequest("User Object is not valid");
        }

        [HttpGet]
        public async Task<ActionResult<User>> GetUserByID(string id)
        {
            if (_dataRepository.UserExists(id))
            {
                return await _dataRepository.GetUserAsync(id);
            }
            else
            {
                return BadRequest("User id does not exist");
            }
        }

        [HttpGet("login")]
        public async Task<ActionResult<User>> GetUserByUsernameAndPassword(string userName, string password)
        {

            var user = await _dataRepository.LoginUserAsync(userName, password);

            if (user == null)
            {
                return BadRequest("User with that username does not exist");
            }
                
            if(user.Password != password)
            {
                return BadRequest("Password did not match");
            }

            return user;
        }

        [HttpPut]
        public async Task<IActionResult> UpdateUser([FromBody] User user)
        {
            if (ModelState.IsValid)
            {
                await _dataRepository.EditUserAsync(user);
                return Ok();
            }
            return BadRequest("User object is not valid");
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteUser(string userID)
        {
            if (_dataRepository.UserExists(userID))
            {
                await _dataRepository.DeleteUserAsync(userID);
                return Ok();
            }
            else
            {
                return BadRequest("User id does not exist");
            }

        }
    }
}
