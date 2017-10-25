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
    public class ExamController : Controller
    {
        private Exam_QuizEntities db = new Exam_QuizEntities();

        // GET: /Exam/
        public ActionResult Index()
        {
            var exam_tbl = db.Exam_tbl.Include(e => e.Subject_tbl);
            return View(exam_tbl.ToList());
        }

        // GET: /Exam/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Exam_tbl exam_tbl = db.Exam_tbl.Find(id);
            if (exam_tbl == null)
            {
                return HttpNotFound();
            }
            return View(exam_tbl);
        }

        // GET: /Exam/Create
        public ActionResult Create()
        {
            ViewBag.subject_id = new SelectList(db.Subject_tbl, "subject_id", "subject_title");
            return View();
        }

        // POST: /Exam/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="exam_id,subject_id,exam_title")] Exam_tbl exam_tbl)
        {
            if (ModelState.IsValid)
            {
                db.Exam_tbl.Add(exam_tbl);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.subject_id = new SelectList(db.Subject_tbl, "subject_id", "subject_title", exam_tbl.subject_id);
            return View(exam_tbl);
        }

        // GET: /Exam/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Exam_tbl exam_tbl = db.Exam_tbl.Find(id);
            if (exam_tbl == null)
            {
                return HttpNotFound();
            }
            ViewBag.subject_id = new SelectList(db.Subject_tbl, "subject_id", "subject_title", exam_tbl.subject_id);
            return View(exam_tbl);
        }

        // POST: /Exam/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="exam_id,subject_id,exam_title")] Exam_tbl exam_tbl)
        {
            if (ModelState.IsValid)
            {
                db.Entry(exam_tbl).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.subject_id = new SelectList(db.Subject_tbl, "subject_id", "subject_title", exam_tbl.subject_id);
            return View(exam_tbl);
        }

        // GET: /Exam/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Exam_tbl exam_tbl = db.Exam_tbl.Find(id);
            if (exam_tbl == null)
            {
                return HttpNotFound();
            }
            return View(exam_tbl);
        }

        // POST: /Exam/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Exam_tbl exam_tbl = db.Exam_tbl.Find(id);
            db.Exam_tbl.Remove(exam_tbl);
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
