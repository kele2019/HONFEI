<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="MobileClientBackground.Default" %>

<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>移动端配置</title>
    <script src="Script/jquery-1.8.3.min.js" type="text/javascript"></script>
    <script src="Script/My97DatePicker/WdatePicker.js" type="text/javascript"></script>
    <link href="Script/My97DatePicker/skin/WdatePicker.css" rel="stylesheet" type="text/css" />
    <link href="Css/bootstrap.min.css" rel="stylesheet" type="text/css" />
</head>

<body>
    <form id="form1" runat="server" class="well form-search">
    <br />
    <table class="table table-bordered">
        <tr>
            <td><%= Resources.Resource.Default_Search_ProcessName %></td>
            <td>
                <asp:TextBox ID="txtProcessName" runat="server"></asp:TextBox>
            </td>
            <td><%= Resources.Resource.Default_Search_CreatePage %></td>
            <td>
                <div class="controls">
                    <asp:DropDownList ID="dropCreatePage" runat="server">
                        <asp:ListItem Text="" Value=""></asp:ListItem>
                        <asp:ListItem Text="是" Value="1"></asp:ListItem>
                        <asp:ListItem Text="否" Value="0"></asp:ListItem>
                    </asp:DropDownList>
                </div>
            </td>
        </tr>
        <tr>
            <td colspan="4">
                <asp:Button ID="Button2" CssClass="btn btn-primary" runat="server" Text="<%$ Resources:Resource,Default_Search_SearchButton %>"
                    OnClick="Button2_Click" />
                <asp:Button ID="Button1"  CssClass="btn " runat="server" Text="<%$ Resources:Resource,Default_Search_ResetButton %>" 
                    OnClientClick="return ResetSearch()" onclick="Button1_Click"/>
                <script type="text/javascript" language="javascript">
                    function ResetSearch() {
                        $("#txtProcessName").val("");
                        $("#txtCreateTimeBegin").val("");
                        $("#txtCreateTimeEnd").val("");
                        $("#dropCreatePage option[value='']").attr("selected", "selected");
                        $("#txtUpdateTimeBegin").val("");
                        $("#txtUpdateTimeEnd").val("");
                        return true;
                    }
                </script>
            </td>
        </tr>
    </table>
    <table class="table table-condensed">
        <thead>
            <tr>
                <th><%= Resources.Resource.Default_List_No %></th>
                <th><%= Resources.Resource.Default_List_ProcessName %></th>
                <th><%= Resources.Resource.Default_List_CreatePage %></th>
                <th><%= Resources.Resource.Default_List_Operation %></th>
                 
            </tr>
        </thead>
        <tbody>
            <asp:Repeater ID="Repeater1" runat="server" onitemcommand="Repeater1_ItemCommand">
                <ItemTemplate>
                    <tr align="center">
                        <td><%# Container.ItemIndex+1 %></td>
                        <td>
                            <asp:Label ID="lbProcessName" runat="server" Text='<%# Eval("ProcessName")%>'></asp:Label>
                        </td>
                        <td>
                            <asp:LinkButton ID="LinkButton2" runat="server" class="btn" CommandName="CreatePage" CommandArgument='<%# Eval("ID") %>'><%= Resources.Resource.Default_CreatePage %></asp:LinkButton>
                        </td>
                        <td>
                            <asp:LinkButton ID="LinkButton1" runat="server" class="btn" CommandName="OpenProcessConfig" CommandArgument='<%# Eval("ID") %>'><%= Resources.Resource.Default_List_ConfigurationOrModify %></asp:LinkButton>
                        </td>
                       
                    </tr>
                </ItemTemplate>
            </asp:Repeater>
        </tbody>
        <tfoot>
            <tr>
                <td colspan="6">
                    <webdiyer:AspNetPager ID="AspNetPager1" runat="server" CustomInfoHTML="Count %RecordCount%"
                        HorizontalAlign="right" Width="100%" CssClass="aspNetPager" OnPageChanged="AspNetPager1_PageChanged"
                        PageSize="10" AlwaysShow="true" SubmitButtonStyle="display:none" InputBoxStyle="display:none"
                        NextPageText="<%$ Resources:Resource,AspNetPager_NextPageText %>" FirstPageText="<%$ Resources:Resource,AspNetPager_FirstPageText %>"
                        LastPageText="<%$ Resources:Resource,AspNetPager_LastPageText %>" PrevPageText="<%$ Resources:Resource,AspNetPager_PrevPageText %>">
                    </webdiyer:AspNetPager>
                </td>
            </tr>
        </tfoot>
    </table>
    </form>
</body>
</html>