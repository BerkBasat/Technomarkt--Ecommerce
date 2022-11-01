using DAL.Context;
using DAL.Entity;
using Service.Base;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Concrete
{
    public class OrderService : BaseService<Order>
    {
        ApplicationContext db = new ApplicationContext();

        public Order Add(Order model)
        {
            model.ID = Guid.NewGuid();
            var result = db.Set<Order>().Add(model);
            db.SaveChanges();

            return result;
        }

        //Change order status to Shipping
        public string ShipOrder(Guid id)
        {
            try
            {
                Order shipped = GetById(id);
                shipped.OrderStatus = DAL.Enums.OrderStatus.Shipping;
                Update(shipped);
                return "Order sent to cargo!";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        //Change order status to Delivered
        public string DeliverOrder(Guid id)
        {
            try
            {
                Order delivered = GetById(id);
                delivered.OrderStatus = DAL.Enums.OrderStatus.Delivered;
                delivered.DeliveryDate = DateTime.Now.ToString("dddd, dd MMMM yyyy", new CultureInfo("en-GB"));
                Update(delivered);
                return "Order delivered!";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        //Change order status to Cancelled
        public string CancelOrder(Guid id)
        {
            try
            {
                Order cancelled = GetById(id);
                cancelled.OrderStatus = DAL.Enums.OrderStatus.Cancelled;
                Update(cancelled);
                return "Order cancelled!";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        //Change order status to Refunded
        public string RefundOrder(Guid id)
        {
            try
            {
                Order refunded = GetById(id);
                refunded.OrderStatus = DAL.Enums.OrderStatus.Refunded;
                Update(refunded);
                return "Order refunded!";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
    }
}
