using System;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using test1.Models;
using System.Data;
using System.Globalization;
using System.Collections.Generic;
using System.IO;
using System.Web;

namespace test1.Controllers
{
    public class HomeController : Controller
    {
        [HttpPost]
        public ActionResult ChangeState(int SelectedCountry)
        {
            List<MySelectListItem> States = new List<MySelectListItem>();
            Connection.Connection con = new Connection.Connection();
            States = con.GetListOfStates(SelectedCountry);
            return Json(States);
        }
        [HttpPost]
        public ActionResult ChangeCity(int SelectedState)
        {
            List<MySelectListItem> Cities = new List<MySelectListItem>();
            Connection.Connection con = new Connection.Connection();
            Cities = con.GetListOfCities(SelectedState);
            return Json(Cities);
        }
        public ActionResult NewsModule(Int16 Nw_Type)
        {
            NewsListModel lmd = new NewsListModel();
            DataSet ds = new DataSet();
            Connection.Connection con = new Connection.Connection();
            ds = con.NewsData(Nw_Type, 5);
            lmd.Nw_Type = Nw_Type;
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                lmd.NewsItems.Add(new NewsModel
                {
                    Nw_Id = Convert.ToInt32(dr["Nw_Id"]),
                    Nw_Date = Convert.ToDateTime(dr["Nw_Date"]),
                    Nw_ShortDescription = dr["Nw_ShortDescription"].ToString(),
                    Nw_Title = dr["Nw_Title"].ToString(),
                    User_Name = dr["User_Name"].ToString(),
                    Nw_Photo1 = dr["Nw_Photo1"].ToString()
                });
            }
            return View(lmd);
        }
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult News(Int16 Nw_Type, Int16 SelectedNews=0)
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

        [HttpGet]
        public ActionResult PatientList(Int16 SeletedSupporter, Int16 SeletedSeyed)
        {
            PatientListModel lmd = new PatientListModel();
            DataSet ds = new DataSet();
            Connection.Connection con = new Connection.Connection();
            ds = con.PatientData(SeletedSupporter, SeletedSeyed);
            lmd.Countries = con.GetListOfCountries();
            lmd.SeletedSupporter = SeletedSupporter;
            lmd.SeletedSeyed = SeletedSeyed;

            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                PatientModel Tmp = new PatientModel();
                Tmp.Pt_Ct_Code = Convert.ToInt16(dr["Pt_Ct_Code"]);
                Tmp.SelectedState = con.StateData(Tmp.Pt_Ct_Code);
                Tmp.SelectedCountry = con.CountryData(Tmp.SelectedState);

                lmd.Patients.Add(new PatientModel
                {
                    Pt_Id = Convert.ToInt32(dr["Pt_Id"]),
                    //Pt_Name = dr["Pt_Name"].ToString(),
                    //Pt_LName = dr["Pt_LName"].ToString(),
                    Pt_Ct_Code = Tmp.Pt_Ct_Code,
                    Pt_Seyyed = Convert.ToInt16(dr["Pt_Seyyed"]),
                    Pt_Supporter = Convert.ToInt16(dr["Pt_Supporter"]),
                    Pt_BirthDay =  Convert.ToDateTime(dr["Pt_BirthDay"]),
                    Pt_Sickness = dr["Pt_Sickness"].ToString(),
                    Pt_Sex = Convert.ToInt16(dr["Pt_Sex"]),
                    Pt_Tel = dr["Pt_Tel"].ToString(),
                    Pt_Address = dr["Pt_Address"].ToString(),
                    SelectedState = con.StateData(Tmp.Pt_Ct_Code),
                    SelectedCountry = con.CountryData(Tmp.SelectedState),
                    States = con.GetListOfStates(Tmp.SelectedCountry),
                    Cities = con.GetListOfCities(Tmp.SelectedState),
                    Pt_Photo_Path = dr["Pt_Photo_Path"].ToString(),
                    Pt_CompleteName = dr["Pt_Name"].ToString() + ' ' + dr["Pt_LName"].ToString()
                });

            }
            return View(lmd);
        }
         
        public ActionResult Cooperation()
        {
            return View();
        }
        public ActionResult CharityFundRequest()
        {
            return View();
        }
        public ActionResult ConstructionCooperation()
        {
            return View();
        }         
        public ActionResult CndolenceTable()
        {
            return View();
        }
        public ActionResult PeriodicPayment()
        {
            return View();
        }
        public ActionResult GalleryManagement()
        {
            List<Gallery> lmd = new List<Gallery>();
            DataSet ds = new DataSet();
            Connection.Connection con = new Connection.Connection();
            ds = con.GalleryManagementData();

            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                lmd.Add(new Gallery
                {
                    Ga_Id = Convert.ToInt32(dr["Ga_Id"]),
                    Ga_Title = dr["Ga_Title"].ToString(),
                    Ga_Description = dr["Ga_Description"].ToString(),
                    Tmp_Ph_Path = dr["Ph_Path"].ToString()
                }); 
            }
            return View(lmd);
        }

        public ActionResult Gallery(MyGalleryModel Lmd)
        {
            return View(Lmd);
        }

        public ActionResult MyGallery()
        {
            MyGalleryModel lmd = new MyGalleryModel();
            return View(lmd);
        }
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        /*public ActionResult MyGallery(test1.Models.MyGallery model)
        {
            bool TheResult = false;
            if (ModelState.IsValid)
            {
                Connection.Connection con = new Connection.Connection();
                if (User.Identity.GetUserId() != null)
                {
                    //string[] words = model.Ga_Deleteable.Split(' ');
                    //foreach (string word in words)
                        //model.Photoes.RemoveAt(Int32.Parse(word));
                    //hashem TheResult = con.AddMyGallery(model);
                }
            }
            if (TheResult == true)
                return RedirectToAction("Index", "Manage", new { Message = Models.ManageMessageId.AddGallerySuccess });
            else
                return View();
        }
         */
        public ActionResult TestView()//test1.Models.MyGalleryModel lmd)
        {
            return View();
        }

        public PartialViewResult MyPhoto()//test1.Models.MyGalleryModel lmd)
        {
            //test1.Models.Photo Lmd = new test1.Models.Photo();
            //Lmd.Ph_Gr_Id = 1;
            return PartialView();
        }

        [HttpPost]
        //[ValidateAntiForgeryToken]
        public ActionResult MyPhoto(MyGalleryModel MyLmd)
        {
            string ThePath="";
            if(Request.Files.Count>0)
            {
                var file = Request.Files[0];
                if (file != null && file.ContentLength > 0)
                {
                    var fileName = Path.GetFileName(file.FileName);
                    var path = Path.Combine(Server.MapPath("~/Content/Images/"), fileName);
                    file.SaveAs(path);
                    ViewBag.StatusMessage = "";
                    ThePath = "/Content/Images/" + fileName;
                }
                else
                    ViewBag.StatusMessage = "خطا در عکس انتخابی";
            }
            else
                ViewBag.StatusMessage = "لطفا عکسی را انتخاب نمایید";
             
            //return RedirectToAction("MyGallery",lmd);
            //return true;
            return View("Gallery", MyLmd);
            //return MyLmd
        }

        [HttpPost]
        //[ValidateAntiForgeryToken]
        public ActionResult Photo(test1.Models.MyGalleryModel Passedphoto)
        {
            //if (Request.IsAjaxRequest())
            //{
            //    return new EmptyResult();
           // }
            if (Request.Files.Count > 0)
            {
                var file = Request.Files[0];
                if (file != null && file.ContentLength > 0)
                {
                    var fileName = Path.GetFileName(file.FileName);
                    var path = Path.Combine(Server.MapPath("~/Content/Images/"), fileName);
                    file.SaveAs(path);
                    //ViewBag.Message = "";
                    Passedphoto.StatusMessage = "";
                    Passedphoto.AddingPhoto.Ph_Path = "/Content/Images/" + fileName;
                }
                else
                    Passedphoto.StatusMessage = "خطا در عکس انتخابی";
            }
            else
                Passedphoto.StatusMessage = "لطفا عکسی را انتخاب نمایید";
            //return RedirectToAction("Gallery", "Home", Passedphoto);
            return View(Passedphoto);
        }

        public ActionResult NewGallery()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult NewGallery(test1.Models.Gallery model)
        {
            bool TheResult = false;
            if (ModelState.IsValid)
            {
                Connection.Connection con = new Connection.Connection();
                if (User.Identity.GetUserId() != null)
                {
                    string[] words = model.Ga_Deleteable.Split(' ');
                    foreach (string word in words)
                        model.Photoes.RemoveAt(Int32.Parse(word));
                    TheResult = con.AddGallery(model);
                }
            }
            if (TheResult == true)
                return RedirectToAction("Index", "Manage", new { Message = Models.ManageMessageId.AddGallerySuccess });
            else
                return View();
        }

        public ActionResult OnlinePayment2(test1.Models.OnlinePaymentModel lmd)
        {
            return View(lmd);
        }        
        /*public ActionResult Gallery(test1.Models.Gallery Passedphoto)
        {
            return View(Passedphoto);
        }
         * */
        [HttpGet]
        public ActionResult AddPhoto()
        {
            return View();
        }

        [HttpPost]
        //[ValidateAntiForgeryToken]
        public test1.Models.Gallery AddPhoto(test1.Models.Gallery Passedphoto)
        {
//            test1.Models.Gallery Passedphoto = new test1.Models.Gallery();
//            Passedphoto.Id = Id;

            Passedphoto.FirstTime = false;
            if (Request.Files.Count > 0)
            {
                var file = Request.Files[0];
                if (file != null && file.ContentLength > 0)
                {
                    var fileName = Path.GetFileName(file.FileName);
                    var path = Path.Combine(Server.MapPath("~/Content/Images/"), fileName);
                    file.SaveAs(path);
                    //ViewBag.Message = "";
                    Passedphoto.StatusMessage = "";
                    Passedphoto.Tmp_Ph_Path = "/Content/Images/" + fileName;
                }
                else
                    Passedphoto.StatusMessage = "خطا در عکس انتخابی";
            }
            else
                Passedphoto.StatusMessage = "لطفا عکسی را انتخاب نمایید";
            //return RedirectToAction("Gallery", "Home", Passedphoto);
            return Passedphoto;
        }
        public ActionResult PeriodicPayment2()
        {
            PeriodicPaymentModel lmd = new PeriodicPaymentModel();
            Connection.Connection con = new Connection.Connection();
            DateTimeFormatInfo format = new DateTimeFormatInfo();
            format.ShortDatePattern = "mm/dd/yyyy"; 
            if (User.Identity.GetUserId() != null)
            {
                DataSet ds = new DataSet();
                ds = con.DonatorData(User.Identity.GetUserId());
                if (ds.Tables[0].Rows.Count == 1)
                {
                    DataRow dr = ds.Tables[0].Rows[0];
                    lmd.Us_Name = dr["Us_Name"].ToString();
                    lmd.Us_LName = dr["Us_LName"].ToString();
                    lmd.Us_Phone = dr["Us_Phone"].ToString();
                    lmd.Us_Email = dr["Us_Email"].ToString();
                    lmd.Us_BirthDay = Convert.ToDateTime(dr["Us_BirthDay"], format);
                    lmd.Us_HomeAddress = dr["Us_HomeAddress"].ToString();
                    lmd.Us_WorkAddress = dr["Us_WorkAddress"].ToString();
                    lmd.City_Ct_Id = Convert.ToInt16(dr["City_Ct_Id"]);
                    lmd.SelectedState = con.StateData(lmd.City_Ct_Id);
                    lmd.SelectedCountry = con.CountryData(lmd.SelectedState);
                    lmd.Countries = con.GetListOfCountries();
                    lmd.States = con.GetListOfStates(lmd.SelectedCountry);
                    lmd.Cities = con.GetListOfCities(lmd.SelectedState);
                    if (dr["Dr_Us_ID"].ToString() != "")
                    {
                        lmd.Dr_AccTransaction = Convert.ToBoolean(dr["Dr_AccTransaction"]);
                        lmd.Dr_Amount = Convert.ToInt32(dr["Dr_Amount"]);
                        lmd.Dr_CardTranscation = Convert.ToBoolean(dr["Dr_CardTranscation"]);
                        lmd.Dr_Cleaner = Convert.ToBoolean(dr["Dr_Cleaner"]);
                        lmd.Dr_Clothes = Convert.ToBoolean(dr["Dr_Clothes"]);
                        lmd.Dr_CT_Code = Convert.ToByte(dr["Dr_CT_Code"]);
                        lmd.Dr_Endowment = Convert.ToBoolean(dr["Dr_Endowment"]);
                        lmd.Dr_Food = Convert.ToBoolean(dr["Dr_Food"]);
                        lmd.Dr_HelpDay = Convert.ToByte(dr["Dr_HelpDay"]);
                        lmd.Dr_Inperson = Convert.ToBoolean(dr["Dr_Inperson"]);
                        lmd.Dr_Medical = Convert.ToBoolean(dr["Dr_Medical"]);
                        lmd.Dr_Month_Duration = Convert.ToByte(dr["Dr_Month_Duration"]);
                        lmd.Dr_Representative = Convert.ToBoolean(dr["Dr_Representative"]);
                        lmd.Dr_Stationery = Convert.ToBoolean(dr["Dr_Stationery"]);
                    }
                    return View(lmd);
                }
            }
            lmd.Us_BirthDay = Convert.ToDateTime(DateTime.Now, format);
            lmd.Countries = con.GetListOfCountries();
            lmd.States = con.GetListOfStates(1);
            lmd.Cities = con.GetListOfCities(1);
            //lmd.City_Ct_Id-=Convert.ToInt16(lmd.Cities[0].value;
            return View(lmd);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult PeriodicPayment2(PeriodicPaymentModel model)
        {
            bool TheError = false;
            if (ModelState.IsValid)
            {
                Connection.Connection con = new Connection.Connection();
                if (User.Identity.GetUserId() == null)
                    TheError = con.AddUserAndDonator(model);
                else
                    TheError = con.UpdateUserAndDonator(model, User.Identity.GetUserId());
            }
            if (TheError == false)
                 return RedirectToAction("Index", "Manage", new { Message = Models.ManageMessageId.PeriodicPaymentFormSuccess });
            else
                return View(model);
        }
        public ActionResult OnlinePayment()
        {
            OnlinePaymentModel lmd = new OnlinePaymentModel();
            Connection.Connection con = new Connection.Connection();
            DateTimeFormatInfo format = new DateTimeFormatInfo();
            //format.ShortDatePattern = "mm-dd-yyyy";
            format.ShortDatePattern = "mm/dd/yyyy";
            if (User.Identity.GetUserId() != null)
            {
                DataSet ds = new DataSet();
                ds = con.UserData(User.Identity.GetUserId());
                lmd.Do_Date = Convert.ToDateTime(DateTime.Now, format);
                if (ds.Tables[0].Rows.Count == 1)
                {
                    DataRow dr = ds.Tables[0].Rows[0];
                    lmd.Us_Name = dr["Us_Name"].ToString();
                    lmd.Us_LName = dr["Us_LName"].ToString();
                    lmd.Us_Phone = dr["Us_Phone"].ToString();
                    lmd.Us_Email = dr["Us_Email"].ToString();
                    lmd.Us_HomeAddress = dr["Us_HomeAddress"].ToString();
                    lmd.Us_WorkAddress = dr["Us_WorkAddress"].ToString();
                    lmd.City_Ct_Id = Convert.ToInt16(dr["City_Ct_Id"]);
                    lmd.SelectedState = con.StateData(lmd.City_Ct_Id);
                    lmd.SelectedCountry = con.CountryData(lmd.SelectedState);
                    lmd.Countries = con.GetListOfCountries();
                    lmd.States = con.GetListOfStates(lmd.SelectedCountry);
                    lmd.Cities = con.GetListOfCities(lmd.SelectedState);
                    
                    return View(lmd);
                }
            }
            //lmd.Us_BirthDay = Convert.ToDateTime(DateTime.Now, format);
            lmd.SelectedState = 1;
            lmd.SelectedCountry = 1;
            lmd.City_Ct_Id = 1;
            lmd.Countries = con.GetListOfCountries();
            lmd.States = con.GetListOfStates(1);
            lmd.Cities = con.GetListOfCities(1);
            return View(lmd);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult OnlinePayment(OnlinePaymentModel model)
        {
            bool TheError = false;
            if (ModelState.IsValid)
            {
                Connection.Connection con = new Connection.Connection();
                if (User.Identity.GetUserId() == null)
                    TheError = con.AddUserAndPayment(model);
                else
                    TheError = con.UpdateUserAndPayment(model, User.Identity.GetUserId());
            }
            if (TheError == false)
            {
                ViewBag.Message = "";
                return RedirectToAction("OnlinePayment2", "Home", model);
            }
            else
            {
                ViewBag.Message = "خطا در ثبت اطلاعات";
                Connection.Connection con = new Connection.Connection();
                model.Countries = con.GetListOfCountries();
                model.States = con.GetListOfStates(model.SelectedCountry);
                model.Cities = con.GetListOfCities(model.SelectedState);
                return View(model);
            }
        }

        [HttpPost]
        public ActionResult OnlinePayment2()
        {
            return RedirectToAction("Index", "Manage", new { Message = Models.ManageMessageId.OnlinePaymentSuccess });
        }

        public ActionResult RehabilitationServices()
        {
            return View();
        }         
        public ActionResult SupportiveServices()
        {
            return View();
        }         
        public ActionResult MedicalServices()
        {
            return View();
        }          
        public ActionResult Supporters()
        {
            return View();
        }          
        public ActionResult ManagerMessage()
        {
            return View();
        }        
        public ActionResult Directors()
        {
            return View();
        }        
        public ActionResult Chart()
        {
            return View();
        }
        public ActionResult History()
        {
            return View();
        }
        public ActionResult Goal()
        {
            return View();
        }
        public ActionResult Psychology()
        {
            return View();
        }
        public ActionResult Takingcare()
        {
            return View();
        }
        public ActionResult Prevention()
        {
            return View();
        }

        public ActionResult About()
        {
                return View();
        }

        public ActionResult VisitRequest()
        {
            return View();
        }        
        
        public ActionResult DisabilitiesCauses()
        {
            return View();
        }
        
        public ActionResult Contact()
        {
            ViewBag.Message = "شما عزیزان می توانید از طرق زیر با مسئولین و کارکنان موسسه امام جواد در ارتباط باشید.";

            return View();
        }

        public PartialViewResult PhotoSlide()
        {
            List<PhotoSlide> lmd = new List<PhotoSlide>(); 
            DataSet ds = new DataSet();
            Connection.Connection con = new Connection.Connection(); 
            ds = con.PhotoSlideData(); 
            foreach (DataRow dr in ds.Tables[0].Rows) 
            {
                lmd.Add(new PhotoSlide
                {
                    Ph_Id = Convert.ToInt32(dr["Ph_Id"]), 
                    Ph_Description = dr["Ph_Description"].ToString(),
                    Ph_Title = dr["Ph_Title"].ToString(),
                    Ph_Path = dr["Ph_Path"].ToString(),
                    Ph_Link = dr["Ph_Link"].ToString()
                });
            }
            return PartialView("PhotoSlide", lmd);
        }

        public ActionResult WishList()
        {
            List<GoodGroup> lmd = new List<GoodGroup>();
            DataSet ds = new DataSet();
            Connection.Connection con = new Connection.Connection();
            ds = con.GoodGroupData();

            DataSet ds_good = new DataSet();
            Connection.Connection con_good = new Connection.Connection();

            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                List<GoodModel> lmd_good = new List<GoodModel>();
                var Gg_Id = Convert.ToInt32(dr["GG_Id"]);
                ds_good = con_good.GoodData(Gg_Id);
                //lmd_good.RemoveAll(true);
                foreach (DataRow dr_good in ds_good.Tables[0].Rows)
                {
                    lmd_good.Add(new GoodModel
                    {
                        Gd_Id=Convert.ToInt32(dr_good["Gd_Id"]),
                        Gd_Name=dr_good["Gd_Name"].ToString(),
                        Gd_RequiredNo=Convert.ToInt32(dr_good["Gd_RequiredNo"])
                    });
                }
                lmd.Add(new GoodGroup
                {
                    GG_Id = Convert.ToInt16(dr["GG_Id"]),
                    GG_Name = dr["GG_Name"].ToString(),
                    Goods = new List<GoodModel>(lmd_good),
                });
            }
            return View("WishList",lmd);
        }
    }
}