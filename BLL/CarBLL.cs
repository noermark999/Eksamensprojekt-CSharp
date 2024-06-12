using DataAccess.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DTO.Model;

namespace BLL
{
    public class CarBLL
    {
        public Car GetCar(int id)
        {
            if (id < 0) throw new IndexOutOfRangeException();
            return CarRepository.getCar(id);
        }

        public List<Car> GetCarList()
        {
            return CarRepository.getCars();
        }

        public void AddPassengerToCar(Passenger passenger, Car car)
        {
            CarRepository.AddPassengerToCar(passenger, car);
        }

        public void AddCar(Car car)
        {
            CarRepository.AddCar(car);
        }

        public void RemoveCar(int id)
        {
            CarRepository.RemoveCar(id);
        }
    }
}
