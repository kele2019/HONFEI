using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using EntityLibrary;
using DALLibrary;
using MyLib;

namespace MobileClientBackground
{
    public partial class ProcessConfig : BasePage.BasePageClass
    {
        private DALLibrary.MobileClient_Process ProcessDal = new DALLibrary.MobileClient_Process();

        public string LoGoFileType = System.Configuration.ConfigurationManager.AppSettings["LoGoFileType"].ToString().Trim().ToLower();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Request.QueryString["ProcessName"] != null)
                {
                    lbProcessName.Text = Request.QueryString["ProcessName"].ToString().Trim();
                }
                if (Request.QueryString["ID"] != null)
                {
                    ViewState["ID"] = Request.QueryString["ID"].ToString().Trim();
                }
                BingData();
            }
        }

        private void BingData()
        {
            try
            {
                string where = "";
                int id = 0;
                if (ViewState["ID"] != null)
                {
                    id = Convert.ToInt32(ViewState["ID"].ToString());
                }
                if (id > 0)
                {
                    where += "id='" + id.ToString() + "' and ProcessName='" + lbProcessName.Text + "'";
                }
                else
                {
                    where += "ProcessName='" + lbProcessName.Text + "'";
                }
                ProcessID.Value = ViewState["ID"].ToString();
                List<EntityLibrary.MobileClient_Process> list = ProcessDal.GetModel(where);
                if (list.Count > 0)
                {
                    EntityLibrary.MobileClient_Process info = list[0];
                    lbLogo.Text = info.Logo;
                    if(!String.IsNullOrEmpty(info.Logo))
                        viewimage.Visible = true;
                    else
                        viewimage.Visible = false;
                }
            }
            catch (Exception ex)
            {
                PublicClass.ShowMessage(this.Page, Resources.Resource.ProcessConfig_ErrorMessage1);
                PublicClass.WriteLogOfTxt(ex.Message);
            }
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            try
            {
                string FileName = FileUpload1.FileName;
                string FileType = FileName.Substring(FileName.LastIndexOf(".") + 1);
                string NewName = DateTime.Now.ToString("yyyyMMddhhmmss") + "." + FileType;
                string FilePath = Server.MapPath("UpLoadFile/" + NewName);
                FileUpload1.SaveAs(FilePath);

                int id = Convert.ToInt32(ViewState["ID"].ToString());

                if (id > 0)
                {
                    EntityLibrary.MobileClient_Process info = ProcessDal.GetModel(id);
                    info.Logo = NewName;
                    ProcessDal.Update(info);
                    ViewState["ID"] = id;
                }
                else
                {
                    EntityLibrary.MobileClient_Process info = new EntityLibrary.MobileClient_Process();
                    info.ProcessName = lbProcessName.Text.Trim();
                    info.CreateTime = DateTime.Now;
                    info.Logo = NewName;
                    info.ID = ConvertUtil.ToInt32(DateTime.Now.ToOADate() * 10000);
                    int newid = ProcessDal.Add(info);
                    ViewState["ID"] = newid;
                }

                BingData();

            }
            catch (Exception ex)
            {
                PublicClass.ShowMessage(this.Page, Resources.Resource.ProcessConfig_UpLoadErrorMessage1);
                PublicClass.WriteLogOfTxt(ex.Message);
            }
        }

    }
}