using Core.Map;
using DAL.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Map
{
    public class SupplierOrderDetailMap : CoreMap<SupplierOrderDetail>
    {
        public SupplierOrderDetailMap()
        {
            ToTable("dbo.SupplierOrderDetails");
            Property(x => x.ProductImage).HasMaxLength(255).IsRequired();
            Property(x => x.ProductName).HasMaxLength(255).IsRequired();
            Property(x => x.UnitPrice).IsRequired();
            Property(x => x.Quantity).IsRequired();
            Property(x => x.SubTotal).IsRequired();
        }
    }
}
