using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;

namespace Practice_04.Services
{ 
    public class UploadFileService : IFileUpload
    {
        const string UPLOAD_FILES_DIR = "UploadFiles/";

        IWebHostEnvironment _webHostEnvironment;
        public UploadFileService(IWebHostEnvironment webHostEnvironment)
        {
            _webHostEnvironment = webHostEnvironment;
        }
        

        public async Task<string> Upload(IFormFile file)
        {
            double ms = DateTime.Now.ToUniversalTime().Subtract(new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc)).TotalMilliseconds;
            string path = Path.Combine(UPLOAD_FILES_DIR, $"{ms}_{file.FileName}");
            
            using (var fs = new FileStream(Path.Combine(_webHostEnvironment.WebRootPath, path), FileMode.Create))
            {
                await file.CopyToAsync(fs);
            }
            return path;
        }
    }
}
