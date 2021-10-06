using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace Practice_04.Services
{
    public interface IFileUpload
    {
        Task<string> Upload(IFormFile file);
    }
}
