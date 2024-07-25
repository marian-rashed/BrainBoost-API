using System.ComponentModel.DataAnnotations;

namespace BrainBoost_API.DTOs.Account
{
    public class LoginUserDto
    {
        [Required]
        public string UserName { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
