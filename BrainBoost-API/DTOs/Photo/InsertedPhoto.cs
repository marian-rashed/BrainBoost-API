namespace BrainBoost_API.DTOs.Photo
{
    public class InsertedPhoto
    {
        public IFormFile Photo {  get; set; }
        public string WhereToStore {  get; set; }
        public string folderName { get; set; }
    }
}
