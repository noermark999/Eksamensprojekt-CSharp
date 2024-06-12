using DataAccess.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DTO.Model;

namespace BLL
{
    public class PassengerBLL
    {
        public Passenger GetPassenger(int id)
        {
            if (id < 0) throw new IndexOutOfRangeException();
            return PassengerRepository.getPassenger(id);
        }

        public List<Passenger> GetPassengerList()
        {
            return PassengerRepository.getPassengers();
        }

        public void AddPassenger(Passenger passenger)
        {
            PassengerRepository.AddPassenger(passenger);
        }

        public void RemovePassenger(int id)
        {
            PassengerRepository.RemovePassenger(id);
        }

        public List<Passenger> getFerryPassengers(int ferryID)
        {
            return PassengerRepository.getFerryPassengers(ferryID);
        }
    }
}
