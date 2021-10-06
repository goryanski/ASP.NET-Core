using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Practice_19.Data.Repository;

namespace Practice_19.Data.UnitOfWork
{
    public interface IUnitOfWork
    {
        public CarDescriptionRepository CarDescriptionRepository { get; }
        public TranslationRepository TranslationRepository { get; }
    }
}
