using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PinballTourneyApp.Models
{
    public class Tournament
    {
        public int ID { get; set; }
        public Venue Venue { get; set; }
        public int VenueID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime DateTime { get; set; }
        public IList<TournamentUser> TournamentUser { get; set; }

    }
}
