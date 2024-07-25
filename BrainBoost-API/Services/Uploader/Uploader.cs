namespace BrainBoost_API.Services.Uploader
{
    public class Uploader
    {
        public static async Task<string> uploadPhoto(IFormFile InsertedPhoto, string WhereToStore, string folderName)
        {
            var uploads = Path.Combine(Directory.GetCurrentDirectory(), $"wwwroot\\Images\\{WhereToStore}\\{folderName}");
            string photoUrl = "";

            if (!Directory.Exists(uploads))
                Directory.CreateDirectory(uploads);

            // Get all existing files in the directory and delete them
            var existingFiles = Directory.GetFiles(uploads);
            foreach (var file in existingFiles)
            {
                try
                {
                    File.Delete(file);
                }
                catch (IOException ioEx)
                {
                    // Handle any exceptions if needed
                    throw new IOException($"Error deleting existing file: {ioEx.Message}");
                }
            }

            // Save the new file
            var filePath = Path.Combine(uploads, InsertedPhoto.FileName);
            try
            {
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    await InsertedPhoto.CopyToAsync(fileStream);
                }
            }
            catch (Exception ex)
            {
                // Handle any exceptions if needed
                throw new Exception($"Error saving file: {ex.Message}");
            }

            // Construct the photo URL
            photoUrl = $"http://localhost:43827/Images/{WhereToStore}/{folderName}/{InsertedPhoto.FileName}";
            return photoUrl;
        }
    }
}
