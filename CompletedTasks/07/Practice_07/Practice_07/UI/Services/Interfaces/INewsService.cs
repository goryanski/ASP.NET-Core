using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Practice_07.Models.News;

namespace Practice_07.UI.Services.Interfaces
{
    public interface INewsService
    {
        Task<List<NewsViewModel>> GetAllNews();
    }
}
