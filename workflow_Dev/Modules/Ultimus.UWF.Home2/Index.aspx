<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Index.aspx.cs" Inherits="Ultimus.UWF.Home2.Index" %>
<%@ Import Namespace="Ultimus.UWF.Home2.Code" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <link href="css/workflow.css" rel="stylesheet" type="text/css" />
    <script src="js/jquery.min.js" type="text/javascript"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            var currentLanguage = '<%=LanguageHelper.Get("Language") %>';
            if (currentLanguage == "zh-cn") {
                $("#divLanguage_en").show();
                $("#divLanguage").html("English");
            }
            else {
                $("#divLanguage_zh").show();
                $("#divLanguage").html("中文");
            }

            var menuCode = "DBRW"; //默认
            var split = location.href.split("#");
            if (split.length > 1) {
                if ($("#trMenu_" + split[1]).length > 0)
                    menuCode = split[1];
            }
            $("#td_" + menuCode + "").attr("style", "background-color:#f5f5f5");
            onMenuChange(menuCode);

            onInitBigFrameBackground();
            if ($("#hdFlag").val() != "") {
                $("#mTopNavgation").html("");
            }
        });
        function onMouseOver_Out_Language(isOver) {
            var div = document.getElementById("divLanguageIcon");
            if (isOver)
                div.style.backgroundImage = "url(images/V2/language2.png)";
            else
                div.style.backgroundImage = "url(images/V2/language1.png)";
        }
        function changeLanguage() {
            var currentLanguage = '<%=LanguageHelper.Get("Language") %>';
            var targetLanguage = "";
            if (currentLanguage == "zh-cn")
                targetLanguage = "en-us";
            else
                targetLanguage = "zh-cn";
            setCookie("ClientLanguage", targetLanguage);
            location.reload();
        }
        function setCookie(c_name, value, expiredays) {
            var exdate = new Date()
            exdate.setDate(exdate.getDate() + expiredays)
            document.cookie = c_name + "=" + escape(value) + ((expiredays == null) ? "" : ";expires=" + exdate.toGMTString())
        }
        function onMenuChange(menuCode) {
            var trList = $(".menubox a");
            for (var i = 0; i < trList.length; i++) {
                $(trList[i]).attr("style", "background-color:white");
            }
            $("#td_" + menuCode + "").attr("style", "background-color:#f5f5f5");

            var menuTr = $("#trMenu_" + menuCode);
            //            $("#trMenu_" + menuCode).addClass("menuActive");
            //            $("#trMenu_" + menuCode + " .menuActiveLeftImg").show();

            var url = menuTr.attr("MenuUrl");
            $("#fContent").attr("src", url);
            location.href = "#" + menuCode;
        }
        function onInitBigFrameBackground() {
            var bigFrameHeight = $("#bigFrame").height();
            var oFrameHeight = $("#oFrame").height();
            var cj = bigFrameHeight - oFrameHeight;
            if (cj <= 0) {
                var height = bigFrameHeight + 40 - cj;
                var windowHeight = $(document).height();
                if (height < windowHeight)
                    height = windowHeight - 40;
                $("#bigFrame").height(height);
            }
        }
        function onBack() {
            $("#txtUserName").val("");
            $("#txtPwd").val("");
            if ($("#hdFlag").val() == "") {
                $("#divchange").toggle();
            }
        }
        function CheckUserInfo() {
            var username = $("#txtUserName").val();
            var password = $("#txtPwd").val();
            if (password == "" || username == "") {
                alert('<%=LanguageHelper.Get("CheckUserInfoNull") %>');
            }
            else {
                $.post("AjaxHandle.ashx", { "username": username, "password": password },
          function (msg) {
              if (msg == "success") {
                  $("#btnChangeUserInfo").click();
              }
              else {
                  alert('<%=LanguageHelper.Get("CheckUserInfoFail") %>');
              }
          });
            }
        }
    </script>
</head>
<body class="contant_body">
<form runat="server" id="form1">

 <div style="display:none">
      <asp:Button runat="server" CssClass="btn" ID="btnChangeUserInfo" Text="确定" OnClick="btnChangeUserInfo_Click" />
      <asp:HiddenField runat="server" ID="hdFlag" />
 </div>
 <div class="top">
  <div class="box" style="vertical-align:middle;">
    <img src="img/tbg.jpg" style="height:55%;width:auto;margin-top:2%;float:left;margin-left:1.3%;"/>
    <div style="float:left; height:20%; margin-top:2%;width:50%;">
    <p style="text-align:center;font-weight:bold;font-size:xx-large;padding-left:40%;" >Workflow Platform</p>
    </div>
    <div style="float:right;width:200px;margin-top:2%; margin-right:1%;">
        <%--<img id="employeeImg" style="diaplay:none;" src=""/>--%>
        <asp:Image ID="employeeImg" style="height:80px;width:auto;float:left;" runat="server" />
        <div style="float:left;margin-left:4%; margin-top:4%;">
        <asp:Label runat="server" ID="lbUserName" style="float:left;width:100%;"></asp:Label><br/> 
        <asp:Label runat="server" ID="lbUserDept" style="border:0px;float:left;margin-right:3%;"></asp:Label>
        <p style="float:left">|</p>
        <a href="Login.aspx" style="float:left;margin-left:3%;width:20%">Logout</a>
        </div>
    </div>
  </div>
</div>
<div class="contant">
  <div class="contant_left"  >
    <div>
        <dl><dt style="margin-left:6%;font-size:xx-large;">Menu</dt></dl><br />
        <div class="menubox" >
       
          <asp:Repeater ID="rptMenu" runat="server">
             <ItemTemplate>
             <dl style="margin-left:20%;" onclick="onMenuChange('<%#Eval("Code") %>');"  MenuUrl="<%#Eval("Url")%>" id="trMenu_<%#Eval("Code")%>">
             <dt><a href="javascript:void()"    id="td_<%#Eval("Code")%>"><%#Eval("Text")%></a>
             </dt>
             </dl>
              </ItemTemplate>
             </asp:Repeater>
        </div>
    </div>
  </div>
     <div class="contatn_right"  >
     <div id="divchange"  class="listpage_list" style="position:fixed; padding-top:0px; top:45%; left:40%;display:none;  width:400px;background-color:White;">
   <table  width="100%" class="listtable">
   <tr><td colspan="2"  class="type" style="text-align:center"><%=LanguageHelper.Get("UserAccountChange") %></td></tr>
   <tr>
  <td width="30%" style="text-align:right"><%=LanguageHelper.Get("Account")%>：</td>
  <td ><asp:TextBox runat="server" ID="txtUserName" CssClass="inputtext" Width="100px"></asp:TextBox></td>
   </tr>
      <tr>
  <td style="text-align:right"><%=LanguageHelper.Get("Password")%>：</td>
  <td><asp:TextBox runat="server" ID="txtPwd"  CssClass="inputtext" TextMode="Password" Width="100px"></asp:TextBox></td>
   </tr>
   <tr>
   <td colspan="2" style="text-align:center">
   <input type="button" value='<%=LanguageHelper.Get("BtnOk")%>' onclick="CheckUserInfo()" class="inputbtn" />&nbsp;&nbsp;&nbsp;&nbsp;
   <input type="button" value='<%=LanguageHelper.Get("BtnCancel")%>' onclick='javascript:$("#divchange").hide()' class="inputbtn" />   
   </td>
   </tr>
   </table>
   </div>
    <iframe id="fContent" height="950px" width="100%" frameborder="0" src="about:blank" class="mainContentIFrame" />
   </div>
   
   
       
    <div id="bigFrame"  style="display:none">
        <div id="oFrame">
            <div id="mTop">
                <div id="mTopNavgation" onclick="onBack();">
                账号切换
                    <img src="images/V2/back1.png"  width="24px" height="24px" style="padding:10px 0px 0px 14px; display:none;" border="0" onmouseover="this.src='images/V2/back2.png';" onmouseout="this.src='images/V2/back1.png';" />
                </div>
               
                <div id="mTopLanguage" >
                 <div style="float:left; font-size:14px;">Welcome：  </div>
                <div id="divLanguage_zh" class="languageText"  style="display:none;" onclick="changeLanguage();" onmouseover="onMouseOver_Out_Language(true);" onmouseout="onMouseOver_Out_Language(false);"> 中文</div>
                    <div id="divLanguage_en" class="languageText" style="display:none;" onclick="changeLanguage();" onmouseover="onMouseOver_Out_Language(true);" onmouseout="onMouseOver_Out_Language(false);">English</div>
                    <div id="divLanguageIcon" class="languageIcon" onclick="changeLanguage();" onmouseover="onMouseOver_Out_Language(true);" onmouseout="onMouseOver_Out_Language(false);">&nbsp;</div>
                </div>
            </div>
            <div id="mHeader">
                <table border="0" cellpadding="0" cellspacing="0" width="100%">
                    <tr>
                        <td align="left" width="218px">
                            <a href="#">
                                <img src="images/V2/logo.png" border="0" width="218px" height="80px" />
                            </a>
                        </td>
                        <td width="10px">&nbsp;</td>
                        <td align="center" style="background-color:#e4f0e4;">
                            <img src="<%=LanguageHelper.Get("Barner") %>" border="0" height="70px" />
                        </td>
                    </tr>
                </table>
            </div>
            <div id="mMain">
                <table border="0" cellpadding="0" cellspacing="0" width="100%">
                    <tr>
                        <td align="left" width="218px" valign="top">
                            <div style="height:10px;">&nbsp;</div>
                            <div>
                                <div>
                                    <table id="tblMenu" border="0" cellpadding="0" cellspacing="0" width="100%" class="menuCate2Frame">
                                        <%--<asp:Repeater ID="rptMenu" runat="server">
                                            <ItemTemplate>
                                                <tr id="trMenu_<%#Eval("Code")%>" height="50px" style="cursor:pointer;" MenuUrl="<%#Eval("Url")%>" onclick="onMenuChange('<%#Eval("Code") %>');">
                                                    <td width="20px" align="right">
                                                        <img src="images/V2/menuActive.png" class="menuActiveLeftImg" border="0" style="display:none;" />
                                                    </td>
                                                    <td class="menuCate2Title" id="td_<%#Eval("Code")%>">
                                                        <%#Eval("Text")%>
                                                    </td>
                                                </tr>
                                            </ItemTemplate>
                                        </asp:Repeater>--%>
                                    </table>
                                </div>
                            </div>
                        </td>
                        <td width="10px">&nbsp;</td>
                        <td align="left" valign="top">
                        </td>
                    </tr>
                </table>
            </div>
        </div>
    </div>
   </div>
    </form>
</body>
</html>
