﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Approval.aspx.cs" Inherits="Presale.Process.PaymentRequestForm.Approval" %>
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

       $(document).ready(function () {
           $("#tabletbodyDetail").find("tr").each(function (i, Etr) {
               var value = $(Etr).find("td").eq(4).children().text();
               if (value == "Yes") {
                   $(Etr).find("td").eq(3).find(".Yes").children().attr("checked", true);
               }
               if (value == "No") {
                   $(Etr).find("td").eq(3).find(".No").children().attr("checked", true);
               }
              
           });
           if ($("#fld_Emergency").attr("checked"))
               $("#spanEmergency").text("Yes");
           else
               $("#spanEmergency").text("No");

           if ($("#fld_EmergencyNew").attr("checked"))
               $("#spanEmergencyNew").text("Yes");
           else
               $("#spanEmergencyNew").text("No");

           if ($("#hdIncident").val() != "") {
               $("#ButtonList1_btnSubmit").val("Submit");
               $("#ButtonList1_btnBack").hide();
               $("#ButtonList1_btnReject").show();
           }
           changepogr();
       });

       function changepogr() {
           var paymenttype = $("#read_PaymentType").text();
           if (paymenttype == "Payment without PO") {
               $("#trGR").show();
               $("#trPO").hide();
               $("#SAPPO").hide();
           }
           if (paymenttype == "Payment With PO") {
               $("#trGR").hide();
               $("#trPO").show();
               $("#SAPPO").show();

           }

       }
   </script>
</head>
<body>
    <form id="form1" runat="server">
        <div id="myDiv" class="container">
            <div class="row">
                <ui:userinfo id="UserInfo1" processtitle="Payment Request Process" processprefix="FINPAY" tablename="PROC_PaymentRequest"
                    runat="server" tablenamedetail="PROC_PaymentRequest_DT,PROC_PaymentRequestPO_DT"></ui:userinfo>
                <asp:TextBox runat="server" ID="fld_ApprovalUserPost" style="display:none;"></asp:TextBox>
            </div>
            <div class="row">
                <p style="font-weight:bold">Request require</p>
                <table class="table table-condensed table-bordered">

                 <tr>
                    <td class="td-label">
                 <p style="text-align:center">Payment Type</p>
                    </td>
                    <td  class="td-content" colspan="7">
                    <asp:Label runat="server" ID="read_PaymentType"></asp:Label>
                    </td>
                  </tr>

                 <tr style="display:none">
                    <td class="td-label">
                        <p style="text-align:center">PUR. REQUEST NO</p>
                    </td>
                    <td class="td-content" colspan="7">
                        <asp:Label runat="server" ID="read_PurchaseOrderNo"></asp:Label>
                    </td>
                </tr>

                <tr>
                    <td class="td-label">
                        <p style="text-align:center">Payment description</p>
                    </td>
                    <td class="td-content" colspan="7">
                        <asp:Label ID="read_PaymentDescription" runat="server"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td class="td-label">
                        <span style=" height:30px;float:left">&nbsp;</span>
                        <p style="text-align:center">Vendor Code</p>
                    </td>
                    <td class="td-content" colspan="3">
                        <asp:Label ID="read_vendorcode" runat="server"></asp:Label>
                    </td>
                    <td class="td-label">
                        <span style="height:30px;float:left">&nbsp;</span>
                        <p style="text-align:center">Vendor Name</p>
                    </td>
                    <td class="td-content" colspan="3">
                        <asp:Label ID="read_vendorname" runat="server"></asp:Label>
                    </td>
                    
                </tr> 
                <tr>
                    
                    <td class="td-label">
                        <p style="text-align:center">Contract Number</p>
                    </td>
                    <td class="td-content" colspan="3">
                        <asp:Label ID="read_Contract" runat="server"></asp:Label>
                    </td>

                      <td class="td-label">
                        <span style=" height:30px;float:left">&nbsp;</span>
                        <p style="text-align:center">Payment Term</p>
                    </td>
                    <td class="td-content" colspan="3">
                        <asp:Label ID="read_payterm" runat="server"></asp:Label>
                    </td>

                   
                </tr> 
                <tr>
                    <td class="td-label">
                        <p style="text-align:center">Bank Account#</p>
                    </td>
                    <td class="td-content" colspan="3">
                        <asp:Label ID="read_bankAccount" runat="server"></asp:Label>
                    </td>
                    <td class="td-label">
                        <span style="height:30px;float:left">&nbsp;</span>
                        <p style="text-align:center">Bank Name</p>
                    </td>
                    <td class="td-content" colspan="3">
                        <asp:Label ID="read_vendorbank" runat="server"></asp:Label>
                    </td>
                    
                </tr> 
                <tr>
                    <td class="td-label">
                        <p style="text-align:center">Currency</p>
                    </td>
                    <td class="td-content" colspan="3">
                        <asp:Label ID="read_Currency" runat="server"></asp:Label>
                    </td>
                     <td class="td-label">
                        <p style="text-align:center">Swift Number</p>
                    </td>
                    <td class="td-content" colspan="3">
                        <asp:Label ID="read_SwiftNumber" runat="server"></asp:Label>
                    </td>
                </tr> 
                <tr>
                   <td class="td-label">
                        <span style="height:30px;float:left">&nbsp;</span>
                       <p style="text-align:center; color:Red;">EMERGENCY</p>
                    </td>
                    <td class="td-content"  colspan="3">
                         <asp:CheckBox runat="server" ID="fld_EmergencyNew" style="display:none" />
                         <span id="spanEmergencyNew"></span>
                    </td>
                    <td class="td-label">
                        <span style="height:30px;float:left">&nbsp;</span>
                       <p style="text-align:center; color:Red;">CONFIDENTIAL</p>
                    </td>
                    <td class="td-content"  colspan="3">
                         <asp:CheckBox runat="server" ID="fld_Emergency" style="display:none" />
                         <span id="spanEmergency"></span>
                    </td>
                </tr>
                 <tr id="trGR" style="display:none">
                    <td class="td-label">
                        <p style="text-align:center">GR NO</p>
                    </td>
                    <td class="td-content" colspan="7">
                        <asp:Label runat="server" ID="read_GRNo" onprerender="read_GRNo_PreRender"  Width="92%"  ></asp:Label>
                    </td>
                </tr>
                <tr id="trPO">
                 <td    class="td-label">
                        <span style=" height:30px;float:left">&nbsp;</span>
                        <p style="text-align:center">PO Number</p>
                    </td>
                    <td class="td-content" colspan="7">
                        <asp:Label ID="read_PO" runat="server"></asp:Label>
                    </td>
                </tr> 
            </table>

               <table id="SAPPO"  class="table table-condensed table-bordered">
                <tr>
                    <th style="display:none"></th>
                    <th width="10%">PO No</th>
                    <th width="30%">PO Remark</th>
                    <th width="10%">GRNo</th>
                    <th width="10%">GR Total</th>
                    <th width="30%">GR Remark</th>
                </tr>
            <tbody id="Tbody1">
                <asp:Repeater runat="server" ID="read_detail_PROC_PaymentRequestPO_DT"   >
                    <ItemTemplate>
                        <tr>
                          <td style="display:none">
                           <asp:Label ID="read_FORMID" Text='<%#Eval("FORMID") %>' runat="server" Style="display: none" ></asp:Label>
                           <asp:Label ID="read_ROWID" Text='<%# Container.ItemIndex+1%>' runat="server"></asp:Label>
                        </td>
                        <td>
                        <asp:Label runat="server" ID="read_PONo" Text='<%#Eval("PONo") %>' Width="86%"></asp:Label>
                        </td>
                        <td>
                        <asp:Label runat="server" ID="read_PORemark" Text='<%#Eval("PORemark") %>' Width="86%"></asp:Label>
                        </td>
                          <td>
                        <asp:Label runat="server" ID="read_GRNo" Text='<%#Eval("GRNo") %>' Width="86%"></asp:Label>
                        </td>
                          <td>
                        <asp:Label runat="server" ID="read_GRTotal" Text='<%#Eval("GRTotal") %>' Width="86%"></asp:Label>
                        </td>
                            <td>
                        <asp:Label runat="server" ID="read_GRRemark" Text='<%#Eval("GRRemark") %>' Width="86%"></asp:Label>
                        </td>
                        </tr>
                        </ItemTemplate>
                        </asp:Repeater>
            </tbody>
            </table>


            <table class="table table-condensed table-bordered">
            <tr>
                <th width="15%">Inv#</th>
                <th width="15%">Inv date</th>
                <th width="15%">Amount</th>
                <th width="15%">VAT</th>
                <th width="10%">Tax</th>
                <th width="15%">Total Inv Amount</th>
                <th width="15%">Cost Center</th>
            </tr>
            <tbody id="tabletbodyDetail">
                <asp:Repeater runat="server" ID="fld_detail_PROC_PaymentRequest_DT">
                    <ItemTemplate>
                        <tr>
                            <td style="text-align:center">
                                <asp:Label runat="server" ID="read_InvNumer" Text='<%#Eval("InvNumer") %>'></asp:Label>
                            </td>
                            <td style="text-align:center">
                                <asp:Label runat="server" ID="read_InvDate" Text='<%# String.IsNullOrEmpty(Eval("InvDate").ToString())? "":DateTime.Parse(Eval("InvDate").ToString()).ToString("yyyy-MM-dd") %>'></asp:Label>
                            </td>
                            <td style="text-align:center">
                            <span class="money"><%#Eval("Amount") %></span>
                                <asp:Label runat="server" ID="read_Amount" Text='<%#Eval("Amount") %>' style="display:none"></asp:Label>
                            </td>
                            <td class="td-content" style="text-align:center">
                                <asp:RadioButton runat="server" ID="vat1" Class="Yes" GroupName="VAT" Text="Yes" disabled="true" /> &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                <asp:RadioButton runat="server" ID="var2" Class="No" GroupName="VAT" Text="No" disabled="true" />
                            </td>
                            <td style="display:none;">
                                <asp:Label runat="server" ID="read_VAT" Text='<%#Eval("VAT") %>'></asp:Label>
                            </td>
                            <td style="text-align:center">
                                <asp:Label runat="server" ID="read_TaxDiaplay" Text='<%#Eval("TaxDiaplay") %>'></asp:Label>
                                <asp:Label runat="server" ID="read_Tax" Text='<%#Eval("Tax") %>' style="display:none;"></asp:Label>
                            </td>
                            <td style="text-align:center">
                            <span class="money"><%#Eval("TotalAmount") %></span>
                                <asp:Label runat="server" ID="read_TotalAmount" Text='<%#Eval("TotalAmount") %>' style="display:none"></asp:Label>
                            </td>
                            <td style="text-align:center">
                                <asp:Label runat="server" ID="read_CostCenterDisplay" Text='<%#Eval("CostCenterDisplay") %>'></asp:Label>
                                <asp:Label runat="server" ID="read_CostCenterCode" Text='<%#Eval("CostCenterCode") %>' style="display:none"></asp:Label>
                            </td>
                        </tr>
                    </ItemTemplate>
                </asp:Repeater>
                </tbody>
              <tr>
                   <th colspan="5"><p style="text-align:left">Down Payment Amount</p></th>
                <td  style="text-align:center">
                 <asp:Label ID="read_DownPaymentAmount" CssClass="money" runat="server"></asp:Label>
                </td>
               <th></th>
            </tr>
            <tr>
                <th colspan="5"><p style="text-align:left">The total amount of payment</p></th>
                <td style="text-align:center">
                    <asp:Label ID="read_totalamountofpayment" CssClass="money" runat="server"></asp:Label>
                </td>
                <th></th>
            </tr>

             <tr>
             <th>Remark</th>
             <td colspan="6">
             <asp:Label runat="server" ID="read_Remark" ></asp:Label>
             </td>
             </tr>
         </table>
            </div>
            <div class="row">
                <attach:attachments id="Attachments1" Readonly="true" runat="server"></attach:attachments>
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
