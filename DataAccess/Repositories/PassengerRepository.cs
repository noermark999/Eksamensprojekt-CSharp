using DataAccess.Context;
using DataAccess.Mappers;
using DTO.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repositories
{
    public class PassengerRepository
    {
        public static DTO.Model.Passenger getPassenger(int id)
        {
            using (MainContext context = new MainContext())
            {
                return MainMappers.Instance.Map<DTO.Model.Passenger>(context.Passengers.Find(id));
            }
        }
        public static List<DTO.Model.Passenger> getPassengers()
        {
            using (MainContext context = new MainContext())
            {
                List<DTO.Model.Passenger> result = new List<DTO.Model.Passenger>();
                foreach (var Passenger in context.Passengers)
                {
                    result.Add(MainMappers.Instance.Map<DTO.Model.Passenger>(Passenger));
                }
                return result;
            }
        }

        public static void AddPassenger(DTO.Model.Passenger passenger)
        {
            using (MainContext context = new MainContext()) 
            { 
                context.Passengers.Add(MainMappers.Instance.Map<Passenger>(passenger));
                context.SaveChanges();
            }
        }

        public static void RemovePassenger(int id)
        {
            using (MainContext context = new MainContext())
            {
                var passenger = context.Passengers.SingleOrDefault(p => p.PassengerID == id);
                context.Passengers.Remove(passenger);
                context.SaveChanges();
            }
        }
        public static List<DTO.Model.Passenger> getFerryPassengers(int ferryID)
        {
            using (MainContext context = new MainContext())
            {
                var passengers = context.Passengers
                                        .Where(p => p.FerryID == ferryID)
                                        .ToList();

                var result = passengers.Select(p => MainMappers.Instance.Map<DTO.Model.Passenger>(p)).ToList();

                return result;
            }
        }
    }
}
