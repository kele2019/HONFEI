<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="NewTask2.aspx.cs" Inherits="Ultimus.UWF.Workflow.NewTask2" %>

<%@ Import Namespace="Ultimus.UWF.Common.Logic" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <link href="css/task.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="<%=Ultimus.UWF.Common.Logic.WebUtil.GetRootPath() %>/Assets/js/common.js"></script>
    <script type="text/javascript">
        function openForm(taskId, type,serverName, ele) {
            var sheight = screen.height - 150;
            var swidth = screen.width - 10;
            var winoption = "left=0,top=0,height=" + sheight + ",width=" + swidth + ",toolbar=yes,menubar=yes,location=yes,status=yes,scrollbars=yes,resizable=yes";
            s = window.open('OpenForm.aspx?ServerName='+serverName+'&TaskId=' + taskId + '&Type=' + type + '', '', winoption);

            s.focus();

        }
    </script>
    <style type="text/css">
        .well_new
        {
            min-height: 20px;
            padding: 19px;
            margin-bottom: 0px;
            background-color: #f5f5f5;
            border: 1px solid #e3e3e3;
            -webkit-border-radius: 4px;
            -moz-border-radius: 4px;
            border-radius: 4px;
            -webkit-box-shadow: inset 0 1px 1px rgba(0, 0, 0, 0.05);
            -moz-box-shadow: inset 0 1px 1px rgba(0, 0, 0, 0.05);
            box-shadow: inset 0 1px 1px rgba(0, 0, 0, 0.05);
        }
        .well-small_new
        {
            padding: 9px;
            -webkit-border-radius: 3px;
            -moz-border-radius: 3px;
            border-radius: 3px;
        }
        .tanchu_new
        {
            width: 94%;
            background: white;
            display: inline-block;
            line-height: 25px;
            position: relative;
            z-index: 999;
            top: 0px;
            padding: 10px; /*border-left: 1px #CCC solid;*/
            border-left: 4px solid #f5f5f5;
        }
        .tanchu_new li
        {
            float: left;
            width: 200px;
            line-height: 1px;
            margin-right: 10px;
            height: 25px;
            overflow: hidden;
        }
        .tanchu_new li a, .tanchu_new li a:hover
        {
            color: #000;
            font-weight: normal;
            padding-left: 10px;
            background-position: 0 4px;
        }
    </style>
    <script type="text/javascript">
        var tabs;

        $(function () {
            $(window).resize(function () { init(); })
            init();

            function init() {
                //                alert($('body').height())
                //                alert($('body').width())
                //                $(".tanchu_new").height($('body').height());
            }
        });
    </script>
    <style type="text/css">
    .layout{
        text-decoration: none;
    }
    .layout:hover{
        text-decoration: underline;cursor: pointer;
    }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div class="well_new well-small_new">
        <i class="icon-home"></i><strong style="font-size: medium">发起流程(<asp:Label ID="lblCount"
            runat="server" Text=""></asp:Label>)</strong>
        <div style="float:right">请输入要查找的流程名称：
            <asp:TextBox ID="txtSearch" runat="server" Width="150"></asp:TextBox>
            &nbsp;&nbsp;
            <asp:Button ID="Button1"
                runat="server" Text="搜索" CssClass="btn" onclick="Button1_Click" /></div>
    </div>
    <%--<div class="tabBtn">
         
            <ul style="height: 40px;">
            <li id="Li2" class='normalLi1 searchli_new tbtn' style="float:left;padding-left:10px;">
            <strong style="font-size: medium">
                发起流程</strong>
            </li>
                <li id="Li1" class='normalLi1 searchli_new ' style="float:right">
                    <p>
                        <asp:Button ID="Button2" runat="server" CssClass="btn tbtn" Text="搜索"  />
                    </p>
                </li>
            </ul>
        </div>--%>
    <div style="float: left; width: 18%; margin-top: 0px;">
        <div style="float: left;">
            <asp:Repeater ID="rpProcessCategory" runat="server">
                <ItemTemplate>
                    <dl style="float: left; width: 100%; margin: 4px 4px 4px 6px; height: 20px;" id="dlProcess<%#Eval("CategoryID")%>">
                        <%--<dt><a href="javascript:void(0)" onclick="javascript:ShowProcess( '<%#Eval("CategoryID") %>');">
                        <img src="Images/<%#GetCategoryImage(Convert.ToString(Eval("CategoryName"))) %>" /></a></dt>--%>
                        <dd>
                            <img src="Images/folder.png" />
                            <a href="javascript:void(0)" onclick="javascript:ShowProcess( '<%#Eval("CategoryID") %>');">
                                <%#Eval("CategoryName")%><%#Eval("PROCESSCOUNT")%></a></dd>
                    </dl>
                </ItemTemplate>
            </asp:Repeater>
        </div>
    </div>
    <%--.sMenu{width:43px;float:left;overflow:hidden;position:absolute;top:0;left:0;}--%>
    <div style="float: right; height: 100%; width: 82%; margin-top: 0px;">
        <asp:Repeater ID="rpAllProcess" runat="server" OnItemDataBound="rpAllProcess_ItemDataBound">
            <ItemTemplate>
                <ul class="tanchu_new" style="display: inline-block; min-height : 523px; height: 100%;" id="ulProcess<%#Eval("CategoryID")%>">
                    <asp:Repeater ID="rpProcess" runat="server">
                        <ItemTemplate>
                            <li style="width: 250px; height: 80px">
                                <table style="width: 240px; height: 75px; border-style:dashed; 
                                    border-width: 1px; border-color:#B7B7B7;">
                                    <tr>
                                        <td style="width: 45px;">
                                            <img style="margin-left: 5px;" alt="" src="Images/document.png" onclick="changeDoc(this,'<%#Eval("PROCESSNAME")%>','<%#Eval("HELPURL") %>');" />
                                        </td>
                                        <td style="text-align: left;">
                                            <%--<a style=" line-height:20px;" onclick="javascript:openForm('<%#Eval("TASKID") %>','NEWREQUEST','<%#Eval("ServerName") %>',this);" href="#">
                                                
                                                    </a>--%>
                                                    <font class="layout" style=" line-height:18px; font-weight:700; color:#5F8BB7" onclick="javascript:openForm('<%#Eval("TASKID") %>','NEWREQUEST','<%#Eval("ServerName") %>',this);"><%#Eval("PROCESSNAME")%></font>
                                        </td>
                                        <td style="width: 30px; text-align: center; vertical-align: text-top;">
                                            <img style="margin-top: 3px;cursor: pointer;" alt="" src="Images/question.png" onclick="changeFav(this,'<%#Eval("PROCESSNAME")%>','<%#Eval("HELPURL") %>');" />
                                            <%--<img style="margin-top: 3px;cursor: pointer;" alt="" src="Images/fav.jpg" onclick="showProcessPic(this,'<%#Eval("PROCESSNAME") %>');" />--%>
                                        </td>
                                    </tr>
                                </table>
                                <%-- <a href="OpenForm.aspx?TaskID=<%#Eval("InitiateID") %>&ProcessName=<%# Server.UrlEncode(Eval("ProcessName").ToString()) %>&StepName=<%# Server.UrlEncode(Eval("ProcessName").ToString()) %>&Incident=0&Type=newrequest"
                                    target="_blank">
                                    <%#Eval("ProcessName")%></a>--%>
                            </li>
                        </ItemTemplate>
                    </asp:Repeater>
                </ul>
            </ItemTemplate>
        </asp:Repeater>
        <ul class="tanchu_new" style="display: inline-block; min-height : 523px; height: 100%;" id="ulProcessall">
                    <asp:Repeater ID="rpProcess_search" runat="server">
                        <ItemTemplate>
                            <li style="width: 250px; height: 80px">
                                <table style="width: 240px; height: 75px; border-style:dashed; 
                                    border-width: 1px; border-color:#B7B7B7;">
                                    <tr>
                                        <td style="width: 45px;">
                                            <img style="margin-left: 5px;" alt="" src="Images/document.png" onclick="changeDoc(this,'<%#Eval("PROCESSNAME")%>','<%#Eval("HELPURL") %>');" />
                                        </td>
                                        <td style="text-align: left;">
                                            <%--<a style=" line-height:20px;" onclick="javascript:openForm('<%#Eval("TASKID") %>','NEWREQUEST','<%#Eval("ServerName") %>',this);" href="#">
                                                
                                                    </a>--%>
                                                    <font class="layout" style=" line-height:18px; font-weight:700; color:#5F8BB7" onclick="javascript:openForm('<%#Eval("TASKID") %>','NEWREQUEST','<%#Eval("ServerName") %>',this);"><%#Eval("PROCESSNAME")%></font>
                                        </td>
                                        <td style="width: 30px; text-align: center; vertical-align: text-top;">
                                            <img style="margin-top: 3px;cursor: pointer;" alt="" src="Images/question.png" onclick="changeFav(this,'<%#Eval("PROCESSNAME")%>','<%#Eval("HELPURL") %>');" />
                                            <%--<img style="margin-top: 3px;cursor: pointer;" alt="" src="Images/fav.jpg" onclick="showProcessPic(this,'<%#Eval("PROCESSNAME") %>');" />--%>
                                        </td>
                                    </tr>
                                </table>
                            </li>
                        </ItemTemplate>
                    </asp:Repeater>
                </ul>
    </div>
    <div style="display: none">
        <asp:TextBox ID="txtCurrentCategory" runat="server"></asp:TextBox>
    </div>
    <script type="text/javascript">
        var currentResourceId = "";

        $(document).ready(function () {
            $(".tanchu_new").hide();
            ShowProcess($("#txtCurrentCategory").val());

        });

        function ShowProcess(resourceID) {
            if (currentResourceId == resourceID) {
                return;
            }
            //$(".daiban dl").attr("class", "fqi");
            $(".tanchu_new").hide();
            //$("#dlProcess" + resourceID).attr("class", "fqi fqicurrently");
            $("#ulProcess" + resourceID).show();
            currentResourceId = resourceID;
        }

        function changeFav(ele, processname, url) {
            window.open(url, 'newwindow', 'width=' + (window.screen.availWidth - 10) + ',height=' + (window.screen.availHeight - 30) + ',top=0,left=0,toolbar=yes,menubar=yes,scrollbars=yes, resizable=yes,location=yes, status=no');
            //alert("暂无文档")
            //            $.post("AddFavHandler.ashx",
            //            { resourceID: resourceID },
            //            function (result) {
            //                if (result == "0") {
            //                    $(ele).attr("src", "images/fav.jpg");
            //                }
            //                else {
            //                    $(ele).attr("src", "images/fav1.jpg");
            //                }
            //            }
            //         );
        }

        function changeDoc(ele, processname, url) {
            window.open("TaskStatus.aspx?processname=" + escape(processname), 'newwindow', 'width=' + (window.screen.availWidth - 10) + ',height=' + (window.screen.availHeight - 30) + ',top=0,left=0,toolbar=yes,menubar=yes,scrollbars=yes, resizable=yes,location=yes, status=no');
        }

        function showProcessPic(ele, url) { 
            
        }
    </script>
    </form>
</body>
</html>
