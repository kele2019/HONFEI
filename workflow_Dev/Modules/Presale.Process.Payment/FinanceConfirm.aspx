<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="FinanceConfirm.aspx.cs" Inherits="Presale.Process.Payment.FinanceConfirm" %>

<%@ Register Src="/Modules/Ultimus.UWF.Form.ProcessControl/UserInfo.ascx" TagName="UserInfo" TagPrefix="ui" %>
<%@ Register Src="/Modules/Ultimus.UWF.Form.ProcessControl/ApprovalHistory.ascx" TagName="ApprovalHistory" TagPrefix="ah" %>
<%@ Register Src="/Modules/Ultimus.UWF.Form.ProcessControl/Attachments.ascx" TagName="Attachments" TagPrefix="attach" %>
<%@ Register Src="/Modules/Ultimus.UWF.Form.ProcessControl/ButtonList.ascx" TagName="ButtonList" TagPrefix="btn" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta http-equiv="X-UA-Compatible"  content="IE=EmulateIE8" />
    <title>Down Payment Process</title>
    <script type="text/javascript" src="/Assets/js/common.js"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            $("#ButtonList1_btnSubmit").val("Complete");
            if ($("#fld_Emergency").attr("checked"))
                $("#spanEmergency").text("Yes");
            else
                $("#spanEmergency").text("No");
        });
    </script>
</head>
<body>
    <form id="form1" runat="server">
     <div id="myDiv" class="container">
            <div class="row">
                <ui:userinfo id="UserInfo1" processtitle="Down Payment Process" processprefix="FINDP" tablename="PROC_Payment"
                    runat="server"  ></ui:userinfo>
            </div>
            <div class="row">
                <p style="font-weight:bold;">Request require</p>
                <table class="table table-condensed table-bordered">
                    <tr>
                        <td class="td-label"> 
                       <span style="margin-top:5px;  height:30px; float:left;">&nbsp;</span>
                        <p style="text-align:center">Payment description</p>
                        </td>
                        <td class="td-content"  colspan="7" >
                            <asp:Label runat="server"  ID="read_PaymentDescription"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="td-label">
                         <span style="height:30px; float:left;">&nbsp;</span>
                        <p style="text-align:center">Vendor Code</p>
                        </td>
                        <td class="td-content" colspan="3" >
                        <asp:Label runat="server" ID="read_VendorCode"></asp:Label>
                        </td>
                        <td class="td-label">
                         <span style="height:30px; float:left;">&nbsp;</span>
                        <p style="text-align:center">Vendor Name</p>
                        </td>
                        <td class="td-content" colspan="3" >
                        <asp:Label runat="server" ID="read_VendorName"></asp:Label>
                        </td>
                        
                    </tr>
                    <tr>
                        <td class="td-label"> 
                       <span style="margin-top:5px;  height:30px; float:left;">&nbsp;</span>
                        <p style="text-align:center">Contract Number</p>
                        </td>
                        <td class="td-content"  colspan="3" >
                            <asp:Label runat="server"  ID="read_ContractNO"></asp:Label>
                        </td>
                        <td class="td-label">
                         <span style="height:30px; float:left;">&nbsp;</span>
                        <p style="text-align:center">PO Number</p>
                        </td>
                        <td class="td-content" colspan="3" >
                        <asp:Label runat="server" ID="read_PONO"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="td-label">
                         <span style="height:30px; float:left;">&nbsp;</span>
                        <p style="text-align:center">Bank Account#</p>
                        </td>
                        <td class="td-content" colspan="3" >
                        <asp:Label runat="server" ID="read_BankAccount"></asp:Label>
                        </td>
                        <td class="td-label"> 
                       <span style="margin-top:5px;  height:30px; float:left;">&nbsp;</span>
                        <p style="text-align:center">Bank Name</p>
                        </td>
                        <td class="td-content"  colspan="3" >
                            <asp:Label runat="server"  ID="read_VendorBank"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        
                        <td class="td-label"> 
                       <span style="margin-top:5px;  height:30px; float:left;">&nbsp;</span>
                        <p style="text-align:center">Currency</p>
                        </td>
                        <td class="td-content"  colspan="3" >
                            <asp:Label runat="server"  ID="read_Currency"></asp:Label>
                        </td>
                        <td class="td-label"> 
                       <span style="margin-top:5px;  height:30px; float:left;">&nbsp;</span>
                        <p style="text-align:center">PO Amount</p>
                        </td>
                        <td class="td-content"  colspan="3" >
                            <asp:Label runat="server"  ID="read_POAmount"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <%--<td class="td-label">
                         <span style="height:30px; float:left;">&nbsp;</span>
                        <p style="text-align:center">Cost Center</p>
                        </td>
                        <td class="td-content" colspan="3" >
                        <asp:Label runat="server" ID="read_CostCenterDisplay"></asp:Label>
                        </td>--%>
                        <td class="td-label">
                         <span style="height:30px; float:left;">&nbsp;</span>
                        <p style="text-align:center">Downpayment Amount</p>
                        </td>
                        <td class="td-content" colspan="3" >
                        <asp:Label runat="server" ID="read_DownpaymentAmount"></asp:Label>
                        </td>
                          <td class="td-label">
                        <span style="height:30px;float:left">&nbsp;</span>
                       <p style="text-align:center; color:Red;">Emergency</p>
                    </td>
                         <td class="td-content"  colspan="3">
                         <asp:CheckBox runat="server" ID="fld_Emergency" style="display:none" />
                         <span id="spanEmergency"></span>
                    </td>
                    </tr>
                     <tr>
                        <td class="td-label" style="vertical-align:middle">
                         <span style="height:30px; float:left;">&nbsp;</span>
                        <p style="text-align:center">Please State the Down payment reason in below table</p>
                        </td>
                        <td class="td-content" colspan="7" style="vertical-align:middle">
                            <asp:Label runat="server" ID="read_Reason"></asp:Label>
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


