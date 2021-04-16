using JCMFitnessPostgresAPI.Authentication;
using JCMFitnessPostgresAPI.DataAccess;
using JCMFitnessPostgresAPI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JCMFitnessPostgresAPI.Controllers
{
    [Microsoft.AspNetCore.Mvc.Route("api/[controller]")]
    [Authorize]
    [ApiController]
  
    public class UserController : ControllerBase
    {
        private readonly IDataRepository _dataRepository;

        public UserController(IDataRepository userRepository)
        {
            _dataRepository = userRepository;
        }

        [HttpGet("getall")]
        [Authorize(Roles = UserRoles.Admin)]
        public async Task<IEnumerable<ApiUser>> GetAllUsers()
        {
            return await _dataRepository.GetUsersAsync();
        }


        [HttpGet]
        public async Task<ActionResult<ApiUser>> GetUserByID(string userid)
        {
            if (_dataRepository.UserExists(userid))
            {
                return await _dataRepository.GetUserAsync(userid);
            }
            else
            {
                return BadRequest("User id does not exist");
            }
        }


        [HttpPut]
        public async Task<IActionResult> UpdateUser([FromBody] ApiUser user)
        {
            if (ModelState.IsValid)
            {
                await _dataRepository.EditUserAsync(user);
                return Ok();
            }
            return BadRequest("User object is not valid");
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteUser(string userid)
        {
            if (_dataRepository.UserExists(userid))
            {
                await _dataRepository.DeleteUserAsync(userid);
                return Ok();
            }
            else
            {
                return BadRequest("User id does not exist");
            }

        }
    }
}
