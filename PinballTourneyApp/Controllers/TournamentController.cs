using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PinballTourneyApp.Models;
using PinballTourneyApp.ViewModels;
using PinballTourneyApp.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Session;

namespace PinballTourneyApp.Controllers
{
    public class TournamentController : Controller
    {
        private readonly PinballDbContext context;

        public TournamentController(PinballDbContext dbContext)
        {
            context = dbContext;
        }

        public IActionResult Index()
        {
            IList<Tournament> tournaments = context.Tournaments.Include(c => c.Venue).ToList();

            ViewBag.Name = HttpContext.Session.GetString(HomeController.SessionName);
            ViewBag.ID = HttpContext.Session.GetInt32(HomeController.SessionID);

            return View(tournaments);


        }

        public IActionResult Add()
        {
            AddTournamentViewModel addTournamentViewModel = new AddTournamentViewModel(context.Venues.ToList());

            ViewBag.Name = HttpContext.Session.GetString(HomeController.SessionName);
            ViewBag.ID = HttpContext.Session.GetInt32(HomeController.SessionID);

            return View(addTournamentViewModel);
        }

        [HttpPost]
        public IActionResult Add(AddTournamentViewModel addTournamentViewModel)
        {
            ViewBag.Name = HttpContext.Session.GetString(HomeController.SessionName);
            ViewBag.ID = HttpContext.Session.GetInt32(HomeController.SessionID);

            if (ModelState.IsValid)
            {
                Venue newVenue =
                    context.Venues.Single(c => c.ID == addTournamentViewModel.VenueID);

                Tournament newTournament = new Tournament
                {
                    Name = addTournamentViewModel.Name,
                    Description = addTournamentViewModel.Description,
                    Venue = newVenue,
                    DateTime = addTournamentViewModel.DateTime,


                };

                context.Tournaments.Add(newTournament);
                context.SaveChanges();


                return Redirect("/Tournament");
            }
            return View(addTournamentViewModel);
        }

        public IActionResult ViewTournament(int id)
        {   
            Tournament viewTournament = context.Tournaments.Include(v => v.Venue).Single(t => t.ID == id);

            List<TournamentUser> players = context
            .TournamentUsers
            .Include(player => player.User)
            .Where(tu => tu.TournamentID == id)
            .ToList();

            ViewBag.Name = HttpContext.Session.GetString(HomeController.SessionName);
            ViewBag.ID = HttpContext.Session.GetInt32(HomeController.SessionID);

            if (ViewBag.ID != null)
            {

                ViewTournamentViewModel viewTournamentViewModel = new ViewTournamentViewModel()
                {
                    Tournament = viewTournament,
                    Players = players,
                    PlayerID = ViewBag.ID

                };
                return View(viewTournamentViewModel);
            }
            else
            {
                ViewTournamentViewModel viewTournamentViewModel = new ViewTournamentViewModel()
                {
                    Tournament = viewTournament,
                    Players = players,

                };
                return View(viewTournamentViewModel);
            }
        }


        [HttpPost]
        public IActionResult AddPlayer(ViewTournamentViewModel viewTournamentViewModel)
        {

            
            {
                if (ModelState.IsValid)
                {
                    IList<TournamentUser> existingItems = context.TournamentUsers
                    .Where(tu => tu.UserID == viewTournamentViewModel.PlayerID).ToList()
                    .Where(tu => tu.TournamentID == viewTournamentViewModel.Tournament.ID).ToList();

                    if (existingItems.Count() == 0)
                    {
                        TournamentUser newTournamentUser = new TournamentUser
                        {
                            UserID = viewTournamentViewModel.PlayerID,
                            TournamentID = viewTournamentViewModel.Tournament.ID
                        };

                        context.TournamentUsers.Add(newTournamentUser);
                        context.SaveChanges();

                        return Redirect("/Tournament/ViewTournament/" + newTournamentUser.TournamentID);
                    }

                    return Redirect("/Tournament/ViewTournament/" + viewTournamentViewModel.Tournament.ID);
                }
                return View(viewTournamentViewModel);
                ;
            }
        }
    }
}