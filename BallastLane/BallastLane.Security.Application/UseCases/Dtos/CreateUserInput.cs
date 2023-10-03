using System.ComponentModel.DataAnnotations;

namespace BallastLane.Security.Application.UseCases.Dtos
{
    public class CreateUserInput
    {
        [Required]
        [MaxLength(255)]
        public string Name { get; set; }

        [Required]
        [MaxLength(255)]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [MaxLength(255)]
        public string LoginName { get; set; }

        [Required]
        [MinLength(8)]
        [MaxLength(255)]
        public string Password { get; set; }
    }
}
