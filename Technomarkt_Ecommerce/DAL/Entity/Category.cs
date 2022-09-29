using Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Entity
{
    public class Category : EntityRepository
    {
        public string CategoryName { get; set; }

        public virtual List<SubCategory> SubCategories { get; set; }

    }
}
