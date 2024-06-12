using BLL;
using DTO.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace API.Controllers
{
    public class PassengerAPIController : ApiController
    {
        PassengerBLL passengerBLL = new PassengerBLL();
        [HttpGet]
        public Passenger FindPassenger(int id)
        {
            //Her behøver jeg ikke gøre det samme som i FindFerry for api'et da min getpassenger metode ikke har include
            //På passenger objektets ferry og car :)
            return passengerBLL.GetPassenger(id);
        }
        [HttpGet]
        public List<Passenger> GetAllPassengers(int id)
        {
            return passengerBLL.getFerryPassengers(id);
        }

        [HttpPost]
        public Passenger AddPassenger(string name, string gender, string birthdate, int ferryID)
        {
            Passenger passenger = new Passenger { Name = name, Gender = gender, Birthdate = DateTime.Parse(birthdate), FerryID = ferryID };
            passengerBLL.AddPassenger(passenger);
            return passenger;
        }

        [HttpPost]
        public Passenger AddPassenger(string name, string gender, string birthdate, int ferryID, int carID)
        {
            Passenger passenger = new Passenger { Name = name, Gender = gender, Birthdate = DateTime.Parse(birthdate), FerryID = ferryID, CarID = carID };
            passengerBLL.AddPassenger(passenger);
            return passenger;
        }

        [HttpPost]
        public IHttpActionResult PostPassengerObject(Passenger passenger)
        {
            if (!ModelState.IsValid)
                return BadRequest("Not a valid model");
            passengerBLL.AddPassenger(passenger);
            return Ok(passenger);
        }
    }
}
