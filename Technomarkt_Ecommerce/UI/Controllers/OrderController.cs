using Common;
using DAL.Context;
using DAL.Entity;
using Service.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UI.CustomFilters;

namespace UI.Controllers
{
    [AuthFilter]
    public class OrderController : Controller
    {
        //todo: Using db here goes against the architecture of the project, this is only temporary for testing search functionality!
        ApplicationContext db = new ApplicationContext();

        OrderService orderService = new OrderService();
        OrderDetailService orderDetailService = new OrderDetailService();
        AppUserService appUserService = new AppUserService();

        //List orders that belongs to the current user
        public ActionResult Index()
        {
            var currentUserId = appUserService.GetDefault(x => x.Username == System.Web.HttpContext.Current.User.Identity.Name).FirstOrDefault().ID;
            var userOrders = orderService.GetDefault(x => x.AppUserID == currentUserId).ToList();

            return View(userOrders);
        }

        [HttpGet]
        public ActionResult Index(string search)
        {
            ViewData["SearchResults"] = search;
            var currentUserId = appUserService.GetDefault(x => x.Username == System.Web.HttpContext.Current.User.Identity.Name).FirstOrDefault().ID;

            var result = from order in db.Orders where order.AppUserID == currentUserId select order;
            if (!String.IsNullOrEmpty(search))
            {
                result = result.Where(x => x.OrderDate.Contains(search) || x.OrderNo.ToString().Contains(search));
            }
            return View(result.ToList());
        }

        public ActionResult OrderDetails(Guid id)
        {
            var order = orderDetailService.GetDefault(x => x.OrderId == id);
            return View(order);
        }

        public ActionResult Cancel(Guid id)
        {
            try
            {
                var currentUserEmail = appUserService.GetDefault(x => x.Username == System.Web.HttpContext.Current.User.Identity.Name).FirstOrDefault().Email;
                var currentUserPhone = appUserService.GetDefault(x => x.Username == System.Web.HttpContext.Current.User.Identity.Name).FirstOrDefault().PhoneNumber;

                string content = $"Your order has been cancelled!";
                MailSender.SendEmail(currentUserEmail, "Order Cancelled", content);
                SmsSender.SendSms(content, currentUserPhone);

                TempData["info"] = orderService.CancelOrder(id);
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                TempData["error"] = ex.Message;
                return RedirectToAction("Index");
            }
        }

        public ActionResult Refund(Guid id)
        {
            try
            {
                var currentUserEmail = appUserService.GetDefault(x => x.Username == System.Web.HttpContext.Current.User.Identity.Name).FirstOrDefault().Email;
                var currentUserPhone = appUserService.GetDefault(x => x.Username == System.Web.HttpContext.Current.User.Identity.Name).FirstOrDefault().PhoneNumber;

                string content = $"Your order has been refunded!";
                MailSender.SendEmail(currentUserEmail, "Order Refunded", content);
                SmsSender.SendSms(content, currentUserPhone);

                TempData["info"] = orderService.RefundOrder(id);
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                TempData["error"] = ex.Message;
                return RedirectToAction("Index");
            }
        }
    }
}