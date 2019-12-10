﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Approval.aspx.cs" Inherits="Presale.Process.HelpDeskRegistration.Approval" %>

<%@ Register Src="/Modules/Ultimus.UWF.Form.ProcessControl/UserInfo.ascx" TagName="UserInfo" TagPrefix="ui" %>
<%@ Register Src="/Modules/Ultimus.UWF.Form.ProcessControl/ApprovalHistory.ascx" TagName="ApprovalHistory" TagPrefix="ah" %>
<%@ Register Src="/Modules/Ultimus.UWF.Form.ProcessControl/Attachments.ascx" TagName="Attachments" TagPrefix="attach" %>
<%@ Register Src="/Modules/Ultimus.UWF.Form.ProcessControl/ButtonList.ascx" TagName="ButtonList" TagPrefix="btn" %>

<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta http-equiv="X-UA-Compatible"  content="IE=EmulateIE8" />
    <title>IT HelpDesk Registration Process</title>
    <script type="text/javascript" src="/Assets/js/common.js"></script> 
    <script type="text/javascript">
        function showTime(obj) {
            var time = new Date(obj.replace("-", "/"));
            var year = time.getFullYear();
            var month = time.getMonth() + 1;
            var date = time.getDate();
            var value = year + "-" + month + "-" + date
            return value == "NaN-NaN-NaN" ? " " : value;
        }
        $(document).ready(function () {
            $("#read_ProcessingTime").text(showTime($("#read_ProcessingTime").text()));
            $("#read_EndTime").text(showTime($("#read_EndTime").text()));
        });
    </script>
</head>
<body>
    <form id="form1" runat="server">
     <div id="myDiv" class="container">
            <div class="row">
                <ui:userinfo id="UserInfo1" processtitle="IT HelpDesk Registration Process" processprefix="ITHD" tablename="PROC_HelpDeskRegistration" 
                    runat="server"  ></ui:userinfo>
            </div>
            <div class="row">
                <p style="font-weight:bold;">Request require</p>
                <table class="table table-condensed table-bordered">
                    <tr>
                        <td class="td-label">
                             <span style="height:30px; float:left;">&nbsp;</span>
                            <p style="text-align:center;">Contact person</p>
                        </td>
                        <td class="td-content" colspan="3">
                            <asp:Label runat="server" ID="read_ContactPerson"></asp:Label>
                        </td>
                        <td class="td-label">
                             <span style="height:30px; float:left;">&nbsp;</span>
                            <p style="text-align:center;">Question type</p>
                        </td>
                        <td class="td-content" colspan="3">
                            <asp:Label runat="server" ID="read_QuestionType" ></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="td-label">
                             <span style="height:30px; float:left;">&nbsp;</span>
                            <p style="text-align:center;">Question describe</p>
                        </td>
                        <td class="td-content" colspan="7">
                            <asp:Label runat="server" ID="read_Description"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="td-label">
                             <span style="height:30px; float:left;">&nbsp;</span>
                            <p style="text-align:center;">Deal with people</p>
                        </td>
                        <td class="td-content" colspan="3">
                            <asp:Label runat="server" ID="read_DealWithPeople"></asp:Label>
                        </td>
                        <td class="td-label">
                             <span style="height:30px; float:left;">&nbsp;</span>
                            <p style="text-align:center;">Processing time</p>
                        </td>
                        <td class="td-content" colspan="3">
                            <asp:Label runat="server" ID="read_ProcessingTime"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="td-label">
                             <span style="height:30px; float:left;">&nbsp;</span>
                            <p style="text-align:center;">End time</p>
                        </td>
                        <td class="td-content" colspan="7">
                            <asp:Label runat="server" ID="read_EndTime"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="td-label" style="vertical-align:middle">
                             <span style="height:30px; float:left;">&nbsp;</span>
                            <p style="text-align:center;">Result</p>
                        </td>
                        <td class="td-content" colspan="7">
                            <asp:Label runat="server" ID="read_Result"></asp:Label>
                        </td>
                    </tr>
                </table>
            </div>
            <div class="row">
                <attach:attachments id="Attachments1" ReadOnly="true" runat="server"></attach:attachments>
            </div>
            <div class="row">
             <ah:approvalhistory id="ApprovalHistory1" showaction="true" runat="server"></ah:approvalhistory>
            </div>
        </div>
        <btn:buttonlist id="ButtonList1" runat="server"></btn:buttonlist>
        <div style="display:none">
        <asp:TextBox runat="server" ID="fld_TRSummary"></asp:TextBox>
        <asp:HiddenField runat="server" ID="hdIncident" />
        <asp:HiddenField runat="server" ID="hdPrint" />
        </div>
    </form>
</body>
</html>



