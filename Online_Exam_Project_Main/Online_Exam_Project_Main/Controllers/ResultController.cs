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
    public class ResultController : Controller
    {
        private Exam_QuizEntities db = new Exam_QuizEntities();

        // GET: /Result/
        public ActionResult Index()
        {
            var result_tbl = db.Result_tbl.Include(r => r.Candidate_tbl).Include(r => r.Exam_tbl);
            return View(result_tbl.ToList());
        }

        // GET: /Result/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Result_tbl result_tbl = db.Result_tbl.Find(id);
            if (result_tbl == null)
            {
                return HttpNotFound();
            }
            return View(result_tbl);
        }

        // GET: /Result/Create
        public ActionResult Create()
        {

            int score =(int) Session["correctAns"];
            if (score>=9)
            {
                ViewBag.grade = "A+";
                ViewBag.quality = "Best";
            }
            else if (score >= 8)
            {
                ViewBag.grade = "A";
                ViewBag.quality = "Better";
            }
            else if (score >= 5)
            {
                ViewBag.grade = "B";
                ViewBag.quality = "Good";
            }
            else  
            {
                ViewBag.grade = "F";
                ViewBag.quality = "Fail";
            }

            Session["grade"]=ViewBag.grade;
            Session["quality"] = ViewBag.quality;

            ViewBag.candidate_id = 1;
            ViewBag.exam_id = 1;
            return View();
        }

        // POST: /Result/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="result_id,exam_id,candidate_id,total,grade,quality")] Result_tbl result_tbl)
        {
            if (ModelState.IsValid)
            {
                db.Result_tbl.Add(result_tbl);
                db.SaveChanges();
                return RedirectToAction("ShowResult");
            }

            ViewBag.candidate_id = new SelectList(db.Candidate_tbl, "candidate_id", "candidate_name", result_tbl.candidate_id);
            ViewBag.exam_id = new SelectList(db.Exam_tbl, "exam_id", "exam_title", result_tbl.exam_id);
            return View(result_tbl);
        }

        // GET: /Result/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Result_tbl result_tbl = db.Result_tbl.Find(id);
            if (result_tbl == null)
            {
                return HttpNotFound();
            }
            ViewBag.candidate_id = new SelectList(db.Candidate_tbl, "candidate_id", "candidate_name", result_tbl.candidate_id);
            ViewBag.exam_id = new SelectList(db.Exam_tbl, "exam_id", "exam_title", result_tbl.exam_id);
            return View(result_tbl);
        }

        // POST: /Result/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="result_id,exam_id,candidate_id,total,grade,quality")] Result_tbl result_tbl)
        {
            if (ModelState.IsValid)
            {
                db.Entry(result_tbl).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.candidate_id = new SelectList(db.Candidate_tbl, "candidate_id", "candidate_name", result_tbl.candidate_id);
            ViewBag.exam_id = new SelectList(db.Exam_tbl, "exam_id", "exam_title", result_tbl.exam_id);
            return View(result_tbl);
        }

        // GET: /Result/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Result_tbl result_tbl = db.Result_tbl.Find(id);
            if (result_tbl == null)
            {
                return HttpNotFound();
            }
            return View(result_tbl);
        }

        // POST: /Result/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Result_tbl result_tbl = db.Result_tbl.Find(id);
            db.Result_tbl.Remove(result_tbl);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult ShowResult()
        {

            return View();

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
