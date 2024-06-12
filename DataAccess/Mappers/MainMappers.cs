using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Mappers
{
    public class MainMappers
    {
        private static readonly MapperConfiguration Config = new MapperConfiguration(cfg =>
        {
            cfg.CreateMap<Car, DTO.Model.Car>().ReverseMap();
            cfg.CreateMap<Ferry, DTO.Model.Ferry>().ReverseMap();
            cfg.CreateMap<Passenger, DTO.Model.Passenger>().ReverseMap();
        });

        public static Mapper Instance { get; } = new Mapper(Config);

        public static void UpdateFerry(DTO.Model.Ferry ferry, Ferry oldFerry)
        {
            if (ferry != null)
            {
                oldFerry.Name = ferry.Name;
                oldFerry.PriceCar = ferry.PriceCar;
                oldFerry.PricePassenger = ferry.PricePassenger;
                if (oldFerry.Passengers.Count > ferry.MaxPassengers)
                {
                    throw new Exception("Can't change max passengers to lower than whats already on ferry");
                }
                if (oldFerry.Cars.Count > ferry.MaxCars)
                {
                    throw new Exception("Can't change max cars to lower than whats already on ferry");
                }
                oldFerry.MaxCars = ferry.MaxCars;
                oldFerry.MaxPassengers = ferry.MaxPassengers;
            }
            else
                oldFerry = null;
        }
    }
}
