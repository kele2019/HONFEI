<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AddCurrency.aspx.cs" Inherits="Ultimus.UWF.FunctionManager.AddCurrency" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <img src="images/index/u0.png"/>
    <form id="form1" runat="server">
     <div id="addCurrency" >
        <table>
              <tr>
                  <td>币种：</td>
                  <td>
                     <asp:TextBox ID="currencyName" runat="server"></asp:TextBox>
                  </td> 
                </tr>
                <tr>
                  <td>汇率：</td>
                  <td>
                      <asp:TextBox ID="currencyValue" runat="server"></asp:TextBox>
                  </td>
               </tr>
        </table>
        <asp:Button ID="Button1" runat="server" Text="保存" CssClass="btn btn-primary"  onClick="btnAddCurrency_Click"/>
    </div>
    </form>
</body>
</html>

