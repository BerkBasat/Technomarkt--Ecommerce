using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace UI.Models
{
    public class ContactVM
    {
        [Required(ErrorMessage = "Name is Required!")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Email is Required!")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Subject is Required!")]
        public string Subject { get; set; }

        [Required(ErrorMessage ="Message is Required!")]
        public string Message { get; set; }
    }
}