using DAL.Context;
using Service.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UI.CustomFilters;

namespace UI.Areas.Depot.Controllers
{
    [Authorize(Roles = "depot")]
    [AuthFilter]
    public class HomeController : Controller
    {
        ApplicationContext db = new ApplicationContext();
        OrderService orderService = new OrderService();
        OrderDetailService orderDetailService = new OrderDetailService();

        // GET: Depot/Home
        public ActionResult Index()
        {
            return View(orderService.GetList());
        }

        [HttpGet]
        public ActionResult Index(string search)
        {
            ViewData["SearchResults"] = search;

            var result = from order in db.Orders select order;
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

        public ActionResult SendToCargo(Guid id)
        {
            try
            {
                TempData["info"] = orderService.ShipOrder(id);
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                TempData["error"] = ex.Message;
                return RedirectToAction("Index"); 
            }
        }

        public ActionResult Deliver(Guid id)
        {
            try
            {
                TempData["info"] = orderService.DeliverOrder(id);
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