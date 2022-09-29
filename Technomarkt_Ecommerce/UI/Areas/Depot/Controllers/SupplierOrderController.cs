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
    public class SupplierOrderController : Controller
    {
        ApplicationContext db = new ApplicationContext();
        SupplierOrderService supplierOrderService = new SupplierOrderService();
        SupplierOrderDetailService supplierOrderDetailService = new SupplierOrderDetailService();

        public ActionResult Index()
        {
            return View(supplierOrderService.GetList());
        }

        public ActionResult OrderDetails(Guid id)
        {
            var order = supplierOrderDetailService.GetDefault(x => x.SupplierOrderId == id);
            return View(order);
        }

        [HttpGet]
        public ActionResult Index(string search)
        {
            ViewData["SearchResults"] = search;

            var result = from order in db.SupplierOrders select order;
            if (!String.IsNullOrEmpty(search))
            {
                result = result.Where(x => x.OrderDate.Contains(search) || x.OrderNo.ToString().Contains(search));
            }
            return View(result.ToList());
        }
    }
}