using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace test1.Models
{
    public class HomeViewModel
    {
    }
    public class PeriodicPaymentModel
    {
        [Display(Name = "پیامک")]
        public bool SendSMS { set; get; }
        [Display(Name = "ایمیل")]
        public bool SendEmail { set; get; }
        public int Us_Unic_Number { set; get; }
        [Display(Name = "ثبت نام")]
        public bool IsUser { set; get; }
        public int Dr_Id { get; set; }

        [Display(Name = "تاریخ تولد (mm/dd/yyyy)")]
        [DataType(DataType.Date, ErrorMessage = "تاریخ ثبت را به فرمت صحیح وارد نمایید")]
        public System.DateTime Dr_Reg_Date { get; set; }

        [Display(Name = "نام")]
        public string Us_Name { get; set; }

        [Display(Name = "نام خانوادگی")]
        public string Us_LName { get; set;}
        [Display(Name = "تاریخ تولد (mm/dd/yyyy)")]
        [DataType(DataType.Date, ErrorMessage = "تاریخ تولد را به فرمت صحیح وارد نمایید")]
        public System.DateTime Us_BirthDay { get; set; }
        [RegularExpression(@"^\d+$", ErrorMessage = "شماره تلفن باید بصورت عددی وارد شود")]
        [Display(Name = "تلفن تماس")]
        public string Us_Phone { get; set; }
        [Display(Name = "آدرس منزل")]
        public string Us_HomeAddress { get; set; }
        [Display(Name = "تلفن همراه")]
        public string Us_Mob { get; set; }
        [Display(Name = "ایمیل")]
        [RegularExpression(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$", ErrorMessage = "ادرس ایمیل را به فرمت درست وارد نمایید")]
        public string Us_Email { get; set; }
        [Display(Name = "آدرس محل کار")]
        public string Us_WorkAddress { get; set; }
        [Display(Name = "شهر تولد")]
        public short City_Ct_Id { get; set; }
        public List<MySelectListItem> Countries { set; get; }
        [Display(Name = "کشور محل زندگی")]
        public int SelectedCountry { set; get; }
        public List<MySelectListItem> States { set; get; }
        [Display(Name = "استان محل زندگی")]
        public int SelectedState { set; get; }
        public List<MySelectListItem> Cities { set; get; }
        [Display(Name = "شهر محل زندگی")]
        public int Co_Id { get; set; }
        
        public PeriodicPaymentModel()
        {
            Countries = new List<MySelectListItem>();
            States = new List<MySelectListItem>();
            Cities = new List<MySelectListItem>();
            Dr_Month_Duration = 1;
            Dr_Amount = 80000;
            Dr_HelpDay = 1;
            Dr_CT_Code = 0;//email
            Dr_AccTransaction = true;
            SendEmail = true;
            SendSMS = true;
        }
        [Display(Name = "هر چند ماه-مثلا هر ماه یا هر سه ماه")]
        [Range(1,12,ErrorMessage = "تعداد ماه باید بین 1 تا 12 باشد")]
        [RegularExpression(@"^\d+$", ErrorMessage = "دوره زمانی پرداخت به ماه باید بصورت عددی وارد شود")]
        [Required(ErrorMessage = "دوره زمانی پرداخت به ماه می باید مشخص باشد")]
        public Int16 Dr_Month_Duration { get; set; }
        [RegularExpression(@"^\d+$", ErrorMessage = "مبلغ اهدایی باید بصورت عددی وارد شود")]
        [Display(Name = "مبلغ اهدایی به ریا ل")]
        public Int32 Dr_Amount { get; set; }

        [Range(1, 30, ErrorMessage = "چندمین روز ماه باید بین 1 تا 30 باشد")]
        [Required(ErrorMessage = "روز یادآوری در ماه باید مشخص باشد")]
        [RegularExpression(@"^\d+$", ErrorMessage = "روز یادآوری در ماه باید بصورت عددی وارد شود")]
        [Display(Name = "روز یادآوری در ماه-مثلا ابتدای هر ماه")]
        public Int16 Dr_HelpDay { get; set; }

        [Display(Name = "نحوه یاداوری")]
        public int Dr_CT_Code { get; set; }

        [Display(Name = "واریز به حساب")]
        public Boolean Dr_AccTransaction { get; set; }
        [Display(Name = "واریز به کارت")]
        public Boolean Dr_CardTranscation { get; set; }
        [Display(Name = "مراجعه حضوری به دفتر مشارکتهای مردمی")]
        public Boolean Dr_Inperson { get; set; }
        [Display(Name = "تحویل درب منزل یا محل کار به نماینده خیریه")]
        public Boolean Dr_Representative { get; set; }


        [Display(Name = "تهیه مواد غذایی")]
        public Boolean Dr_Food { get; set; }
        [Display(Name = "تهیه پوشاک")]
        public Boolean Dr_Clothes { get; set; }
        [Display(Name = "تهیه لوازم التحریر")]
        public Boolean Dr_Stationery { get; set; }
        [Display(Name = "تهیه دارو و لوازم پزشکی")]
        public Boolean Dr_Medical { get; set; }
        [Display(Name = "تهیه مواد شوینده و بهداشتی")]
        public Boolean Dr_Cleaner { get; set; }
        [Display(Name = "وقف اموال")]
        public Boolean Dr_Endowment { get; set; }
    }

    public class OnlinePaymentModel
    {
        public int Do_Id { get; set; }

        [Display(Name = "نام")]
        public string Us_Name { get; set; }

        [Display(Name = "نام خانوادگی")]
        public string Us_LName { get; set; }
        [RegularExpression(@"^\d+$", ErrorMessage = "شماره تلفن باید بصورت عددی وارد شود")]
        [Display(Name = "تلفن تماس")]
        public string Us_Phone { get; set; }
        [Display(Name = "آدرس منزل")]
        public string Us_HomeAddress { get; set; }
        [Display(Name = "تلفن همراه")]
        public string Us_Mob { get; set; }
        [Display(Name = "ایمیل")]
        [RegularExpression(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$", ErrorMessage = "ادرس ایمیل را به فرمت درست وارد نمایید")]
        public string Us_Email { get; set; }
        [Display(Name = "آدرس محل کار")]
        public string Us_WorkAddress { get; set; }
        [Display(Name = "شهر تولد")]
        public short City_Ct_Id { get; set; }
        public List<MySelectListItem> Countries { set; get; }
        [Display(Name = "کشور محل زندگی")]
        public int SelectedCountry { set; get; }
        public List<MySelectListItem> States { set; get; }
        [Display(Name = "استان محل زندگی")]
        public int SelectedState { set; get; }
        public List<MySelectListItem> Cities { set; get; }

        public OnlinePaymentModel()
        {
            Countries = new List<MySelectListItem>();
            States = new List<MySelectListItem>();
            Cities = new List<MySelectListItem>();
            PaymentType_Id = 0;//online
            Do_Type = 0;//Cash/Noncash
        }
        public int Do_Type { get; set; }
        public int DoR_Amount { get; set; }
        public int Good_Gd_Id { get; set; }
        public byte PaymentType_Id { get; set; }

        [DataType(DataType.Date, ErrorMessage = "تاریخ پرداخت را به فرمت صحیح وارد نمایید")]
        [DisplayFormat(DataFormatString = "{0:dd MMM yyyy}")]
        [Display(Name = "تاریخ پرداخت (mm/dd/yyyy)")]
        public System.DateTime Do_Date { get; set; }
        public bool IsUser { set; get; } 
    }
}