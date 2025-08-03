using İlkProjeWebUI.Entity;
using İlkProjeWebUI.Identity;
using İlkProjeWebUI.Models;
using Microsoft.Ajax.Utilities;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Hosting;
using System.Web.Mvc;

namespace İlkProjeWebUI.Controllers
{
    public class AccountController : Controller

    {
        private DataContext db = new DataContext();

        private UserManager<ApplicationUser> UserManager;
        private RoleManager<ApplicationRole> RoleManager;
        // GET: Account

        public AccountController()
        {
            var userStore = new UserStore<ApplicationUser>(new IdentityDataContext());

            UserManager= new UserManager<ApplicationUser>(userStore);

            var roleStore = new RoleStore<ApplicationRole>(new IdentityDataContext());

            RoleManager=new RoleManager<ApplicationRole>(roleStore);
        }


        public ActionResult Index()
        { 
            var username = User.Identity.Name;
            var orders = db.Orders
                .Where(o => o.Username == username)
                .Select(o => new UserOrderModel
                {
                    Id = o.Id,
                    OrderNumber = o.OrderNumber,
                    Total = o.Total,
                    OrderState = o.OrderState,
                    Date = o.Date,
                }).ToList();
                                     


            return View(orders);
        }
        public ActionResult Details(int id)
        {
            var entity = db.Orders.Where(i => i.Id == id)
                .Select(  i => new OrderDetails()
                {
                    
                    OrderNumber = i.OrderNumber,
                    Total  = i.Total,
                    Date = i.Date,
                    OrderState= i.OrderState,
                    Adres=i.Adres,
                    AdresTarif=i.AdresTarif,
                }).FirstOrDefault();
                

                
            return View();
        }

        public ActionResult Register()
        {
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register(Register model)
        {

            if (ModelState.IsValid)
            { 
           var user=new ApplicationUser();
                user.Name = model.Name;
                user.Surname = model.Surname;
                user.Email = model.Email;
                user.UserName = model.UserName;

               IdentityResult result=UserManager.Create(user,model.Password);

                if (result.Succeeded)
                {
                    if (RoleManager.RoleExists("user"))
                    {

                        UserManager.AddToRole(user.Id, "user");
                    }
                    return RedirectToAction("Login", "Account");
                }
                else
                {
                    ModelState.AddModelError("RegisterUserError","Kullanıcı Oluşturulma hatası.");
                }



            }

            return View();
        }

        public ActionResult Login()
        {
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(Login model, string ReturnUrl)
        {

            if (ModelState.IsValid)
            {
             //Login İşlemleri
             var user= UserManager.Find(model.UserName,model.Password);

                if (user != null)
                {

                var authManager=HttpContext.GetOwinContext().Authentication;

                    var identityclaims = UserManager.CreateIdentity(user, "ApplicationCookie");
                    var authProperties = new AuthenticationProperties();
                    authProperties.IsPersistent = false;
                    authManager.SignIn(authProperties, identityclaims);
                    var roles = UserManager.GetRoles(user.Id);
                    if (roles.Contains("admin") || roles.Contains("Admin"))
                    {
                        return RedirectToAction("","Products");
                    }


                    if (!String.IsNullOrEmpty(ReturnUrl))
                    {
                        Redirect(ReturnUrl);
                    }

                    return RedirectToAction("Index","Home");
                }
                
                {
                    ModelState.AddModelError("LoginUserError", "Böyle bir Kullanıcı bulunamadı."); 
                }
                
            }

            return View();
        }

        public ActionResult Logout()
        {
            var authManager = HttpContext.GetOwinContext().Authentication;
            authManager.SignOut();


            return RedirectToAction("Index","Home");
        }
    }
}