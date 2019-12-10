<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="DraftList2.aspx.cs" Inherits="Ultimus.UWF.Workflow.DraftList2" %>

<%@ Import Namespace="Ultimus.UWF.Common.Logic" %>
<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <script type="text/javascript" src="<%=WebUtil.GetRootPath() %>/Assets/js/common.js"></script>
    <script type="text/javascript" src="<%=WebUtil.GetRootPath() %>/Assets/js/listpage.js"></script>
    <script type="text/javascript">
        function openForm(taskId, processName, formId, incident, type,serverName, ele) {
            var sheight = screen.height - 150;
            var swidth = screen.width - 10;
            var winoption = "left=0,top=0,height=" + sheight + ",width=" + swidth + ",toolbar=yes,menubar=yes,location=yes,status=yes,scrollbars=yes,resizable=yes";
            s = window.open('OpenForm.aspx?ServerName='+serverName+'&TaskId=' + taskId + '&Type=' + type + '&ProcessName=' + processName + '&FORMID=' + formId + "&incident=" + incident, '', winoption);

            s.focus();

        }

        function delConfirm() {

            return confirm('<%=Lang.Get("DraftList_DeleteConfirm") %>');
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div id="right">
        <div class="tabBtn">
            <ul style="height: 40px;">
                <li id="Li2" class='normalLi1 searchli_new tbtn' style="float: left; padding-left: 10px;">
                    <strong style="font-size: medium" onclick="location.href=location.href;return false;">
                        我的草稿箱</strong> </li>
                <li id="Li1" class='normalLi1 searchli_new ' style="float: right">
                    <p>
                        <asp:Button ID="Button2" runat="server" CssClass="btn tbtn" Text="刷新任务" OnClientClick="location.href=location.href;return false;" />
                    </p>
                </li>
            </ul>
        </div>
        <table id="infoTab" class="tab" width="100%" border="0" cellpadding="5" cellspacing="0">
            <thead>
                <tr>
                    <th class="hide">
                        <asp:CheckBox ID="cbAll" runat="server" onclick="changeStatus(this);" />
                    </th>
                    <th>
                        <%=Lang.Get("TaskList_ProcessName") %>
                    </th>
                    <th>
                        <%=Lang.Get("TaskList_Summary") %>
                    </th>
                    <th class="hide">
                        <%=Lang.Get("TaskList_StartTime")%>
                    </th>
                    <th width="100">
                        <%=Lang.Get("DraftList_Operate") %>
                    </th>
                </tr>
            </thead>
            <tbody>
                <asp:Repeater ID="rptTask" runat="server" OnItemCommand="rptTask_ItemCommand">
                    <ItemTemplate>
                        <tr>
                            <td style="width: 20px;" class="hide">
                                <asp:CheckBox ID="cbSelect" runat="server" />
                            </td>
                            <td>
                                <%-- <a href="OpenForm.aspx?FORMID=<%#Eval("FORMID") %>&ProcessName=<%#GetUrl(Eval("ProcessName"))%>&Incident=<%#Eval("Incident")%>&Type=Draft&TASKID=<%#Eval("TASKID")%>"
                                    target="_blank">
                                    <%#Eval("PROCESSNAME")%></a>--%>
                                <a onclick="javascript:openForm('<%#Eval("TASKID") %>','<%#Eval("ProcessName") %>','','','MyTask','<%#Eval("ServerName") %>',this);"
                                    style="cursor: hand">
                                    <%#Eval("PROCESSNAME")%></a>
                            </td>
                            <td>
                                <%#Eval("SUMMARY")%>
                            </td>
                            <td class="hide">
                                <%#Convert.ToDateTime(Eval("StartTime")).ToString("yyyy/MM/dd HH:mm:ss")%>
                            </td>
                            <td>
                                <asp:Button ID="btnDelete" runat="server" Text="删除" CssClass="btn" CommandName="del"
                                    CommandArgument='<%#Eval("SERVERTASKID") %>' ClientIDMode="Static" OnClientClick='return delConfirm();' />
                            </td>
                        </tr>
                    </ItemTemplate>
                </asp:Repeater>
            </tbody>
            <tfoot>
                <tr class="pageTr">
                    <td colspan="10">
                        <div id="pageBar">
                            <div id="pageLeft">
                            </div>
                            <div id="pageRight">
                                <webdiyer:AspNetPager ID="AspNetPager1" runat="server" CustomInfoHTML="Count %RecordCount%"
                                    Width="100%" CssClass="aspNetPager" OnPageChanged="AspNetPager1_PageChanged"
                                    AlwaysShow="true">
                                </webdiyer:AspNetPager>
                            </div>
                        </div>
                    </td>
                </tr>
            </tfoot>
        </table>
    </div>
    <div style="display: none">
        <asp:TextBox ID="txtType" runat="server"></asp:TextBox>
        <asp:TextBox ID="txtPreSort" runat="server"></asp:TextBox>
        <asp:TextBox ID="txtSort" runat="server"></asp:TextBox>
        <asp:TextBox ID="txtStartTime" runat="server"></asp:TextBox>
        <asp:TextBox ID="txtDateType" runat="server"></asp:TextBox>
        <asp:TextBox ID="txtShowQuery" runat="server"></asp:TextBox>
        <asp:TextBox ID="txtProcessCategory" runat="server"></asp:TextBox>
    </div>
    </form>
</body>
</html>
