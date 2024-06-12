using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Context
{
    internal class MainInitializer : CreateDatabaseIfNotExists<MainContext>
    {
        protected override void Seed(MainContext context)
        {
            Ferry f1 = new Ferry { Name = "Magnolia", MaxCars = 20, MaxPassengers = 100, PriceCar = 197, PricePassenger = 99 };
            Ferry f2 = new Ferry { Name = "Titanic", MaxCars = 40, MaxPassengers = 200, PriceCar = 197, PricePassenger = 99 };

            Car car = new Car { Name = "Opel", Numberplate = "AB73857" };

            Passenger passenger = new Passenger { Name = "Jakob", Gender = "Mand", Birthdate = new DateTime(2001, 3, 24) };

            car.Passengers.Add(passenger);
            f1.Passengers.Add(passenger);
            f1.Cars.Add(car);

            context.Passengers.Add(passenger);
            context.Cars.Add(car);
            context.Ferrys.Add(f1);
            context.Ferrys.Add(f2);
            context.SaveChanges();
        }
        private void dummy()
        {
            string result = System.Data.Entity.SqlServer.SqlFunctions.Char(65);
        }
    }
}
