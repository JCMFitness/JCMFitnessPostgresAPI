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
        public List<UserWorkout> UserWorkouts { get; set; }
    }
}

