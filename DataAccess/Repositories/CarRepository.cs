using AutoMapper;
using DataAccess.Context;
using DataAccess.Mappers;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repositories
{
    public class CarRepository
    {
        public static DTO.Model.Car getCar(int id)
        {
            using (MainContext context = new MainContext())
            {
                return MainMappers.Instance.Map<DTO.Model.Car>(context.Cars
                                                                            .Include(c => c.Passengers)
                                                                            .Include(c => c.Ferry)
                                                                            .FirstOrDefault(c => c.CarID == id));
            }
        }
        public static List<DTO.Model.Car> getCars()
        {
            using (MainContext context = new MainContext())
            {
                var cars = context.Cars.Include(c => c.Passengers)
                                          .Include(c => c.Ferry)
                                          .ToList();
                var result = cars.Select(c => MainMappers.Instance.Map<DTO.Model.Car>(c)).ToList();
                return result;
            }
        }

        public static void AddPassengerToCar(DTO.Model.Passenger passenger, DTO.Model.Car car)
        {
            if (car.Passengers.Count < 5)
            {
                car.Passengers.Add(passenger);
            } else
            {
                throw new Exception("Cannot add more than 5 Passengers to Car");
            }
            
        }

        public static void AddCar(DTO.Model.Car car)
        {
            using (MainContext context = new MainContext())
            {
                context.Cars.Add(MainMappers.Instance.Map<Car>(car));
                context.SaveChanges();
            }
        }

        public static void RemoveCar(int id)
        {
            using (MainContext context = new MainContext())
            {
                var car = context.Cars.Include(c => c.Passengers).SingleOrDefault(c => c.CarID == id);
                foreach (var passenger in car.Passengers)
                {
                    passenger.CarID = null;
                }
                context.Cars.Remove(car);
                context.SaveChanges();
            }
        }
    }
}
