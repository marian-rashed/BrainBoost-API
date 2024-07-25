namespace BrainBoost_API.Models
{
    public class Plan
    {
        public int Id { get; set; }
        public string Name  { get; set; }
        public int Price { get; set; }
        public int Duration { get; set; }
        public bool IsDeleted { get; set; }

    }
}
