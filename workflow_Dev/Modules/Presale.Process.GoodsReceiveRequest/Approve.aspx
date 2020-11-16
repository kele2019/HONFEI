<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Approve.aspx.cs" Inherits="Presale.Process.GoodsReceiveRequest.Approve" %>
 
<%@ Register Src="/Modules/Ultimus.UWF.Form.ProcessControl/UserInfo.ascx" TagName="UserInfo" TagPrefix="ui" %>
<%@ Register Src="/Modules/Ultimus.UWF.Form.ProcessControl/ApprovalHistory.ascx" TagName="ApprovalHistory" TagPrefix="ah" %>
<%@ Register Src="/Modules/Ultimus.UWF.Form.ProcessControl/Attachments.ascx" TagName="Attachments" TagPrefix="attach" %>
<%@ Register Src="/Modules/Ultimus.UWF.Form.ProcessControl/ButtonList.ascx" TagName="ButtonList" TagPrefix="btn" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta http-equiv="X-UA-Compatible"  content="IE=EmulateIE8" />
    <title>Goods Receive Process</title>
    <script type="text/javascript" src="/Assets/js/common.js"></script>
    <script type="text/javascript">

        function beforeSubmit() {
            var summary = "Goods Receive Process";
            $("#UserInfo1_fld_PROCESSSUMMARY").val(summary);
            return true;
        }
        function beforeSave() {
          
            return true;
        }

        function summaryTotalAmount() {
            var totaltax = 0; var totalnontax = 0;
            $("#detail").find("tr").each(function (i, Etr) {
                var TaxAmount = $(Etr).find('.TaxAmount').val();
                var NonTaxAmount = $(Etr).find('.NonTaxAmount').val();
                totaltax += TaxAmount - 0;
                totalnontax += NonTaxAmount - 0;
            });
            $("#NonTaxAmount").text(totalnontax.toFixed(2));
            $("#TaxAmountan").text(totaltax.toFixed(2));
        }

        $(document).ready(function () {

            $(".container").attr("style", "width:1200px");
            $(".td-label").attr("style", "width:15%");
            $(".td-content").attr("style", "width:35%");

            //            $("#buyer option:selected").text($("#fld_BUYER").val());
            //            $("#wareHouse option:selected").text($("#fld_WAREHOUSE").val());
            //            $("#currency").val($("#fld_Currency").val());
            POStatus_onchange($("#read_GoodsFlag").val());
            summaryTotalAmount();
        });
        function POStatus_onchange(obj) {
            if (obj != "") {
                if (obj == 1) {
                    $("#span_GoodsFlag").text("Goods Receiving with PO");
                    //$("#PO").attr("checked", true);
                    $("#attchmentpo").show();
                    $("#attchmentpr").hide();
                    $("#trPO").show();
                    $("#divPO").show();
                    $("#trPR").hide();
                    $("#fld_PurchaseRequestNo").val("");
                }
                if (obj == 0) {
                    $("#span_GoodsFlag").text("None  PO ");
                    // $("#NoPO").attr("checked", true);
                    $("#attchmentpo").hide();
                    $("#attchmentpr").show();
                    $("#trPR").show();
                    $("#divPO").hide();
                    $("#trPO").hide();
                    $("#fld_PurchaseOrderNo").val("");
                }
            }
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <div id="myDiv" class="container">
            <div class="row">
                <ui:userinfo id="UserInfo1" processtitle="Goods Receive Request" processprefix="GR" tablename="PROC_GoodsReceive" tablenamedetail="PROC_GoodsReceive_DT" runat="server"  ></ui:userinfo>
            </div>
      
        <div class="row">
            <p style="font-weight:bold;">Request require </p>
            <table class="table table-condensed table-bordered">
                <tr>
                    <td class="td-label" style="vertical-align:middle;">
                        <p style="text-align:center">Supplier</p>
                    </td>
                    <td class="td-content" colspan="3">
                        <asp:TextBox runat="server" ID="read_CardCode" style="display:none;"></asp:TextBox>
                        <asp:Label runat="server" ID="read_CardName"  ></asp:Label>
                    </td>
                     <td class="td-label"> 
                        <p style="text-align:center">Currency</p>
                    </td>
                    <td class="td-content" colspan="3"> 
                        <asp:Label ID="read_Currency" runat="server"    ></asp:Label>
                    </td>
                </tr>
             
                <tr>
                    <td class="td-label" style="vertical-align:middle;"> 
                        <p style="text-align:center">Buyer</p>
                    </td>
                    <td class="td-content" colspan="3">
                        <asp:Label runat="server"  ID="read_BUYER" value="Buyer1"  ></asp:Label>
                    </td>
                    <td class="td-label" style="vertical-align:middle;"> 
                        
                        <p style="text-align:center">Warehouse</p>
                    </td>
                    <td class="td-content" colspan="3">
                        <asp:Label runat="server"  ID="read_WAREHOUSE" value="901"  ></asp:Label>
                    </td>
                </tr>

                   <tr style="display:none;">
                    <td class="td-label">
                        <p style="text-align:center">Purcahse Order Status</p>
                    </td>
                    <td class="td-content" colspan="7">
                    <span id="span_GoodsFlag"></span>
                        <asp:TextBox runat="server"  ID="read_GoodsFlag"  style="display:none"  ></asp:TextBox>
                    </td>
                </tr>


                 <tr id="trPO" style="display:none">
                    <td class="td-label">
                        <p style="text-align:center">Purcahse Order No</p>
                    </td>
                    <td class="td-content" colspan="7">
                        <asp:Label runat="server" ID="read_PurchaseOrderNo"   ></asp:Label>
                       
                    </td>
                </tr>

                <tr id="trPR"  style="display:none">
                    <td class="td-label">
                        <p style="text-align:center">Purchase Request No.</p>
                    </td>
                    <td class="td-content" colspan="7">
                        <asp:Label runat="server" ID="read_PurchaseRequestNo"  onprerender="read_PurchaseRequestNo_PreRender" ></asp:Label>

                    </td>
                </tr>

                 <tr>
                 <td class="td-label">
                        <p style="text-align:center">Attachement Description</p>
                    </td>
                     <td class="td-content" colspan="7">
                    <asp:Label runat="server" ID="read_Attchmentlist"   ></asp:Label>
                     </td>
                </tr>
                <tr>
                <td class="td-label">
                 <p style="text-align:center">Remark</p>
                </td>
                 <td  colspan="7" class="td-content">
                 <asp:TextBox runat="server" ID="read_GRRemark" ReadOnly="true" TextMode="MultiLine" Rows="5"  Width="98%" ></asp:TextBox>
                 </td>
                  
                </tr>
            </table>
            <div id="divPO" style="display:none;">
            <p style="font-weight:bold">Request require</p>
            <table class="table table-condensed table-bordered">
                <tr>
                    <th width="6.8%">ITEMNO.</th>
                     <th width="3.8%">OpenQty</th>
                    <th width="3.8%">OrderQty</th>
                    <th width="7.6%">UNIT PRICE</th>
                    <th width="7.6">TAX CODE</th>
                       <th width="3">NonTax-Amount</th>
                      <th width="3">Tax-Amount</th>
                    <th width="7.1%">COSTCENTER</th>
                    <th width="4%">UOM CODE</th>  
                    <th width="7.6%">PO NO.</th>
                    <th width="10.1%">PR NO.</th>
                    <th width="7%">REQUESTER</th>
                    <th width="7%">DETAIL DESCRIPTION</th>
                    <th width="4%">REQUIRED DATE</th>
                  
                    <th width="5%">POLINENO.</th>
                    <th width="6%">PRLINENO.</th>
                </tr>
                <tbody id="detail">
                    <asp:Repeater runat="server" ID="read_detail_PROC_GoodsReceive_DT"  >
                        <ItemTemplate>
                            <tr>
                                <td style="text-align:center;display:none;">
                                    <asp:TextBox ID="read_FORMID" Text='<%#Eval("FORMID") %>' runat="server" Style="display: none" ></asp:TextBox>
                                    <asp:Label ID="fld_ROWID" Text='<%# Container.ItemIndex+1%>' runat="server"></asp:Label>
                                </td>
                                <td>
                                    <%#Eval("ITEMNO") %>
                                </td>
                                  <td><%#Eval("QUANTITY")%>
                                </td>
                                 <td>
                                  <%#Eval("OrderQty")%>
                                  </td>

                               <td>
                                <%#Eval("UnitPrice")%>
                                </td>
                                <td>
                                 <%#Eval("TaxCode")%>
                                </td>
                                 <td>
                                    <%#Eval("NonTaxAmount")%> 
                                    <asp:TextBox ID="read_NonTaxAmount" CssClass="NonTaxAmount" style="display:none" runat="server" MaxLength="17" Text='<%#Eval("NonTaxAmount") %>' Width="74%"></asp:TextBox>
                                  </td>
                                 <td>
                                    <%#Eval("TaxAmount")%> 
                                    <asp:TextBox ID="read_TaxAmount"  CssClass="TaxAmount"  style="display:none" runat="server" MaxLength="17" Text='<%#Eval("TaxAmount") %>' Width="74%"></asp:TextBox>

                                  </td>
                                  
                                  <td>
                                   <%#Eval("CostCenter")%>
                               </td>
                                <td>
                                 <%#Eval("UOMCode")%>
                                </td>
                                 <td>
                                 <%#Eval("PONo")%>
                                </td>
                                  <td>
                                   <%#Eval("PRNo")%>
                                </td>
                                <td>
                                 <%#Eval("Requester")%>
                                </td>
                                <td>
                                 <%#Eval("DetailDescription")%>
                                </td>
                                <td>
                                <%# String.IsNullOrEmpty(Eval("RequestDate").ToString()) ? "" : DateTime.Parse(Eval("RequestDate").ToString()).ToString("yyyy-MM-dd")%>
                                </td>
                                  <td>
                                  <%#Eval("POLineNo")%>
                                </td>

                                  <td>
                                  <%#Eval("PRLineNo")%>
                                </td>
                            </tr>
                        </ItemTemplate>
                    </asp:Repeater>
                </tbody>

                <tr>
               <td colspan="5" style="text-align:right;">Total Amount</td>
               <td><span id="NonTaxAmount"></span></td>
               <td><span id="TaxAmountan"></span></td>
               <td colspan="9"></td>
               </tr>
            </table>
            </div>

        </div>
         <div class="row">
            <p id="attchmentpo" style="display:none;color:Red; font-weight:bold;"> Confirm acceptance of the above quantity of goods (services) according to the delivery requirements agreed by PO</p>
            <p id="attchmentpr"  style="display:none;color:Red; font-weight:bold;">Confirmation of acceptance of the goods (services) listed in the annex (packing list/invoice) </p>
            <attach:attachments id="Attachments1"  ReadOnly="true" runat="server"></attach:attachments>
        </div>
        <div class="row">
            <ah:approvalhistory id="ApprovalHistory1" showaction="true" runat="server"></ah:approvalhistory>
        </div>
        <btn:buttonlist id="ButtonList1" runat="server"></btn:buttonlist>
        <div style="display:none">
            <asp:TextBox runat="server" ID="fld_TRSummary"></asp:TextBox>
        </div>
    </form>
</body>
</html>

