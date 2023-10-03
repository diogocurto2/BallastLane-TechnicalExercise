using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BallastLane.Security.Domain.Entities
{
    public class User
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(255)]
        public string Name { get; set; }

        [Required]
        [MaxLength(255)]
        public string Email { get; set; }

        [Required]
        [MaxLength(255)]
        public string LoginName { get; set; }

        [Required]
        [MaxLength(255)]
        public string Password { get; set; }

    }
}
