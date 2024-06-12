using BLL;
using DTO.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;

namespace MVC.Controllers
{
    public class FerryController : Controller
    {
        FerryBLL ferryBLL = new FerryBLL();
        PassengerBLL passengerBLL = new PassengerBLL();
        CarBLL carBLL = new CarBLL();
        // GET: Ferry
        public ActionResult Index()
        {
            ViewBag.Ferrys = ferryBLL.GetFerryList();
            return View();
        }

        public ActionResult FerryDetails(int FerryID) 
        {
            Ferry ferry = ferryBLL.GetFerry(FerryID);
            ViewBag.FerryPrice = ferryBLL.GetFerryPrice(ferry);
            return View(ferry); 
        }

        [HttpPost]
        public ActionResult EditFerry(Ferry ferry)
        {
            if (ModelState.IsValid)
            {
                ferryBLL.EditFerry(ferry);
                Ferry newFerry = ferryBLL.GetFerry(ferry.FerryID);
                ViewBag.FerryPrice = ferryBLL.GetFerryPrice(newFerry);
                return View("FerryDetails", newFerry);
            } else
            {
                Ferry oldferry = ferryBLL.GetFerry(ferry.FerryID);
                ViewBag.FerryPrice = ferryBLL.GetFerryPrice(oldferry);
                return View("FerryDetails", oldferry);
            }
        }

        public ActionResult PassengerDelete(int PassengerID)
        {
            Passenger passenger = passengerBLL.GetPassenger(PassengerID);
            passengerBLL.RemovePassenger(PassengerID);
            Ferry ferry = ferryBLL.GetFerry(passenger.FerryID);
            ViewBag.FerryPrice = ferryBLL.GetFerryPrice(ferry);
            return View("FerryDetails", ferry);
        }
        public ActionResult CarDelete(int CarID)
        {
            Car car = carBLL.GetCar(CarID);
            carBLL.RemoveCar(CarID);
            Ferry ferry = ferryBLL.GetFerry(car.FerryID);
            ViewBag.FerryPrice = ferryBLL.GetFerryPrice(ferry);
            return View("FerryDetails", ferry);
        }

    }
}