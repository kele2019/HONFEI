<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MenuRightsList.aspx.cs" Inherits="Ultimus.UWF.Security.MenuRightsList" %>
<%@ Import Namespace="Ultimus.UWF.Common.Logic" %>

<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <script type="text/javascript" src="<%=WebUtil.GetRootPath()%>/Assets/js/common.js"></script>
    <script type="text/javascript">
        function openDetail(id) {
            window.location.href = "MenuRightsDetail.aspx";
            return false;
        }
    </script>
</head>
<body>
<script type="text/javascript">
    function confirmDelete() {
        return confirm('<%=Lang.Get("SecurityList_ConfirmDelete") %>');
    }
</script>
    <form id="form1" runat="server">
    <fieldset>
        <legend><%=Lang.Get("SecurityList")  %><span class="right"><asp:Button ID="btnAdd" runat="server" Text="新增"
            CssClass="btn btn-primary" OnClientClick="return openDetail();" /></span></legend>
        <table class="table table-hover table-bordered table-condensed">
            <thead>
                <tr>
                    <th>
                        <%=Lang.Get("SecurityList_Name")  %>
                    </th>
                    <th>
                        <%=Lang.Get("SecurityList_Member") %>
                    </th>
                    <th>
                        <%=Lang.Get("SecurityList_Edit")  %>
                    </th>
                    <th>
                        <%=Lang.Get("SecurityList_Delete")  %>
                    </th>
                </tr>
            </thead>
            <tbody>
                <asp:Repeater ID="rptList" runat="server" onitemcommand="rptList_ItemCommand">
                    <ItemTemplate>
                        <tr>
                            <td>
                                <%#Eval("RIGHTSNAME")%>
                            </td>
                            <td>
                                <%#Eval("MEMBERNAME")%>
                            </td>
                            <td>
                            <a href='MenuRightsDetail.aspx?ResourceId=<%#Eval("RIGHTSID") %>'><%=Lang.Get("SecurityList_Edit")  %></a>
                            </td>
                            <td>
                                <asp:LinkButton ID="lbDelete" runat="server" OnClientClick="return confirmDelete();" CommandArgument='<%#Eval("RIGHTSID") %>' CommandName="Delete">删除</asp:LinkButton>
                            </td>
                        </tr>
                    </ItemTemplate>
                </asp:Repeater>
            </tbody>
        </table>
        <div id="pagelist">
        <span
                class="right">
                <webdiyer:AspNetPager ID="AspNetPager1" runat="server" CustomInfoHTML="Count %RecordCount%"
                    Width="100%" CssClass="aspNetPager" OnPageChanged="AspNetPager1_PageChanged"
                    AlwaysShow="true">
                </webdiyer:AspNetPager></span>
    </div>
    </fieldset>
    <div style="display: none">
    </div>
    </form>
    <%--<script type="text/javascript">
        $(document).ready(function () {
            parent.document.getElementById('frmContent').height = document.body.clientHeight;
        });
    </script>--%>
    
</body>
</html>
