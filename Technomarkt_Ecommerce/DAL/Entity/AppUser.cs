using Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Entity
{
    public class AppUser : EntityRepository
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Address { get; set; }
        public string Password { get; set; }

        public virtual List<AppUserAndRole> AppUserAndRoles { get; set; }
        public virtual List<AppUserCard> AppUserCards { get; set; }
    }
}
