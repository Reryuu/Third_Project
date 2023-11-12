using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Text;
using System.Web.Mvc;
using PagedList;
using project3.Models;

namespace project3.Areas.Admin.Controllers
{
    public class vacanciesController : Controller
    {
        private project3Entities db = new project3Entities();

        // GET: Admin/vacancies
        public ActionResult Index(int? i, string search,string Department, string Status)
        {
            var vacancies = db.vacancies.Include(v => v.department).Include(v => v.description).ToList();
            if (!String.IsNullOrWhiteSpace(search))
            {
                vacancies = vacancies.Where(x => x.title.Contains(search) || x.id.Equals(search)).ToList();
            }

            var DepartmentLst = new List<String>();
            var DepartmentQry = from d in db.vacancies
                                orderby d.id
                                select d.department.name;
            DepartmentLst.AddRange(DepartmentQry.Distinct());
            ViewBag.Department = new SelectList(DepartmentLst);

            var StatusLst = new List<String>();
            var StatusQry = from c in db.vacancies
                            orderby c.id
                            select c.status;
            StatusLst.AddRange(StatusQry.Distinct());
            ViewBag.Status = new SelectList(StatusLst);

            if (!String.IsNullOrEmpty(Status))
            {
                vacancies = vacancies.Where(a => a.status.Contains(Status)).ToList();
            }
            if (!String.IsNullOrEmpty(Department))
            {
                vacancies = vacancies.Where(a => a.department.name.Contains(Department)).ToList();
            }

            foreach (var item in vacancies)
            {
                if (!string.IsNullOrWhiteSpace(item.applicants_Id))
                {
                    if (item.applicants_Id.Split(',').Length == item.required)
                    {
                        item.status = "Close";
                        db.Entry(item).State = EntityState.Modified;
                        db.SaveChanges();
                    }
                }  
                if(item.endAt < DateTime.Now)
                {
                    item.status = "Close";
                    db.Entry(item).State = EntityState.Modified;
                    db.SaveChanges();
                }
            }
            return View(vacancies.ToPagedList(i ?? 1, 10));
        }

        public ActionResult Hired(string id)
        {
            var applicants = db.applicants.ToList();
            var lst = id.Split(',');
            foreach(var item in lst)
            {
                applicants = applicants.Where(a => a.id.Equals(item)).ToList();
            }
            return View(applicants);
        }

        // GET: Admin/vacancies/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            vacancy vacancy = db.vacancies.Find(id);
            if (vacancy == null)
            {
                return HttpNotFound();
            }
            return View(vacancy);
        }

        // GET: Admin/vacancies/Create
        public ActionResult Create()
        {
            ViewBag.departmentId = new SelectList(db.departments, "id", "name");
            return View();
        }

        // POST: Admin/vacancies/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,title,endAt,salary,required,status,descriptionId,departmentId,applicants_Id")] vacancy vacancy, [Bind(Include = "id,about,required,interests")] description description, int start, int end)
        {
            var v = db.vacancies.OrderByDescending(q => q.id).FirstOrDefault();
            int myId = 0;
            //int myId = v == null ? 0 : Int32.Parse(v.id.TrimStart('V', '0'));
            if (v != null)
            {
                myId = Int32.Parse(v.id.TrimStart('V', '0'));
            }

            myId++;
            string a = "";
            switch (myId.ToString().Length)
            {
                case 1:
                    a = "V00" + myId;
                    break;
                case 2:
                    a = "V0" + myId;
                    break;
                case 3:
                    a = "V" + myId;
                    break;
                default:
                    break;
            }
            vacancy.id = a;
            if (start <= 0 || end <= 0 || start == end)
            {
                TempData["AlertMessage"] = "Salary is wrong, please check again";
                Error();
                return RedirectToAction("Create");
            }
            if ( vacancy.endAt < DateTime.Now)
            {
                TempData["AlertMessage"] = "Vacancy must have a closed day later than now.";
                Error();
                return RedirectToAction("Create");
            }
            vacancy.salary = start + ",000,000 - " + end + ",000,000 VND";
            vacancy.status = "Open";
            vacancy.applicants_Id = "";
            if (ModelState.IsValid)
            {
                db.descriptions.Add(description);
                db.SaveChanges();
                var d = db.descriptions.OrderByDescending(q => q.id).FirstOrDefault();
                vacancy.descriptionId = Convert.ToInt32(d.id.ToString());

                db.vacancies.Add(vacancy);
                db.SaveChanges();
                TempData["AlertMessage"] = "Create succesfully";
                return RedirectToAction("Create");
            }
            TempData["AlertMessage"] = "Some input is wrong, please check again.";
            Error();
            ViewBag.departmentId = new SelectList(db.departments, "id", "name", vacancy.departmentId);
            return View(vacancy);
        }

        // GET: Admin/vacancies/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            vacancy vacancy = db.vacancies.Find(id);
            if (vacancy == null)
            {
                return HttpNotFound();
            }
            ViewBag.departmentId = new SelectList(db.departments, "id", "name", vacancy.departmentId);          
            return View(vacancy);
        }

        // POST: Admin/vacancies/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,title,endAt,salary,required,status,descriptionId,departmentId,applicants_Id")] vacancy vacancy, [Bind(Include = "id,about,required,interests")] description description, int start, int end)
        {
            if (start <= 0 || end <= 0 || start == end)
            {
                TempData["AlertMessage"] = "Salary is wrong, please check again";
                Error();
                return RedirectToAction("Create");
            }
            vacancy.salary = start + ",000,000 - " + end + ",000,000 VND";
            if (ModelState.IsValid)
            {
                db.Entry(description).State = EntityState.Modified;
                db.SaveChanges();

                vacancy.descriptionId = description.id;
                db.Entry(vacancy).State = EntityState.Modified;
                TempData["AlertMessage"] = "Create succesfully";
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            TempData["AlertMessage"] = "Some input is wrong, please check again.";
            Error();
            ViewBag.departmentId = new SelectList(db.departments, "id", "name", vacancy.departmentId);       
            return View(vacancy);
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
