<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="BudgetAccountEdit.aspx.cs" Inherits="Presale.Process.BudgetRequestAndApproval.BudgetAccountEdit" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
   <title>Budget Account Edit</title>
     <script type="text/javascript" src="/Assets/js/common.js"></script>
     <script type="text/javascript">
         function checkNullData() { 
         var Msg="";
         if ($("#txtCode").val() == "")
             Msg = "Budeget Code Is null ";
         if ($("#txtDesc").val() == "")
             Msg = "Budeget Desc Is null ";

         if (Msg != "") {
             alert(Msg);
             return false;
         }
     }
     function PageCancel() {
         location.href = "BudgetAccountManager.aspx";
     }
     </script>
</head>
<body>
    <form id="form1" runat="server">
     <div id="myDiv" class="container">
     <div style="text-align:center">  <h2>Budget Edit</h2> </div>
    <table class="table table-condensed table-bordered">
    <tr>
    <th>Budeget Code：</th>
    <td><asp:Label runat="server" ID="lbCode"></asp:Label>
    <asp:TextBox runat="server" ID="txtCode" Width="80%"></asp:TextBox>
    </td>
    </tr>
    <tr>
    <th>Budeget Desc：</th>
    <td>
    <asp:TextBox runat="server" ID="txtDesc" Width="80%"></asp:TextBox>
    </td>
    </tr>
    <tr>
    <th>Type：</th>
    <td>
    <asp:DropDownList runat="server" ID="dropType" Width="85%">
    <asp:ListItem Value="SG&A">SG&A</asp:ListItem>
    <asp:ListItem Value="RDE">RDE</asp:ListItem>
    <asp:ListItem Value="Captial">Captial</asp:ListItem>
    </asp:DropDownList>
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
    </td>
    </tr>

    </table>
    </div>
    </form>
</body>
</html>
