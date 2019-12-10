<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Header.ascx.cs" Inherits="MobileClient.Header" %>
 <%--<script src="cordova.js"></script>--%>
<div class=" lt04">
    <table width="100%" border="0" cellspacing="0" cellpadding="0" height="5px">
        <tr>
            <td width="33%" bgcolor="#f56b00">
            </td>
            <td width="34%" bgcolor="#00a1c8">
            </td>
            <td width="33%" bgcolor="#76b817">
            </td>
        </tr>
    </table>
</div>
<div class="lt2" style="clear: both;">
    <table width="100%" border="0" cellpadding="0" cellspacing="0">
        <tr>
            <td width="1%">
            </td>
            <td width="6%">
                <a href="ToDoTask.aspx">
                    <img src="images/logo_2.png" width="52" height="52" border="0px" /></a>
            </td>
            <td width="6%">
                <a href="javascript:void(0);" onclick="history.go(-1);">
                    <img src="images/back.png" width="52" height="52" border="0px" /></a>
            </td>
            <td align="center">
                <asp:Literal ID="litTitle" runat="server"></asp:Literal>
            </td>
            <td width="13%">
                <img style="border:0" src="images/home.png" width="36" height="36" onclick='cordova.exec(null,null, "ToastPlugin", "gohome", [])'/>
            </td>
        </tr>
    </table>
</div>
<div class="lt03">    
    <div class="<%=lt2 %>" style="width:49%;margin-left:5px;">
        <a href="ToDoTask.aspx" class="<%=nav2 %>">
            <%= Resources.Resource.NewTask_ToDoTask %></a></div>
    <div class="<%=lt3 %>" style="width:49%;">
        <a href="AlreadyDoTask.aspx" class="<%=nav3 %>">
            <%= Resources.Resource.NewTask_AlreadyDoTask %></a></div>
</div>
