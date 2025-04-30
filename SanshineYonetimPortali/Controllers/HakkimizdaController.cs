using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebApplication4.Controllers
{
    public class HakkimizdaController : Controller
    {
        [Route("hakkimizda")]
        public ActionResult Hakkimizda()
        {
            return View();
        }
    }
}