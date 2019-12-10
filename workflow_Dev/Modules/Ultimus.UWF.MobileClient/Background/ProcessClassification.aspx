<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ProcessClassification.aspx.cs" Inherits="MobileClientBackground.ProcessClassification" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Mobile phone approval background configuration</title>
    <link href="Css/bootstrap.min.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server" class="well form-search">
        <table class="table">
            <tr>
                <td>
                    <asp:Button ID="Button1" runat="server" Text="添加" CssClass="btn" 
                        onclick="Button1_Click"/>
                    <asp:Button ID="Button2" runat="server" Text="删除" CssClass="btn" 
                        onclick="Button2_Click" />
                    <asp:Button ID="Button3" runat="server" Text="保存" CssClass="btn" 
                        onclick="Button3_Click" />
                </td>
            </tr>
        </table>
        <table class="table table-bordered">
            <tr>
                <th>序号</th>
                <th>选择</th>
                <th>分类名称(中文)</th>
                <th>分类名称(英文)</th>
                <th>流程名称</th>
                <th>是否启用</th>
            </tr>
            <asp:Repeater ID="Repeater1" runat="server" 
                onitemdatabound="Repeater1_ItemDataBound">
                <ItemTemplate>
                    <tr>
                        <td><%# Container.ItemIndex+1 %></td>
                        <td><input type="checkbox" id="checkbox" runat="server" class="checkbox" value='<%# Eval("ID") %>'/></td>
                        <td>
                            <asp:TextBox ID="TextBox1" runat="server" CssClass="input-medium" Text='<%# Eval("CategoryCName")%>'></asp:TextBox>
                        </td>
                        <td>
                            <asp:TextBox ID="TextBox2" runat="server" CssClass="input-medium" Text='<%# Eval("CategoryEName")%>'></asp:TextBox>
                        </td>
                        <td>
                            <asp:DropDownList ID="DropDownList1" runat="server" CssClass="input-medium">
                            </asp:DropDownList>
                            <asp:HiddenField runat="server" ID="processname" Value='<%# Eval("ProcessName")%>'/>
                        </td>
                        <td>
                            <asp:DropDownList ID="DropDownList2" runat="server" CssClass="input-medium">
                                <asp:ListItem Text="" Value=""></asp:ListItem>
                                <asp:ListItem Text="启用" Value="1"></asp:ListItem>
                                <asp:ListItem Text="禁用" Value="0"></asp:ListItem>
                            </asp:DropDownList>
                            <asp:HiddenField runat="server" ID="isaction" Value='<%# Eval("IsAction")%>'/>
                        </td>
                    </tr>
                </ItemTemplate>
            </asp:Repeater>
        </table>
    </form>
</body>
</html>
