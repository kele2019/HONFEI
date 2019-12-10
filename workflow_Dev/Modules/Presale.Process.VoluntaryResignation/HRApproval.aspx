<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="HRApproval.aspx.cs" Inherits="Presale.Process.VoluntaryResignation.HRApproval" %>

<%@ Register Src="/Modules/Ultimus.UWF.Form.ProcessControl/UserInfo.ascx" TagName="UserInfo" TagPrefix="ui" %>
<%@ Register Src="/Modules/Ultimus.UWF.Form.ProcessControl/ApprovalHistory.ascx" TagName="ApprovalHistory" TagPrefix="ah" %>
<%@ Register Src="/Modules/Ultimus.UWF.Form.ProcessControl/Attachments.ascx" TagName="Attachments" TagPrefix="attach" %>
<%@ Register Src="/Modules/Ultimus.UWF.Form.ProcessControl/ButtonList.ascx" TagName="ButtonList" TagPrefix="btn" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta http-equiv="X-UA-Compatible"  content="IE=EmulateIE8" />
    <title>Voluntary Resignation Application Process</title>
    <script type="text/javascript" src="/Assets/js/common.js"></script>
    <script type="text/javascript">
        function showTime(obj) {
            var time = new Date(obj);
            var year = time.getFullYear();
            var month = time.getMonth() + 1;
            var date = time.getDate();
            return (year + "-" + month + "-" + date) == "NaN-NaN-NaN" ? "" : (year + "-" + month + "-" + date);
        }
        $(document).ready(function () {
            $("#read_HireDate").text(showTime($("#read_HireDate").text()));
            $("#read_ResignationDate").text(showTime($("#read_ResignationDate").text()));
            $("#read_LWDate").text(showTime($("#read_LWDate").text()));
        });
    </script>
</head>
<body>
    <form id="form1" runat="server">
     <div id="myDiv" class="container">
            <div class="row">
                <ui:userinfo id="UserInfo1" processtitle="Voluntary Resignation Application Process" processprefix="VR" tablename="PROC_VoluntaryResignation"
                    runat="server"  ></ui:userinfo>
                <asp:TextBox runat="server" ID="fld_UserPost" style="display:none;"></asp:TextBox>
            </div>
            <div class="row">
                <p style="font-weight:bold;">Employee Voluntary Resignation Description</p>
                <table class="table table-condensed table-bordered">
                    <tr>
                        <td class="td-label">
                         <span style="height:30px; float:left;">&nbsp;</span>
                        <p style="text-align:center">Chinese Name</p>
                        </td>
                        <td class="td-content" colspan="3" >
                          <%--  <asp:Label runat="server" ID="ChineseName"></asp:Label>--%>
                            <asp:Label ID="read_ChineseName" runat="server"></asp:Label>
                        </td>
                        <td class="td-label"> 
                       <span style="margin-top:5px;  height:30px; float:left;">&nbsp;</span>
                        <p style="text-align:center">English Name</p>
                        </td>
                        <td class="td-content"  colspan="3" >
                            <asp:Label runat="server"  ID="read_EnglishName"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="td-label">
                         <span style=" height:30px; float:left;">&nbsp;</span>
                        <p style="text-align:center">ID #</p>
                        </td>
                        <td class="td-content" colspan="3" >
                            <asp:Label runat="server" ID="read_IDNumber"></asp:Label>
                        </td>
                        <td class="td-label"> 
                       <span style=" margin-top:5px;  height:30px; float:left;">&nbsp;</span>
                        <p style="text-align:center">Department</p>
                        </td>
                        <td class="td-content"  colspan="3" >
                            <asp:Label runat="server"  ID="read_ApplicantDepartment"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="td-label" >
                         <span style=" height:30px; float:left;">&nbsp;</span>
                        <p style="text-align:center">Location</p>
                        </td>
                        <td class="td-content" colspan="3" >
                            <asp:Label runat="server" ID="fld_ApplicantLocation"></asp:Label>
                        </td>
                        <td class="td-label"> 
                       <span style=" margin-top:5px;  height:30px; float:left;">&nbsp;</span>
                        <p style="text-align:center">Title</p>
                        </td>
                        <td class="td-content"  colspan="3" >
                            <asp:Label runat="server"  ID="read_ApplicantTitle"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="td-label" >
                         <span style="  height:30px; float:left;">&nbsp;</span>
                        <p style="text-align:center">Hire Date</p>
                        </td>
                        <td class="td-content" colspan="3" >
                            <asp:Label runat="server" ID="read_HireDate"></asp:Label>
                        </td>
                        <td class="td-label"> 
                       <span style=" margin-top:5px;  height:30px; float:left;">&nbsp;</span>
                        <p style="text-align:center">Resignation Date</p>
                        </td>
                        <td class="td-content"  colspan="3" >
                            <asp:Label runat="server"  ID="read_ResignationDate"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="td-label" >
                         <span style=" height:30px; float:left;">&nbsp;</span>
                        <p style="text-align:center">Reasons of Resignation</p>
                        </td>
                        <td class="td-content" colspan="7" >
                            <asp:Label runat="server" ID="read_Reason"></asp:Label>
                        </td>
                        
                    </tr>
                    <tr>
                        <td class="td-label">
                         <span style=" height:30px; float:left;">&nbsp;</span>
                        <p style="text-align:center">Job Handover of People</p>
                        </td>
                        <td class="td-content" colspan="3" >
                            <asp:Label runat="server" ID="read_JHOP"></asp:Label>
                        </td>
                        <td class="td-label"> 
                       <span style="margin-top:5px;  height:30px; float:left;">&nbsp;</span>
                        <p style="text-align:center">Last Working Day</p>
                        </td>
                        <td class="td-content"  colspan="3" >
                            <asp:Label runat="server"  ID="read_LWDate"></asp:Label>
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
        <btn:buttonlist id="ButtonList1" runat="server"></btn:buttonlist>
        <div style="display:none">
        <asp:TextBox runat="server" ID="fld_TRSummary"></asp:TextBox>
        <asp:HiddenField runat="server" ID="hdIncident" />
        <asp:HiddenField runat="server" ID="hdPrint" />
        </div>
    </form>
</body>
</html>


