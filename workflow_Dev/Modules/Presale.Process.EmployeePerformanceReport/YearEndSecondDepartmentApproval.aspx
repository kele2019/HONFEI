<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="YearEndSecondDepartmentApproval.aspx.cs" Inherits="Presale.Process.EmployeePerformanceReport.YearEndSecondDepartmentApproval" %>

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
        function submitPageReview(obj) {
            $("#ButtonList1_btnSubmit").click();
        }
        $(document).ready(function () {
//            if ($("#read_Result1Score").text() == "Exceed") {
//                $("#RadioButton1").attr("checked", true);
//            }
//            if ($("#read_Result1Score").text() == "At") {
//                $("#RadioButton2").attr("checked", true);
//            }
//            if ($("#read_Result1Score").text() == "Below") {
//                $("#RadioButton3").attr("checked", true);
//            }
//            if ($("#read_Result2Score").text() == "Exceed") {
//                $("#RadioButton4").attr("checked", true);
//            }
//            if ($("#read_Result2Score").text() == "At") {
//                $("#RadioButton5").attr("checked", true);
//            }
//            if ($("#read_Result2Score").text() == "Below") {
//                $("#RadioButton6").attr("checked", true);
//            }
//            if ($("#read_Result3Score").text() == "Exceed") {
//                $("#RadioButton7").attr("checked", true);
//            }
//            if ($("#read_Result3Score").text() == "At") {
//                $("#RadioButton8").attr("checked", true);
//            }
//            if ($("#read_Result3Score").text() == "Below") {
//                $("#RadioButton9").attr("checked", true);
//            }
//            if ($("#read_Result4Score").text() == "Exceed") {
//                $("#RadioButton10").attr("checked", true);
//            }
//            if ($("#read_Result4Score").text() == "At") {
//                $("#RadioButton11").attr("checked", true);
//            }
//            if ($("#read_Result4Score").text() == "Below") {
//                $("#RadioButton12").attr("checked", true);
//            }
//            if ($("#read_Result5Score").text() == "Exceed") {
//                $("#RadioButton13").attr("checked", true);
//            }
//            if ($("#read_Result5Score").text() == "At") {
//                $("#RadioButton14").attr("checked", true);
//            }
//            if ($("#read_Result5Score").text() == "Below") {
//                $("#RadioButton15").attr("checked", true);
//            }
//            if ($("#read_Result6Score").text() == "Exceed") {
//                $("#RadioButton16").attr("checked", true);
//            }
//            if ($("#read_Result6Score").text() == "At") {
//                $("#RadioButton17").attr("checked", true);
//            }
//            if ($("#read_Result6Score").text() == "Below") {
//                $("#RadioButton18").attr("checked", true);
//            }
//            if ($("#read_Result7Score").text() == "Exceed") {
//                $("#RadioButton19").attr("checked", true);
//            }
//            if ($("#read_Result7Score").text() == "At") {
//                $("#RadioButton20").attr("checked", true);
//            }
//            if ($("#read_Result7Score").text() == "Below") {
//                $("#RadioButton21").attr("checked", true);
//            }
//            if ($("#read_Result8Score").text() == "Exceed") {
//                $("#RadioButton22").attr("checked", true);
//            }
//            if ($("#read_Result8Score").text() == "At") {
//                $("#RadioButton23").attr("checked", true);
//            }
//            if ($("#read_Result8Score").text() == "Below") {
//                $("#RadioButton24").attr("checked", true);
//            }
//            if ($("#read_Behavior1Score").text() == "Exceed") {
//                $("#RadioButton25").attr("checked", true);
//            }
//            if ($("#read_Behavior1Score").text() == "At") {
//                $("#RadioButton26").attr("checked", true);
//            }
//            if ($("#read_Behavior1Score").text() == "Below") {
//                $("#RadioButton27").attr("checked", true);
//            }
//            if ($("#read_Behavior2Score").text() == "Exceed") {
//                $("#RadioButton28").attr("checked", true);
//            }
//            if ($("#read_Behavior2Score").text() == "At") {
//                $("#RadioButton29").attr("checked", true);
//            }
//            if ($("#read_Behavior2Score").text() == "Below") {
//                $("#RadioButton30").attr("checked", true);
//            }
//            if ($("#read_Behavior3Score").text() == "Exceed") {
//                $("#RadioButton31").attr("checked", true);
//            }
//            if ($("#read_Behavior3Score").text() == "At") {
//                $("#RadioButton32").attr("checked", true);
//            }
//            if ($("#read_Behavior3Score").text() == "Below") {
//                $("#RadioButton33").attr("checked", true);
//            }
//            if ($("#read_Behavior4Score").text() == "Exceed") {
//                $("#RadioButton34").attr("checked", true);
//            }
//            if ($("#read_Behavior4Score").text() == "At") {
//                $("#RadioButton35").attr("checked", true);
//            }
//            if ($("#read_Behavior4Score").text() == "Below") {
//                $("#RadioButton36").attr("checked", true);
//            }
//            if ($("#read_Behavior5Score").text() == "Exceed") {
//                $("#RadioButton37").attr("checked", true);
//            }
//            if ($("#read_Behavior5Score").text() == "At") {
//                $("#RadioButton38").attr("checked", true);
//            }
//            if ($("#read_Behavior5Score").text() == "Below") {
//                $("#RadioButton39").attr("checked", true);
//            }
//            if ($("#read_Behavior6Score").text() == "Exceed") {
//                $("#RadioButton40").attr("checked", true);
//            }
//            if ($("#read_Behavior6Score").text() == "At") {
//                $("#RadioButton41").attr("checked", true);
//            }
//            if ($("#read_Behavior6Score").text() == "Below") {
//                $("#RadioButton42").attr("checked", true);
//            }
//            if ($("#read_Behavior7Score").text() == "Exceed") {
//                $("#RadioButton43").attr("checked", true);
//            }
//            if ($("#read_Behavior7Score").text() == "At") {
//                $("#RadioButton44").attr("checked", true);
//            }
//            if ($("#read_Behavior7Score").text() == "Below") {
//                $("#RadioButton45").attr("checked", true);
//            }
//            if ($("#read_Behavior8Score").text() == "Exceed") {
//                $("#RadioButton46").attr("checked", true);
//            }
//            if ($("#read_Behavior8Score").text() == "At") {
//                $("#RadioButton47").attr("checked", true);
//            }
//            if ($("#read_Behavior8Score").text() == "Below") {
//                $("#RadioButton48").attr("checked", true);
//            }
//            if ($("#read_Behavior9Score").text() == "Exceed") {
//                $("#RadioButton49").attr("checked", true);
//            }
//            if ($("#read_Behavior9Score").text() == "At") {
//                $("#RadioButton50").attr("checked", true);
//            }
//            if ($("#read_Behavior9Score").text() == "Below") {
//                $("#RadioButton51").attr("checked", true);
//            }
//            if ($("#read_Behavior10Score").text() == "Exceed") {
//                $("#RadioButton52").attr("checked", true);
//            }
//            if ($("#read_Behavior10Score").text() == "At") {
//                $("#RadioButton53").attr("checked", true);
//            }
//            if ($("#read_Behavior10Score").text() == "Below") {
//                $("#RadioButton54").attr("checked", true);
//            }
//            if ($("#read_Behavior11Score").text() == "Exceed") {
//                $("#RadioButton55").attr("checked", true);
//            }
//            if ($("#read_Behavior11Score").text() == "At") {
//                $("#RadioButton56").attr("checked", true);
//            }
//            if ($("#read_Behavior11Score").text() == "Below") {
//                $("#RadioButton57").attr("checked", true);
//            }
//            if ($("#read_Behavior12Score").text() == "Exceed") {
//                $("#RadioButton58").attr("checked", true);
//            }
//            if ($("#read_Behavior12Score").text() == "At") {
//                $("#RadioButton59").attr("checked", true);
//            }
//            if ($("#read_Behavior12Score").text() == "Below") {
//                $("#RadioButton60").attr("checked", true);
//            }

            if ($("#hdIncident").val() != "") {
                $("#ButtonList1_btnSubmit").val("Submit");
                $("#ButtonList1_btnBack").hide();
                $("#ButtonList1_btnReject").show();
            }
            LoadBehavior($("#read_Behavior1Score"), 'Behavior1Score');
            LoadBehavior($("#read_Behavior2Score"), 'Behavior2Score');
            LoadBehavior($("#read_Behavior3Score"), 'Behavior3Score');
            LoadBehavior($("#read_Behavior4Score"), 'Behavior4Score');
            LoadBehavior($("#read_Behavior5Score"), 'Behavior5Score');
            LoadBehavior($("#read_Behavior6Score"), 'Behavior6Score');
            LoadBehavior($("#read_Behavior7Score"), 'Behavior7Score');
            LoadBehavior($("#read_Behavior8Score"), 'Behavior8Score');
            LoadBehavior($("#read_Behavior9Score"));
            LoadBehavior($("#read_Behavior10Score"));
            LoadBehavior($("#read_Behavior11Score"));
            LoadBehavior($("#read_Behavior12Score"));

        });
        function LoadBehavior(obj, flag) {
            var RDate = $("#UserInfo1_fld_REQUESTDATE").text();
            var RYear = new Date(RDate).getFullYear() - 0;
            var value = $(obj).text();

            if (RYear < 2017) {
                $("#behaviortable1").show();
                $("#behaviortable2").hide();

                if (value == "Exceed")
                    $(obj).parent().prev().prev().find("input").attr("checked", true);
                if (value == "At")
                    $(obj).parent().prev().find("input").attr("checked", true);
                if (value == "Below")
                    $(obj).prev().attr("checked", true);
                if (value == "Rarely")
                    $(obj).prev().attr("checked", true);
            }
            else {
                $("#behaviortable1").hide();
                $("#behaviortable2").show();
                if (flag != undefined) {
                    if (value == "Exceed")
                        $("input[name^='" + flag + "']").eq(3).attr("checked", true);
                    if (value == "At")
                        $("input[name^='" + flag + "']").eq(4).attr("checked", true);
                    if (value == "Below")
                        $("input[name^='" + flag + "']").eq(5).attr("checked", true);
                    if (value == "Rarely")
                        $("input[name^='" + flag + "']").eq(6).attr("checked", true);

                }
            }
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
     <div id="myDiv" class="container">
            <div class="row">
                <ui:userinfo id="UserInfo1" processtitle="Employee Performance Report" processprefix="HRRP" tablename="PROC_EmployeePerformance"
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
                        <td class="td-content">
                            <asp:Label ID="read_Year" runat="server"></asp:Label>
                        </td>
                        <td class="td-label"> 
                       <span style="margin-top:5px;  height:30px; float:left;">&nbsp;</span>
                        <p style="text-align:center">Report Type</p>
                        </td>
                        <td class="td-content">
                            <asp:Label ID="read_ReportType" runat="server"></asp:Label>
                        </td>
                    </tr>
                </table>
                <p style="font-weight:bold;">Employee Information</p>
                <table class="table table-condensed table-bordered">
                    <tr>
                        <td class="td-label">
                         <span style="height:30px; float:left;">&nbsp;</span>
                        <p style="text-align:center">Employee Name</p>
                        </td>
                        <td class="td-content">
                            <asp:Label ID="read_EmployeeName" runat="server"></asp:Label>
                        </td>
                        <td class="td-label"> 
                       <span style="margin-top:5px;  height:30px; float:left;">&nbsp;</span>
                        <p style="text-align:center">On-boarding department</p>
                        </td>
                        <td class="td-content">
                            <asp:Label ID="read_OnBoardingDepartment" runat="server"></asp:Label>
                        </td>
                    </tr>
                </table>
                <p style="font-weight:bold;">Results Assessment</p>
                <table class="table table-condensed table-bordered">
                    <tr>
                        <th width="4%">
                            <p style="text-align:center" >No</p>
                        </th>
                         <th width="54%">
                            <p style="text-align:center">Goals</p>
                        </th>
                         <th width="14%">
                            <p style="text-align:center" >Exceed</p>
                        </th>
                        <th width="14%">
                            <p style="text-align:center">At</p>
                        </th>
                         <th width="14%">
                            <p style="text-align:center">Below</p>
                        </th>
                    </tr>
                    <tr>
                        <td style="text-align:center;vertical-align:middle">
                         <span style="height:30px; float:left;">&nbsp;</span>
                        <p style="text-align:center">1</p>
                        </td>
                        <td style="text-align:center;vertical-align:middle">
                            <asp:Label ID="read_Result1Goals" runat="server"></asp:Label>
                        </td>
                        <td style="text-align:center;vertical-align:middle"> 
                       <span style="height:30px; float:left;">&nbsp;</span>
                            <asp:RadioButton ID="RadioButton1" runat="server" GroupName="Result1Score" Value="Exceed" ReadOnly="true"/>
                        </td>
                        <td style="text-align:center;vertical-align:middle">
                       <span style="height:30px; float:left;">&nbsp;</span>
                            <asp:RadioButton ID="RadioButton2" runat="server" GroupName="Result1Score" Value="At" ReadOnly="true"/>
                        </td>
                        <td style="text-align:center;vertical-align:middle">
                       <span style="height:30px; float:left;">&nbsp;</span>
                            <asp:RadioButton ID="RadioButton3" runat="server" GroupName="Result1Score" Value="Below" ReadOnly="true"/>
                            <asp:Label runat="server" ID="read_Result1Score" style="display:none;"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align:center;vertical-align:middle">
                         <span style="height:30px; float:left;">&nbsp;</span>
                        <p style="text-align:center">2</p>
                        </td>
                        <td style="text-align:center;vertical-align:middle">
                            <asp:Label ID="read_Result2Goals" runat="server"></asp:Label>
                        </td>
                        <td style="text-align:center;vertical-align:middle"> 
                       <span style="height:30px; float:left;">&nbsp;</span>
                            <asp:RadioButton ID="RadioButton4" runat="server" GroupName="Result2Score" Value="Exceed" ReadOnly="true" />
                        </td>
                        <td style="text-align:center;vertical-align:middle">
                       <span style="height:30px; float:left;">&nbsp;</span>
                            <asp:RadioButton ID="RadioButton5" runat="server" GroupName="Result2Score" Value="At" ReadOnly="true" />
                        </td>
                        <td style="text-align:center;vertical-align:middle">
                       <span style="height:30px; float:left;">&nbsp;</span>
                            <asp:RadioButton ID="RadioButton6" runat="server" GroupName="Result2Score" Value="Below" ReadOnly="true" />
                            <asp:Label runat="server" ID="read_Result2Score" style="display:none;"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align:center;vertical-align:middle">
                         <span style="height:30px; float:left;">&nbsp;</span>
                        <p style="text-align:center">3</p>
                        </td>
                        <td style="text-align:center;vertical-align:middle">
                            <asp:Label ID="read_Result3Goals" runat="server"></asp:Label>
                        </td>
                        <td style="text-align:center;vertical-align:middle"> 
                       <span style="height:30px; float:left;">&nbsp;</span>
                            <asp:RadioButton ID="RadioButton7" runat="server" GroupName="Result3Score" Value="Exceed" ReadOnly="true" />
                        </td>
                        <td style="text-align:center;vertical-align:middle">
                       <span style="height:30px; float:left;">&nbsp;</span>
                            <asp:RadioButton ID="RadioButton8" runat="server" GroupName="Result3Score" Value="At" ReadOnly="true" />
                        </td>
                        <td style="text-align:center;vertical-align:middle">
                       <span style="height:30px; float:left;">&nbsp;</span>
                            <asp:RadioButton ID="RadioButton9" runat="server" GroupName="Result3Score" Value="Below" ReadOnly="true" />
                            <asp:Label runat="server" ID="read_Result3Score" style="display:none;"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align:center;vertical-align:middle">
                         <span style="height:30px; float:left;">&nbsp;</span>
                        <p style="text-align:center">4</p>
                        </td>
                        <td style="text-align:center;vertical-align:middle">
                            <asp:Label ID="read_Result4Goals" runat="server"></asp:Label>
                        </td>
                        <td style="text-align:center;vertical-align:middle"> 
                       <span style="height:30px; float:left;">&nbsp;</span>
                            <asp:RadioButton ID="RadioButton10" runat="server" GroupName="Result4Score" Value="Exceed" ReadOnly="true" />
                        </td>
                        <td style="text-align:center;vertical-align:middle">
                       <span style="height:30px; float:left;">&nbsp;</span>
                            <asp:RadioButton ID="RadioButton11" runat="server" GroupName="Result4Score" Value="At" ReadOnly="true" />
                        </td>
                        <td style="text-align:center;vertical-align:middle">
                       <span style="height:30px; float:left;">&nbsp;</span>
                            <asp:RadioButton ID="RadioButton12" runat="server" GroupName="Result4Score" Value="Below" ReadOnly="true" />
                            <asp:Label runat="server" ID="read_Result4Score" style="display:none;"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align:center;vertical-align:middle">
                         <span style="height:30px; float:left;">&nbsp;</span>
                        <p style="text-align:center">5</p>
                        </td>
                        <td style="text-align:center;vertical-align:middle">
                            <asp:Label ID="read_Result5Goals" runat="server" ></asp:Label>
                        </td>
                        <td style="text-align:center;vertical-align:middle"> 
                       <span style="height:30px; float:left;">&nbsp;</span>
                            <asp:RadioButton ID="RadioButton13" runat="server" GroupName="Result5Score" Value="Exceed" ReadOnly="true"/>
                        </td>
                        <td style="text-align:center;vertical-align:middle">
                       <span style="height:30px; float:left;">&nbsp;</span>
                            <asp:RadioButton ID="RadioButton14" runat="server" GroupName="Result5Score" Value="At" ReadOnly="true" />
                        </td>
                        <td style="text-align:center;vertical-align:middle">
                       <span style="height:30px; float:left;">&nbsp;</span>
                            <asp:RadioButton ID="RadioButton15" runat="server" GroupName="Result5Score" Value="Below" ReadOnly="true"/>
                            <asp:Label runat="server" ID="read_Result5Score" style="display:none;"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align:center;vertical-align:middle">
                         <span style="height:30px; float:left;">&nbsp;</span>
                        <p style="text-align:center">6</p>
                        </td>
                        <td style="text-align:center;vertical-align:middle">
                            <asp:Label ID="read_Result6Goals" runat="server"></asp:Label>
                        </td>
                        <td style="text-align:center;vertical-align:middle"> 
                       <span style="height:30px; float:left;">&nbsp;</span>
                            <asp:RadioButton ID="RadioButton16" runat="server" GroupName="Result6Score" Value="Exceed" ReadOnly="true" />
                        </td>
                        <td style="text-align:center;vertical-align:middle">
                       <span style="height:30px; float:left;">&nbsp;</span>
                            <asp:RadioButton ID="RadioButton17" runat="server" GroupName="Result6Score" Value="At" ReadOnly="true" />
                        </td>
                        <td style="text-align:center;vertical-align:middle">
                       <span style="height:30px; float:left;">&nbsp;</span>
                            <asp:RadioButton ID="RadioButton18" runat="server" GroupName="Result6Score" Value="Below" ReadOnly="true" />
                            <asp:Label runat="server" ID="read_Result6Score" style="display:none;"></asp:Label>
                        </td>
                    </tr>
                     <tr>
                        <td style="text-align:center;vertical-align:middle">
                         <span style="height:30px; float:left;">&nbsp;</span>
                        <p style="text-align:center">7</p>
                        </td>
                        <td style="text-align:center;vertical-align:middle">
                            <asp:Label ID="read_Result7Goals" runat="server"></asp:Label>
                        </td>
                        <td style="text-align:center;vertical-align:middle"> 
                       <span style="height:30px; float:left;">&nbsp;</span>
                            <asp:RadioButton ID="RadioButton19" runat="server" GroupName="Result7Score" Value="Exceed" ReadOnly="true" />
                        </td>
                        <td style="text-align:center;vertical-align:middle">
                       <span style="height:30px; float:left;">&nbsp;</span>
                            <asp:RadioButton ID="RadioButton20" runat="server" GroupName="Result7Score" Value="At" ReadOnly="true"/>
                        </td>
                        <td style="text-align:center;vertical-align:middle">
                       <span style="height:30px; float:left;">&nbsp;</span>
                            <asp:RadioButton ID="RadioButton21" runat="server" GroupName="Result7Score" Value="Below" ReadOnly="true"/>
                            <asp:Label runat="server" ID="read_Result7Score" style="display:none;"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align:center;vertical-align:middle">
                         <span style="height:30px; float:left;">&nbsp;</span>
                        <p style="text-align:center">8</p>
                        </td>
                        <td style="text-align:center;vertical-align:middle">
                            <asp:Label ID="read_Result8Goals" runat="server"></asp:Label>
                        </td>
                        <td style="text-align:center;vertical-align:middle"> 
                       <span style="height:30px; float:left;">&nbsp;</span>
                            <asp:RadioButton ID="RadioButton22" runat="server" GroupName="Result8Score" Value="Exceed" ReadOnly="true"/>
                        </td>
                        <td style="text-align:center;vertical-align:middle">
                       <span style="height:30px; float:left;">&nbsp;</span>
                            <asp:RadioButton ID="RadioButton23" runat="server" GroupName="Result8Score" Value="At" ReadOnly="true" />
                        </td>
                        <td style="text-align:center;vertical-align:middle">
                       <span style="height:30px; float:left;">&nbsp;</span>
                            <asp:RadioButton ID="RadioButton24" runat="server" GroupName="Result8Score" Value="Below" ReadOnly="true"/>
                            <asp:Label runat="server" ID="read_Result8Score" style="display:none;"></asp:Label>
                        </td>
                    </tr>
                </table>
                <table class="table table-condensed table-bordered">
                    <tr>
                        <td class="td-label">
                         <span style="height:30px; float:left;">&nbsp;</span>
                        <p style="text-align:center">Comments</p>
                        </td>
                        <td class="td-content" colspan="7" >
                            <asp:Label ID="read_Comments" runat="server"></asp:Label>
                        </td>
                    </tr>
                </table>
                <p style="font-weight:bold;">Behavior Assessment</p>
                <table class="table table-condensed table-bordered" id="behaviortable1">
                    <tr>
                        <th width="4%">
                            <p style="text-align:center" >No</p>
                        </th>
                         <th width="54%">
                            <p style="text-align:center">Goals</p>
                        </th>
                         <th width="14%">
                            <p style="text-align:center" >Exceed</p>
                        </th>
                        <th width="14%">
                            <p style="text-align:center">At</p>
                        </th>
                         <th width="14%">
                            <p style="text-align:center">Below</p>
                        </th>
                    </tr>
                    <tr>
                        <td style="text-align:center;vertical-align:middle;">
                         <span style="height:30px; float:left;">&nbsp;</span>
                        <p style="text-align:center">1</p>
                        </td>
                        <td style="text-align:center;vertical-align:middle;">
                            <p style="text-align:center">Global Mindset</p>
                        </td>
                        <td style="text-align:center;vertical-align:middle;"> 
                       <span style="height:30px; float:left;">&nbsp;</span>
                            <asp:RadioButton ID="RadioButton25" runat="server" GroupName="Behavior1Score" Value="Exceed" ReadOnly="true" />
                        </td>
                        <td style="text-align:center;vertical-align:middle;">
                       <span style="height:30px; float:left;">&nbsp;</span>
                            <asp:RadioButton ID="RadioButton26" runat="server" GroupName="Behavior1Score" Value="At" ReadOnly="true"/>
                        </td>
                        <td style="text-align:center;vertical-align:middle;">
                       <span style="height:30px; float:left;">&nbsp;</span>
                            <asp:RadioButton ID="RadioButton27" runat="server" GroupName="Behavior1Score" Value="Below" ReadOnly="true"/>
                            <asp:Label runat="server" ID="read_Behavior1Score" style="display:none;"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align:center;vertical-align:middle;">
                         <span style="height:30px; float:left;">&nbsp;</span>
                        <p style="text-align:center">2</p>
                        </td>
                        <td style="text-align:center;vertical-align:middle;">
                            <p style="text-align:center">Leadership Impact</p>
                        </td>
                        <td style="text-align:center;vertical-align:middle;"> 
                       <span style="height:30px; float:left;">&nbsp;</span>
                            <asp:RadioButton ID="RadioButton28" runat="server" GroupName="Behavior2Score" Value="Exceed" ReadOnly="true" />
                        </td>
                        <td style="text-align:center;vertical-align:middle;">
                       <span style="height:30px; float:left;">&nbsp;</span>
                            <asp:RadioButton ID="RadioButton29" runat="server" GroupName="Behavior2Score" Value="At" ReadOnly="true"/>
                        </td>
                        <td style="text-align:center;vertical-align:middle;">
                       <span style="height:30px; float:left;">&nbsp;</span>
                            <asp:RadioButton ID="RadioButton30" runat="server" GroupName="Behavior2Score" Value="Below" ReadOnly="true"/>
                            <asp:Label runat="server" ID="read_Behavior2Score" style="display:none;"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align:center;vertical-align:middle;">
                         <span style="height:30px; float:left;">&nbsp;</span>
                        <p style="text-align:center">3</p>
                        </td>
                        <td style="text-align:center;vertical-align:middle;">
                            <p style="text-align:center">Gets Results</p>
                        </td>
                        <td style="text-align:center;vertical-align:middle;"> 
                       <span style="height:30px; float:left;">&nbsp;</span>
                            <asp:RadioButton ID="RadioButton31" runat="server" GroupName="Behavior3Score" Value="Exceed" ReadOnly="true"/>
                        </td>
                        <td style="text-align:center;vertical-align:middle;">
                       <span style="margin-top:5px;  height:30px; float:left;">&nbsp;</span>
                            <asp:RadioButton ID="RadioButton32" runat="server" GroupName="Behavior3Score" Value="At" ReadOnly="true"/>
                        </td>
                        <td style="text-align:center;vertical-align:middle;">
                       <span style="margin-top:5px;  height:30px; float:left;">&nbsp;</span>
                            <asp:RadioButton ID="RadioButton33" runat="server" GroupName="Behavior3Score" Value="Below" ReadOnly="true" />
                            <asp:Label runat="server" ID="read_Behavior3Score" style="display:none;"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align:center;vertical-align:middle;">
                         <span style="height:30px; float:left;">&nbsp;</span>
                        <p style="text-align:center">4</p>
                        </td>
                        <td style="text-align:center;vertical-align:middle;">
                            <p style="text-align:center">Makes People Better</p>
                        </td>
                        <td style="text-align:center;vertical-align:middle;"> 
                       <span style="margin-top:5px;  height:30px; float:left;">&nbsp;</span>
                            <asp:RadioButton ID="RadioButton34" runat="server" GroupName="Behavior4Score" Value="Exceed" ReadOnly="true"/>
                        </td>
                        <td style="text-align:center;vertical-align:middle;">
                       <span style="height:30px; float:left;">&nbsp;</span>
                            <asp:RadioButton ID="RadioButton35" runat="server" GroupName="Behavior4Score" Value="At" ReadOnly="true"/>
                        </td>
                        <td style="text-align:center;vertical-align:middle;">
                       <span style="height:30px; float:left;">&nbsp;</span>
                            <asp:RadioButton ID="RadioButton36" runat="server" GroupName="Behavior4Score" Value="Below" ReadOnly="true" />
                            <asp:Label runat="server" ID="read_Behavior4Score" style="display:none;"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align:center;vertical-align:middle;">
                         <span style="height:30px; float:left;">&nbsp;</span>
                        <p style="text-align:center">5</p>
                        </td>
                        <td style="text-align:center;vertical-align:middle;">
                            <p style="text-align:center">Integrative Thinker</p>
                        </td>
                        <td style="text-align:center;vertical-align:middle;"> 
                       <span style="height:30px; float:left;">&nbsp;</span>
                            <asp:RadioButton ID="RadioButton37" runat="server" GroupName="Behavior5Score" Value="Exceed" ReadOnly="true"/>
                        </td>
                        <td style="text-align:center;vertical-align:middle;">
                       <span style="height:30px; float:left;">&nbsp;</span>
                            <asp:RadioButton ID="RadioButton38" runat="server" GroupName="Behavior5Score" Value="At" ReadOnly="true"/>
                        </td>
                        <td style="text-align:center;vertical-align:middle;">
                       <span style="height:30px; float:left;">&nbsp;</span>
                            <asp:RadioButton ID="RadioButton39" runat="server" GroupName="Behavior5Score" Value="Below" ReadOnly="true" />
                            <asp:Label runat="server" ID="read_Behavior5Score" style="display:none;"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align:center;vertical-align:middle;">
                         <span style="height:30px; float:left;">&nbsp;</span>
                        <p style="text-align:center">6</p>
                        </td>
                        <td style="text-align:center;vertical-align:middle;">
                            <p style="text-align:center">Fosters Teamwork and Diversity</p>
                        </td>
                        <td style="text-align:center;vertical-align:middle;"> 
                       <span style="height:30px; float:left;">&nbsp;</span>
                            <asp:RadioButton ID="RadioButton40" runat="server" GroupName="Behavior6Score" Value="Exceed" ReadOnly="true"/>
                        </td>
                        <td style="text-align:center;vertical-align:middle;">
                       <span style="height:30px; float:left;">&nbsp;</span>
                            <asp:RadioButton ID="RadioButton41" runat="server" GroupName="Behavior6Score" Value="At" ReadOnly="true" />
                        </td>
                        <td style="text-align:center;vertical-align:middle;">
                       <span style="height:30px; float:left;">&nbsp;</span>
                            <asp:RadioButton ID="RadioButton42" runat="server" GroupName="Behavior6Score" Value="Below" ReadOnly="true" />
                            <asp:Label runat="server" ID="read_Behavior6Score" style="display:none;"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align:center;vertical-align:middle;">
                         <span style="height:30px; float:left;">&nbsp;</span>
                        <p style="text-align:center">7</p>
                        </td>
                        <td style="text-align:center;vertical-align:middle;">
                            <p style="text-align:center">Champions Change and 6 Sigma</p>
                        </td>
                        <td style="text-align:center;vertical-align:middle;"> 
                       <span style="height:30px; float:left;">&nbsp;</span>
                            <asp:RadioButton ID="RadioButton43" runat="server" GroupName="Behavior7Score" Value="Exceed" ReadOnly="true" />
                        </td>
                        <td style="text-align:center;vertical-align:middle;">
                       <span style="height:30px; float:left;">&nbsp;</span>
                            <asp:RadioButton ID="RadioButton44" runat="server" GroupName="Behavior7Score" Value="At" ReadOnly="true" />
                        </td>
                        <td style="text-align:center;vertical-align:middle;">
                       <span style="height:30px; float:left;">&nbsp;</span>
                            <asp:RadioButton ID="RadioButton45" runat="server" GroupName="Behavior7Score" Value="Below" ReadOnly="true"/>
                            <asp:Label runat="server" ID="read_Behavior7Score" style="display:none;"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align:center;vertical-align:middle;">
                         <span style="height:30px; float:left;">&nbsp;</span>
                        <p style="text-align:center">8</p>
                        </td>
                        <td style="text-align:center;vertical-align:middle;">
                            <p style="text-align:center">Intelligent Risk Taking</p>
                        </td>
                        <td style="text-align:center;vertical-align:middle;"> 
                       <span style="height:30px; float:left;">&nbsp;</span>
                            <asp:RadioButton ID="RadioButton46" runat="server" GroupName="Behavior8Score" Value="Exceed" ReadOnly="true" />
                        </td>
                        <td style="text-align:center;vertical-align:middle;">
                       <span style="height:30px; float:left;">&nbsp;</span>
                            <asp:RadioButton ID="RadioButton47" runat="server" GroupName="Behavior8Score" Value="At" ReadOnly="true" />
                        </td>
                        <td style="text-align:center;vertical-align:middle;">
                       <span style="height:30px; float:left;">&nbsp;</span>
                            <asp:RadioButton ID="RadioButton48" runat="server" GroupName="Behavior8Score" Value="Below" ReadOnly="true"/>
                            <asp:Label runat="server" ID="read_Behavior8Score" style="display:none;"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align:center;vertical-align:middle;">
                         <span style="height:30px; float:left;">&nbsp;</span>
                        <p style="text-align:center">9</p>
                        </td>
                        <td style="text-align:center;vertical-align:middle;">
                            <p style="text-align:center">Self-Aware/Learner</p>
                        </td>
                        <td style="text-align:center;vertical-align:middle;"> 
                       <span style="height:30px; float:left;">&nbsp;</span>
                            <asp:RadioButton ID="RadioButton49" runat="server" GroupName="Behavior9Score" Value="Exceed" ReadOnly="true" />
                        </td>
                        <td style="text-align:center;vertical-align:middle;">
                       <span style="height:30px; float:left;">&nbsp;</span>
                            <asp:RadioButton ID="RadioButton50" runat="server" GroupName="Behavior9Score" Value="At" ReadOnly="true" />
                        </td>
                        <td style="text-align:center;vertical-align:middle;">
                       <span style="height:30px; float:left;">&nbsp;</span>
                            <asp:RadioButton ID="RadioButton51" runat="server" GroupName="Behavior9Score" Value="Below" ReadOnly="true"/>
                            <asp:Label runat="server" ID="read_Behavior9Score" style="display:none;"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align:center;vertical-align:middle;">
                         <span style="height:30px; float:left;">&nbsp;</span>
                        <p style="text-align:center">10</p>
                        </td>
                        <td style="text-align:center;vertical-align:middle;">
                            <p style="text-align:center">Effective Communicator</p>
                        </td>
                        <td style="text-align:center;vertical-align:middle;"> 
                       <span style="height:30px; float:left;">&nbsp;</span>
                            <asp:RadioButton ID="RadioButton52" runat="server" GroupName="Behavior10Score" Value="Exceed" ReadOnly="true" />
                        </td>
                        <td style="text-align:center;vertical-align:middle;">
                       <span style="height:30px; float:left;">&nbsp;</span>
                            <asp:RadioButton ID="RadioButton53" runat="server" GroupName="Behavior10Score" Value="At" ReadOnly="true" />
                        </td>
                        <td style="text-align:center;vertical-align:middle;">
                       <span style="height:30px; float:left;">&nbsp;</span>
                            <asp:RadioButton ID="RadioButton54" runat="server" GroupName="Behavior10Score" Value="Below" ReadOnly="true"/>
                            <asp:Label runat="server" ID="read_Behavior10Score" style="display:none;"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align:center;vertical-align:middle;">
                         <span style="height:30px; float:left;">&nbsp;</span>
                        <p style="text-align:center">11</p>
                        </td>
                        <td style="text-align:center;vertical-align:middle;">
                            <p style="text-align:center">Growth and Customer Focus</p>
                        </td>
                        <td style="text-align:center;vertical-align:middle;"> 
                       <span style="height:30px; float:left;">&nbsp;</span>
                            <asp:RadioButton ID="RadioButton55" runat="server" GroupName="Behavior11Score" Value="Exceed" ReadOnly="true"/>
                        </td>
                        <td style="text-align:center;vertical-align:middle;">
                       <span style="height:30px; float:left;">&nbsp;</span>
                            <asp:RadioButton ID="RadioButton56" runat="server" GroupName="Behavior11Score" Value="At" ReadOnly="true"/>
                        </td>
                        <td style="text-align:center;vertical-align:middle;">
                       <span style="height:30px; float:left;">&nbsp;</span>
                            <asp:RadioButton ID="RadioButton57" runat="server" GroupName="Behavior11Score" Value="Below" ReadOnly="true" />
                            <asp:Label runat="server" ID="read_Behavior11Score" style="display:none;"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align:center;vertical-align:middle;">
                         <span style="height:30px; float:left;">&nbsp;</span>
                        <p style="text-align:center">12</p>
                        </td>
                        <td style="text-align:center;vertical-align:middle;">
                            <p style="text-align:center">Technical or Functional Excellence</p>
                        </td>
                        <td style="text-align:center;vertical-align:middle;"> 
                       <span style="height:30px; float:left;">&nbsp;</span>
                            <asp:RadioButton ID="RadioButton58" runat="server" GroupName="Behavior12Score" Value="Exceed" />
                        </td>
                        <td style="text-align:center;vertical-align:middle;">
                       <span style="height:30px; float:left;">&nbsp;</span>
                            <asp:RadioButton ID="RadioButton59" runat="server" GroupName="Behavior12Score" Value="At" />
                        </td>
                        <td style="text-align:center;vertical-align:middle;">
                       <span style="height:30px; float:left;">&nbsp;</span>
                            <asp:RadioButton ID="RadioButton60" runat="server" GroupName="Behavior12Score" Value="Below" />
                            <asp:Label runat="server" ID="read_Behavior12Score" style="display:none;"></asp:Label>
                        </td>
                    </tr>
                </table>


                                  <table class="table table-condensed table-bordered" id="behaviortable2">
                    <tr>
                        <th width="4%">
                            <p style="text-align:center" >No</p>
                        </th>
                         <th width="40%">
                            <p style="text-align:center">Behaviors</p>
                        </th>
                         <th width="14%">
                            <p style="text-align:center" >Always</p>
                        </th>
                        <th width="14%">
                            <p style="text-align:center">Frequently</p>
                        </th>
                         <th width="14%">
                            <p style="text-align:center">Sometimes</p>
                        </th>
                          <th width="14%">
                            <p style="text-align:center">Rarely</p>
                        </th>
                    </tr>
                    <tr>
                        <td style="text-align:center;vertical-align:middle;">
                         <span style="background:red;height:30px; float:left;">&nbsp;</span>
                        <p style="text-align:center">1</p>
                        </td>
                        <td style="text-align:center;vertical-align:middle;">
                            <p style="text-align:center">Have a Passion for Winning</p>
                        </td>
                        <td style="text-align:center;vertical-align:middle;"> 
                       <span style="height:30px; float:left;">&nbsp;</span>
                            <asp:RadioButton ID="RadioButton101" runat="server" GroupName="Behavior1Score" Value="Exceed"   Enabled="false"  />
                        </td>
                        <td style="text-align:center;vertical-align:middle;">
                       <span style="height:30px; float:left;">&nbsp;</span>
                            <asp:RadioButton ID="RadioButton102" runat="server" GroupName="Behavior1Score" Value="At"  Enabled="false"/>
                        </td>
                        <td style="text-align:center;vertical-align:middle;">
                       <span style="height:30px; float:left;">&nbsp;</span>
                            <asp:RadioButton ID="RadioButton103" runat="server" GroupName="Behavior1Score" Value="Below"  Enabled="false"/>
                        </td>
                         <td style="text-align:center;vertical-align:middle;">
                       <span style="height:30px; float:left;">&nbsp;</span>
                            <asp:RadioButton ID="RadioButton68" runat="server" GroupName="Behavior1Score" Value="Below"  Enabled="false"/>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align:center;vertical-align:middle;">
                         <span style=" background:red;height:30px; float:left;">&nbsp;</span>
                        <p style="text-align:center">2</p>
                        </td>
                        <td style="text-align:center;vertical-align:middle;">
                            <p style="text-align:center">Be a Zealot for Growth</p>
                        </td>
                        <td style="text-align:center;vertical-align:middle;"> 
                       <span style="height:30px; float:left;">&nbsp;</span>
                            <asp:RadioButton ID="RadioButton61" runat="server" GroupName="Behavior2Score" Value="Exceed"  Enabled="false"/>
                        </td>
                        <td style="text-align:center;vertical-align:middle;">
                       <span style="height:30px; float:left;">&nbsp;</span>
                            <asp:RadioButton ID="RadioButton62" runat="server" GroupName="Behavior2Score" Value="At"   Enabled="false"/>
                        </td>
                        <td style="text-align:center;vertical-align:middle;">
                       <span style="height:30px; float:left;">&nbsp;</span>
                            <asp:RadioButton ID="RadioButton63" runat="server" GroupName="Behavior2Score" Value="Below"  Enabled="false" />
                        </td>
                        <td style="text-align:center;vertical-align:middle;">
                       <span style="height:30px; float:left;">&nbsp;</span>
                            <asp:RadioButton ID="RadioButton67" runat="server" GroupName="Behavior2Score" Value="Below"  Enabled="false" />
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align:center;vertical-align:middle;">
                         <span style="background:red;height:30px; float:left;">&nbsp;</span>
                        <p style="text-align:center">3</p>
                        </td>
                        <td style="text-align:center;vertical-align:middle;">
                            <p style="text-align:center">Think Big … Then Make It Happen</p>
                        </td>
                        <td style="text-align:center;vertical-align:middle;"> 
                       <span style="height:30px; float:left;">&nbsp;</span>
                            <asp:RadioButton ID="RadioButton64" runat="server" GroupName="Behavior3Score" Value="Exceed"   Enabled="false" />
                        </td>
                        <td style="text-align:center;vertical-align:middle;">
                       <span style="height:30px; float:left;">&nbsp;</span>
                            <asp:RadioButton ID="RadioButton65" runat="server" GroupName="Behavior3Score" Value="At"   Enabled="false"/>
                        </td>
                        <td style="text-align:center;vertical-align:middle;">
                       <span style="height:30px; float:left;">&nbsp;</span>
                            <asp:RadioButton ID="RadioButton66" runat="server" GroupName="Behavior3Score" Value="Below"   Enabled="false" />
                        </td>
                          <td style="text-align:center;vertical-align:middle;">
                       <span style="height:30px; float:left;">&nbsp;</span>
                            <asp:RadioButton ID="RadioButton671" runat="server" GroupName="Behavior3Score" Value="Below"   Enabled="false" />
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align:center;vertical-align:middle;">
                         <span style=" background:red;height:30px; float:left;">&nbsp;</span>
                        <p style="text-align:center">4</p>
                        </td>
                        <td style="text-align:center;vertical-align:middle;">
                            <p style="text-align:center">Act with Urgency</p>
                        </td>
                        <td style="text-align:center;vertical-align:middle;"> 
                       <span style="height:30px; float:left;">&nbsp;</span>
                            <asp:RadioButton ID="RadioButton681" runat="server" GroupName="Behavior4Score" Value="Exceed"   Enabled="false"/>
                        </td>
                        <td style="text-align:center;vertical-align:middle;">
                       <span style="height:30px; float:left;">&nbsp;</span>
                            <asp:RadioButton ID="RadioButton69" runat="server" GroupName="Behavior4Score" Value="At"   Enabled="false"/>
                        </td>
                        <td style="text-align:center;vertical-align:middle;">
                       <span style="height:30px; float:left;">&nbsp;</span>
                            <asp:RadioButton ID="RadioButton70" runat="server" GroupName="Behavior4Score" Value="Below"   Enabled="false"/>
                        </td>
                        <td style="text-align:center;vertical-align:middle;">
                       <span style="height:30px; float:left;">&nbsp;</span>
                            <asp:RadioButton ID="RadioButton71" runat="server" GroupName="Behavior4Score" Value="Below"   Enabled="false"/>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align:center;vertical-align:middle;">
                         <span style=" background:red;height:30px; float:left;">&nbsp;</span>
                        <p style="text-align:center">5</p>
                        </td>
                        <td style="text-align:center;vertical-align:middle;">
                            <p style="text-align:center">Be Courageous</p>
                        </td>
                        <td style="text-align:center;vertical-align:middle;"> 
                       <span style="height:30px; float:left;">&nbsp;</span>
                            <asp:RadioButton ID="RadioButton72" runat="server" GroupName="Behavior5Score" Value="Exceed"   Enabled="false" />
                        </td>
                        <td style="text-align:center;vertical-align:middle;">
                       <span style="height:30px; float:left;">&nbsp;</span>
                            <asp:RadioButton ID="RadioButton73" runat="server" GroupName="Behavior5Score" Value="At"  Enabled="false"/>
                        </td>
                        <td style="text-align:center;vertical-align:middle;">
                       <span style="height:30px; float:left;">&nbsp;</span>
                            <asp:RadioButton ID="RadioButton74" runat="server" GroupName="Behavior5Score" Value="Below"   Enabled="false" />
                        </td>

                        <td style="text-align:center;vertical-align:middle;">
                       <span style="height:30px; float:left;">&nbsp;</span>
                            <asp:RadioButton ID="RadioButton75" runat="server" GroupName="Behavior5Score" Value="Below"  Enabled="false" />
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align:center;vertical-align:middle;">
                         <span style=" background:red;height:30px; float:left;">&nbsp;</span>
                        <p style="text-align:center">6</p>
                        </td>
                        <td style="text-align:center;vertical-align:middle;">
                            <p style="text-align:center">Go Beyond</p>
                        </td>
                        <td style="text-align:center;vertical-align:middle;"> 
                       <span style="height:30px; float:left;">&nbsp;</span>
                            <asp:RadioButton ID="RadioButton76" runat="server" GroupName="Behavior6Score" Value="Exceed"   Enabled="false" />
                        </td>
                        <td style="text-align:center;vertical-align:middle;">
                       <span style="height:30px; float:left;">&nbsp;</span>
                            <asp:RadioButton ID="RadioButton77" runat="server" GroupName="Behavior6Score" Value="At"   Enabled="false" />
                        </td>
                        <td style="text-align:center;vertical-align:middle;">
                       <span style="height:30px; float:left;">&nbsp;</span>
                            <asp:RadioButton ID="RadioButton78" runat="server" GroupName="Behavior6Score" Value="Below"   Enabled="false"/>
                        </td>

                         <td style="text-align:center;vertical-align:middle;">
                       <span style="height:30px; float:left;">&nbsp;</span>
                            <asp:RadioButton ID="RadioButton79" runat="server" GroupName="Behavior6Score" Value="Below"   Enabled="false" />
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align:center;vertical-align:middle;">
                         <span style=" background:red;height:30px; float:left;">&nbsp;</span>
                        <p style="text-align:center">7</p>
                        </td>
                        <td style="text-align:center;vertical-align:middle;">
                            <p style="text-align:center">Inspire Greatness</p>
                        </td>
                        <td style="text-align:center;vertical-align:middle;"> 
                       <span style="height:30px; float:left;">&nbsp;</span>
                            <asp:RadioButton ID="RadioButton80" runat="server" GroupName="Behavior7Score" Value="Exceed"  Enabled="false"/>
                        </td>
                        <td style="text-align:center;vertical-align:middle;">
                       <span style="height:30px; float:left;">&nbsp;</span>
                            <asp:RadioButton ID="RadioButton81" runat="server" GroupName="Behavior7Score" Value="At"  Enabled="false" />
                        </td>
                        <td style="text-align:center;vertical-align:middle;">
                       <span style="height:30px; float:left;">&nbsp;</span>
                            <asp:RadioButton ID="RadioButton82" runat="server" GroupName="Behavior7Score" Value="Below"  Enabled="false"/>
                        </td>

                          <td style="text-align:center;vertical-align:middle;">
                       <span style="height:30px; float:left;">&nbsp;</span>
                            <asp:RadioButton ID="RadioButton83" runat="server" GroupName="Behavior7Score" Value="Below"  Enabled="false"/>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align:center;vertical-align:middle;">
                         <span style=" background:red;height:30px; float:left;">&nbsp;</span>
                        <p style="text-align:center">8</p>
                        </td>
                        <td style="text-align:center;vertical-align:middle;">
                            <p style="text-align:center">Become Your Best</p>
                        </td>
                        <td style="text-align:center;vertical-align:middle;"> 
                       <span style="height:30px; float:left;">&nbsp;</span>
                            <asp:RadioButton ID="RadioButton84" runat="server" GroupName="Behavior8Score" Value="Exceed"  Enabled="false"/>
                        </td>
                        <td style="text-align:center;vertical-align:middle;">
                       <span style="height:30px; float:left;">&nbsp;</span>
                            <asp:RadioButton ID="RadioButton85" runat="server" GroupName="Behavior8Score" Value="At"   Enabled="false"/>
                        </td>
                        <td style="text-align:center;vertical-align:middle;">
                       <span style="height:30px; float:left;">&nbsp;</span>
                            <asp:RadioButton ID="RadioButton86" runat="server" GroupName="Behavior8Score" Value="Below"  Enabled="false" />
                        </td>
                        <td style="text-align:center;vertical-align:middle;">
                       <span style="height:30px; float:left;">&nbsp;</span>
                            <asp:RadioButton ID="RadioButton87" runat="server" GroupName="Behavior8Score" Value="Below"   Enabled="false" />
                        </td>
                    </tr>
                    
                    
                  
                </table>




                 <table class="table table-condensed table-bordered">
                    <tr>
                        <td class="td-label">
                         <span style="height:30px; float:left;">&nbsp;</span>
                        <p style="text-align:center">Describe behaviros employees demonstrates well</p>
                        </td>
                        <td class="td-content" colspan="7" >
                            <asp:Label ID="read_DBEDW" runat="server"></asp:Label>
                        </td>
                    </tr>
                </table>
                <table class="table table-condensed table-bordered">
                    <tr>
                        <td class="td-label">
                         <span style="height:30px; float:left;">&nbsp;</span>
                        <p style="text-align:center">Describe behaviors employee can improve</p>
                        </td>
                        <td class="td-content" colspan="7" >
                            <asp:Label ID="read_DBECI" runat="server"></asp:Label>
                        </td>
                    </tr>
                </table>
                <table class="table table-condensed table-bordered">
                    <tr>
                        <td class="td-label">
                         <span style="height:30px; float:left;">&nbsp;</span>
                        <p style="text-align:center">9 Block rating</p>
                        </td>
                        <td class="td-content" colspan="7" >
                            <asp:Label ID="read_BRating" runat="server"></asp:Label>
                        </td>
                    </tr>
                   <%-- <tr>
                        <td class="td-label">
                         <span style="height:30px; float:left;">&nbsp;</span>
                        <p style="text-align:center"></p>
                        </td>
                        <td class="td-content" colspan="7" >
                            <asp:TextBox ID="TextBox1" runat="server" CssClass="validate[required]" ></asp:TextBox>
                        </td>
                    </tr>--%>
                </table>
                <table class="table table-condensed table-bordered">
                    <tr>
                        <td class="td-label">
                         <span style="height:30px; float:left;">&nbsp;</span>
                        <p style="text-align:center">Overall Summary</p>
                        </td>
                        <td class="td-content" colspan="7" >
                            <asp:Label ID="read_OverallSummary" runat="server"></asp:Label>
                        </td>
                    </tr>
                </table>
                <table class="table table-condensed table-bordered">
                    <tr>
                        <td class="td-label">
                         <span style="height:30px; float:left;">&nbsp;</span>
                        <p style="text-align:center">Development Plan</p>
                        </td>
                        <td class="td-content" colspan="7" >
                            <asp:Label ID="read_DevelopmentPlan" runat="server"></asp:Label>
                        </td>
                    </tr>
                </table>
                <table class="table table-condensed table-bordered">
                    <tr>
                        <td class="td-label">
                         <span style="height:30px; float:left;">&nbsp;</span>
                        <p style="text-align:center">Potential Next Moves</p>
                        </td>
                        <td class="td-content" colspan="7" >
                            <asp:Label ID="read_PNMoves" runat="server" ></asp:Label>
                        </td>
                    </tr>
                </table>
                <p style="font-weight:bold;">Second Level Manager Feedback（"<span style=" background:red;">&nbsp;</span>" must write）</p>
                <table class="table table-condensed table-bordered">
                    <tr>
                        <td class="td-label" style="vertical-align:middle;">
                         <span style="background:red;height:30px; float:left;">&nbsp;</span>
                        <p style="text-align:center">Feedback</p>
                        </td>
                        <td class="td-content" colspan="7" >
                            <asp:TextBox ID="fld_Feedback" runat="server" Rows="3" TextMode="MultiLine" CssClass="validate[required]" Width="98%"></asp:TextBox>
                        </td>
                    </tr>
                </table>
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
                                <input type="button"  class="btn" value="Submit" onclick="submitPageReview('0')" />
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


