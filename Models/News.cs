using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace test1.Models
{
    public class NewsModel
    {
        [Display(Name = "کد خبر")]
        public int Nw_Id { set; get; }
        public string Nw_ShortDescription { set; get; }
        [Display(Name = "متن خبر")]
        public string Nw_Description{ set; get; }
        [Display(Name = "عنوان خبر")]
        public string Nw_Title{ set; get; }
        [Display(Name = "تاریخ")]
        public DateTime Nw_Date{ set; get; }
        [Display(Name = "نویسنده")]
        public string User_Name { set; get; }
        public string Nw_Photo1 { set; get; }
        public string Nw_Photo2 { set; get; }
        public string Nw_Photo3 { set; get; }
        public string Nw_Photo4 { set; get; }
    }
    public class NewsListModel
    {
        public Int16 SeletedNews { set; get; }
        public Int16 Nw_Type { set; get; }
        public List<NewsModel> NewsItems { set; get; }

        public NewsListModel()
        {
            NewsItems = new List<NewsModel>();
        }
    }
}