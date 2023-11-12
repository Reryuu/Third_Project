using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using PagedList;
using project3.Models;

namespace project3.Areas.Interviewer.Controllers
{
    public class details_interviewController : Controller
    {
        private project3Entities db = new project3Entities();

        // GET: Interviewer/details_interview
        public ActionResult Index(int? i)
        {
            string id = TempData["id"].ToString();
            TempData.Keep("id");
            var details_interview = db.details_interview.Include(d => d.applicant).Include(d => d.employee).Include(d => d.vacancy).Where(d => d.employee.id.Equals(id)).OrderByDescending(d => d.status);
            return View(details_interview.ToList().ToPagedList(i ?? 1, 5));
        }

        public ActionResult Hired(string id)
        {
            applicant applicant = db.applicants.Find(id);
            return View(applicant);
        }

        public ActionResult Vacancy(string id)
        {
            vacancy vacancy = db.vacancies.Find(id);
            return View(vacancy);
        }

        // GET: Interviewer/details_interview/Edit/5
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
            ViewBag.applicantId = new SelectList(db.applicants, "id", "display_name", details_interview.applicantId);
            ViewBag.employeeId = new SelectList(db.employees, "id", "display_name", details_interview.employeeId);
            ViewBag.vacancyId = new SelectList(db.vacancies, "id", "title", details_interview.vacancyId);
            return View(details_interview);
        }

        // POST: Interviewer/details_interview/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,applicantId,vacancyId,employeeId,time,status")] details_interview details_interview, string apl, string vac, string emp)
        {
            //details_interview.applicantId = apl;
            //details_interview.vacancyId = vac;
            //details_interview.employeeId = emp;
            if (ModelState.IsValid)
            {
                db.Entry(details_interview).State = EntityState.Modified;
                if (details_interview.status.Equals("Selected"))
                {
                    var applicants = db.applicants.Where(a => a.status.Equals("Not in process") && a.id.Equals(details_interview.applicantId)).FirstOrDefault();
                    applicants.status = "Hired";

                    var vacancies = db.vacancies.Where(v => v.status.Equals("Open") && v.id.Equals(details_interview.vacancyId)).FirstOrDefault();
                    if (string.IsNullOrEmpty(vacancies.applicants_Id))
                    {
                        vacancies.applicants_Id += details_interview.applicantId;
                    }
                    else
                    {
                        vacancies.applicants_Id += "," + details_interview.applicantId;
                    }
                }
                db.SaveChanges();
                TempData["AlertMessage"] = "Update succesfully";
                return RedirectToAction("Index");
            }
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
    }
}
