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
        public IActionResult AddUser([FromBody] User user)
        {
            if (ModelState.IsValid)
            {
                //Guid obj = Guid.NewGuid();
                //user.UserID = obj.ToString("n");
                _dataRepository.AddUserAsync(user);
                return Ok();
            }
            return BadRequest();
        }

        [HttpGet("{id}")]
        public Task<User> GetUserByID(string id)
        {
            return _dataRepository.GetUserAsync(id);
        }

        [HttpPut]
        public IActionResult UpdateUser([FromBody] User user)
        {
            if (ModelState.IsValid)
            {
                _dataRepository.EditUserAsync(user);
                return Ok();
            }
            return BadRequest();
        }
    }
}
