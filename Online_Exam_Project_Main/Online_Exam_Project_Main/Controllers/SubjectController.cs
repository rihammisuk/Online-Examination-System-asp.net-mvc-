using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Online_Exam_Project_Main.Models;

namespace Online_Exam_Project_Main.Controllers
{
    public class SubjectController : Controller
    {
        private Exam_QuizEntities db = new Exam_QuizEntities();

        // GET: /Subject/
        public ActionResult Index()
        {
            return View(db.Subject_tbl.ToList());
        }

        // GET: /Subject/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Subject_tbl subject_tbl = db.Subject_tbl.Find(id);
            if (subject_tbl == null)
            {
                return HttpNotFound();
            }
            return View(subject_tbl);
        }

        // GET: /Subject/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: /Subject/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="subject_id,subject_title,subject_code")] Subject_tbl subject_tbl)
        {
            if (ModelState.IsValid)
            {
                db.Subject_tbl.Add(subject_tbl);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(subject_tbl);
        }

        // GET: /Subject/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Subject_tbl subject_tbl = db.Subject_tbl.Find(id);
            if (subject_tbl == null)
            {
                return HttpNotFound();
            }
            return View(subject_tbl);
        }

        // POST: /Subject/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="subject_id,subject_title,subject_code")] Subject_tbl subject_tbl)
        {
            if (ModelState.IsValid)
            {
                db.Entry(subject_tbl).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(subject_tbl);
        }

        // GET: /Subject/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Subject_tbl subject_tbl = db.Subject_tbl.Find(id);
            if (subject_tbl == null)
            {
                return HttpNotFound();
            }
            return View(subject_tbl);
        }

        // POST: /Subject/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Subject_tbl subject_tbl = db.Subject_tbl.Find(id);
            db.Subject_tbl.Remove(subject_tbl);
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
