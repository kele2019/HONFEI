<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ProcessStepList.aspx.cs" Inherits="MobileClientBackground.ProcessStepList" %>

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
                    <asp:Button ID="Button2" runat="server" CssClass="btn" Text="<%$ Resources:Resource,FormConfiguration_List_GoBackButton %>" onclick="Button2_Click" />
                </td>
            </tr>
        </table>
        <table class="table table-bordered">
            <thead>
                <tr>
                    <th><%= Resources.Resource.ProcessStepList_List_No %></th>
                    <th><%= Resources.Resource.ProcessStepList_List_ProcessName %></th>
                    <th><%= Resources.Resource.ProcessStepList_List_StepName %></th>
                    <th><%= Resources.Resource.ProcessStepList_List_StepCName %></th>
                    <th><%= Resources.Resource.ProcessStepList_List_StepEName %></th>
                    <th><%= Resources.Resource.ProcessStepList_List_FormConfiguration %></th>
                    <th><%= Resources.Resource.ProcessStepList_List_DataSave %></th>
                    <th><%= Resources.Resource.ProcessStepList_List_Shortcuts %></th>
                </tr>
            </thead>
            <tbody>
                <asp:Repeater ID="Repeater1" runat="server" onitemcommand="Repeater1_ItemCommand" onitemdatabound="Repeater1_ItemDataBound">
                    <ItemTemplate>
                        <tr style="text-align:center;">
                            <td><%# Container.ItemIndex+1 %></td>
                            <td>
                                <asp:Label ID="lbProcessName" runat="server" Text='<%# Eval("ProcessName") %>'></asp:Label>
                            </td>
                            <td>
                                <asp:Label ID="lbStepName" runat="server" Text='<%# Eval("StepName") %>'></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtStepCName" runat="server" CssClass="input-medium"></asp:TextBox>
                            </td>
                            <td>
                                <asp:TextBox ID="txtStepEName" runat="server" CssClass="input-medium"></asp:TextBox>
                            </td>
                            <td>
                                <asp:Button ID="Button1" CssClass="btn" runat="server" Text="<%$ Resources:Resource,ProcessStepList_List_FormOperation %>" CommandArgument='<%#Eval("StepName")%>' CommandName="OpenFormOperation"/>
                            </td>
                            <td>
                                <asp:Button ID="Button3" CssClass="btn" runat="server" Text="<%$ Resources:Resource,ProcessStepList_List_DataSaveConfig %>" CommandArgument='<%#Eval("StepName")%>' CommandName="OpenConfigDataSave"/>
                            </td>
                            <td>
                                <asp:Button ID="Button4" CssClass="btn" runat="server" Text="<%$ Resources:Resource,ProcessStepList_List_Copy %>" CommandArgument='<%#Eval("StepName")%>' CommandName="OpenCopy"/>
                            </td>
                        </tr>
                    </ItemTemplate>
                </asp:Repeater>
            </tbody>
        </table>
    </form>
</body>
</html>
