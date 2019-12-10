<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="Ultimus.UWF.Home3.Login" %>

<%@ Import Namespace="Ultimus.UWF.Common.Logic" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>
        <%=Lang.Get("Login_Title")%></title>
    <script src="<%=Ultimus.UWF.Common.Logic.WebUtil.GetRootPath()%>/assets/js/jquery.js"
        type="text/javascript"></script>
    <script src="<%=Ultimus.UWF.Common.Logic.WebUtil.GetRootPath()%>/assets/js/jquery.cookie.js"
        type="text/javascript"></script>
    <link href="style.css" type="text/css" rel="stylesheet" />
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
            // $.cookie('lang', "en-US", { expires: 3 });
            return true;
        }
    </script>
</head>
<body id="bodyy">
    <form id="form1" class="form" runat="server">
    <div id="loginbg" class="loginbg">
        <div class="loginfont">
            <%=Lang.Get("Login_ProjectTitle")%></div>
        <div>
            <asp:TextBox ID="txtUser" runat="server" class="txt1" ></asp:TextBox>
        </div>
        <div>
            <asp:TextBox ID="txtPassword" runat="server" class="txt2" TextMode="Password" ></asp:TextBox>
        </div>
        <div>
            <asp:DropDownList ID="ddlDomains" class="txt3" runat="server">
            </asp:DropDownList>
        </div>
        <div class="rempwd">
            <div class="rempwd_l">
                <asp:CheckBox ID="cbxRememberMe"   Checked="true" runat="server" />&nbsp; &nbsp;<label
                    for="cbxRememberMe" style="display: inline"><%=Lang.Get("Login_RememberMe")%></label>
            </div>
            <div class="rempwd_r">
                
            </div>
        </div>
        <div>
            <asp:Button ID="btnSubmit" runat="server" Text="" OnClientClick="return validate();"
                class="btn " OnClick="btnSubmit_Click" />
        </div>
    </div>
    <!--[if gte IE 6]>
    <script type="text/javascript">  
        document.getElementById('bodyy').style.height = document.documentElement.clientHeight + "px";
    </script>
    <![endif]-->
    <div class="hide">
        <asp:HiddenField ID="hidReturnUrl" runat="server" />
    </div>
    </form>
</body>
</html>
