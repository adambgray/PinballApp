using Microsoft.AspNetCore.Mvc;
using PinballTourneyApp.Models;
using System.Collections.Generic;
using PinballTourneyApp.ViewModels;
using PinballTourneyApp.Data;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;

namespace PinballTourneyApp.Controllers
{
    public class UserController : Controller
    {
        private readonly PinballDbContext context;

        public UserController(PinballDbContext dbContext)
        {
            context = dbContext;
        }

        public IActionResult Index()
        {
            IList<User> users = context.Users.ToList();
            ViewBag.Name = HttpContext.Session.GetString(HomeController.SessionName);
            ViewBag.ID = HttpContext.Session.GetInt32(HomeController.SessionID);
            return View(users);
        }

        public IActionResult Add()
        {
            AddUserViewModel addUserViewModel = new AddUserViewModel();
            return View(addUserViewModel);
        }

        public IActionResult ViewUser(int id)
        {
            User viewUser = context.Users.Single(u => u.ID == id);

            List<TournamentUser> tournaments = context
            .TournamentUsers
            .Include(tournament => tournament.Tournament)
            .Where(tu => tu.UserID == id)
            .ToList();

            ViewBag.Name = HttpContext.Session.GetString(HomeController.SessionName);
            ViewBag.ID = HttpContext.Session.GetInt32(HomeController.SessionID);

            ViewUserViewModel viewUserViewModel = new ViewUserViewModel()
            {
                User = viewUser,
                Tournaments = tournaments,

            };
            return View(viewUserViewModel);
        }

        [HttpPost]
        public IActionResult Add(AddUserViewModel addUserViewModel)
        {
            if (addUserViewModel.Confirm != addUserViewModel.Password)
            {
                ModelState.AddModelError("Confirm", "Password and confirmation must match.");
            }

            HashSet<char> specialCharacters = new HashSet<char>() { '%', '$', '#', '!' };

            if (addUserViewModel.Password.Any(char.IsLower) && //Lower case 
                 addUserViewModel.Password.Any(char.IsUpper) &&
                 addUserViewModel.Password.Any(char.IsDigit) &&
                 addUserViewModel.Password.Any(specialCharacters.Contains))
            {
                
            }
            else
            {
                ModelState.AddModelError("Password", "Password must contain at least one of each(Uppercase, Lowercase, Integer, and Special Character.");
            }

            if (ModelState.IsValid)
                {
                User newUser = new User
                {
                    Name = addUserViewModel.Name,
                    Email = addUserViewModel.Email,
                    Password = addUserViewModel.Password,
                };

                context.Users.Add(newUser);
                context.SaveChanges();
                HttpContext.Session.Clear();
                HttpContext.Session.SetString("_Email", addUserViewModel.Email); 
                HttpContext.Session.SetString("_Name", addUserViewModel.Name);


                return Redirect("/User");
            }
            return View(addUserViewModel);
        }

    }
}