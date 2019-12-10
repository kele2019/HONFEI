<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="StepConfigCopy.aspx.cs" Inherits="MobileClientBackground.StepConfigCopy" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="Css/bootstrap.min.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server" class="well form-search">
        <table class="table">
            <tr>
                <td>
                    <asp:Button ID="Button1" runat="server" CssClass="btn" 
                        Text="<%$ Resources:Resource,StepConfigCopy_Confirm %>" 
                        onclick="Button1_Click" />
                    <asp:Button ID="Button2" runat="server" CssClass="btn" 
                        Text="<%$ Resources:Resource,StepConfigCopy_Return %>" 
                        onclick="Button2_Click" />
                </td>
            </tr>
        </table>
        <table class="table table-bordered">
            <tr>
                <th><%= Resources.Resource.ProcessStepList_List_No %></th>
                <th><%= Resources.Resource.ProcessStepList_List_StepName %></th>
                <th><%= Resources.Resource.FormConfiguration_List_Choose %></th>
            </tr>
            <asp:Repeater ID="Repeater1" runat="server">
                <ItemTemplate>
                    <tr>
                        <td><%# Container.ItemIndex+1 %></td>
                        <td><%# Eval("StepName") %></td>
                        <td>
                            <input type="checkbox" id="checkbox" runat="server" value='<%# Eval("ID") %>'/>
                        </td>
                    </tr>
                </ItemTemplate>
            </asp:Repeater>
        </table>
    </form>
</body>
</html>
