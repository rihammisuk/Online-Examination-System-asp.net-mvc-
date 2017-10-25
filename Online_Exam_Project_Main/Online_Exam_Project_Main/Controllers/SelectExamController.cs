using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Online_Exam_Project_Main.Models;

namespace Online_Exam_Project_Main.Controllers
{
    public class SelectExamController : Controller
    {
        private Exam_QuizEntities db = new Exam_QuizEntities();
        //
        // GET: /SelectExam/
        public ActionResult Index()
        {
            int count = 0;
            var name = db.Exam_tbl.ToList();
            SelectList list = new SelectList(name, "exam_id", "exam_title");
            DrpList drp = new DrpList();
            drp.Examlist = name;
            drp.QuestionNo = 1;
            ViewBag.name = list;
            Session["name"] = ViewBag.name;




            return View(drp);
        }
        [HttpPost]
        public void SetFromDrp(int val)
        {
            ViewBag.drpVal = val;
        }
	}
}