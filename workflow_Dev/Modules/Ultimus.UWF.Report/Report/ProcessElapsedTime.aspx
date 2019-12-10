<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ProcessElapsedTime.aspx.cs"
    Inherits="BPM.ReportDesign.Report.ProcessElapsedTime" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>流程耗时分析表</title>
    <link href="css/reportCss.css" rel="stylesheet" type="text/css" />
    <script src="script/js.js" type="text/javascript"></script>
    <script src="../../js/jquery-1.4.2.min.js" type="text/javascript"></script>
    <script src="script/js.js" type="text/javascript"></script>
    <style>
        .mask
        {
            z-index:9998;
            position:fixed;
            top:0;
            left:0;
            width:100%;
            height:100%;
            background:#000;
            opacity:0.4;
            filter:alpha(opacity:40);
            display:none;
            }
            .mask img
            {
                display:block;
                margin:200px auto;
                z-index:9999;
                }
                a
                {
                    text-decoration:none;margin-left:5px;}
    </style>
    <script>
        window.onload = function () {
            var oTable = document.getElementById("table1");
            //var data = { processName: '', incident: '', steps: [{ stepName: '', stepOverTime: '', stepTime: '' }, { stepname: '', stepOverTime: '', stepTime: '' }, { stepname: '', stepOverTime: '', stepTime: ''}] };
            var oBtn = document.getElementById("btnSearch");
            var total;
            oBtn.onclick = function () {
                var processname = $("#txtProcessName").val();
                var stepname = $("#txtStepName").val();
                //                if ($("#txtStepName").val()) {
                //                    stepname = encodeURIComponent(stepname);
                //                }
                if (!$("#txtProcessName").val()) {
                    alert("请填写流程名称");
                } else {
                    $("#table1  tr").eq(0).html("");
                    $("#table1  tr").eq(1).html("");
                    $("#table1 tbody").html("");
                    $(".mask").show();
                    var isbtn = false;
                    $.post('AjaxPage/getlist.ashx', { ProcessName: processname, stepName: stepname, incident: $("#txtIncident").val(), index: 1 }, function (data) {
                        data = eval("(" + data + ")");
                        isbtn = true;
                        if (parseInt(data.listcount) > 0) {
                            $("#hide").hide();
                            setTable(data);

                            //alert(Math.ceil(total / 24));分页区域
                            getPage({ id: 'pageDiv', nowNum: 1, totalNum: Math.ceil(total / 10), callBack: function (nowNum, totalNum) {
                                if (!isbtn) {
                                    //alert('1');
                                    $(".mask").show();
                                    $.post('AjaxPage/getlist.ashx', { ProcessName: processname, stepName: stepname, incident: $("#txtIncident").val(), index: nowNum }, function (data) {
                                        data = eval("(" + data + ")");
                                        if (parseInt(data.listcount) > 0) {
                                            $("#hide").hide();
                                            setTable(data);
                                        } else {
                                            $("#hide").show();
                                        }
                                        $(".mask").hide();
                                    });
                                } else {
                                    isbtn = false;
                                }
                            }
                            });

                            //分页

                        } else {
                            $("#hide").show();
                        }
                        $(".mask").hide();

                    });

                }
            }


            function setTable(data) {
                total = data.listcount;
                $("#tips").html("共计" + total + "条数据");
                var clientwidth = document.documentElement.clientWidth + 30;
                var steplength = data.list[0].steps.length;
                //先找出 最长的 节点数
                for (var i = 0; i < data.list.length; i++) {
                    var len = data.list[i].steps.length;
                    if (steplength < len) {
                        steplength = len;
                    }
                }
                //table宽度的 设置
                if (steplength > 5) {
                    oTable.style.width = (clientwidth + (steplength - 5) * 260) + 'px';
                }
                var oTr1;
                oTr1 = $("#table1 tr").eq(0);
                var oTr2;
                oTr2 = $("#table1 tr").eq(1);
                var tmp1 = '<th colspan="3">活动节点</th>';
                var tmp2 = '<td style="width: 100px">节点名称</td><td style="width: 80px">时限要求</td><td style="width: 80px">实际耗时</td>';
                var str1 = ''; var str2 = '';
                for (var i = 0; i < steplength; i++) {
                    str1 += tmp1;
                    str2 += tmp2;
                }
                //console.log(document.documentElement.clientWidth); //1366
                //oTr1.innerHTML += str1;
                //oTr2.innerHTML += str2;
                oTr1.html("<th></th><th></th><th></th>" + str1);
                oTr2.html("<td style='width: 50px'>序号</td><td style='width: 150px'>流程名</td><td style='width: 50px'>                        实例号</td>" + str2);

                var allTrs = "";
                for (var i = 0; i < data.list.length; i++) {
                    var dataitem = data.list[i];
                    var color = i % 2 == 0 ? '#F9F9F9' : '';
                    var tr = '<tr onmouseover="currentcolor=this.style.backgroundColor;this.style.backgroundColor=\'#F3F3F3\';" onmouseout="this.style.backgroundColor=currentcolor;" style="background-color:' + color + ';cursor: pointer;"><td>' + (i + 1) + '</td><td>' + dataitem.processName + '</td><td>' + dataitem.incident + '</td>';
                    steplength = dataitem.steps.length;
                    for (var j = 0; j < steplength; j++) {
                        var stepitem = dataitem.steps[j];
                        tr += "<td>" + stepitem.stepName + "</td><td>" + stepitem.stepOverTime + "</td><td>" + stepitem.stepTime + "</td>";
                    }
                    tr += "</tr>";
                    allTrs += tr;
                    tr = "";
                }

                $("#table1 tbody").html(allTrs);
            }
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div class="divHead">
        <h2>
            流程耗时分析表</h2>
    </div>
    <div id="divCondition" runat="server" class="div_default_condition">
        <fieldset>
            <legend>查询条件</legend>
            <div class="div_default_field" style="width: 280px">
                <div>
                    流程名称&nbsp;<span style="color: Red">*</span></div>
                <div>
                    <input type="text" name='' runat="server" id="txtProcessName" style="width: 150px" /></div>
            </div>
            <div class="div_default_field">
                <div>
                    节点名称&nbsp;</div>
                <div>
                    <input type="text" name='' runat="server" id="txtStepName" /></div>
            </div>
            <div class="div_default_field">
                <div>
                    实例号&nbsp;</div>
                <div>
                    <input type="text" name='' runat="server" id="txtIncident" /></div>
            </div>
            <div class="div_default_field" style="width: 100px">
                <input type="button" id="btnSearch" class="bluebuttoncss fr" value="查 询" />
            </div>
        </fieldset>
    </div>
    <div>
        <table cellspacing="1" style="margin-left: 2px" border="1" bordercolor="#ccc" id="table1">
            <thead>
                <tr>
                    
                </tr>
                <tr>
                    <%--<td style="width: 50px">
                        序号
                    </td>
                    <td style="width: 150px">
                        流程名
                    </td>
                    <td style="width: 50px">
                        实例号
                    </td>--%>
                </tr>
            </thead>
            <tbody>
            </tbody>
        </table>

        <div id="hide" style="color:Red">无数据</div>
        <span id="tips"></span>
        <div id="pageDiv" style="font-size:12px;margin-top:10px;margin-left:5px"></div>
    </div>
    <div class="mask"><img src="css/loading.gif" /></div>
    </form>
</body>
</html>
