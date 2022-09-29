using Core;
using DAL.Enums;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Entity
{
    public class SupplierOrder : EntityRepository
    {
        public SupplierOrder()
        {
            OrderDate = DateTime.Now.ToString("dddd, dd MMMM yyyy", new CultureInfo("en-GB"));
        }

        public int OrderNo { get; set; }
        public string OrderDate { get; set; }

        public virtual List<SupplierOrderDetail> SupplierOrderDetails { get; set; }
    }
}
