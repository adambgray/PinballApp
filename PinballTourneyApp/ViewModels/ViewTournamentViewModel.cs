using PinballTourneyApp.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using PinballTourneyApp.Data;

namespace PinballTourneyApp.ViewModels
{
    public class ViewTournamentViewModel
    {
        public Tournament Tournament { get; set; }
        public IList<TournamentUser> Players { get; set; }
        public int PlayerID { get; set; }

        public ViewTournamentViewModel()
            {

            }
    }


}