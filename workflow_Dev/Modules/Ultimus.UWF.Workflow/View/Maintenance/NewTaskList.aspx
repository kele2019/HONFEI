<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="NewTaskList.aspx.cs" Inherits="Ultimus.UWF.Workflow.View.Maintenanc.NewTaskList" %>
<%@ Import Namespace="Ultimus.UWF.Common.Logic" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>流程待办</title>
    <script type="text/javascript" src="<%=Ultimus.UWF.Common.Logic.WebUtil.GetRootPath() %>/Assets/js/jquery.js"></script>
    <script type="text/javascript" src="<%=Ultimus.UWF.Common.Logic.WebUtil.GetRootPath() %>/Assets/js/jquery.pager.js"></script>
    <link href="../../Css/home.css" rel="stylesheet" type="text/css" />
    <link href="../../Css/pager.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript">
        $(function () {
            ajaxGetTaskList();
        });
        var pageIndex = 1;
        var pageSize = 5;
        var pageCount = 1;

        function ajaxGetTaskList() {//开始
            var pmt = { 'Method': 'NewTask', 'ProcessName': $("#ProcessName").val(), 'Incident': $("#Incident").val(),
                StratDate: $("#StartDate").val(), EndDate: $("#EndDate").val(), Summary: $("#Summary").val(),
                OrderBy: 't.StartTime desc', TaskStatus: 1, IncStatus: 1, PageIndex: pageIndex, PageSize: pageSize
            }; 
            //$.post($("#imangeUrl").val() + "Modules/Ultimus.UWF.Workflow/Controller/TaskClient.ashx", pmt, function (data) { loadTaskList(data); });
            $.post("../../Controller/TaskClient.ashx", pmt, function (data) { loadTaskList(data); });
            
        }
        //加载任务数据
        function loadTaskList(dataStr) {
            var data = eval("(" + dataStr + ")");
            if (data.TaskDatas != undefined) {
                $("#table_taskList tbody").empty();
                for (var i = 0; i < data.TaskDatas.length; i++) {
                    addTaskListRow(data.TaskDatas[i], i);
                }
                pageCount = (data.Count / pageSize) + ((data.Count % pageSize) > 0 ? 1 : 0);
                $("#pager").pager({ pagenumber: pageIndex, pagecount: pageCount, buttonClickCallback: PageClick });
            }
            //结束
        }
        //分页控件点击回传事件
        PageClick = function (pageclickednumber) {
            $("#pager").pager({ pagenumber: pageclickednumber, pagecount: pageCount, buttonClickCallback: PageClick });
            pageIndex = pageclickednumber;
            ajaxGetTaskList();
        }
        //添加任务行数据
        function addTaskListRow(data, rowIndex) {
            var html = "<tr>";
            html += "<td><input type='checkbox' pName='" + data.PROCESSNAME + "' pId='" + data.TASKID + "' pIncident='" + data.INCIDENT + "' /></td>";
            html += "<td><a href='OpenGraphical.aspx.aspx?TaskId=" + data.TASKID + "&ProcessName=" + escape(data.PROCESSNAME) + "&Incident=" + data.INCIDENT + "' target='_blank'><img src='" + $("#imangeUrl").val() + "Assets/images/bt04.jpg' alt='' width='32' height='32' /></a></td>";
            //html += "<td><a href='OpenForm.aspx?TaskId=" + data.TASKID + "&ProcessName=" + escape(data.PROCESSNAME) + "&Incident=" + data.INCIDENT + "' target='_blank'>" + data.PROCESSNAME + "</a></td>";
            html += "<td>" + data.INCIDENT + "</td>";
            html += "<td>" + data.TASKUSER + "&nbsp;</td>";
            html += "<td><a href='OpenForm.aspx?TaskId=" + data.TASKID + "&ProcessName=" + escape(data.PROCESSNAME) + "&Incident=" + data.INCIDENT + "' target='_blank'>" + data.PROCESSNAME + "</a><span>" + data.STEPLABEL + "&nbsp;</span>" + data.SUMMARY + "</td>";
            html += "<td>" + data.STARTTIME + "</td>";
            html += "</tr>";
            $("#table_taskList").append(html);
        }

        function btnSearch_Click() {
            pageIndex = 1;
            ajaxGetTaskList();
        }        
    </script>
    <style type="text/css">
table.gridtable {	font-family: verdana,arial,sans-serif;
	font-size:12px;
	line-height:18px;
	color:#333333;
	border-width: 1px;
	border-color: #CCC;
	border-collapse: collapse;
}
table.gridtable { 
	font-family: verdana,arial,sans-serif;
	font-size:12px;
	line-height:18px;
	color:#333333;
	border-width: 1px;
	border-color: #CCC;
	border-collapse: collapse;
}
table.gridtable th {
	border-width: 1px;
	padding: 8px;
	border-style: solid;
	border-color: #CCC;
	background-color: #f9f9f9;
}
table.gridtable td {
	border-width: 1px;
	padding: 2px;
	border-style: solid;
	border-color: #CCC;
	background-color: #ffffff;
}
span{ color:#CCC;}
table.bt td{ border:0;}
/*CSS quotes style pagination*/

DIV.quotes {
	PADDING-RIGHT: 3px; PADDING-LEFT: 3px; PADDING-BOTTOM: 3px; MARGIN: 3px; PADDING-TOP: 3px; TEXT-ALIGN: right
}
DIV.quotes A {
	BORDER-RIGHT: #ddd 1px solid; PADDING-RIGHT: 5px; BORDER-TOP: #ddd 1px solid; PADDING-LEFT: 5px; PADDING-BOTTOM: 2px; BORDER-LEFT: #ddd 1px solid; COLOR: #aaa; MARGIN-RIGHT: 2px; PADDING-TOP: 2px; BORDER-BOTTOM: #ddd 1px solid; TEXT-DECORATION: none
}
DIV.quotes A:hover {
	BORDER-RIGHT: #a0a0a0 1px solid; PADDING-RIGHT: 5px; BORDER-TOP: #a0a0a0 1px solid; PADDING-LEFT: 5px; PADDING-BOTTOM: 2px; BORDER-LEFT: #a0a0a0 1px solid; MARGIN-RIGHT: 2px; PADDING-TOP: 2px; BORDER-BOTTOM: #a0a0a0 1px solid
}
DIV.quotes A:active {
	BORDER-RIGHT: #a0a0a0 1px solid; PADDING-RIGHT: 5px; BORDER-TOP: #a0a0a0 1px solid; PADDING-LEFT: 5px; PADDING-BOTTOM: 2px; BORDER-LEFT: #a0a0a0 1px solid; MARGIN-RIGHT: 2px; PADDING-TOP: 2px; BORDER-BOTTOM: #a0a0a0 1px solid
}
DIV.quotes SPAN.current {
	BORDER-RIGHT: #e0e0e0 1px solid; PADDING-RIGHT: 5px; BORDER-TOP: #e0e0e0 1px solid; PADDING-LEFT: 5px; FONT-WEIGHT: bold; PADDING-BOTTOM: 2px; BORDER-LEFT: #e0e0e0 1px solid; COLOR: #aaa; MARGIN-RIGHT: 2px; PADDING-TOP: 2px; BORDER-BOTTOM: #e0e0e0 1px solid; BACKGROUND-COLOR: #f0f0f0
}
DIV.quotes SPAN.disabled {
	BORDER-RIGHT: #f3f3f3 1px solid; PADDING-RIGHT: 5px; BORDER-TOP: #f3f3f3 1px solid; PADDING-LEFT: 5px; PADDING-BOTTOM: 2px; BORDER-LEFT: #f3f3f3 1px solid; COLOR: #ccc; MARGIN-RIGHT: 2px; PADDING-TOP: 2px; BORDER-BOTTOM: #f3f3f3 1px solid
}

</style>
</head>
<body>
    <form id="form1" runat="server">
        <input type="hidden" id="imangeUrl" value="<%=Ultimus.UWF.Common.Logic.WebUtil.GetRootPath() %>/" />
        <div id="mc">
            <div class="location" style="display:none"><p>当前位置>待办任务</p></div>
            <div class="status">
                <div class="taskAll">
                    <ul class="pshow pshowSystem">
                        <li class="syrw">
                            <p class="name">所有流程</p>
                            <p class="num">3254</p>                        
                        </li>
                    </ul>
                </div>
                <ul class="pshow pshowSystem">
                    <li class="xzhq">
                        <p class="name">行政后勤</p>
                        <p class="num">254</p>
                    
                    </li>
    <!--                 <li class="rlzy">
                        <p class="name">人力资源</p>
                        <p class="num">180</p>
                    
                    </li>
                    <li class="kjgl">
                        <p class="name">科技管理</p>
                        <p class="num">342</p>
                    
                    </li>
                    <li class="itfw">
                        <p class="name">IT服务</p>
                        <p class="num">260</p>
                    
                    </li>
                    <li class="nksj">
                        <p class="name">内控审计</p>
                        <p class="num">36</p>
                    
                    </li>

                    <li class="zlfz">
                        <p class="name">战略发展</p>
                        <p class="num">45</p>
                    
                    </li>

                    <li class="cwgl">
                        <p class="name">财务管理</p>
                        <p class="num">186</p>
                    
                    </li>
                    <li class="jyone">
                        <p class="name">精益ONE</p>
                        <p class="num">180</p>
                    
                    </li>
                    <li class="flsw">
                        <p class="name">法律事务</p>
                        <p class="num">180</p>
                    
                    </li>
                    <li class="zjgl">
                        <p class="name">资金管理</p>
                        <p class="num">256</p>
                    
                    </li> -->
                </ul>
                <div class="c"></div>
            </div>
            <div class="task taskNoWrap">
                <div class="taskTit">流程中心</div>
                <div class="taskTitMore" style="display:none"><a href="#" >More</a></div>                
                    <div class="search" style="padding-left:10px">
                        <table width="96%" border="0" cellspacing="0" cellpadding="0">
                            <tr style="display:none">
                                <td width="2%">&nbsp;</td>
                                <td width="6%"><a href="#"><img src="<%=Ultimus.UWF.Common.Logic.WebUtil.GetRootPath() %>/Assets/images/sxlb.jpg" alt="" width="66" height="24" /></a></td>
                                <td width="6%"><a href="#"><img src="<%=Ultimus.UWF.Common.Logic.WebUtil.GetRootPath() %>/Assets/images/wtbl.jpg" alt="" width="66" height="24" /></a></td>
                                <td width="31%"><a href="#"><img src="<%=Ultimus.UWF.Common.Logic.WebUtil.GetRootPath() %>/Assets/images/scdwjj.jpg" alt="" width="93" height="24" /></a></td>
                                <td width="57%">
                                    
                                    
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    流程名称:<asp:DropDownList ID="ProcessName" runat="server"></asp:DropDownList>
                                    实例号:<input type="text" id="Incident" />
                                    开始日期:<input type="text" id="StartDate" />-<input type="text" id="EndDate" />
                                    摘要：<input type="text" id="Summary" value="" size="0" style="line-height:24px;" />                                    
                                    <input type="button" value="高级" />&nbsp;
                                    <input type="button" value="搜索" onclick="btnSearch_Click()" />
                                    <input type="button" value="批量指派" />&nbsp;
                                    <input type="button" value="全部指派" />
                               </td>
                            </tr>
                        </table>
                    </div>
                    <div style="padding:10px; padding-top:0;"> 
                        <table width="100%" class="gridtable" id="table_taskList">
                            <thead>
                                <tr>
                                    <th width="20">
                                        <input type="checkbox" name="checkbox" id="checkbox" />
                                        <label for="checkbox"></label>
                                    </th>
                                    <th width="24">&nbsp;</th>
                                    <th width="89">实例号</th>
                                    <th width="153">申请人</th>
                                    <th>流程名&amp;步骤&amp;摘要</th>
                                    <th width="146">申请时间</th>
                                </tr>    
                            </thead>            
                      </table>
                      <div id="pager" style="float: right; padding-right: 20px"></div>
                    </div>
                <div class="c"></div>
            </div>
            <p class="fixFolat">&nbsp;</p>        
        </div> 
    </form>
</body>
</html>
