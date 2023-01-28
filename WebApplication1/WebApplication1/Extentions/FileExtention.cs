using WebApplication1.Models;
using WebApplication1.VMs.PortfolioVMs;

namespace WebApplication1.Extentions
{
    public static class FileExtention
    {
        public static string CreateFile(this IFormFile formFile,string env,string folderPath)
        {
            string Image = Guid.NewGuid().ToString() + formFile.FileName;
            string FullPath = Path.Combine(env, "assets/img", Image);
            using (FileStream fileStream = new FileStream(FullPath, FileMode.Create))
            {
                formFile.CopyTo(fileStream);
            }
            return Image;
        }
        public static bool IsSizeOkay(this IFormFile formFile, int size)
        {
            return (int)Math.Ceiling((decimal)formFile.Length / 1024 / 1024) <=size;
        }
        public static bool IsFormatOkay(this IFormFile formFile,string fileFormat)
        {
            return formFile.ContentType.Contains(fileFormat.ToLower());
        }
    }
}
