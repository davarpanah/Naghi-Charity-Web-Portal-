using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
//using System;
using System.ComponentModel.DataAnnotations;

namespace test1.Models
{
    public class Gallery
    {
        public Int32 Ga_Id { set; get; }
        //[Required]
        [Display(Name = "عنوان گالری")]
        public string Ga_Title { set; get; }
        [Display(Name = "توضیحات در مورد گالری")]
        public string Ga_Description { set; get; }
        public string Ga_Deleteable { set; get; }

        [Display(Name = "توضیحات عکس")]
        public string Tmp_Ph_Description { set; get; }
        [Display(Name = "عنوان عکس")]
        public string Tmp_Ph_Title { set; get; }
        [Display(Name = "مسیر عکس")]
        public string Tmp_Ph_Path { set; get; }
        public string StatusMessage { set; get; }
        public string Tmp_HiddenImges { set; get; }
        public int ImageNo { set; get; }
        public bool FirstTime { set; get; }
        
        public List<Photo> Photoes;
        public Gallery()
        {
            Photoes=new List<Photo>();
            Ga_Deleteable = "";
            FirstTime = true;
            ImageNo = -1;
        }
    }

    public class Photo
    {
        public Int32 Ph_Id { set; get; }
        [Display(Name = "توضیحات عکس")]
        public string Ph_Description { set; get; }
        [Display(Name = "عنوان عکس")]
        public string Ph_Title { set; get; }
        [Display(Name = "مسیر عکس")]
        public string Ph_Path { set; get; }
        //public string Ph_Link { set; get; }
        public Int32 Ph_Gr_Id { set; get; }
        public string StatusMessage { set; get; }
        public Photo()
        {
            Ph_Description = "Description";
            Ph_Title = "Title";
            StatusMessage = "";
        }
    }

    public class MyGalleryModel
    {
        public List<Photo> Photos { set; get; }
        public Photo AddingPhoto { set; get; }
        public string StatusMessage { set; get; }
        public Int32 Ga_Id { set; get; }
        //[Required]
        [Display(Name = "عنوان گالری")]
        public string Ga_Title { set; get; }
        [Display(Name = "توضیحات در مورد گالری")]
        public string Ga_Description { set; get; }
        public string Ga_Deleteable { set; get; }
        HttpPostedFileBase PhotoFile { set; get; }
        public MyGalleryModel()
        {
            Photos = new List<Photo>();
            AddingPhoto = new Photo();
            Ga_Deleteable = "";
            StatusMessage = "";
        }
    }

}