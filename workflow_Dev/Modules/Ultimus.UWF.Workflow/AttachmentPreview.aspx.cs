using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MyLib;

namespace Ultimus.UWF.Workflow
{
    public partial class AttachmentPreview : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string name = "";
            string formid = Request.QueryString["FormID"]; 
            string type = Request.QueryString["Type"];
            name = ConvertUtil.ToString(DataAccess.Instance("BizDB").ExecuteScalar("select newname+filetype from wf_attachment where formid='"+formid+"' and type='"+type+"'"));
            if (!string.IsNullOrEmpty(name))
            {
                string sz = name.Split('~')[0];
                string aa = name.Split('.')[1];
                name = sz + "." + aa;
                Response.Redirect("http://eip.quanyou.com.cn/_layouts/15/WopiFrame2.aspx?sourcedoc=/Ultimus/attachment/" + name + "&action=embedview");
            }
        }
    }
}