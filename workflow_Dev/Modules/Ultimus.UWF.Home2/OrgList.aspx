<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="OrgList.aspx.cs" Inherits="Ultimus.UWF.Home2.OrgList" %>

<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <link href="css/workflow.css" rel="stylesheet" type="text/css" />
    <style>
        body{margin:0px;}
        .tugl
        {
            float:right;
            line-height:38px;
            vertical-align:middle;
        }
        .tugl a
        {
            color:#003aff;
        }
        .tugl a:hover
        {
            color:#003aff;
        }
        .cb
        {
            clear:both;
        }
        .topbar
        {
            border: #DFDFDF 1px solid;
            background-color:#F5F5F5;
            line-height:42px;
            vertical-align:middle;
        }
        .tb
        {
            width:120px;
            border: #DFDFDF 1px solid;
            padding-left:4px;
        }
        .td-l
        {
            padding-left:10px;
            padding-right:5px;
        }
        .btnImg
        {
            width:70px;
            vertical-align:middle;
            margin-top:-4px;
            border: #DFDFDF 1px solid;
        }
        .cGrid
        {
            padding-top:10px;
        }
        .tblList
        {
            table-layout: fixed;
            width:100%;
        }
        .tblList th
        {
            text-align:left;
            padding-left:20px;
            background-color:#EEEEEE;
            height:42px;
        }
        .tblList tbody td
        {
            padding-left:10px;
        }
        .tblList tbody tr
        {
            height:40px;
        }
        .dot
        {
            text-overflow: ellipsis; /* for IE */
            -moz-text-overflow: ellipsis; /* for Firefox,mozilla */
            overflow: hidden;
            white-space: nowrap;
        }
        .btnAction a
        {
            color:#003aff;
        }
        .btnAction a:hover
        {
            color:#003aff;
        }
    </style>
    <script src="js/jquery.min.js"></script>
    <script>
        function createOrg(parentID) {
            location.href = "OrgEdit.aspx?ParentID=" + parentID + "&ReturnUrl=" + encodeURIComponent(location.href);
        }
        function editOrg(id) {
            location.href = "OrgEdit.aspx?ID=" + id + "&ReturnUrl=" + encodeURIComponent(location.href);
        }
        function removeOrg(id) {
            if (!confirm("您确定要删除此部门吗？"))
                return;
            $.ajax({
                url: 'AjaxOrgUser.aspx?Method=delorg&id=' + id, // 跳转到 action  
                type: 'post',
                cache: false,
                dataType: 'json',
                success: function (data) {
                    if (data.Code == 0) {
                        parent.window.deleteOrgNode(id);
                        window.location.reload();
                    } else {
                        alert(data.ErrorMsg);
                    }
                },
                error: function () {
                    alert("删除数据时发生错误！");
                }
            });
        }

        $(document).ready(function () {
        var DBRWStr = $(window.parent.$("#orgTree"));
        $(DBRWStr).children().eq(1).hide();
        });
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <div class="tugl">
            <a href="UserList.aspx?ParentID=<%=ParentID %>">UserManager</a>
        </div>
        <div class="cb"></div>
        <div class="topbar">
            <table border="0" cellpadding="0" cellspacing="0">
                <tr>
                    <td width="20px">&nbsp;</td>
                    <td class="td-l">DepartmentName</td>
                    <td>
                        <asp:TextBox ID="txtQueryName" runat="server" CssClass="tb" />
                    </td>
                    <td class="td-l">CostCenter</td>
                    <td>
                        <asp:TextBox ID="txtQueryCBCenter" runat="server" CssClass="tb" />
                    </td>
                    <td class="td-l">
                        <asp:ImageButton ID="btnImg" ImageUrl="images/v2/search.png" runat="server" 
                            CssClass="btnImg" onclick="btnImg_Click" />
                        <a href="javascript:createOrg(<%=ParentID%>);"><img class="btnImg" src="images/v2/add.png" border="0" /></a>
                    </td>
                </tr>
            </table>
        </div>
        <div class="cGrid">
            <table cellpadding="0" cellspacing="0" border="0" class="tblList">
                <thead>
                    <tr class="tblListHeaderTr">
                        <th width="10%">No</th>
                        <th  width="40%" style="text-align:center" >DepartmentName</th>
                      <%--  <th width="150px">英文名称</th>
                        <th width="130px">部门级别</th>
                        <th width="130px">上级部门</th>--%>
                        <th width="40%" style="text-align:center" >CostCenter</th>
                        <th width="10%">Operator</th>
                    </tr>
                </thead>
                <tbody>
                    <asp:Repeater ID="rpt" runat="server">
                        <ItemTemplate>
                            <tr>
                                <td><%#Container.ItemIndex * PageIndex + 1%></td>
                                <td style="text-align:center" class="dot"><%#Eval("DEPARTMENTNAME")%></td>
                              <%--  <td class="dot"><%#Eval("EngName")%></td>
                                <td class="dot"><%#Eval("Level")%></td>
                                <td class="dot"><%#this.GetName(Eval("PARENTID"))%></td>--%>
                                <td style="text-align:center" class="dot"><%#Eval("CBCenter")%></td>
                                <td class="dot btnAction">
                                    <a href="javascript:void(0);" onclick="editOrg(<%#Eval("DEPARTMENTID")%>);">Edit</a>
                                   <%-- <a href="javascript:void(0);" onclick="removeOrg(<%#Eval("DEPARTMENTID")%>)">删除</a>--%>
                                </td>
                            </tr>
                        </ItemTemplate>
                    </asp:Repeater>
                    <asp:PlaceHolder ID="plcEmpty" runat="server">
                        <tr>
                            <td colspan="4">没有数据！</td>
                        </tr>
                    </asp:PlaceHolder>
                </tbody>
            </table>
            <div  class="pagelist"  style="border:0">
                <webdiyer:AspNetPager ID="AspNetPager1" runat="server" CustomInfoHTML="Count %RecordCount%"
                    Width="100%" CurrentPageButtonClass="pageBtn" OnPageChanged="AspNetPager1_PageChanged"
                    AlwaysShow="true" PageSize="10" CssClass="dPager">
                </webdiyer:AspNetPager>
            </div>
        </div>
    </div>
    </form>
</body>
</html>
