using İlkProjeWebUI.Entity;
using İlkProjeWebUI.Models;
using System;
using System.Linq;
using System.Web.Mvc;

namespace İlkProjeWebUI.Controllers
{
    public class HomeController : Controller
    {
        public DataContext _context = new DataContext();

        public ActionResult Index()
        {
            var rnd = new Random();

            var urunler = _context.Products
                                  .Where(p => p.IsHome && p.IsApproved)
                                  .ToList()
                                  .OrderBy(x => rnd.Next())
                                  .Take(6)
                                  .Select(p => new ProductModel
                                  {
                                      Id = p.Id,
                                      Name = p.Name.Length > 50
                                              ? p.Name.Substring(0, 47) + "..."
                                              : p.Name,
                                      Description = p.Description.Length > 50
                                                   ? p.Description.Substring(0, 47) + "..."
                                                   : p.Description,
                                      Price = p.Price,
                                      Image = p.Image,
                                      CategoryId = p.CategoryId
                                  })
                                  .ToList();

            return View(urunler);
        }

        public ActionResult List(string category,string query)
        {
            var products = _context.Products
        .Include("Category")
        .Where(p => p.IsApproved);

            // Kategori filtresi
            if (!string.IsNullOrEmpty(category))
            {
                products = products.Where(p => p.Category.Name == category);
            }

            // Arama filtresi
            if (!string.IsNullOrEmpty(query))
            {
                products = products.Where(p => p.Name.Contains(query));
            }

            var list = products
                .Select(p => new ProductModel
                {
                    Id = p.Id,
                    Name = p.Name,
                    Description = p.Description,
                    Price = p.Price,
                    Image = p.Image ?? "1.jpg",
                    CategoryId = p.CategoryId
                })
           
                               .ToList();


            ViewBag.Kategoriler = _context.Categories.ToList();
            ViewBag.SeciliKategori = category;
            ViewBag.Arama = query;

            return View(list);
        }

        public ActionResult Details(int id)
        {
            var prod = _context.Products
                               .FirstOrDefault(p => p.Id == id && p.IsApproved);
            if (prod == null)
                return HttpNotFound();
            return View(prod);
        }

        public ActionResult About() => View();
        public ActionResult Contact() => View();
    }
   
}


