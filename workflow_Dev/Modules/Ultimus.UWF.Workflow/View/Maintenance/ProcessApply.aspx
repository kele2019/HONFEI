<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ProcessApply.aspx.cs" Inherits="Ultimus.UWF.Workflow.View.Maintenanc.ProcessApply" %>
<%@ Import Namespace="Ultimus.UWF.Common.Logic" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <script type="text/javascript" src="<%=Ultimus.UWF.Common.Logic.WebUtil.GetRootPath() %>/Assets/js/common.js"></script>
    <script src="js/jquery.js" type="text/javascript"></script>
    <script type="text/javascript">
        $(function () {
            ajaxGetTaskList();
        });
        var pageIndex = 1;
        var pageSize = 8;
        var pageCount = 1;

        function ajaxGetTaskList() {
            var pmt = { 'Method': 'Apply' };
            $.post("../../Controller/TaskClient.ashx", pmt, function (data) { loadPorcessList(data); });
        }
        //加载任务数据
        function loadPorcessList(dataStr) { 
            var data = eval("(" + dataStr + ")");
            if (data.DataList != undefined) {
                $("#table_process tbody").empty();
                for (var i = 0; i < data.DataList.length; i++) {
                    addPorcessListRow(data.DataList[i], i);
                }
            }
        }
        //添加任务行数据
        function addPorcessListRow(data, rowIndex) {
            var html = "<tr>";
            html += "<td><a href='OpenForm.aspx?TaskId=" + data.TASKID + "&ProcessName=" + escape(data.PROCESSNAME) + "' target='_blank'>" + data.PROCESSNAME + "</a></td>";           
            html += "</tr>";
            $("#table_process").append(html);
        }       
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <table width="100%" id="table_process">
            <thead>
                <tr>
                    <td>流程名称</td>
                </tr>
            </thead>
        </table>
    </form>
</body>
</html>
