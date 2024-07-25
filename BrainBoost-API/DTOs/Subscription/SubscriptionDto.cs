using System.ComponentModel.DataAnnotations;

namespace BrainBoost_API.DTOs.Subscription
{
    public class SubscriptionDto
    {
        public int Id { get; set; }
        public DateTime EndDate { get; set; }
        public bool IsActive => false;
        [Required(ErrorMessage = "TeacherID is required")]
        public int TeacherId { get; set; }
        [Required(ErrorMessage = "PlanID is required")]
        public int PlanId { get; set; }
    }
}
