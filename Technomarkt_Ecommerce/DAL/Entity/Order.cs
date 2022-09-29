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
    public class Order : EntityRepository
    {
        public Order()
        {
            OrderDate = DateTime.Now.ToString("dddd, dd MMMM yyyy", new CultureInfo("en-GB"));
            OrderStatus = OrderStatus.Processing;
        }

        public Guid AppUserID { get; set; }
        public AppUser AppUser { get; set; }
        public int OrderNo { get; set; }
        public OrderStatus OrderStatus { get; set; }
        public string OrderDate { get; set; }
        public string DeliveryDate { get; set; }

        public virtual List<OrderDetail> OrderDetails { get; set; }


    }
}
