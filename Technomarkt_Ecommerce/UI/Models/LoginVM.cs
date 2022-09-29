using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace UI.Models
{
    public class LoginVM
    {
        [Required(ErrorMessage = "Username is Required!")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Password is Required!")]
        public string Password { get; set; }
    }
}