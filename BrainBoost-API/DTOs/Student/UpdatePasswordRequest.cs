namespace BrainBoost_API.DTOs.Student
{
    public class UpdatePasswordRequest
    {
        public string UserId { get; set; }
        public string NewPassword { get; set; }
        public string CurrentPassword { get; set; }
    }
}
