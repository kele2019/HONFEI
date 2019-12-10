<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AllEmployeeFinish.aspx.cs" Inherits="Presale.Process.EmployeePerformanceReport.AllEmployeeFinish" %>
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
        $(document).ready(function () {
            if ($("#read_ReportType") == "Begin-Year goal plan") { $("#ReportType1").attr("checked", true); }
            if ($("#read_ReportType") == "Mid-Year Update") { $("#ReportType2").attr("checked", true); }
            if ($("#read_ReportType") == "End-Year Performance") { $("#ReportType3").attr("checked", true); }
            if ($("#hdIncident").val() != "") {
                $("#ButtonList1_btnSubmit").val("Submit");
                $("#ButtonList1_btnBack").hide();
                $("#ButtonList1_btnReject").show();
            }
        });
        function beforeSave() {
            var summary = "Remind Submit Employee Performance Process";
            $("#UserInfo1_fld_PROCESSSUMMARY").val(summary);
            $("#fld_TRSummary").val(summary);
            return true;
        }
        function submitPageReview(obj) {
            $("#ButtonList1_btnSubmit").click();
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
     <div id="myDiv" class="container">
            <div class="row">
                <ui:userinfo id="UserInfo1" processtitle="Remind Submit Employee Performance Process" processprefix="HRPE" tablename="PROC_RemindSEP"
                    runat="server"  ></ui:userinfo>
            </div>
            <div class="row">
                <p style="font-weight:bold;">Report information</p>
                <table class="table table-condensed table-bordered">
                    <tr>
                        <td class="td-label"  style="width:100px">
                         <span style="height:30px; float:left;">&nbsp;</span>
                        <p style="text-align:center"> Year</p>
                        </td>
                        <td class="td-content" colspan="1" >
                            <asp:Label ID="read_Year" runat="server"></asp:Label>
                        </td>
                        <td class="td-label"> 
                       <span style="margin-top:5px;  height:30px; float:left;">&nbsp;</span>
                        <p style="text-align:center">Report Type</p>
                        </td>
                        <td class="td-content"  colspan="5" >
                            <asp:Label ID="read_ReportType" runat="server"></asp:Label>
                        </td>
                    </tr>
                </table>
                 <table class="table table-condensed table-bordered">
                    <tr>
                        <td class="td-label" style="vertical-align:middle;">
                            <p style="text-align:center">Remark</p>
                        </td>
                        <td class="td-content" colspan="4">
                            <asp:Label runat="server" ID="read_remindRemark"></asp:Label>
                        </td>
                    </tr>
                </table>
            </div>
            <div class="row">
             <ah:approvalhistory id="ApprovalHistory1" showaction="true" runat="server"></ah:approvalhistory>
            </div>
        </div>
        <div id="btnDiv" runat="server"   >
            <table style="width: 100%;" >
                <tr>
                    <td align="center"  >
                        <table>
                            <tr>
                                <td> 
                                <input type="button"  class="btn" value="Complete" onclick="submitPageReview('0')" />
                                </td>
                            </tr>
                       </table>
                    </td>
                 </tr>
            </table>
        </div>
        <div style="display:none;">
        <btn:buttonlist id="ButtonList1" runat="server"></btn:buttonlist>
        </div>
        <div style="display:none">
        <asp:TextBox runat="server" ID="fld_TRSummary"></asp:TextBox>
        <asp:HiddenField runat="server" ID="hdIncident" />
        <asp:HiddenField runat="server" ID="hdPrint" />
        </div>
    </form>
</body>
</html>


