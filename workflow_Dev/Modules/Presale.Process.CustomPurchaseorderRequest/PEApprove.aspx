﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PEApprove.aspx.cs" Inherits="Presale.Process.CustomPurchaseorderRequest.PEApprove" %>
 

  <%@ Register Src="/Modules/Ultimus.UWF.Form.ProcessControl/UserInfo.ascx" TagName="UserInfo" TagPrefix="ui" %>
<%@ Register Src="/Modules/Ultimus.UWF.Form.ProcessControl/ApprovalHistory.ascx" TagName="ApprovalHistory" TagPrefix="ah" %>
<%@ Register Src="/Modules/Ultimus.UWF.Form.ProcessControl/Attachments.ascx" TagName="Attachments" TagPrefix="attach" %>
<%@ Register Src="/Modules/Ultimus.UWF.Form.ProcessControl/ButtonList.ascx" TagName="ButtonList" TagPrefix="btn" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta http-equiv="X-UA-Compatible"  content="IE=EmulateIE8" />
    <title>Custom Purchase order review</title>
    <script type="text/javascript" src="/Assets/js/common.js"></script>
    <script type="text/javascript">

        function beforeSubmit() {
            return true;
        }

        $(document).ready(function () {
            selectproducttype();
        });
        function selectproducttype() {

            var pttyp = $("#read_Producttype").text();
            if (pttyp == "New products") {
                $("#ptype1").attr("checked", true);
            }
            if (pttyp == "Existing products") {
                $("#ptype2").attr("checked", true);
            }
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
     <div id="myDiv" class="container">
            <div class="row">
                <ui:userinfo id="UserInfo1" processtitle="Customer Purchase Order Review" processprefix="CPOR" tablename="PROC_CustomerPurchase"
                    runat="server"  ></ui:userinfo>
            </div>
            <div class="row">
                <p style="font-weight:bold;">Request require </p>
                <table class="table table-condensed table-bordered">
                    <tr>
                        <td class="td-label">
                         <p style="text-align:center">Program</p>
                        </td>
                        <td class="td-content"   >
                        <asp:Label runat="server" ID="read_Program"  ></asp:Label>
                        </td>
                        <td class="td-label">
                         <p style="text-align:center">Buyer</p>
                        </td>
                        <td class="td-content"  >
                        <asp:Label runat="server" ID="read_Buyer" ></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="td-label">
                          
                         <p style="text-align:center">Purchase Order No</p>
                        </td>
                        <td class="td-content"   >
                        <asp:Label runat="server" ID="read_PurchaseOrderNo"   ></asp:Label>
                        </td>
                        <td class="td-label">
                         <p style="text-align:center">Purchase Order Revision</p>
                        </td>
                        <td class="td-content"  >
                        <asp:Label runat="server" ID="read_PurchaseOrderrevision"  ></asp:Label>
                        </td>
                    </tr>
                    <tr>
                      <td class="td-label">
                         <p style="text-align:center">Products Type</p>

                        </td>
                      <td class="td-content"  colspan="3" >
                       <input type="radio" name="ptype" disabled="disabled" id="ptype1" onclick="selectproducttype('1')" />  New products
                       <br />
                       Review attached Customer PO and Provide the supporting documents as below:
                        <br />●Specify roles and method to different customer requirements
                        <br /> ●Opportunity and Risk Assessment
                        <br />●Quantify Customer Needs
                        <br />●Special requirements of products and services are determined
                       <br />
                       <input type="radio" name="ptype" disabled="disabled" id="ptype2" onclick="selectproducttype('2')" /> Existing products
                       <br />
                       Review attached Customer PO 
                        <asp:Label runat="server" ID="read_Producttype"  style="display:none" ></asp:Label>
                      </td>

                    </tr>
                </table>
            </div>

            <div class="row">
                <p style="font-weight:bold;">Review content  </p>
                <table class="table table-condensed table-bordered">
                    <tr>
                    <td class="td-label">
                         <p style="text-align:center">Technical and Quality Requirements</p>
                        </td>
                        <td class="td-content" colspan="3" >
                        <asp:Label runat="server" ID="read_Techicalquality"    ></asp:Label>
                        </td>
                    </tr>
                      <tr>
                    <td class="td-label">
                         <p style="text-align:center">Quantity And Delivery Schedule</p>
                        </td>
                        <td class="td-content" colspan="3" >
                        <asp:Label runat="server" ID="read_Quantitydelivery"   ></asp:Label>
                        </td>
                    </tr>

                       <tr>
                         <td class="td-label">
                         <p style="text-align:center">Currency</p>
                        </td>
                        <td class="td-content" >
                          <asp:Label runat="server" ID="read_Currency" ></asp:Label>
                        </td>
                    <td class="td-label">
                         <p style="text-align:center">PO Amount</p>
                        </td>
                        <td class="td-content"  >
                        <asp:Label runat="server" ID="read_PoAmount"></asp:Label>
                        </td>
                    </tr>

                     <tr>
                    <td class="td-label">
                        
                         <p style="text-align:center">Delivery Terms</p>
                        </td>
                        <td class="td-content" colspan="3" >
                        <asp:Label runat="server" ID="read_Deliveryterms"    ></asp:Label>
                        </td>
                    </tr>
                     <tr>
                    <td class="td-label">
                         <p style="text-align:center">Other Matters</p>
                        </td>
                        <td class="td-content" colspan="3" >
                        <asp:Label runat="server" ID="read_Othermatters"  ></asp:Label>
                        </td>
                    </tr>

                    <tr>
                    <td class="td-label">
                         <p style="text-align:center">Contract comments</p>
                        </td>
                        <td class="td-content" colspan="3" >
                        <asp:Label runat="server" ID="read_Contractcomments"  MaxLength="100"   ></asp:Label>
                        </td>
                    </tr>
                     
                       <tr>
                    <td class="td-label">
                         <p style="text-align:center">PE  Comments</p>
                        </td>
                        <td class="td-content" colspan="3" >
                        <asp:Label runat="server" ID="read_PEComments"   ></asp:Label>
                        </td>
                    </tr>
                      <tr>
                    <td class="td-label">
                         <p style="text-align:center">QUA comments</p>
                        </td>
                        <td class="td-content" colspan="3" >
                        <asp:Label runat="server" ID="read_QUAComments"  ></asp:Label>
                        </td>
                    </tr>
                      <tr>
                    <td class="td-label">
                         <p style="text-align:center">FIN comments</p>
                        </td>
                        <td class="td-content" colspan="3" >
                        <asp:Label runat="server" ID="read_FINComments"    ></asp:Label>
                        </td>
                    </tr>
                      <tr>
                    <td class="td-label">
                         <p style="text-align:center">SCM comments</p>
                        </td>
                        <td class="td-content" colspan="3" >
                        <asp:Label runat="server" ID="read_SCMComments"   ></asp:Label>
                        </td>
                    </tr>
                    
                   
                    </table>


                      <div style="display:block">
                     <p style="font-weight:bold;">Program Execution manager Review</p>
                     <table class="table table-condensed table-bordered">
                     
                      <tr>
                    <td class="td-label"  style="width:17%;">
                         <span style=" background:red;  height:30px; float:left;">&nbsp;</span>
                         <p style="text-align:center">Review Conclusion</p>
                        </td>
                        <td class="td-content" colspan="3" >
             <p style="color:Red; font-weight:bold;">Note:the items abovementioned have been pre-reviewed by the team prior to this formal review.</p>
                        <asp:TextBox runat="server"   ID="fld_Reviewcomments"  TextMode="MultiLine" Rows="5"  Width="95%"  CssClass="validate[required]"></asp:TextBox>
                        </td>
                    </tr>
                     </table>
                     </div>

                    </div>

            <div class="row" style="display:block;">
                <attach:attachments id="Attachments1" Readonly="true"  runat="server"></attach:attachments>
            </div>
            <div class="row">
             <ah:approvalhistory id="ApprovalHistory1" showaction="true" runat="server"></ah:approvalhistory>
            </div>
             
        <btn:buttonlist id="ButtonList1" runat="server"></btn:buttonlist>
       
        </div>
    </form>
</body>
</html>

