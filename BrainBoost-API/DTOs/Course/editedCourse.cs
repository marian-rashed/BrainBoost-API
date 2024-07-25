using Microsoft.IdentityModel.Tokens;

namespace BrainBoost_API.DTOs.Course
{
    public class editedCourse
    {
        public int id { get; set; }
        public int price { get; set; }
        public string name { get; set; }
        public string? description {  get; set; }
        public string? categoryName { get; set; }
        public string? language { get; set; }
        public string? level { get; set; }

    }
}
