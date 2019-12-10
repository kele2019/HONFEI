<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Groups.aspx.cs" Inherits="Ultimus.UWF.OrgChart.Groups" %>
<%@ Import Namespace="Ultimus.UWF.Common.Logic" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>组维护界面</title>
    <script type="text/javascript" src="js/jquery.js"></script>
    <script type="text/javascript" src="js/selector.js"></script>
    <script type="text/javascript" src="<%=WebUtil.GetRootPath()%>/Assets/js/common.js"></script>
    <script type="text/javascript" language="javascript">
        function addDeptClick() {
            selectUser(4, "txtChoosedDepts", "txtChoosedDeptids");
            __doPostBack('addDept', '');
        }
        function addUserClick() {
            selectUser(2, "txtChoosedDepts", "txtChoosedDeptids");
            __doPostBack('addDept', '');
        }
        function addExUserClick() {
            selectUser(2, "txtChoosedDepts", "txtChoosedDeptids");
            __doPostBack('addExUser', '');
        }
    </script>
    <style type="text/css">
        .listbox
        {
            border: 1px solid gray;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <strong><font color="#666666" face="Arial" style="font-size: 9pt">组列表</strong>
    <div style="float: left; overflow: scroll; border: 1px solid; width: 300px; height: 480px">
        <asp:TreeView ID="tvGroups" runat="server" ShowExpandCollapse="true" ShowLines="true"
            OnSelectedNodeChanged="tvGroups_SelectedNodeChanged">
            <SelectedNodeStyle Font-Bold="true" />
        </asp:TreeView>
    </div>
    <div style="float: none; margin-left: 10px; left: 20px; position: relative;">
        <table>
            <tr>
                <td>
                    组名：
                    <div>
                        <asp:TextBox ID="txtGroupName" runat="server"></asp:TextBox><asp:HiddenField ID="hdGid"
                            runat="server" />
                        <asp:Button ID="btnAddNewGroup" runat="server" Text="添加组" OnClick="btnAddNewGroup_Click"
                            CssClass="btn" />
                    </div>
                </td>
            </tr>
            <tr>
                <td style="left: 20px;">
                    组成员：<div style="border: 1px solid gray; width: 400px; height: 200px;">
                        <asp:ListBox ID="lbContain" runat="server" Width="390" Height="160" CssClass="listbox">
                        </asp:ListBox>
                        <input type="button" id="btnExAddDept" value="添加部门" onclick="addDeptClick()" class="btn" />
                        <input type="button" id="btnAddUser" value="添加用户" onclick="addUserClick()" class="btn" />
                        <asp:Button ID="btnDelMem" runat="server" Text="删除成员" OnClick="btnDelMem_Click" CssClass="btn" />
                    </div>
                </td>
            </tr>
            <tr>
                <td>
                    组排除成员：<div style="border: 1px solid gray; width: 400px; height: 110px;">
                        <asp:ListBox ID="lbExclude" runat="server" Width="390" Height="70" Style="border-style: solid;">
                        </asp:ListBox>
                        <input type="button" id="btnExAddUser" value="添加成员" onclick="addExUserClick()" class="btn" />
                        <asp:Button ID="btnDelExMem" Text="删除成员" runat="server" OnClick="btnDelExMem_Click"
                            CssClass="btn" />
                    </div>
                </td>
            </tr>
            <tr>
                <td>
                    <br />
                    <asp:Button ID="btnSave" runat="server" Text="保存组" OnClick="btnSave_Click" OnClientClick="return saveclick();"
                        CssClass="btn btn-warning" />
                    <asp:Button ID="btnDel" runat="server" Text="删除组" OnClick="btnDel_Click" CssClass="btn btn-warning"
                        OnClientClick=" return delClick();" />
                    <script type="text/javascript" language="javascript">
                        function saveclick() {

                            if ($.trim($("#txtGroupName").val()) == "") {
                                alert("请输入组名！");
                                return false;
                            }
                            return confirm('确定保存？');
                        }
                        function delClick() {
                            if ($("#hdGid").val() == undefined || $("#hdGid").val() == "") {
                                alert('请选择所要删除的组！');
                            }
                            return confirm('确定删除？');
                        }
                    </script>
                </td>
            </tr>
        </table>
    </div>
    <div style="display: none;">
        <asp:TextBox ID="txtChoosedDepts" runat="server"></asp:TextBox>
        <asp:TextBox ID="txtChoosedDeptids" runat="server"></asp:TextBox>
        <asp:LinkButton ID="addDept" runat="server" Text="添加组" OnClick="addDept_Click"></asp:LinkButton>
        <asp:LinkButton ID="addExUser" runat="server" Text="添加排除成员" OnClick="addExUser_Click"></asp:LinkButton>
    </div>
    </form>
</body>
</html>
