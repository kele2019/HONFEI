<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="Ultimus.UWF.Home3.Default" %>

<%@ Import Namespace="Ultimus.UWF.Common.Logic" %>
<%@ Import Namespace="Ultimus.UWF.Common.Logic" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>
        <%=Lang.Get("Default_ProjectTitle") %>
    </title>
    <meta http-equiv="Content-Type" content="text/html" charset="utf-8" />
    <link rel="stylesheet" type="text/css" href="CSS/base.css" media="all" />
    <script src="js/jquery.min.js" type="text/javascript"></script>
    <script src="js/jquery.tabs.js" type="text/javascript"></script>
    <script type="text/javascript">
        var tabs;

        $(function () {

            tabs = new EM.ui.tabs({
                el: '#tabdiv',
                closable: true,
                refresh: true,
                selectedIndex: 0,
                headerWidth: '76px'
            });
            add("<%=Lang.Get("DEFAULT_MYTASK") %>", "../Ultimus.UWF.Workflow/TaskList2.aspx?type=mytask", "3", "false"); //home.html

            $(".topIcon li").click(function () {
                $(this).addClass("current").siblings().removeClass("current").removeClass("currentHover");
                var currentItem = $(this).find("span").attr("class");
                var currentItemId = "#" + currentItem;
                var toggleWidth = 0;
                if ($("#sm").is(":visible") && $(currentItemId).is(":visible")) {
                    $("#sm").fadeOut();

                } else if ($("#sm").is(":visible") && !$(currentItemId).is(":visible")) {
                    //alert("当前菜单不会关闭, \n对应的二级子菜单将被打开");
                    showItem();
                    $(currentItemId).show();
                } else {
                    $("#sm").fadeIn();
                    showItem();
                    $(currentItemId).show();

                }

                function showItem() {
                    $("#sm > div").hide();
                    toggleWidth = 180;
                }

                //$("#tabdiv").css("margin-left", 43 + toggleWidth);

                var EMTabsHeight = $('body').height() - 25 - 89 - 5;
                var EMTabsWidth = $('body').width() - 46 - toggleWidth;
                $('iframe').height(EMTabsHeight).width(EMTabsWidth);
                $(".em-tab-strip-wrap,.em-tab-panel-header").width(EMTabsWidth);
                $('iframe').parents("div[class*='em-tab-content']").height(EMTabsHeight).width(EMTabsWidth);
                
                $(".sMenu").css("width", 43 + toggleWidth)
                return false;
            })

            $("#ssi li").hover(function () {
                $(this).addClass("currentHover");
            }, function () {
                if ($(this).hasClass("current")) {
                } else {
                    $(this).removeClass("currentHover");
                }
            })


            //弹出菜单收缩20140703
            $("#sm ul li").click(function (event) {
                $(".smula").hide();
                if ($(this).find('.smula').length) $(this).find('.smula').show();
            });
            //弹出菜单收缩20140703

            //头部收缩功能20140710
            $(".headerToggle").click(function (event) {
                var topHeight = 0;
                var sideHeight = 0;
                if ($(".top").is(":visible")) {
                    topHeight = 62; sideHeight = 62;
                } else {
                    topHeight = -62;
                }
                //alert(typeof(topHeight) +","+topHeight);

                var bh = $("body").height() - 70;
                $(".mid").height(bh + sideHeight);
                $("#ssi,#sm").height(bh + sideHeight);//- 30 

                $(".top").toggle(400);
                $(this).toggleClass("headerToggleOpen");
                var EMTabsHeight = $('body').height() - 25 - 70 - 5 + topHeight;
                var EMTabsWidth = $('body').width() - 46;
                $('iframe').height(EMTabsHeight).width(EMTabsWidth);
                $(".em-tab-strip-wrap,.em-tab-panel-header").width(EMTabsWidth);
                $('iframe').parents("div[class*='em-tab-content']").height(EMTabsHeight).width(EMTabsWidth);
            });
            //头部收缩功能20140710

            $(window).resize(function () { init(); })
            init();

        });
        //新增页签
        function add(Title, Url, TabName, closable) {
            aa= tabs.add(Title, Url, TabName, closable,"",Title);
            //$(".em-tab-strip li").removeAttr("title");
        }
        //关闭当前
        function removeCurrent() {
            tabs.removeCurrent();
        }

        function init() {
            var th = $(".top").is(":visible") ? 0 : 62;
            var bh = $("body").height() - 70 + th;
            $(".mid").height(bh);
            $("#ssi,#sm").height(bh );//- 30
            $("html,body").css("overflow", "hidden");
            var EMTabsHeight = $('body').height()  - 70 - 5 + th;//- 25
            var EMTabsWidth = $('body').width() - 46;
            if ($("#sm").is(":visible")) EMTabsWidth -= 180;
            $('iframe').height(EMTabsHeight).width(EMTabsWidth);
            //alert($(".em-tab-strip-wrap").length );
            $(".em-tab-strip-wrap,.em-tab-panel-header").width(EMTabsWidth);
            $('iframe').parents("div[class*='em-tab-content']").height(EMTabsHeight).width(EMTabsWidth)
            $(".em-tab-panel-header").css("display", "none");
            //$(".em-tab-panel").css("height", $(".em-tab-content-selected").height + 30);
            //$(".em-tab-content-selected").css("height", $(".em-tab-content-selected").height + 30);
            
        }

        function ShowTop () {
                //$(".topIcon li").addClass("current").siblings().removeClass("current").removeClass("currentHover");
                var currentItem = $(".topIcon li").find("span").attr("class");
                var currentItemId = "#" + currentItem;
                var toggleWidth = 0;
                if ($("#sm").is(":visible") && $(currentItemId).is(":visible")) {
                    $("#sm").fadeOut();

                } else if ($("#sm").is(":visible") && !$(currentItemId).is(":visible")) {
                    //alert("当前菜单不会关闭, \n对应的二级子菜单将被打开");
                    showItem();
                    $(currentItemId).show();
                } else {
                    $("#sm").fadeIn();
                    $("#sm > div").hide();
                    toggleWidth = 180 ;//- 43;
                    $("#sm").css("width", 180 + 43)
                    $(currentItemId).show();

                }

                //$("#tabdiv").css("margin-left", 43 + toggleWidth);

                var EMTabsHeight = $('body').height() - 25 - 89 - 5;
                var EMTabsWidth = $('body').width() - 46 - toggleWidth;
                $('iframe').height(EMTabsHeight).width(EMTabsWidth);
                $(".em-tab-strip-wrap,.em-tab-panel-header").width(EMTabsWidth);
                $('iframe').parents("div[class*='em-tab-content']").height(EMTabsHeight).width(EMTabsWidth);
                
                $(".sMenu").css("width", 43 + toggleWidth)
                return false;
            }

         function ShowMenu() {
                $(".smula").hide();
                if ($("#sm ul li").find('.smula').length) $("#sm ul li").find('.smula').show();
            }   

    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div class="top" style="display: none;">
        <div class="fl" style="padding-left: 5px">
            <a href="Default.aspx">
                <img src="images/logo _biger.jpg"></a></div>
        <div class="fr">
            <a href="javascript:;" onclick="add('<%=Lang.Get("Default_PersonInfo")%>','../Ultimus.UWF.OrgChart/PersonInfo.aspx','personSet','true')">
                <%=User_FullName %></a> | <a href="Login.aspx" onclick="return confirm('<%=Default_LogoutConfirm %>');">
                    <%=Lang.Get("Default_Logout")%></a>&nbsp;&nbsp;&nbsp;&nbsp;
            <%--<select name="">
      <option>切换语言</option>
      <option>ENGLISH</option>
      <option>中文</option>
    </select>--%>
        </div>
    </div>
    <div class="mid">
        <table cellpadding="0" cellspacing="0">
            <tr>
                <td>
                    <div class="sMenu">
                        <div class="toptIcon" style="display: none;">
                            <a href="javascript:;" class="headerToggle"></a>
                        </div>
                        <div id="ssi" style="display: none;">
                            <ul class="topIcon">
                                <asp:Repeater ID="rptTop" runat="server">
                                    <ItemTemplate>
                                        <li class="<%#Eval("EXT01") %>"><span class="<%#Eval("ICON") %>" title="<%#Eval("DISPLAYNAME") %>">
                                            <%#Eval("DISPLAYNAME") %></span></li>
                                    </ItemTemplate>
                                </asp:Repeater>
                            </ul>
                        </div>
                        <div id="sm">
                            <asp:Repeater ID="rptTop1" runat="server" OnItemDataBound="rptTop1_ItemDataBound">
                                <ItemTemplate>
                                    <div id="<%#Eval("ICON") %>" class="hidden">
                                        <div class="smt">
                                            <%#Eval("DISPLAYNAME") %></div>
                                        <ul>
                                            <asp:Repeater ID="rptTop2" runat="server" OnItemDataBound="rptTop2_ItemDataBound">
                                                <ItemTemplate>
                                                    <li><a href="javascript:;" class="app" style="border-top: none">
                                                        <%#Eval("DISPLAYNAME") %></a>
                                                        <div class="smula">
                                                            <asp:Repeater ID="rptTop3" runat="server">
                                                                <ItemTemplate>
                                                                    <a href="javascript:;" title="<%#Eval("DISPLAYNAME") %>" class="dsub" onclick="add('<%#Eval("DISPLAYNAME") %>','<%#Eval("URL") %>','<%#Eval("ID") %>','true')">
                                                                        <%#Eval("DISPLAYNAME") %></a>
                                                                </ItemTemplate>
                                                            </asp:Repeater>
                                                        </div>
                                                    </li>
                                                </ItemTemplate>
                                            </asp:Repeater>
                                        </ul>
                                    </div>
                                </ItemTemplate>
                            </asp:Repeater>
                        </div>
                    </div>
                </td>
                <td>
                    <div id="tabdiv" class="bottomTab" style="margin-left: 223px;">
                        <ul>
                        </ul>
                    </div>
                </td>
            </tr>
        </table>
    </div>
    <div class="bot hidden" style="width: 100%" >
        <div class="" align="right" style="padding-right: 5px">
            <%=Lang.Get("Copyright")%></div>
    </div>
    </form>
    <script type="text/javascript">
        ShowTop();
        ShowMenu();
    </script>
</body>
</html>
