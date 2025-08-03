using Microsoft.Owin;
using Microsoft.Owin.Security.Cookies;
using Mysqlx.Session;
using Owin;
using System;
using System.Threading.Tasks;

[assembly: OwinStartup("Startup1", typeof(İlkProjeWebUI.App_Start.Startup1))]
namespace İlkProjeWebUI.App_Start
{
    public class Startup1
    {
        public void Configuration(IAppBuilder app)
        {
            app.UseCookieAuthentication(new CookieAuthenticationOptions()
            {
                AuthenticationType = "ApplicationCookie",
                LoginPath = new PathString("/Account/Login"),
                 ExpireTimeSpan = TimeSpan.FromMinutes(30),
                SlidingExpiration = false,
                 CookieSecure = CookieSecureOption.SameAsRequest,
                CookieHttpOnly = true,
                CookieManager = new Microsoft.Owin.Host.SystemWeb.SystemWebCookieManager()

            });
        }
    }
}
