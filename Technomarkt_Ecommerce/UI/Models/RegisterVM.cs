using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace UI.Models
{
    public class RegisterVM
    {
        [Required(ErrorMessage ="Username is Required!")]
        public string UserName { get; set; }

        [Required(ErrorMessage ="Email is Required!")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Phone Number is Required!")]
        public string PhoneNumber { get; set; }

        [Required(ErrorMessage = "Password is Required!")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Password Confirmation is Required!")]
        [Compare("Password", ErrorMessage ="Passwords don't match!")]
        public string ConfirmPassword { get; set; }
    }
}