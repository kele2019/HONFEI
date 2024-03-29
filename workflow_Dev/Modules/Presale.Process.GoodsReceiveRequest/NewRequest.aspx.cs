﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.ComponentModel;
using MyLib;
using Presale.Process.Common;
using Ultimus.UWF.Form.ProcessControl;

namespace Presale.Process.GoodsReceiveRequest
{
    public partial class NewRequest : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                object ProcessName = Request.QueryString["Processname"];
                object myRequest = Request.QueryString["Type"];
                string Incident = Request.QueryString["Incident"];
                if (Incident != "0")
                {
                    hdIncident.Value = Incident;
                }
                string StrqlCotcenter = @" select top(1) C.EXT02 from (
	select USERID from ORG_USER WHERE LOGINNAME='"+Page.User.Identity.Name+"') A left join ORG_JOB B on A.USERID=B.USERID left join ORG_DEPARTMENT C on B.DEPARTMENTID=C.DEPARTMENTID";
                object UserCostCenter = DataAccess.Instance("BizDB").ExecuteScalar(StrqlCotcenter);
                if (UserCostCenter != null)
                {
                    hidCostCenter.Value = UserCostCenter.ToString();
                }
                if (Incident.ToString() != "0")
                {
                    string FlagStatus = DataAccess.Instance("BizDB").ExecuteScalar("select  status from PROC_GoodsReceive where INCIDENT='" + Incident + "' ").ToString();

                    if (FlagStatus == "1" && int.Parse(Incident.ToString()) > 0 && myRequest.ToString().ToUpper() == "MYREQUEST")
                    {
                        hdUrgeTask.Value = "Yes";
                        string strRevoke = string.Format("select status from  dbo.PROC_REVOKE with(nolock) where ProcessName='{0}' and incident='{1}'", ProcessName, Incident);
                        object FlagRevoke = DataAccess.Instance("BizDB").ExecuteScalar(strRevoke);
                        if (FlagRevoke != null)
                        {
                            if (FlagRevoke.ToString() == "1")
                            {
                                btnRevoke.Visible = true;
                            }

                        }
                        object Requestor = Request.QueryString["Requestor"];
                        if (Requestor != null)
                        {
                            string CurrentUser = ConfigurationManager.AppSettings["Domain"] + "\\" + Requestor.ToString();
                            if (Page.User.Identity.Name.ToLower() == CurrentUser.ToLower())
                            {
                                btnRevoke.Visible = false;
                            }
                        }
                    }
                    else
                    {

                    }
                }

            }
            ((ButtonList)ButtonList1).BeforeSubmit += new System.ComponentModel.CancelEventHandler(NewRequest_BeforeSubmit);
            ((ButtonList)ButtonList1).AfterSubmit += new System.ComponentModel.CancelEventHandler(NewRequest_AfterSubmit);

        }


        protected void NewRequest_BeforeSubmit(object sender, CancelEventArgs g)
        {
            #region
            object Incident = Request.QueryString["Incident"];
            object ProcessName = Request.QueryString["Processname"];
            string strRevoke = string.Format("update PROC_REVOKE set status=1,OperatorDate=getdate() where  ProcessName='{0}' and incident='{1}'", ProcessName, Incident);
            DataAccess.Instance("BizDB").ExecuteNonQuery(strRevoke);
            #endregion

        }
        protected void NewRequest_AfterSubmit(object sender, CancelEventArgs g)
        {
            if (!string.IsNullOrEmpty(fld_PurchaseRequestNo.Text))
            {
                string strinsert = string.Format("update PROC_Purchase set PurchaseOrdStatus = 1 where DOCUMENTNO='" + fld_PurchaseRequestNo.Text + "'");
                DataAccess.Instance("BizDB").ExecuteNonQuery(strinsert);
            }
        }
        protected void btnAdd_Click(object sender, EventArgs e)
        {

            UserInfo userInfo = Page.FindControl("UserInfo1") as UserInfo;
            userInfo.AddNewRow(fld_detail_PROC_GoodsReceive_DT);

            string JSonstr = hdPOList.Value;
            if (!string.IsNullOrEmpty(JSonstr))
            { 
                string  JSonstrnew="["+JSonstr.TrimEnd(',')+"]";
                List<SAPPOEntity> listEntity = Newtonsoft.Json.JsonConvert.DeserializeObject<List<SAPPOEntity>>(JSonstrnew);

               List<TaxEntity> listtax=DataAccess.Instance("BizDB").ExecuteList<TaxEntity>("select * from PROC_PURCHASE_TAXCODE");

                if (listEntity.Count > 0)
                {
                    foreach (var item in listEntity)
                    {
                        item.FORMID = userInfo.FormId;
                        fld_PurchaseOrderNo.Text = item.PONo + ",";
                        item.QUANTITY = Convert.ToDecimal(item.QUANTITY).ToString("f2");
                        item.UnitPrice = Convert.ToDecimal(item.UnitPrice).ToString("f2");
                        item.OrderQty = Convert.ToDecimal(item.OrderQty).ToString("f2");
                        var taxtdata=listtax.FirstOrDefault(o=>o.ID==item.TaxCode);
                        if(taxtdata!=null)
                        {
                         item.TaxRate=taxtdata.code.ToString();
                         item.TaxAmount = (Convert.ToDecimal(item.QUANTITY) * Convert.ToDecimal(item.UnitPrice) * taxtdata.code).ToString("f2");
                        }
                        item.NonTaxAmount = (Convert.ToDecimal(item.QUANTITY) * Convert.ToDecimal(item.UnitPrice)).ToString("f2");
                    }
                     List<string> listpono=listEntity.Select(o => o.PONo).Distinct().ToList();
                     string PONOstr = string.Join(",", listpono);
                    fld_PurchaseOrderNo.Text = PONOstr;// fld_PurchaseOrderNo.Text.TrimEnd(',');
                    fld_detail_PROC_GoodsReceive_DT.DataSource = listEntity;
                    fld_detail_PROC_GoodsReceive_DT.DataBind();
                }

            }
            else
            {
                fld_PurchaseOrderNo.Text = "";
                fld_detail_PROC_GoodsReceive_DT.DataSource = null;
                fld_detail_PROC_GoodsReceive_DT.DataBind();
            }
        }
        protected void btnRevoke_Click(object sender, EventArgs e)//撤销
        {
            object ProcessName = Request.QueryString["Processname"];
            object Incident = Request.QueryString["Incident"];
            object StepName = Request.QueryString["StepName"];
            string strRevoke = string.Format("select status from  dbo.PROC_REVOKE with(nolock) where ProcessName='{0}' and incident='{1}'", ProcessName.ToString(), Incident.ToString());
            string FlagRevoke = DataAccess.Instance("BizDB").ExecuteScalar(strRevoke).ToString();
            if (FlagRevoke != "2")
            {
                if (GetOrgLevel.RevokeFunc(ProcessName.ToString(), StepName.ToString(), Incident.ToString(), Page.User.Identity.Name))
                {
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "RevokSuccess()", true);
                }
                else
                {
                    MessageBox.Show(this.Page, "撤回失败！\\nRevoke Faile!");
                }
            }
            else
            {
                MessageBox.Show(this.Page, "任务已经被处理，无法撤回！\\n Task Already Pass, Don't Revoke!");
            }
        }
		 
        protected void fld_detail_PROC_GoodsReceive_DT_ItemCommand(object source, RepeaterCommandEventArgs e)
		{
			UserInfo userInfo = Page.FindControl("UserInfo1") as UserInfo;
			userInfo.DeleteRow(fld_detail_PROC_GoodsReceive_DT, e);
		}

		protected void fld_detail_PROC_GoodsReceive_DT_ItemDataBound(object sender, RepeaterItemEventArgs e)
		{

        }
            


    }
}