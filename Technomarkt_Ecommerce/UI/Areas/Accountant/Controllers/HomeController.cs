using Service.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UI.CustomFilters;

namespace UI.Areas.Accountant.Controllers
{
    [Authorize(Roles = "accountant")]
    [AuthFilter]
    public class HomeController : Controller
    {
        OrderService orderService = new OrderService();
        OrderDetailService orderDetailService = new OrderDetailService();

        // GET: Accountant/Home
        public ActionResult Index()
        {
            ViewBag.OrderSum = orderDetailService.OrderIncome();
            return View(orderService.GetList());
        }
    }
}