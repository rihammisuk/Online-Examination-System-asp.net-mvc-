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
    public class AdminController : Controller
    {
        private Exam_QuizEntities db = new Exam_QuizEntities();

        // GET: /Admin/
        public ActionResult Index()
        {
            return View(db.Admin_tbl.ToList());
        }

        // GET: /Admin/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Admin_tbl admin_tbl = db.Admin_tbl.Find(id);
            if (admin_tbl == null)
            {
                return HttpNotFound();
            }
            return View(admin_tbl);
        }

        // GET: /Admin/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: /Admin/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="admin_id,admin_name,admin_password")] Admin_tbl admin_tbl)
        {
            if (ModelState.IsValid)
            {
                db.Admin_tbl.Add(admin_tbl);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(admin_tbl);
        }

        // GET: /Admin/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Admin_tbl admin_tbl = db.Admin_tbl.Find(id);
            if (admin_tbl == null)
            {
                return HttpNotFound();
            }
            return View(admin_tbl);
        }

        // POST: /Admin/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="admin_id,admin_name,admin_password")] Admin_tbl admin_tbl)
        {
            if (ModelState.IsValid)
            {
                db.Entry(admin_tbl).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(admin_tbl);
        }

        // GET: /Admin/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Admin_tbl admin_tbl = db.Admin_tbl.Find(id);
            if (admin_tbl == null)
            {
                return HttpNotFound();
            }
            return View(admin_tbl);
        }

        // POST: /Admin/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Admin_tbl admin_tbl = db.Admin_tbl.Find(id);
            db.Admin_tbl.Remove(admin_tbl);
            db.SaveChanges();
            return RedirectToAction("Index");
        }


        public ActionResult Login()
        {

            return View();

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(Admin_tbl ut)
        {
            if (ModelState.IsValid)
            {
                using (Exam_QuizEntities db = new Exam_QuizEntities())
                {

                    var log = db.Admin_tbl.FirstOrDefault(model => model.admin_name.Equals(ut.admin_name) && model.admin_password.Equals(ut.admin_password));
                    if (log != null)
                    {

                        Session["admin_name"] = log.admin_name;

                        return RedirectToAction("AdminProfile", "Admin");
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
            return RedirectToAction("Login", "Admin");


        }

        public ActionResult AdminProfile()
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
