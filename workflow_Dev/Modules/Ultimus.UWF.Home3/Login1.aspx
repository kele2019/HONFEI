<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login1.aspx.cs" Inherits="Ultimus.UWF.Home3.Login1" %>
<%@ Import Namespace="Ultimus.UWF.Common.Logic" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title><%=Lang.Get("Login_Title")%></title>
    <script src="<%=Ultimus.UWF.Common.Logic.WebUtil.GetRootPath()%>/assets/js/common.js" type="text/javascript"></script>
    <link href="css/style.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript">
        self.moveTo(0, 0)
        self.resizeTo(screen.availWidth, screen.availHeight)

        $(document).ready(function () {
            var rem = $.cookie('rememberme');
            if (rem == 1) {
                $("#cbxRememberMe").attr("checked", "true");
            }
            else {
                $("#cbxRememberMe").removeAttr("checked");
            }
            $("#txtUser").val($.cookie('username'));
            $("#txtUser").focus();
        });
    </script>
</head>
<body>
    <script type="text/javascript">
        function checkError() {
            alert('<%=Lang.Get("Login_UserOrPasswordInvalid")%>');
        }

        function validate() {
            if ($("#txtUser").val() == "") {
                alert('<%=Lang.Get("Login_UserRequired")%>');
                return false;
            }
            var chx = $("#cbxRememberMe").attr("checked");
            var name = $("#txtUser").val();
            if (!chx) {
                chx = 0;
                name = "";
            }
            else {
                chx = 1;
            }
            $.cookie('rememberme', chx, { expires: 3 });
            $.cookie('username', name, { expires: 3 });
            //            $.cookie('lang', "en-US", { expires: 3 });
            return true;
        }
    </script>
    <form id="form1" class="form" runat="server">
    <div class="container" style="margin-top: 40px">
        <div class="row row20">
            <fieldset>
                <legend>
                    <img src="Images/logo.gif" alt="Logo" style="height: 51px; width: 212px" />
                    <img src="Images/split.png" class="pl10 pr10" />
        <span class="logotext"><%=Lang.Get("Login_ProjectTitle")%></span></legend>
                <div class="span8">
                    <img src="Images/login_bg.jpg" alt="Logo background"  />
                </div>
                <div class="span4">
                    <h4 class="hide">
                        <%=Lang.Get("Login_UserLogin")%></h4>
                    <label for="txtUser">
                        <%=Lang.Get("Login_UserName")%></label>
                    <asp:TextBox ID="txtUser" runat="server" placeholder='<%=Lang.Get("Login_PleaseInputUserName")%>'></asp:TextBox>
                    <div style="padding-top:5px"></div>
                    <label for="txtPassword">
                        <%=Lang.Get("Login_Password")%></label>
                    <asp:TextBox ID="txtPassword" runat="server" TextMode="Password" placeholder='<%=Lang.Get("Login_PleaseInputPassword")%>'></asp:TextBox>
                    <label for="ddlDomains" runat="server" id="lblDomains">
                       <%=Lang.Get("Login_Domain")%></label>
                        <asp:DropDownList ID="ddlDomains" runat="server" >
                        </asp:DropDownList>
                    <div>
                    <div style="padding-top:5px"></div>
                        <span>
                            <asp:CheckBox ID="cbxRememberMe" Checked="true" runat="server" /><label for="cbxRememberMe" style="display: inline"><%=Lang.Get("Login_RememberMe")%></label>
                            <span style="padding-left: 40px;" class="pt10">
                                <asp:Button ID="btnSubmit" runat="server" Text="Login" OnClientClick="return validate();"
                                    class="btn btn-primary " OnClick="btnSubmit_Click" /></span> </span>
                    </div>
                </div>
                
            </fieldset>
        </div>
        <hr />
        <div class="well">
            <p>
                <%=Lang.Get("Copyright")%></p>
        </div>
    </div>
    <div class="hide">
        <asp:HiddenField ID="hidReturnUrl" runat="server" />
    </div>
    </form>
</body>
</html>