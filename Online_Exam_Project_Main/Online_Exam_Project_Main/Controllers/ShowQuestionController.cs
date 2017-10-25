using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Online_Exam_Project_Main.Models;

namespace Online_Exam_Project_Main.Controllers
{
    public class ShowQuestionController : Controller
    {
        //
        // GET: /ShowQuestion/
        Exam_QuizEntities db = new Exam_QuizEntities();


        
        [HttpPost]
        public ActionResult Index(DrpList drp)
        {

            ViewBag.drpData = drp;
            ViewBag.questionNo = drp.QuestionNo;

            TempData["a"] = drp.QuestionNo;

            if (drp.QuestionNo == 1)
            //if (questionNo == null || id == null)
            {
                Session["exid"] = drp.examid;

                Questions_tbl SingleQuestion = db.Questions_tbl.SingleOrDefault(m => m.qus_id == 1 && m.exam_id == drp.examid);

                
                TempData["qData"] = SingleQuestion;
                return RedirectToAction("NextQuestion");
                //return View(SingleQuestion);
            }
            else
            {


                Questions_tbl SingleQuestion = db.Questions_tbl.SingleOrDefault(m => m.qus_id == drp.QuestionNo && m.exam_id == drp.examid);
                int qus = (int)drp.QuestionNo;

                //for (int i = 0; i <= drp.QuestionNo; i++)
                //{
                //    ViewBag.questionNo = qusno + i;
                //}


                return View(SingleQuestion);
            }

        }


        int add = 0;


       

        public ActionResult NextQuestion()
        {
            int qNo = (int)TempData["a"];
            ViewBag.questionNo = qNo;
            Questions_tbl a = (Questions_tbl)TempData["qData"];

            return View(a);

        }
        [HttpPost]
        public ActionResult NextQuestion(Questions_tbl aaa)
        {
            if (aaa.correct_ans == aaa.selectedvalue && aaa.qus_id!=1)
            {
                Session["correctAns"] = Convert.ToInt32(Session["correctAns"]) + 1;
            }
            else if (aaa.correct_ans == aaa.selectedvalue && aaa.qus_id == 1)
            {
                Session["correctAns"] = 1;
            }

            if (aaa.qus_id == 10)
            {
                return RedirectToAction("Create","Result");

            }
            int qId = (int)aaa.qus_id + 1;
            Questions_tbl SingleQuestion = db.Questions_tbl.SingleOrDefault(m => m.qus_id == qId && m.exam_id == aaa.exam_id);
            
            
            ViewBag.questionNo = qId;
            TempData["a"] = SingleQuestion.qus_id;
            TempData["qData"] = SingleQuestion;
            return RedirectToAction("NextQuestion");

        }
    }
	}
