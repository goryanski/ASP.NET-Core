using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Practice_04.Services.Interfaces;

namespace Practice_04.Services.Implementation
{
    public class FileHelper : IFileHelper
    {
        const string UPLOAD_FILES_DIR = "UploadFiles/";

        IWebHostEnvironment _webHostEnvironment;
        public FileHelper(IWebHostEnvironment webHostEnvironment)
        {
            _webHostEnvironment = webHostEnvironment;
        }

        public void DeleteProductPrevPhoto(string prevPhoto)
        {
            string prevPhotoName = Path.GetFileName(prevPhoto);
            string directoryFullPath = Path.Combine(_webHostEnvironment.WebRootPath, UPLOAD_FILES_DIR);
            string[] files = Directory.GetFiles(directoryFullPath);
            for (int i = 0; i < files.Length; i++)
            {
                if (Path.GetFileName(files[i]).Equals(prevPhotoName))
                {
                    File.Delete(Path.Combine(directoryFullPath, prevPhotoName));
                }
            }
        }
    }
}
