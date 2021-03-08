using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace JCMFitnessPostgresAPI.Models
{
    public class User
    {
        [Required]
        [Key]
        public string UserID { get; set; }

        public string FirstName { get; set; }
        public string LastName { get; set; }


        public string UserName { get; set; }

        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        public string Password { get; set; }


        public DateTime JoinedDate { get; set; } = DateTime.UtcNow;
        public bool IsAdmin { get; set; }
    }
}
