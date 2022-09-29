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
    public class SupplierOrderDetailService:BaseService<SupplierOrderDetail>
    {
        ApplicationContext db = new ApplicationContext();

        public decimal? TotalExpense()
        {
            var total = db.SupplierOrderDetails.Sum(x => x.SubTotal);
            return total;
        }
    }
}
