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
    public class SubCategoryController : Controller
    {
        
        SubCategoryService subCategoryService = new SubCategoryService();
        CategoryService categoryService = new CategoryService();

        public ActionResult Index()
        {
            return View(subCategoryService.GetList());
        }

        public ActionResult Create()
        {
            ViewBag.Categories = categoryService.GetList();
            return View();
        }

        [HttpPost]
        public ActionResult Create(SubCategory subCategory)
        {
            try
            {
                string result = subCategoryService.Add(subCategory);
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
            ViewBag.Categories = categoryService.GetList();
            try
            {
                SubCategory updated = subCategoryService.GetById(id);
                return View(updated);
            }
            catch (Exception ex)
            {
                TempData["error"] = ex.Message;
                return View();
            }
        }

        [HttpPost]
        public ActionResult Update(SubCategory updated)
        {
            try
            {
                string result = subCategoryService.Update(updated);
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
            var deleted = subCategoryService.GetById(id);

            try
            {
                TempData["info"] = subCategoryService.Delete(deleted);
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
