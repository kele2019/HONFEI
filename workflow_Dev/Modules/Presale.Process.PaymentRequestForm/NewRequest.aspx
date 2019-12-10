<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="NewRequest.aspx.cs" Inherits="Presale.Process.PaymentRequestForm.NewRequest" %>
<%@ Register Src="/Modules/Ultimus.UWF.Form.ProcessControl/UserInfo.ascx" TagName="UserInfo" TagPrefix="ui" %>
<%@ Register Src="/Modules/Ultimus.UWF.Form.ProcessControl/ApprovalHistory.ascx" TagName="ApprovalHistory" TagPrefix="ah" %>
<%@ Register Src="/Modules/Ultimus.UWF.Form.ProcessControl/Attachments.ascx" TagName="Attachments" TagPrefix="attach" %>
<%@ Register Src="/Modules/Ultimus.UWF.Form.ProcessControl/ButtonList.ascx" TagName="ButtonList" TagPrefix="btn" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Payment Request Process</title>
    <script type="text/javascript" src="/Assets/js/common.js"></script>
   <script type="text/javascript">
       function getButtonCheck(obj,index) {
           var value = "";
           if ($(obj).attr("checked") && index == "1") {
               value = "Yes";
           }
           if ($(obj).attr("checked") && index == "2") {
               value = "No";
           }
           $(obj).parent().parent().next().children().val(value);
       }
       function ShowTime(obj, index) {
           var mintime = $(obj).parent().prev().find("input").val();
           var maxtime = $(obj).parent().next().find("input").val();
           var StartDate = "";
           var EndDate = "";
           if (index == 1 && maxtime != undefined) {
               EndDate = maxtime
           }
           if (mintime != undefined && index == 2) {
               StartDate = mintime;
           }
           if (index == 2) {
               WdatePicker({ dateFmt: 'yyyy-MM-dd' });
           }
           else {
               WdatePicker({ dateFmt: 'yyyy-MM-dd' });
           }
       }
       function beforeSubmit() {
           
           if ($("#fld_Currency").val().replace(/(^\s*)|(\s*$)/g, "") == "RMB") {
               $("#fld_totalamountofpaymentUSD").val((($("#fld_totalamountofpayment").val() - 0) / ($("#fld_Rate").val() - 0)).toFixed(2));
           }
           if ($("#fld_Currency").val().replace(/(^\s*)|(\s*$)/g, "") == "USD") {
               $("#fld_totalamountofpaymentUSD").val(($("#fld_totalamountofpayment").val() - 0).toFixed(2));
           }
           $("#Attachments1_txtMust").val("1");
           var summary = "Payment Request Process";
           $("#UserInfo1_fld_PROCESSSUMMARY").val(summary);
            
           return true;
       }
       function beforeSave() {
           var summary = "Payment Request Process";
           $("#UserInfo1_fld_PROCESSSUMMARY").val(summary);
           $("#fld_TRSummary").val(summary);
           return true;
       }
       $(document).ready(function () {

           $("#tabletbodyDetail").find("tr").each(function (i, Etr) {
               var taxDisplay = $(Etr).find("td").eq(5).find(".taxorg").next().val();
               $(Etr).find("td").eq(5).find(".taxorg").find("option:selected").text(taxDisplay);
               var value = $(Etr).find("td").eq(4).children().val();
               if (value == "Yes") {
                   $(Etr).find("td").eq(3).find(".Yes").children().attr("checked", true);
               }
               if (value == "No") {
                   $(Etr).find("td").eq(3).find(".No").children().attr("checked", true);
               }
           });
           if ($("#hdIncident").val() != "") {
               $("#ButtonList1_btnSubmit").val("Submit");
               $("#ButtonList1_btnBack").hide();
               $("#ButtonList1_btnReject").show();
           }
           if ($("#hdUrgeTask").val() == "Yes") {
               $("#ReturnBackTask").show();
           }
           totalAmount_onchange();
       });
       function amount_onblur(obj) {
           var amount = $(obj).parent().parent().find("td").eq(2).children().val() - 0;
           var tax = $(obj).parent().parent().find("td").eq(5).children().last().val() - 0;
           var TotalAmount = (amount * tax) - 0;
           $(obj).parent().parent().find("td").eq(6).children().eq(0).text(formatNumber(TotalAmount, 2, 1));
           $(obj).parent().parent().find("td").eq(6).children().eq(1).val(TotalAmount.toFixed(2));
           totalAmount_onchange(obj);
       }
       function totalAmount_onchange(obj) {
           var total = 0;
           $("#tabletbodyDetail").find("tr").each(function (i, Etr) {
               var amount = $(Etr).find("td").eq(6).children().last().val() - 0;
               total += amount;
           });
           var downPayment = $("#fld_DownPaymentAmount").val() - 0;
           $("#fld_DownPaymentAmount").val(downPayment.toFixed(2));
           var totalAmount = (total - downPayment) - 0;

           $("#lb_totalamountofpayment").text(formatNumber(totalAmount.toFixed(2), 2, 1));
           $("#fld_totalamountofpayment").val(totalAmount.toFixed(2));
           if ((totalAmount < 0) && obj != undefined) {
               if (!confirm("down payment value > total Inv Amount?")) {
                   $("#fld_DownPaymentAmount").val("0.00");
                   totalAmount_onchange();
               }
           }

//           if (totalAmount > 0) {
//               $("#lb_totalamountofpayment").text(formatNumber(totalAmount.toFixed(2), 2, 1));
//               $("#fld_totalamountofpayment").val(totalAmount.toFixed(2));
//           }
//           else {
//               $("#lb_totalamountofpayment").text("0.00");
//               $("#fld_totalamountofpayment").val("0.00");
//           }
       }
       function vendor_onclick(obj) {
           var digStr = "dialogHeight:700px;dialogWidth:850px;";
           var ReturnValue = window.showModalDialog("./Vendor.aspx", "", digStr);
           if (ReturnValue != null) {
               var vendor = eval("(" + ReturnValue + ")");
               var VendorCode = vendor[0].VendorCode;
               $(obj).val(VendorCode);
               var VendorName = vendor[1].VendorName;
               var VendorDesp = vendor[2].VendorDesp;
               $("#fld_vendorname").val(VendorName + "-" + VendorDesp);
               var Currency = vendor[3].Currency;
               $("#fld_Currency").val(Currency);
               var GroupNum = vendor[4].GroupNum;
               $("#fld_payterm").val(GroupNum);
               var VendorBank = vendor[5].VendorBank;
               $("#fld_vendorbank").val(VendorBank);
               var DfiAccount = vendor[6].DfiAccount;
               $("#fld_bankAccount").val(DfiAccount);
               var Swift = vendor[7].Swift;
               $("#fld_SwiftNumber").val(Swift);
           }
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
       }
       function tax_onchange(obj) {
           $(obj).next().val($(obj).find("option:selected").text());
           $(obj).next().next().val($(obj).val());
           amount_onblur(obj);
       }

       function showPurchaseOrder() {
           var digStr = "dialogHeight:500px;dialogWidth:850px;"
           var ReturnValue = window.showModalDialog("Purchase.aspx", null, digStr);
           if (ReturnValue != null) {
               var purchaseNo = eval("(" + ReturnValue + ")");
               // var havePapers1 = eval(havePapers);
               var value = $("#fld_PurchaseOrderNo").val();
               value += "," + purchaseNo[0].PurchaseNo;
               if (value.substr(0, 1) == ',')
                   value = value.substr(1);
               $("#fld_PurchaseOrderNo").val(value);
               var applicant = $("#Allpplicant").val();
               applicant += purchaseNo[1].Applicant;
               $("#Allpplicant").val(applicant);
           }
       }


   </script>
</head>
<body>
<form id="form1" runat="server">
    <div id="myDiv" class="container">
        <div class="row">
            <ui:userinfo id="UserInfo1" processtitle="Payment Request Process" processprefix="FINPAY" tablename="PROC_PaymentRequest" runat="server" tablenamedetail="PROC_PaymentRequest_DT"></ui:userinfo>
            <asp:TextBox runat="server" ID="fld_DGMLogin" style="display:none"></asp:TextBox>
            <asp:TextBox runat="server" ID="fld_GMLogin" style="display:none"></asp:TextBox>
            <asp:TextBox runat="server" ID="fld_CompareValue" value="1500.00" style="display:none"></asp:TextBox>
            <asp:TextBox runat="server" ID="fld_deptLogin" style="display:none"></asp:TextBox>
            <asp:TextBox runat="server" ID="fld_Rate" style="display:none;"></asp:TextBox>
            <asp:TextBox runat="server" ID="fld_totalamountofpaymentUSD" style="display:none;"></asp:TextBox>
            <asp:TextBox runat="server" ID="fld_deptName" style="display:none;"></asp:TextBox>
        </div>
        <div class="row">
            <p style="font-weight:bold">Request require（<span style=" background:red">&nbsp;</span> must write）</p>
            <table class="table table-condensed table-bordered">


             <tr>
                    <td class="td-label">
                 <%--   <span style=" background:red;height:30px; float:left;">&nbsp;</span>--%>
                        <p style="text-align:center">PUR. REQUEST NO</p>
                    </td>
                    <td class="td-content" colspan="7">
                        
                        <asp:TextBox runat="server" ID="fld_PurchaseOrderNo"  Width="92%" style="background-color:White;" onfocus="this.blur()"></asp:TextBox>
                        <input type="button" value="..." class="btn" onclick="return showPurchaseOrder()" />

                    </td>
                </tr>

                <tr>
                        <td class="td-label"> 
                            <span style="background:red;height:30px; float:left;">&nbsp;</span>
                            <p style="text-align:center">Payment description</p>
                        </td>
                        <td class="td-content"  colspan="7" >
                            <asp:TextBox runat="server" CssClass="validate[required]" ID="fld_PaymentDescription" Width="98%"></asp:TextBox>
                        </td>
                    </tr>
                <tr>
                    <td class="td-label">
                        <span style="background:red;height:30px;float:left">&nbsp;</span>
                        <p style="text-align:center" >Vendor Code</p>
                    </td>
                    <td class="td-content" colspan="3">
                        <asp:TextBox ID="fld_vendorcode" runat="server" CssClass="validate[required]" Width="95%" onfocus="this.blur()" onclick="vendor_onclick(this)"></asp:TextBox>
                    </td>
                    <td class="td-label">
                        <span style="height:30px;float:left">&nbsp;</span>
                        <p style="text-align:center">Vendor Name</p>
                    </td>
                    <td class="td-content" colspan="3">
                        <asp:TextBox ID="fld_vendorname" runat="server" Width="95%" onfocus="this.blur()" onclick="vendor_onclick(this)"></asp:TextBox>
                    </td>
                    
                </tr> 
                <tr>
                    
                    <td class="td-label">
                        <p style="text-align:center">Contract Number</p>
                    </td>
                    <td class="td-content" colspan="3">
                        <asp:TextBox ID="fld_Contract" runat="server" Width="95%"></asp:TextBox>
                    </td>
                    <td class="td-label">
                        <span style=" height:30px;float:left">&nbsp;</span>
                        <p style="text-align:center">PO Number</p>
                    </td>
                    <td class="td-content" colspan="3">
                        <asp:TextBox ID="fld_PO" runat="server" Width="95%"></asp:TextBox>
                    </td>
                </tr> 
                <tr>
                    <td class="td-label">
                        <p style="text-align:center">Bank Account#</p>
                    </td>
                    <td class="td-content" colspan="3">
                        <asp:TextBox ID="fld_bankAccount" runat="server" Width="95%"></asp:TextBox>
                    </td>
                    <td class="td-label">
                        <span style="height:30px;float:left">&nbsp;</span>
                        <p style="text-align:center">Vendor Bank</p>
                    </td>
                    <td class="td-content" colspan="3">
                        <asp:TextBox ID="fld_vendorbank" runat="server" Width="95%" onclick="vendor_onclick(this)"></asp:TextBox>
                    </td>
                    
                </tr> 
                <tr>
                    <td class="td-label">
                        <p style="text-align:center">Currency</p>
                    </td>
                    <td class="td-content" colspan="3">
                        <asp:TextBox ID="fld_Currency" runat="server" Width="95%"></asp:TextBox>
                    </td>
                     <td class="td-label">
                        <p style="text-align:center">Swift Number</p>
                    </td>
                    <td class="td-content" colspan="3">
                        <asp:TextBox ID="fld_SwiftNumber" runat="server" Width="95%"></asp:TextBox>
                    </td>
                </tr> 
                <tr>
                    <td class="td-label">
                        <span style=" height:30px;float:left">&nbsp;</span>
                        <p style="text-align:center">Payment Term</p>
                    </td>
                    <td class="td-content" colspan="3">
                        <asp:TextBox ID="fld_payterm" runat="server" Width="95%"></asp:TextBox>
                    </td>
                    <td class="td-label">
                        <span style="height:30px;float:left">&nbsp;</span>
                        <p style="text-align:center; color:Red;">CONFIDENTIAL</p>
                    </td>
                    <td class="td-content" colspan="3">
                      <asp:CheckBox runat="server" ID="fld_Emergency" />
                    </td>
                </tr>
                <tr>
                  <td class="td-label">
                        <span style="height:30px;float:left">&nbsp;</span>
                        <p style="text-align:center; color:Red;">EMERGENCY</p>
                    </td>
                    <td class="td-content" colspan="7">
                      <asp:CheckBox runat="server" ID="fld_EmergencyNew" />
                    </td>
                </tr> 
            </table>
            <table class="table table-condensed table-bordered">
                <tr>
                    <th width="14%">Inv#</th>
                    <th width="10%">Inv date</th>
                    <th width="14%">Amount</th>
                    <th width="14%">VAT</th>
                    <th width="17">Tax</th>
                    <th width="10%">Total Inv Amount</th>
                    <th width="14%">Cost Center</th>
                    <th width="9%"></th>
                </tr>
            <tbody id="tabletbodyDetail">
                <asp:Repeater runat="server" ID="fld_detail_PROC_PaymentRequest_DT" OnItemCommand="fld_detail_PROC_PaymentRequest_DT_ItemCommand" OnItemDataBound="fld_detail_PROC_PaymentRequest_DT_ItemDataBound">
                    <ItemTemplate>
                        <tr>
                            <td>
                                <asp:TextBox runat="server" ID="fld_InvNumer" Text='<%#Eval("InvNumer") %>' Width="86%"></asp:TextBox>
                            </td>
                            <td>
                                <asp:TextBox runat="server" ID="fld_InvDate" onclick="WdatePicker({readOnly:false,dateFmt: 'yyyy-MM-dd'})"  Text='<%# String.IsNullOrEmpty(Eval("InvDate").ToString())? "":DateTime.Parse(Eval("InvDate").ToString()).ToString("yyyy-MM-dd") %>'  Width="84%"></asp:TextBox>
                            </td>
                            <td>
                                <asp:TextBox runat="server" ID="fld_Amount" Text='<%#Eval("Amount") %>' onblur="amount_onblur(this)" Width="84%"></asp:TextBox>
                            </td>
                            <td class="td-content" style="text-align:center">
                                <asp:RadioButton runat="server" Class="Yes" GroupName="VAT" Text="Yes" onclick="getButtonCheck(this,1)" />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                <asp:RadioButton runat="server" Class="No" GroupName="VAT" Text="No" onclick="getButtonCheck(this,2)" />
                            </td>
                            <td style="display:none;">
                                <asp:TextBox runat="server" ID="fld_VAT" Text='<%#Eval("VAT") %>'></asp:TextBox>
                            </td>
                            <td>
                                <asp:DropDownList runat="server" ID="tax" class="taxorg" onchange="tax_onchange(this)" Width="90%">
                                    <asp:ListItem Selected="True" Value="1">--select--</asp:ListItem>
                                </asp:DropDownList>
                                <asp:TextBox runat="server" ID="fld_TaxDiaplay" value="--select--" Text='<%#Eval("TaxDiaplay") %>' style="display:none;"></asp:TextBox>
                                <asp:TextBox runat="server" ID="fld_Tax" Text='<%#Eval("Tax") %>' value="0.00" style="display:none;"></asp:TextBox>
                            </td>
                            <td>
                                <asp:Label runat="server" ID="lb_TotalAmount" Text='<%#Eval("TotalAmount") %>' ></asp:Label>
                                <asp:TextBox runat="server" ID="fld_TotalAmount" Text='<%#Eval("TotalAmount") %>' value="0.00" onfocus="this.blur()" Width="84%" style="display:none"></asp:TextBox>
                            </td>
                            <td>
                                <asp:TextBox runat="server" ID="fld_CostCenterDisplay" Text='<%#Eval("CostCenterDisplay") %>' onfocus="this.blur()" onclick="costcenter_onclick(this)" Width="84%"></asp:TextBox>
                                <asp:TextBox runat="server" ID="fld_CostCenterCode" Text='<%#Eval("CostCenterCode") %>' style="display:none"></asp:TextBox>
                            </td>
                            <td>
                                <asp:Button ID="btnDelete" runat="server" Text="delete" CssClass="btn" CommandName="del" ClientIDMode="Static" OnClientClick="return confirm('Confirm Del？')" />
                            </td>
                        </tr>
                    </ItemTemplate>
                </asp:Repeater>
            </tbody>
            <tr>
                <td colspan="8" style="text-align:left">
                    <asp:Button ID="Button1" runat="server" Text="add" CssClass="btn" CausesValidation="false" OnClick="btnAdd_Click"/>
                </td>
            </tr>  
                          <tr>
                          <th  colspan="5"> <p style="text-align:left">Down Payment Amount</p></th>
                            <td>
                                <asp:TextBox ID="fld_DownPaymentAmount" runat="server" onchange="totalAmount_onchange(this)" value="0.00" CssClass="validate[required]" Width="84%"></asp:TextBox>
                            </td>
                            <th colspan="2"></th>
                        </tr>
                        <tr>
                            <th colspan="5"><p style="text-align:left">The total amount of payment</p></th>
                            <td>
                            <asp:Label runat="server" ID="lb_totalamountofpayment" Text="0.00" CssClass="money"></asp:Label>
                                <asp:TextBox ID="fld_totalamountofpayment" value="0.00" runat="server" Width="84%" style="display:none"></asp:TextBox>
                            </td>
                            <th colspan="2"></th>
                        </tr>
                        <tr>
                        <th>Remark</th>
                        <td colspan="7" style="vertical-align:middle">
                        <asp:TextBox runat="server" ID="fld_Remark" TextMode="MultiLine" Rows="5" Width="98%"></asp:TextBox>
                        </td>
                        </tr>
            </table>
        </div>
        <div class="row"">
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