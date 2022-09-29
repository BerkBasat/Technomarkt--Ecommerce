
using DAL.Context;
using DAL.Entity;
using Service.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UI.CustomFilters;
using UI.Models;
using UI.Utils;

namespace UI.Areas.Admin.Controllers
{
    [Authorize(Roles = "admin")]
    [AuthFilter]
    public class HomeController : Controller
    {
        ApplicationContext db = new ApplicationContext();
        ProductService productService = new ProductService();
        SubCategoryService subCategoryService = new SubCategoryService();
        BrandService brandService = new BrandService();
        SupplierService supplierService = new SupplierService();
        SupplierOrderService supplierOrderService = new SupplierOrderService();
        SupplierOrderDetailService supplierOrderDetailService = new SupplierOrderDetailService();

        public ActionResult Index()
        {
            return View(productService.GetList());
        }

        [HttpGet]
        public ActionResult Index(string search)
        {
            ViewData["SearchResults"] = search;

            var result = from product in db.Products select product;
            if (!String.IsNullOrEmpty(search))
            {
                result = result.Where(x => x.ProductName.Contains(search));
            }
            return View(result.ToList());
        }

        public ActionResult Create()
        {
            ViewBag.SubCategories = subCategoryService.GetList();
            ViewBag.Brands = brandService.GetList();
            ViewBag.Suppliers = supplierService.GetList();
            return View();
        }

        [HttpPost]
        public ActionResult Create(Product product, HttpPostedFileBase fileImage)
        {
            try
            {
                product.ProductImagePath = ImageUploader.UploadImage("~/Content/img/", fileImage);
                string result = productService.Add(product);
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
            ViewBag.SubCategories = subCategoryService.GetList();
            ViewBag.Brands = brandService.GetList();
            ViewBag.Suppliers = supplierService.GetList();

            try
            {
                Product updated = productService.GetById(id);
                return View(updated);
            }
            catch (Exception ex)
            {
                TempData["error"] = ex.Message;
                return View();
            }
        }

        [HttpPost]
        public ActionResult Update(Product updated)
        {
            try
            {
                string result = productService.Update(updated);
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
            var deleted = productService.GetById(id);

            try
            {
                TempData["info"] = productService.Delete(deleted);
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                TempData["error"] = ex.Message;
                return RedirectToAction("Index");
            }
        }

        public ActionResult Activate(Guid id)
        {
            try
            {
                TempData["info"] = productService.Activate(id);
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                TempData["error"] = ex.Message;
                return RedirectToAction("Index");
            }
        }

        public ActionResult Remove(Guid id)
        {
            try
            {
                TempData["info"] = productService.Remove(id);
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                TempData["error"] = ex.Message;
                return RedirectToAction("Index");
            }
        }

        public ActionResult Discontinue(Guid id)
        {
            try
            {
                TempData["info"] = productService.Discontinue(id);
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                TempData["error"] = ex.Message;
                return RedirectToAction("Index");
            }
        }


        #region This area is for ordering products from supplier to restock!
        public ActionResult AddToCart(Guid id)
        {
            try
            {
                Product product = productService.GetById(id);
                Cart cart = null;

                if (Session["cart"] == null)
                {
                    cart = new Cart();
                }
                else
                {
                    cart = Session["cart"] as Cart;
                }

                CartItem cartItem = new CartItem();
                cartItem.Id = product.ID;
                cartItem.ProductImage = product.ProductImagePath;
                cartItem.ProductName = product.ProductName;
                cartItem.Price = product.SupplyCost;
                cart.AddItem(cartItem);
                Session["cart"] = cart;

                return RedirectToAction("Index");
            }
            catch (Exception)
            {
                TempData["error"] = $"Could not find a product with id no:{id}";
                return View();
            }
        }

        public ActionResult AdminCart()
        {
            if (Session["cart"] != null)
            {
                return View();
            }
            else
            {
                TempData["error"] = "Your cart is empty!";
                return RedirectToAction("Index");
            }
        }


        public ActionResult BillingDetails()
        {
            return View();
        }


        public ActionResult Checkout()
        {
            return View();
        }



        public ActionResult OrderComplete()
        {
            Cart cart = Session["cart"] as Cart;
            AppUser user = Session["login"] as AppUser;

            List<SupplierOrderDetail> supplierOrderDetails = new List<SupplierOrderDetail>();


            if (cart != null)
            {

                Random rnd = new Random();
                string productList = "";

                SupplierOrder supplierOrder = new SupplierOrder();
                supplierOrder.OrderNo = rnd.Next(1000, 100000);
                SupplierOrder result = supplierOrderService.Add(supplierOrder);

                foreach (var item in cart.myCart)
                {

                    SupplierOrderDetail supplierOrderDetail = new SupplierOrderDetail();
                    supplierOrderDetail.SupplierOrderId = result.ID;
                    supplierOrderDetail.ProductId = item.Id;
                    supplierOrderDetail.ProductImage = item.ProductImage;
                    supplierOrderDetail.ProductName = item.ProductName;
                    supplierOrderDetail.UnitPrice = (decimal)item.Price;
                    supplierOrderDetail.Quantity = item.Quantity;
                    supplierOrderDetail.SubTotal = (decimal)item.SubTotal;

                    supplierOrderDetails.Add(supplierOrderDetail);
                    supplierOrderDetailService.Add(supplierOrderDetail);

                    Product product = productService.GetById(item.Id);
                    product.UnitsInStock += Convert.ToInt16(item.Quantity);

                    productList = $"Product: {item.ProductName} - Price: {item.Price} - Total: {item.SubTotal}";
                }


                Session.Remove("cart");

                supplierOrderDetails = supplierOrderDetailService.GetDefault(x => x.SupplierOrderId == result.ID);

            }

            return View(supplierOrderDetails);
        }

        public ActionResult DeleteCartItem(Guid id)
        {
            Cart cart = Session["cart"] as Cart;

            if (cart != null)
            {
                cart.DeleteItem(id);
            }

            return RedirectToAction("Index");
        }
        #endregion
    }
}