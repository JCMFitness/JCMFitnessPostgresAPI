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
        [Required]
        [Key]
        public string Id { get; set; }
        public int UserID { get; set; }
        public int WorkoutID { get; set; }

    
        public virtual User User { get; set; }

        public virtual Workout Workout { get; set; }

        public DateTime CreatedDate { get; set; } = DateTime.UtcNow;
        public bool IsPublic { get; set; }
    }
}
