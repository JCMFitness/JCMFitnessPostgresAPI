using JCMFitnessPostgresAPI.Authentication;
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
    [Route("api/[controller]")]
    //[Authorize]
    [ApiController]
  
    public class UserController : ControllerBase
    {
        private readonly IDataRepository _dataRepository;
        private readonly ILogger<UserController> _logger;
        public UserController(IDataRepository userRepository, ILogger<UserController> logger)
        {
            _dataRepository = userRepository;
            _logger = logger;
        }

        [HttpGet("getall")]
        //[Authorize(Roles = UserRoles.Admin)]
        public async Task<IEnumerable<ApiUser>> GetAllUsers()
        {
            //logdebug every call
            return await _dataRepository.GetUsersAsync();
        }


        [HttpGet]
        public async Task<ActionResult<ApiUser>> GetUserByID(string userid)
        {
            if (_dataRepository.UserExists(userid))
            {
                //logInfo succesfully retrived a user 
                return await _dataRepository.GetUserAsync(userid);
            }
            else
            {
                //logerror user doesnt exist
                return BadRequest("User id does not exist");
            }
        }


        [HttpPut]
        public async Task<IActionResult> UpdateUser([FromBody] ApiUser user)
        {
            if (ModelState.IsValid)
            {
                //loginfo user edited
                await _dataRepository.EditUserAsync(user);
                return Ok();
            }
            //logerror user edit object invalid
            return BadRequest("User object is not valid");
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteUser(string userid)
        {
            if (_dataRepository.UserExists(userid))
            {
                //loginfo userid of user being deleted
                await _dataRepository.DeleteUserAsync(userid);
                return Ok();
            }
            else
            {
                //logerror tried to delete user that doesnt exist, shouldnt ever reach this.
                return BadRequest("User id does not exist");
            }

        }
    }
}
