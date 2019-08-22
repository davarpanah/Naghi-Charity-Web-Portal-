using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;

namespace test1.Models
{
    public enum ManageMessageId
    {
        AddPhoneSuccess,
        ChangePasswordSuccess,
        SetTwoFactorSuccess,
        SetPasswordSuccess,
        RemoveLoginSuccess,
        RemovePhoneSuccess,
        UpdatePersonalInformation,
        PeriodicPaymentFormSuccess,
        OnlinePaymentSuccess,
        AddGallerySuccess,
        Error
    }
    public class IndexViewModel
    {
        public bool HasPassword { get; set; }
        public IList<UserLoginInfo> Logins { get; set; }
        public string PhoneNumber { get; set; }
        public int GroupType { get; set; }
    }
    public class UserActivationModel
    {
         [Display(Name = "نام")]
         public string Us_Name{ get; set; }
         [Display(Name = "نام خانوادگی")]
         public string Us_LName { get; set; }
         [Display(Name = "ایمیل")]
         public string Us_Email { get; set; }
        public int Us_Unic_Number{ get; set; }
         [Display(Name = "فعال/غیرفعال")]
         public bool Us_Active { get; set; }
        [Display(Name = "گروه کاربر")]
        public int UserGroup_Gr_Id { get; set; }
    }
    public class ManageLoginsViewModel
    {
        public IList<UserLoginInfo> CurrentLogins { get; set; }
        public IList<AuthenticationDescription> OtherLogins { get; set; }
    }

    public class SetPasswordViewModel
    {
        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "رمز عبور جدید")]
        public string NewPassword { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "تایید رمز عبور")]
        [Compare("NewPassword", ErrorMessage = "رمز عبور و تایید آن مطابقت ندارند")]
        public string ConfirmPassword { get; set; }
    }

    public class MySelectListItem
    {
        public int value{set;get;}
        public string text{set;get;}
    }
    
    public class ChangePersonalInformation
    {
        [Display(Name = "نام")]
        public string Us_Name { get; set; }

        [Display(Name = "نام خانوادگی")]
        public string Us_LName { get; set;}
        [Display(Name = "تاریخ تولد (mm/dd/yyyy)")]
        [DataType(DataType.Date, ErrorMessage = "تاریخ تولد را به فرمت صحیح وارد نمایید")]
        //[RegularExpression("^(?=\\d{2}([-.,\\/])\\d{2}\\1\\d{4}$)(?:0[1-9]|1\\d|[2][0-8]|29(?!.02.(?!(?!(?:[02468][1-35-79]|[13579][0-13-57-9])00)\\d{2}(?:[02468][048]|[13579][26])))|30(?!.02)|31(?=.(?:0[13578]|10|12))).(?:0[1-9]|1[012]).\\d{4}$", ErrorMessage = "تاریخ تولد را به فرمت صحیح وارد نمایید")]
        public System.DateTime Us_BirthDay { get; set; }
        [RegularExpression(@"^\d+$", ErrorMessage = "شماره تلفن باید بصورت عددی وارد شود")]
        [Display(Name = "تلفن تماس")]
        public string Us_Phone { get; set; }
        [Display(Name = "آدرس منزل")]
        public string Us_HomeAddress { get; set; }
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
        public ChangePersonalInformation()
        {
            Countries = new List<MySelectListItem>();
            States = new List<MySelectListItem>();
            Cities = new List<MySelectListItem>();
        }
    }

    public class ChangePasswordViewModel
    {
        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "رمز عبور جاری")]
        public string OldPassword { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "رمز عبور جدید")]
        public string NewPassword { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "تایید روز عبور")]
        [Compare("NewPassword", ErrorMessage = "رمز عبور و تایید آن مطابقت ندارند")]
        public string ConfirmPassword { get; set; }
    }

    public class AddPhoneNumberViewModel
    {
        [Required]
        [Phone]
        [Display(Name = "شماره تلفن")]
        public string Number { get; set; }
    }

    public class VerifyPhoneNumberViewModel
    {
        [Required]
        [Display(Name = "کد")]
        public string Code { get; set; }

        [Required]
        [Phone]
        [Display(Name = "شماره تلفن")]
        public string PhoneNumber { get; set; }
    }


}