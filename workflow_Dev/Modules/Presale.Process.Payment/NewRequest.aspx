<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="NewRequest.aspx.cs" Inherits="Presale.Process.Payment.NewRequest" %>
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
        function vendor_onclick(obj) {
            var digStr = "dialogHeight:500px;dialogWidth:850px;";
            var ReturnValue = window.showModalDialog("./Vendor.aspx", "", digStr);
            if (ReturnValue != null) {
                var vendor = eval("(" + ReturnValue + ")");
                var VendorCode = vendor[0].VendorCode;
                $(obj).val(VendorCode);
                var VendorName = vendor[1].VendorName;
                var VendorDesp = vendor[2].VendorDesp;
                $("#fld_VendorName").val(VendorName + "-" + VendorDesp);
                var Currency = vendor[3].Currency;
                $("#fld_Currency").val(Currency);
//                var GroupNum = vendor[4].GroupNum;
//                $("#fld_Currency").val(GroupNum);
                var VendorBank = vendor[5].VendorBank;
                $("#fld_VendorBank").val(VendorBank);
                var DfiAccount = vendor[6].DfiAccount;
                $("#fld_BankAccount").val(DfiAccount);
            }
        }
        function beforeSubmit() {
            if ($("#fld_Currency").val().replace(/(^\s*)|(\s*$)/g, "") == "RMB") {
                $("#fld_DownpaymentAmountUSD").val((($("#fld_DownpaymentAmount").val() - 0) / ($("#fld_Rate").val() - 0)).toFixed(2));
            }
            if ($("#fld_Currency").val().replace(/(^\s*)|(\s*$)/g, "") == "USD") {
                $("#fld_DownpaymentAmountUSD").val(($("#fld_DownpaymentAmount").val() - 0).toFixed(2));
            }
            if ($("#UserInfo1_fld_APPLICANT").text() == "WANG Jianyuan(jianyuan.wang)") {
                $("#fld_USERPOST").val("DGM");
            }
            var summary = "Down Payment Process";
            $("#UserInfo1_fld_PROCESSSUMMARY").val(summary);
            return true;
        }
        $(document).ready(function () {
            if ($("#hdIncident").val() != "") {
                $("#ButtonList1_btnSubmit").val("Submit");
                $("#ButtonList1_btnBack").hide();
                $("#ButtonList1_btnReject").show();
            }
            if ($("#hdUrgeTask").val() == "Yes") {
                $("#ReturnBackTask").show();
            }
        });
        function beforeSave() {
            var summary = "Down Payment Process";
            $("#UserInfo1_fld_PROCESSSUMMARY").val(summary);
            $("#fld_TRSummary").val(summary);
            return true;
        }
        function costcenter_onclick(obj) {
            var digStr = "dialogHeight:500px;dialogWidth:850px;";
            var ReturnValue = window.showModalDialog("./CostCenter.aspx", "", digStr);
            if (ReturnValue != null) {
                var cosetcenterDetail = eval("(" + ReturnValue + ")");
                var CostCenter = cosetcenterDetail[0].cosetcenter;
                var Description = cosetcenterDetail[1].Description;
                $(obj).val(CostCenter + "-" + Description);
                $(obj).next().val(CostCenter);
            }
            if ($("#fld_CostCenterValue").val() == "50806200") {
                $("#fld_Project").val("EC919CA003");
            }
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
     <div id="myDiv" class="container">
            <div class="row">
                <ui:userinfo id="UserInfo1" processtitle="Down Payment Process" processprefix="FINDP" tablename="PROC_Payment"
                    runat="server"  ></ui:userinfo>
                <asp:TextBox runat="server" ID="fld_DGMLogin" style="display:none"></asp:TextBox>
                <asp:TextBox runat="server" ID="fld_GMLogin" style="display:none"></asp:TextBox>
                <asp:TextBox runat="server" ID="fld_CompareValue" value="1500.00" style="display:none"></asp:TextBox>
                <asp:TextBox runat="server" ID="fld_deptLogin" style="display:none"></asp:TextBox>
                <asp:TextBox runat="server" ID="fld_Rate" style="display:none;"></asp:TextBox>
                <asp:TextBox runat="server" ID="fld_ApplierLogin" style="display:none"></asp:TextBox>
            </div>
            <asp:TextBox ID="fld_USERPOST" runat="server" style="display:none;"></asp:TextBox>
            <div class="row">
                <p style="font-weight:bold;">Request require（"<span style=" background:red;">&nbsp;</span>" must write） </p>
                <table class="table table-condensed table-bordered">
                    <tr>
                        <td class="td-label"> 
                            <span style="background:red;height:30px; float:left;">&nbsp;</span>
                            <p style="text-align:center">Payment description</p>
                        </td>
                        <td class="td-content"  colspan="7" >
                            <asp:TextBox runat="server"  CssClass="validate[required]" ID="fld_PaymentDescription"  Width="95%"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="td-label">
                            <span style="background:red;height:30px; float:left;">&nbsp;</span>
                            <p style="text-align:center">Vendor Code</p>
                        </td>
                        <td class="td-content" colspan="3" >
                            <asp:TextBox runat="server" ID="fld_VendorCode" Width="95%" onfocus="this.blur()"  CssClass="validate[required]" onclick="vendor_onclick(this)"></asp:TextBox>
                        </td>
                        <td class="td-label">
                            <span style="height:30px; float:left;">&nbsp;</span>
                            <p style="text-align:center">Vendor Name</p>
                        </td>
                        <td class="td-content" colspan="3" >
                            <asp:TextBox runat="server" ID="fld_VendorName" onfocus="this.blur()" Width="95%"></asp:TextBox>
                        </td>
                        
                    </tr>
                    <tr>
                        <td class="td-label"> 
                            <span style="height:30px; float:left;">&nbsp;</span>
                            <p style="text-align:center">Contract Number</p>
                        </td>
                        <td class="td-content"  colspan="3" >
                            <asp:TextBox runat="server"  ID="fld_ContractNO" Width="95%"></asp:TextBox>
                        </td>
                         <td class="td-label">
                            <span style="height:30px; float:left;">&nbsp;</span>
                            <p style="text-align:center">PO Number</p>
                        </td>
                        <td class="td-content" colspan="3" >
                            <asp:TextBox runat="server" ID="fld_PONO" Width="95%"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="td-label">
                            <span style="height:30px; float:left;">&nbsp;</span>
                            <p style="text-align:center">Bank Account#</p>
                        </td>
                        <td class="td-content" colspan="3" >
                            <asp:TextBox runat="server" ID="fld_BankAccount" Width="95%" ></asp:TextBox>
                        </td>
                        <td class="td-label"> 
                            <span style="margin-top:5px;  height:30px; float:left;">&nbsp;</span>
                            <p style="text-align:center">Bank Name</p>
                        </td>
                        <td class="td-content"  colspan="3" >
                            <asp:TextBox runat="server"  ID="fld_VendorBank" onfocus="this.blur()"  Width="95%"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                       
                        <td class="td-label"> 
                            <span style="margin-top:5px;  height:30px; float:left;">&nbsp;</span>
                            <p style="text-align:center">Currency</p>
                        </td>
                        <td class="td-content"  colspan="3" >
                            <asp:TextBox runat="server"  ID="fld_Currency" onfocus="this.blur()" Width="95%"></asp:TextBox>
                        </td>
                       
                        <td class="td-label"> 
                            <span style="margin-top:5px;  height:30px; float:left;">&nbsp;</span>
                            <p style="text-align:center">PO Amount</p>
                        </td>
                        <td class="td-content"  colspan="3" >
                            <asp:TextBox runat="server"  ID="fld_POAmount" Width="95%"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <%--<td class="td-label">
                           <span style="background:red;height:30px; float:left;">&nbsp;</span>
                            <p style="text-align:center">Cost Center</p>
                        </td>
                         <td class="td-content" colspan="3" >
                            <asp:TextBox runat="server" ID="fld_CostCenterDisplay" Width="95%" onfocus="this.blur()" onclick="costcenter_onclick(this)"  CssClass="validate[required]"></asp:TextBox>
                            <asp:TextBox runat="server" ID="fld_CostCenterCode" style="display:none;"></asp:TextBox>
                        </td>--%>
                        <td class="td-label">
                            <span style="background:red;height:30px; float:left;">&nbsp;</span>
                            <p style="text-align:center">Downpayment Amount</p>
                        </td>
                        <td class="td-content" colspan="3" >
                            <asp:TextBox runat="server" ID="fld_DownpaymentAmount" Width="95%"  CssClass="validate[required]"></asp:TextBox>
                            <asp:TextBox runat="server" ID="fld_DownpaymentAmountUSD" style="display:none;"></asp:TextBox>
                        </td>

                         <td class="td-label">
                        <span style="height:30px;float:left">&nbsp;</span>
                        <p style="text-align:center; color:Red;">Emergency</p>
                    </td>
                    <td class="td-content" colspan="3">
                      <asp:CheckBox runat="server" ID="fld_Emergency" />
                    </td>
                    </tr>
                     <tr>
                        <td class="td-label" style="vertical-align:middle">
                            <span style=" background:red;  height:60px; float:left;">&nbsp;</span>
                            <p style="text-align:center">Please State the Down payment reason in below table</p>
                        </td>
                        <td class="td-content" colspan="7" style="vertical-align:middle">
                            <asp:TextBox runat="server" ID="fld_Reason" TextMode="MultiLine" Rows="5" Width="98%"  CssClass="validate[required]"></asp:TextBox>
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
              <div id="ReturnBackTask" style="display:none; text-align:center">
        <asp:Button runat="server" ID="btnRevoke"  Visible="false" CssClass="btn" Text="Revoke" 
                onclick="btnRevoke_Click" />
        </div>
        </div>
        <btn:buttonlist id="ButtonList1" runat="server"></btn:buttonlist>
        <div style="display:none">
        <asp:TextBox runat="server" ID="fld_TRSummary"></asp:TextBox>
        <asp:HiddenField runat="server" ID="hdIncident" />
        <asp:HiddenField runat="server" ID="hdPrint" />
         <asp:HiddenField runat="server" ID="hdUrgeTask" />
        </div>
    </form>
</body>
</html>


