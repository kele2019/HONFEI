<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="UpdateCurrency.aspx.cs" Inherits="Ultimus.UWF.FunctionManager.UpdateCurrency" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <img src="images/index/u0.png"/>
    <form id="form1" runat="server">
    <div id="updateCurrency">
        <table>
            <tr style="display:none;">
                <td style="display:none;">
                    <asp:TextBox ID="currencyChangeId" runat="server" style="display:none;"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>币种:</td>
                <td>
                    <asp:TextBox ID="currencyChangeName" runat="server" ReadOnly="true" BorderWidth="0"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>汇率：</td>
                <td>
                    <asp:TextBox ID="currencyChangeValue" runat="server"></asp:TextBox>
                </td>
            </tr>
        </table>
        <asp:Button ID="Button1" runat="server" Text="保存" CssClass="btn btn-primary" OnClick="btnUpdateCurrency_Click"/>
    </div>
    </form>
</body>
</html>

