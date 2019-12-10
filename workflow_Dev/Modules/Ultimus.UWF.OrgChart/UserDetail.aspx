<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="UserDetail.aspx.cs" Inherits="Ultimus.UWF.OrgChart.UserDetail" %>

<%@ Import Namespace="Ultimus.UWF.Common.Logic" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>人员维护</title>
    <base target="_self" />
    <script type="text/javascript" src="<%=WebUtil.GetRootPath()%>/Assets/js/common.js"></script>
    <script type="text/javascript" src="<%=WebUtil.GetRootPath()%>/Assets/js/jquery.js"></script>
    <script type="text/javascript" src="<%=WebUtil.GetRootPath()%>/Assets/js/jquery.validationEngine.js"></script>
    <script type="text/javascript" src="<%=WebUtil.GetRootPath()%>/Assets/js/languages/jquery.validationEngine-zh_CN.js"></script>
    <script type="text/javascript">
        self.moveTo(0, 0)
        self.resizeTo(screen.availWidth, screen.availHeight)
        function closeWin() {
            window.opener = null;
            window.open('', '_self');
            window.close();
            return false;
        }

        jQuery(document).ready(function () {
            jQuery("#form1").validationEngine('attach', {
                onValidationComplete: function (form, status) {
                    if (status == false) {
                        //submitTimes = 0;
                        //closeDiv();
                    }
                }
            });

        })

    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div class="container-fluid">
        <div class="row">
            <fieldset>
                <legend>岗位信息<span class="right">
                    <asp:Button ID="btnSave" runat="server" CssClass="btn btn-primary" Text="保存" OnClick="btnSave_Click" />
                    <asp:Button ID="Button1" runat="server" CssClass="btn btn-warning" Text="保存并新增下一个"
                        OnClick="Button1_Click" />
                    <asp:Button ID="btnBack" runat="server" CssClass="btn" Text="关闭" OnClientClick="closeWin();return false;"
                        OnClick="btnBack_Click" />
                </span></legend>
                <div>
                    <table class="table table-condensed table-bordered" style="width: 90%">
                        <tr>
                            <td class="td-label">
                                部门：
                            </td>
                            <td class="td-content">
                                <div class="hidden">
                                    <asp:TextBox ID="txtDepartmentID" runat="server"></asp:TextBox></div>
                                <asp:TextBox ID="txtDepartmentName" runat="server"></asp:TextBox>
                                <button type="button" class="btn" onclick="selectUser(4,'txtDepartmentName','txtDepartmentID');">
                                    ...</button>
                            </td>
                            <td class="td-label">
                                上级岗位：
                            </td>
                            <td class="td-content">
                                <div class="hidden">
                                    <asp:TextBox ID="txtDirectReportID" runat="server"></asp:TextBox></div>
                                <asp:TextBox ID="txtDirectReportName" runat="server"></asp:TextBox>
                                <button type="button" class="btn" onclick="selectUser(1,'txtDirectReportName','txtDirectReportID');">
                                    ...</button>
                            </td>
                        </tr>
                        <tr>
                            <td class="td-label">
                                岗位*：
                            </td>
                            <td class="td-content">
                                <asp:TextBox ID="txtJobFunction" runat="server" CssClass="validate[required]"></asp:TextBox>
                            </td>
                            <td class="td-label">
                                岗位等级：
                            </td>
                            <td class="td-content">
                                <asp:DropDownList ID="ddlJobGrade" runat="server">
                                    <asp:ListItem></asp:ListItem>
                                    <asp:ListItem Text="Level1" Value="1"></asp:ListItem>
                                    <asp:ListItem Text="Level2" Value="2"></asp:ListItem>
                                    <asp:ListItem Text="Level3" Value="3"></asp:ListItem>
                                    <asp:ListItem Text="Level4" Value="4"></asp:ListItem>
                                    <asp:ListItem Text="Level5" Value="5"></asp:ListItem>
                                    <asp:ListItem Text="Level6" Value="6"></asp:ListItem>
                                    <asp:ListItem Text="Level7" Value="7"></asp:ListItem>
                                    <asp:ListItem Text="Level8" Value="8"></asp:ListItem>
                                    <asp:ListItem Text="Level9" Value="9"></asp:ListItem>
                                    <asp:ListItem Text="Level10" Value="10"></asp:ListItem>
                                    <asp:ListItem Text="Level11" Value="11"></asp:ListItem>
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td class="td-label">
                                部门负责人：
                            </td>
                            <td class="td-content" colspan="3">
                                <asp:CheckBox ID="cbxIsManager" runat="server" />
                            </td>
                        </tr>
                    </table>
                </div>
            </fieldset>
            <fieldset>
                <legend>人员信息<span class="right"> </span></legend>
                <div>
                    <table class="table table-condensed table-bordered" style="width: 90%">
                        <tr>
                            <td class="td-label">
                                用户ID：
                            </td>
                            <td class="td-content">
                                <asp:TextBox ID="txtUSERID" runat="server"></asp:TextBox>
                                <button type="button" class="btn" onclick="selectUser(1,'txtCNNAME','txtUSERID');__doPostBack('lbSelectUser','');">
                                    ...</button>
                            </td>
                            <td class="td-label">
                                工号：
                            </td>
                            <td class="td-content">
                                <asp:TextBox ID="txtUSERCODE" runat="server"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="td-label">
                                姓名*：
                            </td>
                            <td class="td-content">
                                <asp:TextBox ID="txtCNNAME" runat="server" CssClass="validate[required]"></asp:TextBox>
                            </td>
                            <td class="td-label">
                                主岗位：
                            </td>
                            <td class="td-content">
                                <asp:CheckBox ID="cbxIsPrimary" runat="server" Checked="true" />
                            </td>
                        </tr>
                        <tr>
                            <td class="td-label">
                                登录账号*：
                            </td>
                            <td class="td-content">
                                <asp:TextBox ID="txtLoginName" runat="server" CssClass="validate[required]"></asp:TextBox>
                            </td>
                            <td class="td-label">
                                Email：
                            </td>
                            <td class="td-content">
                                <asp:TextBox ID="txtEmail" runat="server"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="td-label">
                                手机：
                            </td>
                            <td class="td-content">
                                <asp:TextBox ID="txtTel" runat="server"></asp:TextBox>
                            </td>
                            <td class="td-label">
                                英文名：
                            </td>
                            <td class="td-content">
                                <asp:TextBox ID="txtQQ" runat="server"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="td-label">
                                排序号：
                            </td>
                            <td class="td-content">
                                <asp:TextBox ID="txtOrderNo" runat="server"></asp:TextBox>
                            </td>
                            <td class="td-label">
                                是否有效：
                            </td>
                            <td class="td-content">
                                <asp:CheckBox ID="cbxIsActive" runat="server" Checked="true" />
                            </td>
                        </tr>
                        <tr>
                            <td class="td-label">
                                成本中心：
                            </td>
                            <td class="td-content">
                                <asp:TextBox ID="txtExt01" runat="server"></asp:TextBox>
                            </td>
                            <td class="td-label">
                                扩展2：
                            </td>
                            <td class="td-content">
                                <asp:TextBox ID="txtExt02" runat="server"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="td-label">
                                扩展3：
                            </td>
                            <td class="td-content">
                                <asp:TextBox ID="txtExt03" runat="server"></asp:TextBox>
                            </td>
                            <td class="td-label">
                                扩展4：
                            </td>
                            <td class="td-content">
                                <asp:TextBox ID="txtExt04" runat="server"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="td-label">
                                扩展5：
                            </td>
                            <td class="td-content">
                                <asp:TextBox ID="txtExt05" runat="server"></asp:TextBox>
                            </td>
                            <td class="td-label">
                                扩展6：
                            </td>
                            <td class="td-content">
                                <asp:TextBox ID="txtExt06" runat="server"></asp:TextBox>
                            </td>
                        </tr>
                    </table>
                </div>
            </fieldset>
        </div>
    </div>
    <div class="hidden">
        <asp:TextBox ID="txtJobId" runat="server"></asp:TextBox>
        <asp:LinkButton ID="lbSelectUser" runat="server" OnClick="lbSelectUser_Click"></asp:LinkButton>
    </div>
    </form>
    <script type="text/javascript">
        $(document).ready(function () {
            //parent.document.getElementById('frmContent').height = 800;
        });
    </script>
</body>
</html>
