using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using PagedList;
using project3.Models;

namespace project3.Areas.Admin.Controllers
{
    public class applicantsController : Controller
    {
        private project3Entities db = new project3Entities();

        // GET: Admin/applicants
        public ActionResult Index(int? i, string search, string SortOrder)
        {
            var applicants = db.applicants.ToList();
            if (!String.IsNullOrWhiteSpace(search))
            {
                applicants = applicants.Where(x => x.id.Equals(search) || x.display_name.Contains(search)).ToList();
            }
            ViewBag.NameSortParm = String.IsNullOrEmpty(SortOrder) ? "name_desc" : "";
            ViewBag.StatusSortParm = SortOrder == "Status" ? "Status" : "Status_desc";
            switch (SortOrder)
            {
                case "name_desc":
                    applicants = applicants.OrderByDescending(a => a.display_name).ToList();
                    break;
                case "Status_desc":
                    applicants = applicants.OrderByDescending(a => a.status).ToList();
                    break;
                case "Status":
                    applicants = applicants.OrderBy(a => a.status).ToList();
                    break;
                default:
                    applicants = applicants.OrderBy(a => a.display_name).ToList();
                    break;
            }
            return View(applicants.ToList().ToPagedList(i ?? 1, 10));
        }

        // GET: Admin/applicants/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            applicant applicant = db.applicants.Find(id);
            if (applicant == null)
            {
                return HttpNotFound();
            }
            return View(applicant);
        }

        // GET: Admin/applicants/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/applicants/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(string target,string experience, string education, string skills,string phone, HttpPostedFileBase image, string display_name, string email)
        {
            applicant applicant = new applicant();
            var applicantId = db.applicants.OrderByDescending(x => x.id).FirstOrDefault();
            int myId = 0;
            //int myId = applicantId == null ? 0 : Int32.Parse(v.id.TrimStart('A', '0'));
            if (applicantId != null)
            {
                myId = Int32.Parse(applicantId.id.TrimStart('A', '0'));
            }
            myId++;
            string a = "";
            switch (myId.ToString().Length)
            {
                case 1:
                    a = "A000" + myId;
                    break;
                case 2:
                    a = "A00" + myId;
                    break;
                case 3:
                    a = "A0" + myId;
                    break;
                case 4:
                    a = "A" + myId;
                    break;
                default:
                    break;
            }

            if (db.applicants.Any(x => x.phone.Equals(applicant.phone)))
            {
                TempData["AlertMessage"] = "Phone number is exist, please check again";
                Error();
            }

            if (db.applicants.Any(x => x.email.Equals(applicant.email)))
            {
                TempData["AlertMessage"] = "Email is exist, please check again";
                Error();
            }

            applicant.target = target;
            applicant.experience = experience;
            applicant.education = education;
            applicant.skills = skills;
            applicant.phone = phone;
            applicant.status = "Not in process";
            applicant.id = a;
            applicant.createAt = DateTime.Now.Date;
            applicant.username = "";
            applicant.password = "";
            applicant.display_name = display_name;
            applicant.email = email;

            string path = Path.Combine(Server.MapPath("~/image/applicant"), Path.GetFileName(image.FileName));
            image.SaveAs(path);

            if (ModelState.IsValid)
            {
                db.applicants.Add(applicant);
                db.SaveChanges();
                TempData["AlertMessage"] = "Create succesfully";
                return RedirectToAction("Index");
            }
            TempData["AlertMessage"] = "Some input is wrong, please check again";
            Error();
            return View(applicant);
        }
        public ActionResult Banned(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            applicant applicant = db.applicants.Find(id);
            if (applicant == null)
            {
                return HttpNotFound();
            }
            return View(applicant);
        }

        // POST: Admin/applicants/Delete/5
        [HttpPost, ActionName("Banned")]
        [ValidateAntiForgeryToken]
        public ActionResult BannedConfirmed(string id)
        {
            applicant applicant = db.applicants.Find(id);
            applicant.status = "Banned";
            db.Entry(applicant).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
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
