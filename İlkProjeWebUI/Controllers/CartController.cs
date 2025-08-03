using İlkProjeWebUI.Entity;
using İlkProjeWebUI.Models;
using Microsoft.Owin.Security.Provider;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using System.Web.UI.WebControls;

namespace İlkProjeWebUI.Controllers
{
    public class CartController : Controller
    {
        private DataContext db = new DataContext();

        // GET: /Cart
        public ActionResult Index()
        {
            return View(GetCart());
        }

        public ActionResult AddToCart(int Id)
        {
            var product = db.Products.FirstOrDefault(i => i.Id == Id);
            if (product != null)
            {
                GetCart().AddProduct(product, 1);
            }
            return RedirectToAction("Index");

        }

        public ActionResult RemoveFromCart(int Id)
        {
            var product = db.Products.FirstOrDefault(i => i.Id == Id);
            if (product != null)
            {
                GetCart().DeleteProduct(product);
            }
            return RedirectToAction("Index");

        }
        public Cart GetCart()
        {
            var cart = (Cart)(Session["Cart"]);
            if (cart == null)
            {
                cart = new Cart();
                Session["Cart"] = cart;
            }
            return cart;
        }
        public PartialViewResult Summary()
        {
            var count = GetCart().CartLines.Sum(l => l.Quantity);
            ViewBag.CartCount = count;
            return PartialView("_CartSummary");
        }
       
        [Authorize]
        public ActionResult Checkout()
        {

            return View(new ShippingDetails());
        }
        [HttpPost]
        public ActionResult Checkout(ShippingDetails entity)
        {
            var cart = GetCart();

            if (cart.CartLines.Count == 0)
            {
                ModelState.AddModelError("","Sepetinizde ürün bulunmamaktadır.");
            }
            else
            {
                if (ModelState.IsValid)
                {

                    SaveOrder(cart,entity);

                    cart.Clear();

                    return View("Completed");
                }
                return View(entity);
            }

            return View();
        }

        private void SaveOrder(Cart cart, ShippingDetails entity)
        {
            var order= new Order();

            order.OrderNumber = "S"+(new Random()).Next(11111,99999).ToString();
            order.Total = (decimal)cart.Total();
            order.Date = DateTime.Now;
            order.OrderState=EnumOrderState.Waiting;
            order.Username=User.Identity.Name;

            
            order.Adres = entity.Adres;
            order.sehir = entity.sehir;
            order.Semt = entity.Semt;
            order.Mahalle = entity.Mahalle;
            order.AdresTarif = entity.AdresTarif;

            order.OrderLine = new List<OrderLine>();

            foreach (var pr in cart.CartLines)
            {
                OrderLine orderline=new OrderLine();
                orderline.Quantity = pr.Quantity;
                orderline.Price = (pr.Quantity*pr.Product.Price);
                orderline.ProductId = pr.Product.Id;

                order.OrderLine.Add(orderline);
            }
            db.Orders.Add(order);
            db.SaveChanges();
        }
    }
}


    

