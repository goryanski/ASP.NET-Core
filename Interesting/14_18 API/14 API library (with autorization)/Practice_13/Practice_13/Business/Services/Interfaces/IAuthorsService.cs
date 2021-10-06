using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Practice_13.Business.DTO;

namespace Practice_13.Business.Services.Interfaces
{
    public interface IAuthorsService
    {
        Task<bool> AuthorWasNotFound(int id);
        Task<AuthorDTO> GetAuthorFullInfo(int id);
    }
}
