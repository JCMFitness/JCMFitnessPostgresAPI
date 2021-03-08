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
        private readonly IUserRepository _userRepository;

        public UserController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        [HttpGet]
        public IEnumerable<User> GetAllUsers()
        {
            return _userRepository.GetAllUsers();
        }

        [HttpPost]
        public IActionResult AddUser([FromBody] User user)
        {
            if (ModelState.IsValid)
            {
                //Guid obj = Guid.NewGuid();
                //user.UserID = obj.ToString("n");
                _userRepository.AddUser(user);
                return Ok();
            }
            return BadRequest();
        }

        [HttpGet("{id}")]
        public User GetUserByID(string id)
        {
            return _userRepository.GetUserByID(id);
        }

        [HttpPut]
        public IActionResult UpdateUser([FromBody] User user)
        {
            if (ModelState.IsValid)
            {
                _userRepository.UpdateUser(user);
                return Ok();
            }
            return BadRequest();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteUser(string id)
        {
            var data = _userRepository.GetUserByID(id);

            if (data == null)
            {
                return NotFound();
            }
            _userRepository.DeleteUser(id);
            return Ok();
        }
/*
        [HttpGet("/workouts")]
        public List<Workout> GetWorkouts(string id)
        {
            var user = _userRepository.GetUserByID(id);

           
        }*/

    }
}
