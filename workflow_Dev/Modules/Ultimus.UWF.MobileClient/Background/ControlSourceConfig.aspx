<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ControlSourceConfig.aspx.cs"
    Inherits="MobileClientBackground.ControlSourceConfig" %>

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
            <td colspan="6">
                <asp:Button ID="Button1" runat="server" CssClass="btn"
                    Text="<%$ Resources:Resource,FormConfiguration_List_GoBackButton %>" onclick="Button1_Click" />
            </td>
        </tr>
        <tr>
            <td width="10%">
                <%= Resources.Resource.ControlSourceConfig_ProcessName %>
            </td>
            <td>
                <asp:Label ID="lbProcessName" runat="server"></asp:Label>
            </td>
            <td width="10%">
                <%= Resources.Resource.ControlSourceConfig_StepName %>
            </td>
            <td>
                <asp:Label ID="lbStepName" runat="server"></asp:Label>
            </td>
            <td width="10%">
                <%= Resources.Resource.ControlSourceConfig_ColumnName %>
            </td>
            <td>
                <asp:Label ID="lbColumnName" runat="server"></asp:Label>
            </td>
        </tr>
        <tr>
            <td width="10%">
                <%= Resources.Resource.ControlSourceConfig_ControlSourceType %>
            </td>
            <td colspan="5">
                <asp:CheckBox ID="cbDataBase" runat="server" CssClass="checkbox" Checked="true" AutoPostBack="true" OnCheckedChanged="cbDataBase_CheckedChanged" />
                <label for="cbDataBase" style="cursor: pointer;">
                    <%= Resources.Resource.ControlSourceConfig_ControlSource_DataBase %></label>
                    <p></p>
                <asp:CheckBox ID="cbElectronicForm" CssClass="checkbox" runat="server" AutoPostBack="true" OnCheckedChanged="cbElectronicForm_CheckedChanged" />
                <label for="cbElectronicForm" style="cursor: pointer;">
                    <%= Resources.Resource.ControlSourceConfig_ControlSource_ElectronicForm %></label>
            </td>
        </tr>
    </table>
    <br />
    <table id="DataBase" runat="server" class="table table-bordered">
        <tr>
            <td width="10%"><%= Resources.Resource.ControlSourceConfig_ControlSource_ConnectionString %></td>
            <td colspan="5">
                <asp:DropDownList ID="dropConnectionString" runat="server" CssClass="input-block-level" AutoPostBack="true" 
                    onselectedindexchanged="dropConnectionString_SelectedIndexChanged">
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td width="10%"><%= Resources.Resource.ControlSourceConfig_ControlSource_TableName %></td>
            <td colspan="5">
                <asp:DropDownList ID="dropTableName" runat="server" CssClass="input-block-level" AutoPostBack="true" 
                    onselectedindexchanged="dropTableName_SelectedIndexChanged">
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td width="10%"><%= Resources.Resource.ControlSourceConfig_ControlSource_ColumnName %></td>
            <td colspan="5">
                <asp:DropDownList ID="dropColumnName" runat="server" CssClass="input-block-level">
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td width="10%"><%= Resources.Resource.ControlSourceConfig_ControlSource_strWhere %></td>
            <td colspan="5">
                <asp:TextBox ID="txtSearchWhere" runat="server" CssClass="input-xxlarge" Height="100" TextMode="MultiLine"></asp:TextBox>
            </td>
        </tr>
    </table>
    <br />
    <table id="ElectronicForm" runat="server" visible="false" class="table table-bordered">
        <tr>
            <td width="10%"><%= Resources.Resource.ControlSourceConfig_ControlSource_VariableName %></td>
            <td colspan="5">
                <asp:DropDownList ID="dropVariableName" runat="server" CssClass="input-block-level">
                </asp:DropDownList>
            </td>
        </tr>
    </table>
    <asp:HiddenField runat="server" ID="tableID" Value="0"/>
    </form>
</body>
</html>