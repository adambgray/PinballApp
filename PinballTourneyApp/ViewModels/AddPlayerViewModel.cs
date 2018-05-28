using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;
using PinballTourneyApp.Models;

namespace PinballTourneyApp.ViewModels
{
    public class AddPlayerViewModel
    {
        [Required]
        public int PlayerID { get; set; }

        [Required]
        public int TournamentID { get; set; }

        public AddPlayerViewModel()
            {
            
            }
    }
}
