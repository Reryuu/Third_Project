using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;
using PagedList;
using project3.Models;

namespace project3.Areas.Admin.Controllers
{
    public class details_interviewController : Controller
    {
        private project3Entities db = new project3Entities();

        // GET: Admin/details_interview
        public ActionResult Index(int? i)
        {
            var details_interview = db.details_interview.Include(d => d.applicant).Include(d => d.employee).Include(d => d.vacancy);
            return View(details_interview.ToList().ToPagedList(i ?? 1, 10));
        }
        // GET: Admin/details_interview/Create
        public ActionResult Create(string vacancyId)
        {
            List<applicant> apls = GetApplicant();
            ViewBag.applicantId = apls;

            List<vacancy> vacs = getVacancy();
            ViewBag.vacancyId = vacs;
            return View();
        }

        // POST: Admin/details_interview/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,applicantId,vacancyId,employeeId,time,status")] details_interview details_interview, string apl, string vac, string emp)
        {
            if (getVacancy().Where(v => v.id.Equals(vac)).FirstOrDefault().endAt < details_interview.time)
            {
                TempData["AlertMessage"] = "Interview day should be before the vacancy is closed.";
                return RedirectToAction("Create");
            }
            details_interview.status = "Scheduled";
            details_interview.applicantId = apl;
            details_interview.vacancyId = vac;
            details_interview.employeeId = emp;
            if (ModelState.IsValid)
            {
                db.details_interview.Add(details_interview);
                db.SaveChanges();
                TempData["AlertMessage"] = "Create succesfully";

                var subject = "Interview invitation letter";
                var body = "<h3>Dear " + GetApplicant().Where(a => a.id.Equals(apl)).FirstOrDefault().display_name + ",</h3> <br/><br/> " +
                        "I'm " + TempData["name"].ToString() + " in the personnel department from ABC Company " +
                        " <br/><br/> Thank you for your interest in the " + details_interview.vacancy.title + " position at ABC Company. " +
                        "We would like to invite you to an interview. Please go on time for the interview schedule : <br/><br/>" +
                        "Day: <b>" + details_interview.time + "</b><br/>" +
                        "Location: <b> 6th Floor, ABC building </b><br/>" +
                        "Position: <b>" + getVacancy().Where(v => v.id.Equals(vac)).FirstOrDefault().title + "</b><br/>" +
                        "If you have any additional questions, please don't hesitate to contact us at: <a href = 'abcCompany@gmail.vn'> abcCompany@gmail.vn </a><br/>" +
                        "Please confirm your attendence via this email.<br/></br>" +
                        "Best regards,</br></br>  From ABC Company.";
                SendMail(GetApplicant().Where(a => a.id.Equals(apl)).FirstOrDefault().email, body, subject);

                return RedirectToAction("Create");
            }
            TempData["AlertMessage"] = "Some input is wrong, please check again.";
            Error();
            List<applicant> apls = GetApplicant();
            ViewBag.applicantId = apls;

            List<vacancy> vacs = getVacancy();
            ViewBag.vacancyId = vacs;
            return View(details_interview);
        }

        // GET: Admin/details_interview/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            details_interview details_interview = db.details_interview.Find(id);
            if (details_interview == null)
            {
                return HttpNotFound();
            }
            List<applicant> apl = GetApplicant();
            ViewBag.applicantId = apl;

            List<vacancy> vac = getVacancy();
            ViewBag.vacancyId = vac;
            return View(details_interview);
        }

        // POST: Admin/details_interview/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,applicantId,vacancyId,interviewerId,time,status")] details_interview details_interview)
        {
            if (ModelState.IsValid)
            {
                db.Entry(details_interview).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            List<applicant> apl = GetApplicant();
            ViewBag.applicantId = apl;

            List<vacancy> vac = getVacancy();
            ViewBag.vacancyId = vac;
            return View(details_interview);
        }
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
        public JsonResult AjaxSearchVac(string SearchValue)
        {
            var data = getVacancy();
            if (!string.IsNullOrWhiteSpace(SearchValue))
            {
                data = data.Where(s => s.id.Contains(SearchValue) || s.title.Contains(SearchValue)).ToList();
            }

            string json = Newtonsoft.Json.JsonConvert.SerializeObject(new
            {
                result = data
            });
            return Json(new { data = json }, JsonRequestBehavior.AllowGet);
        }
        public JsonResult AjaxSearchApl(string SearchValue)
        {
            var data = GetApplicant();
            if (!string.IsNullOrWhiteSpace(SearchValue))
            {
                data = data.Where(s => s.id.Contains(SearchValue) || s.display_name.Contains(SearchValue)).ToList();
            }

            string json = Newtonsoft.Json.JsonConvert.SerializeObject(new
            {
                result = data
            });
            return Json(new { data = json }, JsonRequestBehavior.AllowGet);
        }
        public JsonResult AjaxSearchItv(string SearchValue)
        {
            string json;
            if (string.IsNullOrWhiteSpace(SearchValue))
            {
                json = Newtonsoft.Json.JsonConvert.SerializeObject(new
                {
                    result = "".ToList()
                });
                //ViewBag.interviewerId = null;
            }
            else
            {
                var data = GetInterviewer();
                var DepartOfVac = db.vacancies.Where(a => a.title.Equals(SearchValue)).FirstOrDefault();
                data = data.Where(s => s.department.name == DepartOfVac.department.name).ToList();

                //ViewBag.interviewerId = data;
                json = Newtonsoft.Json.JsonConvert.SerializeObject(new
                {
                    result = data
                });            }
            return Json(new { data = json }, JsonRequestBehavior.AllowGet);
        }

        public List<vacancy> getVacancy()
        {
            return db.vacancies.Where(v => v.status.Equals("Open")).ToList();
        }

        public List<applicant> GetApplicant()
        {
            return db.applicants.Where(a => a.status.Equals("Not in process")).ToList();
        }

        public List<employee> GetInterviewer()
        {
            return db.employees.Where(e => e.department_id != "D003").ToList();
        }

        //[HttpPost]
        private void SendMail(string emailAddress, string body, string subject)
        {
            using (MailMessage mm = new MailMessage(TempData["email"].ToString(), emailAddress))
            {
                mm.Subject = subject;
                mm.Body = body;

                mm.IsBodyHtml = true;
                using (SmtpClient smtp = new SmtpClient())
                {
                    smtp.Host = "smtp.gmail.com";
                    smtp.EnableSsl = true;

                    NetworkCredential cred = new NetworkCredential(TempData["email"].ToString(), "Ilovepage226");
                    smtp.UseDefaultCredentials = true;
                    smtp.Credentials = cred;
                    smtp.Port = 587;

                    smtp.Send(mm);
                }
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
