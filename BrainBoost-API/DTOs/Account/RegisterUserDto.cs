using System.Runtime.CompilerServices;
using System.ComponentModel.DataAnnotations;

namespace BrainBoost_API.DTOs.Account
{
    public class RegisterUserDto
    {
        public string UserName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
