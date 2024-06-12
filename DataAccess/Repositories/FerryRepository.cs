using DataAccess.Context;
using DataAccess.Mappers;
using DTO.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repositories
{
    public class FerryRepository
    {
        public static DTO.Model.Ferry getFerry(int id)
        {
            using (MainContext context = new MainContext())
            {
                return MainMappers.Instance.Map<DTO.Model.Ferry>(context.Ferrys
                                                    .Include(f => f.Passengers)
                                                    .Include(f => f.Cars)
                                                    .FirstOrDefault(f => f.FerryID == id));
            }
        }

        public static List<DTO.Model.Ferry> getFerrys()
        {
            using (MainContext context = new MainContext())
            {
                var ferrys = context.Ferrys.Include(f => f.Passengers)
                                          .Include(f => f.Cars)
                                          .ToList();
                var result = ferrys.Select(f => MainMappers.Instance.Map<DTO.Model.Ferry>(f)).ToList();
                return result;
            }
        }

        public static void EditFerry(DTO.Model.Ferry ferry)
        {
            using (MainContext context = new MainContext())
            {
                Ferry oldFerry = context.Ferrys.Include(f => f.Passengers)
                                                .Include(f => f.Cars)
                                                .FirstOrDefault(f => f.FerryID == ferry.FerryID);
                MainMappers.UpdateFerry(ferry, oldFerry);
                context.SaveChanges();
            }
        }

        public static void AddPassengerToFerry(DTO.Model.Passenger passenger, DTO.Model.Ferry ferry)
        {
            using (MainContext context = new MainContext())
            {
                if (ferry.Passengers.Count < ferry.MaxPassengers)
                {
                    ferry.Passengers.Add(passenger);
                    passenger.FerryID = ferry.FerryID;
                    PassengerRepository.AddPassenger(passenger);
                } else
                {
                    throw new Exception("Max passenger count exceeded");
                }
            }
        }

        public static void AddCarToFerry(DTO.Model.Car car, DTO.Model.Ferry ferry)
        {
            using (MainContext context = new MainContext())
            {
                if ((ferry.Passengers.Count+car.Passengers.Count) <= ferry.MaxPassengers && ferry.Cars.Count < ferry.MaxCars)
                {
                    if (car.Passengers.Count > 0)
                    {
                        foreach (var p in car.Passengers)
                        {
                            p.FerryID = ferry.FerryID;
                            p.CarID = car.CarID;
                        }
                        ferry.Cars.Add(car);
                        car.FerryID = ferry.FerryID;
                        CarRepository.AddCar(car);
                    } else
                    {
                        throw new Exception("Car must have atleast 1 passenger");
                    }
                } else
                {
                    throw new Exception("Max Passenger or car count exceeded");
                }
            }
        }
    }
}
