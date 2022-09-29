using DAL.Context;
using DAL.Entity;
using Service.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Concrete
{
    public class OrderDetailService : BaseService<OrderDetail>
    {
        ApplicationContext db = new ApplicationContext();

        public decimal? OrderIncome()
        {
            var total = db.OrderDetails.Sum(x => x.SubTotal);
            return total;
        }
    }
}
