using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PinballTourneyApp.Models
{
    public class Venue
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public IList<Tournament> Tournaments { get; set; }
    }
}
