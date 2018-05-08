using Microsoft.AspNetCore.Mvc;
using PinballTourneyApp.Models;
using System.Collections.Generic;
using PinballTourneyApp.ViewModels;
using PinballTourneyApp.Data;
using System.Linq;


namespace PinballTourneyApp.Controllers
{
    public class VenueController : Controller
    {
        private readonly PinballDbContext context;

        public VenueController(PinballDbContext dbContext)
        {
            context = dbContext;
        }

        public IActionResult Index()
        {
            List<Venue> venues = context.Venues.ToList();
            return View(venues);
        }

        public IActionResult Add()
        {
            AddVenueViewModel addVenueViewModel = new AddVenueViewModel();
            return View(addVenueViewModel);
        }

        [HttpPost]
        public IActionResult Add(AddVenueViewModel addVenueViewModel)
        {
            if (ModelState.IsValid)
            {
                
                Venue newVenue = new Venue
                {
                    Name = addVenueViewModel.Name,
                    Address = addVenueViewModel.Address,
                };

                context.Venues.Add(newVenue);
                context.SaveChanges();

                return Redirect("/Venue");
            }
            return View(addVenueViewModel);
        }
    }
}