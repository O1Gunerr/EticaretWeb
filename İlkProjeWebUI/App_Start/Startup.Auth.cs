using Microsoft.Owin;
using Microsoft.Owin.Security.Cookies;
using Owin;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;              // ← ekledik
using İlkProjeWebUI.Models;                        // ← ApplicationDbContext, ApplicationUserManager için
using Microsoft.AspNet.Identity.EntityFramework;

[assembly: OwinStartup(typeof(İlkProjeWebUI.App_Start.Startup))]

namespace İlkProjeWebUI.App_Start
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            // 1) Identity DbContext’i OWIN scope’a ekle
            app.CreatePerOwinContext(ApplicationDbContext.Create);

            // 2) UserManager (ApplicationUserManager) ekle
            app.CreatePerOwinContext<ApplicationUserManager>(ApplicationUserManager.Create);

            // 3) SignInManager (ApplicationSignInManager) ekle
            app.CreatePerOwinContext<ApplicationSignInManager>(ApplicationSignInManager.Create);

            // 4) Cookie tabanlı Authentication
            app.UseCookieAuthentication(new CookieAuthenticationOptions
            {
                AuthenticationType = DefaultAuthenticationTypes.ApplicationCookie,
                LoginPath = new PathString("/Account/Login")
            });
        }
    }
}
