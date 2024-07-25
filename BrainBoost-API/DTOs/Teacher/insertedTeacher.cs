namespace BrainBoost_API.DTOs.Teacher
{
    public class insertedTeacher
    {
       public string jobTitles {  get; set; }
       public string aboutYou { get; set; }
       public IFormFile photo { get; set; }
       public int yearsOfExperience { get; set; }
       public string userId { get; set; }
    }
}
