namespace BrainBoost_API.DTOs.Uploader
{
    public class Uploader
    {
        public static async Task<string> uploadPhoto(IFormFile InsertedPhoto, string WhereToStore,string folderName)
        {
            var uploads = Path.Combine(Directory.GetCurrentDirectory(), $"wwwroot\\Images\\{WhereToStore}\\{folderName}");
            string photoUrl = "";
            if (!Directory.Exists(uploads))
                Directory.CreateDirectory(uploads);
            var filePath = Path.Combine(uploads, InsertedPhoto.FileName);

            using (var fileStream = new FileStream(filePath, FileMode.Create))
            {
                await InsertedPhoto.CopyToAsync(fileStream);
            }
            photoUrl = $"http://localhost:43827/Images/{WhereToStore}/{folderName}/{InsertedPhoto.FileName}";
            return photoUrl;
        }
    }
}
