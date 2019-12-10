<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="DataSaveConfig.aspx.cs" Inherits="MobileClientBackground.DataSaveConfig" %>

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
                    <asp:Button ID="Button2" runat="server" CssClass="btn"
                        Text="<%$ Resources:Resource,FormConfiguration_List_GoBackButton %>" 
                        onclick="Button2_Click"/>
                </td>
            </tr>
        </table>
        <table class="table">
            <tr>
                <th colspan="6" style="text-align:left;"><%= Resources.Resource.DataSaveConfig_MasterConfig %></th>
            </tr>
            <tr>
                <td width="10%"><%= Resources.Resource.DataSaveConfig_ConnectionString %></td>
                <td width="20%">
                    <asp:DropDownList ID="_dropMasterConnectionString" runat="server" CssClass="input-xlarge" AutoPostBack="true" onselectedindexchanged="_dropMasterConnectionString_SelectedIndexChanged">
                    </asp:DropDownList>
                </td>
                <td width="10%"><%= Resources.Resource.DataSaveConfig_TableName %></td>
                <td>
                    <asp:DropDownList ID="_dropMasterTableName" runat="server" CssClass="input-xlarge" AutoPostBack="true">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <th colspan="6" style="text-align:left;"><%= Resources.Resource.DataSaveConfig_SublistConfig %></th>
            </tr>
            <tr>
                <td width="10%"><%= Resources.Resource.DataSaveConfig_ConnectionString %></td>
                <td width="20%">
                    <asp:DropDownList ID="_dropSublistConnectionString" runat="server" CssClass="input-xlarge">
                    </asp:DropDownList>
                </td>
            </tr>
        </table>
        <table class="table table-bordered">
            <tr>
                <th><%= Resources.Resource.DataSaveConfig_No %></th>
                <th><%= Resources.Resource.DataSaveConfig_ProcessName %></th>
                <th><%= Resources.Resource.DataSaveConfig_StepName %></th>
                <th><%= Resources.Resource.DataSaveConfig_ColumnName %></th>
                <th><%= Resources.Resource.DataSaveConfig_DataBase %></th>
                <th><%= Resources.Resource.DataSaveConfig_ElectronicForm %></th>
                <th><%= Resources.Resource.DataSaveConfig_IsMaster %></th>
                <th><%= Resources.Resource.DataSaveConfig_IsSublist %></th>
                <th><%= Resources.Resource.DataSaveConfig_TableName %></th>
                <th><%= Resources.Resource.DataSaveConfig_FieldName %></th>
                <th><%= Resources.Resource.DataSaveConfig_VariableName %></th>
            </tr>
            <asp:Repeater ID="Repeater1" runat="server" onitemdatabound="Repeater1_ItemDataBound">
                <ItemTemplate>
                    <tr>
                        <td>
                            <%# Container.ItemIndex+1 %>
                            <asp:HiddenField runat="server" ID="ItemID" Value='<%# Eval("ID") %>'/>
                        </td>
                        <td><%# Request.QueryString["ProcessName"].ToString().Trim() %></td>
                        <td><%# Request.QueryString["StepName"].ToString().Trim() %></td>
                        <td>
                            <asp:Label ID="lbColumnName" runat="server"></asp:Label>
                        </td>
                        <td>
                            <asp:CheckBox ID="cbSaveToDataBase" runat="server" CssClass="checkbox" AutoPostBack="true" OnCheckedChanged="cbSaveToDataBase_CheckedChanged"/>
                        </td>
                        <td>
                            <asp:CheckBox ID="cbSaveToElectronicForm" runat="server" CssClass="checkbox" AutoPostBack="true" OnCheckedChanged="cbSaveToElectronicForm_CheckedChanged"/>
                        </td>
                        <td>
                            <asp:RadioButton ID="radioMaster" runat="server" Checked="true" CssClass="radio" GroupName="radio" AutoPostBack="true" OnCheckedChanged="radioMaster_CheckedChanged"/>
                        </td>
                        <td>
                            <asp:RadioButton ID="radioSublist" runat="server" CssClass="radio" GroupName="radio" AutoPostBack="true" OnCheckedChanged="radioSublist_CheckedChanged"/>
                        </td>
                        <td>
                            <asp:DropDownList ID="dropTableName" runat="server" CssClass="input-xlarge" AutoPostBack="true" onselectedindexchanged="dropTableName_SelectedIndexChanged">
                            </asp:DropDownList>
                        </td>
                        <td>
                            <asp:DropDownList ID="dropFieldName" runat="server" CssClass="input-xlarge">
                            </asp:DropDownList>
                        </td>
                        <td>
                            <asp:DropDownList ID="dropVariableName" runat="server" CssClass="input-mini">
                            </asp:DropDownList>
                        </td>
                    </tr>
                </ItemTemplate>
            </asp:Repeater>
        </table>

    </form>
</body>
</html>
