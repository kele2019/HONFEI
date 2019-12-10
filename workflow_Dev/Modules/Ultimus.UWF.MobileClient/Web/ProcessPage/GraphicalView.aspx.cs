using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ClientService;

namespace Ultimus.UWF.Workflow
{
    public class GraphicalView : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string incident = Request.QueryString["Incident"];
            string processName = Server.UrlDecode(Request.QueryString["ProcessName"].Trim());
            try
            {
                WorkflowRef services = new WorkflowRef();
                 byte[] bytesGif = services.GetGraphicalStatus(processName,Convert.ToInt32( incident)); 
                Response.ContentType = "image/gif";
                Response.HeaderEncoding = System.Text.Encoding.GetEncoding("gb2312");
                Response.ContentEncoding = System.Text.Encoding.GetEncoding("gb2312");
                Response.BinaryWrite(bytesGif);
                Response.End();
            }
            catch(Exception ex)
            {
                //throw new Exception(ee.Message);
                Response.End();
            }
        }
    }
}