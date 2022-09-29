using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UI.Models
{
    public class WishlistItem
    {
        public Guid Id { get; set; }
        public string ProductImage { get; set; }
        public string ProductName { get; set; }
        public string SubCategory { get; set; }
    }
}