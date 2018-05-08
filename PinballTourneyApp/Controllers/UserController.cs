using Microsoft.AspNetCore.Mvc;
using PinballTourneyApp.Models;
using System.Collections.Generic;
using PinballTourneyApp.ViewModels;
using PinballTourneyApp.Data;
using System.Linq;
using Microsoft.EntityFrameworkCore;

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
            return View();
        }

        public IActionResult Add()
        {
            AddUserViewModel addUserViewModel = new AddUserViewModel();
            return View(addUserViewModel);
        }

        [HttpPost]
        public IActionResult Add(AddUserViewModel addUserViewModel)
        {
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

                return Redirect("/User");
            }
            return View(addUserViewModel);
        }

    }
}