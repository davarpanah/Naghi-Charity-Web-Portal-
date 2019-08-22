using System;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using test1.Models;
using System.Data.SqlClient;
using System.Data;
using System.Globalization;
using System.Collections.Generic;
using System.Net.Mail;
using System.Net;
using test1.Models;
using System.IO;

namespace test1.Controllers
{
    [Authorize]
    public class ManageController : Controller
    {
        private ApplicationSignInManager _signInManager;
        private ApplicationUserManager _userManager;

        public ManageController()
        {
        }

        public ManageController(ApplicationUserManager userManager, ApplicationSignInManager signInManager)
        {
            UserManager = userManager;
            SignInManager = signInManager;
        }

        public ApplicationSignInManager SignInManager
        {
            get
            {
                return _signInManager ?? HttpContext.GetOwinContext().Get<ApplicationSignInManager>();
            }
            private set 
            { 
                _signInManager = value; 
            }
        }

        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }

        //
        // GET: /Manage/Index
        public async Task<ActionResult> Index(ManageMessageId? message)
        {
            ViewBag.StatusMessage =
                message == ManageMessageId.ChangePasswordSuccess ? "پسورد شما تغییر یافته است"
                : message == ManageMessageId.SetPasswordSuccess ? "پسورد شما ست شده است"
                : message == ManageMessageId.Error ? "خطایی رخ داده است"
                : message == ManageMessageId.AddPhoneSuccess ? "شماره تلفن شما اضاه شده است"
                : message == ManageMessageId.RemovePhoneSuccess ? "شماره تلفن شما حذف شده است"
                : message == ManageMessageId.UpdatePersonalInformation ? "اطلاعات شما با موفقیت ذخیره گردید"
                : message == ManageMessageId.PeriodicPaymentFormSuccess? "فرم حمایت دوره ای به درستی تکمیل گردید"
                : message == ManageMessageId.OnlinePaymentSuccess? "پرداخت آنلاین به درستی انجام شد"
                : message == ManageMessageId.AddGallerySuccess ? "اضافه نمودن به درستی انجام شد"
                : "";

            var UserGroupType = 1;
            var userId = User.Identity.GetUserId();
            Connection.Connection con = new Connection.Connection();
            DataSet ds = new DataSet();
            ds = con.UserData(User.Identity.GetUserId());
            if (ds.Tables[0].Rows.Count == 1)
            {
                DataRow dr = ds.Tables[0].Rows[0];
                UserGroupType=Convert.ToInt16(dr["UserGroup_Gr_Id"]);
            }

            var model = new IndexViewModel
            {
                HasPassword = HasPassword(),
                PhoneNumber = await UserManager.GetPhoneNumberAsync(userId),
                Logins = await UserManager.GetLoginsAsync(userId),
                GroupType = UserGroupType
            };
            return View(model);
        }

        //
        // POST: /Manage/RemoveLogin
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> RemoveLogin(string loginProvider, string providerKey)
        {
            ManageMessageId? message;
            var result = await UserManager.RemoveLoginAsync(User.Identity.GetUserId(), new UserLoginInfo(loginProvider, providerKey));
            if (result.Succeeded)
            {
                var user = await UserManager.FindByIdAsync(User.Identity.GetUserId());
                if (user != null)
                {
                    await SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);
                }
                message = ManageMessageId.RemoveLoginSuccess;
            }
            else
            {
                message = ManageMessageId.Error;
            }
            return RedirectToAction("ManageLogins", new { Message = message });
        }

        public ActionResult WishListUpdation()
        {
            DataSet ds = new DataSet();
            Connection.Connection con = new Connection.Connection();
            WhishUpdationModel lmd = new WhishUpdationModel();
            ds = con.GoodGroupDataNull();
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                lmd.Groups.Add(new GoodGroup
                {
                    GG_Id = Convert.ToInt16(dr["GG_Id"]),
                    GG_Name = dr["GG_Name"].ToString()
                });
            }
            lmd.SeletedGroup = lmd.Groups[0].GG_Id; 
            ds = con.GoodData(lmd.SeletedGroup);
            lmd.Goods.Clear();
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                lmd.Goods.Add(new GoodModel
                {
                    Gd_Id = Convert.ToInt16(dr["Gd_Id"]),
                    Gd_Name = dr["Gd_Name"].ToString(),
                    Gd_RequiredNo = Convert.ToInt16(dr["Gd_RequiredNo"])
                });
            }
            lmd.AddingGood.Gd_Name = "";
            lmd.AddingGood.Gd_RequiredNo = 0;
            lmd.AddingGood.GG_Id = Convert.ToInt16(lmd.SeletedGroup);
            return View(lmd);
        }

        [HttpPost]
        public ActionResult WishListUpdation(int SeletedGroup)
        {
            //if (ModelState.IsValid)
            //{ 
                DataSet ds = new DataSet();
                Connection.Connection con = new Connection.Connection();
                WhishUpdationModel lmd = new WhishUpdationModel();
                ds = con.GoodGroupDataNull();
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    lmd.Groups.Add(new GoodGroup
                    {
                        GG_Id = Convert.ToInt16(dr["GG_Id"]),
                        GG_Name = dr["GG_Name"].ToString()
                    });
                }
            //}
            lmd.SeletedGroup = SeletedGroup;
            ds = con.GoodData(lmd.SeletedGroup);
            lmd.Goods.Clear();
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                lmd.Goods.Add(new GoodModel
                {
                    Gd_Id = Convert.ToInt16(dr["Gd_Id"]),
                    Gd_Name = dr["Gd_Name"].ToString(),
                    Gd_RequiredNo = Convert.ToInt16(dr["Gd_RequiredNo"])
                });
            }
            lmd.AddingGood.GG_Id = Convert.ToInt16(lmd.SeletedGroup);
            return View(lmd);
        }

        public ActionResult AddWishListItem(int SelectedGroup)
        {
            GoodModel lmd = new GoodModel();
            lmd.GG_Id = Convert.ToInt16(SelectedGroup);
            lmd.Gd_Name = "";
            lmd.Gd_RequiredNo = 0;
            return View(lmd);
        }
        
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public Boolean SelectedWishListAjax(WhishUpdationModel lmd)
        {
            Boolean TheResult = false;
            /*if (!ModelState.IsValid)
            {
                return TheResult;
            }
             */
            Connection.Connection con = new Connection.Connection();
            foreach (var item in lmd.Goods)
            { 
                TheResult = con.UpdateGood(item);
                if (TheResult == false)
                    break;
            }
            return TheResult;
        }
        
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public Boolean AddWishListItemAjax(WhishUpdationModel lmd)
        {
            Boolean TheResult = false;
            if (!ModelState.IsValid)
                return TheResult;
            Connection.Connection con = new Connection.Connection();
            TheResult = con.AddGood(lmd.AddingGood);
            return TheResult;
        }

        //
        // GET: /Manage/AddPhoneNumber
        public ActionResult AddPhoneNumber()
        {
            return View();
        }

        //
        // POST: /Manage/AddPhoneNumber
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> AddPhoneNumber(AddPhoneNumberViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            // Generate the token and send it
            var code = await UserManager.GenerateChangePhoneNumberTokenAsync(User.Identity.GetUserId(), model.Number);
            if (UserManager.SmsService != null)
            {
                var message = new IdentityMessage
                {
                    Destination = model.Number,
                    Body = "کد امنیتی شما عبارت است از " + code
                };
                await UserManager.SmsService.SendAsync(message);
            }
            return RedirectToAction("VerifyPhoneNumber", new { PhoneNumber = model.Number });
        }

        //
        // GET: /Manage/VerifyPhoneNumber
        public async Task<ActionResult> VerifyPhoneNumber(string phoneNumber)
        {
            var code = await UserManager.GenerateChangePhoneNumberTokenAsync(User.Identity.GetUserId(), phoneNumber);
            // Send an SMS through the SMS provider to verify the phone number
            return phoneNumber == null ? View("Error") : View(new VerifyPhoneNumberViewModel { PhoneNumber = phoneNumber });
        }

        //
        // POST: /Manage/VerifyPhoneNumber
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> VerifyPhoneNumber(VerifyPhoneNumberViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var result = await UserManager.ChangePhoneNumberAsync(User.Identity.GetUserId(), model.PhoneNumber, model.Code);
            if (result.Succeeded)
            {
                var user = await UserManager.FindByIdAsync(User.Identity.GetUserId());
                if (user != null)
                {
                    await SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);

                    Connection.Connection con = new Connection.Connection();
                    Boolean Status = con.UpdateUserPhone(User.Identity.GetUserId(), model.PhoneNumber);
                }
                return RedirectToAction("Index", new { Message = ManageMessageId.AddPhoneSuccess });
            }
            // If we got this far, something failed, redisplay form
            ModelState.AddModelError("", "خطا در تایید شماره تلفن");
            return View(model);
        }

        //
        // GET: /Manage/RemovePhoneNumber
        public async Task<ActionResult> RemovePhoneNumber()
        {
            var result = await UserManager.SetPhoneNumberAsync(User.Identity.GetUserId(), null);
            if (!result.Succeeded)
            {
                return RedirectToAction("Index", new { Message = ManageMessageId.Error });
            }
            var user = await UserManager.FindByIdAsync(User.Identity.GetUserId());
            if (user != null)
            {
                await SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);

                Connection.Connection con = new Connection.Connection();
                Boolean Status = con.UpdateUserPhone(User.Identity.GetUserId(), "");
            }
            return RedirectToAction("Index", new { Message = ManageMessageId.RemovePhoneSuccess });
        }

        //
        // GET: /Manage/ChangePassword
        public ActionResult ChangePassword()
        {
            return View();
        }

        //
        // POST: /Manage/ChangePassword
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ChangePassword(ChangePasswordViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var result = await UserManager.ChangePasswordAsync(User.Identity.GetUserId(), model.OldPassword, model.NewPassword);
            if (result.Succeeded)
            {
                var user = await UserManager.FindByIdAsync(User.Identity.GetUserId());
                if (user != null)
                {
                    await SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);
                }
                return RedirectToAction("Index", new { Message = ManageMessageId.ChangePasswordSuccess });
            }
            AddErrors(result);
            return View(model);
        }

        //
        // GET: /Manage/SetPassword
        public ActionResult SetPassword()
        {
            return View();
        }
        public async Task<ActionResult> UserActivation()
        {
            List<UserActivationModel> lmd = new List<UserActivationModel>();
                        var user = await UserManager.FindByIdAsync(User.Identity.GetUserId());
                        if (user != null)
                        {
                            Connection.Connection con = new Connection.Connection();
                            DataSet ds = new DataSet();
                            ds = con.ActivatedUsersData(User.Identity.GetUserId());
                            foreach (DataRow dr in ds.Tables[0].Rows)
                            {
                                lmd.Add(new UserActivationModel
                                {
                                    Us_Name = dr["Us_Name"].ToString(),
                                    Us_LName = dr["Us_LName"].ToString(),
                                    //Us_Id = Convert.ToInt32(dr["Us_Id"]),
                                    Us_Email = dr["Us_Email"].ToString(),
                                    Us_Unic_Number = Convert.ToInt32(dr["Us_Unic_Number"]),
                                    Us_Active = Convert.ToBoolean(dr["Us_Active"])
                                });
                            }
                        }
            return View(lmd);
        }
        [HttpPost]
        public ActionResult UserActivation(List<UserActivationModel> lmd)
        {
            Boolean TheResult = true;
            Connection.Connection con = new Connection.Connection();
            DataSet ds = new DataSet();
            foreach (var item in lmd)
            {
                TheResult = con.ActivateUser(item);
                if (TheResult == false)
                    break;
            }
            return View(lmd);
        }
        public async Task<ActionResult> UserGroupChange()
        {
            List<UserActivationModel> lmd = new List<UserActivationModel>();
            var user = await UserManager.FindByIdAsync(User.Identity.GetUserId());
            if (user != null)
            {
                Connection.Connection con = new Connection.Connection();
                DataSet ds = new DataSet();
                ds = con.ChangeUserGroupData(User.Identity.GetUserId());
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    lmd.Add(new UserActivationModel
                    {
                        Us_Name = dr["Us_Name"].ToString(),
                        Us_LName = dr["Us_LName"].ToString(),
                        Us_Email = dr["Us_Email"].ToString(),
                        Us_Unic_Number = Convert.ToInt32(dr["Us_Unic_Number"]),
                        UserGroup_Gr_Id = Convert.ToInt32(dr["UserGroup_Gr_Id"])
                    });
                }
            }
            return View(lmd);
        }
        [HttpPost]
        public ActionResult UserGroupChange(List<UserActivationModel> lmd)
        {
            Boolean TheResult = true;
            Connection.Connection con = new Connection.Connection();
            DataSet ds = new DataSet();
            foreach (var item in lmd)
            {
                TheResult = con.ChangeUserGroup(item);
                if (TheResult == false)
                    break;
            }
            return View(lmd);
        }

        [HttpPost]
        public Boolean SendSMStoAll(List<PeriodicPaymentModel> lmd)
        {
            Boolean TheResult = true;
            foreach (var item in lmd)
            {
                if (item.SendSMS)
                {
                    string SMSText = "سلام" + item.Us_Name + ' ' + item.Us_LName;
                    TheResult = SendSMS(SMSText,item.Us_Mob);
                    if (TheResult == false)
                        break;
                }
            }
            return TheResult;// View(lmd);
        }

        public Boolean SendSMS(string SMSText, string MobNumber)
        {
            Boolean Result = true;
            return(Result);
        }

        [HttpPost]
        public Boolean SendEmailtoAll(List<PeriodicPaymentModel> lmd)
        {
            SmtpClient client = new SmtpClient("smtp.gmail.com");
            client.UseDefaultCredentials = false;
            client.Credentials = new NetworkCredential("davarpanah1978@gmail.com", "mAHSHID123");
            MailMessage mailMessage = new MailMessage();
            mailMessage.From = new MailAddress("davarpanah1978@gmail.com");
            mailMessage.Subject = "سلام خیر گرامی";
            client.Host = "smtp.gmail.com";
            client.EnableSsl = true;
            client.DeliveryMethod = SmtpDeliveryMethod.Network;
            client.Port = 587;

            Boolean TheResult = true;
            foreach (var item in lmd)
            {
                mailMessage.Body = "";
                mailMessage.To.Clear();
                if (item.SendEmail)
                {
                    mailMessage.Body = " سلام " + item.Us_Name + ' ' + item.Us_LName;
                    mailMessage.To.Add(item.Us_Email);
                    try
                    {
                        client.Send(mailMessage);
                    }
                    catch
                    {
                        TheResult = false;
                    }
                }
            }
            return TheResult;
        }

        public async Task<ActionResult> MyPeriodicPayments()
        {
            List<PeriodicPaymentModel> lmd = new List<PeriodicPaymentModel>();
            var user = await UserManager.FindByIdAsync(User.Identity.GetUserId());
            if (user != null)
            {
                Connection.Connection con = new Connection.Connection();
                DataSet ds = new DataSet();
                ds = con.MyPeriodicPaymentData(User.Identity.GetUserId());
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    lmd.Add(new PeriodicPaymentModel
                    {
                        Dr_Reg_Date = Convert.ToDateTime(dr["Dr_Reg_Date"]),
                        Us_Name = dr["Us_Name"].ToString(),
                        Us_LName = dr["Us_LName"].ToString(),
                        Dr_AccTransaction = Convert.ToBoolean(dr["Dr_AccTransaction"]),
                        Dr_Amount=Convert.ToInt32(dr["Dr_Amount"]),
                        Dr_CardTranscation = Convert.ToBoolean(dr["Dr_CardTranscation"]),
                        Dr_Cleaner = Convert.ToBoolean(dr["Dr_Cleaner"]),
                        Dr_Clothes = Convert.ToBoolean(dr["Dr_Clothes"]),
                        Dr_Endowment = Convert.ToBoolean(dr["Dr_Endowment"]),
                        Dr_Food = Convert.ToBoolean(dr["Dr_Food"]),
                        Dr_HelpDay = Convert.ToInt16(dr["Dr_HelpDay"]),
                        Dr_Id=Convert.ToInt32(dr["Dr_Id"]),
                        Dr_Inperson = Convert.ToBoolean(dr["Dr_Inperson"]),
                        Dr_Medical = Convert.ToBoolean(dr["Dr_Medical"]),
                        Dr_Month_Duration = Convert.ToInt16(dr["Dr_Month_Duration"]),
                        Dr_Representative = Convert.ToBoolean(dr["Dr_Representative"]),
                        Dr_Stationery = Convert.ToBoolean(dr["Dr_Stationery"])
                    });
                }

            }
            return View(lmd);
        }

        public async Task<ActionResult> AllOnlinePaymentsList()
        {
            List<OnlinePaymentModel> lmd = new List<OnlinePaymentModel>();
            var user = await UserManager.FindByIdAsync(User.Identity.GetUserId());
            if (user != null)
            {
                Connection.Connection con = new Connection.Connection();
                DataSet ds = new DataSet();
                ds = con.AllOnlinePaymentsData();
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    lmd.Add(new OnlinePaymentModel
                    {
                        Do_Date = Convert.ToDateTime(dr["Do_Date"]),
                        DoR_Amount = Convert.ToInt32(dr["DoR_Amount"]),
                        Do_Id = Convert.ToInt32(dr["Do_Id"]),
                        Us_Name = dr["Us_Name"].ToString(),
                        Us_LName = dr["Us_LName"].ToString(),
                        IsUser = Convert.ToBoolean(dr["IsUser"]),
                        Us_Email = dr["Us_Email"].ToString(),
                        Us_Mob = dr["Us_Mob"].ToString(),
                        Us_Phone = dr["Us_Phone"].ToString()
                    });
                }
            }
            return View(lmd);
        }

        public async Task<ActionResult> DonatorsList()
        {
            List<PeriodicPaymentModel> lmd = new List<PeriodicPaymentModel>();
            var user = await UserManager.FindByIdAsync(User.Identity.GetUserId());
            if (user != null)
            {
                Connection.Connection con = new Connection.Connection();
                DataSet ds = new DataSet();
                ds = con.DonatorsListData();
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    lmd.Add(new PeriodicPaymentModel
                    {
                        Dr_Reg_Date = Convert.ToDateTime(dr["Dr_Reg_Date"]),
                        Dr_Amount = Convert.ToInt32(dr["Dr_Amount"]),
                        Dr_Month_Duration = Convert.ToInt16(dr["Dr_Month_Duration"]),
                        Us_Name = dr["Us_Name"].ToString(),
                        Us_LName = dr["Us_LName"].ToString(),
                        IsUser = Convert.ToBoolean(dr["IsUser"]),
                        Us_Email = dr["Us_Email"].ToString(),
                        Us_Mob = dr["Us_Mob"].ToString(),
                        Us_Phone = dr["Us_Phone"].ToString(),
                        Us_Unic_Number = Convert.ToInt32(dr["Us_Unic_Number"]),
                        Dr_CT_Code = Convert.ToInt16(dr["Dr_CT_Code"])
                    });
                }
            }
            return View(lmd);
        }
        
        public async Task<ActionResult> MyOnlinePayments()
        {
            List<OnlinePaymentModel> lmd = new List<OnlinePaymentModel>();
            var user = await UserManager.FindByIdAsync(User.Identity.GetUserId());
            if (user != null)
            {
                DateTimeFormatInfo format = new DateTimeFormatInfo();
                format.ShortDatePattern = "mm/dd/yyyy"; 
                Connection.Connection con = new Connection.Connection();
                DataSet ds = new DataSet();
                ds = con.MyOnlinePaymentsData(User.Identity.GetUserId());
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    lmd.Add(new OnlinePaymentModel
                    {
                        Do_Date = Convert.ToDateTime(dr["Do_Date"]),
                        DoR_Amount = Convert.ToInt32(dr["DoR_Amount"]),
                        Do_Id = Convert.ToInt32(dr["Do_Id"]),
                        Us_Name = dr["Us_Name"].ToString(),
                        Us_LName = dr["Us_LName"].ToString()
                    });
                }
            }
            return View(lmd);
        }

        //
        // POST: /Manage/SetPassword
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> SetPassword(SetPasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await UserManager.AddPasswordAsync(User.Identity.GetUserId(), model.NewPassword);
                if (result.Succeeded)
                {
                    var user = await UserManager.FindByIdAsync(User.Identity.GetUserId());
                    if (user != null)
                    {
                        await SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);
                    }
                    return RedirectToAction("Index", new { Message = ManageMessageId.SetPasswordSuccess });
                }
                AddErrors(result);
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }

        //
        // GET: /Manage/SetPassword
        public ActionResult ChangePersonalInformation()
        {
            ChangePersonalInformation lmd = new ChangePersonalInformation();
            DataSet ds = new DataSet();
            Connection.Connection con = new Connection.Connection();
            ds = con.UserData(User.Identity.GetUserId());
            if (ds.Tables[0].Rows.Count==1)
            {
                DataRow dr = ds.Tables[0].Rows[0];
                lmd.Us_Name = dr["Us_Name"].ToString();
                lmd.Us_LName = dr["Us_LName"].ToString();
                lmd.Us_Phone = dr["Us_Phone"].ToString();

                DateTimeFormatInfo format = new DateTimeFormatInfo();
                format.ShortDatePattern = "mm/dd/yyyy";
                lmd.Us_BirthDay=Convert.ToDateTime(dr["Us_BirthDay"],format);

                lmd.Us_HomeAddress = dr["Us_HomeAddress"].ToString();
                lmd.Us_WorkAddress = dr["Us_WorkAddress"].ToString();
                lmd.City_Ct_Id = Convert.ToInt16(dr["City_Ct_Id"]);
                lmd.SelectedState=con.StateData(lmd.City_Ct_Id);
                lmd.SelectedCountry = con.CountryData(lmd.SelectedState);
                lmd.Countries = con.GetListOfCountries();
                lmd.States = con.GetListOfStates(lmd.SelectedCountry);
                lmd.Cities = con.GetListOfCities(lmd.SelectedState);
            }
           return View(lmd);
        }

        public Array FillStates(int Co_Id)
        {
            List<MySelectListItem> States = new List<MySelectListItem>();
            ChangePersonalInformation lmd = new ChangePersonalInformation();
            Connection.Connection con = new Connection.Connection();
            States=con.GetListOfStates(Co_Id);
            MySelectListItem[] StatesArr = States.ToArray();
            //int[] StatesArr={3,5};
            return (StatesArr);
        }
        //
        // POST: /Manage/ChangePersonalInformation
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ChangePersonalInformation(ChangePersonalInformation model)
        {
            Boolean TheResult=false;
            if (ModelState.IsValid)
            {
                Connection.Connection con = new Connection.Connection();
                TheResult = con.UpdateUserInformation(model, User.Identity.GetUserId());
            }
            if (TheResult == true)
                return RedirectToAction("Index", new { Message = ManageMessageId.UpdatePersonalInformation });
            else 
                return View();
        }
        public ActionResult ManagePhotoSlide()
        {
            List<PhotoSlide> lmd = new List<PhotoSlide>();
            DataSet ds = new DataSet();
            Connection.Connection con = new Connection.Connection();
            ds = con.PhotoSlideAllData();
            foreach (DataRow dr in ds.Tables[0
                ].Rows)
            {
                lmd.Add(new PhotoSlide
                {
                    Ph_Id = Convert.ToInt32(dr["Ph_Id"]),
                    Ph_Path = dr["Ph_Path"].ToString(),
                    Ph_Showable = Convert.ToBoolean(dr["Ph_Showable"]),
                    Ph_Deleted=false
                });
            }
            return View(lmd);
        }

        [HttpPost]
        public ActionResult SaveNews(NewsListModel Lmd)
        {

            return View();
        }
        public ActionResult ManageNews(Int16 Nw_Type, Int16 SelectedNews = 0)
        {
            NewsListModel lmd = new NewsListModel();
            lmd.SeletedNews = SelectedNews;
            lmd.Nw_Type = Nw_Type;
            DataSet ds = new DataSet();
            Connection.Connection con = new Connection.Connection();
            ds = con.NewsData(Nw_Type);
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                lmd.NewsItems.Add(new NewsModel
                {
                    Nw_Id = Convert.ToInt32(dr["Nw_Id"]),   
                    Nw_Date = Convert.ToDateTime(dr["Nw_Date"]),
                    Nw_ShortDescription = dr["Nw_ShortDescription"].ToString(),
                    Nw_Description = dr["Nw_Description"].ToString(),
                    Nw_Title = dr["Nw_Title"].ToString(),
                    User_Name = dr["User_Name"].ToString(),
                    Nw_Photo1 = dr["Nw_Photo1"].ToString(),
                    Nw_Photo2 = dr["Nw_Photo1"].ToString(),
                    Nw_Photo3 = dr["Nw_Photo1"].ToString(),
                    Nw_Photo4 = dr["Nw_Photo1"].ToString()
                });
            }
            return View(lmd);
        }

        public PartialViewResult NewPhotoSlide()
        {
            return PartialView();
        }
        [HttpPost]
        public ActionResult NewPhotoSlide(HttpPostedFileBase file1)
        {
            string ThePath = "";
            if (Request.Files.Count > 0)
            {
                var file = Request.Files[0];
                if (file != null && file.ContentLength > 0)
                {
                    var fileName = Path.GetFileName(file.FileName);
                    var path = Path.Combine(Server.MapPath("~/Content/Images/"), fileName);
                    file.SaveAs(path);
                    ViewBag.StatusMessage = "";
                    ThePath = "/Content/Images/" + fileName;

                    //PhotoSlide Photolmd=new Models.PhotoSlide();
                    Connection.Connection con = new Connection.Connection();
                    Boolean TheResult = con.AddPhotoSlide(ThePath);
                    if (TheResult == false)
                        ViewBag.StatusMessage = "خطا در عکس انتخابی";
                }
                else
                    ViewBag.StatusMessage = "خطا در عکس انتخابی";
            }
            else
                ViewBag.StatusMessage = "لطفا عکسی را انتخاب نمایید";
            //return true;
            //return RedirectToAction("ManagePhotoSlide","Manage");
            return Redirect("ManagePhotoSlide");
        }

        [HttpPost]
        public ActionResult ManagePhotoSlide(List<PhotoSlide> Photolmd)
        {
            List<PhotoSlide> Photolmd2 = new List<Models.PhotoSlide>();

            Boolean TheResult = false;
            Connection.Connection con = new Connection.Connection();
            foreach (var item in Photolmd)
            {
                if (item.Ph_Deleted)
                    TheResult = con.DeletePhotoSlide(item.Ph_Id);
                else
                {
                    TheResult = con.UpdatePhotoSlide(item);
                    Photolmd2.Add(item);
                }
                if (TheResult == false)
                    break;
            }
            return View(Photolmd2);
        }

        //
        // GET: /Manage/ManageLogins
        public async Task<ActionResult> ManageLogins(ManageMessageId? message)
        {
            ViewBag.StatusMessage =
                message == ManageMessageId.RemoveLoginSuccess ? "ورود خارجی حذف شده است"
                : message == ManageMessageId.Error ? "خطایی رخ داده است"
                : "";
            var user = await UserManager.FindByIdAsync(User.Identity.GetUserId());
            if (user == null)
            {
                return View("Error");
            }
            var userLogins = await UserManager.GetLoginsAsync(User.Identity.GetUserId());
            var otherLogins = AuthenticationManager.GetExternalAuthenticationTypes().Where(auth => userLogins.All(ul => auth.AuthenticationType != ul.LoginProvider)).ToList();
            ViewBag.ShowRemoveButton = user.PasswordHash != null || userLogins.Count > 1;
            return View(new ManageLoginsViewModel
            {
                CurrentLogins = userLogins,
                OtherLogins = otherLogins
            });
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && _userManager != null)
            {
                _userManager.Dispose();
                _userManager = null;
            }

            base.Dispose(disposing);
        }

#region Helpers
        // Used for XSRF protection when adding external logins
        private const string XsrfKey = "XsrfId";

        private IAuthenticationManager AuthenticationManager
        {
            get
            {
                return HttpContext.GetOwinContext().Authentication;
            }
        }

        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error);
            }
        }

        private bool HasPassword()
        {
            var user = UserManager.FindById(User.Identity.GetUserId());
            if (user != null)
            {
                return user.PasswordHash != null;
            }
            return false;
        }

        private bool HasPhoneNumber()
        {
            var user = UserManager.FindById(User.Identity.GetUserId());
            if (user != null)
            {
                return user.PhoneNumber != null;
            }
            return false;
        }

#endregion
    }
}