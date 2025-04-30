using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication4.Models;

namespace WebApplication4.Controllers
{
    public class SirketlerController : Controller
    {
        
        public ActionResult Index()
        {
            return View();
        }

        [Route("sirketler")]
        [Authorize]
        public ActionResult Sirketler()
        {

            string connStr = ConfigurationManager.ConnectionStrings["SankoHoldingDB"].ConnectionString;
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                conn.Open();
                string query = "SELECT * FROM Company";
                SqlCommand cmd = new SqlCommand(query, conn);
                SqlDataReader reader = cmd.ExecuteReader();
                List<company> sirketler = new List<company>();
                while (reader.Read())
                {
                    sirketler.Add(new Models.company
                    {
                        CompanyID = reader["CompanyID"].ToString(),
                        CompanyName = reader["CompanyName"].ToString(),

                    });
                }
                return View(sirketler);
            }
            return View();
        }
        [Route("company-ekle")]
        public ActionResult CompanyEkle()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("company-ekle")]
        public ActionResult CompanyEkle(company newCompany)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    string connStr = ConfigurationManager.ConnectionStrings["SankoHoldingDB"].ConnectionString;
                    using (SqlConnection conn = new SqlConnection(connStr))
                    {
                        conn.Open();

                        
                        string query = "INSERT INTO Company (CompanyID, CompanyName) VALUES (@CompanyID, @CompanyName)";
                        SqlCommand cmd = new SqlCommand(query, conn);

                        cmd.Parameters.AddWithValue("@CompanyID", newCompany.CompanyID);
                        cmd.Parameters.AddWithValue("@CompanyName", newCompany.CompanyName);

                        cmd.ExecuteNonQuery();
                    }
                    
                    return RedirectToAction("Sirketler");
                }
                catch (Exception ex)
                {
                    ViewBag.Error = ex.Message;
                }
            }
            
            return View(newCompany);
        }
        [Route("company-sil")]
        public ActionResult CompanySil(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return RedirectToAction("Sirketler");
            }

            try
            {
                string connStr = ConfigurationManager.ConnectionStrings["SankoHoldingDB"].ConnectionString;
                using (SqlConnection conn = new SqlConnection(connStr))
                {
                    conn.Open();
                    string query = "DELETE FROM Company WHERE CompanyID = @CompanyID";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@CompanyID", id);
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                TempData["Error"] = ex.Message;
            }
            return RedirectToAction("Sirketler");
        }
        [Route("company-duzenle")]
        public ActionResult CompanyDuzenle(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return RedirectToAction("Sirketler");
            }

            try
            {
                string connStr = ConfigurationManager.ConnectionStrings["SankoHoldingDB"].ConnectionString;
                using (SqlConnection conn = new SqlConnection(connStr))
                {
                    conn.Open();
                    string query = "SELECT CompanyID, CompanyName FROM Company WHERE CompanyID = @CompanyID";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@CompanyID", id);
                    SqlDataReader reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        // Verileri model nesnesine aktar
                        var duzenlenecek = new Models.company
                        {
                            CompanyID = reader["CompanyID"].ToString(),
                            CompanyName = reader["CompanyName"].ToString()
                        };
                        reader.Close();
                        return View(duzenlenecek);
                    }
                    else
                    {
                        return RedirectToAction("Sirketler");
                    }
                }
            }
            catch (Exception ex)
            {
                TempData["Error"] = ex.Message;
                return RedirectToAction("Sirketler");
            }
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("company-duzenle")]
        public ActionResult CompanyDuzenle(Models.company updatedCompany)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    string connStr = ConfigurationManager.ConnectionStrings["SankoHoldingDB"].ConnectionString;
                    using (SqlConnection conn = new SqlConnection(connStr))
                    {
                        conn.Open();
                        string query = "UPDATE Company SET CompanyName = @CompanyName WHERE CompanyID = @CompanyID";
                        SqlCommand cmd = new SqlCommand(query, conn);
                        cmd.Parameters.AddWithValue("@CompanyID", updatedCompany.CompanyID);
                        cmd.Parameters.AddWithValue("@CompanyName", updatedCompany.CompanyName);
                        cmd.ExecuteNonQuery();
                    }
                    return RedirectToAction("Sirketler");
                }
                catch (Exception ex)
                {
                    ViewBag.Error = ex.Message;
                }
            }
            return View(updatedCompany);
        }


    }
}