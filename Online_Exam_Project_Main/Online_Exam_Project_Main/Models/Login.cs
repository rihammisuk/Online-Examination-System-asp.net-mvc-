using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Online_Exam_Project_Main.Models
{
    public class Login
    {



        [Required(ErrorMessage = "Admin Name Required")]
        [Display(Name = "Admin Name ")]
        public string admin_name { get; set; }

        [Required(ErrorMessage = "Password Required")]
        [DataType(DataType.Password)]
        [Display(Name = "Password ")]
        public string admin_password { get; set; }


    }


    public class Candidate_Login
    {
        [Required(ErrorMessage = "Candidate Name Required")]
        [Display(Name = "Candidate Name ")]
        public string candidate_name { get; set; }

        [Required(ErrorMessage = "Password Required")]
        [DataType(DataType.Password)]
        [Display(Name = "Password ")]
        public string candidate_password { get; set; }


    }
}