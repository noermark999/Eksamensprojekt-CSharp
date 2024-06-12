using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.Model
{
    public class Car
    {
        public int CarID { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Numberplate { get; set; }
        public virtual List<Passenger> Passengers { get; set; }
        [Required]
        public int FerryID { get; set; }
        public Ferry Ferry { get; set; }

        public Car()
        {
            Passengers = new List<Passenger>();
        }

        public override string ToString()
        {
            return Name + ", " + Numberplate;
        }
    }
}
