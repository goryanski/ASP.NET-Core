using System;
using System.Collections.Generic;
using System.Text;

namespace Services.Abstractions.Service
{
    public interface IServiceManager
    {
        public IToursService ToursService { get; }
        public ICountriesService CountriesService { get; }
    }
}
