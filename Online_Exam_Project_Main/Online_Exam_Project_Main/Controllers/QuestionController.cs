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
    public class QuestionController : Controller
    {
        private Exam_QuizEntities db = new Exam_QuizEntities();

        // GET: /Question/
        public ActionResult Index()
        {
            var questions_tbl = db.Questions_tbl.Include(q => q.Admin_tbl).Include(q => q.Exam_tbl);
            return View(questions_tbl.ToList());
        }

        // GET: /Question/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Questions_tbl questions_tbl = db.Questions_tbl.Find(id);
            if (questions_tbl == null)
            {
                return HttpNotFound();
            }
            return View(questions_tbl);
        }

        // GET: /Question/Create
        public ActionResult Create()
        {
            ViewBag.admin_id = new SelectList(db.Admin_tbl, "admin_id", "admin_name");
            ViewBag.exam_id = new SelectList(db.Exam_tbl, "exam_id", "exam_title");
            return View();
        }

        // POST: /Question/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="all_qus_id,qus_id,admin_id,exam_id,qus_title,ans1,ans2,ans3,ans4,correct_ans")] Questions_tbl questions_tbl)
        {
            if (ModelState.IsValid)
            {
                db.Questions_tbl.Add(questions_tbl);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.admin_id = new SelectList(db.Admin_tbl, "admin_id", "admin_name", questions_tbl.admin_id);
            ViewBag.exam_id = new SelectList(db.Exam_tbl, "exam_id", "exam_title", questions_tbl.exam_id);
            return View(questions_tbl);
        }

        // GET: /Question/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Questions_tbl questions_tbl = db.Questions_tbl.Find(id);
            if (questions_tbl == null)
            {
                return HttpNotFound();
            }
            ViewBag.admin_id = new SelectList(db.Admin_tbl, "admin_id", "admin_name", questions_tbl.admin_id);
            ViewBag.exam_id = new SelectList(db.Exam_tbl, "exam_id", "exam_title", questions_tbl.exam_id);
            return View(questions_tbl);
        }

        // POST: /Question/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="all_qus_id,qus_id,admin_id,exam_id,qus_title,ans1,ans2,ans3,ans4,correct_ans")] Questions_tbl questions_tbl)
        {
            if (ModelState.IsValid)
            {
                db.Entry(questions_tbl).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.admin_id = new SelectList(db.Admin_tbl, "admin_id", "admin_name", questions_tbl.admin_id);
            ViewBag.exam_id = new SelectList(db.Exam_tbl, "exam_id", "exam_title", questions_tbl.exam_id);
            return View(questions_tbl);
        }

        // GET: /Question/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Questions_tbl questions_tbl = db.Questions_tbl.Find(id);
            if (questions_tbl == null)
            {
                return HttpNotFound();
            }
            return View(questions_tbl);
        }

        // POST: /Question/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Questions_tbl questions_tbl = db.Questions_tbl.Find(id);
            db.Questions_tbl.Remove(questions_tbl);
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
