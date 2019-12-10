var empNameGlobalString = ","; //格式为：   ,张三,李四,
var empIdGlobalString = "|";

var SelectUser = function (uName, uId) {
    //    alert($("#" + uName).val());
    //    alert($("#" + uId).val());
    //alert(empNameGlobalString)
    var selectDiv = document.createElement("div");

    //创建“操作层”
    var selectDiv_Action = document.createElement("div");
    selectDiv_Action.style.textAlign = "center";
    selectDiv_Action.style.verticalAlign = "bottom";
    selectDiv_Action.style.height = "40px";
    //填充“操作层”
    var clostAction = "<input type=\"button\" onclick=\"SelectUser.Confirm('" + uName + "', '" + uId + "')\" class=\"btn\" value=\"确定\" />&nbsp;&nbsp;&nbsp;<input type=\"button\" class=\"btn\" onclick=\"SelectUser.CloseDiv('GlobalSelectUserDiv','GlobalMaskDiv')\" value=\"关闭\" />";
    selectDiv_Action.innerHTML = clostAction;

    //创建“搜索层”
    var searchDiv = document.createElement("div");
    searchDiv.innerHTML = "<input id=\"searchText\" style=\"width:65%;margin-left:5px;margin-top:7px;\" type=\"text\"> <input type=\"button\" onclick=\"SelectUser.Search()\" class=\"btn\" value=\"搜索\" />";

    //创建“数据层”
    var dataDiv = document.createElement("div");
    dataDiv.innerHTML = "<table id=\"dataResultTb\" class=\"table table-bordered\"><tr><td>姓名</td><td>账号</td><td>部门</td></tr></table>";

    //创建“已选层”
    var selectedDiv = document.createElement("div");
    selectedDiv.setAttribute("id", "GlobalSelectedDiv");
    selectedDiv.innerHTML = ""; // "张三<a onclick=\"alert(1)\" style=\"color:red\" >x</>";

    selectDiv.appendChild(searchDiv);
    selectDiv.appendChild(dataDiv);
    selectDiv.appendChild(selectedDiv);
    selectDiv.appendChild(selectDiv_Action);

    //“弹出层”样式
    selectDiv.setAttribute("id", "GlobalSelectUserDiv");
    selectDiv.style.display = "none";
    selectDiv.style.position = "absolute";
    selectDiv.style.zIndex = "1005";
    selectDiv.style.backgroundColor = "white";
    //    selectDiv.style.height = "450px";
    //    selectDiv.style.width = "800px";
    selectDiv.style.border = "1px solid #dddddd";
    document.body.appendChild(selectDiv);

    //初始化赋值
    var uNameValue = $("#" + uName).val();
    var uIdValue = $("#" + uId).val();
    if (uNameValue.length > 1) {
        var uNameValue_arr = uNameValue.split(",");
        var uIdValue_arr = uIdValue.split("|");
        document.getElementById("GlobalSelectedDiv").innerHTML = "";
        for (var i = 0; i < uNameValue_arr.length; i++) {
            if (uNameValue_arr[i] != "") {
                var xxx = "<a onclick=\"SelectUser.Delete('" + uNameValue_arr[i] + "','" + uIdValue_arr[i] + "')\" style=\"color:red\" >x</>";
                empNameGlobalString += uNameValue_arr[i] + ",";
                empIdGlobalString += uIdValue_arr[i] + "|";
                document.getElementById("GlobalSelectedDiv").innerHTML = document.getElementById("GlobalSelectedDiv").innerHTML + uNameValue_arr[i] + xxx + " ";
            }
        }
    }

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

//点击确定后事件
SelectUser.Confirm = function (uName, uId) {
    if (empNameGlobalString.indexOf(",") == 0) {
        empNameGlobalString = empNameGlobalString.substring(1);
        empIdGlobalString = empIdGlobalString.substring(1);
    }
    //var reg = /,$/gi;
    if (empNameGlobalString.length > 0) {
        empNameGlobalString = empNameGlobalString.substring(0, empNameGlobalString.length - 1)//empNameGlobalString.replace(reg, "");
        empIdGlobalString = empIdGlobalString.substring(0, empIdGlobalString.length - 1)
    }
    $("#" + uName).val(empNameGlobalString);
    $("#" + uId).val(empIdGlobalString);
    //document.getElementById(uName).innerHTML = empNameGlobalString;
    //document.getElementById(uId).innerHTML = empIdGlobalString;

    empNameGlobalString = ",";
    empIdGlobalString = "|";
    SelectUser.CloseDiv('GlobalSelectUserDiv', 'GlobalMaskDiv');
}

//选中事件
SelectUser.Select = function (row) {
    if (!!$("#dataResultRow_Name" + row)[0]) {
        var xxx = "<a onclick=\"SelectUser.Delete('" + $("#dataResultRow_Name" + row)[0].innerText + "','" + $("#dataResultRow_LoginName" + row)[0].innerText + "')\" style=\"color:red\" >x</>";
        var thisValue = $("#dataResultRow_Name" + row)[0].innerText.replace(/\s*$/g, ''); ;
        var thisValue2 = $("#dataResultRow_LoginName" + row)[0].innerText.replace(/\s*$/g, ''); ;
        if (document.getElementById("GlobalSelectedDiv").innerHTML.indexOf(thisValue + "<") == -1) {
            document.getElementById("GlobalSelectedDiv").innerHTML = document.getElementById("GlobalSelectedDiv").innerHTML + thisValue + xxx + " ";
        }
        if (empNameGlobalString.indexOf(thisValue) == -1) {
            empNameGlobalString += thisValue + ",";
        }
        if (empIdGlobalString.indexOf(thisValue2) == -1) {
            empIdGlobalString += thisValue2 + "|";
        }
    }
}

//删除已选事件
SelectUser.Delete = function (name, loginame) {
    //删除全局变量中对应的值
    if (empNameGlobalString.indexOf("," + name + ",") != -1) {
        empNameGlobalString = empNameGlobalString.replace("," + name + ",", ",");
    }
    if (empIdGlobalString.indexOf("|" + loginame + "|") != -1) {
        empIdGlobalString = empIdGlobalString.replace("|" + loginame + "|", "|");
    }
    var empNameGlobalString_arr = empNameGlobalString.split(",");
    var empIdGlobalString_arr = empIdGlobalString.split("|");
    document.getElementById("GlobalSelectedDiv").innerHTML = "";
    for (var i = 0; i < empNameGlobalString_arr.length; i++) {
        if (empNameGlobalString_arr[i] != "") {
            var xxx = "<a onclick=\"SelectUser.Delete('" + empNameGlobalString_arr[i] + "','" + empIdGlobalString_arr[i] + "')\" style=\"color:red\" >x</>";
            document.getElementById("GlobalSelectedDiv").innerHTML = document.getElementById("GlobalSelectedDiv").innerHTML + empNameGlobalString_arr[i] + xxx + " ";
        }
    }
}

//搜索
SelectUser.Search = function () {
    if ($("#searchText").val().replace(" ", "") != "") {
        $.ajax({
            type: "POST",
            url: "Ajax.ashx?tag=GetUserInfo&searchText=" + encodeURI($("#searchText").val()),
            async: true,
            success: function (date) {
                var json = eval('(' + date + ')').UserInfo;
                $("#dataResultTb tr:not(:first)").each(function () //遍历每一行
                {
                    $(this).remove();
                });
                var rowindex = $("#dataResultTb").find("tr").size() - 1;
                for (var i = 0; i < json.length; i++) {
                    var row = "<tr onclick=\"SelectUser.Select(" + i.toString() + ")\"><td id=\"dataResultRow_Name" + i.toString() + "\">" + json[i].Name + "</td><td style=\"display:none;\" id=\"dataResultRow_LoginName" + i.toString() + "\">" + json[i].LoginName + "</td><td id=\"dataResultRow_LoginNameShow" + i.toString() + "\">" + json[i].LoginNameShow + "</td><td>" + json[i].DepartName + "</td></tr>";
                    $(row).insertAfter($("#dataResultTb tr:last"));
                }
            }
        });
    }
}