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
    public class HomeController : Controller
    {
        public const string SessionName = "_Name";
        public const string SessionID = "_ID";

        private readonly PinballDbContext context;

        public HomeController(PinballDbContext dbContext)
        {
            context = dbContext;
        }

        public IActionResult Index()
        {
            ViewBag.Name = HttpContext.Session.GetString(HomeController.SessionName);
            ViewBag.ID = HttpContext.Session.GetInt32(HomeController.SessionID);
            return View();
        }

        public IActionResult Login()
        {
            LoginViewModel loginViewModel = new LoginViewModel();
            return View(loginViewModel);
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return Redirect("/");
        }

        [HttpPost]
        public IActionResult Login(LoginViewModel loginViewModel)
        {
            if (ModelState.IsValid)
            {
                string email = loginViewModel.Email;
                string password = loginViewModel.Password;
                
                
                User getUser = context.Users.Single(u => ((u.Email == email) && (u.Password == password)));
                if (getUser != null)
                {
                    string name = getUser.Name;
                    int id = getUser.ID;
                    HttpContext.Session.Clear();
                    HttpContext.Session.SetInt32(SessionID, id); 
                    HttpContext.Session.SetString(SessionName, name);
                    return Redirect("/Tournament");   
                }
                ViewBag.ErrorMessage = "Invalid User Name and/or Password ";
                return View();
            }
            return View(loginViewModel);
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
