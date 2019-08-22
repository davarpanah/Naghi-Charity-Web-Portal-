using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace test1.Models
{
    public class PatientModel
    {
        [Display(Name = "شماره مددجو")]
        public int Pt_Id { get; set; }
        [Display(Name = "نام")]
        public string Pt_Name { get; set; }
        [Display(Name = "نام خانوادگی")]
        public string Pt_LName { get; set; }
        [Display(Name = "شهر")]
        public Int16 Pt_Ct_Code { get; set; }
        [Display(Name = "کشور")]
        public int SelectedCountry { set; get; }
        [Display(Name = "استان")]
        public int SelectedState { set; get; }
        [Display(Name = "سید یا عام")]
        [Required(ErrorMessage = "سید یا عام بودن را مشخص نمایید")]
        public Int16 Pt_Seyyed { get; set; }
        [Display(Name = "وضعیت سرپرست")]
        [Required(ErrorMessage = "وضعیت سرپرست را مشخص نمایید")]
        public Int16 Pt_Supporter { get; set; }
        [Display(Name = "تاریخ تولد ")]//(mm/dd/yyyy)
        [DataType(DataType.Date, ErrorMessage = "تاریخ ثبت را به فرمت صحیح وارد نمایید")]
        public DateTime Pt_BirthDay { get; set; }
        [Display(Name = "شرح بیماری")]
        public string Pt_Sickness { get; set; }
        [Display(Name = "جنسیت")]
        [Required(ErrorMessage = "جنسیت را مشخص نمایید")]
        public Int16 Pt_Sex { get; set; }
        [RegularExpression(@"^\d+$", ErrorMessage = "شماره تلفن باید بصورت عددی وارد شود")]
        [Display(Name = "تلفن تماس")]
        public string Pt_Tel { get; set; }
        [Display(Name = "آدرس منزل")]
        public string Pt_Address { get; set; }
        [Display(Name = "عکس پرسنلی")]
        public List<MySelectListItem> States { set; get; }
        public List<MySelectListItem> Cities { set; get; }
        public string Pt_Photo_Path { get; set; }
        [Display(Name = "نام و نام خانوادگی")]
        public string Pt_CompleteName { set; get; }
    }

    public class PatientListModel
    {
        public Int16 SeletedSeyed { set; get; }
        public Int16 SeletedSupporter { set; get; }
        public Dictionary<int, string> Sexes { get; set; }
        public List<MySelectListItem> Countries { set; get; }

        [Display(Name = "سید یا عام بودن را انتخاب نمایید")]
        public Dictionary<int, string> Seyyeds { get; set; }

        [Display(Name = "نوع سرپرست را انتخاب نمایید")]
        public Dictionary<int, string> Supporters { get; set; }
        public List<PatientModel> Patients { set; get; }

        public PatientListModel()
        {
            Patients = new List<PatientModel>();
            Seyyeds = new Dictionary<int, string>()
            {
                { 0, "فرقی ندارد"},                
                { 1, "سید"}, 
                { 2, "عام"}
            };
            Supporters = new Dictionary<int, string>()
            {
                { 0, "فرقی ندارد"},                
                { 1, "بدون سرپرست"}, 
                { 2, "با سرپرست"},
                { 3, "بد سرپرست"}
            };
            Sexes = new Dictionary<int, string>()
            {
                { 0, "فرقی ندارد"},                
                { 1, "مرد"}, 
                { 2, "زن"}
            };
        }
    }
}