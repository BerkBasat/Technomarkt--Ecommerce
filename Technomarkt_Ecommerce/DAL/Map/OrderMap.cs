using Core.Map;
using DAL.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Map
{
    public class OrderMap:CoreMap<Order>
    {
        public OrderMap()
        {
            ToTable("dbo.Orders");
            Property(x => x.OrderNo).IsRequired();
            Property(x => x.OrderDate).IsRequired();
            Property(x => x.DeliveryDate).IsOptional();
        }
    }
}
