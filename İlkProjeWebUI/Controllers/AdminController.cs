using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace İlkProjeWebUI.Controllers
{
    [Authorize(Roles = "Admin")] 
    public class AdminController : Controller
    {
        // /Admin veya /Admin/Index adresi buraya gelir
        public ActionResult Index()
        {
            return View();

        }
    }
}
