<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="BeginYearGoalPlanComplete.aspx.cs" Inherits="Presale.Process.EmployeePerformanceReport.Approval" %>
<%@ Register Src="/Modules/Ultimus.UWF.Form.ProcessControl/UserInfo.ascx" TagName="UserInfo" TagPrefix="ui" %>
<%@ Register Src="/Modules/Ultimus.UWF.Form.ProcessControl/ApprovalHistory.ascx" TagName="ApprovalHistory" TagPrefix="ah" %>
<%@ Register Src="/Modules/Ultimus.UWF.Form.ProcessControl/Attachments.ascx" TagName="Attachments" TagPrefix="attach" %>
<%@ Register Src="/Modules/Ultimus.UWF.Form.ProcessControl/ButtonList.ascx" TagName="ButtonList" TagPrefix="btn" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta http-equiv="X-UA-Compatible"  content="IE=EmulateIE8" />
    <title>Employee Performance Report</title>
    <script type="text/javascript" src="/Assets/js/common.js"></script>
    <script type="text/javascript">
        $(document).ready(function () {
//            $("#tablediv").find("table").each(function (i, Etr) {
//                var values = $(Etr).find(".Honfei5InitiativesValue").text().split(",");
//                for (var j = 0; j < values.length; j++) {
//                    if (values[j] == "Growth") {
//                        $(Etr).find(".Honfei5Initiatives").eq(0).children().attr("checked", true);
//                    }
//                    if (values[j] == "Productivity") {
//                        $(Etr).find(".Honfei5Initiatives").eq(1).children().attr("checked", true);
//                    }
//                    if (values[j] == "Cash") {
//                        $(Etr).find(".Honfei5Initiatives").eq(2).children().attr("checked", true);
//                    }
//                    if (values[j] == "People") {
//                        $(Etr).find(".Honfei5Initiatives").eq(3).children().attr("checked", true);
//                    }
//                    if (values[j] == "Enablers") {
//                        $(Etr).find(".Honfei5Initiatives").eq(4).children().attr("checked", true);
//                    }
//                }
            //            });
            var PYear = $("#read_Year").text() - 0;
            $("#tablediv").find("table").each(function (i, Etr) {
                if (PYear > 2017) {
                    $(Etr).find(".Initiativesclass").find("label").eq(0).html("Culture")
                    $(Etr).find(".Initiativesclass").find("label").eq(1).html("Expand Margins")
                    $(Etr).find(".Initiativesclass").find("label").eq(2).html("Organic Growth")
                    $(Etr).find(".Initiativesclass").find("label").eq(3).html("Smart Investment")
                    $(Etr).find(".Initiativesclass").find("label").eq(4).html("Software Transformation")

                }
                var values = $(Etr).find(".Honfei5InitiativesValue").text().split(",");
                for (var j = 0; j < values.length; j++) {
                    if (values[j] == "Growth" || values[j] == "Culture") {
                        $(Etr).find(".Honfei5Initiatives").eq(0).children().attr("checked", true);
                    }
                    if (values[j] == "Productivity" || values[j] == "Expand Margins") {
                        $(Etr).find(".Honfei5Initiatives").eq(1).children().attr("checked", true);
                    }
                    if (values[j] == "Cash" || values[j] == "Organic Growth") {
                        $(Etr).find(".Honfei5Initiatives").eq(2).children().attr("checked", true);
                    }
                    if (values[j] == "People" || values[j] == "Smart Investment") {
                        $(Etr).find(".Honfei5Initiatives").eq(3).children().attr("checked", true);
                    }
                    if (values[j] == "Enablers" || values[j] == "Software Transformation") {
                        $(Etr).find(".Honfei5Initiatives").eq(4).children().attr("checked", true);
                    }
                }
            });
            if (request('Type') == "myapproval")
                $("#btnDiv").hide();
        });
        function submitPageReview(obj) {
            $("#ButtonList1_btnSubmit").click();
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
     <div id="myDiv" class="container">
            <div class="row">
                <ui:userinfo id="UserInfo1" processtitle="Employee Performance Report" processprefix="HRRP" tablename="PROC_EmployeePerformance" tablenamedetail="PROC_EmployeePerformance_DT"
                    runat="server"  ></ui:userinfo>
            </div>
            <div class="row">
                <p style="font-weight:bold;">Report information</p>
                <table class="table table-condensed table-bordered">
                    <tr>
                        <td class="td-label">
                         <span style="height:30px; float:left;">&nbsp;</span>
                        <p style="text-align:center"> Year</p>
                        </td>
                        <td class="td-content" colspan="3" >
                            <asp:Label ID="read_Year" runat="server"></asp:Label>
                        </td>
                        <td class="td-label"> 
                       <span style="margin-top:5px;  height:30px; float:left;">&nbsp;</span>
                        <p style="text-align:center">Report Type</p>
                        </td>
                        <td class="td-content"  colspan="3" >
                            <asp:Label ID="read_ReportType" runat="server"></asp:Label>
                        </td>
                    </tr>
                </table>
                <p style="font-weight:bold;">Employee Information </p>
                <table class="table table-condensed table-bordered">
                    <tr>
                        <td class="td-label">
                         <span style="height:30px; float:left;">&nbsp;</span>
                        <p style="text-align:center">Employee Name</p>
                        </td>
                        <td class="td-content" colspan="3" >
                            <asp:Label ID="read_EmployeeName" runat="server"></asp:Label>
                        </td>
                        <td class="td-label"> 
                       <span style="margin-top:5px;  height:30px; float:left;">&nbsp;</span>
                        <p style="text-align:center">On-boarding department</p>
                        </td>
                        <td class="td-content"  colspan="3" >
                            <asp:Label ID="read_OnBoardingDepartment" runat="server"></asp:Label>
                        </td>
                    </tr>
                </table>
                <div id="tablediv">
                <p style="font-weight:bold;">Goal Plan</p>
                <asp:Repeater ID="fld_detail_PROC_EmployeePerformance_DT" runat="server" >
                    <ItemTemplate>
                        <table class="table table-condensed table-bordered">
                            <tr>
                                <td class="td-label">
                                 <span style="height:30px; float:left;">&nbsp;</span>
                                <p style="text-align:center">Goal Name</p>
                                </td>
                                <td class="td-content" colspan="3" >
                                    <asp:Label ID="read_GoalName" Text='<%#Eval("GoalName") %>' runat="server"></asp:Label>
                                </td>
                                <td class="td-label"> 
                               <span style="margin-top:5px;  height:30px; float:left;">&nbsp;</span>
                                <p style="text-align:center">Completion Status</p>
                                </td>
                                <td class="td-content"  colspan="2" >
                                    <asp:Label runat="server" ID="read_CompletionStatus" Text='<%#Eval("CompletionStatus") %>'></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td class="td-label">
                                 <span style="height:30px; float:left;">&nbsp;</span>
                                <p style="text-align:center">Goal Details</p>
                                </td>
                                <td class="td-content" colspan="6" >
                                    <asp:Label ID="read_GoalDetails" Text='<%#Eval("GoalDetails") %>' runat="server"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td class="td-label">
                                 <span style="height:30px; float:left;">&nbsp;</span>
                                <p style="text-align:center">Honfei 5 Initiatives</p>
                                </td>
                                <td class="td-content Initiativesclass" colspan="6" >
                                    <asp:CheckBox runat="server" Enabled="false" class="Honfei5Initiatives" ID="Honfei5Initiatives1" Text="Growth"  onclick="getButtonCheck(this,1)" />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                    <asp:CheckBox runat="server" Enabled="false" class="Honfei5Initiatives" ID="Honfei5Initiatives2" Text="Productivity" onclick="getButtonCheck(this,2)" />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                    <asp:CheckBox runat="server" Enabled="false" class="Honfei5Initiatives" ID="Honfei5Initiatives3" Text="Cash" onclick="getButtonCheck(this,3)" />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                    <asp:CheckBox runat="server" Enabled="false" class="Honfei5Initiatives" ID="Honfei5Initiatives4" Text="People" onclick="getButtonCheck(this,4)" />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                    <asp:CheckBox runat="server" Enabled="false" class="Honfei5Initiatives" ID="Honfei5Initiatives5" Text="Enablers" onclick="getButtonCheck(this,5)"  />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                </td>
                                <td style="display:none">
                                    <asp:Label runat="server" class="Honfei5InitiativesValue" ID="read_Honfei5Initiatives"  Text='<%#Eval("Honfei5Initiatives") %>'></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td class="td-label">
                                 <span style="height:30px; float:left;">&nbsp;</span>
                                <p style="text-align:center">Assigned Date</p>
                                </td>
                                <td class="td-content" colspan="3" >
                                    <asp:Label runat="server" ID="read_AssignedDate" Text='<%# String.IsNullOrEmpty(Eval("AssignedDate").ToString())? "":DateTime.Parse(Eval("AssignedDate").ToString()).ToString("yyyy-MM-dd") %>'></asp:Label>
                                </td>
                                <td class="td-label">
                                 <span style="height:30px; float:left;">&nbsp;</span>
                                <p style="text-align:center">Due Date</p>
                                </td>
                                <td class="td-content" colspan="2" >
                                    <asp:Label runat="server" ID="read_DueDate" Text='<%# String.IsNullOrEmpty(Eval("DueDate").ToString())? "":DateTime.Parse(Eval("DueDate").ToString()).ToString("yyyy-MM-dd") %>'></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td class="td-label">
                                 <span style="height:30px; float:left;">&nbsp;</span>
                                <p style="text-align:center">Results</p>
                                </td>
                                <td class="td-content" colspan="6" >
                                    <asp:Label ID="read_Results" Text='<%#Eval("Results") %>' runat="server" ></asp:Label>
                                </td>
                            </tr>
                        </table>
                    </ItemTemplate>
                </asp:Repeater>
                </div>
            </div>
            <div class="row" style="display:none;">
                <attach:attachments id="Attachments1" runat="server"></attach:attachments>
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











