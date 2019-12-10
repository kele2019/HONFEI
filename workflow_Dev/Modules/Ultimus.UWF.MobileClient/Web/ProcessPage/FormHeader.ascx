<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="FormHeader.ascx.cs" Inherits="MobileClient.FormHeader" %>

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
            <td width="16%">
                <a href="../ToDoTask.aspx">
                    <img src="../images/logo_2.png" width="52" height="52" border="0px" /></a>
            </td>
            <td width="16%">
                <a href="javascript:void(0);" onclick="history.go(-1);">
                    <img src="../images/back.png" width="52" height="52" border="0px" /></a>
            </td>
            <td align="center">
                <asp:Literal ID="litTitle" runat="server"></asp:Literal>
            </td>
            <td width="13%">
            </td>
        </tr>
    </table>
</div>
<div class="lt03">
    <div class="<%=lt1 %>" style="width:32%;margin-left:5px;">
        <a href="<%=Request.QueryString["StepID"] %>.aspx?ProcessName=<%=Request.QueryString["ProcessName"] %>&Incident=<%=Request.QueryString["Incident"] %>&TaskID=<%=Request.QueryString["TaskID"] %>&StepName=<%=Request.QueryString["StepName"] %>&YB=<%=Request.QueryString["YB"] %>&StepID=<%=Request.QueryString["StepID"] %>" class="<%=nav1 %>">
            <%= Resources.Resource.Form_Text%></a></div>
    <div class="<%=lt2 %>" style="width:32%;">
        <a href="ApprovalHistory.aspx?ProcessName=<%=Request.QueryString["ProcessName"] %>&Incident=<%=Request.QueryString["Incident"] %>&TaskID=<%=Request.QueryString["TaskID"] %>&StepName=<%=Request.QueryString["StepName"] %>&YB=<%=Request.QueryString["YB"] %>&StepID=<%=Request.QueryString["StepID"] %>" class="<%=nav2 %>">
            <%= Resources.Resource.Form_ApprovalHistory%></a></div>
    <div class="<%=lt3 %>" style="width:32%;">
        <a href="FlowChart.aspx?ProcessName=<%=Request.QueryString["ProcessName"] %>&Incident=<%=Request.QueryString["Incident"] %>&TaskID=<%=Request.QueryString["TaskID"] %>&StepName=<%=Request.QueryString["StepName"] %>&YB=<%=Request.QueryString["YB"] %>&StepID=<%=Request.QueryString["StepID"] %>" class="<%=nav3 %>">
            <%= Resources.Resource.Form_FlowChart%></a></div>
</div>
