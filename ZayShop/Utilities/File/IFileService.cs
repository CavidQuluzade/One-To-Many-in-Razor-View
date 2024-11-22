namespace ZayShop.Utilities.File
{
    public interface IFileService
    {
        string Upload(IFormFile file, string folder);
        void Delete(string folder, string fileName);
        bool isImage(string contentType);
        bool isValidSize(long length, long maxLength = 100);
    }
}
