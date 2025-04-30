using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.SqlClient;
using System.Configuration;
using WebApplication4.Models;
using System.Web.Security;
using System.Diagnostics.Eventing.Reader;

namespace WebApplication4.Controllers
{

    public class AnasayfaController : Controller
    {
        // GET: Default

        public ActionResult Index()
        {
            return View();
        }

    }
    
    
}