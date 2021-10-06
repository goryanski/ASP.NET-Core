using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Practice_07.Business.DTO;
using Practice_07.DB.Repositories;

namespace Practice_07.Business.Services
{
    public class NewsDbService : INewsDbService
    {
        private Automapper.ObjectMapper objectMapper = Automapper.ObjectMapper.Instance;
        IUnitOfWork uow;
        public NewsDbService(IUnitOfWork uow)
        {
            this.uow = uow;
        }
        public /*async*/ Task CreateNewNews(NewsDTO news)
        {
            //await uow.NewsRepository.CreateAsync()
            throw new NotImplementedException();
        }

        public async Task<List<NewsDTO>> GetAllNews()
        {
            
            var result = await uow.NewsRepository.GetAllAsync();
            return objectMapper.Mapper.Map<List<NewsDTO>>(result);
        }

        public Task<NewsDTO> GetNewsById(int id)
        {
            throw new NotImplementedException();
        }
    }
}
