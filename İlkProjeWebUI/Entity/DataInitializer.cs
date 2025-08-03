using System;
using System.Collections.Generic;
using System.Data.Entity;

namespace İlkProjeWebUI.Entity
{
    public class DataInitializer : DropCreateDatabaseIfModelChanges<DataContext>
    {
        protected override void Seed(DataContext context)
        {
            var kategoriler = new List<Category>()
            {
                new Category { Name = "Telefon", Description = "Telefon Ürünleri" },
                new Category { Name = "Bilgisayar", Description = "Bilgisayar Ürünleri" },
                new Category { Name = "Elektronik", Description = "Elektronik Ürünleri" },
                new Category { Name = "Televizyon", Description = "Televizyon Ürünleri" },
                new Category { Name = "Beyaz Eşya", Description = "Beyaz Eşya Ürünleri" }
            };

            foreach (var kategori in kategoriler)
            {
                context.Categories.Add(kategori);
            }
            

            var products = new List<Product>()
            {
                new Product { Name = "Galaxy S24", Description = "Amiral gemisi performans, zarif tasarım.", Price = 22000, Stock = 20, IsApproved = true, IsHome = true, CategoryId = 1, Image = "1.jpg" },
                new Product { Name = "Galaxy S25 Ultra", Description = "S Pen destekli, 200MP kamera ile profesyonel fotoğraflar.", Price = 19000 , Stock = 15, IsApproved = true, IsHome = true, CategoryId = 1, Image = "2.jpg" },
                new Product { Name = "Samsung OLED TV 65”", Description = "Gerçek siyahlar ve 4K çözünürlükte yüksek kalite.", Price = 35000 , Stock = 10, IsApproved = true, IsHome = true, CategoryId = 4, Image = "3.jpg" },
                new Product { Name = "Galaxy Watch 6 Classic", Description = "Sağlık takibi, kondisyon ölçümü ve uzun pil ömrü.", Price = 3200, Stock = 25, IsApproved = true, IsHome = false, CategoryId = 3, Image = "4.jpg" },
                new Product { Name = "Galaxy Buds 2 Pro", Description = "Aktif gürültü engelleme ve yüksek ses kalitesi.", Price = 1800, Stock = 50, IsApproved = true, IsHome = false, CategoryId = 3, Image = "5.jpg" },
                new Product { Name = "Samsung Inox Buzdolabı", Description = "Geniş iç hacim, akıllı soğutma ve enerji verimliliği.", Price = 40000, Stock = 12, IsApproved = true, IsHome = true, CategoryId = 5, Image = "6.jpg" },
                new Product { Name = "Galaxy Z Flip 5", Description = "Katlanabilir ekranıyla taşınabilirliği yeniden tanımlar.", Price = 30000, Stock = 20, IsApproved = true, IsHome = true, CategoryId = 1, Image = "7.jpg" },
                new Product { Name = "Galaxy A15", Description = "Uygun fiyatlı, geniş ekranlı günlük akıllı telefon.", Price = 3490, Stock = 35, IsApproved = true, IsHome = true, CategoryId = 1, Image = "8.jpg" },
                new Product { Name = "Samsung Çamaşır Makinesi", Description = "AI teknolojisiyle akıllı ve sessiz yıkama deneyimi.", Price = 29000, Stock = 18, IsApproved = true, IsHome = false, CategoryId = 5, Image = "9.jpg" }
                

            };

            foreach (var urun in products)
            {
                context.Products.Add(urun);
            }
            var coupon = new List<Coupon>()
            {
                new Coupon { Code = "S1234S", Id = 1, IsPercentage = true, DiscountValue =25 , ExpirationDate = DateTime.Now.AddDays(30) },
                 new Coupon { Code = "S2345S", Id = 1, IsPercentage = true, DiscountValue =15 , ExpirationDate = DateTime.Now.AddDays(40) },
                 new Coupon { Code = "S3456S", Id = 1, IsPercentage = true, DiscountValue =25 , ExpirationDate = DateTime.Now.AddDays(60) },
                 new Coupon { Code = "S4567S", Id = 1, IsPercentage = true, DiscountValue =30 , ExpirationDate = DateTime.Now.AddDays(50) },
            };

            foreach (var items in coupon)
            {
                context.Coupons.Add(items);
            }

            context.SaveChanges();
            base.Seed(context);
        }
    }
}
