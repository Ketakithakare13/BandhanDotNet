//using BandhanApp.Infrastructure;

//namespace BandhanApp.Repositories
//{
//    public class ProfileImageRepo : IProfileImage

//    {
//        private readonly IWebHostEnvironment _webHostEnvironment;

//        public ProfileImageRepo(IWebHostEnvironment webHostEnvironment)
//        {
//            _webHostEnvironment = webHostEnvironment;
//        }

//        private Microsoft.AspNetCore.Hosting.IHostingEnvironment _hostingEnvironment;
//        public ProfileImageRepo(Microsoft.AspNetCore.Hosting.IHostingEnvironment hostingEnvironment)
//        {
//            _hostingEnvironment = hostingEnvironment;

//        }
//        public async void UploadImage(IFormFile file)
//        {
//            long totalBytes = file.Length;
//            string filename = file.FileName.Trim('"');
//            filename = EnsureFileName(filename);
//            byte[] buffer = new byte[16 * 1024];
//            using (FileStream output = System.IO.File.Create(GetpathAndFileName(filename)))
//            {
//                using (Stream input = file.OpenReadStream())
//                {
//                    int readBytes;
//                    while ((readBytes = input.Read(buffer, 0, buffer.Length)) > 0)
//                    {
//                        await output.WriteAsync(buffer, 0, readBytes);
//                        totalBytes += readBytes;
//                    }
//                }

//            }
//        }



//        private string GetpathAndFileName(string filename)
//        {
//            string path = _hostingEnvironment.WebRootPath + "\\uploads\\";
//            if (!Directory.Exists(path))

//                Directory.CreateDirectory(path);
//                return path + filename;

//        }

//        private string EnsureFileName(string filename)
//        {
//            if(filename.Contains("\\"))
//                filename=filename.Substring(filename.LastIndexOf("\\") + 1);
//            return filename;
//        }
//    }
//}

using BandhanApp.Infrastructure;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System.IO;
using System.Threading.Tasks;

namespace BandhanApp.Repositories
{
    public class ProfileImageRepo : IProfileImage
    {
        private readonly IWebHostEnvironment _webHostEnvironment;

        public ProfileImageRepo(IWebHostEnvironment webHostEnvironment)
        {
            _webHostEnvironment = webHostEnvironment;
        }

        public async void UploadImage(IFormFile file)
        {
            long totalBytes = file.Length;
            string filename = file.FileName.Trim('"');
            filename = EnsureFileName(filename);
            byte[] buffer = new byte[16 * 1024];
            using (FileStream output = System.IO.File.Create(GetpathAndFileName(filename)))
            {
                using (Stream input = file.OpenReadStream())
                {
                    int readBytes;
                    while ((readBytes = input.Read(buffer, 0, buffer.Length)) > 0)
                    {
                        await output.WriteAsync(buffer, 0, readBytes);
                        totalBytes += readBytes;
                    }
                }

            }
        }
        //public async Task UploadImage(IFormFile file)
        //{
        //    long totalBytes = file.Length;
        //    string filename = file.FileName.Trim('"');
        //    filename = EnsureFileName(filename);
        //    byte[] buffer = new byte[16 * 1024];
        //    using (FileStream output = System.IO.File.Create(GetPathAndFileName(filename)))
        //    {
        //        using (Stream input = file.OpenReadStream())
        //        {
        //            int readBytes;
        //            while ((readBytes = input.Read(buffer, 0, buffer.Length)) > 0)
        //            {
        //                await output.WriteAsync(buffer, 0, readBytes);
        //                totalBytes += readBytes;
        //            }
        //        }
        //    }
        //}

        private string GetpathAndFileName(string filename)
        {
            string path = Path.Combine(_webHostEnvironment.WebRootPath, "uploads");
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            return Path.Combine(path, filename);
        }

        private string EnsureFileName(string filename)
        {
            if (filename.Contains("\\"))
            {
                filename = filename.Substring(filename.LastIndexOf("\\") + 1);
            }
            return filename;
        }

    }
}
