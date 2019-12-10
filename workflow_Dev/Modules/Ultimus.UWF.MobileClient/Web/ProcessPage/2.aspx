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
				System.Data.DataSet ctlDs = srv.GetPageControlSource(hidProcessName.Value, hidStepName.Value);
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

                System.Data.DataSet ds = srv.GetPageDestSource(hidProcessName.Value, incident,hidStepName.Value);
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
                        if (ctl is Label)
                        {
							Label lbl=(ctl as Label);
							if(lbl.ToolTip=="Date")
							{
								try
								{
									double d=double.Parse(Convert.ToString(mainDt.Rows[0][col]));
									lbl.Text=DateTime.FromOADate(d).ToShortDateString();
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
						else if (ctl is TextBox)
                        {
                            (ctl as TextBox).Text = Convert.ToString(mainDt.Rows[0][col]);
                        }
						else if (ctl is DropDownList)
                        {
                            (ctl as DropDownList).SelectedValue = Convert.ToString(mainDt.Rows[0][col]);
                        }
						else if (ctl is CheckBox)
                        {
                            (ctl as CheckBox).Checked = Convert.ToString(mainDt.Rows[0][col]) == "1" ? true : false;
                        }
						else if (ctl is RadioButton)
                        {
                            (ctl as RadioButton).Checked = Convert.ToString(mainDt.Rows[0][col]) == "1" ? true : false;
                        }

						//ishow

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
				//Attachments
                if(Page.FindControl("ctlAttachments")!=null)
                {
                    GridView grid = Page.FindControl("ctlAttachments") as GridView;
                    //grid.DataSource = srv.GetProcessAttachments(hidProcessName.Value, hidIncident.Value);
                    //grid.DataBind();
                }
            }
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
                    if (ctl is Label)
                    {
                        mainDt.Rows[0][col]=(ctl as Label).Text;
                    }

                    if (ctl is TextBox)
                    {
                        mainDt.Rows[0][col]=(ctl as TextBox).Text ;
                    }

                    if (ctl is DropDownList)
                    {
                        mainDt.Rows[0][col] = (ctl as DropDownList).SelectedValue ;
                    }

                    if (ctl is CheckBox)
                    {
                        mainDt.Rows[0][col] =(ctl as CheckBox).Checked ? 1 : 0;
                    }

                    if (ctl is RadioButton)
                    {
                        mainDt.Rows[0][col]=(ctl as RadioButton).Checked  ? 1 : 0;
                    }

					if (ctl is ImageButton)
                    {
                        if (btn==ctl)
                        {
                            mainDt.Rows[0][col] = "1";
                        }
                    }
                }
            }

			//审批历史记录
			System.Data.DataTable dt = new System.Data.DataTable("ApprovalRemark");
            Control remark = Page.FindControl("ctlComments" );
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
                    wf.ReturnTask(Convert.ToString(Session["Account"]),hidTaskID.Value,"","",null,ds);
                    break;
            }

			if(tt>0)
			{
				Page.ClientScript.RegisterStartupScript(this.GetType(), "a", "alert('提交成功');window.location.href='../ToDoTask.aspx';",true);
			}
			else
			{
				Page.ClientScript.RegisterStartupScript(this.GetType(), "a", "alert('提交失败');",true);
			}
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div class="cont">
        <uc1:Header ID="Header1" runat="server" />
        <div class="well well-small" style="color: #f56b00;padding-bottom:0;margin-bottom:0;">
            <h4>
                BusinessTrip [部门经理审批]</h4>
        </div>
        <div>
            <table class="table table-bordered">
						<tr id="ctlDestination_row" runat="server">
							<th>Destination</th>					
							<td><asp:LABEL ID="ctlDestination" runat="server"></asp:LABEL></td>					
						</tr>
						<tr id="ctlComments_row" runat="server">
							<th>Comments</th>					
							<td><asp:TEXTBOX ID="ctlComments" runat="server"></asp:TEXTBOX></td>					
						</tr>



			<tr id="trBtn" runat="server">
				<td colspan="2" >
							<asp:ImageButton ID="ctlApprove" runat="server" CommandName="Approve"  OnClientClick="return confirm('您确定要提交吗？');" ToolTip="Approve"  OnClick="Btn_Click" ImageUrl="../images/approve.png"/>
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
