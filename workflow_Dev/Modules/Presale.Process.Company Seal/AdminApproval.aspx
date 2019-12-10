<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AdminApproval.aspx.cs" Inherits="Presale.Process.Company_Seal.AdminApproval" %>

<%@ Register Src="/Modules/Ultimus.UWF.Form.ProcessControl/UserInfo.ascx" TagName="UserInfo" TagPrefix="ui" %>
<%@ Register Src="/Modules/Ultimus.UWF.Form.ProcessControl/ApprovalHistory.ascx" TagName="ApprovalHistory" TagPrefix="ah" %>
<%@ Register Src="/Modules/Ultimus.UWF.Form.ProcessControl/Attachments.ascx" TagName="Attachments" TagPrefix="attach" %>
<%@ Register Src="/Modules/Ultimus.UWF.Form.ProcessControl/ButtonList.ascx" TagName="ButtonList" TagPrefix="btn" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
        <meta http-equiv="X-UA-Compatible"  content="IE=EmulateIE8" />
    <title>Company Sale Process</title>
    <script type="text/javascript" src="/Assets/js/common.js"></script>
     
</head>
<body>
    <form id="form1" runat="server">
     <div id="myDiv" class="container">
            <div class="row">
                <ui:userinfo id="UserInfo1" processtitle="Company Sale Process" processprefix="CS" tablename="PROC_CompanySeal"
                    runat="server"  ></ui:userinfo>
            </div>
            <div class="row">
                <table class="table table-condensed table-bordered">
                    <tr>
                        <td class="banner" colspan="4">Request require（"
                        <span style=" background:red;">&nbsp;</span>" must write） 
                        </td>
                    </tr>
                    <tr>
                        <td class="td-label"  style="width:100px">
                         <span style=" background:red;  height:30px; float:left;">&nbsp;</span>
                        Quantity：
                        </td>
                        <td class="td-content" colspan="3" >
                        <asp:TextBox runat="server" ID="fld_Quantity" Width="98%"  CssClass="validate[required]"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="td-label"> 
                       <span style=" background:red; margin-top:5px;  height:30px; float:left;">&nbsp;</span>
                        Purpose：
                        </td>
                        <td class="td-content"  colspan="3" >
                            <asp:TextBox runat="server"  ID="fld_Purpose" TextMode="MultiLine"  CssClass="validate[required]"   Width="98%"></asp:TextBox>
                        </td>
                    </tr>
                </table>
            </div>
            <div class="row">
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
