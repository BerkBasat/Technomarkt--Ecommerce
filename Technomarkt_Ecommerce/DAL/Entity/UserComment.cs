using Core;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Entity
{
    public class UserComment : EntityRepository
    {

        public string Author { get; set; }
        public string Comment { get; set; }

        public virtual List<UserAndComment> UserAndComments { get; set; }
    }
}
