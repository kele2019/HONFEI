<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="NewRequest.aspx.cs" Inherits="Presale.Process.GoodsReceiveRequest.NewRequest" %>

 
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
          
        function showPurchaseOrder() {
            var digStr = "dialogHeight:500px;dialogWidth:850px;"
            var VenderInfo = $("#fld_CardCode").val();
            if (VenderInfo != "") {
                var ReturnValue = window.showModalDialog("./PurchaseOrderList.aspx?VenderInfo=" + VenderInfo, null, digStr);
                if (ReturnValue != null) {
                    $("#hdPOList").val(ReturnValue);
                   $("#btnAdd").click();
                }
            }
            else {
                alert("Please select vender infomation");
            }
        }
        function showPurchase() {
            var digStr = "dialogHeight:500px;dialogWidth:850px;"
            var ReturnValue = window.showModalDialog("../Presale.Process.PurchaseOrder/Purchase.aspx", null, digStr);
            if (ReturnValue != null) {
                var purchaseNo = eval("(" + ReturnValue + ")");
                var value = $("#fld_PurchaseOrderNo").val();
                value += "," + purchaseNo[0].PurchaseNo;
                if (value.substr(0, 1) == ',')
                    value = value.substr(1);
                $("#fld_PurchaseRequestNo").val(value);
            }
        }
       
        function changecode(obj) {
         
        }
         
        function beforeSubmit() {
              
            var summary = "Goods Receive Process";
            $("#UserInfo1_fld_PROCESSSUMMARY").val(summary);
            return true;
        }
        function beforeSave() {
            var summary = "Goods Receive Process";
            $("#UserInfo1_fld_PROCESSSUMMARY").val(summary);
            $("#fld_TRSummary").val(summary);
            return true;
        }
        $(document).ready(function () {

            $(".container").attr("style", "width:1200px");
            $(".td-label").attr("style", "width:15%");
            $(".td-content").attr("style", "width:35%");

            $("#buyer option:selected").text($("#fld_BUYER").val());
            $("#wareHouse option:selected").text($("#fld_WAREHOUSE").val());
            $("#currency").val($("#fld_Currency").val());
            POStatus_onchange($("#fld_GoodsFlag").val());

            if ($("#hdIncident").val() != "") {
                $("#ButtonList1_btnSubmit").val("Submit");
                $("#ButtonList1_btnBack").hide();
                $("#ButtonList1_btnReject").show();
            }
            if ($("#hdUrgeTask").val() == "Yes") {
                $("#ReturnBackTask").show();
            }

            $("#fld_Remark").keyup(function () {
                if ($("#fld_Remark").val().length > 254) {
                    $("#fld_Remark").val($("#fld_Remark").val().substr(0, 254));
                }
            });

        });
        function POStatus_onchange(obj) {
            if (obj != ""){
                $("#fld_GoodsFlag").val(obj);
                if (obj == 1) {
                    $("#PO").attr("checked", true);
                    $("#trPO").show();
                    $("#divPO").show();
                    $("#trPR").hide();
                    $("#fld_PurchaseRequestNo").val("");
                }
                if (obj == 0) {
                    $("#NoPO").attr("checked", true);
                    $("#trPR").show();
                    $("#divPO").hide();
                    $("#trPO").hide();
                    $("#fld_PurchaseOrderNo").val("");
                }
            }
        }
        function supplier_onclick(obj) {
            var digStr = "dialogHeight:500px;dialogWidth:850px;";
            var ReturnValue = window.showModalDialog("../Presale.Process.PurchaseOrder/Vendor.aspx", "", digStr);
            if (ReturnValue != null) {
                var vendor = eval("(" + ReturnValue + ")");
                var VendorCode = vendor[0].VendorCode;
                var VendorName = vendor[1].VendorName;
                var Currency = vendor[3].Currency;
                $("#fld_SUPPLIER").val(VendorCode + "-" + VendorName);
                $("#fld_CardCode").val(VendorCode);
                $("#fld_CardName").val(VendorName);
                $("#fld_Currency").val(Currency);

                var venderinfo = $("#fld_PurchaseOrderNo").val();
                if (venderinfo != "") {
                    $("#hdPOList").val("");
                    $("#btnAdd").click();
                }
                $("#fld_PurchaseOrderNo").val("");
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
            <p style="font-weight:bold;">Request require（"<span style=" background:red;">&nbsp;</span>" must write） </p>
            <table class="table table-condensed table-bordered">
                <tr>
                    <td class="td-label" style="vertical-align:middle;">
                        <span style=" background:red;  height:30px; float:left;">&nbsp;</span>
                        <p style="text-align:center">SUPPLIER</p>
                    </td>
                    <td class="td-content" colspan="3">
                        <asp:TextBox runat="server" ID="fld_SUPPLIER" onfocus="this.blur()" CssClass="validate[required]" onclick="supplier_onclick(this)" Width="94%"></asp:TextBox>
                        <asp:TextBox runat="server" ID="fld_CardCode" style="display:none;"></asp:TextBox>
                        <asp:TextBox runat="server" ID="fld_CardName" style="display:none;"></asp:TextBox>
                    </td>
                     <td class="td-label"> 
                    <span style=" background:red;height:30px; float:left;">&nbsp;</span>
                        <p style="text-align:center">CURRENCY</p>
                    </td>
                    <td class="td-content" colspan="3"> 
                        <asp:TextBox ID="fld_Currency" runat="server"   style="Width:95%"></asp:TextBox>
                    </td>
                </tr>
             
                <tr>
                    <td class="td-label" style="vertical-align:middle;"> 
                        <span style=" background:red;height:30px; float:left;">&nbsp;</span>
                        <p style="text-align:center">BUYER</p>
                    </td>
                    <td class="td-content" colspan="3">
                        <asp:DropDownList runat="server" ID="buyer"  onchange="drop_onchange(this)">
                            <asp:ListItem Selected="True">Buyer1</asp:ListItem>
                        </asp:DropDownList>
                        <asp:TextBox runat="server"  ID="fld_BUYER" value="Buyer1" style=" display:none;" Width="94%"></asp:TextBox>
                    </td>
                    <td class="td-label" style="vertical-align:middle;"> 
                        <span style=" background:red;height:30px; float:left;">&nbsp;</span>
                        <p style="text-align:center">WAREHOUSE</p>
                    </td>
                    <td class="td-content" colspan="3">
                         <asp:DropDownList runat="server" ID="wareHouse"  onchange="drop_onchange(this)">
                            <asp:ListItem Selected="True">901</asp:ListItem>
                         </asp:DropDownList>
                        <asp:TextBox runat="server"  ID="fld_WAREHOUSE" value="901" style="display:none;"></asp:TextBox>
                    </td>
                </tr>

                   <tr>
                    <td class="td-label">
                    <span style=" background:red;height:30px; float:left;">&nbsp;</span>
                        <p style="text-align:center">Purcahse Order Status</p>
                    </td>
                    <td class="td-content" colspan="7">
                       <input type="radio" name="POPR" id="PO" onclick="POStatus_onchange('1')" /> PO  &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                       <input type="radio" name="POPR" id="NoPO" onclick="POStatus_onchange('0')"  />No PO
                        <asp:TextBox runat="server"  ID="fld_GoodsFlag"   style="display:none;"></asp:TextBox>
                    </td>
                </tr>


                 <tr id="trPO" style="display:none">
                    <td class="td-label">
                    <span style=" background:red;height:30px; float:left;">&nbsp;</span>
                        <p style="text-align:center">Purcahse Order No</p>
                    </td>
                    <td class="td-content" colspan="7">
                        <asp:TextBox runat="server" ID="fld_PurchaseOrderNo" CssClass="validate[required]" Width="90%" style="background-color:White;" onfocus="this.blur()"></asp:TextBox>
                        <input type="button" value="..." class="btn" onclick="return showPurchaseOrder()" />

                    </td>
                </tr>


                <tr id="trPR"  style="display:none">
                    <td class="td-label">
                    <span style=" background:red;height:30px; float:left;">&nbsp;</span>
                        <p style="text-align:center">PUR. REQUEST NO</p>
                    </td>
                    <td class="td-content" colspan="7">
                        <asp:TextBox runat="server" ID="fld_PurchaseRequestNo" CssClass="validate[required]" Width="90%" style="background-color:White;" onfocus="this.blur()"></asp:TextBox>
                        <input type="button" value="..." class="btn" onclick="return showPurchase()" />

                    </td>
                </tr>
                <tr>
                <td class="td-label">Remark</td>
                 <td  colspan="7" class="td-content">
                 <asp:TextBox runat="server" ID="fld_GRRemark" TextMode="MultiLine" Rows="5"  Width="95%" ></asp:TextBox>
                 </td>
                  
                </tr>
            </table>
            <div id="divPO" style="display:none;">
            <p style="font-weight:bold">Request require</p>
            <table class="table table-condensed table-bordered">
                <tr>
                    <th width="0.3%">NO.</th>
                    <th width="6.8%">ITEMNO.</th>
                    <th width="7.6%">QUANTITY</th>
                    <th width="7.6%">UNIT PRICE</th>
                    <th width="7.6">TAX CODE</th>
                    <th width="7.1%">COSTCENTER</th>
                    <th width="10.1%">UOM CODE</th>
                    <th width="10.1%">PR NO.</th>
                    <th width="7%">REQUESTER</th>
                    <th width="7%">DETAIL DESCRIPTION</th>
                    <th width="4%">REQUIRED DATE</th>
                    <th width="7.6%">PO NO.</th>
                    <th width="5%">POLINENO.</th>
                    <th width="6%">PRLINENO.</th>
                    <th width="6.2%"></th>
                </tr>
                <tbody id="detail">
                    <asp:Repeater runat="server" ID="fld_detail_PROC_GoodsReceive_DT" OnItemCommand="fld_detail_PROC_GoodsReceive_DT_ItemCommand" OnItemDataBound="fld_detail_PROC_GoodsReceive_DT_ItemDataBound">
                        <ItemTemplate>
                            <tr>
                                <td style="text-align:center">
                                    <asp:TextBox ID="fld_FORMID" Text='<%#Eval("FORMID") %>' runat="server" Style="display: none" ></asp:TextBox>
                                    <asp:Label ID="fld_ROWID" Text='<%# Container.ItemIndex+1%>' runat="server"></asp:Label>
                                </td>
                                <td>
                                    <%#Eval("ITEMNO") %>
                                    <asp:TextBox ID="fld_ITEMNO" style="display:none" runat="server" Text='<%#Eval("ITEMNO") %>' onfocus="this.blur()" CssClass="validate[required]" onclick="openITEMNO(this)" Width="70%"></asp:TextBox>
                                </td>
                                  <td>
                                    <asp:TextBox ID="fld_QUANTITY" runat="server"  CssClass="validate[required]" Text='<%#Eval("QUANTITY") %>' Width="80%"></asp:TextBox>
                                </td>
                               <td>
                                    <asp:TextBox ID="fld_UnitPrice" runat="server"  CssClass="validate[required]" Text='<%#Eval("UnitPrice") %>' Width="80%"></asp:TextBox>
                                </td>
                                <td>
                                 <%#Eval("TaxCode")%>
                                    <asp:TextBox ID="fld_TaxCode" style="display:none" runat="server" MaxLength="17" Text='<%#Eval("TaxCode") %>' Width="74%"></asp:TextBox>
                                </td>
                                  <td>
                                   <%#Eval("CostCenter")%>
                                    <asp:TextBox ID="fld_CostCenter" style="display:none" runat="server" Text='<%#Eval("CostCenter") %>' Width="80%"></asp:TextBox>
                               </td>
                                <td>
                                 <%#Eval("UOMCode")%>
                                    <asp:TextBox ID="fld_UOMCode" style="display:none" runat="server"   Text='<%#Eval("UOMCode") %>'  Width="84%"></asp:TextBox>
                                </td>
                                  <td>
                                   <%#Eval("PRNo")%>
                                    <asp:TextBox ID="fld_PRNo" style="display:none" runat="server"    Text='<%#Eval("PRNo") %>' Width="74%"  ></asp:TextBox>
                                </td>
                                <td>
                                 <%#Eval("Requester")%>
                                    <asp:TextBox ID="fld_Requester" style="display:none" runat="server"   Text='<%#Eval("Requester") %>' Width="74%"  ></asp:TextBox>
                                </td>
                                <td>
                                 <%#Eval("DetailDescription")%>
                                    <asp:TextBox ID="fld_DetailDescription" style="display:none" runat="server"  Text='<%#Eval("DetailDescription") %>' Width="40%"></asp:TextBox>
                                </td>
                                <td>
                                <%# String.IsNullOrEmpty(Eval("RequestDate").ToString()) ? "" : DateTime.Parse(Eval("RequestDate").ToString()).ToString("yyyy-MM-dd")%>
                                    <asp:TextBox ID="fld_RequestDate" style="display:none" runat="server"  CssClass="validate[required]" Text='<%# String.IsNullOrEmpty(Eval("RequestDate").ToString())? "":DateTime.Parse(Eval("RequestDate").ToString()).ToString("yyyy-MM-dd") %>'  Width="84%"></asp:TextBox>
                                </td>
                              
                                <td>
                                 <%#Eval("PONo")%>
                               <asp:TextBox ID="fld_PONo" style="display:none" runat="server" Text='<%#Eval("PONo") %>' Width="84%" ></asp:TextBox>
                                </td>
                                  <td>
                                  <%#Eval("POLineNo")%>
                                    <asp:TextBox ID="fld_POLineNo" style="display:none" runat="server" Text='<%#Eval("POLineNo") %>' Width="84%"  ></asp:TextBox>
                                </td>

                                  <td>
                                  <%#Eval("PRLineNo")%>
                                    <asp:TextBox ID="fld_PRLineNo" style="display:none" runat="server" Text='<%#Eval("PRLineNo")%>' Width="74%" ></asp:TextBox>
                                </td>
                                <td>
                                    <asp:Button ID="btnDelete" runat="server" Text="Del" CssClass="btn" CommandName="del" ClientIDMode="Static" OnClientClick="return confirm('Confirm Del？')" />
                                </td>
                            </tr>
                        </ItemTemplate>
                    </asp:Repeater>
                </tbody>
            </table>
            </div>

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
        <btn:buttonlist id="ButtonList1" runat="server"></btn:buttonlist>
        <div style="display:none">
            <asp:TextBox runat="server" ID="fld_TRSummary"></asp:TextBox>
            <asp:HiddenField runat="server" ID="hdIncident" />
            <asp:HiddenField runat="server" ID="hdPrint" />
            <asp:HiddenField runat="server" ID="hdUrgeTask" />
            <asp:HiddenField runat="server" ID="hdPOList" />
            <asp:Button ID="btnAdd" runat="server" Text="add" CssClass="btn" CausesValidation="false" OnClick="btnAdd_Click"/>
        </div>
    </form>
</body>
</html>

