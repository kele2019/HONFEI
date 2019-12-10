using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Ultimus.UWF.Home2.Code;
using Ultimus.UWF.Common.Logic;
using MyLib;
using System.Data;
using System.Text.RegularExpressions;
using Ultimus.UWF.Home2;

namespace Ultimus.UWF.Home2
{
    public partial class Index : BasePage
    {
        public string  UserChange="";
        protected void Page_Load(object sender, EventArgs e)
        {
            InitMenuCodeList();
            if (!IsPostBack)
            {
			   try { 
			   string sql = "select PICTURE from ORG_USER where loginname = '" + Page.User.Identity.Name + "'";
			   Picture Picture = DataAccess.Instance("BizDB").ExecuteEntity<Picture>(sql);
			   string pictureStatur = Picture.PICTURE;
			   string Domain = (ConfigurationManager.AppSettings["Domain"]) + "\\\\";
               
               string subUrl = Page.User.Identity.Name.ToUpper().Replace("HONFEI\\", "");

               string subUrlImg = subUrl.Replace("HonFei\\", "");// Page.User.Identity.Name.Replace("HonFei\\", "");
             string url = "../Ultimus.UWF.Home2/images/employeeImg/" + subUrlImg + ".png";
			   employeeImg.ImageUrl = url;
				}
			   catch{}
            }
        }
        public void DataBindUserInfo(string UserID)
        {
           // object UserInfo = MyLib.DataAccess.Instance("BizDB").ExecuteScalar("select  USERNAME+' '+(case  when USERNAME=EXT04 then '' else EXT04 end) from  ORG_USER where  LOGINNAME='" + UserID + "'");
			//string strsql = "select * from (";
			//strsql += " select (case  when USERNAME=EXT04 then '' else EXT04 end) USERNAME ,USERID from  ORG_USER where  LOGINNAME='" + UserID + "')A";
			//strsql += " left join   (SELECT MEMBERID FROM (select MENUID from  dbo.SEC_MENU where  MENUNAME='ChangeUserInfo') B";
			//strsql += "  LEFT JOIN dbo.SEC_MENURIGHTSMEMBER C ON B.MENUID=C.RIGHTSID) D  ON A.USERID=D.MEMBERID";
			string strsql = "select S.* from ";
			strsql += "(Select M.USERNAME USERNAME,M.USERID USERID,M.MEMBERID MEMBERID,N.DEPARTMENTNAME DEPARTMENT FROM ";
			        strsql += "( SELECT f.DEPARTMENTID,E.* FROM ";
			        strsql += "(select * from (select (case  when USERNAME=EXT04 then '' else EXT04 end) USERNAME ,USERID from  ORG_USER where  LOGINNAME='" + UserID + "')A";
			        strsql += " left join   (SELECT MEMBERID FROM (select MENUID from  dbo.SEC_MENU where  MENUNAME='ChangeUserInfo') B";
			        strsql += " LEFT JOIN dbo.SEC_MENURIGHTSMEMBER C ON B.MENUID=C.RIGHTSID) D  ON A.USERID=D.MEMBERID)E ";
			        strsql += " left join dbo.ORG_JOB F on E.USERID = F.JOBID)M ";
			        strsql += " LEFT JOIN dbo.ORG_DEPARTMENT N ON M.DEPARTMENTID = N.DEPARTMENTID)S";

                  DataTable dtUserInfo = MyLib.DataAccess.Instance("BizDB").ExecuteDataTable(strsql);
                  if (dtUserInfo.Rows.Count>0)
                  {
					lbUserName.Text =dtUserInfo.Rows[0]["USERNAME"].ToString();
					lbUserDept.Text = dtUserInfo.Rows[0]["DEPARTMENT"].ToString();
					if (dtUserInfo.Rows[0]["MEMBERID"].ToString() != "")
					{
						 UserChange = "1";
					}
                  }
        }
        private void InitMenuCodeList()
        {
            try
            {
                List<MenuInfo> menuList = new List<MenuInfo>();
                menuList.Add(new MenuInfo("CJLC", "Create.aspx"));
                menuList.Add(new MenuInfo("DBRW", "ToDoList.aspx?Type=mytask"));
                menuList.Add(new MenuInfo("YBRW", "List.aspx?Type=myapproval"));
                menuList.Add(new MenuInfo("WDSQ", "List.aspx?Type=myrequest"));
                menuList.Add(new MenuInfo("CGX", "DraftList.aspx"));
                menuList.Add(new MenuInfo("REPORT", "report.aspx?MID=Report"));
                menuList.Add(new MenuInfo("SETUP", "OtherPage.aspx?MID=2"));
                foreach (var item in menuList)
                {
                    if (item.Code != "REPORT"&&item.Code!="SETUP")
                        item.Text = LanguageHelper.Get(string.Format("Menu_{0}", item.Code));// + (item.Code == "DBRW" ? "(10)" : "")
                    else
                      if(item.Code == "REPORT")
                        item.Text = "Report";
                    if (item.Code == "SETUP")
                        item.Text = "SETUP";
                }


                string MeanuSql = string.Format(@"select M.* from (select RIGHTSID from dbo.SEC_MENURIGHTSMEMBER  where
MEMBERID=(select USERID from ORG_USER where LOGINNAME='{0}')) MM left join (select * from  SEC_MENU where MENUTYPE='MENU') M on MM.RIGHTSID=M.MENUID  where M.MENUID is not null", Page.User.Identity.Name);
                DataTable dtMeaun = DataAccess.Instance("BizDB").ExecuteDataTable(MeanuSql);

               
                if (dtMeaun.Rows.Count > 0)
                {
                    foreach (DataRow item in dtMeaun.Rows)
                    {
                        MenuInfo Mode = new MenuInfo();
                        Mode.Code = item["MENUNAME"].ToString();
                        Mode.Text = item["MENUNAME"].ToString();
                        Mode.Url = item["URL"].ToString();
                        menuList.Add(Mode);
                    }
                }
                rptMenu.DataSource = menuList;
                rptMenu.DataBind();
            }
            catch (Exception)
            {
 
            }
        }

        protected void btnChangeUserInfo_Click(object sender, EventArgs e)
        {
            string Domain = ConfigurationManager.AppSettings["Domain"];
            SessionLogic.Login(Domain+"\\"+txtUserName.Text.Trim());
            HttpContext.Current.Session["OldUserName"] = Page.User.Identity.Name;
            HttpContext.Current.Session["AgentUser"] =txtUserName.Text.Trim();
            Response.Redirect("Index.aspx");
        }
    }
}