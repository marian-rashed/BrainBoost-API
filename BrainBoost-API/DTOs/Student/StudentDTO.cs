namespace BrainBoost_API.DTOs.Student
{
    public class StudentDTO
    {
        public int Id { get; set; }

        public int? NumOfCrsEnrolled { get; set; }
        public int? NumOfCertificates { get; set; }
        public int? NumOfCrsSaved { get; set; }
        public string? Fname { get; set; }
        public string? Lname { get; set; }
        public string? PictureUrl { get; set; }
    }
}
