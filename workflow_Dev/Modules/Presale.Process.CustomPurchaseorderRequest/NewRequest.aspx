<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="NewRequest.aspx.cs" Inherits="Presale.Process.CustomPurchaseorderRequest.NewRequest" %>

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
            var summary = "Custom Purchase order review";
            $("#UserInfo1_fld_PROCESSSUMMARY").val(summary);
            return true;
        }

        $(document).ready(function () {
            $("#Attachments1_txtMust").val("1");
            if ($("#hdIncident").val() - 0 > 0) {
                $("#ButtonList1_btnSubmit").val("Submit");
                $("#ButtonList1_btnBack").hide();
                $("#ButtonList1_btnReject").show();
            }
            if ($("#hdUrgeTask").val() == "Yes") {
                $("#ReturnBackTask").show();
            }
            selectproducttype();
        });
        function selectproducttype(obj) {
            if (obj != undefined) {
                if (obj == "1") {
                    $("#ptype1").attr("checked", true);
                    $("#fld_Producttype").val("New products");
                  
                }
                if (obj == "2") {
                    $("#ptype2").attr("checked", true);
                    $("#fld_Producttype").val("Existing products");
                }
            }
            else {
                var pttyp = $("#fld_Producttype").val();
                if (pttyp == "New products") {
                    $("#ptype1").attr("checked", true);
                }
                if (pttyp == "Existing products") {
                    $("#ptype2").attr("checked", true);
                }
            }
        }
        function beforeSave() {
            var summary = "Custom Purchase order review";
            $("#UserInfo1_fld_PROCESSSUMMARY").val(summary);
            $("#fld_TRSummary").val(summary);
            return true;
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
                <p style="font-weight:bold;">Request require（"<span style=" background:red;">&nbsp;</span>" must write） </p>
                <table class="table table-condensed table-bordered">
                    <tr>
                        <td class="td-label">
                         <span style=" background:red;  height:30px; float:left;">&nbsp;</span>
                         <p style="text-align:center">Program</p>
                        </td>
                        <td class="td-content"   >
                        <asp:TextBox runat="server" ID="fld_Program"   Width="95%"  CssClass="validate[required]"></asp:TextBox>
                        </td>
                        <td class="td-label">
                         <span style=" background:red;  height:30px; float:left;">&nbsp;</span>
                         <p style="text-align:center">Buyer</p>
                        </td>
                        <td class="td-content"  >
                        <asp:TextBox runat="server" ID="fld_Buyer" Width="95%"  CssClass="validate[required]" ></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="td-label">
                         <span style=" background:red;  height:30px; float:left;">&nbsp;</span>
                         <p style="text-align:center">Purchase Order No</p>
                        </td>
                        <td class="td-content"   >
                        <asp:TextBox runat="server" ID="fld_PurchaseOrderNo" MaxLength="5" Width="95%"  CssClass="validate[required]"></asp:TextBox>
                        </td>
                        <td class="td-label">
                         <span style=" background:red;  height:30px; float:left;">&nbsp;</span>
                         <p style="text-align:center">Purchase Order Revision</p>
                        </td>
                        <td class="td-content"  >
                        <asp:TextBox runat="server" ID="fld_PurchaseOrderrevision" Width="95%"  CssClass="validate[required]" ></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                      <td class="td-label">
                         <p style="text-align:center">Products Type</p>

                        </td>
                      <td class="td-content"  colspan="3" >
                       <input type="radio" name="ptype" id="ptype1" onclick="selectproducttype('1')" />  New products
                       <br />
                       Review attached Customer PO and Provide the supporting documents as below:
                        <br />●Specify roles and method to different customer requirements
                        <br /> ●Opportunity and Risk Assessment
                        <br />●Quantify Customer Needs
                        <br />●Special requirements of products and services are determined
                       <br />
                       <input type="radio" name="ptype" id="ptype2" onclick="selectproducttype('2')" /> Existing products
                       <br />
                       Review attached Customer PO 
                        <asp:TextBox runat="server" ID="fld_Producttype"  style="display:none" ></asp:TextBox>
                      </td>

                    </tr>

                </table>
            </div>

            <div class="row">
                <p style="font-weight:bold;">Review content（"<span style=" background:red;">&nbsp;</span>" must write） </p>
                <table class="table table-condensed table-bordered">
                    <tr>
                    <td class="td-label">
                         <span style=" background:red;  height:30px; float:left;">&nbsp;</span>
                         <p style="text-align:center">Technical Requirements</p>
                        </td>
                        <td class="td-content" colspan="3" >
                        <asp:TextBox runat="server" ID="fld_Techicalquality"  MaxLength="50"  Width="95%"  CssClass="validate[required]"></asp:TextBox>
                        </td>
                    </tr>

                     <tr>
                    <td class="td-label">
                         <span style=" background:red;  height:30px; float:left;">&nbsp;</span>
                         <p style="text-align:center">Quantity Requirements</p>
                        </td>
                        <td class="td-content" colspan="3" >
                        <asp:TextBox runat="server" ID="fld_qualityrequirements"    MaxLength="50" Width="95%"  CssClass="validate[required]"></asp:TextBox>
                        </td>
                    </tr>
                       <tr>
                    <td class="td-label">
                         <span style=" background:red;  height:30px; float:left;">&nbsp;</span>
                         <p style="text-align:center">Currency</p>
                        </td>
                        <td class="td-content" >
                         <asp:DropDownList runat="server" ID="fld_Currency">
                            <asp:ListItem Selected="True" Value="RMB">RMB</asp:ListItem>
                            <asp:ListItem  Value="USD">USD</asp:ListItem>
                         </asp:DropDownList>
                        </td>
                        <td>
                         <span style=" background:red;  height:30px; float:left;">&nbsp;</span>
                         <p style="text-align:center">PO Amount</p>
                        </td>
                        <td class="td-content"  >
                        <asp:TextBox runat="server" ID="fld_PoAmount"   Width="95%"  CssClass="validate[required]"></asp:TextBox>
                        </td>
                    </tr>

                    <tr>
                    <td class="td-label">
                         <span style=" background:red;  height:30px; float:left;">&nbsp;</span>
                         <p style="text-align:center">Quantity And Delivery Schedule</p>
                        </td>
                        <td class="td-content" colspan="3" >
                        <asp:TextBox runat="server" ID="fld_Quantitydelivery"   MaxLength="50"   Width="95%"  CssClass="validate[required]"></asp:TextBox>
                        </td>
                    </tr>

                     <tr>
                    <td class="td-label">
                         <span style=" background:red;  height:30px; float:left;">&nbsp;</span>
                         <p style="text-align:center">Delivery Terms</p>
                        </td>
                        <td class="td-content" colspan="3" >
                        <asp:TextBox runat="server" ID="fld_Deliveryterms"  MaxLength="50"    Width="95%"  CssClass="validate[required]"></asp:TextBox>
                        </td>
                    </tr>
                     <tr>
                    <td class="td-label">
                         <span style=" background:red;  height:30px; float:left;">&nbsp;</span>
                         <p style="text-align:center">Other Matters</p>
                        </td>
                        <td class="td-content" colspan="3" >
                        <asp:TextBox runat="server" ID="fld_Othermatters"  MaxLength="100"   Width="95%"  CssClass="validate[required]"></asp:TextBox>
                        </td>
                    </tr>

                     <tr>
                    <td class="td-label">
                         <span style=" background:red;  height:30px; float:left;">&nbsp;</span>
                         <p style="text-align:center">Contract comments</p>
                        </td>
                        <td class="td-content" colspan="3" >
                        <asp:TextBox runat="server" ID="fld_Contractcomments"  MaxLength="100"   Width="95%"  CssClass="validate[required]"></asp:TextBox>
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
                      <tr>
                    <td class="td-label">
                         <p style="text-align:center">Program Execution manager Review</p>
                        </td>
                        <td class="td-content" colspan="3" >
            <p style="color:Red; font-weight:bold;">Note:the items abovementioned have been pre-reviewed by the team prior to this formal review.</p>
                        <asp:TextBox runat="server" ID="read_Reviewcomments"  ReadOnly="true" TextMode="MultiLine" Rows="5"  Width="95%"  ></asp:TextBox>
                        </td>
                    </tr>
                    </table>
                    </div>

            <div class="row" style="display:block;">
                <attach:attachments id="Attachments1" runat="server"></attach:attachments>
            </div>
            <div class="row">
             <ah:approvalhistory id="ApprovalHistory1" showaction="true" runat="server"></ah:approvalhistory>
            </div>
              <div id="ReturnBackTask" style="display:none; text-align:center">
        <asp:Button runat="server" ID="btnRevoke"  Visible="false" CssClass="btn" Text="Revoke" 
                onclick="btnRevoke_Click" />
        </div>
        <btn:buttonlist id="ButtonList1" runat="server"></btn:buttonlist>
        <div style="display:none">
        <asp:TextBox runat="server" ID="fld_TRSummary"></asp:TextBox>
        <asp:HiddenField runat="server" ID="hdIncident" />
        <asp:HiddenField runat="server" ID="hdPrint" />
          <asp:HiddenField runat="server" ID="hdUrgeTask" />
        </div>
        </div>
    </form>
</body>
</html>


