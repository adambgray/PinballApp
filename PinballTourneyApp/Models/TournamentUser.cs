using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PinballTourneyApp.Models
{
    public class TournamentUser
    {
        public int TournamentID { get; set; }
        public Tournament Tournament { get; set; }

        public int UserID { get; set; }
        public User User { get; set; }

    }
}
