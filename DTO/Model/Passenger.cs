using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.Model
{
    public class Passenger
    {
        public int PassengerID { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Gender { get; set; }
        [Required]
        public DateTime Birthdate { get; set; }
        public int? CarID { get; set; }
        public Car Car { get; set; }
        [Required]
        public int FerryID { get; set; }
        public Ferry Ferry { get; set; }
        public Passenger() { }

        public override string ToString()
        {
            return Name + ", " + Gender + ", " + Birthdate.ToString("d");
        }
    }
}
