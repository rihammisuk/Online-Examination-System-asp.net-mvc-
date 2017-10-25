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
    public class CandidateController : Controller
    {
        private Exam_QuizEntities db = new Exam_QuizEntities();

        // GET: /Candidate/
        public ActionResult Index()
        {
            return View(db.Candidate_tbl.ToList());
        }

        // GET: /Candidate/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Candidate_tbl candidate_tbl = db.Candidate_tbl.Find(id);
            if (candidate_tbl == null)
            {
                return HttpNotFound();
            }
            return View(candidate_tbl);
        }

        // GET: /Candidate/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: /Candidate/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="candidate_id,candidate_name,candidate_password,candidate_email,candidate_contact,candidate_institution")] Candidate_tbl candidate_tbl)
        {
            if (ModelState.IsValid)
            {
                db.Candidate_tbl.Add(candidate_tbl);
                db.SaveChanges();
                if (Session["admin_name"]!=null)
                {
                    return RedirectToAction("Index");
                    
                    
                }
                else
                {
                    return RedirectToAction("Login");
                }
                
            }

            return View(candidate_tbl);
        }

        // GET: /Candidate/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Candidate_tbl candidate_tbl = db.Candidate_tbl.Find(id);
            if (candidate_tbl == null)
            {
                return HttpNotFound();
            }
            return View(candidate_tbl);
        }

        // POST: /Candidate/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="candidate_id,candidate_name,candidate_password,candidate_email,candidate_contact,candidate_institution")] Candidate_tbl candidate_tbl)
        {
            if (ModelState.IsValid)
            {
                db.Entry(candidate_tbl).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(candidate_tbl);
        }

        // GET: /Candidate/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Candidate_tbl candidate_tbl = db.Candidate_tbl.Find(id);
            if (candidate_tbl == null)
            {
                return HttpNotFound();
            }
            return View(candidate_tbl);
        }

        // POST: /Candidate/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Candidate_tbl candidate_tbl = db.Candidate_tbl.Find(id);
            db.Candidate_tbl.Remove(candidate_tbl);
            db.SaveChanges();
            return RedirectToAction("Index");
        }



        public ActionResult Login()
        {

            return View();

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(Candidate_Login ut)
        {
            if (ModelState.IsValid)
            {
                using (Exam_QuizEntities db = new Exam_QuizEntities())
                {
                    
                    
                    var log = db.Candidate_tbl.FirstOrDefault(model => model.candidate_name.Equals(ut.candidate_name) && model.candidate_password.Equals(ut.candidate_password));
                    if (log != null)
                    {
                        

                        Session["candidate_name"] = log.candidate_name;


                        return RedirectToAction("Index", "SelectExam");
                    }
                    else
                    {
                        ViewBag.Message = "Username or password incorrect";
                        Response.Write("<script> alert('User Name or Password is wrong')</script>");

                    }
                }

            }

            return View();
        }



        public ActionResult Logout()
        {
            Session.Clear();
            return RedirectToAction("Login", "Candidate");


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
