using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace JCMFitnessPostgresAPI.Models
{
    public class UserWorkout
    {
        [Key, Column(Order = 0)]
        public int UserID { get; set; }

        [Key, Column(Order = 1)]
        public int WorkoutID { get; set; }

        public virtual User User { get; set; }
        public virtual Workout Workout { get; set; }

        public DateTime CreatedDate { get; set; } = DateTime.UtcNow;

    }
}
