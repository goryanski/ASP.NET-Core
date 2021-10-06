using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Practice_07.Business.Services;
using Practice_07.Models.News;
using Practice_07.UI.Services.Interfaces;

namespace Practice_07.UI.Services.Implementation
{
    public class NewsService : INewsService
    {
        private Automapper.ObjectMapper objectMapper = Automapper.ObjectMapper.Instance;
        INewsDbService _newsDbService;
        public NewsService(INewsDbService newsDbService)
        {
            _newsDbService = newsDbService;
        }

        public async Task<List<NewsViewModel>> GetAllNews()
        {
            var result = await _newsDbService.GetAllNews();
            return objectMapper.Mapper.Map<List<NewsViewModel>>(result);
        }
    }
}
