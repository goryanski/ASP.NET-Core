using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Practice_13.Business.DTO;
using Practice_13.Business.Services.Interfaces;
using Practice_13.DB.Repositories.Interfaces;

namespace Practice_13.Business.Services
{
    public class AuthorsService : IAuthorsService
    {
        private IUnitOfWork uow;
        private Automapper.ObjectMapper objectMapper = Automapper.ObjectMapper.Instance;

        public AuthorsService(IUnitOfWork uow)
        {
            this.uow = uow;
        }

        public async Task<bool> AuthorWasNotFound(int id)
        {
            return await uow.AuthorsRepository.GetAsync(id) == null;
        }

        public async Task<AuthorDTO> GetAuthorFullInfo(int id)
        {
            var result = await uow.AuthorsRepository.GetFullAuthorInfo(id);

            // to avoid queries cycle
            result.Books.ForEach(b =>
            {
                b.Authors = null;
            });
            return objectMapper.Mapper.Map<AuthorDTO>(result);
        }
    }
}
