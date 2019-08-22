using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace test1.Models
{
    public class GoodGroup
    {
        public Int16 GG_Id { set; get; }
        public string GG_Name { set; get; }
        public List<GoodModel> Goods { set; get; }
    }

    public class GoodModel
    {
        public Int32 Gd_Id { set; get; }
        [Display(Name = "نام کالا")]
        [Required(ErrorMessage = "نام کالای مورد نیاز باید مشخص باشد")]
        public string Gd_Name { set; get; }
        [Display(Name = "تعداد مورد نیاز")]
        [Required(ErrorMessage = "تعداد مورد نیاز باید مشخص باشد")]
        [RegularExpression(@"^\d+$", ErrorMessage = "تعداد مورد نیاز باید بصورت عددی وارد شود")]
 
        public Int64 Gd_RequiredNo { set; get; }
        public Int16 GG_Id { set; get; }
    }
    public class WhishUpdationModel
    {
        public int SeletedGroup { set; get; }

        [Display(Name = "گروه اقلام مورد نظر را انتخاب نمایید")]
        public List<GoodGroup> Groups { set; get; }
        public List<GoodModel> Goods { set; get; }
        public GoodModel AddingGood { set; get; }
        public WhishUpdationModel()
        {
            Groups = new List<GoodGroup>();
            Goods = new List<GoodModel>();
            AddingGood = new GoodModel();
        }
    }
}