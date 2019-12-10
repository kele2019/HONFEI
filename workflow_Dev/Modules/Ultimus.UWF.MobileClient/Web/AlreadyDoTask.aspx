<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AlreadyDoTask.aspx.cs"
    Inherits="MobileClient.AlreadyDoTask" %>

<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>
<%@ Register src="Header.ascx" tagname="Header" tagprefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Ultimus Mobile Client</title>
    <meta content="width=device-width, initial-scale=1.0" name="viewport" />
    <link href="Css/CSS.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" language="javascript">
        function processSumHide(obj) {
            if (obj.value == "流程摘要") obj.value = "";
        }
        function processSumShow(obj) {
            if (obj.value == "") obj.value = "流程摘要";
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div class="cont">
               <uc1:Header ID="Header1" runat="server" />
        <div class="lt5" width="100%">
            <table width="100%" border="0" cellpadding="0" cellspacing="0" align="center">
                <%--<tr>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                </tr>--%>
                <tr>
                    <td width="15%" class="hide"><%= Resources.Resource.ToDoTask_ProcessName %></td>
                    <td width="35%" class="hide">
                        <div class="inputform" style="clear: both">
                            <asp:TextBox ID="txtProcessName" runat="server" Style="background-color: transparent;
                                height: 35px; border: 0px; font-size: 14px; font-family: 黑体, Aria; color: #7d8a93;
                                padding-top: 5px; padding-left: 10px;"></asp:TextBox>
                        </div>
                    </td>
                    <td width="20%" class="hide">&nbsp;&nbsp;<%= Resources.Resource.ToDoTask_Incident %></td>
                    <td width="35%" class="hide">
                        <div class="inputform" style="clear: both">
                            <asp:TextBox ID="txtIncident" runat="server" Style="background-color: transparent;
                                height: 35px; border: 0px; font-size: 14px; font-family: 黑体, Aria; color: #7d8a93;
                                padding-top: 5px; padding-left: 10px;"></asp:TextBox>
                        </div>
                    </td>
                </tr>
                <%--<tr>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                    <td colspan="4">&nbsp;&nbsp;<%= Resources.Resource.ToDoTask_Summary %></td>
                </tr>--%>
                <tr>
                    <td class="hide"><%= Resources.Resource.ToDoTask_StepName %></td>
                    <td class="hide">
                        <div class="inputform" style="clear: both">
                            <asp:TextBox ID="txtStepName" runat="server" Style="background-color: transparent;
                                height: 35px; border: 0px; font-size: 14px; font-family: 黑体, Aria; color: #7d8a93;
                                padding-top: 5px; padding-left: 10px;"></asp:TextBox>
                        </div>
                    </td>
                    <td>&nbsp;&nbsp;</td>
                    <td>
                        <div class="inputform" style="clear: both;margin-top:4px;margin-bottom:4px;">
                            <asp:TextBox ID="txtSummary" runat="server" Style="background-color: transparent;
                                height: 35px; border: 0px; font-size: 14px; font-family: 黑体, Aria; color: #7d8a93;
                                padding-top: 5px; padding-left: 10px;" Text="流程摘要" onfocus="processSumHide(this);" onblur="processSumShow(this);"></asp:TextBox>
                        </div>
                    </td>
                    <td>
                        <asp:ImageButton ID="ImageButton1" runat="server" ImageUrl="images/Search.png" Width="101" Height="46" onclick="ImageButton1_Click"/>
                    </td>
                </tr>
                <%--<tr>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                </tr>
                <tr>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                </tr>--%>
                <%--<tr>
                    <td>
                       
                    </td>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                </tr>--%>
            </table>
        </div>
        <div class="lt5">
            <table border="0" cellpadding="0" cellspacing="0" width="100%" class="datalist">
                <tbody>
                    <asp:Repeater ID="Repeater1" runat="server">
                        <ItemTemplate>
                            <tr>
                                <td   class="lt6">
                                    <table class="lt7"   border="0" cellpadding="0" cellspacing="0">
                                        <tr>
                                            <td width="10%" align="center">
                                               <a href="ProcessMonitoring.aspx?ProcessName=<%# Server.UrlEncode(Eval("processname").ToString()) %>&Incident=<%# Eval("incident") %>">
                                                    <p>
                                                        <img src="images/jiankong.png" style="border:0" width="36" height="36" /></p>
                                                    <p style="color: #7d8a93; font-size: 14px;">
                                                        <%= Resources.Resource.ToDoTask_ProcessMonitoring %>
                                                    </p>
                                                </a>
                                            </td>
                                            <td width="405">
                                                <table width="100%" height="80" border="0" cellpadding="0" cellspacing="0">
                                                    <tr>
                                                        <td colspan="2">
                                                             <a href="OpenForm.aspx?ProcessName=<%# Server.UrlEncode(Eval("processname").ToString()) %>&StepName=<%#Server.UrlEncode(Eval("StepLabel").ToString()) %>&Incident=<%# Eval("Incident") %>&TaskId=<%# Eval("TaskId") %>&YB=1">
                                                            <%# Eval("processname") %></a>
                                                        </td>
                                                        
                                                    </tr>
                                                    <tr>
                                                        <td colspan="2">
                                                            <font style="color: #7d8a93; font-size: 14px;">
                                                            申请人：&nbsp;&nbsp;&nbsp;
                                                            <%# Eval("APLICANT")%></font>&nbsp;
                                                            <font style="color: #7d8a93; font-size: 14px;">
                                                            <%= Resources.Resource.ToDoTask_Incident %>：
                                                            <%# Eval("incident") %></font>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td colspan="2">
                                                        <font style="color: #7d8a93; font-size: 14px;">
                                                                接收时间：
                                                                <%# Eval("STARTTIME")%></font>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td colspan="2">
                                                            <font style="color: #7d8a93; font-size: 14px;">
                                                                <%= Resources.Resource.ToDoTask_StepName %>：
                                                                <%# Eval("StepLabel")%></font>
                                                        </td>
                                                        <td class="hide">
                                                            <font style="color: #7d8a93; font-size: 14px;">
                                                                <a href="javascript:void(0);">
                                                                    <%= Resources.Resource.ToDoTask_OpenForm %></a></font>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td colspan="2">
                                                            <font style="color: #7d8a93; font-size: 14px;">
                                                                <%= Resources.Resource.ToDoTask_Summary %>：
                                                                <%# Eval("summary") %></font>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                        </ItemTemplate>
                    </asp:Repeater>
                </tbody>
                <tfoot>
                    <tr>
                        <td width="479" class="lt6">
                            <asp:Button ID="Button1" runat="server" Text="加载更多"   onclick="Button1_Click" />
                        </td>
                    </tr>
                </tfoot>
            </table>
        </div>
    </div>
    </form>
</body>
</html>
