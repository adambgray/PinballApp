using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PinballTourneyApp.ViewModels
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "Must enter Email.")]
        [Display(Name = "Email")]
        public string Email { get; set; }


        [Required(ErrorMessage = "Must enter Password.")]
        [Display(Name = "Password")]
        public string Password { get; set; }

        public LoginViewModel()
        {

        }
    }
}