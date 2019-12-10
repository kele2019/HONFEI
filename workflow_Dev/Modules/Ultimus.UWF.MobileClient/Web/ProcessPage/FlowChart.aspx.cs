using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MobileClient.PublicFunctionClass;
using System.Drawing;
//using MobileClient.MobileClientBackgroundRef;
using ClientService;

namespace MobileClient
{
    public partial class FlowChart : BasePageClass.BasePage
    {
        private WorkflowRef services = new WorkflowRef();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BingProcessMonitoring();
            }
        }

        private void BingProcessMonitoring()
        {
            try
            {
                //int incident = Convert.ToInt32(Request.QueryString["Incident"].ToString());
                //string processName = Server.UrlDecode(Request.QueryString["ProcessName"].ToString().Trim());
                //byte[] bytesGif = services.GetGraphicalStatus(processName, incident);
                ////Response.ContentType = "image/gif";
                ////Response.ContentEncoding = System.Text.Encoding.GetEncoding("gb2312");
                ////Response.BinaryWrite(bytesGif);
                //Session["flowpic"] = bytesGif;


                //System.IO.MemoryStream ms = new System.IO.MemoryStream(bytesGif);
                //System.Drawing.Image img = System.Drawing.Image.FromStream(ms);
                //string filename = DateTime.Now.ToString("yyyyMMddhhmmss") + ".gif";
                //string filepath = Server.MapPath("../ProcessImage/" + filename);
                //img.Save(filepath);

                //Image1.ImageUrl = "../ProcessImage/" + filename;

            }
            catch(Exception ex)
            {
                PublicClass.ShowMessage(this.Page, Resources.Resource.ProcessMonitoring_ErrorMessage1);
                PublicClass.WriteLogOfTxt(ex.Message);
            }
        }

    }
}