using System.ComponentModel.DataAnnotations;

namespace CryptoAvenue.Dtos.UserDtos
{
    public class UserPutPostDto
    {
        [Required]
        public string Username { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        public int Age { get; set; }

        [Required]
        public string SecurityQuestion { get; set; }
        [Required]
        public string SecurityAnswer { get; set; }
        public bool PrivateProfile { get; set; }
    }
}
