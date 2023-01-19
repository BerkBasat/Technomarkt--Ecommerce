using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DAL.Entity;
using Service.Concrete;
using UI.CustomFilters;
using UI.Models;
using Common;
using System.Web.Razor.Generator;

namespace UI.Controllers
{
    public class CartController : Controller
    {

        ProductService productService = new ProductService();
        OrderService orderService = new OrderService();
        OrderDetailService orderDetailService = new OrderDetailService();
        AppUserCardService appUserCardService = new AppUserCardService();
        AppUserService appUserService = new AppUserService();

        public ActionResult Index()
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

        [AuthFilter]
        public ActionResult BillingDetails()
        {
            return View();
        }

        [AuthFilter]
        public ActionResult Checkout()
        {
            var currentUserId = appUserService.GetDefault(x => x.Username == System.Web.HttpContext.Current.User.Identity.Name).FirstOrDefault().ID;
            var userCards = appUserCardService.GetDefault(x => x.AppUserId == currentUserId).ToList();
            return View(userCards);
        }

        #region Add, delete, update user credit card
        [AuthFilter]
        public ActionResult SaveCard()
        {
            return View();
        }

        [AuthFilter]
        [HttpPost]
        public ActionResult SaveCard(AppUserCard appUserCard)
        {
            try
            {
                appUserCard.AppUserId = appUserService.GetDefault(x => x.Username == System.Web.HttpContext.Current.User.Identity.Name).FirstOrDefault().ID;
                string result = appUserCardService.Add(appUserCard);
                TempData["info"] = result;
                return RedirectToAction("Checkout");
            }
            catch (Exception ex)
            {
                TempData["error"] = ex.Message;
            }
            return View();
        }

        [AuthFilter]
        public ActionResult UpdateCard(Guid id)
        {
            try
            {
                AppUserCard updated = appUserCardService.GetById(id);
                return View(updated);
            }
            catch (Exception ex)
            {
                TempData["error"] = ex.Message;
            }

            return View();
        }

        [AuthFilter]
        [HttpPost]
        public ActionResult UpdateCard(AppUserCard updated)
        {
            try
            {
                string result = appUserCardService.Update(updated);
                TempData["info"] = result;
                return RedirectToAction("Checkout");
            }
            catch (Exception ex)
            {
                TempData["error"] = ex.Message;
            }

            return View();
        }


        [AuthFilter]
        public ActionResult DeleteCard(Guid id)
        {
            var deleted = appUserCardService.GetById(id);

            try
            {
                TempData["info"] = appUserCardService.Delete(deleted);
                return RedirectToAction("Checkout");
            }
            catch (Exception ex)
            {
                TempData["error"] = ex.Message;
                return RedirectToAction("Checkout");
            }
        }
        #endregion

        static List<OrderDetail> orderDetailList = new List<OrderDetail>();

        // Complete order
        [AuthFilter]
        public ActionResult OrderComplete()
        {

            CompleteOrder();

            return View(orderDetailList);
        }

        private void CompleteOrder ()
        {
            //Create sessions for cart and user

            Cart cart = Session["cart"] as Cart;
            AppUser user = Session["login"] as AppUser;

            // Check if the cart has items in it
            if (cart != null)
            {

                Random rnd = new Random();
                string productList = ""; // Empty product list will be filled with card items and used while sending email and sms to user

                Order order = new Order();
                order.AppUserID = user.ID;
                order.OrderNo = rnd.Next(1000, 100000); //generate random order number
                Order result = orderService.Add(order);

                //Add up all the items in the cart to total
                foreach (var item in cart.myCart)
                {

                    OrderDetail orderDetail = new OrderDetail();
                    orderDetail.OrderId = result.ID;
                    orderDetail.ProductId = item.Id;
                    orderDetail.ProductImage = item.ProductImage;
                    orderDetail.ProductName = item.ProductName;
                    orderDetail.UnitPrice = (decimal)item.Price;
                    orderDetail.Quantity = item.Quantity;
                    orderDetail.SubTotal = (decimal)item.SubTotal;

                    //Add cart items to order detail table
                    orderDetailList.Add(orderDetail);
                    orderDetailService.Add(orderDetail);

                    // Decrease stock amount of all products in the cart
                    Product product = productService.GetById(item.Id);
                    product.UnitsInStock -= Convert.ToInt16(item.Quantity);

                    productList = $"- {item.ProductName}";
                }

                // Sending email and sms to user with order details
                //string content = $"We have received your order. Order No: {order.OrderNo}\nYour order:\n{productList}";
                //MailSender.SendEmail(user.Email, "Order Info", content);
                //SmsSender.SendSms($"We have received your order. Order No: {order.OrderNo}\nYour order:\n{productList}", user.PhoneNumber);

                // Remove cart session once the purchase is complete
                Session.Remove("cart");

                //Get the order to display it in Order Complete view
                orderDetailList = orderDetailService.GetDefault(x => x.OrderId == result.ID);

            }
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
    }
}