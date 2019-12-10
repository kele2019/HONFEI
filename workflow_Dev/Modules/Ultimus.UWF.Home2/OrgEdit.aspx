<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="OrgEdit.aspx.cs" Inherits="Ultimus.UWF.Home2.OrgEdit" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <link href="css/workflow.css" rel="stylesheet" type="text/css" />
    <style>
        body{margin:0px;}
        .tp
        {
            line-height:42px;
        }
        .cGrid
        {
            padding-top:10px;
        }
        .tb
        {
            width:200px;
            border: #DFDFDF 1px solid;
            padding-left:4px;
        }
        .tbl
        {
            width:100%;
        }
        .tbl tr
        {
            height:34px;
        }
        .tbl td
        {
            border-left:#DFDFDF 1px solid;
            border-top:#DFDFDF 1px solid;
        }
        .td1
        {
            padding-left:46px;
            background-color:#f5f5f5;
            width:160px;
        }
        .td2
        {
            padding-left:10px;
        }
        .tdB
        {
            border-bottom:#DFDFDF 1px solid;
        }
        .tdR
        {
            border-right:#DFDFDF 1px solid;
        }
        .btnImg
        {
            width:70px;
            vertical-align:middle;
            margin-top:-4px;
            border: #DFDFDF 1px solid;
        }
        .tbl tr.trBtn
        {
            height:60px;
        }
        .starRed
        {
            color:#ff0000;
        }
        .hid
        {
            visibility:hidden;
        }
    </style>
    <script src="js/jquery.min.js"></script>
    <script>
        function onSave() {
            var v = true;
            v &= validateField("name");
           // v &= validateField("engname");
           // v &= validateField("level");
           // v &= validateField("cbcenter");
            v &= validateField("sort");
            return v == 1;
        }
        function validateField(flag) {
            var result = true;
            var em = "";
            switch (flag) {
                case "name":
                    if ($("#txtName").val() == "") {
                        result = false;
                        em = "必填项";
                    }
                    else {
                        var parentID = $("#hidParentID").val();
                        $.ajax({
                            url: 'AjaxOrgUser.aspx?Method=validateorgname&id=<%=Query_ID %>&parentid=' + parentID + '&name=' + encodeURIComponent($("#txtName").val()), // 跳转到 action  
                            type: 'post',
                            cache: false,
                            async: false,
                            dataType: 'json',
                            success: function (data) {
                                if (data.Code != 0) {
                                    result = false;
                                    em = data.ErrorMsg;
                                }
                            },
                            error: function () {
                                alert("验证数据时发生错误！");
                            }
                        });
                    }
                    $("#emName").html(em);
                    break;
                case "engname":
                    if ($("#txtEngName").val() == "") {
                        result = false;
                        em = "必填项";
                    }
                    else {
                        var parentID = $("#hidParentID").val();
                        $.ajax({
                            url: 'AjaxOrgUser.aspx?Method=validateorgname&id=<%=Query_ID %>&parentid=' + parentID + '&engname=' + encodeURIComponent($("#txtEngName").val()), // 跳转到 action  
                            type: 'post',
                            cache: false,
                            async: false,
                            dataType: 'json',
                            success: function (data) {
                                if (data.Code != 0) {
                                    result = false;
                                    em = data.ErrorMsg;
                                }
                            },
                            error: function () {
                                alert("验证数据时发生错误！");
                            }
                        });
                    }
                    $("#emEngName").html(em);
                    break;
                case "level":
                    if ($("#txtLevel").val() == "") {
                        result = false;
                        em = "必填项";
                    }
                    $("#emLevel").html(em);
                    break;
                case "cbcenter":
                    if ($("#txtCBCenter").val() == "") {
                        result = false;
                        em = "必填项";
                    }
                    $("#emCBCenter").html(em);
                    break;
                case "sort":
                    if ($("#txtSort").val() == "") {
                        result = false;
                        em = "必填项";
                    }
                    else {
                        var sort = parseInt($("#txtSort").val());
                        if (isNaN(sort)) {
                            result = false;
                            em = "必须为整数";
                        }
                    }
                    $("#emSort").html(em);
                    break;
            }
            return result;
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <asp:PlaceHolder ID="plcCreateSuccess" runat="server" Visible="false">
        <asp:HiddenField ID="hidNewOrgID" runat="server" />
        <script>
            function addToParentWindowTree() {
                var orgID = $("#hidNewOrgID").val();
                var orgName = $("#txtName").val();
                parent.window.addOrgNodeToTree(orgName, orgID);
            }
            $(document).ready(function () {
                addToParentWindowTree();
            });
        </script>
    </asp:PlaceHolder>
    <div class="tp">
        部门信息
    </div>
    <div class="cGrid">
    <%--<input type="button" onclick="parent.window.addOrgNodeToTree('sdf',12);" value="ccc" />--%>
        <table cellpadding="0" cellspacing="0" border="0" class="tbl">
            <tr>
                <td class="td1">部门名称</td>
                <td class="td2 tdR">
                    <asp:TextBox ID="txtName" runat="server" CssClass="tb" MaxLength="30" onblur="validateField('name');" />
                    <font class="starRed">*</font>
                    <div id="emName" class="starRed" />
                </td>
            </tr>
            <tr style="display:none">
                <td class="td1">部门英文名称</td>
                <td class="td2 tdR">
                    <asp:TextBox ID="txtEngName" runat="server" CssClass="tb" MaxLength="30" onblur="validateField('engname');" />
                    <font class="starRed">*</font>
                    <div id="emEngName" class="starRed" />
                </td>
            </tr>
            <tr>
                <td class="td1">上级部门</td>
                <td class="td2 tdR">
                    <asp:TextBox ID="txtParentName" runat="server" CssClass="tb" />
                    <asp:HiddenField ID="hidParentID" runat="server" />
                </td>
            </tr>
            <tr style="display:none">
                <td class="td1">上级部门英文名称</td>
                <td class="td2 tdR">
                    <asp:TextBox ID="txtParentEngName" runat="server" CssClass="tb" />
                </td>
            </tr>
            <tr style="display:none">
                <td class="td1">部门级别</td>
                <td class="td2 tdR">
                    <asp:TextBox ID="txtLevel" runat="server" CssClass="tb" MaxLength="30" onblur="validateField('level');" />
                    <font class="starRed">*</font>
                    <div id="emLevel" class="starRed" />
                </td>
            </tr>
            <tr>
                <td class="td1">成本中心</td>
                <td class="td2 tdR">
                    <asp:TextBox ID="txtCBCenter" runat="server" CssClass="tb" MaxLength="30" />
                  <%--  <font class="starRed">*</font>
                    <div id="emCBCenter" class="starRed" />--%>
                </td>
            </tr>
            <tr>
                <td class="td1">排序</td>
                <td class="td2 tdR">
                    <asp:TextBox ID="txtSort" runat="server" CssClass="tb" MaxLength="30" onblur="validateField('sort');" />
                    <font class="starRed">*</font>
                    <div id="emSort" class="starRed" />
                </td>
            </tr>
            <tr class="trBtn">
                <td class="td2 tdB tdR" colspan="4" align="center">
                    <asp:ImageButton ID="btnCancel" runat="server" ImageUrl="images/v2/cancel.png" 
                        onclick="btnCancel_Click" />
                    <asp:ImageButton ID="btnSave" runat="server" ImageUrl="images/v2/save.png" 
                        onclick="btnSave_Click" OnClientClick="return onSave();" />
                </td>
            </tr>
        </table>
    </div>
    </form>
</body>
</html>
