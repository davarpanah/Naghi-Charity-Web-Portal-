using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;

namespace test1.Connection
{
    public class Connection
    {
        public Boolean ExecutionGeneral(string RunningCommand)
        {
            try
            {
                SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["NaghiConnection"].ToString());
                con.Open();
                SqlCommand Cm = new SqlCommand("", con);
                Cm.CommandText = RunningCommand;
                Cm.ExecuteNonQuery();
                con.Close();
            }
            catch(Exception)
            {
                return(false);
            }
            return(true);
        }

        public DataSet GeneralData(string SelectCommand)
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["NaghiConnection"].ToString());
            SqlCommand cmd = new SqlCommand(SelectCommand, con);
            cmd.CommandType = CommandType.Text;
            SqlDataAdapter da = new SqlDataAdapter();
            da.SelectCommand = cmd;
            DataSet myrec = new DataSet();
            da.Fill(myrec);
            return myrec;            
        }

        public DataSet NewsData(int Nw_Type, int NewsNo=0)
        {
            DataSet myrec;
            if (NewsNo > 0)
                myrec = GeneralData(String.Format("select top({0}) Nw_Id, Nw_Title, Nw_Date, left(Nw_Description,50)+'... ' as Nw_ShortDescription, Nw_Description, Nw_Photo1, Nw_Photo2, Nw_Photo3, Nw_Photo4, u.Us_Name+' '+u.Us_LName as User_Name from [dbo].[NewsSet] inner join [dbo].[UserSet] as u on User_Us_ID=u.Us_Unic_Number where Nw_Type={1} order by Nw_Date desc ", NewsNo, Nw_Type));
            else
                myrec = GeneralData(String.Format("select Nw_Id, Nw_Title, Nw_Date, left(Nw_Description,50)+'... ' as Nw_ShortDescription, Nw_Description, Nw_Photo1, Nw_Photo2, Nw_Photo3, Nw_Photo4, u.Us_Name+' '+u.Us_LName as User_Name from [dbo].[NewsSet] inner join [dbo].[UserSet] as u on User_Us_ID=u.Us_Unic_Number where Nw_Type={0} order by Nw_Date desc", Nw_Type));
            return myrec;
        }

        public DataSet PatientData(int Supporter, int Seyyed)
        {
            String StrCom = "select [Pt_Photo_Path],[Pt_Id],[Pt_Name],[Pt_LName],[Pt_Ct_Code],[Pt_Seyyed],[Pt_Supporter],[Pt_BirthDay],[Pt_Sickness],[Pt_Sex],[Pt_Tel],[Pt_Address] from [dbo].[PatientSet] where 1=1";
            if(Supporter>0)
                StrCom=StrCom+" and [Pt_Supporter]="+Supporter;
            if (Seyyed> 0)
                StrCom=StrCom+" and [Pt_Seyyed]="+Seyyed;
            DataSet myrec = GeneralData(StrCom);
            return myrec;
        }       
        public DataSet PhotoSlideAllData()
        {
            DataSet myrec = GeneralData("select Ph_Id,Ph_Path,Ph_Showable from PhotoSet where Gallary_Ga_Id=1");
            return myrec;
        }       
        public DataSet PhotoSlideData()
        {
            DataSet myrec = GeneralData("select Ph_Id,Ph_Path,Ph_Title,Ph_Description,Ph_Link from PhotoSet where Gallary_Ga_Id=1 and Ph_Showable=1");
            return myrec;
        }
        public DataSet GoodGroupDataNull()
        {
            DataSet myrec = GeneralData("select GG_Id,GG_Name from GoodGroupSet as gr where GG_Id>1");
            return myrec;
        }
        public DataSet GoodGroupData()
        {
            DataSet myrec = GeneralData("select GG_Id,GG_Name from GoodGroupSet as gr where GG_Id>1 and (select count(Gd_Id) from GoodSet as g where gr.GG_Id=g.GoodGroup_GG_Id and g.Gd_RequiredNo>0)>0 ");
            return myrec;
        }
        public DataSet GalleryManagementData()
        {
            DataSet myrec = GeneralData("select g.Ga_Id,g.Ga_Title,g.Ga_Description,(select top(1) Ph_Path from [dbo].[PhotoSet] as p where g.Ga_Id=p.Gallary_Ga_Id and p.Ph_Showable=1) as Ph_Path from [dbo].[GallarySet] as G where g.Ga_Showable=1; ");
            return myrec;
        }
        public DataSet GoodData(int GoupId)
        {
            DataSet myrec = GeneralData("select Gd_Id,Gd_name,Gd_RequiredNo from GoodSet where GoodGroup_GG_Id="+GoupId);
            return myrec;
        }
        public int StateData(int City_Id)
        {
            int State=0;
            DataSet ds = new DataSet();
            ds = GeneralData("select [State_St_Id] from [dbo].[CitySet] where [Ct_Id]="+City_Id);
            if (ds.Tables[0].Rows.Count == 1)
            {
                DataRow dr = ds.Tables[0].Rows[0];
                State = Convert.ToInt32(dr["State_St_Id"]);
            }
            return State;
        }
        public int CountryData(int State_Id)
        {
            int Country = 0;
            DataSet ds = new DataSet();
            ds = GeneralData("select [Country_Co_Id] from [dbo].[StateSet] where [St_Id]="+State_Id);
            if (ds.Tables[0].Rows.Count == 1)
            {
                DataRow dr = ds.Tables[0].Rows[0];
                Country = Convert.ToInt32(dr["Country_Co_Id"]);
            }
            return Country;
        }

        public List<Models.MySelectListItem> GetListOfItems(String CommandStr, int? Condition)
        {
            var Items = new List<Models.MySelectListItem>();
            DataSet ds = new DataSet();
            if (Condition!=null)
                ds = GeneralData(CommandStr+Condition);
            else
                ds = GeneralData(CommandStr);
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                Items.Add(new Models.MySelectListItem
                {
                    value = Convert.ToInt32(dr["Id"]),
                    text = dr["Name"].ToString()
                });
            }
            return Items;
        }

        public List<Models.MySelectListItem> GetListOfCountries()
        {
            var Countries = new List<Models.MySelectListItem>();
            Countries = GetListOfItems("select [Co_Id] as Id, [Co_Name]+'     '+[Co_Latin] as Name from [dbo].[CountrySet]", null);// order by [Co_Id] OFFSET 1 ROWS
            return Countries;
        }
        public List<Models.MySelectListItem> GetListOfStates(int Country_Id)
        {
            var States = new List<Models.MySelectListItem>();
            States = GetListOfItems("select [St_Id] as Id, [St_Name]+'     '+[St_Latin] as Name from [dbo].[StateSet] where [St_Id]=1 or [Country_Co_Id]=", Country_Id);
            return States;
        }
        public List<Models.MySelectListItem> GetListOfCities(int State_Id)
        {
            var Cities = new List<Models.MySelectListItem>();
            Cities = GetListOfItems("select [Ct_Id] as Id,[Ct_Name]+'    '+[Ct_Latin] as Name from [dbo].[CitySet] where [Ct_Id]=(select min(Ct_Id) from [dbo].[CitySet]) or [State_St_Id]=", State_Id);
            return Cities;
        }
        public DataSet ChangeUserGroupData(string UserId)
        {
            DataSet myrec = GeneralData(String.Format("select Us_Name, Us_LName, Us_Email, Us_Unic_Number, UserGroup_Gr_Id from UserSet where Us_Active=1 and UserGroup_Gr_Id!=2 and Us_Id!='{0}'", UserId));
            return myrec;
        }
        public Boolean ChangeUserGroup(test1.Models.UserActivationModel Model)
        {
            Boolean Status = ExecutionGeneral(String.Format("update UserSet set UserGroup_Gr_Id={0} where Us_Unic_Number={1};", Model.UserGroup_Gr_Id, Model.Us_Unic_Number));
            return Status;
        }
        public DataSet ActivatedUsersData(string UserId)
        {
            DataSet myrec = GeneralData(String.Format("select Us_Name, Us_LName, Us_Email, Us_Unic_Number, Us_Active from UserSet where UserGroup_Gr_Id!=3 and (Us_Id is NULL or Us_Id!='{0}') order by Us_Active", UserId));
            return myrec;
        }
        public Boolean AddPhotoSlide(String Path)
        {
            Boolean Status = ExecutionGeneral(String.Format("insert into [dbo].[PhotoSet] (Gallary_Ga_Id,Ph_Path,Ph_Showable) values(1,'{0}',1)", Path));
            return Status;
        }
        public Boolean DeletePhotoSlide(int Ph_Id)
        {
            Boolean Status = ExecutionGeneral(String.Format("delete from [dbo].[PhotoSet] where [Ph_Id]={0};", Ph_Id));
            return Status;
        }        
        public Boolean UpdatePhotoSlide(test1.Models.PhotoSlide Model)
        {
            Boolean Status = ExecutionGeneral(String.Format("update [dbo].[PhotoSet] set [Ph_Showable]='{0}' where [Ph_Id]={1};", Model.Ph_Showable, Model.Ph_Id));
            return Status;
        }
        public Boolean ActivateUser(test1.Models.UserActivationModel Model)
        {
            Boolean Status = ExecutionGeneral(String.Format("update UserSet set Us_Active='{0}' where Us_Unic_Number={1};", Model.Us_Active, Model.Us_Unic_Number));
            return Status;
        }
        public DataSet UserData(string UserId)
        {
            DataSet myrec = GeneralData(String.Format("select [Us_Name],[Us_LName],isnull([Us_BirthDay],getdate()) as Us_BirthDay,[Us_Phone],[Us_HomeAddress],[Us_WorkAddress],[City_Ct_Id],[Us_Email],[UserGroup_Gr_Id] from [dbo].[UserSet] where [Us_ID]='{0}' and [Us_Active]=1", UserId));
            return myrec;
        }
        public DataSet DonatorData(string UserId)
        {
            DataSet myrec = GeneralData(String.Format("select d.Dr_CT_Code, u.[Us_Name],u.[Us_LName],isnull(u.[Us_BirthDay],getdate()) as Us_BirthDay,u.[Us_Phone],u.[Us_HomeAddress],u.[Us_WorkAddress],u.[City_Ct_Id], u.[Us_Mob], u.[Us_Email], d.Us_UnicNumber as Dr_Us_ID, d.Dr_AccTransaction, d.Dr_Amount, d.Dr_CardTranscation, d.Dr_Cleaner, d.Dr_Clothes, d.Dr_CT_Code, d.Dr_Endowment, d.Dr_Food, d.Dr_HelpDay, d.Dr_Id, d.Dr_Inperson, d.Dr_Medical, d.Dr_Month_Duration, d.Dr_Representative, d.Dr_Stationery, d.Us_UnicNumber from [dbo].[UserSet] u LEFT OUTER JOIN [dbo].[Donator] d on u.Us_Unic_Number=d.Us_UnicNumber" +
                                                    " where u.Us_ID='{0}' and u.Us_Active=1", UserId));
            return myrec;
        }
        public DataSet AllOnlinePaymentsData()
        {
            DataSet myrec = GeneralData("select CASE WHEN u.Us_Id IS NULL THEN 'False' ELSE 'True' END AS IsUser,  "+
                                        " u.Us_Name,u.Us_LName, u.Us_Email, u.Us_Mob, u.Us_Phone, d.Do_Id,d.Do_Date,dr.DoR_Amount from [dbo].[UserSet] as u inner join [dbo].[DonateSet] as d on u.Us_Unic_Number=d.Us_ID "+
                                        " inner join [dbo].[DonateRowSet] as dr on d.Do_Id=dr.Donate_Do_Id where  u.Us_Active=1 and d.Do_Type=0 and dr.PaymentType_Id=0 "+
                                        " order by d.Do_Id desc");
            return myrec;
        }
        public DataSet DonatorsListData()
        {
            DataSet myrec = GeneralData("select CASE WHEN u.Us_Id IS NULL THEN 'False' ELSE 'True' END AS IsUser, "+
                                         " u.Us_Unic_Number,u.Us_Name, u.Us_LName, u.Us_Email, u.Us_Mob, u.Us_Phone, d.Dr_Amount, d.Dr_Month_Duration, d.Dr_Reg_Date, d.Dr_CT_Code  " +
                                         " from [dbo].[UserSet] as u inner join [dbo].[Donator] as d on u.Us_Unic_Number=d.Us_UnicNumber "+
                                         " where u.Us_Active=1");
            return myrec;
        }
        
        public DataSet MyOnlinePaymentsData(string UserId)
        {
            DataSet myrec = GeneralData(String.Format("select u.Us_Name,u.Us_LName,d.Do_Id,d.Do_Date,dr.DoR_Amount from [dbo].[UserSet] as u inner join [dbo].[DonateSet] as d on u.Us_Unic_Number=d.Us_ID"+
                                                      " inner join [dbo].[DonateRowSet] as dr on d.Do_Id=dr.Donate_Do_Id where u.Us_Active=1 and d.Do_Type=0 and dr.PaymentType_Id=0 and u.Us_Id='{0}'", UserId));
            return myrec;
        }

        public DataSet MyPeriodicPaymentData(string UserId)
        {
            DataSet myrec = GeneralData(String.Format("select u.Us_Name, u.Us_LName,d.* from [dbo].[UserSet] as u inner join [dbo].[Donator] as d on u.Us_Unic_Number=d.Us_UnicNumber where u.Us_Id='{0}' and u.Us_Active=1" , UserId));
            return myrec;
        }


        

        public Boolean UpdateUserInformation(Models.ChangePersonalInformation model, String Us_id)
        {
            Boolean Status = ExecutionGeneral(String.Format("Update [UserSet] set [Us_Name]='{0}',[Us_LName]='{1}',[Us_BirthDay]='{2}',[Us_Phone]='{3}',[Us_HomeAddress]='{4}',[Us_WorkAddress]='{5}',[City_Ct_Id] ={6} where [Us_ID]='{7}' and [Us_Active]=1;", model.Us_Name, model.Us_LName, model.Us_BirthDay.ToShortDateString(), model.Us_Phone, model.Us_HomeAddress, model.Us_WorkAddress, model.City_Ct_Id, Us_id));
            return Status;
        }

        public Boolean AddGood(Models.GoodModel model)
        {
            Boolean Status = ExecutionGeneral(String.Format("insert into GoodSet (Gd_Name,Gd_RequiredNo,GoodGroup_GG_Id)values('{0}',{1},{2});", model.Gd_Name, model.Gd_RequiredNo, model.GG_Id));
            return Status;
        }
        public Boolean UpdateGood(Models.GoodModel model)
        {
            Boolean Status = ExecutionGeneral(String.Format("update GoodSet set Gd_RequiredNo={0} where Gd_Id={1};", model.Gd_RequiredNo, model.Gd_Id));
            return Status;
        }
        public Boolean UpdateDonatorInformation(Models.PeriodicPaymentModel model, String Us_id)
        {
            Boolean Status = ExecutionGeneral(String.Format("Update [UserSet] set [Us_Name]='{0}',[Us_LName]='{1}',[Us_BirthDay]='{2}',[Us_Phone]='{3}',[Us_HomeAddress]='{4}',[Us_WorkAddress]='{5}',[City_Ct_Id] ={6} where [Us_ID]='{7}' and  and [Us_Active]=1;", model.Us_Name, model.Us_LName, model.Us_BirthDay.ToShortDateString(), model.Us_Phone, model.Us_HomeAddress, model.Us_WorkAddress, model.City_Ct_Id, Us_id));
            return Status;
        }

        public Boolean UpdateUserPhone(String Us_id, String PhoneNo)
        {
            Boolean Status = ExecutionGeneral(String.Format("Update [UserSet] set [Us_Mob]='{0}' where [Us_ID]='{1}' and [Us_Active]=1;", PhoneNo, Us_id));
            return Status;
        }    
         public Boolean AddUser(String Us_id, String Email)
        {
            Boolean Status = ExecutionGeneral(String.Format("insert into [dbo].[UserSet]([Us_ID],[Us_Email],[UserGroup_Gr_Id],[Us_Active],[Us_Reg_Date],[Us_BirthDay],[Us_Unic_Number],[City_Ct_Id])values('{0}','{1}',1,1,GETDATE(),GETDATE(),(select max([Us_Unic_Number]) from [dbo].[UserSet])+1,5)", Us_id, Email));//hashem 5 should be changed to 1 
            return Status;
        }

         public Boolean AddUserAndDonator(Models.PeriodicPaymentModel Model)
         {
             Boolean MyError=false;
             Boolean Status = ExecutionGeneral(String.Format("exec {0}=AddUserAndDonator '{1}','{2}',{3},{4},'{5}','{6}','{7}','{8}',{9},{10},{11},{12},{13},{14},{15},{16},{17},{18},{19},{20},{21},{22}", MyError, Model.Us_Name, Model.Us_LName, Model.Us_BirthDay, Model.Us_Phone, Model.Us_Mob, Model.Us_HomeAddress, Model.Us_Email, Model.Us_WorkAddress, Model.City_Ct_Id, Model.Dr_HelpDay, Model.Dr_CT_Code, Model.Dr_AccTransaction, Model.Dr_Inperson, Model.Dr_Representative, Model.Dr_Month_Duration, Model.Dr_Amount, Model.Dr_Food, Model.Dr_Clothes, Model.Dr_Stationery, Model.Dr_Medical, Model.Dr_Cleaner, Model.Dr_Endowment));
             return MyError & Status;
         }

        public Boolean UpdateUserAndDonator(Models.PeriodicPaymentModel Model, String User_Id)
         {
             Boolean Error=false;
             Boolean Status = ExecutionGeneral(String.Format("exec {0}=UpdateUserAndDonator '{1}','{2}',{3},'{4}','{5}',{6},{7},{8},{9},{10},{11},{12},{13},{14},{15},{16},{17},{18},{19},'{20}'", Error,
                 Model.Us_Name, Model.Us_LName, Model.Us_Phone, Model.Us_HomeAddress, Model.Us_WorkAddress, Model.City_Ct_Id, Model.Dr_HelpDay, Model.Dr_CT_Code, Model.Dr_AccTransaction, Model.Dr_Inperson, Model.Dr_Representative, Model.Dr_Month_Duration, Model.Dr_Amount, Model.Dr_Food, Model.Dr_Clothes, Model.Dr_Stationery, Model.Dr_Medical, Model.Dr_Cleaner, Model.Dr_Endowment, User_Id));
             return Error & Status;
         }
        public Boolean AddUserAndPayment(Models.OnlinePaymentModel Model)
        {
            Boolean MyError = false;
            Boolean Status = ExecutionGeneral(String.Format("exec {0}=AddUserAndPayment '{1}','{2}',{3},'{4}','{5}','{6}','{7}',{8},{9},{10},{11},{12}", MyError, Model.Us_Name, Model.Us_LName, Model.Us_Phone, Model.Us_Mob, Model.Us_HomeAddress, Model.Us_Email, Model.Us_WorkAddress, Model.City_Ct_Id,Model.Do_Type, Model.DoR_Amount, Model.Good_Gd_Id, Model.PaymentType_Id));
            return MyError & Status;
        }

        public Boolean AddGallery(Models.Gallery Model)
        {
            Boolean Status = ExecutionGeneral(String.Format("insert into [dbo].[GallarySet] ([Ga_Title],[Ga_Description],[Ga_Showable]) values('{0}','{1}',1)", Model.Ga_Title, Model.Ga_Title));
            if(Status)
            {
                DataSet myrec = GeneralData("select max(Ga_Id) Ga_Id into from [dbo].[GallarySet]");
                if (myrec.Tables[0].Rows.Count > 0)
                {
                    int Ga_Id = Int32.Parse(myrec.Tables[0].Rows[0]["Ga_Id"].ToString());
                    foreach(Models.Photo Photo in Model.Photoes)
                    {
                        Status = ExecutionGeneral(String.Format("insert into [dbo].[PhotoSet] ([Ph_Description],[Ph_Title],[Ph_Path],[Gallary_Ga_Id],[Ph_Showable]) values('{0}','{1}','{2}',{3},1)", Photo.Ph_Description, Photo.Ph_Title, Photo.Ph_Path, Ga_Id));
                        if (!Status)
                            return (Status);
                    }
                }
            }
            return Status;
        }

        public Boolean UpdateUserAndPayment(Models.OnlinePaymentModel Model, String User_Id)
         {
             Boolean Error=false;
             Boolean Status = ExecutionGeneral(String.Format("exec {0}=UpdateUserAndPayment '{1}','{2}',{3},'{4}','{5}',{6},'{7}',{8},{9},{10},{11}", Error,Model.Us_Name, Model.Us_LName, Model.Us_Phone, Model.Us_HomeAddress, Model.Us_WorkAddress, Model.City_Ct_Id,User_Id,Model.Do_Type, Model.DoR_Amount, Model.Good_Gd_Id, Model.PaymentType_Id));
             return Error & Status;
         }
    }
}