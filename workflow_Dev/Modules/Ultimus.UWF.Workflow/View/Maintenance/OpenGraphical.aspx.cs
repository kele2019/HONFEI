using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MyLib;

namespace Ultimus.UWF.Workflow.View.Maintenanc
{
    public partial class OpenGraphical : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e) {
            string incident = Request["Incident"];
            string processName = Server.UrlDecode(Request["ProcessName"].Trim());
            try {
                //Incident objInc = new Incident();
                //bool pReturnInc = objInc.LoadIncident(processName, int.Parse(incident));

                //Incident.Status objIncStatus = new Incident.Status();
                //objInc.GetIncidentStatus(out objIncStatus);
                byte[] bytesGif = null;
                if (Session["flowpic"] != null) {
                    bytesGif = (byte[])Session["flowpic"];
                }
                //objIncStatus.GetGraphicalStatus(objInc.strProcessName, objInc.nIncidentNo, objInc.nVersion, out bytesGif);
                Response.ContentType = "image/gif";
                Response.HeaderEncoding = System.Text.Encoding.GetEncoding("gb2312");
                Response.ContentEncoding = System.Text.Encoding.GetEncoding("gb2312");
                Response.BinaryWrite(bytesGif);
                
            }
            catch(Exception ex) {
                LogUtil.Info(string.Format("获取流程图失败!错误信息：{0},流程名称：{1},实例号:{2}", ex.Message, processName, incident));
            }
            Response.End();
        }
    }
}