using DAL.Entity;
using Service.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UI.CustomFilters;
using UI.Models;

namespace UI.Areas.Admin.Controllers
{
    [Authorize(Roles = "admin")]
    [AuthFilter]
    public class SupplierOrderController : Controller
    {
        SupplierOrderService supplierOrderService = new SupplierOrderService();
        SupplierOrderDetailService supplierOrderDetailService = new SupplierOrderDetailService();

        public ActionResult Index()
        {
            return View(supplierOrderService.GetList());
        }
    }
}