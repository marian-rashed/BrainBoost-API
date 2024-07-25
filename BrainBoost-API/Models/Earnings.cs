using System.ComponentModel.DataAnnotations.Schema;

namespace BrainBoost_API.Models
{
    public class Earnings
    {
        public int Id { get; set; }
        public bool IsDeleted { get; set; }
        [ForeignKey("enrollment")]
        public int? enrollmentId { get; set; }
        public decimal Amount { get; set; }
        public decimal InstructorEarnings { get; set; }
        public decimal WebsiteEarnings { get; set; }
        public Enrollment enrollment { get; set; }
        public DateTime paymentDate { get; set; }
    }
}
