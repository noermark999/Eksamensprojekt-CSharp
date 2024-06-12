using BLL;
using DTO.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVC.Controllers
{
    public class PassengerController : Controller
    {
        FerryBLL ferryBLL = new FerryBLL();
        // GET: Passenger
        public ActionResult Index(int id)
        {
            ViewBag.ID = id;
            return View("Index");
        }

        [HttpPost]
        public ActionResult AddPassengerFerry(Passenger passenger, int FerryID) 
        {
            if (ModelState.IsValid)
            {
                Ferry ferry = ferryBLL.GetFerry(FerryID);
                ferryBLL.AddPassengerToFerry(passenger, ferry);
                return RedirectToAction("Index", "Ferry");
            }
            else
            {
                return Index(FerryID);
            }
        }
    }
}