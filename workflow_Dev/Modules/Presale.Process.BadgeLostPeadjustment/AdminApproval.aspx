<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AdminApproval.aspx.cs" Inherits="Presale.Process.BadgeLostPeadjustment.AdminApproval" %>

<%@ Register Src="/Modules/Ultimus.UWF.Form.ProcessControl/UserInfo.ascx" TagName="UserInfo" TagPrefix="ui" %>
<%@ Register Src="/Modules/Ultimus.UWF.Form.ProcessControl/ApprovalHistory.ascx" TagName="ApprovalHistory" TagPrefix="ah" %>
<%@ Register Src="/Modules/Ultimus.UWF.Form.ProcessControl/Attachments.ascx" TagName="Attachments" TagPrefix="attach" %>
<%@ Register Src="/Modules/Ultimus.UWF.Form.ProcessControl/ButtonList.ascx" TagName="ButtonList" TagPrefix="btn" %>

<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
        <meta http-equiv="X-UA-Compatible"  content="IE=EmulateIE8" />
    <title>Badge Lost/Readjustment Application</title>
    <script type="text/javascript" src="/Assets/js/common.js"></script> 
    <script type="text/javascript">
        $(document).ready(function () {
            var ReasonType = $("#read_Reason").text();
            if (ReasonType == "Role change" || ReasonType == "Role Change / New Access") {
                $("#original").show();
                $("#current").show();
            }
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
                <ui:userinfo id="UserInfo1" processtitle="Badge Lost/Readjustment Application" processprefix="BLP" tablename="PROC_BadgeLostPeadjustment"
                    runat="server"  ></ui:userinfo>
            </div>
            <div class="row">
                <p style="font-weight:bold;" >Request require</p>
                <table class="table table-condensed table-bordered">
                    <tr>
                        <td class="td-label">
                         <span style=" height:30px; float:left;">&nbsp;</span>
                        <p style="text-align:center">Reason</p>
                        </td>
                        <td class="td-content" colspan="7" >
                            <asp:Label runat="server" ID="read_Reason"></asp:Label>
                        </td>
                    </tr>
                    <tr id="original" style="display:none">
                        <td class="td-label">
                         <span style=" height:30px; float:left;">&nbsp;</span>
                        <p style="text-align:center">Original role</p>
                        </td>
                        <td class="td-content" colspan="3" >
                            <asp:Label runat="server" ID="read_OriginalRole"></asp:Label>
                        </td>
                        <td class="td-label">
                         <span style=" height:30px; float:left;">&nbsp;</span>
                        <p style="text-align:center">Original Manager</p>
                        </td>
                        <td class="td-content" colspan="3" >
                            <asp:Label runat="server" ID="read_OriginalManager"></asp:Label>
                        </td>
                    </tr>
                    <tr id="current" style="display:none;">
                        <td class="td-label" >
                         <span style=" height:30px; float:left;">&nbsp;</span>
                        <p style="text-align:center">Current role</p>
                        </td>
                        <td class="td-content" colspan="3" >
                            <asp:Label runat="server" ID="read_CurrentRole"></asp:Label>
                        </td>
                        <td class="td-label" >
                         <span style=" height:30px; float:left;">&nbsp;</span>
                        <p style="text-align:center">Current Manager</p>
                        </td>
                        <td class="td-content" colspan="3" >
                            <asp:Label runat="server" ID="read_CurrentManager"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="td-label"> 
                       <span style=" height:30px; float:left;">&nbsp;</span>
                        <p style="text-align:center">Access</p>
                        </td>
                        <td class="td-content"  colspan="7" >
                            <asp:Label runat="server" ID="read_Access"></asp:Label>
                        </td>
                    </tr>
                     <tr>
                        <td class="td-label"> 
                       <span style=" height:30px; float:left;">&nbsp;</span>
                        <p style="text-align:center">Comments</p>
                        </td>
                        <td class="td-content"  colspan="7" >
                            <asp:Label runat="server" ID="read_Comments"></asp:Label>
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
        <div  style="display:none;">
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



