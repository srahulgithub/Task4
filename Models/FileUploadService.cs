namespace Task4.Models
{
    using MongoDB.Driver;
    using MongoDB.Driver.GridFS;

    public class FileUploadService
    {
        private readonly ImageDataContext _context;

        public FileUploadService(ImageDataContext context)
        {
            _context = context;
        }

        public async Task<string> UploadFile(IFormFile file, string text)
        {
            var imageData = new ImageData
            {
                Text = text
            };

            using (var stream = new MemoryStream())
            {
                await file.CopyToAsync(stream);
                var fileId = await _context.UploadFile(stream.ToArray(), file.FileName);
                imageData.ImageId = fileId;
            }

            await _context.ImageDatas.InsertOneAsync(imageData);
            return imageData.Id;
        }

        public async Task<ImageData> GetImageData(string id)
        {
            var imageData = await _context.ImageDatas.Find(x => x.Id == id).FirstOrDefaultAsync();
            return imageData;
        }

        public async Task UpdateImageData(string id, IFormFile file, string text)
        {
            var imageData = await _context.ImageDatas.Find(x => x.Id == id).FirstOrDefaultAsync();
            if (imageData == null)
            {
                return;
            }

            if (file != null)
            {
                await _context.DeleteFile(imageData.ImageId);
                using (var stream = new MemoryStream())
                {
                    await file.CopyToAsync(stream);
                    var fileId = await _context.UploadFile(stream.ToArray(), file.FileName);
                    imageData.ImageId = fileId;
                }
            }

            imageData.Text = text;
            await _context.ImageDatas.ReplaceOneAsync(x => x.Id == id, imageData);
        }
    }
}
