<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="FormConfiguration.aspx.cs" Inherits="MobileClientBackground.FormConfiguration" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Mobile phone approval background configuration</title>
    <script src="Script/jquery-1.8.3.min.js" type="text/javascript"></script>
    <script src="Script/My97DatePicker/WdatePicker.js" type="text/javascript"></script>
    <link href="Script/My97DatePicker/skin/WdatePicker.css" rel="stylesheet" type="text/css" />
    <link href="Css/bootstrap.min.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server" class="well form-search">
        <table class="table">
            <tr>
                <td>
                    <asp:Button ID="AddControl" CssClass="btn" runat="server" Text="<%$ Resources:Resource,FormConfiguration_List_AddButton %>" onclick="AddControl_Click" />
                    <asp:Button ID="Button1" CssClass="btn" runat="server" Text="<%$ Resources:Resource,FormConfiguration_List_DelButton %>" onclick="Button1_Click" />
                    <asp:Button ID="Button2" CssClass="btn" runat="server" Text="<%$ Resources:Resource,FormConfiguration_List_GoBackButton %>" onclick="Button2_Click" />
                </td>
            </tr>
        </table>
        <table class="table table-bordered">
            <thead>
                <tr>
                    <th><%= Resources.Resource.FormConfiguration_List_Choose %></th>
                    <th><%= Resources.Resource.FormConfiguration_List_No %></th>
                    <th><%= Resources.Resource.FormConfiguration_List_ProcessName %></th>
                    <th><%= Resources.Resource.FormConfiguration_List_StepName %></th>
                    <th><%= Resources.Resource.FormConfiguration_List_ColumnName %></th>
                    <th><%= Resources.Resource.FormConfiguration_List_ControlName %></th>
                    <th><%= Resources.Resource.FormConfiguration_List_ConfigureDataSource %></th>
                    <th><%= Resources.Resource.FormConfiguration_List_ControlValueFormat %></th>
                    <th><%= Resources.Resource.FormConfiguration_List_ExternalLinks %></th>
                    <th><%= Resources.Resource.FormConfiguration_List_Required %></th>
                    <th><%= Resources.Resource.FormConfiguration_List_ReadOnly %></th>
                    <th><%= Resources.Resource.FormConfiguration_List_IsShow %></th>
                    <th><%= Resources.Resource.FormConfiguration_List_OrderBy %></th>
                </tr>
            </thead>
            <tbody>
                <asp:Repeater ID="Repeater1" runat="server" 
                    onitemdatabound="Repeater1_ItemDataBound" 
                    onitemcommand="Repeater1_ItemCommand">
                    <ItemTemplate>
                        <tr style="text-align:center;">
                            <td><input type="checkbox" id="ItemCheckBox" class="checkbox" runat="server" value='<%# Eval("ID") %>'><asp:HiddenField runat="server" ID="FK_ID" Value='<%#Eval("FK_ID") %>'/></td>
                            <td><%# Container.ItemIndex+1 %></td>
                            <td><%# Request.QueryString["ProcessName"].ToString().Trim() %></td>
                            <td><%# Request.QueryString["StepName"].ToString().Trim() %></td>
                            <td>
                                <asp:TextBox ID="ItemColumnName" CssClass="input-small" runat="server" Text='<%# Eval("ColumnName") %>'></asp:TextBox>
                            </td>
                            <td>
                                <asp:DropDownList ID="ItemdropControl" runat="server" CssClass="input-small">
                                </asp:DropDownList>
                                <asp:HiddenField runat="server" ID="ItemControlID" Value='<%# Eval("ControlID") %>'/>
                            </td>
                            <td>
                                <asp:LinkButton ID="LinkButton1" CssClass="btn" runat="server" CommandName="OpenConfigForm" CommandArgument='<%# Eval("ID") %>'><%= Resources.Resource.FormConfiguration_List_Configure %></asp:LinkButton>
                            </td>
                            <td>
                                <asp:TextBox ID="ItemFormat" CssClass="input-small" runat="server" Text='<%# Eval("Format") %>' Height="50" TextMode="MultiLine"></asp:TextBox>
                            </td>
                            <td>
                                <asp:TextBox ID="ItemExternalLinks" CssClass="input-small" runat="server" Text='<%# Eval("ExternalLinks") %>' Height="50" TextMode="MultiLine"></asp:TextBox>
                            </td>
                            <td>
                                <asp:CheckBox ID="ItemIsWillFill" runat="server" CssClas="checkbox"/>
                                <asp:HiddenField runat="server" ID="ItemhfIsWillFill" Value='<%# Eval("IsWillFill") %>'/>
                            </td>
                            <td>
                                <asp:CheckBox ID="ItemReadOnly" runat="server" CssClas="checkbox"/>
                                <asp:HiddenField runat="server" ID="ItemhfReadOnly" Value='<%# Eval("ReadOnly") %>'/>
                            </td>
                            <td>
                                <asp:TextBox ID="ItemIsShow" runat="server" CssClass="input-small" Text='<%# Eval("IsShow") %>'/></asp:TextBox>
                            </td>
                            <td>
                                <asp:TextBox ID="ItemOrderBy" runat="server" CssClass="input-mini" Text='<%# Eval("OrderBy") %>' Width="50"></asp:TextBox>
                            </td>
                        </tr>
                    </ItemTemplate>
                </asp:Repeater>
            </tbody>
        </table>
        <asp:HiddenField runat="server" ID="ProcessID"/>
        <asp:HiddenField runat="server" ID="ProcessName"/>
    </form>
</body>
</html>