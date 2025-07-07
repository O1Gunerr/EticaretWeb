using İlkProjeWebUI.Identity;
using İlkProjeWebUI.Models;
using Microsoft.Ajax.Utilities;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
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
                    if (RoleManager.RoleExists("User"))
                    {

                        UserManager.AddToRole(user.Id, "User");
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
    }
}