using Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Entity
{
    public class AppUserCard : EntityRepository
    {
        public string CardHolderName { get; set; }
        public string CardNumber { get; set; }
        public string CardType { get; set; }
        public string ExpirationDate { get; set; }
        public string CVC { get; set; }

        public Guid AppUserId { get; set; }
        public virtual AppUser AppUser { get; set; }
    }
}
