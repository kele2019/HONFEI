<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ApprovalHistory.aspx.cs" Inherits="MobileClient.ApprovalHistory" %>

<%@ Register Src="FormHeader.ascx" TagName="Header" TagPrefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Ultimus Mobile Client</title>
    <meta content="width=device-width, initial-scale=1.0" name="viewport" />
    <link href="../Css/CSS.css" rel="stylesheet" type="text/css" />
    <link href="../Css/bootstrap.min.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    <div class="cont" style="overflow-x:auto;overflow-y:hidden;">
        <uc1:Header ID="Header1" runat="server" />
        <table class="table table-bordered" style="border-right:0px">
        <tr>
        <th style=" width:15%; min-width:80px">步骤名</th>
        <th style=" width:15%; min-width:42px">用户</th>
        <th style=" width:15%; min-width:65px">审批时间</th>
        <th style=" width:15%; min-width:42px">操作</th>
        <th style=" width:40%; min-width:250px">审批意见</th>
        </tr>
            <asp:Repeater ID="Repeater1" runat="server">
            <ItemTemplate>
            <tr>
            <td><%#Eval("StepName")%></td>
            <td><%#Eval("UserName")%></td>
            <td><%#Eval("CreateDate")%></td>
            <td><%#Eval("Action")%></td>
            <td><%#Eval("Comments")%></td>
            </tr>
            </ItemTemplate>
            </asp:Repeater>
        </table>
    </div>
    </form>
</body>
</html>