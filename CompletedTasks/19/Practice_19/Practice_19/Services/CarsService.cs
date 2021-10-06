using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Practice_19.Data.Entity;
using Practice_19.Data.UnitOfWork;
using Practice_19.Models;

namespace Practice_19.Services
{
    public class CarsService : ICarsService
    {
        IUnitOfWork uow;
        const string KeyName = "name";
        const string KeyDescription = "description";
        const string KeyCharacteristics = "characteristics";
        public CarsService(IUnitOfWork uow)
        {
            this.uow = uow;
        }

        public async Task AddCar(List<AddCarViewModel> carTranslateVersions)
        {
            List<Translation> translations = new List<Translation>();
            foreach (var version in carTranslateVersions)
            {
                translations.Add(new Translation
                {
                    Key = KeyName,
                    Value = version.Name,
                    Lang = version.Lang
                });
                translations.Add(new Translation
                {
                    Key = KeyDescription,
                    Value = version.Description,
                    Lang = version.Lang
                });
                translations.Add(new Translation
                {
                    Key = KeyCharacteristics,
                    Value = version.Characteristics,
                    Lang = version.Lang
                });
            }
            foreach (var translation in translations)
            {
                await uow.TranslationRepository.Create(translation);
            }
            CarDescription car = new CarDescription
            {
                Translations = translations
            };
            await uow.CarDescriptionRepository.Create(car);
        }

        public async Task<List<CarViewModel>> GetCars(string lang)
        {
            List<CarDescription> carsEntities = (await uow.CarDescriptionRepository.GetAll()).ToList();
            List<CarViewModel> carsViews = new List<CarViewModel>(); 
            foreach (var carEntity in carsEntities)
            {
                CarViewModel car = new CarViewModel();
                foreach (var translation in carEntity.Translations)
                {
                    if (translation.Lang.Equals(lang))
                    {
                         switch(translation.Key)
                         {
                            case KeyName:
                                car.Name = translation.Value;
                                break;
                            case KeyDescription:
                                car.Description = translation.Value;
                                break;
                            case KeyCharacteristics:
                                car.Characteristics = translation.Value;
                                break;
                         }
                    }
                }
                carsViews.Add(car);
            }
            return carsViews;
        }
    }
}
