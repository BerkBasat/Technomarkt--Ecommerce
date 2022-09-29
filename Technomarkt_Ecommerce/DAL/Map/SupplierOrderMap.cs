using Core.Map;
using DAL.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Map
{
    public class SupplierOrderMap:CoreMap<SupplierOrder>
    {
        public SupplierOrderMap()
        {
            ToTable("dbo.SupplierOrders");
            Property(x => x.OrderNo).IsRequired();
            Property(x => x.OrderDate).IsRequired();
        }
    }
}
