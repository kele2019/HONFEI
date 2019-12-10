<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="UserList.aspx.cs" Inherits="Ultimus.UWF.Home2.UserList" %>

<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <link href="css/workflow.css" rel="stylesheet" type="text/css" />
    <link href="css/easyui.css" rel="stylesheet" type="text/css" />
    <style type="text/css">
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
            background-color:#EEEEEE;
            height:42px;
            text-align:left;
            padding-left:20px;
        }
        .tblList tbody tr
        {
            height:40px;
        }
        .tblList tbody td
        {
            padding-left:10px;
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
    <style type="text/css">
    .tree-indent
    {
        width:16px;
        height:18px;
        display:inline-block;
    }
    .tree-expanded
    {
        cursor:pointer;
        display:inline-block;
        width:16px;
        height:16px;
        background:url(images/V2/expand.png) no-repeat 6px 6px;
    }
    .tree-collapsed
    {
        cursor:pointer;
        display:inline-block;
        width:16px;
        height:16px;
        background:url(images/V2/noexpand.png) no-repeat 6px 6px;
    }
    .tree-title
    {
        cursor:pointer;
    }
    .tree-node-selected
    {
        color:#71A5CF;
        background-color:#EEEEEE;
    }
    .tree-node
    {
        line-height:24px;
    }
    .tree-icon
    {
        width:0px;
    }
    .tree-file
    {
        width:0px;
    } 
    .combo-arrow
    {
        background:url(images/V2/combo_arrow.png) no-repeat center center
    }
    .combo
    {
        border-color:#dfdfdf;
    }
    </style>
    <script src="js/jquery.min.js"></script>
    <script src="js/jquery.easyui.min.js"></script>
    <script>
        function createUser(parentID) {
            location.href = "UserEdit.aspx?ParentID=" + parentID + "&ReturnUrl=" + encodeURIComponent(location.href);
        }
        function editUser(id) {
            location.href = "UserEdit.aspx?ID=" + id + "&ReturnUrl=" + encodeURIComponent(location.href);
        }
        function removeUser(id) {
            if (!confirm("Confirm Delete？"))
                return;
            $.ajax({
                url: 'AjaxOrgUser.aspx?Method=deluser&id=' + id, // 跳转到 action  
                type: 'post',
                cache: false,
                dataType: 'json',
                success: function (data) {
                    if (data.Code == 0) {
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
            $('#txtQueryOrgName').combotree({
                url: "AjaxOrgTree.aspx?tttt" + (new Date()).toLocaleTimeString(),
                valueField: 'id',
                textField: 'text',
                editable: false,
                onClick: function (node) {
                    $("#hidQueryOrgID").val(node.id);
                }, //全部折叠
                onLoadSuccess: function (node, data) {
                    $('#txtQueryOrgName').combotree('tree').tree("collapseAll");
                    var rooNode = $('#txtQueryOrgName').combotree('tree').tree("getRoot");
                    $('#txtQueryOrgName').combotree('tree').tree("expand", rooNode.target);
                }
            });
            var DBRWStr = $(window.parent.$("#orgTree"));
            $(DBRWStr).children().eq(1).show();
        });
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <asp:HiddenField ID="hidIsSearch" runat="server" />
    <div>
        <div class="tugl">
            <a href="OrgList.aspx?ParentID=<%=ParentID %>">DepartmentManager</a>
        </div>
        <div class="cb"></div>
        <div class="topbar">
            <table border="0" cellpadding="0" cellspacing="0">
                <tr>
                    <td width="20px">&nbsp;</td>
                    <td class="td-l">Name</td>
                    <td>
                        <asp:TextBox ID="txtQueryName" runat="server" CssClass="tb" />
                    </td>
                    <td class="td-l">Login Account</td>
                    <td class="td-l">
                        <asp:TextBox ID="txtQueryAccount" runat="server" CssClass="tb" />
                    </td>
                    <td>Department</td>
                    <td class="td-l">
                        <asp:TextBox ID="txtQueryOrgName" runat="server" CssClass="easyui-combotree" Width="180px" />
                        <asp:HiddenField ID="hidQueryOrgID" runat="server" />
                        <%--<input id="txtOrgTree" class="easyui-combotree" data-options="url:'AjaxOrgTree.aspx',method:'get'" style="width:180px;" />--%>
                    </td>
                    <td class="td-l">
                        <asp:ImageButton ID="btnImg" ImageUrl="images/v2/search.png" runat="server" 
                            CssClass="btnImg" onclick="btnImg_Click" />
                        <a href="javascript:createUser(<%=ParentID%>);"><img class="btnImg" src="images/v2/add.png" border="0" /></a>
                    </td>
                </tr>
            </table>
        </div>
        <div class="cGrid">
            <table cellpadding="0" cellspacing="0" border="0" class="tblList">
                <thead>
                    <tr class="tblListHeaderTr">
                        <th  width="40px">No.</th>
                        <th  width="100px">User Code</th>
                        <th  width="130px">English Name</th>
                        <th  width="130px">Chinese Name</th>
                        <th   width="130px">Department</th>
                        <th   width="100px">JobTitle</th>
                        <th>Operator</th>
                    </tr>
                </thead>
                <tbody>
                    <asp:Repeater ID="rpt" runat="server">
                        <ItemTemplate>
                            <tr>
                                <td><%#(PageIndex - 1) * PageSize + Container.ItemIndex + 1%></td>
                                <td style="padding-left:20px;" class="dot"><%#Eval("USERCODE")%></td>
                                <td style="padding-left:20px;" class="dot"><%#Eval("EXT04")%></td>
                                <td style="padding-left:20px;" class="dot"><%#Eval("USERNAME")%></td>
                                <td style="padding-left:20px;" class="dot"><%#Eval("DEPARTMENT") %></td>
                               <%-- <td class="dot"><%#GetOrgName(1, Eval("OrgID"))%></td>--%>
                                <td style="padding-left:20px;" class="dot"><%#Eval("GW")%></td>
                                <td style="padding-left:20px;" class="dot btnAction">
                                    <a href="javascript:void(0);" onclick="editUser(<%#Eval("USERID")%>);">Edit</a>
                                   <%-- <a href="javascript:void(0);" style="display:none" onclick="removeUser(<%#Eval("USERID")%>)">删除</a>--%>
                                </td>
                            </tr>
                        </ItemTemplate>
                    </asp:Repeater>
                    <asp:PlaceHolder ID="plcEmpty" runat="server">
                        <tr>
                            <td colspan="7">没有数据！</td>
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
