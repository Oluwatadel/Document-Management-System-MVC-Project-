using DMSMVC.Repository.Interface;

namespace DMSMVC.Repository.Implementation
{
    public class FileRepository : IFileRepository
    {
        private readonly IWebHostEnvironment _environment;
        private readonly IConfiguration _configure;

        public FileRepository(IWebHostEnvironment environment, IConfiguration configure)
        {
            _environment = environment;
            _configure = configure;
        }

        public string Upload(IFormFile file)
        {
            var uploadedFile = file.FileName.Split('.');
            var newFileName = $"{uploadedFile[0]}{Guid.NewGuid().ToString().Substring(1, 6)}.{uploadedFile[uploadedFile.Length - 1]}";
            var filePath = "";

            if (uploadedFile[uploadedFile.Length - 1] == "doc" || uploadedFile[uploadedFile.Length - 1] == "docx" || uploadedFile[uploadedFile.Length - 1] == "txt" || uploadedFile[uploadedFile.Length - 1] == "xlsx" || uploadedFile[1] == "pdf")
            {
                filePath = Path.Combine(_environment.WebRootPath, "src/Documents");
            }
            else if(uploadedFile[uploadedFile.Length - 1] == "jpg" || uploadedFile[uploadedFile.Length - 1] == "jpeg" || uploadedFile[uploadedFile.Length - 1] == "png")
            {
                filePath = Path.Combine(_environment.WebRootPath, "src/Picture");
            }
            else
            {
                filePath = Path.Combine(_environment.WebRootPath, "src/Others");

            }
            if (!Directory.Exists(filePath))
            {
                Directory.CreateDirectory(filePath);
            }

            var newFilePath = Path.Combine(filePath, newFileName);
            using (var fileTobeUploaded = new FileStream(newFilePath, FileMode.Create))
            {
                file.CopyTo(fileTobeUploaded);
            }
            return newFileName;
        }
    }
}
