<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PRPOInfo.aspx.cs" Inherits="Presale.Process.PurchaseApplication.PRPOInfo" %>

<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
 <base target="_self"></base>
 <meta charset="UTF-8" http-equiv="X-UA-Compatible" content="IE=edge,chrome=1"/>
 <title>PR&PO&GR Info</title>
 <script type="text/javascript" src="/Assets/js/common.js"></script>
 <script type="text/javascript">
     $(document).ready(function () {
         $(".container").attr("style", "width:1630px");
         $(".td-label").attr("style", "width:15%");
         $(".td-content").attr("style", "width:35%");
     if($("#hidFlag").val()=="")
          $(".Operation").hide();
 });

 function POsupplier(obj) {
     var digStr = "dialogHeight:500px;dialogWidth:850px;";
     var ReturnValue = window.showModalDialog("../Presale.Process.PurchaseOrder/Vendor.aspx", "", digStr);
     if (ReturnValue != null) {
         var vendor = eval("(" + ReturnValue + ")");
         var VendorCode = vendor[0].VendorCode;
         var VendorName = vendor[1].VendorName;
         var Currency = vendor[3].Currency;
         $("#txtPO").val(VendorCode + "-" + VendorName);
         $("#hidPOVenderCode").val(VendorCode);
     }
 }

 function GRsupplier(obj) {
     var digStr = "dialogHeight:500px;dialogWidth:850px;";
     var ReturnValue = window.showModalDialog("../Presale.Process.PurchaseOrder/Vendor.aspx", "", digStr);
     if (ReturnValue != null) {
         var vendor = eval("(" + ReturnValue + ")");
         var VendorCode = vendor[0].VendorCode;
         var VendorName = vendor[1].VendorName;
         var Currency = vendor[3].Currency;
         $("#txtGR").val(VendorCode + "-" + VendorName);
         $("#hidGRVenderCode").val(VendorCode);
     }
 }
 function ResetSearch() {
     $("#txtPO").val("");
     $("#hidPOVenderCode").val("");
     $("#txtGR").val("");
     $("#hidGRVenderCode").val("");
     $("#DropPRStatus").val("");
     $("#btnSearch").click();
 }
</script>
</head>
<body>
    <form id="form1" runat="server">
    <div  style="width:98%;"   class="container" >
     <div style="text-align:center"> <h2>PR&PO&GR Reports</h2></div>

    <table class="table table-condensed table-bordered" width="100%">
    <tr><th colspan="4" style="text-align:left; font-weight:bold;">Search </th> </tr>
    <tr>
    <th> PO Supplier：</th>
    <td><asp:TextBox runat="server" ID="txtPO" onfocus="this.blur()" onclick="POsupplier(this)" ></asp:TextBox>
    <asp:HiddenField runat="server" ID="hidPOVenderCode" />
    </td>
    <th>GR Supplier：</th>
    <td>
    <asp:TextBox runat="server" ID="txtGR" onfocus="this.blur()" onclick="GRsupplier(this)" ></asp:TextBox>
    <asp:HiddenField runat="server" ID="hidGRVenderCode" />
    </td>
    </tr>
    <tr>
  <th>PR Status：</th>
  <td colspan="3">
  <asp:DropDownList runat="server" ID="DropPRStatus">
  <asp:ListItem Value="">Pls Select</asp:ListItem>
  <asp:ListItem Value="0">Open</asp:ListItem>
  <asp:ListItem Value="2">Close</asp:ListItem>
  </asp:DropDownList>
   
    <asp:Button runat="server" ID="btnSearch" CssClass="btn" Text="Search" 
            onclick="btnSearch_Click" style="margin-left:10px; margin-right:10px;" />
          <asp:HiddenField runat="server" ID="hidFlag" />

          <input type="button" value="Reset" class="btn" onclick="ResetSearch()" />
    </td>
    </tr>
   </table>
    
     
     <table class="table table-condensed table-bordered">
     <tr>
     <th>No</th>
     <th>PRNo</th>
     <th style="width:1800px">Remark</th>
     <th width="6%">PR Amount</th>
     <th>PRApplicant</th>
     <th>PRRequestdate</th>
     <th>PR Process Status</th>
     <th>PONo</th>
     <th>SUPPLIER</th>
     <th width="5%">PO Amount</th>
     <th>PORequestdate</th>
     <th>PRStatus</th>
     <th class="Operation">Operation</th>
     <th>GRNo</th>
      <th>GR Applicant</th>
     <th>GRRequestdate</th>
     <th>GR Supplier</th>
     </tr>
      
     <asp:Repeater runat="server" ID="RPList" onitemcommand="RPList_ItemCommand" 
             onitemdatabound="RPList_ItemDataBound">
     <ItemTemplate>
     <tr>
     <td><%#Eval("RN") %></td>
     <td>
     <a href='Approval.aspx?Incident=<%#Eval("Incident") %>&ProcessName=Purchase+Request&Type=myapproval' target="_blank"><%#Eval("DOCUMENTNO")%></a>
     </td>
     <td style="width:1800px"><%#Eval("Remarks")%></td>
     <td><%#Eval("TotalAmount")%></td>
     <td><%#Eval("PRAPPLICANT")%></td>
     <td><%#Eval("PRREQUESTDATE")%></td>
     <td><%# Eval("STATUS").ToString()=="1"?"Inprocessing":(Eval("STATUS").ToString()=="2"?"Complete":"Colse")%></td>
     <td> <a href='../Presale.Process.PurchaseOrder/Approval.aspx?Incident=<%#Eval("POINCIDENT") %>&ProcessName=Purchase+Order&Type=myapproval' target="_blank"><%#Eval("PONO") %></a></td>
     <td><%#Eval("SUPPLIER")%></td>
      <td><%#Eval("TotalValueWithTax")%></td>
    
     <td><%#Eval("POREQUESTDATE")%></td>
     <td>
     <%#Eval("PurchaseOrdStatus").ToString()=="0"?"Open":"Close" %>
     </td>
     <td class="Operation">
     <asp:Button runat="server" ID="btnOpen"  CommandName="EnableCom" OnClientClick='return confirm("Confirm Open this PR")' CommandArgument='<%#Eval("DOCUMENTNO") %>' Visible='<%#  Eval("Status").ToString()=="2"?(Eval("PurchaseOrdStatus").ToString()=="0"? false:true):false%>' Text="Open" CssClass="btn"></asp:Button>
     <asp:Button runat="server" ID="btnClose" CommandName="DisableCom" OnClientClick='return confirm("Confirm Close this PR")' CommandArgument='<%#Eval("DOCUMENTNO") %>'  Visible='<%# Eval("Status").ToString()=="2"? (Eval("PurchaseOrdStatus").ToString()!="0"? false:true) :false%>' Text="Close" CssClass="btn"></asp:Button>
     </td>

     <td>
     <a href='../Presale.Process.GoodsReceiveRequest/Approve.aspx?Incident=<%#Eval("GRIncident") %>&ProcessName=Goods%20Receive%20Application&Type=myapproval' target="_blank"><%#Eval("GRNo")%></a>
     </td>
     <td><%#Eval("GRAPPLICANT")%></td>
     <td><%#Eval("GRREQUESTDATE")%></td>
     <td><%#Eval("GRSupplier")%></td>

     </tr>
     </ItemTemplate>
     </asp:Repeater>
     </table>
       <webdiyer:aspnetpager id="AspNetPager1" runat="server" custominfohtml="Count %RecordCount%"
                        horizontalalign="right" width="100%" cssclass="aspNetPager" onpagechanged="AspNetPager1_PageChanged"
                        pagesize="10" alwaysshow="true" submitbuttonstyle="display:none" inputboxstyle="display:none"
                        nextpagetext="Next" firstpagetext="Home" lastpagetext="End" prevpagetext="Prev">
                    </webdiyer:aspnetpager>
    
    </div>
    </form>
</body>
</html>
