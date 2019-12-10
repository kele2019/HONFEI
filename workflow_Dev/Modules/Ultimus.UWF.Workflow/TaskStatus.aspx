<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="TaskStatus.aspx.cs" Inherits="Ultimus.UWF.Workflow.TaskStatus" %>
<%@ Import Namespace="Ultimus.UWF.Common.Logic" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>
        <%=Lang.Get("TaskStatus_Title") %></title>
        <script type="text/javascript" src="/Assets/js/common.js"></script>

    <script language="javascript" type="text/javascript">
        self.moveTo(0, 0)
        self.resizeTo(screen.availWidth, screen.availHeight)
        function closeWin() {
            window.opener = null;
            window.open('', '_self');
            window.close();
            return false;
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div class="container-fluid">
        <fieldset>
            <legend  class="<%=HIDDEN %>"><strong>
                <%=Lang.Get("TaskStatus_ApprovalHistory") %>
            </strong></legend>
            <table class="table table-bordered table-striped table-condensed <%=HIDDEN %>" >
                <tr>
                    <th>
                        <%=Lang.Get("TaskStatus_StepName")  %>
                    </th>
                    <th>
                        <%=Lang.Get("TaskStatus_Approver") %>
                    </th>
                    <th>
                        <%=Lang.Get("TaskStatus_StartTime") %>
                    </th>
                    <th>
                        <%=Lang.Get("TaskStatus_EndTime") %>
                    </th>
                    <th>
                        <%=Lang.Get("TaskStatus_Status") %>
                    </th>
                </tr>
                <asp:Repeater ID="rptTaskList" runat="server">
                    <ItemTemplate>
                        <tr>
                            <td>
                                <%# Eval("StepName")%>
                            </td>
                            <td>
                                <%# Eval("StepUser")%>
                            </td>
                            <td>
                                <%# Eval("StartTime")%>
                            </td>
                            <td>
                                <%# Eval("EndTime")%>
                            </td>
                            <td>
                                <%# Eval("Status")%>
                            </td>
                        </tr>
                    </ItemTemplate>
                </asp:Repeater>
            </table>
            <div class="offset7"> 
            <asp:Button ID="btnClose" runat="server" Text="Close" CssClass="btn btn-primary  "  OnClientClick="return closeWin();" /></div>
        </fieldset>
        <fieldset>
            <legend><strong>
                <%=Lang.Get("TaskStatus_FlowChart")%>
            </strong></legend>
            <iframe id="rightframe" name="rightframe" hspace="0" vspace="0" src="GraphicalView.aspx?ProcessName=<%=Server.UrlEncode(Request.QueryString["ProcessName"]) %>&Incident=<%=Request.QueryString["Incident"] %>"
                frameborder="0" width="98%" height="600"></iframe>
        </fieldset>
    </div>
    </form>
</body>
</html>


