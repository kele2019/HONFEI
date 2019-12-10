<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="UserEdit.aspx.cs" Inherits="Ultimus.UWF.Home2.UserEdit" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
<title>组织机构维护</title>
    <link href="css/workflow.css" rel="stylesheet" type="text/css" />
    <link href="css/easyui.css" rel="stylesheet" type="text/css" />
    <style>
        body{margin:0px;}
        .tp
        {
            line-height:22px;
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
    </style>
    <style>
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
    .tree-user
    {
        width:16px;
        height:16px;
        padding-left:16px;
        display:inline-block;
        background:url(images/V2/user.png) no-repeat center center
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
    <script src="../../Assets/js/My97DatePicker/WdatePicker.js" type="text/javascript"></script>
    <script>
        function onSave() {
            var v = true;
            v &= validateField("Sex");
          
            v &= validateField("National");
            v &= validateField("BirthDay");
            v &= validateField("JoinDate");
            v &= validateField("FirstWorkDate");
            v &= validateField("Sort");
          
            v &= validateField("orgname");
          
            v &= validateField("orgname2");

          
            v &= validateField("gw");
            v &= validateField("annal");
            v &= validateField("usercode");
            
            v &= validateField("gwlevel");
          
            v &= validateField("name");  
            v &= validateField("engname");  
            v &= validateField("loginname");  
            v &= validateField("email");
           

            v &= validateField("photoimg");  
            v &= validateField("signimg");
           
           // v &= validateField("cbcenter"); 
            v &= validateField("mobile");
            return v == 1;
        }
        function validateField(flag) {
            var result = true;
            var em = "";
            switch (flag) {
                case "gw":
                    if ($("#txtGW").val() == "") {
                        result = false;
                        em = "必填项";
                    }
                    $("#emGW").html(em);
                    break;

                case "annal":
                    if ($("#txtAnualDay").val() == "") {
                        result = false;
                        em = "必填项";
                    }
                    $("#annal").html(em);
                    break;

                case "usercode":
                    if ($("#txtUserCode").val() == "") {
                        result = false;
                        em = "必填项";
                    }
                    else {
                        $.ajax({
                            url: 'AjaxOrgUser.aspx?Method=validateusercode&id=<%=Query_ID %>&usercode=' + encodeURIComponent($("#txtUserCode").val()), // 跳转到 action  
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
                    $("#emUserCode").html(em);
                    break;
                case "gwengname":
                    if ($("#txtGWEngName").val() == "") {
                        result = false;
                        em = "必填项";
                    }
                    $("#emGWEngName").html(em);
                    break;
                case "photoimg":
                    if (!checkImgFormate($("#fileUserPhotoImg").val())) {
                        result = false;
                        em = "格式不正确";
                    }
                    $("#UserPhotoImg").html(em);
                    break;
                case "signimg":
                    if (!checkImgFormate($("#fileUserSignImg").val())) {
                        result = false;
                        em = "格式不正确";
                    }
                    $("#UserSignImg").html(em);
                    break;
                case "name":
                    if ($("#txtName").val() == "") {
                        result = false;
                        em = "必填项";
                    }
                    $("#emName").html(em);
                    break;
                case "engname":
                    if ($("#txtEngName").val() == "") {
                        result = false;
                        em = "必填项";
                    }
                    $("#emEngName").html(em);
                    break;
                case "loginname":
                    if ($("#txtLoginName").val() == "") {
                        result = false;
                        em = "必填项";
                    }
                    else {
                        $.ajax({
                            url: 'AjaxOrgUser.aspx?Method=validateusername&id=<%=Query_ID %>&loginname=' + encodeURIComponent($("#txtLoginName").val()), // 跳转到 action  
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
                    $("#emLoginName").html(em);
                    break;
                case "email":
                    if ($("#txtEmail").val() == "") {
                        result = false;
                        em = "必填项";
                    }
                    else {
                        result = checkEmail($("#txtEmail").val());
                        if (!result)
                            em = "格式不正确";
                    }
                    $("#emEmail").html(em);
                    break;
//                case "sort":
//                    if ($("#txtSort").val() == "") {
//                    }
//                    else {
//                        var sort = parseInt($("#txtSort").val());
//                        if (isNaN(sort)) {
//                            result = false;
//                            em = "必须为整数";
//                        }
//                    }
//                    $("#emSort").html(em);
//                    break;
                case "cbcenter":
                    if ($("#txtCBCenter").val() == "") {
                        result = false;
                        em = "必填项";
                    }
                    $("#emCBCenter").html(em);
                    break;
                case "mobile":
                    if ($("#txtMobile").val() == "") {
                    }
                    else {
                        var myreg = /^(((1[0-9][0-9]{1})|159|153)+\d{8})$/;
                        if (!myreg.test($("#txtMobile").val())) {
                            result = false;
                            em = "手机格式错误";
                        }
                    }
                    $("#emMobile").html(em);
                    break;
                case "orgname":
                    if ($("#txtOrgName").val() == "") {
                        result = false;
                        em = "必填项";
                    }
                    $("#emOrgName").html(em);
                    break;
                case "orgengname":
                    if ($("#txtOrgEngName").val() == "") {
                        result = false;
                        em = "必填项";
                    }
                    $("#emOrgEngName").html(em);
                    break;
                case "orgname2":
                    if ($("#txtOrgName2").val() == "") {
                        result = false;
                        em = "必填项";
                    }
                    $("#emOrgName2").html(em);
                    break;
                case "orgengname2":
                    if ($("#txtOrgEngName2").val() == "") {
                        result = false;
                        em = "必填项";
                    }
                    $("#emOrgEngName2").html(em);
                    break;
                case "LeadName":
                    if ($("#hidLeaderLoginName").val() == "") {
                        result = false;
                        em = "必填项";
                    }
                    $("#LeadName").html(em);
                    break;
                case "LeadName1":
                    if ($("#hidLeaderLoginName").val() == "") {
                        result = true;
                        em = "";
                    }
                    $("#LeadName").html(em);
                    break;
                case "Sex":
                    if ($("#txtSex").combobox("getValue") == "") {
                        result = false;
                        em = "必填项";
                    }
                    $("#Sex").html(em);
                    break;
                case "National":
                    if ($("#txtNational").combobox("getValue") == "") {
                        result = false;
                        em = "必填项";
                    }
                    $("#National").html(em);
                    break;
                case "BirthDay":
                    if ($("#txtBirthDay").val() == "") {
                        result = false;
                        em = "必填项";
                    }
                    $("#BirthDay").html(em);
                    break;
                case "FirstWorkDate":
//                    if ($("#dropthanTwenty").val() == "") {
//                        result = false;
//                        em = "必填项";
                    //                    }
                    if ($("#dropthanTwenty").combobox("getValue") == "") {
                        result = false;
                        em = "必填项";
                    }

                    $("#FirstWorkDate").html(em);
                    break;
                case "JoinDate":
                    if ($("#txtJoinDate").val() == "") {
                        result = false;
                        em = "必填项";
                    }
                    $("#JoinDate").html(em);
                    break;
                case "Role":
                    if ($("#txtRole").combobox("getValue") == "") {
                        result = false;
                        em = "必填项";
                    }
                    $("#Role").html(em);
                    break;
//                case "Sort":
//                    if ($("#txtSort").val() == "") {
//                        result = false;
//                        em = "必填项";
//                    }
//                    if (isNaN(($("#txtSort").val() - 0))) {
//                        result = false;
//                        em = "请填写数字";
//                    }
//                    $("#Sort").html(em);
//                    break;

            }
            return result;
        }

        function checkEmail(value) {
            var myreg = /^([a-zA-Z0-9]+[_|\_|\.]?)*[a-zA-Z0-9]+@([a-zA-Z0-9]+[_|\_|\.]?)*[a-zA-Z0-9]+\.[a-zA-Z]{2,3}$/;
            if (!myreg.test(value)) {
                return false;
            }
            return true;
        }

        $(document).ready(function () {
            $('#txtLeader').combotree({
                url: "AjaxOrgUserTree.aspx?tttt" + (new Date()).toLocaleTimeString(),
                valueField: 'id',
                textField: 'text',
                editable: false,
                onClick: function (node, a, b) {
                    if (node.type == "user") {
                        $("#hidLeaderLoginName").val(node.id);
                        $("#txtLeaderEngName").val(node.EngName);
                    }
                    else {
                        $("#hidLeaderLoginName").val("");
                        $($("#txtLeader")[0].parentNode).find("input").val("");
                        $('#txtLeader').combotree('clear');
                        alert("请选择用户！");
                    }
//                    var UserLeve = $("input[name='ddlGWLevel']").val();
//                    if (UserLeve != 50) {
//                        validateField("LeadName");
//                    }
//                    else {
//                        validateField("LeadName1");
//                    }
                },
                onLoadSuccess: function (node, data) {
                    $('#txtLeader').combotree('tree').tree("collapseAll");
                    var rootNode = $('#txtLeader').combotree('tree').tree("getRoot");
                    $('#txtLeader').combotree('tree').tree("expand", rootNode.target);
                    duleTreeUserIcon(rootNode)
                }
            });

            $('#txtOrgName').combotree({
                url: "AjaxOrgTree.aspx?level1=1&aa=" + (new Date()).toLocaleTimeString(),
                valueField: 'id',
                textField: 'text',
                editable: false,
                onClick: function (node, a, b) {
                    $("#hidOrgID").val(node.id);
                    $("#txtOrgName").val(node.text);
                    $("#txtOrgEngName").val(node.EngName);
                    reBindOrgName2(node.id);

                    $("#hidOrgID2").val("");
                    $("#txtOrgName2").val("");
                    $("#txtOrgEngName2").val("");
                    $("#txtCBCenter").val("");
                    $("#txtOrgName2").combobox("clear");

                    validateField("orgname");
                },
                onLoadSuccess: function (node, data) {
                    $('#txtOrgName').combotree('tree').tree("collapseAll");
                    var rootNode = $('#txtOrgName').combotree('tree').tree("getRoot");
                    $('#txtOrgName').combotree('tree').tree("expand", rootNode.target);
                    //duleTreeUserIcon(rootNode)
                }
            });

            reBindOrgName2($("#hidOrgID").val())

//            $("#ddlGWLevel").combobox({
//                editable: false,
//                onSelect: function (node, a, b) {
//                    var UserLeve = $("input[name='ddlGWLevel']").val();
//                    if (UserLeve == 50) {
//                        validateField("LeadName1");
//                    }
//                    validateField("gwlevel");
//                }
//            });

            $("#txtSex").combobox({
                editable: false,
                onSelect: function (node, a, b) {
                    validateField("Sex");
                }
            });
            $("#dropthanTwenty").combobox({
                editable: false,
                onSelect: function (node, a, b) {
                    validateField("FirstWorkDate");
                }
            });

            $("#txtNational").combobox({
                editable: false,
                onSelect: function (node, a, b) {
                    validateField("National");
                }
            });
//            $("#txtRole").combobox({
//                editable: false,
//                onSelect: function (node, a, b) {
//                    validateField("Role");
//                }
//            });

        });

        function reBindOrgName2(parentID) {
            $('#txtOrgName2').combotree({
                url: "AjaxOrgTree.aspx?ParentID=" + parentID + "&aa=" + (new Date()).toLocaleTimeString(),
                valueField: 'id',
                textField: 'text',
                editable: false,
                onClick: function (node, a, b) {
                    $("#hidOrgID2").val(node.id);
                    $("#txtOrgName2").val(node.text);
                    $("#txtOrgEngName2").val(node.EngName);
                    $("#txtCBCenter").val(node.ext02);

                    validateField("orgname2");
                    //validateField("orgengname2");
                    //validateField("cbcenter");
                },
                onLoadSuccess: function (node, data) {
                    if (data == null)
                        return;
                    $('#txtOrgName2').combotree('tree').tree("collapseAll");
                    var rootNode = $('#txtOrgName2').combotree('tree').tree("getRoot");
                    $('#txtOrgName2').combotree('tree').tree("expand", rootNode.target);
                    //duleTreeUserIcon(rootNode)
                }
            });
        }

        function duleTreeUserIcon(node) {
            var spans = $(node.target).find("span");
            if (node.type == "user") {
                for (var i = 0; i < spans.length; i++) {
                    if ($(spans[i]).hasClass("tree-file")) {
                        $(spans[i]).removeClass("tree-file");
                        $(spans[i]).addClass("tree-user");
                        break;
                    }
                }
            }
            else if (node.type == "org" && node.children != null) {
                for (var i = 0; i < node.children.length; i++) {
                    duleTreeUserIcon(node.children[i]);
                }
            }
        }

        function checkImgFormate(filename) {
            if (filename != "") {
                var extension = new String(filename.substring(filename.lastIndexOf(".") + 1, filename.length)).toLowerCase();
                if (extension == "jpg" || extension == "gif" || extension == "jpeg" || extension == "png" || extension == "bmp") {
                    return true;
                 }
                else {
                    return false;
                }
            }
            else
                return true;
        }
        function onDelete() {
            if (!confirm("确定要删除吗？"))
                return false;
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div class="tp">
        岗位信息
    </div>
    <div class="cGrid">
        <asp:HiddenField ID="hidOrgID" runat="server" />
        <asp:HiddenField ID="hidOrgID2" runat="server" />
        <table cellpadding="0" cellspacing="0" border="0" class="tbl">
            <tr>
                <td class="td1">一级部门名称</td>
                <td class="td2">
                    <asp:TextBox ID="txtOrgName" runat="server" CssClass="easyui-combotree" Width="205px" />
                    <font class="starRed">*</font>
                    <div id="emOrgName" class="starRed" />
                </td>
                <td class="td1">二级部门名称</td>
                <td class="td2 tdR">
                    <asp:TextBox ID="txtOrgName2" runat="server" CssClass="tb" />
                    <font class="starRed">*</font>
                    <div id="emOrgName2" class="starRed" />
                </td>
            </tr>
            <tr style="display:none">
                <td class="td1">一级部门英文名称</td>
                <td class="td2">
                    <asp:TextBox ID="txtOrgEngName" runat="server" CssClass="tb" />
                    <font class="starRed">*</font>
                    <div id="emOrgEngName" class="starRed" />
                </td>
                <td class="td1">二级部门英文名称</td>
                <td class="td2 tdR">
                    <asp:TextBox ID="txtOrgEngName2" runat="server" CssClass="tb" />
                    <font class="starRed">*</font>
                    <div id="emOrgEngName2" class="starRed" />
                </td>
            </tr>
            <tr>
                <td class="td1">上级领导</td>
                <td class="td2">
                    <asp:TextBox ID="txtLeader" runat="server" CssClass="easyui-combotree" Width="205px"   />
                    <asp:HiddenField ID="hidLeaderLoginName" runat="server" />
                   
                </td>
                <td class="td1">成本中心</td>
                <td class="td2 tdR">
                    <asp:TextBox ID="txtCBCenter" onfocus="this.blur()" runat="server" CssClass="tb"   />
                 <%--   <font class="starRed">*</font>
                    <div id="emCBCenter" class="starRed" />--%>
                </td>
            </tr>
            <tr>
                <td class="td1 tdB">上级领导英文名称</td>
                <td class="td2 tdB">
                    <asp:TextBox ID="txtLeaderEngName" runat="server" CssClass="tb"  />
                    
                </td>
                <td class="td1 tdB"></td>
                <td class="td2 tdB tdR"></td>
            </tr>
        </table>
    </div>
    
    <div class="tp">
        人员信息
    </div>
    <div class="cGrid">
        <table cellpadding="0" cellspacing="0" border="0" class="tbl">
            
            <tr>
                <td class="td1">登录账号</td>
                <td class="td2">
                    <asp:TextBox ID="txtLoginName" runat="server" CssClass="tb" MaxLength="20" onblur="validateField('loginname');"  />
                    <font class="starRed">*</font>
                    <div id="emLoginName" class="starRed" />
                </td>
                <td class="td1">员工编号</td>
                <td class="td2 tdR">
                    <asp:TextBox ID="txtUserCode" runat="server" CssClass="tb" MaxLength="20" onblur="validateField('usercode');" />
                    <font class="starRed">*</font>
                    <div id="emUserCode" class="starRed" />
                </td>
            </tr>
            <tr>
                <td class="td1">姓名</td>
                <td class="td2">
                    <asp:TextBox ID="txtName" runat="server" MaxLength="20" CssClass="tb" onblur="validateField('name');" />
                    <font class="starRed">*</font>
                    <div id="emName" class="starRed" />
                </td>
                <td class="td1">英文名称</td>
                <td class="td2 tdR">
                    <asp:TextBox ID="txtEngName" runat="server" CssClass="tb" MaxLength="20" onblur="validateField('engname');" />
                    <font class="starRed">*</font>
                    <div id="emEngName" class="starRed" />
                </td>
            </tr>
            <tr>
               <td class="td1">出生日期</td><%--,readOnly:true--%>
               <td class="td2">
              <asp:TextBox runat="server" ID="txtBirthDay"  onclick="WdatePicker({dateFmt:'yyyy-MM-dd',maxDate:'#F{$dp.$D(\'txtFirstWorkDate\')}'})" onblur="validateField('BirthDay');" CssClass="tb"></asp:TextBox>
               <font class="starRed">*</font>
                    <div id="BirthDay" class="starRed" />
               </td>
                <td  class="td1">
           <%--参加工作日期--%>
           参加工作超过20年
            </td>
            <td class="td2 tdR">
            <asp:TextBox runat="server" ID="txtFirstWorkDate"  style="display:none" onclick="WdatePicker({dateFmt:'yyyy-MM-dd',minDate:'#F{$dp.$D(\'txtBirthDay\')}', maxDate:'#F{$dp.$D(\'txtJoinDate\')}'})"  onblur="validateField('FirstWorkDate');" CssClass="tb"></asp:TextBox>
            
             <asp:DropDownList runat="server" ID="dropthanTwenty" CssClass="easyui-combobox" style="width:205px;">
                 <asp:ListItem Text="--请选择--" Value="" Selected="True" />
                 <asp:ListItem Text="Y" Value="Yes"></asp:ListItem>
                 <asp:ListItem Text="N" Value="No"></asp:ListItem>
              </asp:DropDownList>
                <font class="starRed">*</font>
            <div id="FirstWorkDate" class="starRed" />

            </td>
            </tr>
            <tr>
             <td class="td1">性别</td>
              <td class="td2">
              <asp:DropDownList runat="server" ID="txtSex" CssClass="easyui-combobox" style="width:205px;">
                 <asp:ListItem Text="--请选择--" Value="" Selected="True" />
                 <asp:ListItem Text="男" Value="男"></asp:ListItem>
                 <asp:ListItem Text="女" Value="女"></asp:ListItem>
              </asp:DropDownList>
               <font class="starRed">*</font>
                    <div id="Sex" class="starRed" />
              </td>
            <td  class="td1">国籍</td>
            <td class="td2 tdR">
            <asp:DropDownList runat="server" ID="txtNational" CssClass="easyui-combobox" style="width:205px;">
            <asp:ListItem Text="--请选择--" Value="" Selected="True" />
            <asp:ListItem   value="中国">中国</asp:ListItem>
            <asp:ListItem value="中国台湾">中国台湾</asp:ListItem>
            <asp:ListItem value="中国香港">中国香港</asp:ListItem>
            <asp:ListItem value="美国">美国</asp:ListItem>
            <asp:ListItem value="英国">英国</asp:ListItem>
            <asp:ListItem value="德国">德国</asp:ListItem>
            <asp:ListItem value="俄罗斯">俄罗斯</asp:ListItem>
            <asp:ListItem value="澳大利亚">澳大利亚</asp:ListItem>
            <asp:ListItem value="巴西">巴西</asp:ListItem>
            <asp:ListItem value="比利时">比利时</asp:ListItem>
            <asp:ListItem value="韩国">韩国</asp:ListItem>
            <asp:ListItem value="法国">法国</asp:ListItem>
            <asp:ListItem value="加拿大">加拿大</asp:ListItem>
            <asp:ListItem value="马来西亚">马来西亚</asp:ListItem>
            <asp:ListItem value="日本">日本</asp:ListItem>
            <asp:ListItem value="瑞典">瑞典</asp:ListItem>
            <asp:ListItem value="西班牙">西班牙</asp:ListItem>
            <asp:ListItem value="意大利">意大利</asp:ListItem>
            </asp:DropDownList>
             <font class="starRed">*</font>
                    <div id="National" class="starRed" />
            </td>
             

            </tr>
            <tr>
               
                <td class="td1">手机</td>
                <td class="td2 ">
                    <asp:TextBox ID="txtMobile" runat="server" CssClass="tb" onblur="validateField('mobile');" />
                    <div id="emMobile" class="starRed" />
                </td>
                 <td class="td1">Email</td>
                <td class="td2 tdR">
                    <asp:TextBox ID="txtEmail" runat="server" CssClass="tb" onblur="validateField('email');" />
                    <font class="starRed">*</font>
                    <div id="emEmail" class="starRed" />
                </td>
            </tr>
            <%--<tr>
               
                <td class="td1"></td>
                <td class="td2 tdR">
                   <%-- <asp:DropDownList ID="ddlGWLevel" runat="server" CssClass="easyui-combobox" style="width:205px;">
                        <asp:ListItem Text="--请选择--" Value="" Selected="True" />
                        <asp:ListItem Text="普通员工" Value="10" />
                        <asp:ListItem Text="经理" Value="20" />
                         <asp:ListItem Text="副总监" Value="25" />
                        <asp:ListItem Text="总监" Value="30" />
                        <asp:ListItem Text="副总裁" Value="40" />
                        <asp:ListItem Text="总裁" Value="50" />
                    </asp:DropDownList> 
                  <font class="starRed">*</font>
                    <div id="emGWLevel" class="starRed" /> 
                </td>
            </tr>--%>
            
            <tr>
            <td class="td1">入职日期</td>
                <td class="td2 ">
                <asp:TextBox runat="server" ID="txtJoinDate"  onclick="WdatePicker({dateFmt:'yyyy-MM-dd',minDate:'#F{$dp.$D(\'txtFirstWorkDate\')}', maxDate:'#F{$dp.$D(\'txtDepartureDate\')}'})" CssClass="tb" onblur="validateField('JoinDate');"></asp:TextBox>
               <font class="starRed">*</font>
                    <div id="JoinDate" class="starRed" />
                </td>

                <td class="td1">离职日期</td>
                <td class="td2 tdR">
                <asp:TextBox runat="server" ID="txtDepartureDate" onclick="WdatePicker({dateFmt:'yyyy-MM-dd', minDate:'#F{$dp.$D(\'txtJoinDate\')}'})" CssClass="tb"></asp:TextBox>
                </td>
               
            </tr>

            <tr>
               <%-- <td class="td1">排序号</td>--%>
               <%-- <td class="td2 tdR">--%>
                  <%--   <font class="starRed">*</font>
                    <div id="Sort" class="starRed" />--%>
                <%--</td>--%>
        
         <td class="td1">岗位</td>
                <td class="td2">
                    <asp:TextBox ID="txtSort" runat="server"  Visible="false" Text="0"  onblur="validateField('Sort');"/>
                    <asp:TextBox ID="txtGW" runat="server" MaxLength="20" CssClass="tb" onblur="validateField('gw');" />
                    <font class="starRed">*</font>
                    <div id="emGW" class="starRed" />
                </td>

           <td class="td1">是否有效</td>
                <td class="td2 tdR" >
                    <asp:CheckBox ID="chkEnable" runat="server" Checked="true" />
           </td>
          </tr>
          

           <tr>
         <td class="td1">年假天数</td>
                <td class="td2">
                    <asp:TextBox ID="txtAnualDay" runat="server"    Text="0"  CssClass="tb" onblur="validateField('annal');"/>
                    <font class="starRed">*</font>
                    <div id="annal" class="starRed" />
                </td>
           <td class="td1"></td>
                <td class="td2 tdR" >
           </td>
          </tr>
           
          <tr>
                <td class="td1">头像</td>
                <td class="td2 tdR">
                  <asp:FileUpload runat="server" ID="fileUserPhotoImg" />
                </td>
        
           <td class="td1">签名照</td>
                <td class="td2 tdR" >
                        <asp:FileUpload runat="server" ID="fileUserSignImg" />
                </td>
          </tr>

            <tr class="trBtn">
                <td class="td2 tdB tdR" colspan="4" align="center">
                    <asp:ImageButton ID="btnCancel" runat="server" ImageUrl="images/v2/cancel.png" 
                        onclick="btnCancel_Click" />
                    <asp:ImageButton ID="btnSave" runat="server" ImageUrl="images/v2/save.png" 
                        onclick="btnSave_Click" OnClientClick="return onSave();" />
                    <asp:ImageButton ID="btnDelete" runat="server" ImageUrl="images/v2/delete.png" Visible="false"
                        onclick="btnDelete_Click" OnClientClick="return onDelete();" />
                </td>
            </tr>
        </table>
    </div>
    </form>
</body>
</html>
