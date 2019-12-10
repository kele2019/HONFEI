<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="NewRequest.aspx.cs" Inherits="Presale.Process.ProcessPerformance.NewRequest" %>
<%@ Register Src="/Modules/Ultimus.UWF.Form.ProcessControl/UserInfo.ascx" TagName="UserInfo" TagPrefix="ui" %>
<%@ Register Src="/Modules/Ultimus.UWF.Form.ProcessControl/ApprovalHistory.ascx" TagName="ApprovalHistory" TagPrefix="ah" %>
<%@ Register Src="/Modules/Ultimus.UWF.Form.ProcessControl/Attachments.ascx" TagName="Attachments" TagPrefix="attach" %>
<%@ Register Src="/Modules/Ultimus.UWF.Form.ProcessControl/ButtonList.ascx" TagName="ButtonList" TagPrefix="btn" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta http-equiv="X-UA-Compatible"  content="IE=EmulateIE8" />
    <title>KPI Submit Process</title>
    <script type="text/javascript" src="/Assets/js/common.js"></script>
    <script type="text/javascript">
        function beforeSubmit() {
            $("#fld_Month").val($("#dropMonth").val());
            var summary = "KPI Submit Process";
            $("#UserInfo1_fld_PROCESSSUMMARY").val(summary);
            return true;
        }
        function beforeSave() {
            var summary = "KPI Submit Process";
            $("#UserInfo1_fld_PROCESSSUMMARY").val(summary);
            $("#fld_TRSummary").val(summary);
            return true;
        }
        $(document).ready(function () {
            $("#dropMonth").val($("#fld_Month").val());
            var now = new Date();
            var year = now.getFullYear();
            $("#fld_Year").val(year.toString());
            if ($("#hdIncident").val() != "") {
                $("#ButtonList1_btnSubmit").val("Submit");
                $("#ButtonList1_btnBack").hide();
                $("#ButtonList1_btnReject").show();
            }
            if ($("#hdUrgeTask").val() == "Yes") {
                $("#ReturnBackTask").show();
            }
            if ($("#hdUrgeTask").val() == "No") {
                $("#divClose").show();
            }
        });
        function dropMonth_onchange(obj) {
            $(obj).next().val($(obj).find("option:selected").text());
        }
        function loadReprots() {
            var incident = request('Incident');
            location.href = "../Presale.Process.ProcessPerformance/Approval.aspx?ProcessName=KPI%20Submit%20Process&StepName=Approve&Incident=" + incident + "&Type=myapprove";
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
     <div id="myDiv" class="container">
            <div class="row">
                <ui:userinfo id="UserInfo1" processtitle="KPI Submit Process" processprefix="KPI" tablename="PROC_ProcessPerformance"
                    runat="server"  ></ui:userinfo>
                    <asp:TextBox runat="server" ID="fld_DGMLogin" style="display:none"></asp:TextBox>
                    <asp:TextBox runat="server" ID="fld_deptITLogin" style="display:none"></asp:TextBox>
                    <asp:TextBox runat="server" ID="fld_deptHRMLogin" style="display:none"></asp:TextBox>
                    <asp:TextBox runat="server" ID="fld_CTOLogin" style="display:none"></asp:TextBox>
                    <asp:TextBox runat="server" ID="fld_QAMLogin" style="display:none"></asp:TextBox>
                    <asp:TextBox runat="server" ID="fld_deptQELogin" style="display:none"></asp:TextBox>
                    <asp:TextBox runat="server" ID="fld_deptAdminLogin" style="display:none"></asp:TextBox>
                    <asp:TextBox runat="server" ID="fld_deptPMLogin" style="display:none"></asp:TextBox>
                    <asp:TextBox runat="server" ID="fld_deptSupplierMLogin" style="display:none"></asp:TextBox>
            </div>
            <div class="row">
                <p style="font-weight:bold;">Summary time（"<span style=" background:red;">&nbsp;</span>" must write） </p>
                <table class="table table-condensed table-bordered">
                    <tr>
                        <td class="td-label"> 
                       <span style="margin-top:5px;  height:30px; float:left;">&nbsp;</span>
                        <p style="text-align:center">Year</p>
                        </td>
                        <td class="td-content"  colspan="3" >
                            <%--<asp:Label runat="server" ID="Year" ></asp:Label>--%>
                            <asp:TextBox runat="server" ID="fld_Year" ></asp:TextBox>
                        </td>
                        <td class="td-label"> 
                       <span style=" background:red; margin-top:5px;  height:30px; float:left;">&nbsp;</span>
                        <p style="text-align:center">Month</p>
                        </td>
                        <td class="td-content"  colspan="3" >
                            <asp:DropDownList runat="server" ID="dropMonth" onchange="dropMonth_onchange(this)">
                                <asp:ListItem Selected="True">January</asp:ListItem>
                                <asp:ListItem>February</asp:ListItem>
                                <asp:ListItem>March</asp:ListItem>
                                <asp:ListItem>April</asp:ListItem>
                                <asp:ListItem>May</asp:ListItem>
                                <asp:ListItem>June</asp:ListItem>
                                <asp:ListItem>July</asp:ListItem>
                                <asp:ListItem>August</asp:ListItem>
                                <asp:ListItem>September</asp:ListItem>
                                <asp:ListItem>October</asp:ListItem>
                                <asp:ListItem>November</asp:ListItem>
                                <asp:ListItem>December</asp:ListItem>
                            </asp:DropDownList>
                            <asp:TextBox runat="server" ID="fld_Month" style="display:none;"></asp:TextBox>
                        </td>
                    </tr>
                    
                </table>
            </div>
            <div class="row">
             <ah:approvalhistory id="ApprovalHistory1" showaction="true" runat="server"></ah:approvalhistory>
            </div>
              <div id="ReturnBackTask" style="display:none; text-align:center">
        <asp:Button runat="server" ID="btnRevoke"  Visible="false" CssClass="btn" Text="Revoke" 
                onclick="btnRevoke_Click" />
        </div>
        <div style="display:none" id="divClose">
        <input type="button" value="Reports" name="btnRevoke" id="btnprint" onclick="loadReprots()" style="margin-left:50%" class="btn" />
        </div>
        </div>
        <btn:buttonlist id="ButtonList1" runat="server"></btn:buttonlist>
        <div style="display:none">
        <asp:TextBox runat="server" ID="fld_TRSummary"></asp:TextBox>
        <asp:HiddenField runat="server" ID="hdIncident" />
        <asp:HiddenField runat="server" ID="hdPrint" />
          <asp:HiddenField runat="server" ID="hdUrgeTask" />
        </div>
    </form>
</body>
</html>




