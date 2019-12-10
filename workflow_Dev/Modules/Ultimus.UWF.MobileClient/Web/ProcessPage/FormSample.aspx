<%@ Page Language="C#" AutoEventWireup="true" %>

<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>
<%@ Register Src="FormHeader.ascx" TagName="Header" TagPrefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Ultimus Mobile Client</title>
    <meta content="width=device-width, initial-scale=1.0" name="viewport" />
    <link href="../Css/CSS.css" rel="stylesheet" type="text/css" />
    <link href="../Css/bootstrap.min.css" rel="stylesheet" type="text/css" />
    <style type="text/css">
        INPUT
        {
            width: auto;
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

        MobileClient.MobileClientBackgroundRef.Ultimus_MobileServices srv = new MobileClient.MobileClientBackgroundRef.Ultimus_MobileServices();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                hidProcessName.Value = Request.QueryString["ProcessName"];
                hidIncident.Value = Request.QueryString["Incident"];
                hidStepName.Value = Request.QueryString["StepName"];
                hidTaskID.Value = Request.QueryString["TaskID"];
                int incident = 0;
                if (!string.IsNullOrEmpty(hidIncident.Value))
                {
                    incident = Convert.ToInt32(hidIncident.Value);
                }
                System.Data.DataSet ds = srv.GetPageSource(hidProcessName.Value, incident,hidStepName.Value);
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
                            (ctl as Label).Text = Convert.ToString(mainDt.Rows[0][col]);
                        }

                        if (ctl is TextBox)
                        {
                            (ctl as TextBox).Text = Convert.ToString(mainDt.Rows[0][col]);
                        }

                        if (ctl is DropDownList)
                        {
                            (ctl as DropDownList).SelectedValue = Convert.ToString(mainDt.Rows[0][col]);
                        }

                        if (ctl is CheckBox)
                        {
                            (ctl as CheckBox).Checked = Convert.ToString(mainDt.Rows[0][col]) == "1" ? true : false;
                        }

                        if (ctl is RadioButton)
                        {
                            (ctl as RadioButton).Checked = Convert.ToString(mainDt.Rows[0][col]) == "1" ? true : false;
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
                        mainDt.Rows[0][col] = (ctl as CheckBox).Checked ? 1 : 0;
                    }

                    if (ctl is RadioButton)
                    {
                        mainDt.Rows[0][col]=(ctl as RadioButton).Checked  ? 1 : 0;
                    }
                }
            }
            switch (btn.CommandName)
            {
                case "approve":
                    srv.Send(hidTaskID.Value,"",null,ds);
                    break;
                case "return":
                    srv.Send(hidTaskID.Value,"",null,ds);
                    break;
            }
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div class="cont">
        <uc1:Header ID="Header1" runat="server" />
        <div class="well well-small" style="color: #f56b00; padding-bottom: 0; margin-bottom: 0;">
            <h4>
                Travel Application [Application]</h4>
        </div>
        <div>
            <table class="table table-bordered">
                <tr>
                    <th>
                        申请人姓名
                    </th>
                    <td>
                        <asp:TextBox ID="ctl申请人姓名" runat="server"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <th>
                        申请时间
                    </th>
                    <td>
                        <asp:TextBox ID="ctl申请时间" runat="server"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <th>
                        申请部门
                    </th>
                    <td>
                        <asp:TextBox ID="ctl申请部门" runat="server"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td colspan="2">
                        <asp:ImageButton ID="ImageButton1" runat="server" OnClick="Btn_Click" CommandName="approve" />
                    </td>
                </tr>
            </table>
        </div>
    </div>
    <div>
        <asp:HiddenField ID="hidProcessName" runat="server" />
        <asp:HiddenField ID="hidStepName" runat="server" />
        <asp:HiddenField ID="hidIncident" runat="server" />
        <asp:HiddenField ID="hidTaskID" runat="server" />
    </div>
    </form>
</body>
</html>
