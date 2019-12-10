<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="UserManagement.aspx.cs"
    Inherits="Ultimus.UWF.OrgChart.UserManagement" %>
<%@ Import Namespace="Ultimus.UWF.Common.Logic" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <base target="_self"></base>
    <title>User Management</title> 
         <script type="text/javascript" src="<%=WebUtil.GetRootPath()%>/Assets/js/common.js"></script>
    <script type="text/javascript">
        $(function () {
            $("input[id$=RadioButton1]").attr("name", "username");
        });
    </script>
</head>
<script type="text/javascript">
    function confirmDelete() {
        return confirm("<%=SecurityList_ConfirmDelete %>");
    }

    function addnew() {

    }
</script>
<body style="overflow-x: hidden; overflow-y: hidden;">
    <form id="form1" runat="server" defaultbutton="btnSearch">
    <div>
        <div class="pt10 pb10 pr10">
            &nbsp;&nbsp;&nbsp;&nbsp;
            <asp:TextBox ID="txtSearch" runat="server" Width="500"></asp:TextBox>&nbsp;&nbsp;&nbsp;&nbsp;<asp:Button
                ID="btnSearch" runat="server" Text="查询" CssClass="btn btn-primary" OnClick="btnSearch_Click" /><div
                    class="right">
                    <asp:Button ID="btnAdd" runat="server" Text="" CssClass="btn btn-primary" 
                        onclick="btnAdd_Click" OnClientClick="addnew();return false;" Visible="false" />
                        <a href="UserDetail.aspx?DepartmentID=<%=hidDepartmentID.Value %>&DepartmentName=<%=Server.UrlEncode( hidDepartmentName.Value) %>" target="_blank" class="btn btn-primary">新增用户</a>
                        </div>
        </div>
        <div style="float: left; width: 30%; margin-left: 5px; height: 450px; overflow: scroll;">
            <asp:TreeView ID="UserTreeView" runat="server" OnSelectedNodeChanged="UserTreeView_SelectedNodeChanged"
                OnTreeNodeCheckChanged="UserTreeView_TreeNodeCheckChanged">
                <NodeStyle BorderStyle="None"  ImageUrl="../../assets/images/DepartIcon.png" />
                <SelectedNodeStyle Font-Bold="True" ImageUrl="../../assets/images/DepartIcon.png" />
            </asp:TreeView>
        </div>
        <div style="float: right; width: 68%;">
            <div style="overflow-x: hidden; overflow-y: scroll; height: 450px;" id="divUser"
                runat="server">
                <table class=" table table-hover table-bordered table-condensed listTable" style="width: 100%;
                    overflow: hidden;">
                    <thead>
                        <tr class="bg">
                            <th width="50px" class="hidden">
                                选择
                            </th>
                            
                            
                            <th width="150px">
                                用户名
                            </th>
                            <th width="150px">
                                岗位
                            </th>
                            <th width="150px">
                                登录账号
                            </th>
                           <%-- <th width="200px">
                                主管
                            </th>--%>
                            <th width="200px">
                                操作
                            </th>
                        </tr>
                    </thead>
                    <tbody id="tbody">
                        <asp:Repeater ID="Repeater1" runat="server" 
                            OnItemDataBound="Repeater1_ItemDataBound" onitemcommand="Repeater1_ItemCommand">
                            <ItemTemplate>
                                <tr id='<%# Container.ItemIndex+1 %>' class="TableDataRow">
                                    <td width="29px" class="hidden">
                                        <asp:CheckBox ID="CheckBox1" runat="server" AutoPostBack="true" OnCheckedChanged="Button1_Click" />
                                        <asp:RadioButton ID="RadioButton1" runat="server" GroupName="username" />
                                    </td>
                                    
                                    
                                    <td width="148px">
                                        <asp:Label ID="Label1" runat="server" Text='<%# Eval("FirstName") %>'></asp:Label>
                                    </td>
                                    <td width="156px">
                                        <asp:Label ID="Label2" runat="server" Text='<%# Eval("Title") %>'></asp:Label>
                                    </td>
                                    <td width="212px">
                                        <asp:Label ID="Label3" runat="server" Text='<%# Eval("SHORTNAME") %>'></asp:Label>
                                    </td>
                                    <%--<td width="212px">
                                        <asp:Label ID="Label4" runat="server" Text='<%# Eval("FirstName") %>'></asp:Label>
                                    </td>--%>
                                    <td width="212px">
                                        <a href="UserDetail.aspx?JOBID=<%#Eval("JOBID") %>" target="_blank">编辑</a>&nbsp;&nbsp;&nbsp;&nbsp;
                                <asp:LinkButton ID="lbDelete" runat="server" OnClientClick="return confirmDelete();" CommandArgument='<%#Eval("EmployeeID") %>' CommandName="Delete"><%=Ultimus.UWF.Common.Logic.Lang.Get("SecurityList_Delete") %></asp:LinkButton>
                                    </td>
                                    <td style="display: none;">
                                        <asp:HiddenField ID="UserAccount" runat="server" Value='<%# Eval("ShortName") %>' />
                                    </td>
                                    <td style="display: none;">
                                        <asp:HiddenField ID="UserID" runat="server" Value='<%# Eval("EmployeeID") %>' />
                                    </td>
                                </tr>
                            </ItemTemplate>
                        </asp:Repeater>
                    </tbody>
                </table>
            </div>
            <div style="overflow: hidden; height: 30px; width: 545px;">
                <table class="listTable" style="width: 545px; overflow: hidden;">
                    <tr>
                        <td>
                        </td>
                    </tr>
                </table>
            </div>
            <div id="SelectedList" runat="server" visible="false" style="overflow: hidden; height: 190px;">
                <div style="height: 190px; overflow-x: hidden; overflow-y: scroll;">
                    <table class="table table-hover table-bordered table-condensed listTable" style="width: 545px;
                        overflow: hidden;">
                        <thead>
                            <tr class="bg">
                                <th width="50px">
                                    选择
                                </th>
                                <th width="150px">
                                    用户名
                                </th>
                                <th width="150px">
                                    岗位
                                </th>
                                <th width="200px">
                                    Email
                                </th>
                            </tr>
                        </thead>
                        <tbody id="tab">
                            <asp:Repeater ID="Repeater2" runat="server">
                                <ItemTemplate>
                                    <tr id='<%# Container.ItemIndex+1 %>' class="TableDataRow">
                                        <td>
                                            <asp:CheckBox ID="CheckBox2" runat="server" />
                                        </td>
                                        <td>
                                            <%# Eval("FirstName")%>
                                        </td>
                                        <td>
                                            <%# Eval("Title")%>
                                        </td>
                                        <td>
                                            <%# Eval("email") %>
                                        </td>
                                        <td style="display: none;">
                                            <asp:HiddenField ID="UserAccount" runat="server" Value='<%# Eval("ShortName") %>' />
                                        </td>
                                        <td style="display: none;">
                                            <asp:HiddenField ID="UserID" runat="server" Value='<%# Eval("EmployeeID") %>' />
                                        </td>
                                    </tr>
                                </ItemTemplate>
                            </asp:Repeater>
                        </tbody>
                    </table>
                </div>
            </div>
        </div>
    </div>
    <asp:HiddenField ID="hidDepartmentID" runat="server" />
    <asp:HiddenField ID="hidDepartmentName" runat="server" />
    <asp:HiddenField ID="hidSelectType" runat="server" />
    </form>
</body>
</html>
