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
    public class PersonellerController : Controller
    {
       
        public ActionResult Index()
        {
            return View();
        }

        [Route("personeller")]
        [Authorize]
        public ActionResult Personeller()
        {

            string connStr = ConfigurationManager.ConnectionStrings["SankoHoldingDB"].ConnectionString;
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                conn.Open();
                string query = "SELECT * FROM Personel"; 
                SqlCommand cmd = new SqlCommand(query, conn);
                SqlDataReader reader = cmd.ExecuteReader();
                List<personel> personeller = new List<personel>();

                while (reader.Read())
                {
                    personeller.Add(new Models.personel
                    {
                        CardID = reader["CardID"].ToString(),
                        CardNo = reader["CardNo"].ToString(),
                        RollID = reader["RollID"].ToString(),
                        Fullname = reader["Fullname"].ToString(),
                        Company = reader["Company"].ToString(),
                        CompanyID = reader["CompanyID"].ToString(),
                        PhoneNumber = reader["PhoneNumber"].ToString(),
                        MobileNumber = reader["MobileNumber"].ToString(),
                        PhoneNumber2 = reader["PhoneNumber2"].ToString(),
                        EmailAdress = reader["EmailAdress"].ToString(),
                        Password = reader["Password"].ToString(),
                        JobTitle = reader["JobTitle"].ToString(),
                        WorkAdress = reader["WorkAdress"].ToString(),
                        Website = reader["Website"].ToString(),
                        Logo = reader["Logo"].ToString(),
                        AddDate = Convert.ToDateTime(reader["AddDate"]),
                        UpdDate = Convert.ToDateTime(reader["UpdDate"])
                    });
                }
                return View(personeller);
            }
        }



     
        [Route("personel-ekle")]
        public ActionResult PersonelEkle()
        {
            return View();
        }

     
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("personel-ekle")]
        public ActionResult PersonelEkle(personel yeniPersonel)
        {
            if (ModelState.IsValid)
            {
                try
                {
                   
                    string connStr = ConfigurationManager.ConnectionStrings["SankoHoldingDB"].ConnectionString;
                    using (SqlConnection conn = new SqlConnection(connStr))
                    {
                        conn.Open();

                        
                        string query = @"INSERT INTO Personel 
                (CardID, CardNo, RollID, Fullname, Company, CompanyID, PhoneNumber, MobileNumber, PhoneNumber2, EmailAdress, Password, JobTitle, WorkAdress, Image, Website, Logo, AddDate, UpdDate)
                VALUES (@CardID, @CardNo, @RollID, @Fullname, @Company, @CompanyID, @PhoneNumber, @MobileNumber, @PhoneNumber2, @EmailAdress, @Password, @JobTitle, @WorkAdress, @Image, @Website, @Logo, @AddDate, @UpdDate)";

                        SqlCommand cmd = new SqlCommand(query, conn);

                      

                        cmd.Parameters.AddWithValue("@CardID", yeniPersonel.CardID);
                        cmd.Parameters.AddWithValue("@CardNo", string.IsNullOrEmpty(yeniPersonel.CardNo) ? DBNull.Value : (object)yeniPersonel.CardNo);
                        cmd.Parameters.AddWithValue("@RollID", string.IsNullOrEmpty(yeniPersonel.RollID) ? DBNull.Value : (object)yeniPersonel.RollID);
                        cmd.Parameters.AddWithValue("@Fullname", string.IsNullOrEmpty(yeniPersonel.Fullname) ? DBNull.Value : (object)yeniPersonel.Fullname);
                        cmd.Parameters.AddWithValue("@Company", string.IsNullOrEmpty(yeniPersonel.Company) ? DBNull.Value : (object)yeniPersonel.Company);
                        cmd.Parameters.AddWithValue("@CompanyID", string.IsNullOrEmpty(yeniPersonel.CompanyID) ? DBNull.Value : (object)yeniPersonel.CompanyID);
                        cmd.Parameters.AddWithValue("@PhoneNumber", string.IsNullOrEmpty(yeniPersonel.PhoneNumber) ? DBNull.Value : (object)yeniPersonel.PhoneNumber);
                        cmd.Parameters.AddWithValue("@MobileNumber", string.IsNullOrEmpty(yeniPersonel.MobileNumber) ? DBNull.Value : (object)yeniPersonel.MobileNumber);
                        cmd.Parameters.AddWithValue("@PhoneNumber2", string.IsNullOrEmpty(yeniPersonel.PhoneNumber2) ? DBNull.Value : (object)yeniPersonel.PhoneNumber2);
                        cmd.Parameters.AddWithValue("@EmailAdress", string.IsNullOrEmpty(yeniPersonel.EmailAdress) ? DBNull.Value : (object)yeniPersonel.EmailAdress);
                        cmd.Parameters.AddWithValue("@Password", string.IsNullOrEmpty(yeniPersonel.Password) ? DBNull.Value : (object)yeniPersonel.Password);
                        cmd.Parameters.AddWithValue("@JobTitle", string.IsNullOrEmpty(yeniPersonel.JobTitle) ? DBNull.Value : (object)yeniPersonel.JobTitle);
                        cmd.Parameters.AddWithValue("@WorkAdress", string.IsNullOrEmpty(yeniPersonel.WorkAdress) ? DBNull.Value : (object)yeniPersonel.WorkAdress);
                        cmd.Parameters.AddWithValue("@Image", string.IsNullOrEmpty(yeniPersonel.Image) ? DBNull.Value : (object)yeniPersonel.Image);
                        cmd.Parameters.AddWithValue("@Website", string.IsNullOrEmpty(yeniPersonel.Website) ? DBNull.Value : (object)yeniPersonel.Website);
                        cmd.Parameters.AddWithValue("@Logo", string.IsNullOrEmpty(yeniPersonel.Logo) ? DBNull.Value : (object)yeniPersonel.Logo);

                        
                        yeniPersonel.AddDate = DateTime.Now;
                        yeniPersonel.UpdDate = DateTime.Now;
                        cmd.Parameters.AddWithValue("@AddDate", yeniPersonel.AddDate);
                        cmd.Parameters.AddWithValue("@UpdDate", yeniPersonel.UpdDate);

                        cmd.ExecuteNonQuery();
                    }
                   
                    return RedirectToAction("Personeller");
                }
                catch (Exception ex)
                {
                   
                    ViewBag.Error = ex.Message;
                }
            }
         
            return View(yeniPersonel);
        }
        
        public ActionResult PersonelSil(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
               
                return RedirectToAction("Personeller");
            }

            try
            {
                string connStr = ConfigurationManager.ConnectionStrings["SankoHoldingDB"].ConnectionString;
                using (SqlConnection conn = new SqlConnection(connStr))
                {
                    conn.Open();
                    string query = "DELETE FROM Personel WHERE CardID = @CardID";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@CardID", id);
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {

                TempData["Error"] = ex.Message;
            }

            return RedirectToAction("Personeller");
        }
        [Route("personel-duzenle")]
        public ActionResult PersonelDuzenle(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return RedirectToAction("Personeller");
            }

            try
            {
                string connStr = ConfigurationManager.ConnectionStrings["SankoHoldingDB"].ConnectionString;
                using (SqlConnection conn = new SqlConnection(connStr))
                {
                    conn.Open();
                    string query = "SELECT * FROM Personel WHERE CardID = @CardID";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@CardID", id);
                    SqlDataReader reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        personel duzenlenecek = new personel
                        {
                            CardID = reader["CardID"].ToString(),
                            CardNo = reader["CardNo"].ToString(),
                            RollID = reader["RollID"].ToString(),
                            Fullname = reader["Fullname"].ToString(),
                            Company = reader["Company"].ToString(),
                            CompanyID = reader["CompanyID"].ToString(),
                            PhoneNumber = reader["PhoneNumber"].ToString(),
                            MobileNumber = reader["MobileNumber"].ToString(),
                            PhoneNumber2 = reader["PhoneNumber2"].ToString(),
                            EmailAdress = reader["EmailAdress"].ToString(),
                            Password = reader["Password"].ToString(),
                            JobTitle = reader["JobTitle"].ToString(),
                            WorkAdress = reader["WorkAdress"].ToString(),
                            Image = reader["Image"].ToString(),
                            Website = reader["Website"].ToString(),
                            Logo = reader["Logo"].ToString(),
                            AddDate = Convert.ToDateTime(reader["AddDate"]),
                            UpdDate = Convert.ToDateTime(reader["UpdDate"])
                        };
                        return View(duzenlenecek);
                    }
                    else
                    {
                        return RedirectToAction("Personeller");
                    }
                }
            }
            catch (Exception ex)
            {
                TempData["Error"] = ex.Message;
                return RedirectToAction("Personeller");
            }
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("personel-duzenle")]
        public ActionResult PersonelDuzenle(personel duzenlenenPersonel)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    string connStr = ConfigurationManager.ConnectionStrings["SankoHoldingDB"].ConnectionString;
                    using (SqlConnection conn = new SqlConnection(connStr))
                    {
                        conn.Open();
                        string query = @"UPDATE Personel 
                                 SET 
                                     CardNo = @CardNo,
                                     RollID = @RollID,
                                     Fullname = @Fullname,
                                     Company = @Company,
                                     CompanyID = @CompanyID,
                                     PhoneNumber = @PhoneNumber,
                                     MobileNumber = @MobileNumber,
                                     PhoneNumber2 = @PhoneNumber2,
                                     EmailAdress = @EmailAdress,
                                     Password = @Password,
                                     JobTitle = @JobTitle,
                                     WorkAdress = @WorkAdress,
                                     Image = @Image,
                                     Website = @Website,
                                     Logo = @Logo,
                                     UpdDate = @UpdDate
                                 WHERE CardID = @CardID";
                        SqlCommand cmd = new SqlCommand(query, conn);

                        cmd.Parameters.AddWithValue("@CardID", duzenlenenPersonel.CardID);
                        cmd.Parameters.AddWithValue("@CardNo", string.IsNullOrEmpty(duzenlenenPersonel.CardNo) ? DBNull.Value : (object)duzenlenenPersonel.CardNo);
                        cmd.Parameters.AddWithValue("@RollID", string.IsNullOrEmpty(duzenlenenPersonel.RollID) ? DBNull.Value : (object)duzenlenenPersonel.RollID);
                        cmd.Parameters.AddWithValue("@Fullname", string.IsNullOrEmpty(duzenlenenPersonel.Fullname) ? DBNull.Value : (object)duzenlenenPersonel.Fullname);
                        cmd.Parameters.AddWithValue("@Company", string.IsNullOrEmpty(duzenlenenPersonel.Company) ? DBNull.Value : (object)duzenlenenPersonel.Company);
                        cmd.Parameters.AddWithValue("@CompanyID", string.IsNullOrEmpty(duzenlenenPersonel.CompanyID) ? DBNull.Value : (object)duzenlenenPersonel.CompanyID);
                        cmd.Parameters.AddWithValue("@PhoneNumber", string.IsNullOrEmpty(duzenlenenPersonel.PhoneNumber) ? DBNull.Value : (object)duzenlenenPersonel.PhoneNumber);
                        cmd.Parameters.AddWithValue("@MobileNumber", string.IsNullOrEmpty(duzenlenenPersonel.MobileNumber) ? DBNull.Value : (object)duzenlenenPersonel.MobileNumber);
                        cmd.Parameters.AddWithValue("@PhoneNumber2", string.IsNullOrEmpty(duzenlenenPersonel.PhoneNumber2) ? DBNull.Value : (object)duzenlenenPersonel.PhoneNumber2);
                        cmd.Parameters.AddWithValue("@EmailAdress", string.IsNullOrEmpty(duzenlenenPersonel.EmailAdress) ? DBNull.Value : (object)duzenlenenPersonel.EmailAdress);
                        cmd.Parameters.AddWithValue("@Password", string.IsNullOrEmpty(duzenlenenPersonel.Password) ? DBNull.Value : (object)duzenlenenPersonel.Password);
                        cmd.Parameters.AddWithValue("@JobTitle", string.IsNullOrEmpty(duzenlenenPersonel.JobTitle) ? DBNull.Value : (object)duzenlenenPersonel.JobTitle);
                        cmd.Parameters.AddWithValue("@WorkAdress", string.IsNullOrEmpty(duzenlenenPersonel.WorkAdress) ? DBNull.Value : (object)duzenlenenPersonel.WorkAdress);
                        cmd.Parameters.AddWithValue("@Image", string.IsNullOrEmpty(duzenlenenPersonel.Image) ? DBNull.Value : (object)duzenlenenPersonel.Image);
                        cmd.Parameters.AddWithValue("@Website", string.IsNullOrEmpty(duzenlenenPersonel.Website) ? DBNull.Value : (object)duzenlenenPersonel.Website);
                        cmd.Parameters.AddWithValue("@Logo", string.IsNullOrEmpty(duzenlenenPersonel.Logo) ? DBNull.Value : (object)duzenlenenPersonel.Logo);

                       
                        duzenlenenPersonel.UpdDate = DateTime.Now;
                        cmd.Parameters.AddWithValue("@UpdDate", duzenlenenPersonel.UpdDate);

                        cmd.ExecuteNonQuery();
                    }
                    return RedirectToAction("Personeller");
                }
                catch (Exception ex)
                {
                    ViewBag.Error = ex.Message;
                }
            }
            return View(duzenlenenPersonel);
        }

    }
}