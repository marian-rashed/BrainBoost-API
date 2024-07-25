using System.ComponentModel.DataAnnotations.Schema;

namespace BrainBoost_API.Models
{
    public class subscription
    {
        public int Id { get; set; }
        public DateTime EndDate { get; set; }
        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; } 
        [ForeignKey("Teacher")]
        public int TeacherId { get; set; }
        [ForeignKey("Plan")]
        public int PlanId { get; set; }

        // Additional properties
        public string? SubscribtionsStatus { get; set; }
        public string? TransactionNo { get; set; }
        public string? CheckUrl { get; set; }
        public string? orderNumber { get; set; }


        public Teacher? Teacher { get; set; }
        public Plan? Plan { get; set; }

    }
}
