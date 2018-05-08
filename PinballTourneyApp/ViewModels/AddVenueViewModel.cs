using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace PinballTourneyApp.ViewModels
{
    public class AddVenueViewModel
    {
        [Required]
        [Display(Name = "Venue Name")]
        public string Name { get; set; }

        [Required]
        [Display(Name = "Venue Address")]
        public string Address { get; set; }
    }
}
