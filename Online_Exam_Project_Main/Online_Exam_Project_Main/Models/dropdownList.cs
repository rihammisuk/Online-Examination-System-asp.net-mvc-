using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Online_Exam_Project_Main.Models
{
    public class CustomModel
    {
        public List<DrpList> Examlist { get; set; }
        public int ExamId { get; set; }
    }

    public class DrpList
    {
        public string exam_title { get; set; }
        public int examid { get; set; }

        public List<Exam_tbl> Examlist { get; set; }
        public int QuestionNo { get; set; }
    }
}