using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using MyLib;

namespace Presale.Process.EmployeeTraining
{
    public partial class HRApproval : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        protected void RPList_PreRender(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string Incident = Request.QueryString["Incident"];
                string Strsql = @" select B.EXT04,A.SumDate,A.USERID from (
  select UserID,SumDate from COM_EmployeeTrainSignInfo where TrainDocmentNo=( select DOCUMENTNO from  PROC_EmployeeTraining where  incident='" + Incident + "') )A left join ORG_USER B on A.UserID=B.USERID";
                DataTable dtUserInfo = DataAccess.Instance("BizDB").ExecuteDataTable(Strsql);
                RPList.DataSource = dtUserInfo;
                RPList.DataBind();
            }
        }
    }
}