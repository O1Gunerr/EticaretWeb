using İlkProjeWebUI.Entity;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin.Security.OAuth;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace İlkProjeWebUI.Identity
{
    public class IdentityInitializer : CreateDatabaseIfNotExists<IdentityDataContext>
    {
        protected override void Seed(IdentityDataContext context)
        {
            //Rolleri
            if (!context.Roles.Any(i=>i.Name=="admin"))
            {
                var store = new RoleStore<ApplicationRole>(context);
                var manager=new RoleManager<ApplicationRole>(store);
                var role = new ApplicationRole() {Name="admin",Description="admin rolü" };
                manager.Create(role);
            }


            //User
            if (!context.Roles.Any(i => i.Name == "user"))
            {
                var store = new RoleStore<ApplicationRole>(context);
                var manager = new RoleManager<ApplicationRole>(store);
                var role = new ApplicationRole() { Name = "user", Description = "user rolü" };
                manager.Create(role);
            }


            if (!context.Users.Any(i => i.Name == "Omerguner"))
            {
                var store = new UserStore<ApplicationUser>(context);
                var manager = new UserManager<ApplicationUser>(store);
                var user = new ApplicationUser() {Name="Ömer",Surname="Güner",UserName="Omerguner",Email= "Omerguner@gmail.com" };
                manager.Create(user,"1234567");
                manager.AddToRole(user.Id, "admin");
                manager.AddToRole(user.Id, "user");

            }


            if (!context.Users.Any(i => i.Name == "Doguguner"))
            {
                var store = new UserStore<ApplicationUser>(context);
                var manager = new UserManager<ApplicationUser>(store);
                var user = new ApplicationUser() { Name = "Doğu", Surname = "Güner", UserName = "Doguner", Email = "Doguner@gmail.com" };
                manager.Create(user, "1234567");
                manager.AddToRole(user.Id, "user");

            }

            base.Seed(context);
        }

    }
}