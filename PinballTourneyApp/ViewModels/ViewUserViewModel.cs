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
    public class ViewUserViewModel
    {
        public User User { get; set; }
        public IList<TournamentUser> Tournaments { get; set; }
    

        public ViewUserViewModel()
            {

            }
    }


}