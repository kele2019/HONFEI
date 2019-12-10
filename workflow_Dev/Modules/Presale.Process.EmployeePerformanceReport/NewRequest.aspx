<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="NewRequest.aspx.cs" Inherits="Presale.Process.EmployeePerformanceReport.NewRequest"  ValidateRequest="false" %>

<%@ Register Src="/Modules/Ultimus.UWF.Form.ProcessControl/UserInfo.ascx" TagName="UserInfo" TagPrefix="ui" %>
<%@ Register Src="/Modules/Ultimus.UWF.Form.ProcessControl/ApprovalHistory.ascx" TagName="ApprovalHistory" TagPrefix="ah" %>
<%@ Register Src="/Modules/Ultimus.UWF.Form.ProcessControl/Attachments.ascx" TagName="Attachments" TagPrefix="attach" %>
<%@ Register Src="/Modules/Ultimus.UWF.Form.ProcessControl/ButtonList.ascx" TagName="ButtonList" TagPrefix="btn" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta http-equiv="X-UA-Compatible"  content="IE=EmulateIE8" />
    <title>Remind Submit Employee Performance Process</title>
    <script type="text/javascript" src="/Assets/js/common.js"></script>
    <script type="text/javascript">
        function beforeSubmit() {
            if ($("#ReportType1").attr("checked")) {
                $("#fld_ReportType").val("Begin-Year goal plan");
            }
            if ($("#ReportType2").attr("checked")) {
                $("#fld_ReportType").val("Mid-Year Update");
            }
            if ($("#ReportType3").attr("checked")) {
                $("#fld_ReportType").val("End-Year Performance");
            }
            $("#fld_Year").val($("#dropYear option:selected").text());
            var summary = "Remind Submit Employee Performance Process";
            $("#UserInfo1_fld_PROCESSSUMMARY").val(summary);
            return true;
        }
        $(document).ready(function () {
            if ($("#fld_ReportType").val() == "Begin-Year goal plan") { $("#ReportType1").attr("checked", true); }
            if ($("#fld_ReportType").val() == "Mid-Year Update") { $("#ReportType2").attr("checked", true); }
            if ($("#fld_ReportType").val() == "End-Year Performance") { $("#ReportType3").attr("checked", true); }
            if ($("#hdIncident").val() != "") {
                $("#ButtonList1_btnSubmit").val("Submit");
                $("#ButtonList1_btnBack").hide();
                $("#ButtonList1_btnReject").show();
            }
            if ($("#hdUrgeTask").val() == "Yes") {
                $("#ReturnBackTask").show();
            }

          $("#dropYear").val($("#fld_Year").val());
        });
        function beforeSave() {
            var summary = "Remind Submit Employee Performance Process";
            $("#UserInfo1_fld_PROCESSSUMMARY").val(summary);
            $("#fld_TRSummary").val(summary);
            return true;
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
     <div id="myDiv" class="container">
            <div class="row">
                <ui:userinfo id="UserInfo1" processtitle="Remind Submit Employee Performance Process" processprefix="HRPE" tablename="PROC_RemindSEP"
                    runat="server"  ></ui:userinfo>
                <asp:TextBox runat="server" ID="fld_ApprovalArr_AllEmployee" style="display:none;"></asp:TextBox>
            </div>
            <div class="row">
                <p style="font-weight:bold;">Report information（"<span style=" background:red;">&nbsp;</span>" must write） </p>
                <table class="table table-condensed table-bordered">
                    <tr>
                        <td class="td-label" style="vertical-align:middle;">
                         <span style=" background:red;  height:30px; float:left;">&nbsp;</span>
                        <p style="text-align:center"> Year</p>
                        </td>
                        <td class="td-content" style="vertical-align:middle;">
                            <asp:DropDownList ID="dropYear" runat="server"></asp:DropDownList>
                            <asp:TextBox ID="fld_Year" runat="server" style="display:none;"></asp:TextBox>
                        </td>
                        <td class="td-label" style="vertical-align:middle;"> 
                       <span style=" background:red;height:30px; float:left;">&nbsp;</span>
                        <p style="text-align:center">Report Type</p>
                        </td>
                        <td class="td-content">
                            <asp:RadioButton ID="ReportType1" GroupName="ReportType" runat="server" value="Begin-Year goal plan" Checked="false" />Begin-Year goal plan&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                            <asp:RadioButton ID="ReportType2" GroupName="ReportType" runat="server" value="Mid-Year Update" />Mid-Year Update<br />
                            <asp:RadioButton ID="ReportType3" GroupName="ReportType" runat="server" value="End-Year Performance" />End-Year Performance&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                            <asp:TextBox ID="fld_ReportType" runat="server" style="display:none;"></asp:TextBox>
                        </td>
                    </tr>
                </table>
                <table class="table table-condensed table-bordered">
                    <tr>
                        <td class="td-label" style="vertical-align:middle;">
                            <p style="text-align:center">Remark</p>
                        </td>
                        <td class="td-content" colspan="4">
                            <asp:TextBox runat="server" ID="fld_remindRemark" TextMode="MultiLine" Rows="3" Width="98%"></asp:TextBox>
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


