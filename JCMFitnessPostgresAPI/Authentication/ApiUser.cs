using JCMFitnessPostgresAPI.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JCMFitnessPostgresAPI.Authentication
{
    public class ApiUser:IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTimeOffset LastUpdated { get; set; }
        public bool IsDeleted { get; set; }

        public List<UserWorkout> UserWorkouts { get; set; }
    }
}

