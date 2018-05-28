using Microsoft.AspNetCore.Mvc;
using PinballTourneyApp.Models;
using System.Collections.Generic;
using PinballTourneyApp.ViewModels;
using PinballTourneyApp.Data;
using System.Linq;
using Microsoft.AspNetCore.Http;

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
            ViewBag.Name = HttpContext.Session.GetString(HomeController.SessionName);
            ViewBag.ID = HttpContext.Session.GetInt32(HomeController.SessionID);
            List<Venue> venues = context.Venues.ToList();
            return View(venues);
        }

        public IActionResult ViewVenue(int id)
        {
            Venue viewVenue = context.Venues.Single(v => v.ID == id);

            List<Tournament> tournaments = context
            .Tournaments
            .Where(t => t.VenueID == id)
            .ToList();

            ViewBag.Name = HttpContext.Session.GetString(HomeController.SessionName);
            ViewBag.ID = HttpContext.Session.GetInt32(HomeController.SessionID);

            
            ViewVenueViewModel viewVenueViewModel = new ViewVenueViewModel()
            {
                Venue = viewVenue,
                Tournaments = tournaments,

            };
            return View(viewVenueViewModel);
            
        }

        public IActionResult Add()
        {
            ViewBag.Name = HttpContext.Session.GetString(HomeController.SessionName);
            ViewBag.ID = HttpContext.Session.GetInt32(HomeController.SessionID);
            AddVenueViewModel addVenueViewModel = new AddVenueViewModel();
            return View(addVenueViewModel);
        }

        [HttpPost]
        public IActionResult Add(AddVenueViewModel addVenueViewModel)
        {
            ViewBag.Name = HttpContext.Session.GetString(HomeController.SessionName);
            ViewBag.ID = HttpContext.Session.GetInt32(HomeController.SessionID);

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