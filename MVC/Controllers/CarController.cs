using BLL;
using DTO.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVC.Controllers
{
    public class CarController : Controller
    {
        // GET: Car
        FerryBLL ferryBLL = new FerryBLL();
        PassengerBLL passengerBLL = new PassengerBLL();
        private List<Passenger> AddedPassengers
        {
            get
            {
                if (Session["AddedPassengers"] == null)
                {
                    Session["AddedPassengers"] = new List<Passenger>();
                }
                return (List<Passenger>)Session["AddedPassengers"];
            }
            set
            {
                Session["AddedPassengers"] = value;
            }
        }
        public ActionResult Index(int id)
        {
            ViewBag.ID = id;
            ViewBag.Passengers = AddedPassengers;
            ViewBag.AllPassengers = ferryBLL.GetFerry(id).Passengers;
            return View("Index");
        }

        [HttpPost]
        public ActionResult AddCar(Car car, int FerryID)
        {
            if (ModelState.IsValid)
            {
                try
                {


                    Ferry ferry = ferryBLL.GetFerry(FerryID);
                    car.Passengers = AddedPassengers;
                    foreach (var passenger in AddedPassengers)
                    {
                        passengerBLL.RemovePassenger(passenger.PassengerID);
                    }
                    ferryBLL.AddCarToFerry(car, ferry);

                    Session["AddedPassengers"] = null;

                    return RedirectToAction("Index", "Ferry");
                } catch (Exception ex)
                {
                    TempData["alertMessage"] = ex.Message;
                    return Index(FerryID);
                }
            }
            else
            {
                return Index(FerryID);
            }
        }

        public ActionResult AddPassenger(int PassengerID)
        {
            Passenger passenger = passengerBLL.GetPassenger(PassengerID);
            if (AddedPassengers.Count < 5)
            {
                if (!AddedPassengers.Any(p => p.PassengerID == PassengerID))
                {
                    AddedPassengers.Add(passenger);
                }
            } else
            {
                TempData["alertMessage"] = "Can't add more than 5 passengers";
            }
            return Index(passenger.FerryID);
        }
        public ActionResult ClearList(int id)
        {
            Session["AddedPassengers"] = null;
            return Index(id);
        }
    }
}