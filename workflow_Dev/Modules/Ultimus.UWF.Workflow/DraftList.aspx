<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="DraftList.aspx.cs" Inherits="Ultimus.UWF.Workflow.DraftList" %>
<%@ Import Namespace="Ultimus.UWF.Common.Logic" %>

<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    
        <script type="text/javascript" src="<%=WebUtil.GetRootPath() %>/Assets/js/common.js"></script>

    <script type="text/javascript">
        function showAdv() {
            $("#divQuery").toggle();
        }

        function changeCss1(ele) {
            $(ele).css("cur");
        }

        $(document).ready(function () {
            if ($("#txtShowQuery").val() == "1") {
                $("#divQuery").show();
            }
        });

        function changeStatus(ele) {
            $("input[type='checkbox']").attr("checked", ele.checked);
        }

        function openForm(taskId, processName, formId, incident, type, ele) {
            var sheight = screen.height - 150;
            var swidth = screen.width - 10;
            var winoption = "left=0,top=0,height=" + sheight + ",width=" + swidth + ",toolbar=yes,menubar=yes,location=yes,status=yes,scrollbars=yes,resizable=yes";
            s = window.open('OpenForm.aspx?TaskId=' + taskId + '&Type=' + type + '&ProcessName=' +escape( processName) + '&FORMID=' + formId + "&incident=" + incident, '', winoption);

            s.focus();

        }
    </script>
</head>
<body>
    <style type="text/css">
        input[type='text']
        {
            height: 13px;
            width: 100px;
        }
        
        .lbl
        {
            display: inline-block;
            width: 90px;
        }
        
        .small
        {
            height: 22px;
            padding-top: 0px;
            margin-top: 0px;
        }
    </style>
    <script type="text/javascript">
        function assign() {
            var taskid = "";
            $("#tab tr").each(function () {
                if ($(this).find("td:eq(0)").children().attr("checked")) {
                    taskid += $(this).find("td:eq(0)").children().val() + ",";
                }
            });
            if (taskid != "") {
                taskid = taskid.substring(0, taskid.lastIndexOf(","));
            }
            var PageName = "Assign.aspx";
            if (taskid != "") {
                PageName += "?TaskID=" + encodeURI(taskid);
            }
            //window.showModalDialog(PageName, "javascript", "dialogHeight=300px;dialogWidth=500px;scroll=no;");
            window.open(PageName, "javascript", "");
            location.reload();
        }

        function delConfirm() {

            return confirm('<%=Lang.Get("DraftList_DeleteConfirm") %>');
        }
    </script>
    <form id="form1" runat="server">
    <fieldset><legend><% =Lang.Get("DraftList_Title")  %></legend>
    <div class="category_nav_box hidden">
        <dl class="first">
            <dt>
                <%=Lang.Get("TaskList_ProcessCategory")  %>：</dt>
            <dd>
              
            </dd>
        </dl>
     
        <div id="divQuery" class="hide">
            <dl style="padding-top: 6px; padding-bottom: 9px">
                <dt>
                    <%=Lang.Get("DraftList_DeleteConfirm")   %>：</dt>
                <dd>
                    <asp:Label ID="lblProcessName" runat="server" Text="流程名称" CssClass="lbl"></asp:Label>
                    <asp:TextBox ID="txtProcessName" CssClass="input-query" runat="server"></asp:TextBox>
                    <asp:Label ID="lblStartTime" runat="server" Text="开始时间" CssClass="lbl"></asp:Label>
                    <asp:TextBox ID="txtStartDate" CssClass="input-query" onclick="WdatePicker({skin:'whyGreen',dateFmt:'yyyy/MM/dd'})"
                        runat="server"></asp:TextBox>
                    <asp:Label ID="Label6" runat="server" Text="-" CssClass="lbl"></asp:Label>
                    <asp:TextBox ID="txtEndDate" CssClass="input-query" onclick="WdatePicker({skin:'whyGreen',dateFmt:'yyyy/MM/dd'})"
                        runat="server"></asp:TextBox>
                </dd>
            </dl>
            <dl style="padding-top: 6px; padding-bottom: 9px">
                <dd>
                    <asp:Label ID="lblIncident" runat="server" Text="实例号" CssClass="lbl"></asp:Label>
                    <asp:TextBox ID="txtIncident" CssClass="input-query" runat="server"></asp:TextBox>
                    <div class="hidden">
                        <asp:Label ID="lblApplicant" runat="server" Text="申请人" CssClass="lbl"></asp:Label>
                        <asp:TextBox ID="txtApplicant" CssClass="input-query" runat="server"></asp:TextBox></div>
                    <asp:Label ID="lblSummary" runat="server" Text="摘要" CssClass="lbl"></asp:Label>
                    <asp:TextBox ID="txtSummary" CssClass="input-query" runat="server"></asp:TextBox>
                    <%-- <asp:Button ID="btnSearch" runat="server" CssClass="btn small" Text="查询" OnClick="btnSearch_Click" />--%>
                </dd>
            </dl>
        </div>
        <%--  <dl class="sort_order">
            <dt>
                <%=Workflow.Resources.lang.TaskList_Sort %>：</dt>
            <dd>
                <a href='<%=GetSortUrl("a.STARTTIME DESC") %>' class='<%=GetSortCurCss("0") %>'>
                    <%=Workflow.Resources.lang.TaskList_Default %></a> <a href='<%=GetSortUrl("a.STARTTIME DESC") %>'
                        class='<%=GetSortCurCss("a.STARTTIME DESC") %>'>
                        <%=Workflow.Resources.lang.TaskList_StartTime %></a> <a href='<%=GetSortUrl("a.PROCESSNAME") %>'
                            class='<%=GetSortCurCss("a.PROCESSNAME") %>'>
                            <%=Workflow.Resources.lang.TaskList_ProcessName %></a>
                <%--  <a href='<%=GetSortUrl("b.INITIATOR") %>' class='<%=GetSortCurCss("b.INITIATOR") %>'><%=Workflow.Resources.lang.TaskList_Applicant %></a>--%>
        </dd> </dl>--%>
    </div>
    <div class="pt10">
    </div>
    <div>
        <table class="table table-bordered table-hover table-condensed" id="tab">
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
                    <th>
                        <%=Lang.Get("TaskList_StartTime")%>
                    </th>
                    <th>
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

                                     <a onclick="javascript:openForm('<%#Eval("TASKID") %>','<%#Eval("ProcessName") %>','<%#Eval("FORMID") %>','<%#Eval("Incident") %>','Draft',this);" style="cursor:hand">
                                    <%#Eval("PROCESSNAME")%></a>

                            </td>
                            <td>
                                <%#Eval("SUMMARY")%>
                            </td>
                            <td>
                                <%#Convert.ToDateTime(Eval("CREATEDATE")).ToString("yyyy/MM/dd HH:mm:ss")%>
                            </td>
                            <td>
                                <asp:Button ID="btnDelete" runat="server" Text="Del" CssClass="btn" CommandName="del"
                                    CommandArgument='<%#Eval("FormID") %>' ClientIDMode="Static" OnClientClick='return delConfirm();' />
                            </td>
                        </tr>
                    </ItemTemplate>
                </asp:Repeater>
            </tbody>
        </table>
    </div>
    <div id="pagelist">
        <span class="right">
            <webdiyer:AspNetPager ID="AspNetPager1" runat="server" CustomInfoHTML="Count %RecordCount%"
                Width="100%" CssClass="aspNetPager" OnPageChanged="AspNetPager1_PageChanged"
                AlwaysShow="true">
            </webdiyer:AspNetPager></span>
    </div>
    </fieldset>
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

