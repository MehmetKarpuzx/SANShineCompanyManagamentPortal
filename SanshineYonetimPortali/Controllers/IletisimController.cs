using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebApplication4.Controllers
{
    public class IletisimController : Controller
    {
        [Route("iletisim")]
        public ActionResult Iletisim()
        {
            return View();
        }
    }
}