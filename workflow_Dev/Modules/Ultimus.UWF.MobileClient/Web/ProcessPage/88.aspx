<%@ Page Language="C#" AutoEventWireup="true" %>
<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>
<%@ Register Src="FormHeader.ascx" TagName="Header" TagPrefix="uc1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Ultimus Mobile Client</title>
    <meta content="width=device-width, initial-scale=1.0" name="viewport" />
    <link href="../Css/CSS.css" rel="stylesheet" type="text/css" />
    <link href="../Css/bootstrap.min.css" rel="stylesheet" type="text/css" />
	<style type="text/css">
    INPUT
    {
        width:auto;
    }
    </style>
    <script type="text/C#" runat="server">
        System.Data.DataSet InnerDataSet
        {
            get
            {
                if (ViewState["InnerDataSet"] != null)
                {
                    return ViewState["InnerDataSet"] as System.Data.DataSet;
                }
                return null;
            }
            set
            {
                ViewState["InnerDataSet"] = value;
            }
        }

		protected override void InitializeCulture()
        {
            string Language = Request.ServerVariables["HTTP_ACCEPT_LANGUAGE"].ToString().Split(',')[0];
            if (Session["Language"] != null)
            {
                Language = Session["Language"].ToString();
            }
            System.Threading.Thread.CurrentThread.CurrentCulture = System.Globalization.CultureInfo.CreateSpecificCulture(Language);
            System.Threading.Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo(Language);
            base.InitializeCulture();
        }

        DALLibrary.PageSource srv = new DALLibrary.PageSource();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                hidProcessName.Value = Request.QueryString["ProcessName"];
                hidIncident.Value = Request.QueryString["Incident"];
                hidStepName.Value = Request.QueryString["StepName"];
                hidTaskID.Value = Request.QueryString["TaskID"];
				string yb=Request.QueryString["YB"];
				if(yb=="1")
				{
					trBtn.Visible=false;
					Control ctl = Page.FindControl("trApprovalRemark");
                    if (ctl != null)
                    {
                        ctl.Visible = false;
                    }
				}
				
                int incident = 0;
                if (!string.IsNullOrEmpty(hidIncident.Value))
                {
                    incident = Convert.ToInt32(hidIncident.Value);
                }
				System.Data.DataSet ctlDs = srv.GetPageControlSource(hidProcessName.Value, hidStepName.Value, hidTaskID.Value, hidIncident.Value);
                if (ctlDs != null)
                {
                    foreach (System.Data.DataTable dt in ctlDs.Tables)
                    {
                        if (dt.Rows.Count > 0)
                        {
                            Control ctl = Page.FindControl("ctl" + dt.TableName);
                            if (ctl is DropDownList)
                            {
                                DropDownList ddl = ctl as DropDownList;
                                if (dt.Columns.Count > 0)
                                {
                                    ddl.DataTextField = dt.Columns[0].ColumnName;
                                    ddl.DataSource = dt;
                                    ddl.DataBind();
                                }
                            }
                        }
                    }
                }

                System.Data.DataSet ds = srv.GetPageDestSource(hidProcessName.Value, incident,hidStepName.Value, hidTaskID.Value );
                if (ds == null)
                {
                    return;
                }
                this.InnerDataSet = ds;
                System.Data.DataTable mainDt = ds.Tables[0];//主表
                if (mainDt.Rows.Count > 0)
                {
                    foreach (System.Data.DataColumn col in mainDt.Columns)
                    {
                        Control ctl = Page.FindControl("ctl" + col.ColumnName);
						if( ctl != null)
						{
							//if (ctl is Label)
							if(ctl.GetType().Name == "Label")
							{
								Label lbl = (ctl as Label);
								if (lbl.ToolTip == "Date")
								{
									try
									{
										double d = double.Parse(Convert.ToString(mainDt.Rows[0][col]));
										lbl.Text = DateTime.FromOADate(d).ToShortDateString();
									}
									catch
									{
										(ctl as Label).Text = Convert.ToString(mainDt.Rows[0][col]);
									}
								}
								else
								{
									(ctl as Label).Text = Convert.ToString(mainDt.Rows[0][col]);
								}
							}
							else if(ctl.GetType().Name == "DATE")
							{
								(ctl as TextBox).Text = Convert.ToString(mainDt.Rows[0][col]);
							}
							//else if (ctl is TextBox)
							else if(ctl.GetType().Name == "TextBox")
							{
								(ctl as TextBox).Text = Convert.ToString(mainDt.Rows[0][col]);
							}
							//else if (ctl is DropDownList)
							else if(ctl.GetType().Name == "DropDownList")
							{
								(ctl as DropDownList).SelectedValue = Convert.ToString(mainDt.Rows[0][col]);
							}
							//else if (ctl is CheckBox)
							else if(ctl.GetType().Name == "CheckBox")
							{
								(ctl as CheckBox).Checked = Convert.ToString(mainDt.Rows[0][col]) == "1" || Convert.ToString(mainDt.Rows[0][col]).ToUpper() == "TRUE" ? true : false;
							}
							//else if (ctl is RadioButton)
							else if(ctl.GetType().Name == "RadioButton")
							{
								(ctl as RadioButton).Checked = Convert.ToString(mainDt.Rows[0][col]) == "1" || Convert.ToString(mainDt.Rows[0][col]).ToUpper() == "TRUE" ? true : false;
							}
						}

						if (col.ColumnName == "AssistFormUrl")
                        {
                            //表单链接
                            Control ctl_link = Page.FindControl("FormLink");
                            if (ctl_link != null)
                            {
                                (ctl_link as HyperLink).NavigateUrl = Convert.ToString(mainDt.Rows[0][col]);
                            }
                        }

						//附件
                        Control att = Page.FindControl("att_" + col.ColumnName);
                        if (att is GridView) 
                        {
                            System.Data.DataTable childDt = srv.GetAttchmentInfo(Convert.ToString(mainDt.Rows[0][col]), col.ColumnName);
                            (att as GridView).DataSource = childDt;
                            (att as GridView).DataBind();
                        }
                    }
                }

                for (int i = 1; i < ds.Tables.Count; i++)
                {
                    //子表
                    System.Data.DataTable childDt = ds.Tables[i];
                    Control ctl = Page.FindControl("ctl" + childDt.TableName);
                    if (ctl is GridView)
                    {
                        (ctl as GridView).DataSource = childDt;
                        (ctl as GridView).DataBind();
                    }
                }

				
            }
        }

		protected string GetAttchmentName(string attName)
        {
            string fkId = Request.QueryString["StepId"];
            string sql = "SELECT COLUMNNAME FROM [MOBILECLIENT_STEPCONTROL] where FORMAT='" + attName + "' and  FK_ID='" + fkId + "'";
            return MyLib.DataAccess.Instance("BizDB").ExecuteScalar(sql).ToString();
        }

        protected void Btn_Click(object sender, ImageClickEventArgs e)
        {
            ImageButton btn = (ImageButton)sender;
            System.Data.DataSet ds = this.InnerDataSet;
            System.Data.DataTable mainDt = ds.Tables[0];//主表
            if (mainDt.Rows.Count > 0)
            {
                foreach (System.Data.DataColumn col in mainDt.Columns)
                {
                    Control ctl = Page.FindControl("ctl" + col.ColumnName);
					if(ctl != null)
					{
						if (ctl is Label)
						{
							//mainDt.Rows[0][col.ColumnName]=(ctl as Label).Text;
						}
						//ctl is TextBox
						if (ctl.GetType().Name == "TextBox")
						{
							mainDt.Rows[0][col.ColumnName]=(ctl as TextBox).Text ;
						}
						//ctl is DropDownList
						if (ctl.GetType().Name == "DropDownList")
						{
							mainDt.Rows[0][col.ColumnName] = (ctl as DropDownList).SelectedValue ;
						}
						//ctl is CheckBox
						if (ctl.GetType().Name == "CheckBox")
						{
							mainDt.Rows[0][col.ColumnName] =(ctl as CheckBox).Checked ? "true" : "false";
						}
						//ctl is RadioButton 
						if (ctl.GetType().Name == "RadioButton")
						{
							mainDt.Rows[0][col.ColumnName]=(ctl as RadioButton).Checked  ? "true" : "false";
						}
						//ctl is ImageButton
						if (ctl.GetType().Name == "ImageButton")
						{
							if (btn==ctl)
							{
								mainDt.Rows[0][col.ColumnName] = "1";
							}
						}
					}
                }
            }

			//审批历史记录
			System.Data.DataTable dt = new System.Data.DataTable("ApprovalRemark");
            Control remark = Page.FindControl("ctl审批意见" );
            if (remark != null)
            {
                System.Data.DataRow dr = dt.NewRow();
                dt.Columns.Add("ApprovalRemark");
                dr[0] = ((TextBox)remark).Text;
                dt.Rows.Add(dr);
            }
            ds.Tables.Add(dt);

			int tt=0;
            ClientService.WorkflowRef wf=new ClientService.WorkflowRef();
            switch (btn.CommandName)
            {
                case "Approve":
                    tt=wf.SubmitTask(Convert.ToString(Session["Account"]),hidTaskID.Value,"",null,ds);
                    break;
                case "Return":
                    tt=wf.ReturnTask(Convert.ToString(Session["Account"]),hidTaskID.Value,"","",null,ds);
                    break;
				case "Submit":
                    tt=wf.SubmitTask(Convert.ToString(Session["Account"]),hidTaskID.Value,"",null,ds);
                    break;
            }

			//如果为-999,则为 还有协办任务未完成，不允许提交
            if (tt == -999) {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "a", "alert('提交失败，协办任务未完成');window.location.href='../ToDoTask.aspx';", true);
            }
            else if (tt > 0)
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "a", "alert('提交成功');window.location.href='../ToDoTask.aspx';", true);
            }
            else
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "a", "alert('提交失败');window.location.href='../ToDoTask.aspx';", true);
            }
        }
    </script>
	<script type="text/javascript" src="jquery.js"></script>
    <script type="text/javascript" src="SelectUser.js?version=20150728"></script>
	<script type="text/javascript" src="My97DatePicker/WdatePicker.js"></script>
	<script type="text/javascript" language="javascript">
		function CheckValid() {
	        var validResult = true;
	        $("input[IsFill=1]").each(function () {
	            if ($(this).val() == null || $(this).val() == '') {
	                if (validResult == true) { alert($(this).attr("FillName") + "不能为空"); }
	                validResult = false;
	            }
	        });
			$("textarea[IsFill=1]").each(function () {
	            if ($(this).val() == null || $(this).val() == '') {
	                if (validResult == true) { alert($(this).attr("FillName") + "不能为空"); }
	                validResult = false;
	            }
	        });
	        $("select[IsFill=1]").each(function () {
	            if ($(this).find("option:selected").val() == 'undefined' || $(this).find("option:selected").val() == null || $(this).val() == '') {
	                if (validResult == true) { alert($(this).attr("FillName") + "不能为空"); }
	                validResult = false;
	            }
	        });
		    if (validResult == true) {
		        if (confirm('您确定要提交吗？') == true) {
		            return true;
		        } else {
		            return false;
		        }
		    } else {
		        return false;
            }
		}

		//抄送或协办
		function CarbonOrBackUp(procName) {
		    if (procName == "抄送流程") {
		        if ($("#CarbonPersonName").val() == null || $("#CarbonPersonName").val() == "") {
		            alert("抄送人不能为空");
		            return false;
		        }
		        if ($("#CarbonRemark").val() == null || $("#CarbonRemark").val() == "") {
		            alert("备注不能为空");
		            return false;
		        }
		        $.ajax({
		            type: "POST",
		            url: "Ajax.ashx?tag=CarbonOrBackUp&procName=" + encodeURI(procName) + "&taskId=" + $("#hidTaskID").val() + "&txtUserId=" + $("#CarbonPersonAccount").val() + "&txtUsers=" + encodeURI($("#CarbonPersonName").val()) + "&txtRemark=" + encodeURI($("#CarbonRemark").val()),
		            async: true,
		            success: function (date) {
		                var json = eval('(' + date + ')');
		                alert(json.result);
		            }
		        });
		    } else if (procName == "协办流程") {
		        if ($("#BackUpPersonName").val() == null || $("#BackUpPersonName").val() == "") {
		            alert("协办人不能为空");
		            return false;
		        }
		        if ($("#BackUpRemark").val() == null || $("#BackUpRemark").val() == "") {
		            alert("备注不能为空");
		            return false;
		        }
		        $.ajax({
		            type: "POST",
		            url: "Ajax.ashx?tag=CarbonOrBackUp&procName=" + encodeURI(procName) + "&taskId=" + $("#hidTaskID").val() + "&txtUserId=" + $("#BackUpPersonAccount").val() + "&txtUsers=" + encodeURI($("#BackUpPersonName").val()) + "&txtRemark=" + encodeURI($("#BackUpRemark").val()),
		            async: true,
		            success: function (date) {
		                var json = eval('(' + date + ')');
		                alert(json.result);
		            }
		        });
            }
        }

		//判断表单是否配置了移动
        function CheckFormLink(obj) {
            var href = obj.getAttribute("href");
            var taskId = href.substring(href.indexOf('TaskId=') + 7);
            var res = true;
            var processName = '';
            var stepLabel = '';
            var incident = '';
            var rootPath = '';
		    $.ajax({
		        type: "POST",
		        url: "Ajax.ashx?tag=CheckFormLink&taskId=" + taskId,
		        async: false,
		        success: function (date) {
		            var json = eval('(' + date + ')');
		            if (json.result == "failure") {
		                alert("流程未配置移动端功能，请在PC端进行查阅");
		                res = false;
		            }
                    processName = json.processName;
                    stepLabel = json.stepLabel;
                    incident = json.incident;
                    rootPath = json.rootPath;
		        }
		    });
		    if (res == true) {
		        obj.setAttribute("href", encodeURI(rootPath + "Web/OpenForm.aspx?ProcessName=" + processName + "&StepName=" + stepLabel + "&Incident=" + incident + "&TaskId=" + taskId + "&YB=1")) 
            }
		    return res;
        }
	</script>
</head>
<body>
    <form id="form1" runat="server">
    <div class="cont">
        <uc1:Header ID="Header1" runat="server" />
        <div class="well well-small" style="color: #f56b00;padding-bottom:0;margin-bottom:0;">
            <h4>
                软控费用报销单 [财务稽核]</h4>
        </div>
        <div>
            <table class="table table-bordered">
			
										<tr id="ctlAPPLICANT_row" runat="server">
											<th style="width:30%">申请人</th>					
											<td style="width:70%"><asp:LABEL ID="ctlAPPLICANT" Width="90%" runat="server"></asp:LABEL></td>					
										</tr>
										<tr id="ctlREQUESTDATE_row" runat="server">
											<th style="width:30%">申请日期</th>					
											<td style="width:70%"><asp:LABEL ID="ctlREQUESTDATE" Width="90%" runat="server"></asp:LABEL></td>					
										</tr>
										<tr id="ctlDEPARTMENT_row" runat="server">
											<th style="width:30%">申请部门</th>					
											<td style="width:70%"><asp:LABEL ID="ctlDEPARTMENT" Width="90%" runat="server"></asp:LABEL></td>					
										</tr>
										<tr id="ctlCOMPANY_row" runat="server">
											<th style="width:30%">法人公司</th>					
											<td style="width:70%"><asp:LABEL ID="ctlCOMPANY" Width="90%" runat="server"></asp:LABEL></td>					
										</tr>
										<tr id="ctlDOCUMENTNO_row" runat="server">
											<th style="width:30%">单据编号</th>					
											<td style="width:70%"><asp:LABEL ID="ctlDOCUMENTNO" Width="90%" runat="server"></asp:LABEL></td>					
										</tr>
										<tr id="ctlCurrency_row" runat="server">
											<th style="width:30%">币种</th>					
											<td style="width:70%"><asp:LABEL ID="ctlCurrency" Width="90%" runat="server"></asp:LABEL></td>					
										</tr>
										<tr id="ctlRate_row" runat="server">
											<th style="width:30%">汇率</th>					
											<td style="width:70%"><asp:LABEL ID="ctlRate" Width="90%" runat="server"></asp:LABEL></td>					
										</tr>
                                        <tr>
						<td colspan="2">
							 <asp:GridView ID="ctlPROC_Exp_ITEM" CssClass="table table-bordered" runat="server">
                </asp:GridView>
						</td>
					</tr>

               
										<tr id="ctl审批意见_row" runat="server">
											<th style="width:30%">审批意见</th>					
											<td style="width:70%"><asp:TEXTBOX ID="ctl审批意见" Width="90%" runat="server"></asp:TEXTBOX></td>					
										</tr>

			<tr id="trBtn" runat="server">
				<td colspan="2" >
							<asp:ImageButton ID="ctl同意" runat="server" CommandName="Approve"  OnClientClick="return CheckValid();" ToolTip="Approve"  OnClick="Btn_Click" ImageUrl="../images/approve.png"/>
							<asp:ImageButton ID="ctl退回" runat="server" CommandName="Return"  OnClientClick="return confirm('您确定要提交吗？');"  ToolTip="Return" OnClick="Btn_Click" ImageUrl="../images/return.jpg"/>
				</td>
			</tr>
            </table>
        </div>
    </div>
	<div>
        <asp:HiddenField ID="hidProcessName" runat="server" />
        <asp:HiddenField ID="hidIncident" runat="server" />
        <asp:HiddenField ID="hidStepName" runat="server" />
        <asp:HiddenField ID="hidTaskID" runat="server" />
    </div>
    </form>
</body>
</html>
