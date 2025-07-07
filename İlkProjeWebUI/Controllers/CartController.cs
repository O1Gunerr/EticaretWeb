using İlkProjeWebUI.Entity;
using İlkProjeWebUI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace İlkProjeWebUI.Controllers
{
    public class CartController : Controller
    {
        private readonly DataContext _context = new DataContext();

        // GET: /Cart
        public ActionResult Index()
        {
            var cart = Session["cart"] as List<ProductModel>
                       ?? new List<ProductModel>();

            ViewBag.CouponCode = Session["CouponCode"] as string;
            ViewBag.Discount = Session["CouponDiscount"] as decimal? ?? 0m;

            return View(cart);
        }

        // Sepete ürün ekle
        public ActionResult AddToCart(int id)
        {
            var item = _context.Products
                               .Where(p => p.Id == id)
                               .Select(p => new ProductModel
                               {
                                   Id = p.Id,
                                   Name = p.Name,
                                   Price = p.Price,
                                   Image = p.Image,
                                   Quantity = 1
                               })
                               .FirstOrDefault();

            if (item != null)
            {
                var cart = Session["cart"] as List<ProductModel>
                           ?? new List<ProductModel>();
                var exist = cart.FirstOrDefault(c => c.Id == id);
                if (exist != null) exist.Quantity++;
                else cart.Add(item);

                Session["cart"] = cart;
                Session["CartCount"] = cart.Sum(c => c.Quantity);
            }

            return RedirectToAction("Index", "Home");
        }

        // Sepetteki miktarı güncelle
        [HttpPost]
        public ActionResult UpdateQuantity(int id, int quantity)
        {
            var cart = Session["cart"] as List<ProductModel>;
            if (cart != null)
            {
                var item = cart.FirstOrDefault(c => c.Id == id);
                if (item != null)
                {
                    if (quantity <= 0) cart.Remove(item);
                    else item.Quantity = quantity;

                    Session["cart"] = cart;
                    Session["CartCount"] = cart.Sum(c => c.Quantity);
                }
            }
            return RedirectToAction("Index");
        }

        // Sepetten ürün çıkar
        public ActionResult RemoveFromCart(int id)
        {
            // Mevcut sepeti al, boşsa yeni liste oluştur
            var cart = Session["cart"] as List<ProductModel>
                       ?? new List<ProductModel>();

            // Çıkarılacak ürünü bul
            var item = cart.FirstOrDefault(c => c.Id == id);
            if (item != null)
            {
                cart.Remove(item);

                // Session’a güncellenmiş sepeti ve toplam adet sayısını kaydet
                Session["cart"] = cart;
                Session["CartCount"] = cart.Sum(c => c.Quantity);

                // Eğer sepet boşaldıysa, kupon bilgisini ve indirim tutarını sıfırla
                if (!cart.Any())
                {
                    Session["CouponCode"] = null;
                    Session["CouponDiscount"] = 0m;
                }
            }

            return RedirectToAction("Index");
        }

        // POST: Kupon kodu uygula
        [HttpPost]
        public ActionResult ApplyCoupon(string couponCode)
        {
            if (string.IsNullOrWhiteSpace(couponCode))
            {
                TempData["CouponError"] = "Lütfen bir kupon kodu girin.";
                return RedirectToAction("Index");
            }

            // Süresi dolmamış kuponu ara
            var coupon = _context.Coupons
                                 .FirstOrDefault(c =>
                                     c.Code == couponCode &&
                                     c.ExpirationDate >= DateTime.Now);
            if (coupon == null)
            {
                TempData["CouponError"] = "Kupon bulunamadı veya süresi dolmuş.";
                return RedirectToAction("Index");
            }

            // Sepet toplamını hesapla
            var cart = Session["cart"] as List<ProductModel>
                           ?? new List<ProductModel>();
            var subtotal = cart.Sum(c => c.Price * c.Quantity);

            // İndirimi hesapla: yüzde mi, sabit mi?
            decimal discountAmount = coupon.IsPercentage
                ? subtotal * coupon.DiscountValue / 100m
                : coupon.DiscountValue;

            // Session’a kaydet
            Session["CouponCode"] = couponCode;
            Session["CouponDiscount"] = discountAmount;

            TempData["CouponSuccess"] = $"“{couponCode}” kodu uygulandı, {discountAmount:C2} indirim kazandınız.";
            return RedirectToAction("Index");
        }

        // Kuponu kaldır
        public ActionResult RemoveCoupon()
        {
            Session.Remove("CouponCode");
            Session.Remove("CouponDiscount");
            
            return RedirectToAction("Index");

        }
    }
}
