using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Data;
using Ultimus.UWF.Workflow.Interface;
using MyLib;
using System.Text;
using System.Collections;
using System.ComponentModel;
using Ultimus.UWF.Form.ProcessControl.Entity;
using Ultimus.UWF.Form.ProcessControl.Logic;
using Ultimus.UWF.Workflow.Logic;
using System.Data.Common;
using Ultimus.UWF.OrgChart.Entity;
using Ultimus.UWF.OrgChart.Interface;
using Ultimus.UWF.Workflow.Entity;
using Ultimus.UWF.Common.Logic;

namespace Ultimus.UWF.Form.ProcessControl
{
    public partial class ButtonList : System.Web.UI.UserControl
    {
        public event CancelEventHandler BeforeSubmit;
        public event CancelEventHandler AfterSubmit;
        ITask _task = ServiceContainer.Instance().GetService<ITask>();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                UserInfo userInfo = Page.FindControl("UserInfo1") as UserInfo;
                string type = Request.QueryString["Type"];
                btnSaveDraft.Visible = false;
                switch (type.ToUpper().Trim())
                {
                    case "NEWREQUEST":
                        btnSaveDraft.Visible = true;
                        btnXieban.Visible = false;
                         btnCopy.Visible = false;
                        break;
                    case "DRAFT":
                        btnSaveDraft.Visible = true;
                        btnSubmit.Visible = true;
                        break;
                    case "MYAPPROVAL":
                    case "MYREQUEST":
                        btnSubmit.Visible = false;
                        btnReject.Visible = false;
                        break;
                    case "MYTASK":
                        btnAsk.Visible = true;
                        btnSubmit.Text = "Approve";
                        btnBack.Visible = true;
                        break;
                }

                string taskid = userInfo.TaskId;
                if (string.IsNullOrEmpty(taskid)) //没有任务编号，提交不可见
                {
                    btnSubmit.Visible = false;
                }
                else
                {
                    if (!taskid.StartsWith("S")) //已完成任务，提交不可见
                    {
                        CommonLogic cl = new CommonLogic();
                        int iStatus = cl.GetTaskStatusBySql(taskid);
                        if (iStatus != 1)
                        {
                            btnSubmit.Visible = false;
                            btnBack.Visible = false;
                        }
                    }
                }

                if (Request.QueryString["XieBan"] == "1" || Request.QueryString["StepName"]=="发起人")
                {
                    btnXieban.Visible = false;
                    btnCopy.Visible = false;
                }
                if (Request.QueryString["Copy"] == "1")
                {
                    btnXieban.Visible = false;
                    btnCopy.Visible = false;
                    btnSaveDraft.Visible = false;
                    btnSubmit.Visible = false;
                }
                if (Request.QueryString["StepName"] == "财务记账")
                {
                    divGoto.Visible = true;
                }

                //多语言
                //btnSubmit.Text = Resources.lang.Submit;
                btnClose.Text = Resources.lang.Close;
                //btnSaveDraft.Text = Resources.lang.SaveDraft;
                btnXieban.Value = Lang.Get("ButtonList_Xieban");
                btnCopy.Value = Lang.Get("ButtonList_Copy");
                btnGoto.Value = Lang.Get("ButtonList_Goto");
            }
        }

        string GetLoginName(UserInfo userInfo)
        {
            string strApplicantAccout = null;
            if (string.IsNullOrEmpty(SessionLogic.GetLoginName())) //没有登录或者用户取不到 
            {
                LogUtil.Error("B001提交失败！未找到用户信息 IdentityName:" + SessionLogic.GetLoginName() + " SessionUserName:" + ConvertUtil.ToString(HttpContext.Current.Session["loginName"]));
                strApplicantAccout = ((TextBox)userInfo.FindControl("txtApplicantAccount")).Text.Replace("/", "\\");
                if (strApplicantAccout.Trim() == "")
                {
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('提交失败！未找到用户信息！');", true);
                    return "";
                }
                else
                {
                    SessionLogic.Login(strApplicantAccout);
                }
            }
            else
            {
                strApplicantAccout = SessionLogic.GetLoginName();
            }
            LogUtil.Info("B002提交！IdentityName:" + strApplicantAccout + " SessionUserName:" + ConvertUtil.ToString(HttpContext.Current.Session["loginName"]));
            return strApplicantAccout;
        }

        bool SaveBusinessData(DataSet formDS, string tableName, string formID, string processPrefix, 
            ref Hashtable vars, ref List<string> detailTables,bool snCreate,ref string documentNo)
        {
            DataAccess dac = new DataAccess("BizDB");
            DbCommand cmd = dac.CreateCommand();
            //获取流程编号 前缀+年月日+3位流水号
            if (snCreate)
            {
                SerialNoLogic sn = new SerialNoLogic();
                 documentNo = string.Format("{0}{1}{2}{3}{4}", processPrefix,
                    DateTime.Now.Year, DateTime.Now.Month.ToString().PadLeft(2, '0'),DateTime.Now.Day.ToString().PadLeft(2, '0'),
                    sn.GetSerialNo(processPrefix, DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day).ToString().PadLeft(5, '0'));
                // DateTime.Now.Day.ToString().PadLeft(2, '0'),
            }
            //1.保存数据
            StringBuilder sb = new StringBuilder();
            int i = 0;
            foreach (DataTable dt in formDS.Tables)
            {
                if (dt.Rows.Count == 0)
                {
                    continue;
                }
                if (dt.TableName != "MainTable" && dt != null)
                {
                    detailTables.Add(dt.TableName);
                    //1.0 明细表插入
                    sb.AppendFormat("DELETE FROM {0} WHERE  FORMID='{1}';", dt.TableName, formID);
                    foreach (DataRow row in dt.Rows)
                    {
                        StringBuilder sbItem = new StringBuilder();
                        StringBuilder sbItemValue = new StringBuilder();
                        foreach (DataColumn col in dt.Columns)
                        {
                            sbItem.Append(col.ColumnName + ",");
                            sbItemValue.Append(dac.ParameterPrefix + col.ColumnName+"_"+i.ToString() + ",");

                            object value = row[col.ColumnName];
                            if(string.IsNullOrEmpty(ConvertUtil.ToString(value)))
                            {
                                value=DBNull.Value;
                            }
                            if (col.ColumnName.ToUpper().IndexOf("DATE") >= 0)
                            {
                                dac.AddInParameter(cmd, dac.ParameterPrefix + col.ColumnName + "_" + i.ToString(), DbType.Date, value);
                            }
                            else
                            {
                                dac.AddInParameter(cmd, dac.ParameterPrefix + col.ColumnName + "_" + i.ToString(), DbType.String, value);
                            }
                        }
                        sb.AppendFormat("insert into {0}({1}) values({2});", dt.TableName, 
                            sbItem.ToString().TrimEnd(','), sbItemValue.ToString().TrimEnd(','));

                        i++;
                    }
                }

                //1.1 主表插入
                if (dt.TableName == "MainTable")
                {
                    string strQuery = "select count(1) from " + tableName + " where FORMID ='" + formID + "'";
                    int iCount = ConvertUtil.ToInt32(DataAccess.Instance("BizDB").ExecuteScalar(strQuery));
                    //判断是新建申请还是待办任务
                    if (iCount > 0)
                    {
                        StringBuilder sbItem = new StringBuilder();
                        //1.1.1 存在更新
                        foreach (DataColumn col in dt.Columns)
                        {
                            if (!cmd.Parameters.Contains(dac.ParameterPrefix + col.ColumnName))
                            {
                                sbItem.AppendFormat("{0}={1}{0},", col.ColumnName, dac.ParameterPrefix);
                                object value = dt.Rows[0][col.ColumnName];
                                if (string.IsNullOrEmpty(ConvertUtil.ToString(value)))
                                {
                                    value = DBNull.Value;
                                }

                                if (col.ColumnName.ToUpper().IndexOf("DATE") >= 0)
                                {
                                    dac.AddInParameter(cmd, dac.ParameterPrefix + col.ColumnName, DbType.Date, value);
                                }
                                else
                                {
                                    dac.AddInParameter(cmd, dac.ParameterPrefix + col.ColumnName, DbType.String, value);
                                }

                                vars.Add(col.ColumnName, dt.Rows[0][col.ColumnName].ToString());
                            }
                        }
                        sb.AppendFormat("update {0} set {1} where FORMID='{2}';", tableName, sbItem.ToString().TrimEnd(','), formID);

                    }
                    else
                    {
                        //1.1.2 不存在，插入
                        StringBuilder sbItem = new StringBuilder();
                        StringBuilder sbItemValue = new StringBuilder();
                        foreach (DataColumn col in dt.Columns)
                        {
                            if (!cmd.Parameters.Contains(dac.ParameterPrefix + col.ColumnName))
                            {
                                sbItem.AppendFormat("{0},", col.ColumnName);
                                sbItemValue.AppendFormat("{1}{0},", col.ColumnName, dac.ParameterPrefix);
                                object value = dt.Rows[0][col.ColumnName];
                                if (string.IsNullOrEmpty(ConvertUtil.ToString(value)))
                                {
                                    value = DBNull.Value;
                                }
                                if (col.ColumnName.ToUpper().IndexOf("DATE") >= 0)
                                {
                                    dac.AddInParameter(cmd, dac.ParameterPrefix + col.ColumnName, DbType.Date, value);
                                }
                                else
                                {
                                    dac.AddInParameter(cmd, dac.ParameterPrefix + col.ColumnName, DbType.String, value);
                                }
                                vars.Add(col.ColumnName, dt.Rows[0][col.ColumnName].ToString());
                            }
                        }
                        sb.AppendFormat("insert into {0}({1}) values({2});", tableName, sbItem.ToString().TrimEnd(','), sbItemValue.ToString().TrimEnd(','));
                    }
                }
            }

            if (sb.Length > 0) //1.2 写入业务库
            {
                cmd.CommandText = "begin " + sb.ToString() + " end;";
                dac.ExecuteNonQuery(cmd);
            }
            else
            {
                LogUtil.Error("B003提交失败！未获取表单信息!");
                Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('提交失败！未获取表单信息!');", true);
                return false;
            }
            return true;
        }

        List<VarEntity> GetVarList(Hashtable table)
        {
            List<VarEntity> list = new List<VarEntity>();
            foreach (DictionaryEntry ety in table)
            {
                VarEntity p = new VarEntity();
                p.Name = Convert.ToString(ety.Key);
                p.Value = Convert.ToString(ety.Value);
                list.Add(p);
            }
            return list;
        }



        bool Submit(int actionType, string taskId, string processName, ref int incident, string stepLabel,
            string userName, string summary, Hashtable vars, string type, string tableName, List<string> detailTables, string formID,string documentNo)
        {
            string error = "";

            TaskEntity entity = new TaskEntity();
            entity.ASSIGNEDTOUSER = userName;
            entity.TASKID = taskId;
			entity.SUMMARY = documentNo;//summary+
            entity.VarList = GetVarList(vars);
            switch (actionType)
            {
                case 0: //同意
                    int outIncident = 0;
                    outIncident = _task.SubmitTask(entity);
                    error = entity.ERRORMESSAGE;
					if (incident <= 0)
					{ 
						string strSqlRevoke=string.Format("insert into PROC_REVOKE values ('{0}','{1}','1','{2}',getdate())",
							processName,outIncident,userName);
						DataAccess.Instance("BizDB").ExecuteNonQuery(strSqlRevoke);
					}
                    incident = outIncident;
                    break;
                case 1: //退回
                    //删除会签任务
                    string strSql = string.Format("delete from tasks  where taskid<>'{0}' and steplabel='{1}' and processname='{2}' and incident={3} ",
                        taskId, stepLabel, processName, incident);
                    DataAccess.Instance("UltDB").ExecuteNonQuery(strSql);
                    strSql = string.Format("update tasks set recipienttype=0  where    steplabel='{1}' and processname='{2}' and incident={3} ",
                        taskId, stepLabel, processName, incident);
                    DataAccess.Instance("UltDB").ExecuteNonQuery(strSql);
                    _task.ReturnTask(entity);
                    error = entity.ERRORMESSAGE;
                    break;
                case 2:
                    // _task.AbortIncident(entity);
                    _task.RejectTask(entity);
                    error = entity.ERRORMESSAGE;
                    DataAccess.Instance("BizDB").ExecuteNonQuery(string.Format("update {0} set  STATUS=3 where FORMID='{1}'", tableName,formID));
                    break;
            }

            if (!string.IsNullOrEmpty(error)) //2.1提交失败
            {
                DataAccess.Instance("BizDB").ExecuteNonQuery(string.Format("update {0} set INCIDENT={1},  STATUS=0 where FORMID='{2}'",
                    tableName, -1 , formID));
                Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('提交失败：" + error + "');", true);
                return false;
            }
            else //2.2提交成功
            {
                if (type == "NEWREQUEST" || type == "DRAFT")
                {
                    //2.2.1 更新实例号和流程编号
                    StringBuilder str = new StringBuilder();
                    str.AppendFormat("update {0} set INCIDENT={1},  STATUS=1,DOCUMENTNO='{3}' where FORMID='{2}'", tableName, incident, formID,documentNo);
                    str.AppendLine();
                    foreach (string ss in detailTables)
                    {
                        str.AppendFormat("update {0} set INCIDENT={1}, ProcessName='{3}' where FORMID='{2}'", ss, incident, formID, processName);
                        str.AppendLine();
                    }
                    DataAccess.Instance("BizDB").ExecuteNonQuery(str.ToString());
                    //2.2.2 删除草稿箱
                    DataAccess.Instance("BizDB").ExecuteNonQuery(string.Format("DELETE FROM WF_DRAFT WHERE FORMID='{0}'", formID));

                }
            }

            return true;
        }

        bool SaveApprovalHistroy(int actionType,string SpecialAction,string type, string processName, int incident, string comments,
            string userName, string stepLabel)
        {
            ApprovalHistoryEntity approval = new ApprovalHistoryEntity();
            IOrg _org = ServiceContainer.Instance().GetService<IOrg>();
            UserEntity user = _org.GetUserEntity(SessionLogic.GetLoginName());
            if (actionType == 0 && type == "NEWREQUEST" || type=="DRAFT")
            {
                approval.Action = "Request";// Resources.lang.NewRequest;
            }
            if (actionType == 0 && type == "MYTASK")
            {
                //if (SpecialAction != "")
                //    approval.Action = "Approve";
                //else
                //    approval.Action = "";
                approval.Action = SpecialAction;
            }
            if (actionType == 1)
            {
                approval.Action = "Reject"; //Resources.lang.Return;
            }
            if (actionType ==2)
            {
                approval.Action = "Cancel"; //Resources.lang.Return;
            }
            if (actionType == 3)
            {
                approval.Action = "协办";
            }
            approval.ApproveDate = DateTime.Now;
            approval.Approver = user.USERNAME;
            approval.Comments = comments;
            approval.Ext01 = userName;
            string OldUserName="";
            if (HttpContext.Current.Session["OldUserName"] != null)
            { 
             OldUserName=HttpContext.Current.Session["OldUserName"].ToString();
             if (OldUserName== userName)
                 OldUserName = "";
            }
            approval.Ext02 = OldUserName;
            approval.ProcessName = processName;
            approval.Incident = incident;
            approval.StepName = stepLabel == "" ? Resources.lang.NewRequest : stepLabel;
            ApprovalHistoryLogic ahl = new ApprovalHistoryLogic();
            ahl.AddApprovalHistory(null, approval);
            return true;
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            string strApplicantAccout;
            bool flag;
            Hashtable vars = new Hashtable();
            ApprovalHistory approvalHistory = Page.FindControl("ApprovalHistory1") as ApprovalHistory;

            try
            {
                UserInfo userInfo = Page.FindControl("UserInfo1") as UserInfo;
                int incident = ConvertUtil.ToInt32(userInfo.Incident);
                string type = userInfo.Type.ToUpper().Trim();
			
                //1.获取当前登录用户
                strApplicantAccout = GetLoginName(userInfo);
                if (string.IsNullOrEmpty(strApplicantAccout))
                {
                    return;
                }
                //2.开始执行之前的自定义事件
                if (BeforeSubmit != null)
                {
                    CancelEventArgs cea = new CancelEventArgs();
                    BeforeSubmit(vars, cea);
                    if (cea.Cancel)
                    {
                        return;
                    }
                }

                //0判断是否可以提交
                if (incident > 0 && StepSettingsLogic.GetFirstStep(userInfo.ProcessName) != userInfo.StepName)
                {
                    string strRevoke = string.Format("select status from PROC_REVOKE with(nolock) where processName='{0}' and incident='{1}'", userInfo.ProcessName, incident);
                    object FlagRevoke = DataAccess.Instance("BizDB").ExecuteScalar(strRevoke);
                    if (FlagRevoke != null)
                    {
                        if (FlagRevoke.ToString() == "1")
                        {
                            DataAccess.Instance("BizDB").ExecuteNonQuery("update PROC_REVOKE set status=2 where ProcessName='" + userInfo.ProcessName + "' and incident='" + incident + "'");
                        }
                        else
                        {
                            if (FlagRevoke.ToString() == "3")
                            {
                                Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('Task Already Revoke!');", true);
                            }
                        }
                    }
                }

                //3.保存业务库
                List<string> detailTables = new List<string>();
                string documentNo = "";
                bool IsCreate =false;
                if (type == "NEWREQUEST" || type == "DRAFT")
                    IsCreate = true;
                else
                    documentNo = (userInfo.FindControl("fld_DOCUMENTNO") as Label).Text;
                flag = SaveBusinessData(userInfo.GetFormData(), userInfo.TableName, userInfo.FormId, userInfo.ProcessPrefix, ref vars, ref detailTables, IsCreate, ref documentNo);
                if (!flag)
                {
                    return;
                }

                if (Request.QueryString["Xieban"] == "1") //协办
                {
                    flag = SaveApprovalHistroy(3, "xieban","", userInfo.ProcessName,ConvertUtil.ToInt32( Request.QueryString["Incident"]), approvalHistory.Comments
                    , strApplicantAccout, userInfo.StepName);


                    flag = Submit(approvalHistory.ActionType, Request.QueryString["xiebantaskid"], userInfo.ProcessName, ref incident
                    , userInfo.StepName, strApplicantAccout, "", vars, type, userInfo.TableName, detailTables, userInfo.FormId, documentNo);
                    if (flag)
                    {
                        Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "submitSuccess();", true);
                    }
                    return;
                }
                //4.提交
                if (type == "NEWREQUEST" || type == "DRAFT" || StepSettingsLogic.GetFirstStep(userInfo.ProcessName).Equals(userInfo.StepName))
                {
                    userInfo.GetFormVars(ref vars);
                }
                if (var_AskFor.Text.Trim() != "")
                {
                    vars.Add("StepName", userInfo.StepName);
                    vars.Add("AskFor", var_AskForAccount.Text);
                    // DataAccess.Instance("BizDB").ExecuteNonQuery(string.Format("update {0} set  PreStep where FORMID='{1}'", detailTables, userInfo.FormId));
                }
                else
                {
                    if (var_AskForAccount.Text == "")
                    {
                        vars.Add("StepName", "");
                        vars.Add("AskFor", "");
                    }
                }
                userInfo.Summary = userInfo.Summary + "◆" + documentNo;
                flag = Submit(approvalHistory.ActionType, userInfo.TaskId, userInfo.ProcessName, ref incident
                    , userInfo.StepName, strApplicantAccout, userInfo.Summary, vars, type, userInfo.TableName, detailTables, userInfo.FormId,documentNo);
                if (!flag)
                {
                    return;
                }

                //5.插入审批历史记录
                flag = SaveApprovalHistroy(approvalHistory.ActionType,approvalHistory.SpecalAction, type, userInfo.ProcessName, incident, approvalHistory.Comments
                    , strApplicantAccout, userInfo.StepName);
                if (!flag)
                {
                    return;
                }
                //6.执行成功之后的自定义事件
                if (AfterSubmit != null)
                {
                    CancelEventArgs ce = new CancelEventArgs();
                    AfterSubmit(sender, ce);
                    if (ce.Cancel)
                    {
                        return;
                    }
                }
            }
            catch (Exception ex) //意外终止
            {
                LogUtil.Error("提交失败!", ex);
                Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('提交失败!错误信息：" + ex.Message.Replace("'", "").Replace("\r", "").Replace("\n", "") + "');", true);
                return;
            }

            Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "submitSuccess();", true);
        }

        protected void btnSaveDraft_Click(object sender, EventArgs e)
        {
            string strApplicantAccout;
            bool flag;
            Hashtable vars = new Hashtable();
            ApprovalHistory approvalHistory = Page.FindControl("ApprovalHistory1") as ApprovalHistory;

            try
            {
                UserInfo userInfo = Page.FindControl("UserInfo1") as UserInfo;
                //1.获取当前登录用户
                strApplicantAccout = GetLoginName(userInfo);
                if (string.IsNullOrEmpty(strApplicantAccout))
                {
                    return;
                }
                //2.保存业务库
                List<string> detailTables = new List<string>();
                string documentNo="";
                flag = SaveBusinessData(userInfo.GetFormData(), userInfo.TableName, userInfo.FormId, userInfo.ProcessPrefix, ref vars, ref detailTables, false,ref documentNo);
                if (!flag)
                {
                    return;
                }
                //3.保存草稿表
                int incident = -1 ;
                StringBuilder sb = new StringBuilder();
                sb.Append(string.Format("update {0} set STATUS=0,incident={2} where FORMID='{1}'", userInfo.TableName, userInfo.FormId, incident));
                sb.AppendLine();
                sb.Append(string.Format("DELETE FROM WF_DRAFT WHERE FORMID='{0}'", userInfo.FormId));
                sb.AppendLine();
                SerialNoLogic sn=new SerialNoLogic();
                int id=sn.GetMaxNo("WF_DRAFT","ID");
                id=id+1;
                sb.Append(string.Format("insert into WF_DRAFT(ID,PROCESSNAME,FORMID,TABLENAME,SUMMARY,CREATEDATE,CREATEBY,INCIDENT,STEPNAME,TASKID) VALUES('"+id+"','{0}','{1}','{2}','{3}','{4}','{5}',{6},'{7}','{8}')", userInfo.ProcessName, userInfo.FormId, userInfo.TableName + "," + userInfo.TableNameDetail, userInfo.Summary, DateTime.Now, strApplicantAccout, incident, userInfo.StepName, userInfo.TaskId));
                sb.AppendLine();
                DataAccess.Instance("BizDB").ExecuteNonQuery(sb.ToString());
            }
            catch (Exception ex) //意外终止
            {
                LogUtil.Error(ex);
                Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('保存失败!错误信息：" + ex.Message.Replace("'", "").Replace("\r", "").Replace("\n", "") + "');", true);
                return;
            }

            Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "saveSuccess();", true);
        }
    }
}