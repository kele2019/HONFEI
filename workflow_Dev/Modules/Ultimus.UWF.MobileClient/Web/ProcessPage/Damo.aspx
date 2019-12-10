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

        MobileClient.MobileClientBackgroundRef.Ultimus_MobileServices srv = new MobileClient.MobileClientBackgroundRef.Ultimus_MobileServices();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                hidProcessName.Value = Request.QueryString["ProcessName"];
                hidIncident.Value = Request.QueryString["Incident"];
                hidStepName.Value = Request.QueryString["StepName"];
                hidTaskID.Value = Request.QueryString["TaskID"];

                //load data

                StringBuilder sb = new StringBuilder();
                sb.Append("select * from PROC_EXPENSE where PROCESSNAME='" + hidProcessName.Value + "' and INCIDENT='" + hidIncident.Value + "'");
                System.Data.DataTable dt = DataBaseLibrary.DbHelperSQL.Query(sb.ToString()).Tables[0];
                if (dt.Rows.Count > 0)
                {
                    foreach (System.Data.DataColumn col in dt.Columns)
                    {
                        (Page.FindControl("ctl" + col.ColumnName) as Label).Text = dt.Rows[0][col.ColumnName].ToString();
                    }
                }

                ctlAttachments.DataSource = DataBaseLibrary.DbHelperSQL.Query("select * from WF_ATTACHMENT where PROCESSNAME='" + hidProcessName.Value + "' and INCIDENT='" + hidIncident.Value + "'");
                ctlAttachments.DataBind();
            }
        }

        protected void Btn_Click(object sender, ImageClickEventArgs e)
        {
            ImageButton btn = (ImageButton)sender;

            int tt = 0;
            switch (btn.CommandName)
            {
                case "Approve":
                    tt = srv.Send(hidTaskID.Value, "", new List<MobileClient.MobileClientBackgroundRef.VariableInfo>().ToArray(), new System.Data.DataSet());
                    break;
                case "Return":
                    tt = srv.Return(hidTaskID.Value, "", new List<MobileClient.MobileClientBackgroundRef.VariableInfo>().ToArray(), new System.Data.DataSet());
                    break;
                case "Submit":
                    if (Session["UserInfo"] != null)
                    {
                        MobileClient.MobileClientBackgroundRef.UserInfo userInfo = (MobileClient.MobileClientBackgroundRef.UserInfo)Session["UserInfo"];
                        tt = srv.SendForm(hidProcessName.Value, hidStepName.Value, "", userInfo.UserAccount, new List<MobileClient.MobileClientBackgroundRef.VariableInfo>().ToArray(), new System.Data.DataSet());
                    }
                    break;
            }

            if (tt > 0)
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "a", "alert('提交成功');window.location.href='../ToDoTask.aspx';", true);
            }
            else
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "a", "alert('提交失败');", true);
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
                一般经费报销流程 [课长审批]</h4>
        </div>
        <div>
            <table class="table table-bordered">
                <tr id="ctlFORMID_row" runat="server">
                    <th>
                        FORMID
                    </th>
                    <td>
                        <asp:Label ID="ctlFORMID" runat="server"></asp:Label>
                    </td>
                </tr>
                <tr id="ctlPROCESSNAME_row" runat="server">
                    <th>
                        PROCESSNAME
                    </th>
                    <td>
                        <asp:Label ID="ctlPROCESSNAME" runat="server"></asp:Label>
                    </td>
                </tr>
                <tr id="ctlDOCUMENTNO_row" runat="server">
                    <th>
                        DOCUMENTNO
                    </th>
                    <td>
                        <asp:Label ID="ctlDOCUMENTNO" runat="server"></asp:Label>
                    </td>
                </tr>
                <tr id="ctlINCIDENT_row" runat="server">
                    <th>
                        INCIDENT
                    </th>
                    <td>
                        <asp:Label ID="ctlINCIDENT" runat="server"></asp:Label>
                    </td>
                </tr>
                <tr id="ctlBARCODE_row" runat="server">
                    <th>
                        BARCODE
                    </th>
                    <td>
                        <asp:Label ID="ctlBARCODE" runat="server"></asp:Label>
                    </td>
                </tr>
                <tr id="ctlBARCODESTATUS_row" runat="server">
                    <th>
                        BARCODESTATUS
                    </th>
                    <td>
                        <asp:Label ID="ctlBARCODESTATUS" runat="server"></asp:Label>
                    </td>
                </tr>
                <tr id="ctlSTATUS_row" runat="server">
                    <th>
                        STATUS
                    </th>
                    <td>
                        <asp:Label ID="ctlSTATUS" runat="server"></asp:Label>
                    </td>
                </tr>
                <tr id="ctlPROCESSSUMMARY_row" runat="server">
                    <th>
                        PROCESSSUMMARY
                    </th>
                    <td>
                        <asp:Label ID="ctlPROCESSSUMMARY" runat="server"></asp:Label>
                    </td>
                </tr>
                <tr id="ctlAPPLICANT_row" runat="server">
                    <th>
                        APPLICANT
                    </th>
                    <td>
                        <asp:Label ID="ctlAPPLICANT" runat="server"></asp:Label>
                    </td>
                </tr>
                <tr id="ctlREQUESTTIME_row" runat="server">
                    <th>
                        REQUESTTIME
                    </th>
                    <td>
                        <asp:Label ID="ctlREQUESTTIME" runat="server"></asp:Label>
                    </td>
                </tr>
                <tr id="ctlEMPOYEETYPE_row" runat="server">
                    <th>
                        EMPOYEETYPE
                    </th>
                    <td>
                        <asp:Label ID="ctlEMPOYEETYPE" runat="server"></asp:Label>
                    </td>
                </tr>
                <tr id="ctlDEPARTMENT_row" runat="server">
                    <th>
                        DEPARTMENT
                    </th>
                    <td>
                        <asp:Label ID="ctlDEPARTMENT" runat="server"></asp:Label>
                    </td>
                </tr>
                <tr id="ctlCOMPANY_row" runat="server">
                    <th>
                        COMPANY
                    </th>
                    <td>
                        <asp:Label ID="ctlCOMPANY" runat="server"></asp:Label>
                    </td>
                </tr>
                <tr id="ctlJOBDESCRIPTION_row" runat="server">
                    <th>
                        JOBDESCRIPTION
                    </th>
                    <td>
                        <asp:Label ID="ctlJOBDESCRIPTION" runat="server"></asp:Label>
                    </td>
                </tr>
                <tr id="ctlMOBILENO_row" runat="server">
                    <th>
                        MOBILENO
                    </th>
                    <td>
                        <asp:Label ID="ctlMOBILENO" runat="server"></asp:Label>
                    </td>
                </tr>
                <tr id="ctlEMAIL_row" runat="server">
                    <th>
                        EMAIL
                    </th>
                    <td>
                        <asp:Label ID="ctlEMAIL" runat="server"></asp:Label>
                    </td>
                </tr>
                <tr id="ctlCOSTCENTER_row" runat="server">
                    <th>
                        COSTCENTER
                    </th>
                    <td>
                        <asp:Label ID="ctlCOSTCENTER" runat="server"></asp:Label>
                    </td>
                </tr>
                <tr id="ctlLASTCOSTCENTER_row" runat="server">
                    <th>
                        LASTCOSTCENTER
                    </th>
                    <td>
                        <asp:Label ID="ctlLASTCOSTCENTER" runat="server"></asp:Label>
                    </td>
                </tr>
                <tr id="ctlBUDGETCODE_row" runat="server">
                    <th>
                        BUDGETCODE
                    </th>
                    <td>
                        <asp:Label ID="ctlBUDGETCODE" runat="server"></asp:Label>
                    </td>
                </tr>
                <tr id="ctlBUDGETNAME_row" runat="server">
                    <th>
                        BUDGETNAME
                    </th>
                    <td>
                        <asp:Label ID="ctlBUDGETNAME" runat="server"></asp:Label>
                    </td>
                </tr>
                <tr id="ctlACCOUNTCODE_row" runat="server">
                    <th>
                        ACCOUNTCODE
                    </th>
                    <td>
                        <asp:Label ID="ctlACCOUNTCODE" runat="server"></asp:Label>
                    </td>
                </tr>
                <tr id="ctlACCOUNTNAME_row" runat="server">
                    <th>
                        ACCOUNTNAME
                    </th>
                    <td>
                        <asp:Label ID="ctlACCOUNTNAME" runat="server"></asp:Label>
                    </td>
                </tr>
                <tr id="ctlEXPENSUBJECT_row" runat="server">
                    <th>
                        EXPENSUBJECT
                    </th>
                    <td>
                        <asp:Label ID="ctlEXPENSUBJECT" runat="server"></asp:Label>
                    </td>
                </tr>
                <tr id="ctlEXPENSETYPE_row" runat="server">
                    <th>
                        EXPENSETYPE
                    </th>
                    <td>
                        <asp:Label ID="ctlEXPENSETYPE" runat="server"></asp:Label>
                    </td>
                </tr>
                <tr id="ctlEXPENSENO_row" runat="server">
                    <th>
                        EXPENSENO
                    </th>
                    <td>
                        <asp:Label ID="ctlEXPENSENO" runat="server"></asp:Label>
                    </td>
                </tr>
                <tr id="ctlPAYMENT_row" runat="server">
                    <th>
                        PAYMENT
                    </th>
                    <td>
                        <asp:Label ID="ctlPAYMENT" runat="server"></asp:Label>
                    </td>
                </tr>
                <tr id="ctlCHEQUENO_row" runat="server">
                    <th>
                        CHEQUENO
                    </th>
                    <td>
                        <asp:Label ID="ctlCHEQUENO" runat="server"></asp:Label>
                    </td>
                </tr>
                <tr id="ctlPAYEE_row" runat="server">
                    <th>
                        PAYEE
                    </th>
                    <td>
                        <asp:Label ID="ctlPAYEE" runat="server"></asp:Label>
                    </td>
                </tr>
                <tr id="ctlADDRESS_row" runat="server">
                    <th>
                        ADDRESS
                    </th>
                    <td>
                        <asp:Label ID="ctlADDRESS" runat="server"></asp:Label>
                    </td>
                </tr>
                <tr id="ctlBANK_row" runat="server">
                    <th>
                        BANK
                    </th>
                    <td>
                        <asp:Label ID="ctlBANK" runat="server"></asp:Label>
                    </td>
                </tr>
                <tr id="ctlBANKACCOUNT_row" runat="server">
                    <th>
                        BANKACCOUNT
                    </th>
                    <td>
                        <asp:Label ID="ctlBANKACCOUNT" runat="server"></asp:Label>
                    </td>
                </tr>
                <tr id="ctlBORROWINGNO_row" runat="server">
                    <th>
                        BORROWINGNO
                    </th>
                    <td>
                        <asp:Label ID="ctlBORROWINGNO" runat="server"></asp:Label>
                    </td>
                </tr>
                <tr id="ctlCASHADVANCE_row" runat="server">
                    <th>
                        CASHADVANCE
                    </th>
                    <td>
                        <asp:Label ID="ctlCASHADVANCE" runat="server"></asp:Label>
                    </td>
                </tr>
                <tr id="ctlACTUALAMOUNT_row" runat="server">
                    <th>
                        ACTUALAMOUNT
                    </th>
                    <td>
                        <asp:Label ID="ctlACTUALAMOUNT" runat="server"></asp:Label>
                    </td>
                </tr>
                <tr id="ctlCAPITAL_row" style="display:none" runat="server">
                    <th>
                        CAPITAL
                    </th>
                    <td>
                        <asp:Label ID="ctlCAPITAL" runat="server"></asp:Label>
                    </td>
                </tr>
                <tr id="ctlBTONO_row" runat="server">
                    <th>
                        BTONO
                    </th>
                    <td>
                        <asp:Label ID="ctlBTONO" runat="server"></asp:Label>
                    </td>
                </tr>
                <tr id="ctlRULINGNO_row" runat="server">
                    <th>
                        RULINGNO
                    </th>
                    <td>
                        <asp:Label ID="ctlRULINGNO" runat="server"></asp:Label>
                    </td>
                </tr>
                <tr id="ctlISDETAIL_row" runat="server">
                    <th>
                        ISDETAIL
                    </th>
                    <td>
                        <asp:Label ID="ctlISDETAIL" runat="server"></asp:Label>
                    </td>
                </tr>
                <tr id="ctlEXT_01_row" runat="server">
                    <th>
                        EXT_01
                    </th>
                    <td>
                        <asp:Label ID="ctlEXT_01" runat="server"></asp:Label>
                    </td>
                </tr>
                <tr id="ctlEXT_02_row" runat="server">
                    <th>
                        EXT_02
                    </th>
                    <td>
                        <asp:Label ID="ctlEXT_02" runat="server"></asp:Label>
                    </td>
                </tr>
                <tr id="ctlEXT_03_row" runat="server">
                    <th>
                        EXT_03
                    </th>
                    <td>
                        <asp:Label ID="ctlEXT_03" runat="server"></asp:Label>
                    </td>
                </tr>
                <tr id="ctlEXT_04_row" runat="server">
                    <th>
                        EXT_04
                    </th>
                    <td>
                        <asp:Label ID="ctlEXT_04" runat="server"></asp:Label>
                    </td>
                </tr>
                <tr id="ctlEXT_05_row" runat="server">
                    <th>
                        EXT_05
                    </th>
                    <td>
                        <asp:Label ID="ctlEXT_05" runat="server"></asp:Label>
                    </td>
                </tr>
                <tr id="ctlEXT_06_row" runat="server">
                    <th>
                        EXT_06
                    </th>
                    <td>
                        <asp:Label ID="ctlEXT_06" runat="server"></asp:Label>
                    </td>
                </tr>
                <tr id="ctlEXT_07_row" runat="server">
                    <th>
                        EXT_07
                    </th>
                    <td>
                        <asp:Label ID="ctlEXT_07" runat="server"></asp:Label>
                    </td>
                </tr>
                <tr id="ctlEXT_08_row" runat="server">
                    <th>
                        EXT_08
                    </th>
                    <td>
                        <asp:Label ID="ctlEXT_08" runat="server"></asp:Label>
                    </td>
                </tr>
                <tr id="ctlEXT_09_row" runat="server">
                    <th>
                        EXT_09
                    </th>
                    <td>
                        <asp:Label ID="ctlEXT_09" runat="server"></asp:Label>
                    </td>
                </tr>
                <tr id="ctlEXT_10_row" runat="server">
                    <th>
                        EXT_10
                    </th>
                    <td>
                        <asp:Label ID="ctlEXT_10" runat="server"></asp:Label>
                    </td>
                </tr>
                <tr id="ctlEXT_11_row" runat="server">
                    <th>
                        EXT_11
                    </th>
                    <td>
                        <asp:Label ID="ctlEXT_11" runat="server"></asp:Label>
                    </td>
                </tr>
                <tr id="ctlEXT_12_row" runat="server">
                    <th>
                        EXT_12
                    </th>
                    <td>
                        <asp:Label ID="ctlEXT_12" runat="server"></asp:Label>
                    </td>
                </tr>
                <tr id="ctlEXT_13_row" runat="server">
                    <th>
                        EXT_13
                    </th>
                    <td>
                        <asp:Label ID="ctlEXT_13" runat="server"></asp:Label>
                    </td>
                </tr>
                <tr id="ctlEXT_14_row" runat="server">
                    <th>
                        EXT_14
                    </th>
                    <td>
                        <asp:Label ID="ctlEXT_14" runat="server"></asp:Label>
                    </td>
                </tr>
                <tr id="ctlEXT_15_row" runat="server">
                    <th>
                        EXT_15
                    </th>
                    <td>
                        <asp:Label ID="ctlEXT_15" runat="server"></asp:Label>
                    </td>
                </tr>
                <tr id="ctlEXT_16_row" runat="server">
                    <th>
                        EXT_16
                    </th>
                    <td>
                        <asp:Label ID="ctlEXT_16" runat="server"></asp:Label>
                    </td>
                </tr>
                <tr id="ctlEXT_17_row" runat="server">
                    <th>
                        EXT_17
                    </th>
                    <td>
                        <asp:Label ID="ctlEXT_17" runat="server"></asp:Label>
                    </td>
                </tr>
                <tr id="ctlEXT_18_row" runat="server">
                    <th>
                        EXT_18
                    </th>
                    <td>
                        <asp:Label ID="ctlEXT_18" runat="server"></asp:Label>
                    </td>
                </tr>
                <tr id="ctlEXT_19_row" runat="server">
                    <th>
                        EXT_19
                    </th>
                    <td>
                        <asp:Label ID="ctlEXT_19" runat="server"></asp:Label>
                    </td>
                </tr>
                <tr id="ctlEXT_20_row" runat="server">
                    <th>
                        EXT_20
                    </th>
                    <td>
                        <asp:Label ID="ctlEXT_20" runat="server"></asp:Label>
                    </td>
                </tr>
                <tr id="ctlid_row" runat="server">
                    <th>
                        附件
                    </th>
                    <td>
                        <asp:GridView ID="ctlAttachments" CssClass="table table-bordered" runat="server"
                            AutoGenerateColumns="false" ShowHeader="false">
                            <Columns>
                                <asp:TemplateField>
                                    <ItemTemplate>
                                        <a href='<%# System.Configuration.ConfigurationManager.AppSettings["AttachmentPath"] + hidProcessName.Value + "/" + Eval("NEWNAME") + Eval("FILETYPE")%>'
                                            target="_blank">
                                            <%# Eval("FILENAME")%></a>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                    </td>
                </tr>
                <tr>
                    <td colspan="2">
                        <asp:GridView ID="gridDetails" CssClass="table table-bordered" runat="server">
                        </asp:GridView>
                    </td>
                </tr>
                <tr id="trBtn" runat="server">
                    <td colspan="2">
                        <asp:ImageButton ID="ctlDepart" runat="server" CommandName="Approve" OnClientClick="return confirm('您确定要提交吗？');"
                            ToolTip="Approve" OnClick="Btn_Click" ImageUrl="../images/approve.png" />
                        <asp:ImageButton ID="ctlCenter" runat="server" CommandName="Return" OnClientClick="return confirm('您确定要提交吗？');"
                            ToolTip="Return" OnClick="Btn_Click" ImageUrl="../images/return.png" />
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
