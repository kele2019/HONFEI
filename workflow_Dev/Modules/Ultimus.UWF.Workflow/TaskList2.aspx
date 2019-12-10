<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="TaskList2.aspx.cs" Inherits="Ultimus.UWF.Workflow.TaskList2" %>

<%@ Import Namespace="Ultimus.UWF.Common.Logic" %>
<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <script type="text/javascript" src="<%=Ultimus.UWF.Common.Logic.WebUtil.GetRootPath()%>/Assets/js/common.js"></script>
    <script type="text/javascript" src="<%=WebUtil.GetRootPath() %>/Assets/js/listpage.js"></script>
    <script type="text/javascript" src="main.js"></script>
    <script type="text/javascript">
        function request(paras) {
            var url = location.href;
            var paraString = url.substring(url.indexOf("?") + 1, url.length).split("&");
            var paraObj = {}
            for (i = 0; j = paraString[i]; i++) {
                paraObj[j.substring(0, j.indexOf("=")).toLowerCase()] = j.substring(j.indexOf("=") + 1, j.length);
            }
            var returnValue = paraObj[paras.toLowerCase()];
            if (typeof (returnValue) == "undefined") {
                return "";
            } else {
                return returnValue;
            }
        }
        function toogleSearch() {
            $("#divQuery").toggle();
        }

        function changeCss1(ele) {
            $(ele).css("cur");
        }

        $(document).ready(function () {
            if ($("#txtShowQuery").val() == "1") {
                $("#divQuery").show();
            }

//            $("#tab td").each(function () {
//                $(this).css("text-align", "center");
//            });

            var type = request("Type");
            if (type.toLowerCase() == "myrequest") {
                $("td[id$='tdInitior']").each(function () {
                    $(this).css("display", "none");
                });
                $("td[id=CurrentUser]").each(function () {
                    $(this).css("display", "block");
                })
            }

        });

        function changeStatus(ele) {
            $("input[type='checkbox']").attr("checked", ele.checked);
            
        }

        function showPanel() {
              
        }
    </script>
</head>
<body>
    <style type="text/css">
        .hide
        {
            display: none;
        }
        
        .aspNetPager a
        {
        }
        
        .searchli_new
        {
            margin: 7px 0px 0px -5px;
            padding: 0px 0px 0px 0px;
            float: left;
            height: 25px;
            line-height: 25px;
            vertical-align: middle;
            cursor: pointer;
            font-family: "微软雅黑";
            color: #626262;
            border-left: 0px solid #d1d1d1 !important;
            border-right: 0px solid #d1d1d1 !important;
        }
        
        .table td
        {
            text-align:left;    
        }
    </style>
    <script type="text/javascript">
        function assign() {
            var taskid = "";
            $("#tab tr:not(:first)").each(function () {
                if ($(this).find("td:eq(0)").find("[type=checkbox]").attr("checked")) {
                    taskid += $(this).find("td:eq(0)").children()[1].innerText + ",";
                }
            });
            if (taskid != "") {
                taskid = taskid.substring(0, taskid.lastIndexOf(","));
            }
            var PageName = "Assign.aspx";
            if (taskid != "") {
                PageName += "?TaskID=" + encodeURI(taskid);
            }
            //window.open(PageName, "javascript", "top=100,left=200,height=400,width=500");
            window.open(PageName, "javascript", "");
            return false;
        }

        function back() {

            var PageName = "AssignmentList.aspx";
            window.showModalDialog(PageName, "javascript", "dialogHeight=800px;dialogWidth=500px;scroll=no;");
            location.href = location.href;
            return false;
        }

        function Abort() {
            var iFlag = true;
            $("#tab tr").each(function () {
                if ($(this).find("td:eq(0)").children().attr("checked")) {
                    //alert($(this).find("div[id=status]")[0].innerText);
                    if ($(this).find("div[id=status]")[0].innerText == "已完成"
                    || $(this).find("div[id=status]")[0].innerText == "终止"
                    || $(this).find("div[id=status]")[0].innerText == "Completed"
                    || $(this).find("div[id=status]")[0].innerText == "Abort"
                    ) {
                        iFlag = false;
                        alert('<%=CanNotCancel %>');
                        return false;
                    }
                }
            });
            return window.confirm("您确定要终止流程吗?");
        }

        function openForm(taskId, type,serverName, ele) {
            var sheight = screen.height - 150;
            var swidth = screen.width - 10;
            var winoption = "left=0,top=0,height=" + sheight + ",width=" + swidth + ",toolbar=yes,menubar=yes,location=yes,status=yes,scrollbars=yes,resizable=yes";
            s = window.open('OpenForm.aspx?ServerName='+serverName+'&TaskId=' + taskId + '&Type=' + type + '', '', winoption);

            s.focus();
            if ($('#txtType').val() == '') {
                ele.parentNode.parentNode.style.display = 'none';
            }
        }
    </script>
    <form id="form1" runat="server">
    <div id="right">
        <div id="tabBtn" style="display: none;">
            <p>
                <%=Lang.Get("TaskList_ProcessCategory")   %>:</p>
            <ul>
                <asp:Repeater ID="rptProcessCategory" runat="server">
                    <ItemTemplate>
                        <a href='<%#GetProcessCategoryUrl(Eval("CATEGORYNAME").ToString()) %>' class='<%#GetProcessCategoryCurCss(Eval("CATEGORYNAME").ToString()) %>'
                            target="_self">
                            <li id="sortbtn1" onclick="circuitSel(this)">
                                <img src="images/<%#Eval("ICON") %>" />
                                <%#GetCount(MyLib.ConvertUtil.ToString(Eval("CategoryName")))%>
                                <br />
                                <%#Eval(Lang.Get("CategoryNameField"))%></li>
                        </a>
                    </ItemTemplate>
                </asp:Repeater>
            </ul>
        </div>
        <div class="tabBtn">
         
            <ul style="height: 40px;">
            <li id="Li2" class='normalLi1 searchli_new tbtn' style="float:left;padding-left:10px;">
            <strong style="font-size: medium" onclick="location.href=location.href;return false;">
                <asp:Literal ID="ltTitle" runat="server" ></asp:Literal></strong>
            </li>
                <li id="Li1" class='normalLi1 searchli_new ' style="float:right">
                    <p>
                        <asp:Button ID="Button2" runat="server" CssClass="btn tbtn" Text="刷新任务" 
                            onclick="Button2_Click"   />
                        <span id="selPanel">
                        <asp:Button ID="btnAssign" runat="server" CssClass="btn tbtn" Text="委托办理" OnClientClick="assign();return false;"
                            Visible="false" />
                        <asp:Button ID="btnAssignCallback" runat="server" CssClass="btn tbtn" Text="撤销委托" Visible="false"
                            OnClientClick="location.href='AssignmentList.aspx';return false;" />
                        <asp:Button ID="btnAbort" runat="server" CssClass="btn tbtn" Visible="false" Text="终止流程"
                            OnClick="btnAbort_Click" OnClientClick="return Abort();" />
                        <asp:Button ID="btnAlert" runat="server" CssClass="btn tbtn"  
                            Visible="false" Text="催办" OnClick="btnAlert_Click" /></span>
                        <input   type="button" class="btn tbtn" value="搜索任务" onclick="toogleSearch();" />
                    </p>
                </li>
            </ul>
        </div>
        <div id="divQuery" class="hide">
            <table cellpadding="5">
                <tr>
                    <td>
                        <asp:Label ID="lblProcessName" runat="server" Text="流程名称" CssClass="lbl"></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="txtProcessName" CssClass="input-query" runat="server"></asp:TextBox>
                    </td>
                    <td>
                        <asp:Label ID="lblStartTime" runat="server" Text="开始时间" CssClass="lbl"></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="txtStartDate" CssClass="input-query" onclick="WdatePicker({skin:'whyGreen',dateFmt:'yyyy/MM/dd'})"
                            runat="server"></asp:TextBox>
                        <asp:Label ID="Label6" runat="server" Text="-" CssClass="lbl"></asp:Label>
                        <asp:TextBox ID="txtEndDate" CssClass="input-query" onclick="WdatePicker({skin:'whyGreen',dateFmt:'yyyy/MM/dd'})"
                            runat="server"></asp:TextBox>
                    </td>
                    <td>
                        <asp:Label ID="lblIncident" runat="server" Text="实例号" CssClass="lbl"></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="txtIncident" CssClass="input-query" runat="server"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="lbQstep" runat="server" Text="步骤名称" CssClass="lbl"></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="txtStepName" runat="server" CssClass="input-query"></asp:TextBox>
                    </td>
                    <td>
                       <asp:Label
                            ID="lblApplicant" runat="server" Text="申请人" CssClass="lbl"  ></asp:Label>
                    </td>
                    <td>
                       <asp:TextBox
                            ID="txtApplicant" CssClass="input-query" runat="server"  ></asp:TextBox>
                    </td>

                    <td>
                        <asp:Label ID="lblSummary" runat="server" Text="摘要" CssClass="lbl"></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="txtSummary" CssClass="input-query" runat="server"></asp:TextBox>
                        <asp:Button ID="btnSearch" runat="server" CssClass="btn btn-primary" Text="查询" OnClick="btnSearch_Click" />
                    </td>
                    <td>
                    </td>
                    <td>
                    </td>
                </tr>
            </table>
        </div>
        <table id="tab" class="tab" width="100%" border="0" cellpadding="5" cellspacing="0">
            <thead>
                <tr>
                    <th align="center" style="border-right: 1px solid #E8F0E8; width:3%;">
                        <input type="checkbox" id="allCheck" onclick="changeStatus(this)" />
                    </th>
                    <th style="border-right: 1px solid #E8F0E8;width:5%;">
                        <%=Lang.Get("TaskList_Monitor") %>
                    </th>
                    <th style="border-right: 1px solid #E8F0E8;width:15%;">
                        <%=Lang.Get("TaskList_ProcessName") %>
                    </th>
                    <th style="border-right: 1px solid #E8F0E8;width:7%;">
                        申请人
                    </th>
                    <th style="border-right: 1px solid #E8F0E8;width:5%;">
                        <%=Lang.Get("TaskList_Incident") %>
                    </th>
                    
                    <th style="border-right: 1px solid #E8F0E8;width:28%;">
                        <%=Lang.Get("TaskList_Summary") %>
                    </th>
                    <th style="border-right: 1px solid #E8F0E8;width:13%;">
                        当前步骤与处理人
                    </th>
                    <th style="border-right: 1px solid #E8F0E8;width:10%;">
                        接收|完成时间
                    </th>
                    <th style="border-right: 1px solid #E8F0E8;width:8%;">
                        完成时限
                    </th>
                    <th>
                        <%=Lang.Get("TaskList_Status")%>
                    </th>
                </tr>
            </thead>
            <asp:Repeater ID="rptTask" runat="server">
                <ItemTemplate>
                    <tr>
                        <td align="center" style="border-right: 1px solid #E8F0E8;">
                            <asp:CheckBox ID="cbSelect" runat="server" onchange="showPanel();" /><div class="hide">
                                <%#Eval("TaskID") %>
                                <asp:HiddenField ID="hfTaskid" runat="server" Value='<%# Eval("TaskID") %>' />
                            </div>
                        </td>
                        <td style="border-right: 1px solid #E8F0E8;text-align:center; " >
                            <a href="TaskStatus.aspx?ProcessName=<%#Server.UrlEncode(Eval("PROCESSNAME").ToString().Trim()) %>&ServerName=<%#Eval("ServerName") %>&Incident=<%#Eval("INCIDENT") %>"
                                target="_blank">
                                <img src="images/control.png" /></a>
                        </td>
                         <td style="border-right: 1px solid #E8F0E8;text-align:left;">
                            <a onclick="javascript:openForm('<%#Eval("TaskID") %>','<%=Request.QueryString["Type"] %>','<%#Eval("ServerName") %>',this);"
                                style="cursor: hand" href="#">
                                <%#Eval("PROCESSNAME")%></a>
                        </td>
                        <td style="border-right: 1px solid #E8F0E8;text-align:left;">
                            <%#GetName(Eval("Initiator"), Eval("ServerName"))%>
                        </td>
                        <td style="border-right: 1px solid #E8F0E8;text-align:center;">
                            <%#Eval("INCIDENT")%>
                        </td>
                       
                        <td style="border-right: 1px solid #E8F0E8;text-align:left;">
                            <%#Eval("SUMMARY")%>
                        </td>
                        <td style="border-right: 1px solid #E8F0E8;text-align:left;">
                            <%#GetCurrentUser(Eval("STEPLABEL"), Eval("ProcessName"), Eval("Incident"),Eval("ServerName"))%>
                        </td>
                        <td style="border-right: 1px solid #E8F0E8;text-align:center;">
                            <%#Convert.ToDateTime(Eval("STARTTIME")).ToString("MM月dd日 HH:mm")%><br />
                            <%#Convert.ToDateTime(Eval("STARTTIME")) == Convert.ToDateTime(Eval("ENDTIME")) ? "" : Convert.ToDateTime(Eval("ENDTIME")).ToString("MM月dd日 HH:mm")%>
                        </td>
                        <td style="border-right: 1px solid #E8F0E8;">
                            <%#Convert.ToDateTime(Eval("OVERDUETIME")) == DateTime.MinValue ? "" : Convert.ToDateTime(Eval("OVERDUETIME")).ToString("yyyy/MM/dd HH:mm:ss")%>
                        </td>
                        <td>
                            <div id="status">
                                <%#GetStatus(Eval("PROCESSSTATUS").ToString()) %></div>
                        </td>
                    </tr>
                </ItemTemplate>
            </asp:Repeater>
            <tfoot>
                <tr class="pageTr">
                    <td colspan="11">
                        <div id="pageBar">
                            <div id="pageLeft">
                                <%--<asp:Button ID="btnAssign" runat="server" CssClass="btn" Text="指派" OnClientClick="assign();return false;"
                                    Visible="false" />--%>
                                <%--<asp:Button ID="btnAssignCallback" runat="server" CssClass="btn" Text="收回指派" Visible="false"
                                    OnClientClick="location.href='AssignmentList.aspx';return false;" />
                                    
                                <asp:Button ID="btnAbort" runat="server" CssClass="btn" Visible="false" Text="作废"
                                    OnClick="btnAbort_Click" OnClientClick="return Abort();" />--%>
                                共有<asp:Label ID="lblCount" runat="server" Text=""></asp:Label>条记录.
                            </div>
                            <div id="pageRight">
                                <webdiyer:AspNetPager ID="AspNetPager1" runat="server"  CustomInfoHTML="共有 %RecordCount% "
                                    Width="100%" CssClass="aspNetPager" CurrentPageButtonClass="pageBtn" OnPageChanged="AspNetPager1_PageChanged"
                                    AlwaysShow="true" PageSize="9" >
                                </webdiyer:AspNetPager>
                            </div>
                        </div>
                    </td>
                </tr>
            </tfoot>
        </table>
    </div>
    <div style="display: none">
        <asp:TextBox ID="txtType" runat="server"></asp:TextBox>
        <asp:TextBox ID="txtPreSort" runat="server"></asp:TextBox>
        <asp:TextBox ID="txtSort" runat="server"></asp:TextBox>
        <asp:TextBox ID="txtStartTime" runat="server"></asp:TextBox>
        <asp:TextBox ID="txtDateType" runat="server"></asp:TextBox>
        <asp:TextBox ID="txtShowQuery" runat="server"></asp:TextBox>
        <asp:TextBox ID="txtProcessCategory" runat="server"></asp:TextBox>
    </div>
    </form>
</body>
</html>
