using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace test1.Models
{
    public class PhotoSlide
    {
        public Int32 Ph_Id { set; get; }
        public string Ph_Description { set; get; }
        public string Ph_Title { set; get; }
        public string Ph_Path { set; get; }
        public string Ph_Link { set; get; }
        public bool Ph_Showable { set; get; }
        public bool Ph_Deleted { set; get; }
        public PhotoSlide()
        {
            //Ph_Deleted = false;
        }
    }
}