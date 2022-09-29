using Service.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UI.CustomFilters;

namespace UI.Areas.Admin.Controllers
{
    [Authorize(Roles = "admin")]
    [AuthFilter]
    public class OrderController : Controller
    {
        OrderService orderService = new OrderService();

        public ActionResult Index()
        {
            return View(orderService.GetList());
        }
    }
}