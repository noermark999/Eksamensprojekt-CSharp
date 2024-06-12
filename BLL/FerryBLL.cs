using DataAccess.Repositories;
using DTO.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class FerryBLL
    {
        public Ferry GetFerry(int id)
        {
            if (id < 0) throw new IndexOutOfRangeException();
            return FerryRepository.getFerry(id);
        }

        public List<Ferry> GetFerryList()
        {
            return FerryRepository.getFerrys();
        }

        public void EditFerry(Ferry ferry)
        {
            FerryRepository.EditFerry(ferry);
        }

        public void AddPassengerToFerry(Passenger passenger, Ferry ferry)
        {
            FerryRepository.AddPassengerToFerry(passenger, ferry);
        }

        public void AddCarToFerry(Car car, Ferry ferry)
        {
            FerryRepository.AddCarToFerry(car, ferry);
        }

        public int GetFerryPrice(Ferry ferry)
        {
            int price = 0;
            for (int i = 0; i < ferry.Passengers.Count; i++)
            {
                price += ferry.PricePassenger;
            }
            for (int i = 0; i < ferry.Cars.Count; i++)
            {
                price += ferry.PriceCar;
            }
            return price;
        }
    }
}
