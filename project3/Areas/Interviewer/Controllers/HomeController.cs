using project3.Areas.Interviewer.Models;
using project3.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Security.Cryptography;
using System.Web;
using System.Web.Mvc;

namespace project3.Areas.Interviewer.Controllers
{
    public class HomeController : Controller
    {
        private project3Entities db = new project3Entities();
        // GET: Admin/Home
        public ActionResult Index()
        {
            Home home = new Home();
            home.vacancies = db.vacancies.ToList();
            home.applicants = db.applicants.ToList();
            home.employees = db.employees.ToList();
            home.departments = db.departments.ToList();
            return View(home);
        }

        public ActionResult GetData_Applicant()
        {
            //var systems = db.applicants.Select(a => a.id).ToList();
            var query = db.applicants.Where(a => a.createAt.Year == DateTime.Now.Year).GroupBy(p => p.createAt.Month).Select(g => new { name = g.Key, count = g.Count(w => w.id != null) }).ToList();
            return Json(query, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetData_Vacancy()
        {
            
            var query = db.vacancies.Where(v => v.status.Equals("Open")).OrderByDescending(v => v.applicants_Id).Take(5).ToList();
            string json = Newtonsoft.Json.JsonConvert.SerializeObject(new
            {
                result = query
            });
            return Json(new { data = json }, JsonRequestBehavior.AllowGet);  
        }

        public ActionResult ResetPassword(string id)
        {
            //Verify the reset password link
            //Find account associated with this link
            //redirect to reset password page
            if (string.IsNullOrWhiteSpace(id))
            {
                return HttpNotFound();
            }
            using (var context = new project3Entities())
            {
                //var user = context.users.Where(a => a.ResetPasswordCode == id).FirstOrDefault();
                //if (user != null)
                //{
                ResetPasswordModel model = new ResetPasswordModel();
                model.ResetCode = id;
                return View(model);
                //}
                //else
                //{
                //    return HttpNotFound();
                //}
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ResetPassword(ResetPasswordModel model)
        {
            var message = "";
            if (ModelState.IsValid)
            {
                using (var context = new project3Entities())
                {
                    var user = context.employees.Where(a => a.id == model.ResetCode).FirstOrDefault();
                    if (user != null)
                    {
                        //you can encrypt password here, we are not doing it
                        user.password = GetMD5(model.NewPassword);
                        //to avoid validation issues, disable it
                        context.Configuration.ValidateOnSaveEnabled = false;
                        context.SaveChanges();
                        message = "New password updated successfully";
                    }
                }
            }
            else
            {
                Error();
                message = "Something invalid";
            }
            TempData["AlertMessage"] = message;
            return View(model);
        }
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

        private void SendEmail(string emailAddress, string body, string subject)
        {
            using (MailMessage mm = new MailMessage(TempData["email"].ToString(), emailAddress))
            {
                mm.Subject = subject;
                mm.Body = body;

                mm.IsBodyHtml = true;
                SmtpClient smtp = new SmtpClient();
                smtp.Host = "smtp.gmail.com";
                smtp.EnableSsl = true;
                NetworkCredential NetworkCred = new NetworkCredential("youremail@gmail.com", "YourPassword");
                smtp.UseDefaultCredentials = true;
                smtp.Credentials = NetworkCred;
                smtp.Port = 587;
                smtp.Send(mm);

            }
        }

        public void Error()
        {
            TempData["alertBg"] = "#ffdb9b";
            TempData["alertBd"] = "#ffa502";
            TempData["alertMsg"] = "#ce8500";
            TempData["alertBtn"] = "#ffd080";
        }
    }
}