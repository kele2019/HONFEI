using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Ultimus.UWF.OrgChart.Entity;
using Ultimus.UWF.Home2.Code.DAO;
using Ultimus.UWF.Home2.Code.Entity;
using Ultimus.UWF.Home2.Code;
using System.Configuration;
using MyLib;
using System.IO;

namespace Ultimus.UWF.Home2
{
    public partial class UserEdit : BasePage
    {
        protected int Query_ParentID { get { int i = 0; int.TryParse(Request.QueryString["ParentID"], out i); return i; } }
        protected int Query_ID { get { int i = 0; int.TryParse(Request.QueryString["ID"], out i); return i; } }
        protected string Query_ReturnUrl { get { return Request.QueryString["ReturnUrl"]; } }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadUI();
                BindData();
            }
        }

        private void LoadUI()
        {
        }

        private void BindData()
        {
            if (Query_ID != 0)
            {
                UserEntityExt user = UserMgmt.Instance.Get(Query_ID);
                if (user != null)
                {
                    string domian = System.Configuration.ConfigurationManager.AppSettings["Domain"];
                    txtName.Text = user.USERNAME;
                    txtEngName.Text = user.EngName;
                    int domainIndex = user.LOGINNAME.IndexOf('\\')+1;
                    txtLoginName.Text = user.LOGINNAME.Substring(domainIndex);// user.LOGINNAME.Replace(domian + "\\", "");
                    txtUserCode.Text = user.USERCODE;
                    txtEmail.Text = user.EMAIL;
                    txtMobile.Text = user.MOBILENO;
                    txtSort.Text = user.ORDERNO != null && user.ORDERNO.HasValue ? user.ORDERNO.Value.ToString() : "";
                    chkEnable.Checked = user.ISACTIVE == "1";
                    txtGW.Text = user.GW;
                    //txtGWEngName.Text = user.EXT06;
                  //  ddlGWLevel.SelectedValue = user.GWLevel;
                    txtCBCenter.Text = user.EXT05;
                    hidLeaderLoginName.Value = user.Leader;
                    txtBirthDay.Text = user.EXT07;
                    txtSex.SelectedValue = user.EXT08;
                    txtJoinDate.Text = user.EntryDate;
                    txtFirstWorkDate.Text = user.StartWorkDate;
                    dropthanTwenty.SelectedValue = user.EXT15;
                    txtNational.SelectedValue = user.EXT11;
                    txtDepartureDate.Text = user.EXT12;
                    //txtRole.SelectedValue = user.EXT13;
                   // dropAllowancelevel.SelectedValue = user.EXT14;
                    txtAnualDay.Text = GetAnnualInfo(user.LOGINNAME.Replace("\\", "/"));
                    if (!string.IsNullOrWhiteSpace(user.Leader))
                    {
                        UserEntityExt leader = UserMgmt.Instance.Get(user.Leader);
                        if (leader != null)
                        {
                            txtLeader.Text = leader.USERNAME;
                            txtLeaderEngName.Text = leader.EngName;
                            hidLeaderLoginName.Value = leader.LOGINNAME;
                        }
                    }

                    if (user.OrgID > 0)
                    {
                        DepartmentEntityExt parentorg = OrgMgmt.Instance.Get(user.OrgID);
                        if (parentorg != null)
                        {
                            var orgLevel1 = GetOrgLevel(1, parentorg.Path, parentorg.DEPARTMENTID);
                            if (orgLevel1 != null)
                            {
                                txtOrgName.Text = orgLevel1.DEPARTMENTNAME;
                                txtOrgEngName.Text = orgLevel1.EngName;
                                hidOrgID.Value = orgLevel1.DEPARTMENTID.ToString();
                            }

                            var orgLevel2 = GetOrgLevel(2, parentorg.Path, parentorg.DEPARTMENTID);
                            //if (orgLevel2 != null && orgLevel1.DEPARTMENTID != orgLevel2.DEPARTMENTID)
                            if (orgLevel2 != null)
                            {
                                txtOrgName2.Text = orgLevel2.DEPARTMENTNAME;
                                txtOrgEngName2.Text = orgLevel2.EngName;
                               // txtCBCenter.Text = orgLevel2.CBCenter;
                                hidOrgID2.Value = orgLevel2.DEPARTMENTID.ToString();
                            }
                        }
                    }
                }
            }
            else
            {
                txtUserCode.Text = UserCodeFmt(UserMgmt.Instance.GetMaxUserCode() + 1);
                if (Query_ParentID != 0)
                {
                    DepartmentEntityExt org = OrgMgmt.Instance.Get(Query_ParentID);
                    if (org != null)
                    {
                        if (org.Path.Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries).Length == 0)
                        {
                        }
                        else
                        {
                            var orgLevel1 = GetOrgLevel(1, org.Path, org.DEPARTMENTID);
                            if (orgLevel1 != null)
                            {
                                txtOrgName.Text = orgLevel1.DEPARTMENTNAME;
                                txtOrgEngName.Text = orgLevel1.EngName;
                                hidOrgID.Value = orgLevel1.DEPARTMENTID.ToString();
                            }

                            var orgLevel2 = GetOrgLevel(2, org.Path, org.DEPARTMENTID);
                            if (orgLevel2 != null && orgLevel1.DEPARTMENTID != orgLevel2.DEPARTMENTID)
                            {
                                txtOrgName2.Text = orgLevel2.DEPARTMENTNAME;
                                txtOrgEngName2.Text = orgLevel2.EngName;
                                txtCBCenter.Text = orgLevel2.CBCenter;
                                hidOrgID2.Value = orgLevel2.DEPARTMENTID.ToString();
                            }
                        }
                    }
                }
                else
                {
                    txtOrgName.Text = OrgMgmt.RootOrgName;
                    txtOrgEngName.Text = OrgMgmt.RootOrgEngName;
                }
            }
            txtOrgEngName.ReadOnly = txtOrgEngName2.ReadOnly = txtLeaderEngName.ReadOnly = true;
        }

        private DepartmentEntityExt GetOrgLevel(int level, string orgPath, int currentOrgID)
        {
            List<string> ps = (orgPath + string.Empty).Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries).ToList();

            int orgID = 0;
            if (ps.Count <= 1)
            {
                orgID = currentOrgID;
            }
            else
            {
                int orgIDLevel1 = int.Parse(ps[1]);
                int orgIDLevel2 = ps.Count >= 3 ? int.Parse(ps[2]) : currentOrgID;
                if (level == 1)
                    orgID = orgIDLevel1;
                else if (level == 2)
                    orgID = orgIDLevel2;
            }
            if (orgID == 0)
                return null;
            return OrgMgmt.Instance.Get(orgID);
        }

        private string UserCodeFmt(int userCode)
        {
            string result = "";
            int l = 4 - userCode.ToString().Length;
            for (int i = 0; i < l; i++)
            {
                result = "0" + result;
            }
            return result + userCode.ToString();
        }

        protected void btnSave_Click(object sender, ImageClickEventArgs e)
        {
            UserEntityExt user = null;
            if (Query_ID == 0)
            {
                user = new UserEntityExt();
            }
            else
            {
                user = UserMgmt.Instance.Get(Query_ID);
            }
            user.OrgID = int.Parse(hidOrgID2.Value);
            string domian = System.Configuration.ConfigurationManager.AppSettings["Domain"];
            user.USERNAME = txtName.Text;
            user.LOGINNAME = domian + "\\" + txtLoginName.Text.ToLower();
            user.USERCODE = txtUserCode.Text;
            user.EMAIL = txtEmail.Text;
            user.MOBILENO = txtMobile.Text;
            int sort = 0;
            int.TryParse(txtSort.Text, out sort);
            user.ORDERNO = sort;
            user.GW = txtGW.Text;
           // user.GWEngName = txtGWEngName.Text;
            //user.GWLevel = ddlGWLevel.SelectedValue;
            user.CBCenter = txtCBCenter.Text;
            user.EngName = txtEngName.Text;
            user.ISACTIVE = chkEnable.Checked ? "1" : "0";
            user.Leader = hidLeaderLoginName.Value;
            user.EXT05 = txtCBCenter.Text;
            user.EXT07 = txtBirthDay.Text;
            user.EXT08 = txtSex.SelectedItem.Value;
            user.EXT09 = txtJoinDate.Text;
            user.EXT10 = txtFirstWorkDate.Text;
            user.EXT15 = dropthanTwenty.SelectedItem.Value;
            user.EXT11 = txtNational.SelectedItem.Value;
            user.EXT12 = txtDepartureDate.Text;
          //  user.EXT13 = txtRole.SelectedItem.Value;
            //user.EXT14 = dropAllowancelevel.SelectedItem.Value;
            var leader = UserMgmt.Instance.Get(user.Leader);
            if (leader != null)
            {
                user.JobID = leader.JobID;
            }

            if (Query_ID == 0)
            {
                user = UserMgmt.Instance.Create(user);
                //Query_ID = user.USERID;
                //int userid = user.USERID;
                //string strinsert = string.Format("exec checkNEWUserALday'" + user.USERID + "'");
                CreateAnnualInfo(user.LOGINNAME.Replace("\\","/"), Convert.ToDecimal(txtAnualDay.Text));
            }
            else
            {
               UserMgmt.Instance.Update(user);
                //string strinsert = string.Format("exec checkNEWUserALday'" + Query_ID + "'");
               UpdateAnnualInfo(user.LOGINNAME.Replace("\\", "/"), Convert.ToDecimal(txtAnualDay.Text));
            }
            if (fileUserPhotoImg.HasFile)
                UploadUserImag("1", txtLoginName.Text,txtEngName.Text);
            if (fileUserSignImg.HasFile)
				UploadUserImag("2", txtLoginName.Text, txtEngName.Text);

            Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('操作成功');parent.location.reload()", true);
            // Response.Redirect(Query_ReturnUrl);
        }

        protected void btnCancel_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect(Query_ReturnUrl);
        }

        protected void btnDelete_Click(object sender, ImageClickEventArgs e)
        {
            UserMgmt.Instance.Delete(Query_ID);
            Response.Redirect(Query_ReturnUrl);
        }

        public void UploadUserImag(string Flag,string UserInfo,string EnglishName)
        {
            string path = "";//
            string UserPhotoImgPath = MyLib.ConfigurationManager.AppSettings["UserPhotoImgPath"];
            string UserSingImgPath = MyLib.ConfigurationManager.AppSettings["UserSignImgPath"];

             if(Flag=="1")
                 path = Server.MapPath(UserPhotoImgPath);
             else
                 path = Server.MapPath(UserSingImgPath);

            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            string str = path + UserInfo +".png";
			string strSign = path + EnglishName + ".png";

            if (Flag == "1")
                fileUserPhotoImg.SaveAs(str);
            else
				fileUserSignImg.SaveAs(strSign);
        }
        /// <summary>
        /// 获取用户年假信息
        /// </summary>
        /// <param name="LoginName"></param>
        /// <returns></returns>
        public string GetAnnualInfo(string LoginName)
        {
            string Strsql = "select * from COM_LevalManager where LeaveYear='" + DateTime.Now.Year + "' and UserAccount='" + LoginName + "'";
            System.Data.DataTable dtAnnual = DataAccess.Instance("BizDB").ExecuteDataTable(Strsql);
            if (dtAnnual.Rows.Count > 0)
            {
                return dtAnnual.Rows[0]["LeaveYearCount"].ToString();
            }
            return "0";
        }

        /// <summary>
        /// 更改用户年假信息
        /// </summary>
        public bool CreateAnnualInfo(string UserAccount, decimal AnnualDay)
        {
            string Strsql = @"insert into COM_LevalManager(UserAccount,LeaveYear,LeaveYearCount,LeaveYearHourCount,LeaveLastYearHourCount,FuallpaySick,CountLeave)
  values('" + UserAccount + "','" + DateTime.Now.Year + "','" + AnnualDay + "','" + (AnnualDay * 8) + "','0','5','" + AnnualDay + "')";
            return DataAccess.Instance("BizDB").ExecuteNonQuery(Strsql) > 0;
        }
        public bool UpdateAnnualInfo(string UserAccount, decimal AnnualDay)
        {
            string Strsql = @"update COM_LevalManager set CountLeave='" + AnnualDay + "', LeaveYearCount='" + AnnualDay + "',LeaveYearHourCount='" + (AnnualDay * 8) + "' where UserAccount='" + UserAccount + "' and LeaveYear='" + DateTime.Now.Year + "'";
            return DataAccess.Instance("BizDB").ExecuteNonQuery(Strsql) > 0;
        }
    }
}