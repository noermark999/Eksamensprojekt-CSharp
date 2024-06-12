using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.Model
{
    public class Ferry
    {
        public int FerryID { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        [Range(10, Double.PositiveInfinity)]
        public int MaxCars { get; set; }
        [Required]
        [Range(40, Double.PositiveInfinity)]
        public int MaxPassengers { get; set; }
        public virtual ICollection<Passenger> Passengers { get; set; }
        public virtual ICollection<Car> Cars { get; set; }
        [Required]
        public int PricePassenger { get; set; }
        [Required]
        public int PriceCar { get; set; }

        public Ferry()
        {
            Passengers = new List<Passenger>();
            Cars = new List<Car>();
        }

        public override string ToString()
        {
            return Name + ", Max Passengers: " + MaxPassengers + ", Passengers onboard: " + Passengers.Count;
        }
    }
}
