using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace WebApplication4.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login
        public ActionResult Login(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View(); 
        }

        [HttpPost]
        public ActionResult Login(string username, string password, string returnUrl)
        {
            string connStr = ConfigurationManager.ConnectionStrings["SankoHoldingDB"].ConnectionString;
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                conn.Open();
                string query = "SELECT * FROM Admin WHERE Username = @Username AND Password = @Password";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@Username", username);
                cmd.Parameters.AddWithValue("@Password", password);
                SqlDataReader reader = cmd.ExecuteReader();




                if (reader.HasRows) 
                {
                    FormsAuthentication.SetAuthCookie(username, false); 

               
                    if (!string.IsNullOrEmpty(returnUrl) && Url.IsLocalUrl(returnUrl))
                    {
                        return Redirect(returnUrl);
                    }


                    return RedirectToAction("Index"); 
                }
                else

                    ViewBag.Error = "Kullanıcı adı veya şifre yanlış!";
                return View();


            }


        }
    }
}