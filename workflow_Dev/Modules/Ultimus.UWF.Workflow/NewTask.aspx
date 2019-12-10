<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="NewTask.aspx.cs" Inherits="Ultimus.UWF.Workflow.NewTask" %>
<%@ Import Namespace="Ultimus.UWF.Common.Logic" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <link href="css/task.css" rel="stylesheet" type="text/css" />
          <script type="text/javascript" src="<%=Ultimus.UWF.Common.Logic.WebUtil.GetRootPath() %>/Assets/js/common.js"></script>
   
    <script type="text/javascript">
        function openForm(taskId, type, ele) {
            var sheight = screen.height - 150;
            var swidth = screen.width - 10;
            var winoption = "left=0,top=0,height=" + sheight + ",width=" + swidth + ",toolbar=yes,menubar=yes,location=yes,status=yes,scrollbars=yes,resizable=yes";
            s = window.open('OpenForm.aspx?TaskId=' + taskId + '&Type=' + type + '', '', winoption);

            s.focus();

        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <%--<div class="well well-small">
        <i class="icon-home"></i>
        <strong>新建申请</strong>
    </div>--%>
    
    <div class="listTable_1">
        <asp:Repeater ID="rpProcessCategory" runat="server">
            <ItemTemplate>
                <dl class="fqi" id="dlProcess<%#Eval("CategoryID")%>">
                    <dt><a href="javascript:void(0)" onclick="javascript:ShowProcess( '<%#Eval("CategoryID") %>');">
                        <img src="Images/<%#GetCategoryImage(Convert.ToString(Eval("CategoryName"))) %>" /></a></dt>
                    <dd>
                        <a href="#" style="width: 300px">
                           <%#Eval(Lang.Get("CategoryNameField"))%></a></dd>
                </dl>
            </ItemTemplate>
        </asp:Repeater>
    </div>
    <div>
        <asp:Repeater ID="rpAllProcess" runat="server" OnItemDataBound="rpAllProcess_ItemDataBound">
            <ItemTemplate>
                <ul class="tanchu" style="display: inline-block" id="ulProcess<%#Eval("CategoryID")%>">
                    <asp:Repeater ID="rpProcess" runat="server">
                        <ItemTemplate>
                            <li style="width: 300px">
                                <img alt="" src="Images/fav.jpg" onclick="changeFav(this,'');" />
                                <a onclick="javascript:openForm('<%#Eval("TASKID") %>','NEWREQUEST',this);" href="#">
                                    <%#Eval("PROCESSNAME")%></a>

                               <%-- <a href="OpenForm.aspx?TaskID=<%#Eval("InitiateID") %>&ProcessName=<%# Server.UrlEncode(Eval("ProcessName").ToString()) %>&StepName=<%# Server.UrlEncode(Eval("ProcessName").ToString()) %>&Incident=0&Type=newrequest"
                                    target="_blank">
                                    <%#Eval("ProcessName")%></a>--%>
                                    </li></ItemTemplate>
                    </asp:Repeater>
                </ul>
            </ItemTemplate>
        </asp:Repeater>
    </div>
    <div style="display: none">
        <asp:TextBox ID="txtCurrentCategory" runat="server"></asp:TextBox>
    </div>
    <script type="text/javascript">
        var currentResourceId = "";

        $(document).ready(function () {
            ShowProcess($("#txtCurrentCategory").val());

        });

        function ShowProcess(resourceID) {
            if (currentResourceId == resourceID) {
                return;
            }
            $(".daiban dl").attr("class", "fqi");
            $(".tanchu").hide();
            $("#dlProcess" + resourceID).attr("class", "fqi fqicurrently");
            $("#ulProcess" + resourceID).show();
            currentResourceId = resourceID;
        }

        function changeFav(ele, resourceID) {
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
   
    </script>
    </form>
</body>
</html>
