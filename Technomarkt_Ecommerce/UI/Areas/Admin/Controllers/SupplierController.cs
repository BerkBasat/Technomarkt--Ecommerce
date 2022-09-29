using DAL.Entity;
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
    public class SupplierController : Controller
    {

        SupplierService supplierService = new SupplierService();

        public ActionResult Index()
        {
            return View(supplierService.GetList());
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(Supplier supplier)
        {
            try
            {
                string result = supplierService.Add(supplier);
                TempData["info"] = result;
                return RedirectToAction("Index");

            }
            catch (Exception ex)
            {
                TempData["error"] = ex.Message;
            }

            return View();
        }

        public ActionResult Update(Guid id)
        {
            try
            {
                Supplier updated = supplierService.GetById(id);
                return View(updated);
            }
            catch (Exception ex)
            {
                TempData["error"] = ex.Message;
                return View();
            }
        }

        [HttpPost]
        public ActionResult Update(Supplier updated)
        {
            try
            {
                string result = supplierService.Update(updated);
                TempData["info"] = result;
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                TempData["error"] = ex.Message;
            }
            return View();
        }

        public ActionResult Delete(Guid id)
        {
            var deleted = supplierService.GetById(id);

            try
            {
                TempData["info"] = supplierService.Delete(deleted);
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