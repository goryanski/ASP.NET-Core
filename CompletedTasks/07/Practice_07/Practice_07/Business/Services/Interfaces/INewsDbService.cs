using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Practice_07.Business.DTO;

namespace Practice_07.Business.Services
{
    public interface INewsDbService
    {
        Task<List<NewsDTO>> GetAllNews();
        Task<NewsDTO> GetNewsById(int id);
        Task CreateNewNews(NewsDTO product);
    }
}
