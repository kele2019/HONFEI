<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SelectUserFromAD.aspx.cs" Inherits="Ultimus.UWF.OrgChart.SelectUserFromAD" %>
<%@ Import Namespace="Ultimus.UWF.Common.Logic" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <base target="_self"></base>
    <meta http-equiv="Expires" content="-1" />
    <meta http-equiv="PRAGMA" content="NO-CACHE" />
    <meta http-equiv="Cache-Control" content="no-cache" />
    <title>选择用户</title>
     
    <script type="text/javascript" src="<%=WebUtil.GetRootPath()%>/Assets/js/common.js"></script>
    <script type="text/javascript">
        $(function () {
            $("input[id$=RadioButton1]").attr("name", "username");
            $("#txtSearch").focus();
        });
    </script>
</head>
<body style="overflow-x: hidden; overflow-y: hidden;">
    <form id="form1" runat="server" defaultbutton="btnSearch">
    <div>
        <div class="pt10 pb10 pr10">
            &nbsp;&nbsp;&nbsp;&nbsp;
            <asp:TextBox ID="txtSearch" runat="server" Width="500"></asp:TextBox>&nbsp;&nbsp;&nbsp;&nbsp;<asp:Button
                ID="btnSearch" runat="server" Text="查询" CssClass="btn btn-primary" OnClick="btnSearch_Click" /></div>
        <div style="float: left; width: 240px; margin-left: 5px; height: 450px; overflow: scroll;">
            <asp:TreeView ID="UserTreeView" runat="server" OnSelectedNodeChanged="UserTreeView_SelectedNodeChanged"
                OnTreeNodeCheckChanged="UserTreeView_TreeNodeCheckChanged" ShowExpandCollapse="true">
                <NodeStyle BorderStyle="None"  ImageUrl="../../assets/images/DepartIcon.png" />
                <SelectedNodeStyle Font-Bold="True" ImageUrl="../../assets/images/DepartIcon.png" />
            </asp:TreeView>
        </div>
        <div style="float: left; width: 545px; height: 450px;">
            <div style="overflow-x: hidden; overflow-y: scroll; height: 210px;" id="divUser" 
                runat="server">
                <table class=" table table-hover table-bordered table-condensed listTable" style="width: 545px;
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
                                账号
                            </th>
                            <th width="200px">
                                部门
                            </th>
                        </tr>
                    </thead>
                    <tbody id="tbody">
                        <asp:Repeater ID="Repeater1" runat="server" OnItemDataBound="Repeater1_ItemDataBound">
                            <ItemTemplate>
                                <tr id='<%# Container.ItemIndex+1 %>' class="TableDataRow">
                                    <td width="29px">
                                        <asp:CheckBox ID="CheckBox1" runat="server" AutoPostBack="true" OnCheckedChanged="Button1_Click" />
                                        <asp:RadioButton ID="RadioButton1" runat="server" GroupName="username" />
                                    </td>
                                    <td width="148px">
                                        <asp:Label ID="Label1" runat="server" Text='<%# Eval("FirstName") %>'></asp:Label>
                                    </td>
                                    <td width="156px">
                                        <asp:Label ID="Label2" runat="server" Text='<%# Eval("ShortName") %>'></asp:Label>
                                    </td>
                                    <td width="212px">
                                        <asp:Label ID="Label3" runat="server" Text='<%# Eval("DepartmentName") %>'></asp:Label>
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
                            <asp:Button ID="Button1" Text="选择" runat="server" CssClass="btn btn-warning" OnClientClick="return CheckSelectItem()"
                                OnClick="Button1_Click" Height="25" Visible="false" />
                            <script type="text/javascript" language="javascript">
                                function CheckSelectItem() {
                                    var isok = false;
                                    $("#tbody").find("tr").each(function () {
                                        if ($(this).find("td:eq(0)").children().attr("checked")) {
                                            isok = true;
                                        }
                                    });
                                    if (!isok) {
                                        return false;
                                    } else {
                                        return true;
                                    }
                                }
                            </script>
                            <div class="left" style="display:inline;position:absolute;"><asp:Button ID="Button2" Text="取消" runat="server" CssClass="btn" OnClientClick="return Cancel()"
                                OnClick="Button2_Click" Height="25" Visible="false" /></div>
                            <script type="text/javascript" language="javascript">
                                function Cancel() {
                                    var isok = false;


                                    $("#tab").find("tr").each(function () {
                                        if ($(this).find("td:eq(0)").children().attr("checked")) {
                                            isok = true;
                                        }
                                    });
                                    if (!isok) {
                                    } else {
                                        if (confirm("您确定要取消吗？")) {
                                            return true;
                                        }
                                    }
                                    return false;
                                }
                            </script>
                            <%--<input type="button" value="确定" class="btn  btn-primary" onclick="Confirm()"  style="height:25px;"/>--%>
                            <div class="right" style="margin-top:-0px;position:absolute;left:600px;">
                                <asp:Button ID="btnOK" runat="server" Text="确 定" OnClick="btnOK_Click" CssClass="btn btn-primary "
                                    Style="height: 25px;" OnClientClick="return SinglePersonConfirm('');" />
                                <input type="button" value="关闭" class="btn" onclick="CloseForm()" style="height: 25px;" /></div>
                            <script type="text/javascript" language="javascript">
                                function SinglePersonConfirm(tnJson) {
                                    if ($("#hidSelectType").val() == "1") {
                                    }
                                    else {
                                        return true;
                                    }
                                    var returnJson = "";
                                    returnJson += "[";

                                    var isok = false;
                                    $("#tbody").find("tr").each(function () {
                                        if ($(this).find("td:eq(0)").children().attr("checked")) {
                                            returnJson += "{'Name':'" + $.trim($(this).find("td:eq(1)").text()) + "[USER]',";
                                            returnJson += "'Type':'USER',";
                                            returnJson += "'Job':'" + $.trim($(this).find("td:eq(3)").text()) + "',";
                                            returnJson += "'LoginName':'" + $.trim($(this).find("td:eq(2)").text()) + "',";
                                            returnJson += "'ID':'" + $.trim($(this).find("td:eq(5)").children().val()) + "'},";
                                        }
                                    });

                                    returnJson = returnJson + tnJson;
                                    if (returnJson.lastIndexOf(",") > 0) {
                                        returnJson = returnJson.substring(0, returnJson.lastIndexOf(","));
                                    }

                                    returnJson += "]";
                                    window.returnValue = returnJson;
                                    window.close();
                                }

                                function Confirm(tnJson) {
                                    var returnJson = "";
                                    returnJson += "[";
                                    $("#tab tr").each(function () {
                                        returnJson += "{'Name':'" + $.trim($(this).find("td:eq(1)").text()) + "[USER]',";
                                        returnJson += "'Type':'USER',";
                                        returnJson += "'Job':'" + $.trim($(this).find("td:eq(3)").text()) + "',";
                                        returnJson += "'LoginName':'" + $.trim($(this).find("td:eq(2)").text()) + "',";
                                        returnJson += "'ID':'" + $.trim($(this).find("td:eq(5)").children().val()) + "'},";
                                    });
                                    //debugger;
                                    returnJson = returnJson + tnJson;
                                    if (returnJson.lastIndexOf(",") > 0) {
                                        returnJson = returnJson.substring(0, returnJson.lastIndexOf(","));
                                    }

                                    returnJson += "]";

                                    window.returnValue = returnJson;
                                    window.close();
                                }
                            </script>
                            <script type="text/javascript" language="javascript">
                                function CloseForm() {
                                    window.close();
                                }
                            </script>
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
                                    账号
                                </th>
                                <th width="200px">
                                    部门
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
                                            <%# Eval("ShortName")%>
                                        </td>
                                        <td>
                                            <%# Eval("DepartmentName")%>
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
    <asp:HiddenField ID="hidSelectType" runat="server" />
    </form>
</body>
</html>
