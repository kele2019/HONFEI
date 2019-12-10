<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="UpdateAgent.aspx.cs" Inherits="Ultimus.UWF.FunctionManager.UpdateAgent" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style type="text/css">
        .btn-primary
        {
            height: 21px;
        }
    </style>
</head>
<body>
    <img src="images/index/u0.png"/>
    <form id="form1" runat="server">
        <div id="updatePwd">
            <table>
                <tr style="display:none;">
                    <td style="display:none;">
                        <asp:TextBox ID="updateAgentId" runat="server" style="display:none;"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>代理用户：</td>
                    <td>
                        <asp:TextBox ID="updateAgentName" runat="server" ReadOnly="true" BorderWidth="0"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>请输入密码：</td>
                    <td>
                       <asp:TextBox ID="updateAgentPwd" runat="server"></asp:TextBox>
                       <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ControlToValidate="updateAgentPwd" runat="server" ErrorMessage="*不能为空"></asp:RequiredFieldValidator>
                    </td>
                </tr>
            </table>
            <asp:Button ID="Button1" runat="server" Text="保存" CssClass="btn btn-primary" onClick="btnUpdatePwd_Click" />
            <asp:Button ID="Button2" runat="server" Text="返回" CssClass="btn btn-primary" CausesValidation="false" OnClick="btnReturnAgent_Click" />
        </div>
    </form>
</body>
</html>