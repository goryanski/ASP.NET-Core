using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Domain.Entity;

namespace Domain.Repository
{
    public interface IUnitOfWork
    {
        public IRepository<Guid, Tour> ToursRepository { get; } 
        public IRepository<Guid, TourEvent> TourEventsRepository { get; } 
        public IRepository<Guid, Location> LocationsRepository { get; } 
        public IRepository<Guid, Hotel> HotelsRepository { get; } 
        public IRepository<Guid, Country> CountriesRepository { get; } 
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}
