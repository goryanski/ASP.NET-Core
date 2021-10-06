using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Practice_19.Models;

namespace Practice_19.Services
{
    public interface ICarsService
    {
        Task AddCar(List<AddCarViewModel> carTranslateVersions);
        Task<List<CarViewModel>> GetCars(string lang);
    }
}
