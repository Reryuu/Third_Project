using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Web;
using System.Web.Mvc;
using project3.Models;

namespace project3.Controllers
{
    public class UsersController : Controller
    {
        private project3Entities db = new project3Entities();

        SqlConnection con = new SqlConnection(@"Data Source=DESKTOP-F1EG3ID\SQLEXPRESS;Initial Catalog=project3;Integrated Security=True;MultipleActiveResultSets=True;Application Name=EntityFramework");
        SqlDataReader dr;

        public ActionResult Login()
        {
            return View();
        }
        // POST: Account
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(string username, string password)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    con.Open();
                    SqlCommand com = new SqlCommand("select * from [employee] where username = @username and password = @password", con);
                    com.Parameters.AddWithValue("username", username);
                    com.Parameters.AddWithValue("password", GetMD5(password));
                    dr = com.ExecuteReader();
                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            //Session["id"] = dr["id"];
                            //Session["Name"] = dr["name"];
                            TempData["id"] = dr["id"];
                            TempData["email"] = dr["email"];
                            TempData["name"] = dr["display_name"];
                            string navi = "Login succesfully. Welcome " + TempData["name"].ToString();
                            TempData["AlertMessage"] = navi;
                            
                            TempData.Keep();
                            switch (dr["department_id"].ToString())
                            {
                                case "D003":
                                    return RedirectToAction("Index", "Home", new { area = "Admin"});                                    
                                default:
                                    return RedirectToAction("Index", "details_interview", new { area = "Interviewer" });
                            }
                        }
                    }
                    TempData["AlertMessage"] = "Login failed! User name or password is wrong.";
                    con.Close();
                    return RedirectToAction("Login");
                }
                catch (SqlException e)
                {
                    Console.WriteLine(e);
                }
            }
            return View();
        }

        //Logout
        public ActionResult Logout()
        {
            Session.Abandon();//remove session
            return RedirectToAction("/Login");
        }

        //create a string MD5
        public string GetMD5(string str)
        {
            MD5 md5 = new MD5CryptoServiceProvider();
            byte[] fromData = System.Text.Encoding.UTF8.GetBytes(str);
            byte[] targetData = md5.ComputeHash(fromData);
            string byte2String = null;

            for (int i = 0; i < targetData.Length; i++)
            {
                byte2String += targetData[i].ToString("x2");

            }
            return byte2String;
        }

        // GET: Users/Details/5

        // GET: Users/Create
        //public ActionResult Create()
        //{
        //    ViewBag.type_userId = new SelectList(db.type_user, "id", "type");
        //    return View();
        //}

        // POST: Users/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        //public ActionResult Create([Bind(Include = "id,username,password,name,email,type_userId")] user user)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        db.users.Add(user);
        //        db.SaveChanges();
        //        return RedirectToAction("Index");
        //    }
        //    ViewBag.type_userId = new SelectList(db.type_user, "id", "type", user.type_userId);
        //    return View(user);
        //}       

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
