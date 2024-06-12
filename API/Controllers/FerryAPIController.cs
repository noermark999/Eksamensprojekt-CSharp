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
    public class FerryAPIController : ApiController
    {
        FerryBLL ferryBLL = new FerryBLL();

        [HttpGet]
        public Ferry FindFerry(int id)
        {
            //Får et uendeligt loop hvis jeg gør det på denne måde da min færge har en liste af passengers
            //og de passengers har en færge som har passengers osv. så bliver nødt til at manuelt lave et objekt
            //og sætte referencerne til null
            //return ferryBLL.GetFerry(id);
            Ferry ferry = ferryBLL.GetFerry(id);
            foreach (var item in ferry.Passengers)
            {
                item.Ferry = null;
            }
            foreach (var item in ferry.Cars)
            {
                item.Ferry = null;
            }
            return ferry;
        }
    }
}
