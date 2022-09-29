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
    public class BrandController : Controller
    {

        BrandService brandService = new BrandService();

        public ActionResult Index()
        {
            return View(brandService.GetList());
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(Brand brand)
        {
            try
            {
                string result = brandService.Add(brand);
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
                Brand updated = brandService.GetById(id);
                return View(updated);
            }
            catch (Exception ex)
            {
                TempData["error"] = ex.Message;
                return View();
            }
        }

        [HttpPost]
        public ActionResult Update(Brand updated)
        {
            try
            {
                string result = brandService.Update(updated);
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
            var deleted = brandService.GetById(id);

            try
            {
                TempData["info"] = brandService.Delete(deleted);
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