using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace JCMFitnessPostgresAPI.Models
{
    public class Workout
    {
        public Workout()
        {
            UserWorkouts = new List<UserWorkout>();
        }

        [Required]
        [Key]
        public string WorkoutID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Category { get; set; }
        public bool IsPublic { get; set; }

        public virtual List<UserWorkout> UserWorkouts { get; set; }

    }
}
