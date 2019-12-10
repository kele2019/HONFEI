<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AddAgent.aspx.cs" Inherits="Ultimus.UWF.FunctionManager.AddAgent" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <script src="/Assets/js/common.js" type="text/javascript"></script>
    <script type="text/javascript">
        function openparent() {
            digStr = "dialogHeight:500px;dialogWidth:800px;"
            var ReturnValue = window.showModalDialog("../Ultimus.UWF.OrgChart/SelectAllUser.aspx",null, digStr);
            if (ReturnValue != null) {
                var returnValue = eval("(" + ReturnValue + ")");
                $("#agentUserName").val(returnValue[0].Name);
                var loginName = returnValue[0].LoginName;
                loginName = loginName.substring(8);
                $("#agentLoginName").val(loginName);
            }
        }
    </script>
</head>
<body>
    <img src="images/index/u0.png"/>
    <form id="form1" runat="server">
        <div id="addAgent">
            <table>
                <tr>
                    <td>代理账户:</td>
                    <td>
                        <asp:TextBox ID="agentUserName" runat="server" onclick="openparent()"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ControlToValidate="agentUserName" runat="server" ErrorMessage="*不能为空"></asp:RequiredFieldValidator>
                    </td>
               </tr>
                <tr>
                    <td>代理账号:</td>
                    <td>
                        <asp:TextBox ID="agentLoginName" runat="server" onclick="openparent()"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" ControlToValidate="agentLoginName" runat="server" ErrorMessage="*不能为空"></asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr>
                    <td>密码:</td>
                    <td>
                        <asp:TextBox ID="agentPwd" runat="server" textmode="Password"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" ControlToValidate="agentPwd" runat="server" ErrorMessage="*不能为空"></asp:RequiredFieldValidator>
                    </td>
                </tr>
            </table>
            <asp:Button ID="Button1" runat="server" Text="保存" CssClass="btn btn-primary" onClick="btnAddAgent_Click" />
        </div>
    </form>
</body>
</html>

