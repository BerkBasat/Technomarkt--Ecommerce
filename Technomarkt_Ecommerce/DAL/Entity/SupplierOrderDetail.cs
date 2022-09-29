using Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Entity
{
    public class SupplierOrderDetail : EntityRepository
    {
        public Guid SupplierOrderId { get; set; }
        public Guid ProductId { get; set; }
        public string ProductImage { get; set; }
        public string ProductName { get; set; }
        public decimal UnitPrice { get; set; }
        public int Quantity { get; set; }
        public decimal? SubTotal { get; set; }
        public decimal? Total { get; set; }

        public virtual SupplierOrder SupplierOrder { get; set; }
        public virtual Product Product { get; set; }
    }
}
