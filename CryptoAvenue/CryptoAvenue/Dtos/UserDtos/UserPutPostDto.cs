using System.ComponentModel.DataAnnotations;

namespace CryptoAvenue.Dtos.UserDtos
{
    public class UserPutPostDto
    {
        [Required]
        [MinLength(6)]
        [MaxLength(30)]
        public string Username { get; set; }

        [Required]
        [EmailAddress]
        [MinLength(10)]
        [MaxLength(50)]
        public string Email { get; set; }

        [Required]
        [MinLength(6)]
        [MaxLength(50)]
        public string Password { get; set; }

        [Required]
        [Range(0, 99)]
        public int Age { get; set; }

        [Required]
        [MinLength(8)]
        [MaxLength(50)]
        public string SecurityQuestion { get; set; }

        [Required]
        [MinLength(8)]
        [MaxLength(50)]
        public string SecurityAnswer { get; set; }
        public bool PrivateProfile { get; set; } = false;
    }
}
