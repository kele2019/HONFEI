//***********************标准表单的js存放文件***********************
var path = "../..";
var orgName = "quanyou.com.cn";
//var orgName = "Business Organization";


//设置路径
var r = document.getElementsByTagName("script");
for (var i = 0; i <= r.length; i++) {
    var obj = r[i];
    if (obj) {
        if (obj.src.toLowerCase().indexOf("project.js") > 0) {
            path = obj.src.toLowerCase().replace("/assets/js/project.js", "");
        }
    }
}

//加载jquery
document.write("<script language='javascript' src='" + path + "/assets/js/jquery.js'></script>");

function loadButton(frmId, type) {
    //表单居中
    $("body *").each(function () {
        if ($(this).css("left") != "auto") {
            if ($(document.body).width() > 850) {
                try {
                    $(this).css({ "left": eval(eval($(this).css("left").replace("px", "") * 1) + eval($(document.body).width() - eval($("div[id*='Frame']:eq(0)").css("width").replace("px", ""))) / 2) + "px" });
                }
                catch (e) {
                }
            }
        }
    });

    //隐藏Toolbar
    //    if (type < 9) {
    //        window.parent.theRows.rows = "0,*"; //type为123，那么隐藏。type为10 20 30那么显示
    //    }

    //type:1 开始节点 2 同意 3 同意+不同意
    //任务状态为待办才显示
    var ifco = window.parent.frames(0).document.theFCO;
    var taskStatus = ifco.GetTaskStatus();

    if ((type == 1 || type == 10 || type == 2 || type == 20) && taskStatus == 1) {
        //添加提交
        $("#" + frmId).append("<div id='div_area' style='height:43 px;padding-top:10px;'><center><input style='height:28;color:#ffffff;background-color:#008663;font-family:华文细黑;font-style:normal;font-weight:bold;font-size:10pt;width:80px;' id='btn_form_submit'  type='button' value='提交'></center></div>");

        //保存模板
        //        $("#div_area").find("center").append("&nbsp;&nbsp;<input style='height:28;color:#ffffff;background-color:#008663;font-family:华文细黑;font-style:normal;font-weight:bold;font-size:10pt;width:80px;' id='btn_form_savetemplate' type='button' value='保存模板'>");

    }

    if ((type == 3 || type == 30) && taskStatus == 1) {
        //添加同意
        $("#" + frmId).append("<div id='div_area' style='height:43 px;padding-top:10px;'><center><input style='height:28;color:#ffffff;background-color:#008663;font-family:华文细黑;font-style:normal;font-weight:bold;font-size:10pt;width:80px;' id='btn_form_approve'   type='button' value='同意'></center></div>");

        //添加不同意
        $("#div_area").find("center").append("&nbsp;&nbsp;<input style='height:28;color:#ffffff;background-color:#008663;font-family:华文细黑;font-style:normal;font-weight:bold;font-size:10pt;width:80px;' id='btn_form_return'  type='button' value='不同意'>");
    }

    if (type == 2 || type == 3 || type == 20 || type == 30) {
        if (taskStatus == 1) {
            //保存表单
            //            $("#div_area").find("center").append("&nbsp;&nbsp;<input style='height:28;color:#ffffff;background-color:#008663;font-family:华文细黑;font-style:normal;font-weight:bold;font-size:10pt;width:80px;' id='btn_form_save'  type='button' value='保存表单'>");

            //添加协办
            $("#div_area").find("center").append("&nbsp;&nbsp;<input style='height:28;color:#ffffff;background-color:#008663;font-family:华文细黑;font-style:normal;font-weight:bold;font-size:10pt;width:80px;' id='btn_form_editXieban'   type='button' value='协办'>");

        }
        //添加传阅
        $("#div_area").find("center").append("&nbsp;&nbsp;<input style='height:28;color:#ffffff;background-color:#008663;font-family:华文细黑;font-style:normal;font-weight:bold;font-size:10pt;width:80px;' id='btn_form_tranTask'  type='button' value='传阅'>");

        //添加打印
        $("#div_area").find("center").append("&nbsp;&nbsp;<input style='height:28;color:#ffffff;background-color:#008663;font-family:华文细黑;font-style:normal;font-weight:bold;font-size:10pt;width:80px;' id='btn_form_printview'  type='button' value='另存打印'>");
    }

    //添加帮助
    //    $("#div_area").find("center").append("&nbsp;&nbsp;<input style='height:28;color:#ffffff;background-color:#008663;font-family:华文细黑;font-style:normal;font-weight:bold;font-size:10pt;width:80px;' id='btn_form_help'  type='button' value='帮助'>");

    $("#btn_form_submit").click(function () {
        SubmitForm()
    });

    $("#btn_form_approve").click(function () {
        Approve()
    });

    $("#btn_form_return").click(function () {
        ReturnForm()
    });

    $("#btn_form_editXieban").click(function () {
        AsstTo()
    });

    $("#btn_form_tranTask").click(function () {
        CopyTo()
    });

    $("#btn_form_printview").click(function () {
        PrintForm()
    });

    //    $("#btn_form_savetemplate").click(function () {
    //        
    //    });

    //    $("#btn_form_save").click(function () {
    //        
    //    });

    //    $("#btn_form_help").click(function () {
    //        
    //    });
}

//注册提交
function SubmitForm() {
    try {
        if (($("input[type$='button']").size() > 0 && $("input[type$='button']").eq("0").attr("disabled") == true)) {
            return false;
        }

        timerSubmit = setInterval(ToggleSubmitButton, 10000);    //启动定时器
        $(this).val("正在提交，请稍候...");
        $(this).attr("disabled", true);
        if (confirm("您确认提交该表单吗？")) {

            $("#btn_form_submit").val("提交");
            $("#btn_form_submit").attr("disabled", false);
            window.parent.frames(0).document.theFCO.DoFormSubmit(1);    //send form
        }

    } catch (e) {

    }
}

//注册同意
function Approve() {
    try {
        if (($("input[type$='button']").size() > 0 && $("input[type$='button']").eq("0").attr("disabled") == true)) {
            return false;
        }

        timerSubmit = setInterval(ToggleApproveButton, 10000);    //启动定时器
        $(this).val("正在操作，请稍候...");
        $(this).attr("disabled", true);
        if (confirm("您确认同意吗？")) {

            $("#btn_form_approve").val("同意");
            $("#btn_form_approve").attr("disabled", false);
            window.parent.frames(0).document.theFCO.DoFormSubmit(1);    //send form
        }

    } catch (e) {

    }
}

//注册不同意
function ReturnForm() {
    try {
        if (($("input[type$='button']").size() > 0 && $("input[type$='button']").eq("0").attr("disabled") == true)) {
            return false;
        }
        var iFCO = window.parent.frames(0).document.theFCO;
        var comments = iFCO.GetVarValue("TaskData.Opinion_Comments");
        if (!comments) {
            alert('不同意时，必须在意见留言栏填写具体原因！');
            return;
        }
        

        timerSubmit = setInterval(ToggleReturnButton, 10000);    //启动定时器
        $(this).val("正在不同意，请稍候...");
        $(this).attr("disabled", true);
        if (confirm("您确认不同意吗？")) {

            $("#btn_form_return").val("不同意");
            $("#btn_form_return").attr("disabled", false);
            window.parent.frames(0).document.theFCO.DoFormSubmit(0);    //send form
        }

    } catch (e) {

    }
}

//注册协办
function AsstTo() {
    showAsst('协办流程');
}

//注册传阅
function CopyTo() {
    showAsst('传阅流程');
}

//注册打印
function PrintForm() {
    printProcess();
}

//定时器,如不能提交则再次启用提交
var timerSubmit;
function ToggleSubmitButton() {
    $("#btn_form_submit").val("提交");
    $("#btn_form_submit").attr("disabled", false);
}

var timerSubmit;
function ToggleApproveButton() {
    $("#btn_form_approve").val("同意");
    $("#btn_form_approve").attr("disabled", false);
}

var timerSubmit;
function ToggleReturnButton() {
    $("#btn_form_return").val("不同意");
    $("#btn_form_return").attr("disabled", false);
}



function showPage(sql, order, displayField, displayFieldCaption, displayFieldWidth, title, dbName) {
    if (!dbName) {
        dbName = "BizDB";
    }
    str = path + "/Modules/Ultimus.UWF.Common/SelectPage.aspx?dbName=" + dbName + "&sql=" + sql + "&order=" + order + "&query=" + displayField + "&caption=" + displayFieldCaption + "&width=" + displayFieldWidth + "&title=" + title;
    str = encodeURI(str);
    val = window.showModalDialog(str, null, "scroll:1;status:0;help:0;dialogWidth=800px;dialogHeight=480px");
    if (!val) {
        val = window.returnValue;
    }
    return val;
}

//选主数据
function selectPage1() {
    rtn = showPage(
        "SELECT NAME,REMARK,ISACTIVE FROM COM_APPSETTINGS",
        "NAME", "NAME,REMARK,ISACTIVE",
        "名称,备注,Active",
        "100,50,50",
        "选择窗口", "BizDB");
    if (!rtn) { return; }
    window.parent.frames(0).document.theFCO.setVarValue("TaskData.Global.Data1", rtn.NAME);
    window.parent.frames(0).document.theFCO.setVarValue("TaskData.Global.Data2", rtn.REMARK);
}
//单选人员
function selectUser2(idVar, nameVar) {
    var val;
    val = window.showModalDialog(path + "/Modules/Ultimus.UWF.OrgChart/SelectUserFromAD.aspx?Type=1", null, "dialogWidth=900px;dialogHeight=500px");
    if (!val) {
        val = window.returnValue;
    }
    if (val) {
        var obj = eval(val);
        var names = "";
        var ids = "";
        if (obj) {
            for (i = 0; i < obj.length; i++) {
                if (i == 0) {
                    names += obj[i].Name;
                    ids += obj[i].LoginName;
                }
                else {
                    names += "," + obj[i].Name;
                    ids += "," + obj[i].LoginName;
                }
            }
        }

        var ifco = window.parent.frames(0).document.theFCO;
        ifco.SetVarValue(idVar, ids);
        ifco.SetVarValue(nameVar, names);
    }
}
//多选人员
function selectUser(idVar, nameVar) {
    var ifco = window.parent.frames(0).document.theFCO;
    var val;
    val = window.showModalDialog(path + "/Modules/Ultimus.UWF.OrgChart/SelectUserFromAD.aspx?Type=2", null, "dialogWidth=900px;dialogHeight=500px");
    var id = new Array();
    if (!val) {
        val = window.returnValue;
    }
    if (val) {
        var obj = eval(val);
        var names = "";
        var ids = "";
        if (obj) {
            for (i = 0; i < obj.length; i++) {
                if (i == 0) {
                    names += obj[i].Name.replace("[USER]", "");
                    ids += obj[i].LoginName;
                }
                else {
                    names += "," + obj[i].Name.replace("[USER]", "");
                    ids += "," + obj[i].LoginName;
                }
                id[i] = "USER:org=" + orgName + ",user=" + obj[i].LoginName.replace("\\", "/");

            }
        }

        ifco.SetVarValuesArray(idVar, id);
        ifco.SetVarValue(nameVar, names);
    }
}

//多选部门
function selectDept(idVar, nameVar) {
    var val;
    val = window.showModalDialog(path + "/Modules/Ultimus.UWF.OrgChart/SelectUserFromAD.aspx?Type=4", null, "dialogWidth=900px;dialogHeight=500px");
    if (!val) {
        val = window.returnValue;
    }
    if (val) {
        var obj = eval(val);
        var names = "";
        var ids = "";
        if (obj) {
            for (i = 0; i < obj.length; i++) {
                if (i == 0) {
                    names += obj[i].Name;
                    ids += obj[i].ID;
                }
                else {
                    names += "," + obj[i].Name;
                    ids += "," + obj[i].ID;
                }
            }
        }

        var ifco = window.parent.frames(0).document.theFCO;
        ifco.SetVarValue(idVar, ids);
        ifco.SetVarValue(nameVar, names);
    }
}

//多选部门
function selectDeptArray(idVar, nameVar) {
    var val;
    val = window.showModalDialog(path + "/Modules/Ultimus.UWF.OrgChart/SelectUserFromAD.aspx?Type=4", null, "dialogWidth=900px;dialogHeight=500px");
    if (!val) {
        val = window.returnValue;
    }
    var id = new Array();
    if (val) {
        var obj = eval(val);
        var names = "";
        var ids = "";
        if (obj) {
            for (i = 0; i < obj.length; i++) {
                if (i == 0) {
                    names += obj[i].Name;
                    ids += obj[i].ID;
                }
                else {
                    names += "," + obj[i].Name;
                    ids += "," + obj[i].ID;
                }
                id[i] = obj[i].ID;
            }
        }

        var ifco = window.parent.frames(0).document.theFCO;
        ifco.SetVarValuesArray(idVar, id);
        ifco.SetVarValue(nameVar, names);
    }
}

//多选岗位
function selectJob(idVar, nameVar) {
    var val;
    val = window.showModalDialog(path + "/Modules/Ultimus.UWF.OrgChart/SelectUserFromAD.aspx?Type=5", null, "dialogWidth=900px;dialogHeight=500px");
    if (!val) {
        val = window.returnValue;
    }
    if (val) {
        var obj = eval(val);
        var names = "";
        var ids = "";
        if (obj) {
            for (i = 0; i < obj.length; i++) {
                if (i == 0) {
                    names += obj[i].Job;
                    ids += obj[i].Job;
                }
                else {
                    names += "," + obj[i].Job;
                    ids += "," + obj[i].Job;
                }
            }
        }

        var ifco = window.parent.frames(0).document.theFCO;
        ifco.SetVarValue(idVar, ids);
        ifco.SetVarValue(nameVar, names);
    }
}

function selectPage2() {
    var ifco = window.parent.frames(0).document.theFCO;
    rtn = showPage(
        "SELECT NAME,REMARK,ISACTIVE FROM COM_APPSETTINGS",
        "NAME", "NAME,REMARK,ISACTIVE",
        "名称,备注,Active",
        "100,50,50",
        "选择窗口");

    row = document.grdItem.GetCurrentSelection();
    if (!row) {
        row = "TaskData.Global.Item1[1]";
    }
    ifco.SetVarValue("TaskData.Global.Item1[1].Name", rtn[0].NAME);
    ifco.SetVarValue("TaskData.Global.Item1[1].Qty", rtn[0].ISACTIVE);

    ifco.SetVarValue("TaskData.Global.Item1[2].Name", rtn[1].NAME);
    ifco.SetVarValue("TaskData.Global.Item1[2].Qty", rtn[1].ISACTIVE);

    document.grdItem.RefreshData();
}

function getValue() {
    var ifco = window.parent.frames(0).document.theFCO;
    var v = ifco.getVarValue("TaskData.Global.Item1[1].Name");
    alert(v);

    //str=document.grdItem.GetCurrentSelection();
    ///alert(str);

}

function addRow() {
    var ifco = window.parent.frames(0).document.theFCO;
    ifco.XSAddNew("TaskData.Global.Item1");
}


//加载审批意见
function loadHistory(history) {
    var iFCO = window.parent.frames(0).document.theFCO;
    var processname = iFCO.GetProcessName();

    var inc = iFCO.GetIncidentNo();
    var url = path + "/Modules/Ultimus.UWF.Workflow/History.aspx?ProcessName=" + escape(processname) + "&Incident=" + inc + "&t=" + new Date().getTime();
    var strHtml = "<IFRAME id='fr_History' width='100%' height='100%' SRC='" + url + "' frameborder='no'  marginwidth='5' marginheight='5' scrolling='yes'  allowtransparency='true'></IFRAME>";
    document.getElementById(history).innerHTML = strHtml;
    //调整高度
    $('#fr_History').load(function () {
        var d = $('#fr_History').contents().find("table tr").size();
        var hh = 0;
        if (d <= 4) {
            hh = eval(d * 28);
            $("#fr_History").css("height", hh + "px");
        } else {
            hh = eval(d * 28);
            $("#fr_History").css("height", hh + "px");
        }
        var t = $("#" + history).css("top").replace("px", "");   //History的TOP
        $("body").append("<div style='position:absolute;top:" + eval(eval(t) + hh + 100) + "px;height:100px;'>&nbsp;</div>");

    });
}

//加载附件
function loadAttachment(controlId, readOnly) {
    var iFCO = window.parent.frames(0).document.theFCO;
    var processname = iFCO.GetProcessName();
    var formId = iFCO.getVarValue("TaskData.Global.FORMID");

    var inc = iFCO.GetIncidentNo();
    var taskStatus = iFCO.GetTaskStatus();
    var url = path + "/Modules/Ultimus.UWF.Workflow/AttachmentControl.aspx?taskStatus=" + taskStatus + "&ReadOnly=" + readOnly + "&TYPE=" + controlId + "&FORMID=" + formId + "&ProcessName=" + escape(processname) + "&Incident=" + inc + "&t=" + new Date().getTime();
    var strHtml = "<IFRAME id='frm" + controlId + "' width='98%'  SRC='" + url + "' frameborder='no'  marginwidth='5' marginheight='5' scrolling='yes'  allowtransparency='true'></IFRAME>";
    document.getElementById(controlId).innerHTML = strHtml;
    //调整高度
    var a = window.parent.frames(1).document.getElementById(controlId).style.height;
    $('#frm' + controlId).css("height", a);
}

//加载正文
function loadWord(controlId, readOnly) {
    var iFCO = window.parent.frames(0).document.theFCO;
    var processname = iFCO.GetProcessName();
    var formId = iFCO.getVarValue("TaskData.Global.FORMID");

    var inc = iFCO.GetIncidentNo();
    var taskStatus = iFCO.GetTaskStatus();
    var url = path + "/Modules/Ultimus.UWF.Workflow/AttachmentControl.aspx?ISWORD=1&taskStatus=" + taskStatus + "&ReadOnly=" + readOnly + "&TYPE=" + controlId + "&FORMID=" + formId + "&ProcessName=" + escape(processname) + "&Incident=" + inc + "&t=" + new Date().getTime();
    var strHtml = "<IFRAME id='frm" + controlId + "' width='98%' height='100%' SRC='" + url + "' frameborder='no'  marginwidth='5' marginheight='5' scrolling='yes'  allowtransparency='true'></IFRAME>";
    document.getElementById(controlId).innerHTML = strHtml;
    //调整高度
    var a = window.parent.frames(1).document.getElementById(controlId).style.height;
    $('#frm' + controlId).css("height", a);
}

//预览正文
function previewWord(controlId) {
    var iFCO = window.parent.frames(0).document.theFCO;
    var formId = iFCO.getVarValue("TaskData.Global.FORMID");
    var url = path + "/Modules/Ultimus.UWF.Workflow/AttachmentPreview.aspx?TYPE=" + controlId + "&FORMID=" + formId + "&t=" + new Date().getTime();
    window.open(url);
}

//获取Url
function getPreviewWord(controlId) {
    var iFCO = window.parent.frames(0).document.theFCO;
    var formId = iFCO.getVarValue("TaskData.Global.FORMID");
    var url = path + "/Modules/Ultimus.UWF.Workflow/AttachmentPreview.aspx?TYPE=" + controlId + "&FORMID=" + formId + "&t=" + new Date().getTime();
    return url;
}

//预览正文Show
function showWord(controlId) {
    var iFCO = window.parent.frames(0).document.theFCO;
    var processname = iFCO.GetProcessName();
    var formId = iFCO.getVarValue("TaskData.Global.FORMID");

    var inc = iFCO.GetIncidentNo();
    var taskStatus = iFCO.GetTaskStatus();
    var url = path + "/Modules/Ultimus.UWF.Workflow/AttachmentPreview.aspx?TYPE=" + controlId + "&FORMID=" + formId + "&ProcessName=" + escape(processname) + "&Incident=" + inc + "&t=" + new Date().getTime();
    var strHtml = "<IFRAME id='frm" + controlId + "' width='98%' height='100%' SRC='" + url + "' frameborder='no'  marginwidth='5' marginheight='5' scrolling='yes'  allowtransparency='true'></IFRAME>";
    document.getElementById(controlId).innerHTML = strHtml;
    //window.open(url);
    //调整高度
    var a = window.parent.frames(1).document.getElementById(controlId).style.height;
    $('#frm' + controlId).css("height", a);
}

//协办或抄送
function showAsst(processName) {
    var iFCO = window.parent.frames(0).document.theFCO;
    var taskId = iFCO.GetTaskID();

    var val;
    val = window.showModalDialog(path + "/Modules/Ultimus.UWF.Workflow/AsstTask.aspx?taskId=" + taskId + "&ProcessName=" + escape(processName), null, "dialogWidth=600px;dialogHeight=300px");
}

//Grid导出
var strExportString = "";
function Export(varName, colNames) {
    var iFCO = window.parent.frames(0).document.theFCO;
    var current = iFCO.XSMoveFirst(varName);
    var pre = iFCO.XSMoveFirst(varName);
    var sz = colNames.split(',');
    var str = "";
    for (j = 0; j < sz.length; j++) {
        str += sz[j] + "|";
    }
    str += "~";
    for (i = 1; i < 100; i++) {
        for (j = 0; j < sz.length; j++) {
            var colName = sz[j];
            colName = colName.split('[')[0];
            str += iFCO.GetVarValue(current + "." + colName) + "|";
        }
        str += "~";
        current = iFCO.XSMoveNext(varName);
        if (pre == current) {
            break;
        }
        pre = current;
    }
    strExportString = str; //格式为c1|c2|~c3|c4~c5|c6
    window.open(path + "/Modules/Ultimus.UWF.Office/GridExport.aspx");
}

function GetExportString() {
    return strExportString;
}

//Grid导入
var w;
var _grid;
function Import(grid,varName, colNames) {
//    var iFCO = window.parent.frames(0).document.theFCO;
//    iFCO.XSDelete("TaskData.Global.Item1");
//    document.getElementById(grid).RefreshData();
//    return;
    //    var a = iFCO.XSMoveLast(varName);
//    alert(a);
    w = window.open(path + "/Modules/Ultimus.UWF.Office/GridImport.aspx?varName=" + varName + "&colNames=" + escape(colNames));
    _grid = grid;
}


function SetImport(varName, names, values, lines) {
    var iFCO = window.parent.frames(0).document.theFCO;
    iFCO.XSDelete(varName);
    //    var last = iFCO.XSMoveLast(varName);
    refreshData();
    iFCO.XSMoveFirst(varName);
        refreshData();
        //    var count = last.split('[')[1].replace("]","");
    for (i = 0; i < lines; i++) {
        iFCO.XSAddNew(varName);
    }
    var sz = names.split(',');
    var sv = values.split(',');
    for (i = 0; i < sz.length; i++) {
        iFCO.SetVarValue(sz[i], sv[i]);
    }

    refreshData();
}


function refreshData() {

    try {
        document.getElementById(_grid).RefreshData();
    }
    catch (e) {
    }
}

function ClearRow(varName) {
    var iFCO = window.parent.frames(0).document.theFCO;
    iFCO.XSClear(varName);
}

function AddNewRow(varName) {
    var iFCO = window.parent.frames(0).document.theFCO;
    iFCO.XSAddNew(varName);
}

//设置文本输入框为只读
function setDisabled(str) {
    $("#" + str).keydown(function () {
        return false;
    });
    $("#" + str).attr("readonly", true);
}

//打印
function printProcess() {
    var iFCO = window.parent.frames(0).document.theFCO;
    var processname = iFCO.GetProcessName();
    var stepName = iFCO.GetStepLabel();
    var incidentNo = iFCO.GetIncidentNo();
    var version = "";
    var taskId = iFCO.GetTaskID();

    window.open("http://bpm.quanyou.com.cn:8082/Web_Print/PrintDoc.aspx?ProcessName=" + processname + "&StepName=" + stepName + "&IncidentNo=" + incidentNo + "&ProcessVersion=" + version + "&TaskID=" + taskId);
}

/**
* 弹出主数据选择窗口
* @width    弹出窗口的宽度
* @height   弹出窗口的高度
* @strSQL   执行的语句编号
* @strName  列名
* @strWidth 列宽
* @strConn  连接数据库的key,oleDB
* @strOrder 排序列名
* @strTitle 弹出页面的标题
* @isMulti  是否多选（值：1或null)
*/
function OpenModal(width, height, strSQL, strName, strWidth, strConn, strOrder, strTitle, isMulti) {
    var strUrl = path + "/Modules/Ultimus.UWF.Common/olePage4.aspx?SQL=" + strSQL + "&Name=" + escape(strName) + "&Width=" + strWidth + "&Conn=" + strConn + "&Order=" + strOrder + "&t=" + new Date().getTime() + "&Title=" + escape(strTitle) + (isMulti != undefined ? "&isMulti=1" : "");
    return window.showModalDialog(strUrl, "", "dialogWidth:" + width + "px; dialogHeight:" + height + "px; dialogLeft: status:no; directories:yes;scrollbars:auto;Resizable=no; ");
}

//转大写
function setMoneyCAP(num) {
    //num = num.replace(",", "").replace(",", "").replace(",", "").replace(",", "").replace(",", "").replace(",", "");
    //num = num.replace("￥", "").replace("￥", "").replace("￥", "").replace("￥", "").replace("￥", "").replace("￥", "");

    if (num == "" || num == null) {
        return;
    }

    if (isNaN(num)) {
        alert("只能输入数字和小数点！");
        return;
    }

    currencyDigits = num;
    //最大值
    var MAXIMUM_NUMBER = 99999999999.99;
    //定义数字大写汉字符号
    var CN_ZERO = "零";
    var CN_ONE = "壹";
    var CN_TWO = "贰";
    var CN_THREE = "叁";
    var CN_FOUR = "肆";
    var CN_FIVE = "伍";
    var CN_SIX = "陆";
    var CN_SEVEN = "柒";
    var CN_EIGHT = "捌";
    var CN_NINE = "玖";
    var CN_TEN = "拾";
    var CN_HUNDRED = "佰";
    var CN_THOUSAND = "仟";
    var CN_TEN_THOUSAND = "万";
    var CN_HUNDRED_MILLION = "亿";
    var CN_SYMBOL = "";
    var CN_DOLLAR = "圆";
    var CN_TEN_CENT = "角";
    var CN_CENT = "分";
    var CN_INTEGER = "整";

    //临时变量
    var integral;           // Represent integral part of digit number.
    var decimal;            // Represent decimal part of digit number.
    var outputCharacters;   // The output result.
    var parts;
    var digits, radices, bigRadices, decimals;
    var zeroCount;
    var i, p, d;
    var quotient, modulus;

    // Validate input string:
    currencyDigits = currencyDigits.toString();
    if (currencyDigits == "") {
        alert("输入为空，不能进行转换！");
        return "";
    }
    if (currencyDigits.match(/[^,.\d]/) != null) {
        alert("数值中存在非法字符！");
        return "";
    }
    if ((currencyDigits).match(/^((\d{1,3}(,\d{3})*(.((\d{3},)*\d{1,3}))?)|(\d+(.\d+)?))$/) == null) {
        alert("非法的数值格式！");
        return "";
    }

    // Normalize the format of input digits:
    currencyDigits = currencyDigits.replace(/,/g, "");      // Remove comma delimiters.
    currencyDigits = currencyDigits.replace(/^0+/, "");     // Trim zeros at the beginning.

    //如果数值超过最大值的范围
    if (Number(currencyDigits) > MAXIMUM_NUMBER) {
        alert("数值过大，无法完成转换！");
        return "";
    }

    // Process the coversion from currency digits to characters:
    // Separate integral and decimal parts before processing coversion:
    parts = currencyDigits.split(".");
    if (parts.length > 1) {
        integral = parts[0];
        decimal = parts[1];
        decimal = decimal.substr(0, 2);     // Cut down redundant decimal digits that are after the second.
    }
    else {
        integral = parts[0];
        decimal = "";
    }

    // Prepare the characters corresponding to the digits:
    digits = new Array(CN_ZERO, CN_ONE, CN_TWO, CN_THREE, CN_FOUR, CN_FIVE, CN_SIX, CN_SEVEN, CN_EIGHT, CN_NINE);
    radices = new Array("", CN_TEN, CN_HUNDRED, CN_THOUSAND);
    bigRadices = new Array("", CN_TEN_THOUSAND, CN_HUNDRED_MILLION);
    decimals = new Array(CN_TEN_CENT, CN_CENT);

    // Start processing:
    outputCharacters = "";

    // Process integral part if it is larger than 0:
    if (Number(integral) > 0) {
        zeroCount = 0;
        for (i = 0; i < integral.length; i++) {
            p = integral.length - i - 1;
            d = integral.substr(i, 1);
            quotient = p / 4;
            modulus = p % 4;
            if (d == "0") {
                zeroCount++;
            }
            else {
                if (zeroCount > 0) {
                    outputCharacters += digits[0];
                }
                zeroCount = 0;
                outputCharacters += digits[Number(d)] + radices[modulus];
            }

            if (modulus == 0 && zeroCount < 4) {
                outputCharacters += bigRadices[quotient];
            }
        }

        outputCharacters += CN_DOLLAR;
    }

    // Process decimal part if there is:
    if (decimal != "") {
        for (i = 0; i < decimal.length; i++) {
            d = decimal.substr(i, 1);
            if (d != "0") {
                outputCharacters += digits[Number(d)] + decimals[i];
            }
        }
    }

    // Confirm and return the final output string:
    if (outputCharacters == "") {
        outputCharacters = CN_ZERO + CN_DOLLAR;
    }

    if (decimal == "" || decimal == "00" || decimal == "0") {
        outputCharacters += CN_INTEGER;
    }

    outputCharacters = CN_SYMBOL + outputCharacters;
    return outputCharacters;
}

//固定可选办理人
function selectFixedUser(idVar, nameVar, configPerson, isShowOther) {
    //创建“弹出层”
    var selectDiv = document.createElement("div");
    //创建“人员数据层”
    var selectDiv_Data = document.createElement("div");
    selectDiv_Data.setAttribute("id", "GlobalSelectUserCheckBoxDiv");
    selectDiv_Data.style.height = "230px";
    selectDiv_Data.style.width = "450px";
    selectDiv_Data.style.marginLeft = "35px";
    //创建“操作层”
    var selectDiv_Action = document.createElement("div");
    selectDiv_Action.style.textAlign = "center";
    selectDiv_Action.style.verticalAlign = "bottom";
    selectDiv_Action.style.height = "40px";
    //填充“操作层”
    var clostAction = "<input type=\"button\" onclick=\"selectFixedUser_Confirm('" + idVar + "','" + nameVar + "')\" style=\"" + btnClass + "\" value=\"确定\" />&nbsp;&nbsp;&nbsp;<input type=\"button\" style=\"" + btnClass + "\" onclick=\"selectFixedUser_CloseDiv('GlobalSelectUserDiv','GlobalMaskDiv')\" value=\"关闭\" />";
    if (isShowOther == true || isShowOther == "true") {
        clostAction += "&nbsp;&nbsp;&nbsp;<input value=\"选择其他办理人\" style=\"" + btnClass + "\" type=\"button\" onclick=\"selectFixedUser_selectUser()\" />";
    }
    selectDiv_Action.innerHTML = clostAction;
    //创建“全选层”
    var selectDiv_SelectAll = document.createElement("div");
    selectDiv_SelectAll.style.width = "480px";
    var selectAllHtml = "<div style=\"text-align:right;margin-right:10px;margin-top:10px;\"><input style=\"\" onclick=\"selectFixedUser_SelectAll(this)\" type=\"checkbox\">全选</div>";
    selectAllHtml += "<hr style=\"width:480px\"/>";
    selectDiv_SelectAll.innerHTML = selectAllHtml;
    //var iframe11 = '<iframe width="100%" height="100%" frameborder="0" style="border:none 0;" allowtransparency="true" id="GlobalSelectUserFrame" name="GlobalSelectUserFrame" src="' + path + '/Modules/Ultimus.UWF.OrgChart/FixedHandle.aspx"></iframe>';

    //填充“人员数据层”
    var DataHtml = "";
    var configPerson_arr = configPerson.split(";");
    for (var i = 0; i < configPerson_arr.length; i++) {
        DataHtml += "<div style=\"width:200px;height:25px;float:left;display:inline\"><input name=\"FixedUser\" type=\"checkbox\" value=\"" + configPerson_arr[i] + "\" />" + configPerson_arr[i] + "</div>";
    }
    selectDiv_Data.innerHTML = DataHtml;

    //“操作层”、“人员数据层”填充到“弹出层”
    selectDiv.appendChild(selectDiv_SelectAll);
    selectDiv.appendChild(selectDiv_Data);
    selectDiv.appendChild(selectDiv_Action);
    //“弹出层”样式
    selectDiv.setAttribute("id", "GlobalSelectUserDiv");
    selectDiv.style.display = "none";
    selectDiv.style.position = "absolute";
    selectDiv.style.zIndex = "1005";
    selectDiv.style.backgroundColor = "white";
    selectDiv.style.height = "350px";
    selectDiv.style.width = "480px";
    selectDiv.style.border = "2px solid #dddddd";
    selectDiv.style.borderStyle = "outset";
    selectDiv.style.fontSize = "14px";
    document.body.appendChild(selectDiv);
    //创建“遮罩层”
    var maskDiv = document.createElement("div");
    maskDiv.setAttribute("id", "GlobalMaskDiv");
    maskDiv.style.display = "none";
    maskDiv.style.position = "absolute";
    maskDiv.style.top = "0%";
    maskDiv.style.left = "0%";
    maskDiv.style.width = "100%";
    maskDiv.style.height = "100%";
    maskDiv.style.backgroundColor = "#CCCCCC";
    maskDiv.style.zIndex = "1001";
    maskDiv.style.mozOpacity = "0.8";
    maskDiv.style.opacity = ".80";
    maskDiv.style.filter = "alpha(opacity=80)";
    document.body.appendChild(maskDiv);

    //显示遮罩
    document.getElementById("GlobalMaskDiv").style.display = 'block';
    var bgdiv = document.getElementById("GlobalMaskDiv");
    bgdiv.style.width = document.body.scrollWidth;
    $("#GlobalMaskDiv").height($(document).height());
    //显示“弹出层”
    var _scrollHeight = $(document.body).scrollTop(), //获取当前窗口距离页面顶部高度

	_windowHeight = document.body.clientHeight, //  $(window).height(), //获取当前窗口高度
	_windowWidth = document.body.clientWidth, //$(window).width(), //获取当前窗口宽度
	_popupHeight = $("#GlobalSelectUserDiv").height(), //获取弹出层高度
	_popupWeight = $("#GlobalSelectUserDiv").width(); //获取弹出层宽度
    _posiTop = (_windowHeight - _popupHeight) / 2 + _scrollHeight;
    _posiLeft = (_windowWidth - _popupWeight) / 2;
    document.body.style.overflow = 'hidden'; //背景滚动条隐藏
    $("#GlobalSelectUserDiv").css({ "left": _posiLeft + "px", "top": _posiTop + "px", "display": "block" }); //设置position
    //("#GlobalSelectUserDiv").css({ "left": "250px", "top": "100px", "display": "block" }); //设置position
}
//关闭弹出层方法
selectFixedUser_CloseDiv = function (show_div, bg_div) {
    //隐藏弹出层
    document.getElementById(show_div).style.display = 'none';
    document.getElementById(bg_div).style.display = 'none';
    document.body.style.overflow = ''; //背景滚动条显示
    $("#GlobalSelectUserDiv").remove(); //删除弹出层元素
};
//全选
selectFixedUser_SelectAll = function (obj) {
    if ($(obj).attr('checked')) { $("[name='FixedUser']").attr('checked', 'true'); }
    else { $("[name='FixedUser']").removeAttr("checked"); }
}
//点击确定
selectFixedUser_Confirm = function (idVar, nameVar) {
    var ifco = window.parent.frames(0).document.theFCO;
    var id = new Array();
    var names = "";
    var i = 0;
    $('input[name="FixedUser"]:checked').each(function () {
        var sValue = $(this).val();
        var name = sValue.substring(sValue.indexOf('<') + 1, sValue.indexOf('>'));
        var ids = sValue.substring(0, sValue.indexOf('<'));
        names += "," + name;
        id[i] = "USER:org=" + orgName + ",user=" + orgName + "/" + ids;
        i++;
    });
    if (names.indexOf(",") == 0) { names = names.substring(1); }
    ifco.SetVarValuesArray(idVar, id);
    ifco.SetVarValue(nameVar, names);
    selectFixedUser_CloseDiv('GlobalSelectUserDiv', 'GlobalMaskDiv');
}

//多选人员
function selectFixedUser_selectUser() {
    var ifco = window.parent.frames(0).document.theFCO;
    var val;
    val = window.showModalDialog(path + "/Modules/Ultimus.UWF.OrgChart/SelectUserFromAD.aspx?Type=2", null, "dialogWidth=900px;dialogHeight=500px");
    var id = new Array();
    if (!val) {
        val = window.returnValue;
    }
    if (val) {
        var obj = eval(val);
        var names = "";
        var ids = "";
        if (obj) {
            for (i = 0; i < obj.length; i++) {
                var sValue = obj[i].LoginName.replace("\\", "/").replace(orgName + "/", "") + "<" + obj[i].Name.replace("[USER]", "") + ">";
                var tempHtml = "<div style=\"width:200px;height:25px;float:left;display:inline\"><input name=\"FixedUser\" type=\"checkbox\" checked=\"checked\" value=\"" + sValue + "\" />" + sValue + "</div>";
                $("#GlobalSelectUserCheckBoxDiv").append(tempHtml);
            }
        }
    }
}

var btnClass = "display: inline-block;padding: 4px 12px;margin-bottom: 0;font-size: 14px;line-height: 20px;color: #333333;text-align: center;text-shadow: 0 1px 1px rgba(255, 255, 255, 0.75);vertical-align: middle;cursor: pointer;background-color: #f5f5f5;background-image: -moz-linear-gradient(top, #ffffff, #e6e6e6);background-image: -webkit-gradient(linear, 0 0, 0 100%, from(#ffffff), to(#e6e6e6));background-image: -webkit-linear-gradient(top, #ffffff, #e6e6e6);background-image: -o-linear-gradient(top, #ffffff, #e6e6e6);background-image: linear-gradient(to bottom, #ffffff, #e6e6e6);background-repeat: repeat-x;border: 1px solid #bbbbbb;border-color: #e6e6e6 #e6e6e6 #bfbfbf;border-color: rgba(0, 0, 0, 0.1) rgba(0, 0, 0, 0.1) rgba(0, 0, 0, 0.25);border-bottom-color: #a2a2a2;-webkit-border-radius: 4px;-moz-border-radius: 4px;border-radius: 4px;filter: progid:DXImageTransform.Microsoft.gradient(startColorstr='#ffffffff', endColorstr='#ffe6e6e6', GradientType=0);filter: progid:DXImageTransform.Microsoft.gradient(enabled=false);-webkit-box-shadow: inset 0 1px 0 rgba(255, 255, 255, 0.2), 0 1px 2px rgba(0, 0, 0, 0.05);-moz-box-shadow: inset 0 1px 0 rgba(255, 255, 255, 0.2), 0 1px 2px rgba(0, 0, 0, 0.05);box-shadow: inset 0 1px 0 rgba(255, 255, 255, 0.2), 0 1px 2px rgba(0, 0, 0, 0.05);";