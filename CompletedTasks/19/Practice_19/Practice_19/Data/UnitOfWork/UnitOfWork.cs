using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Practice_19.Data.Repository;

namespace Practice_19.Data.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    { 
        ApplicationDbContext db;

        CarDescriptionRepository _carDescriptionRepository;
        public CarDescriptionRepository CarDescriptionRepository
            => _carDescriptionRepository ?? (_carDescriptionRepository = new CarDescriptionRepository(db));

        TranslationRepository _translationRepository;
        public TranslationRepository TranslationRepository
            => _translationRepository ?? (_translationRepository = new TranslationRepository(db));

        public UnitOfWork(ApplicationDbContext context)
        {
            db = context;
        }
    }
}
