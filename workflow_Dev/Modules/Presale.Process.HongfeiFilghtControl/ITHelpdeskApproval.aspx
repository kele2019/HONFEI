﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ITHelpdeskApproval.aspx.cs" Inherits="Presale.Process.HongfeiFilghtControl.ITHelpdeskApproval" %>
<%@ Register Src="/Modules/Ultimus.UWF.Form.ProcessControl/UserInfo.ascx" TagName="UserInfo" TagPrefix="ui" %>
<%@ Register Src="/Modules/Ultimus.UWF.Form.ProcessControl/ApprovalHistory.ascx" TagName="ApprovalHistory" TagPrefix="ah" %>
<%@ Register Src="/Modules/Ultimus.UWF.Form.ProcessControl/Attachments.ascx" TagName="Attachments" TagPrefix="attach" %>
<%@ Register Src="/Modules/Ultimus.UWF.Form.ProcessControl/ButtonList.ascx" TagName="ButtonList" TagPrefix="btn" %>
<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta http-equiv="X-UA-Compatible"  content="IE=EmulateIE8" />
    <title>IT Backup Request Process</title>
    <script type="text/javascript" src="/Assets/js/common.js"></script> 
    <script type="text/javascript">
        function submitPageReview(obj) {
            $("#ButtonList1_btnSubmit").click();
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
     <div id="myDiv" class="container">
            <div class="row">
                <ui:userinfo id="UserInfo1" processtitle="IT Backup Request Process" processprefix="ITBR" tablename="PROC_ITBackup"
                    runat="server"  ></ui:userinfo>
                <asp:Label runat="server" ID="read_UserDepartment" style="display:none;"></asp:Label>
            </div>
            <div class="row">
                <p style="font-weight:bold;">Section 1</p>
                <table class="table table-condensed table-bordered">
                    <%--<tr>
                        <td class="td-label">
                         <span style="height:30px; float:left;">&nbsp;</span>
                        <p style="text-align:center">BR</p>
                        </td>
                        <td class="td-content" colspan="3" >
                            <asp:Label runat="server" ID="read_BR"></asp:Label>
                        </td>
                        <td class="td-label">
                         <span style=" height:30px; float:left;">&nbsp;</span>
                        <p style="text-align:center">Phone</p>
                        </td>
                        <td class="td-content" colspan="3" >
                            <asp:Label runat="server" ID="read_Phone"></asp:Label>
                        </td>
                    </tr>--%>
                    <tr>
                        <td class="td-label"> 
                       <span style="height:30px; float:left;">&nbsp;</span>
                        <p style="text-align:center">Reason for the backup</p>
                        </td>
                        <td class="td-content"  colspan="7" >   
                            <asp:Label runat="server" ID="read_RFTB"></asp:Label>
                        </td>
                    </tr>
                    </table>
                    <p style="font-weight:bold;">Section 2</p>
                    <table class="table table-condensed table-bordered">
                    <tr>
                        <td class="td-label">
                         <span style="height:30px; float:left;">&nbsp;</span>
                        <p style="text-align:center">System/Host name</p>
                        </td>
                        <td class="td-content" colspan="3" >
                            <asp:Label runat="server" ID="read_SHostName"></asp:Label>
                        </td>
                        <td class="td-label">
                         <span style="height:30px; float:left;">&nbsp;</span>
                        <p style="text-align:center">System Classification</p>
                        </td>
                        <td class="td-content" colspan="3" >
                            <asp:Label runat="server" ID="read_SClassification"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="td-label">
                         <span style="height:30px; float:left;">&nbsp;</span>
                        <p style="text-align:center">Start/End dates</p>
                        </td>
                        <td class="td-content" colspan="7" >
                            <asp:Label runat="server" ID="read_SEDa"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="td-label" style="vertical-align:middle">
                         <span style="height:30px; float:left;">&nbsp;</span>
                        <p style="text-align:center">Retention Days</p>
                        </td>
                        <td class="td-content" colspan="7" >
                            <asp:Label runat="server" ID="read_RetentionDays"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="td-label">
                         <span style="height:30px; float:left;">&nbsp;</span>
                        <p style="text-align:center">Frequency</p>
                        
                        </td>
                        <td class="td-content" colspan="7" >
                            <asp:Label runat="server" ID="read_Frequency"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="td-label" style="vertical-align:middle">
                         <span style="height:30px; float:left;">&nbsp;</span>
                        <p style="text-align:center">Database/File system <br />details (type/version/Approx Size)</p>
                        
                        </td>
                        <td class="td-content" colspan="7" >
                            <asp:Label runat="server" ID="read_DFSD"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="td-label" style="vertical-align:middle">
                         <span style="height:30px; float:left;">&nbsp;</span>
                       <p style="text-align:center">Type of backup<br />(Full/incremental,online/offine,etc)</p>
                        </td>
                        <td class="td-content" colspan="7" >
                            <asp:Label runat="server" ID="read_TOB"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="td-label" style="vertical-align:middle">
                         <span style="height:30px; float:left;">&nbsp;</span>
                       <p style="text-align:center">Exclusions(If backup should<br /> exclude any directories/data)</p>
                       
                        </td>
                        <td class="td-content" colspan="7" >
                            <asp:Label runat="server" ID="read_Exclusions"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="td-label" style="vertical-align:middle">
                         <span style="height:30px; float:left;">&nbsp;</span>
                       <p style="text-align:center">Recommendation</p>
                       
                        </td>
                        <td class="td-content" colspan="7" >
                            <asp:Label runat="server" ID="read_Recommendation"></asp:Label>
                        </td>
                    </tr>
                 </table>
            </div>
            <div class="row" style="display:none;">
                <attach:attachments id="Attachments1" ReadOnly="true" runat="server"></attach:attachments>
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
