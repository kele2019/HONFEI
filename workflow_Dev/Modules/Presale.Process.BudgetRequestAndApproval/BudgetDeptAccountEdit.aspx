<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="BudgetDeptAccountEdit.aspx.cs" Inherits="Presale.Process.BudgetRequestAndApproval.BudgetDeptAccountEdit" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
   <title>Budget Dept Account Edit</title>
     <script type="text/javascript" src="/Assets/js/common.js"></script>
     <script type="text/javascript">
         $(document).ready(function () {
             if ($("#hidID").val() != "")
                 $("#trCostCenter").hide();
         });
         function checkNullData() {
             var Msg = "";
             if ($("#txtCode").val() == "")
                 Msg = "Budeget Code Is null ";
             if ($("#txtDesc").val() == "")
                 Msg = "Budeget Desc Is null ";
             if (!$("#trCostCenter").is(":hidden")) {
                 var RowCount = 0;
                 $("#CBCostCenterList").find("input").each(function () {
                     if ($(this).attr("checked"))
                         RowCount++;
                 });
                 if (RowCount <= 0) {
                     Msg = "CostCenter Is Null";
                 }
             }
             if (Msg != "") {
                 alert(Msg);
                 return false;
             }
         }
         function PageCancel() {
             location.href = "BudgetDeptAccountManager.aspx";
         }
         function showPage() {
             var strUrl = "LoadBudeget.aspx";
             var width = "1000px";
             var height = "500px";
             var RturnVal = window.showModalDialog(strUrl, "", "dialogWidth:" + width + "px; dialogHeight:" + height + "px; dialogLeft: status:no; directories:yes;scrollbars:auto;Resizable=no;scroll:yes; ");
             if (RturnVal != undefined) {
                 var Code = RturnVal.split(',')[0];
                 var Type = RturnVal.split(',')[1];
                 var BudgetID = RturnVal.split(',')[2];
                 $("#txtCode").val(Code);
                 $("#hidType").val(Type);
                 $("#lbType").text(Type);
                 $("#hidBudgetID").val(BudgetID);
             }
         }

         function SelectAllDept(obj) {
             if ($(obj).attr("checked"))
                 $("#CBCostCenterList").find('input[type="checkbox"]').attr("checked", true);
                 else
                     $("#CBCostCenterList").find('input[type="checkbox"]').attr("checked", false);
         }
         
     </script>

  <style type="text/css">
   .cb input {vertical-align:middle;float:left;}
   .cb input{vertical-align:middle;}
   .cb td{width:100px; border:0;} 
   .cb label{display:inline-block;}
</style>
</head>
<body>
    <form id="form1" runat="server">
     <div id="myDiv" class="container">
     <div style="text-align:center">  <h2>Budget Dept Edit</h2> </div>
    <table class="table table-condensed table-bordered">
    <tr>
    <th>Budeget Code：</th>
    <td><asp:Label runat="server" ID="lbCode"></asp:Label>
    <asp:TextBox runat="server" ID="txtCode" Width="80%" onfocus="this.blur()"></asp:TextBox>
    <input type="button" value="..."  runat="server"  class="btn" id="btnLoadBudegetNo"  onclick="showPage()" />
    </td>
    </tr>
    <tr>
    <th>Budeget Dept Desc：</th>
    <td>
    <asp:TextBox runat="server" ID="txtDesc" Width="80%"></asp:TextBox>
    </td>
    </tr>
    <tr>
    <th>Type：</th>
    <td>
   <asp:Label runat="server" ID="lbType"></asp:Label>
   <asp:HiddenField runat="server" ID="hidType"  />
    </td>
    </tr>
    <tr id="trCostCenter">
    <th>CostCenter：</th>
    <td>
   <asp:CheckBoxList runat="server" ID="CBCostCenterList"  CssClass="cb" Width="80%" RepeatColumns="2" >
   <asp:ListItem Value=""   onclick="SelectAllDept(this)"> Select All</asp:ListItem>
   <asp:ListItem Value="50805000">50805000 General Management</asp:ListItem>
   <asp:ListItem Value="50800510">50800510 HSE&F</asp:ListItem>
   <asp:ListItem Value="50801500">50801500 Information Technology</asp:ListItem>
   <asp:ListItem  Value="50801010">50801010 Admin</asp:ListItem>
    <asp:ListItem  Value="50806500">50806500 Quality</asp:ListItem>
    <asp:ListItem  Value="50801000">50801000 Human Resources</asp:ListItem>
    <asp:ListItem  Value="50806200">50806200 Engineering-Mgt.</asp:ListItem>
    <asp:ListItem  Value="50803000">50803000 Finance</asp:ListItem>
    <asp:ListItem  Value="50808510">50808510 Operation</asp:ListItem>
    <asp:ListItem  Value="50805500">50805500 CTO/DCTO</asp:ListItem>
   <asp:ListItem Value="50807010">50807010 HOS</asp:ListItem>
   <asp:ListItem Value="50807500">50807500 Manufacturing</asp:ListItem>
   <asp:ListItem Value="50808520">50808520 Supply chain</asp:ListItem>
   </asp:CheckBoxList>
    </td>
    </tr>
    <tr>
    <th>Status：</th>
    <td>
    <asp:CheckBox runat="server" ID="cbStatus"   />Active
    </td>
    </tr>

    <tr>
    <td colspan="2" style="text-align:center">
    <asp:Button runat="server" ID="btnOK" Text="Save" CssClass="btn" 
            onclick="btnOK_Click" OnClientClick="return checkNullData()" />
    <input type="button" value="Cancel" style="margin-left:30px" class="btn" onclick="PageCancel()"  />
    <asp:HiddenField runat="server" ID="hidID" />
    <asp:HiddenField runat="server" ID="hidBudgetID" />
    </td>
    </tr>

    </table>
    </div>
    </form>
</body>
</html>
