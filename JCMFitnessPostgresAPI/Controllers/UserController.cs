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
            _logger.LogDebug("{Prefix}: Attempted to get all users", Prefixes.USER);
            return await _dataRepository.GetUsersAsync();
        }


        [HttpGet]
        public async Task<ActionResult<ApiUser>> GetUserByID(string userid)
        {
            if (_dataRepository.UserExists(userid))
            {
                _logger.LogDebug("{Prefix}: Attempted to get user at Id: {Id}", Prefixes.USER, userid);
                return await _dataRepository.GetUserAsync(userid);
            }
            else
            {
                _logger.LogError("{Prefix}: Attempted to get user at Id: {Id} that does not exist", Prefixes.USER,userid);
                return BadRequest("User id does not exist");
            }
        }


        [HttpPut]
        public async Task<IActionResult> UpdateUser([FromBody] ApiUser user)
        {
            if (ModelState.IsValid)
            {
                
                await _dataRepository.EditUserAsync(user);
                _logger.LogInformation("{Prefix}: Edited User with Id: {Id}", Prefixes.USER, user.Id);
                return Ok();
            }
            _logger.LogError("{Prefix}: Invalid edited submitted for Id: {Id}",Prefixes.USER, user.Id);
            return BadRequest("User object is not valid");
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteUser(string userid)
        {
            if (_dataRepository.UserExists(userid))
            {
                //loginfo userid of user being deleted
                await _dataRepository.DeleteUserAsync(userid);
                _logger.LogInformation("{Prefix}: Deleted User with Id: {Id}", Prefixes.USER, userid);
                return Ok();
            }
            else
            {
                _logger.LogError("{Prefix}: Unable to delete User with Id: {Id}, Id does not exist", Prefixes.USER, userid);
                return BadRequest("User id does not exist");
            }

        }
    }
}
