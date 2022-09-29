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
    public class CategoryController : Controller
    {

        CategoryService categoryService = new CategoryService();

        public ActionResult Index()
        {

            return View(categoryService.GetList());
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(Category category)
        {
            try
            {
                string result = categoryService.Add(category);
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
                Category updated = categoryService.GetById(id);
                return View(updated);
            }
            catch (Exception ex)
            {
                TempData["error"] = ex.Message;
                return View();
            }
        }

        [HttpPost]
        public ActionResult Update(Category updated)
        {
            try
            {
                string result = categoryService.Update(updated);
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
            var deleted = categoryService.GetById(id);

            try
            {
                TempData["info"] = categoryService.Delete(deleted);
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