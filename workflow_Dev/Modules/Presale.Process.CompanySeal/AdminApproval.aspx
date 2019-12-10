﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AdminApproval.aspx.cs" Inherits="Presale.Process.CompanySeal.AdminApproval" %>
<%@ Register Src="/Modules/Ultimus.UWF.Form.ProcessControl/UserInfo.ascx" TagName="UserInfo" TagPrefix="ui" %>
<%@ Register Src="/Modules/Ultimus.UWF.Form.ProcessControl/ApprovalHistory.ascx" TagName="ApprovalHistory" TagPrefix="ah" %>
<%@ Register Src="/Modules/Ultimus.UWF.Form.ProcessControl/Attachments.ascx" TagName="Attachments" TagPrefix="attach" %>
<%@ Register Src="/Modules/Ultimus.UWF.Form.ProcessControl/ButtonList.ascx" TagName="ButtonList" TagPrefix="btn" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta http-equiv="X-UA-Compatible"  content="IE=EmulateIE8" />
    <title>Company Seal Request Process</title>
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
                <ui:userinfo id="UserInfo1" processtitle="Company Seal Request Process" processprefix="CS" tablename="PROC_CompanySeal"
                    runat="server"  ></ui:userinfo>
            </div>
            <div class="row">
                <p style="font-weight:bold;">Request require</p>
                <table class="table table-condensed table-bordered">
                    <tr>
                        <td class="td-label">
                         <span style=" height:30px; float:left;">&nbsp;</span>
                         <p style="text-align:center">Quantity</p>
                        </td>
                        <td class="td-content" colspan="3" >
                            <asp:Label runat="server" ID="read_Quantity" ></asp:Label>
                        </td>
                        <td class="td-label">
                         <span style="height:30px; float:left;">&nbsp;</span>
                         <p style="text-align:center">SealName</p>
                        </td>
                        <td class="td-content" colspan="3" >
                        <asp:Label runat="server" ID="read_SealName"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="td-label" style="vertical-align:middle"> 
                       <span style="margin-top:5px;  height:30px; float:left;">&nbsp;</span>
                         <p style="text-align:center">Purpose</p>
                        </td>
                        <td class="td-content"  colspan="7" >
                            <asp:Label runat="server" ID="read_Purpose" ></asp:Label>
                        </td>
                    </tr>
                </table>
            </div>
            <div class="row" style="display:block;">
                <attach:attachments id="Attachments1" Readonly="true" runat="server"></attach:attachments>
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
        <asp:HiddenField runat="server" ID="hdIncident" />
        <asp:HiddenField runat="server" ID="hdPrint"  Value="0" />
        </div>
    </form>
</body>
</html>
