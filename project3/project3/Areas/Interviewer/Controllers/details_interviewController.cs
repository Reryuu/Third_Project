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
            var details_interview = db.details_interview.Include(d => d.applicant).Include(d => d.employee).Include(d => d.vacancy).Where(d => d.employee.id.Equals(id)).OrderByDescending(d => d.status);
            return View(details_interview.ToList().ToPagedList(i ?? 1, 5));
        }

        // GET: Interviewer/details_interview/Details/5
        public ActionResult Details(int? id)
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
            return View(details_interview);
        }

        // GET: Interviewer/details_interview/Create
        public ActionResult Create()
        {
            //ViewBag.applicantId = new SelectList(db.applicants, "id", "experience");
            //ViewBag.interviewerId = new SelectList(db.interviewers, "id", "department_id");
            //ViewBag.vacancyId = new SelectList(db.vacancies, "id", "title");
            return View();
        }

        // POST: Interviewer/details_interview/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,applicantId,vacancyId,interviewerId,time,status")] details_interview details_interview)
        {
            if (ModelState.IsValid)
            {
                db.details_interview.Add(details_interview);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            //ViewBag.applicantId = new SelectList(db.applicants, "id", "experience", details_interview.applicantId);
            //ViewBag.interviewerId = new SelectList(db.interviewers, "id", "department_id", details_interview.interviewerId);
            //ViewBag.vacancyId = new SelectList(db.vacancies, "id", "title", details_interview.vacancyId);
            return View(details_interview);
        }

        // GET: Interviewer/details_interview/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            details_interview details_interview = db.details_interview.Find(id);
            //ViewBag.applicantId = details_interview.applicantId;
            //ViewBag.employeeId = details_interview.employeeId;
            //ViewBag.vacancyId = details_interview.vacancyId;
            //TempData["applicant"] = details_interview.applicant;
            //TempData["vacancy"] = details_interview.vacancy;
            //TempData["employee"] = details_interview.employee;
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
                        vacancies.applicants_Id.Concat(details_interview.applicantId);
                    }
                    else
                    {
                        vacancies.applicants_Id.Concat("," + details_interview.applicantId);
                    }
                }
                db.SaveChanges();
                TempData["AlertMessage"] = "Update succesfully";
                return RedirectToAction("Index");
            }
            return View(details_interview);
        }

        // GET: Interviewer/details_interview/Delete/5
        public ActionResult Delete(int? id)
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
            return View(details_interview);
        }

        // POST: Interviewer/details_interview/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            details_interview details_interview = db.details_interview.Find(id);
            db.details_interview.Remove(details_interview);
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
    }
}
