//20150518 Eastern 人员、组织选择控件
//参数：
//***类型 type 必传 0:选择任意、1:单选人、2:多选人、3:单选部门、4:多选部门
//***显示值 txtName 非必传
//***隐藏值 txtId 非必传
//***确定后执行事件 okButton 非必传
//Demo： onclick="SelectUser({type:'2',txtName:'userName',txtId:'userId',okButton:aaaa})"

//弹出层Id：GlobalSelectUserFrame
//弹出层iframeId：GlobalSelectUserDiv
//遮罩Id:GlobalMaskDiv

//主函数
var SelectUser = function (arg) {
    //点击确认后方法
    if (!!arg.okButton) {
        SelectUser.AfterAction = arg.okButton
    }
    //基本赋值，显示值
    if (!!arg.txtName) {
        SelectUser.NameText = arg.txtName;
    }
    //基本赋值，显示值
    if (!!arg.txtId) {
        SelectUser.IdText = arg.txtId;
    }
    //创建“弹出层”
    var selectDiv = document.createElement("div");
    //创建“人员、组织数据层”
    var selectDiv_Data = document.createElement("div");
    selectDiv_Data.style.height = "410px";
    //创建“操作层”
    var selectDiv_Action = document.createElement("div");
    selectDiv_Action.style.textAlign = "center";
    selectDiv_Action.style.verticalAlign = "bottom";
    selectDiv_Action.style.height = "40px";
    //填充“操作层”
    var clostAction = "<input type=\"button\" onclick=\"SelectUser.Confirm()\" class=\"btn\" value=\"确定\" />&nbsp;&nbsp;&nbsp;<input type=\"button\" class=\"btn\" onclick=\"SelectUser.CloseDiv('GlobalSelectUserDiv','GlobalMaskDiv')\" value=\"关闭\" />";
    selectDiv_Action.innerHTML = clostAction;
    //填充“人员、组织数据层”
    
    var iframe11 = '<iframe width="100%" height="100%" frameborder="0" style="border:none 0;" allowtransparency="true" id="GlobalSelectUserFrame" name="GlobalSelectUserFrame" src="' + path + '/Modules/Ultimus.UWF.OrgChart/SelectUserNew.aspx?Type=' + arg.type + '"></iframe>';
    selectDiv_Data.innerHTML = iframe11;
    //“操作层”、“人员、组织数据层”填充到“弹出层”
    selectDiv.appendChild(selectDiv_Data);
    selectDiv.appendChild(selectDiv_Action);
    //“弹出层”样式
    selectDiv.setAttribute("id", "GlobalSelectUserDiv");
    selectDiv.style.display = "none";
    selectDiv.style.position = "absolute";
    selectDiv.style.zIndex = "1005";
    selectDiv.style.backgroundColor = "white";
    selectDiv.style.height = "450px";
    selectDiv.style.width = "800px";
    selectDiv.style.border = "1px solid #dddddd";
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
    var _scrollHeight = $(document).scrollTop(), //获取当前窗口距离页面顶部高度
	_windowHeight = $(window).height(), //获取当前窗口高度
	_windowWidth = $(window).width(), //获取当前窗口宽度
	_popupHeight = $("#GlobalSelectUserDiv").height(), //获取弹出层高度
	_popupWeight = $("#GlobalSelectUserDiv").width(); //获取弹出层宽度
    _posiTop = (_windowHeight - _popupHeight) / 2 + _scrollHeight;
    _posiLeft = (_windowWidth - _popupWeight) / 2;
    document.body.style.overflow = 'hidden'; //背景滚动条隐藏
    $("#GlobalSelectUserDiv").css({ "left": _posiLeft + "px", "top": _posiTop + "px", "display": "block" }); //设置position
}
SelectUser.AfterAction = null; //点击确认后方法
SelectUser.NameText = null; //显示值
SelectUser.IdText = null; //隐藏值

//点击确定后事件
SelectUser.Confirm = function () {
    var returnJson = ""; //返回对象的字符串

    //遍历组织树
    var tnJson = "";
    window.frames["GlobalSelectUserFrame"].$("#UserTreeView :checkbox:checked").each(function () {
        var textTemp = $(this).next().html();
        var idTemp = $(this).next("a").attr("href").replace(/javascript:__doPostBack\([^,]+,'(.*\\([^']+)|(s.+))'\)/, '$2$3');
        var idTempArr = idTemp.split('||');
        tnJson += "{'Name':'" + textTemp + "[" + idTempArr[1].toString() + "]',";
        tnJson += "'ID':'" + idTempArr[0].toString() + "',";
        tnJson += "'Type':'" + idTempArr[1].toString() + "'},";
    });

    //单选
    if (window.frames["GlobalSelectUserFrame"].document.getElementById("hidSelectType").value == "1") {
        returnJson += "[";

        var isok = false;
        window.frames["GlobalSelectUserFrame"].$("#tbody").find("tr").each(function () {
            if ($(this).find("td:eq(0)").children().attr("checked")) {
                returnJson += "{'Name':'" + $.trim($(this).find("td:eq(1)").text()) + "[USER]',";
                returnJson += "'Type':'USER',";
                returnJson += "'ID':'" + $.trim($(this).find("td:eq(5)").children().val()) + "'},";
            }
        });

        returnJson = returnJson + tnJson;
        if (returnJson.lastIndexOf(",") > 0) {
            returnJson = returnJson.substring(0, returnJson.lastIndexOf(","));
        }

        returnJson += "]";

    }
    else {
        returnJson += "[";
        window.frames["GlobalSelectUserFrame"].$("#tab tr").each(function () {
            returnJson += "{'Name':'" + $.trim($(this).find("td:eq(1)").text()) + "[USER]',";
            returnJson += "'Type':'USER',";
            returnJson += "'ID':'" + $.trim($(this).find("td:eq(5)").children().val()) + "'},";
        });
        returnJson = returnJson + tnJson;
        if (returnJson.lastIndexOf(",") > 0) {
            returnJson = returnJson.substring(0, returnJson.lastIndexOf(","));
        }
        returnJson += "]";
    }
    //转成对象，给控件赋值
    var obj = eval(returnJson);
    var names = ""; var ids = ""; var emails = "";
    if (obj) {
        for (i = 0; i < obj.length; i++) {
            if (i == 0) {
                names += obj[i].Name.substr(0, obj[i].Name.indexOf("[")); ;
                ids += obj[i].ID + "|" + obj[i].Type
            }
            else {
                names += "," + obj[i].Name.substr(0, obj[i].Name.indexOf("["));
                ids += "," + obj[i].ID + "|" + obj[i].Type;
            }
        }
    }
    if (!!SelectUser.NameText) { $("#" + SelectUser.NameText).val(names); }
    if (!!SelectUser.IdText) { $("#" + SelectUser.IdText).val(ids); }
    if (!!SelectUser.AfterAction) { SelectUser.AfterAction(obj); }

    SelectUser.CloseDiv('GlobalSelectUserDiv', 'GlobalMaskDiv'); //关闭弹出层
}

//关闭弹出层方法
SelectUser.CloseDiv = function (show_div, bg_div) {
    SelectUser.AfterAction = null;
    SelectUser.NameText = null;
    SelectUser.IdText = null;
    //隐藏弹出层
    document.getElementById(show_div).style.display = 'none';
    document.getElementById(bg_div).style.display = 'none';
    document.body.style.overflow = ''; //背景滚动条显示
    $("#GlobalSelectUserDiv").remove(); //删除弹出层元素
};