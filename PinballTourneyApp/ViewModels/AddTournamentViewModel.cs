using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;
using PinballTourneyApp.Models;

namespace PinballTourneyApp.ViewModels
{
    public class AddTournamentViewModel
    {
        [Required]
        [Display(Name = "Tournament Name")]
        public string Name { get; set; }

        [Required]
        [Display(Name = "Description")]
        public string Description { get; set; }

        [Required]
        [Display(Name = "Venue")]
        public int VenueID { get; set; }

        public List<SelectListItem> Venues { get; set; }

        public AddTournamentViewModel(IEnumerable<Venue> venues)
        {
            Venues = new List<SelectListItem>();
            foreach (var venue in venues)
            {
                Venues.Add(new SelectListItem
                {
                    Value = venue.ID.ToString(),
                    Text = venue.Name
                });
            }
        }
    }
}
