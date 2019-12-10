function OpenWindows(url) {
    window.open(url, "", "scrollbars=yes, resizable=yes, toolbar=no, menubar=no, location=no, directories=no,width=800,height=650,top=20,left=20");
}
function OpenWin(url) {
    window.open(url, "", "toolbar=no, menubar=no, location=no, directories=no,width=800,height=600,top=40,left=40");
}
function OpenWinNull(url) {
    window.open(url, null, "toolbar=no, menubar=no, location=no, directories=no,width=800,height=600,top=40,left=40");
}

//功能介绍：弹出公告信息窗口
//参数说明：url
function OpenPoP(url) {
    window.open(url, null, "scrollbars=yes, resizable=yes, toolbar=no, menubar=no, location=no, directories=no,width=600,height=400,top=20,left=20");
}
//功能介绍：弹出用户信息窗口
//参数说明：url
var winUser = null;
function OpenWndUser(url) {
    window.open(url, null, "scrollbars=yes, resizable=yes, toolbar=no, menubar=no, location=no, directories=no,width=750,height=500,top=20,left=20");
}

//功能介绍：弹出用户名片信息窗口
function OpenWndUserInfo(url) {
    window.open(url, null, "scrollbars=yes, resizable=no, toolbar=no, menubar=no, location=no, directories=no,width=500,height=320,top=20,left=20");
}

//参数说明：url
function OpenWndUserInfo(url) {
    window.open(url, null, "scrollbars=yes, resizable=no, toolbar=no, menubar=no, location=no, directories=no,width=500,height=320,top=20,left=20");
}

//功能介绍：弹出任务跟踪窗口
//参数说明：url
function OpenWndTrace(url) {
    window.open(url, null, "scrollbars=yes, resizable=yes, toolbar=no, menubar=no, location=no, directories=no,width=500,height=500,top=80,left=120");
}

//功能介绍：弹出部门窗口
//参数说明：url
function OpenWndDept(url) {
    window.open(url, null, "scrollbars=yes, resizable=no, toolbar=no, menubar=no, location=no, directories=no,width=300,height=420,top=20,left=20");
}

//功能介绍：弹出窗口显示全屏
function OpenWndTitle(url) {
    window.open(url, null, "scrollbars=yes, resizable=no, toolbar=no, menubar=no, location=no, directories=no,width=800,height=600,top=20,left=20")
}

//参数说明：url
//WinName:打开窗口名称
function OpenWnd(URL, WinName) {
    var width = screen.availWidth - 10;
    var height = screen.availHeight - 30;
    window.open(URL, WinName, "scrollbars=yes, resizable=yes, toolbar=no, menubar=no, location=no, directories=no,width=" + width + ",height=" + height + ",top=0,left=0");
}

//功能介绍：弹出上传附件窗口
//参数说明：url
function OpenWinFileUpload(url) {
    var winleft;
    var wintop;
    var srnheight;
    var srnwidth;
    wintop = event.screenY - event.offsetY + 20;
    winleft = window.event.screenX - window.event.offsetX;
    srnheight = screen.height;
    srnwidth = screen.width;
    if (winleft > srnwidth - 460) {
        winleft = winleft - 460 + window.event.offsetX;
    }
    if (wintop > srnheight - 200) {
        wintop = wintop - 290;
    }

    window.open(url, "FileUpload", "toolbar=no, menubar=no, location=no, directories=no,width=460,height=230,top=" + wintop + ",left=" + winleft + "");
}

function OpenWinFileUpload1(url) {
    var winleft;
    var wintop;
    var srnheight;
    var srnwidth;
    wintop = event.screenY - event.offsetY + 20;
    winleft = window.event.screenX - window.event.offsetX;
    srnheight = screen.height;
    srnwidth = screen.width;
    if (winleft > srnwidth - 460) {
        winleft = winleft - 460 + window.event.offsetX;
    }
    if (wintop > srnheight - 200) {
        wintop = wintop - 290;
    }

    window.open(url, null, "toolbar=no, menubar=no, location=no, directories=no,width=460,height=230,top=" + wintop + ",left=" + winleft + "");
}

//功能介绍：检查控件ID是否存在
//参数说明：ControlID控件ID
//返回值：true(存在) or false(不存在) 
function CheckControlExist(ControlID) {
    if (document.getElementById(ControlID) == null) {
        return false;
    }
    else {
        return true;
    }
}
//功能介绍：取得Url参数

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

//功能介绍：返回当前表格行索引
//参数说明：obj即this
//返回值：当前表格行索引
function getTableCurRowIndex(obj) {
    return obj.event.srcElement.parentNode.parentNode.rowIndex;
}

function OpenExcelFileUpload(itemname) {
    var url = "common/sel_excelinvoice.aspx";
    OpenWinFileUpload(url);
}

//0-9 .- only for the number of
function IsDigit(num) {
    var i = 0;
    var chnum;

    if (num.length == 0)
        return false;
    for (i = 0; i < num.length; i++) {
        chnum = num.substring(i, i + 1);
        if (chnum < '0' || chnum > '9') {
            if (chnum != '.') {
                return false;
            }
        }
    }

    return true;
}
function numberOnly(event) {
    var iKey = event.keyCode;
    var strKey = String.fromCharCode(iKey);
    if (IsDigit(strKey)) {
        return event.keyCode;
    }
    return "";
}

//0-9 .- only for the number of
function IsDigit1(num) {
    var i = 0;
    var chnum;

    if (num.length == 0)
        return false;

    for (i = 0; i < num.length; i++) {
        chnum = num.substring(i, i + 1);
        if (chnum < '0' || chnum > '9') {
            if (chnum != '.' && chnum != '-') {
                return false;
            }
        }
    }
    return true;
}

//0-9 only for the number of
function IsNumber(num) {
    var i = 0;
    var chnum;

    if (num.length == 0)
        return false;

    for (i = 0; i < num.length; i++) {
        chnum = num.substring(i, i + 1);
        if (chnum < '0' || chnum > '9') {
            return false;
        }
    }
    return true;
}

//Determine whether the value of air control
function IsEmpty(Item) {
    if (Item.value == "") {
        if (Item.type != "hidden")
            Item.focus();
        return true;
    }
    else
        return false;
}

//For more lines of text, the certification of its maximum length, if more than the length of the Jiediao!
function TextareaMaxLength(strInput, strLen) {
    var str;
    var num;

    str = strInput.value;
    num = strInput.value.length;
    //var arr=str.match(/[^\\\\\\\\\\\\\\\\x00-\\\\\\\\\\\\\\\\x80]/ig)
    //if(arr!=null)num+=arr.length
    if (num > strLen) {
        strInput.value = str.substring(0, strLen);
        alert("最多可以输入" + strLen + "个字符.");
    }
}

//Float returned to the type of figures
function checkFloat(tmp) {
    var retValue;

    if (tmp == "" || tmp == "-" || tmp == ".") {
        retValue = 0;
    }
    else {
        if (IsDigit1(tmp) == true) {
            retValue = parseFloat(tmp);
        }
        else {
            retValue = 0;
        }

    }
    return retValue;
}

//当前系统日期
function getCurrentDate() {
    var CurDate;
    var CurYear, CurMonth, CurDay, retValue;

    CurDate = new Date();
    CurYear = CurDate.getFullYear();
    CurMonth = CurDate.getMonth() + 1;
    if (CurMonth < 10) {
        CurMonth = "0" + CurMonth
    }
    CurDay = CurDate.getDate();
    if (CurDay < 10) {
        CurDay = "0" + CurDay
    }

    retValue = CurYear + "-" + CurMonth + "-" + CurDay;

    return retValue;
}

//日期差
function DateDiff(StartDate, EndDate) {
    var arrStartDate = StartDate.split("-");
    var NewStartDate = new Date(arrStartDate[1] + '-' + arrStartDate[2] + '-' + arrStartDate[0]);
    var arrEndDate = EndDate.split("-");
    var NewEndDate = new Date(arrEndDate[1] + '-' + arrEndDate[2] + '-' + arrEndDate[0]);
    var iDays = (NewEndDate - NewStartDate) / 1000 / 60 / 60 / 24;
    return iDays;
}
function DateDiffMonth(StartDate, EndDate) {
    var S = new Date(StartDate);
    var E = new Date(EndDate);
    var Smonth = S.getMonth() + 1;
    var Emonth = E.getMonth() + 1;
    var SYear = S.getFullYear();
    var EYear = E.getFullYear();
    var diffYear = 0;
    if (SYear != EYear) {
        diffYear = (12 - Smonth) + Emonth;
    }
    else {
        diffYear = Emonth - Smonth;
    }
    return diffYear + 1;
   
}

//Number.prototype.toFixed = function (len)//此方法重写toFixed方法，因为原来方法有bug
//{
//    var add = 0;
//    var s, temp;
//    var s1 = this + "";
//    var start = s1.indexOf(".");
//    if (s1.substr(start + len + 1, 1) >= 5) add = 1;
//    var temp = Math.pow(10, len);
//    s = Math.floor(this * temp) + add;
//    return s / temp;
//}

function checkPoint(nValue) {
    return nValue.toFixed(4);
}

//Reservations two decimal
function checkTwoPoint(retValue) {
    var x_Point;
    var s_Value;
    var p1, p2;
    var add;
    var iLen;

    retValue = retValue.toString();
    iLen = retValue.length;
    x_Point = retValue.indexOf(".");

    if (x_Point != -1) {
        if (x_Point != 0) {
            s_Value = retValue.substr(0, x_Point + 2);
            if (iLen != x_Point + 2) {
                add = 0;
                p1 = retValue.charAt(x_Point + 2);
                p2 = retValue.charAt(x_Point + 3);
                if (p2 != null) {
                    if (parseInt(p2) >= 5) {
                        add = 1;
                    }
                    else if (parseInt(p2) < 5 && parseInt(p2) >= 0) {
                        add = 0;
                    }

                    add = parseInt(add) + parseInt(p1);
                }
                if (add == 10) {
                    p0 = retValue.charAt(x_Point + 1);
                    p0 = parseInt(p0) + 1;
                    if (p0 == 10) {
                        ss_Value = retValue.substr(0, x_Point);
                        ss_Value = parseInt(ss_Value) + 1;
                        s_Value = ss_Value + ".00";
                    }
                    else {
                        ss_Value = retValue.substr(0, x_Point + 1);
                        s_Value = ss_Value + p0 + "0";
                    }
                }
                else {
                    s_Value = s_Value + add.toString();
                }
            }
            else {
                s_Value = s_Value + "0";
            }
        }
        else {
            s_Value = "0.00";
        }
    }
    else {
        s_Value = retValue + ".00";
    }

    return s_Value;
}

//Change to four decimal
//Reservations Four decimal
function checkFourPoint(retValue) {
    var add = 0;
    var s, temp;

    //var s1 = this + "";
    var s1 = this + retValue;
    var start = s1.indexOf(".");
    if (s1.substr(start + 4 + 1, 1) >= 5) add = 1;
    var temp = Math.pow(10, 4);
    s = Math.floor(retValue * temp) + add;
    return s / temp;
}


//Change to two decimal
//Reservations two decimal
function checkTwoPoint2(retValue) {
    var add = 0;
    var s, temp;

    //var s1 = this + "";
    var s1 = this + retValue;
    var start = s1.indexOf(".");
    if (s1.substr(start + 2 + 1, 1) >= 3) add = 1;
    var temp = Math.pow(10, 2);
    s = Math.floor(retValue * temp) + add;
    return s / temp;
}

//去左空格; 
function ltrim(s) {
    return s.replace(/^\s*/, "");
}
//去右空格; 
function rtrim(s) {
    return s.replace(/\s*$/, "");
}
//去左右空格; 
function trim(s) {
    return rtrim(ltrim(s));
}

//RMB lowercase to uppercase function
function ChangeRMB(str) {
    var strNumArray = new Array("零", "壹", "贰", "叁", "肆", "伍", "陆", "柒", "捌", "玖");
    var strDigitArray = new Array("拾", "佰", "仟", "万", "亿", "圆", "角", "分", "整");
    var newLeftTemp, newRightTemp, leftString, rightString, strTemp, newString;
    var strLeft, strRight, strDigit;
    var leftLen, rightLen, i, dot;

    newString = new String();
    newLeftTemp = new String();
    newRightTemp = new String();
    leftString = new String();
    rightString = new String();

    var regS = new RegExp(",", "gi");       //替换千分位符号

    str = str.replace(regS, "");

    strTemp = str;
    strDigit = new String();

    dot = strTemp.indexOf(".");

    if (dot != -1) {
        strDigit = strTemp.substr(dot + 1);
        strTemp = strTemp.substr(0, dot);
    }
    else {
        strDigit = ".00";
    }
    leftLen = strTemp.length;
    rightLen = strDigit.length;

    //Integral part of the data conversion
    for (i = 1; i <= leftLen; i++) {
        strLeft = strTemp.substr(i - 1, 1);
        for (j = 0; j <= 9; j++) {
            if (strLeft == j) {
                newLeftTemp = newLeftTemp + strNumArray[j];
            }
        }
    }
    for (k = leftLen; k > 0; k--) {
        strLeft = newLeftTemp.substr(leftLen - k, 1);
        if (k == 9) {
            if (strLeft != "零") {
                leftString = leftString + strLeft + strDigitArray[4];
            }
        }
        if (k == 5) {
            if (strLeft != "零") {
                leftString = leftString + strLeft + strDigitArray[3];
            }
            else {
                leftString = leftString + strDigitArray[3];
            }
        }
        if (k == 4 || k == 8 || k == 13) {
            if (strLeft != "零") {
                leftString = leftString + strLeft + strDigitArray[2];
            }
            else if (k != 4) {
                leftString = leftString;
            }
        }
        if (k == 3 || k == 7 || k == 12) {
            if (strLeft != "零") {
                leftString = leftString + strLeft + strDigitArray[1];
            }
            else if (k != 3) {
                leftString = leftString;
            }
        }
        if (k == 2 || k == 6 || k == 11) {
            if (strLeft != "零") {
                leftString = leftString + strLeft + strDigitArray[0];
            }
            else if (k != 2) {
                leftString = leftString;
            }
        }
        if (k == 1) {
            if (strLeft != "零") {
                leftString = leftString + strLeft + strDigitArray[5];
            }
            else if (leftLen != 1) {
                leftString = leftString + strDigitArray[5];
            }
        }
    }

    //Decimal data conversion
    for (l = 1; l <= rightLen; l++) {
        strRight = strDigit.substr(l - 1, 1);
        for (m = 0; m <= 9; m++) {
            if (strRight == m) {
                newRightTemp = newRightTemp + strNumArray[m];
            }
        }
    }
    for (n = 2; n > 0; n--) {
        strRight = newRightTemp.substr(2 - n, 1);
        if (n == 2) {
            if (strRight != "零") {
                rightString = rightString + strRight + strDigitArray[6];
            }
        }
        if (n == 1) {
            if (strRight != "零" && strRight != "") {
                rightString = rightString + strRight + strDigitArray[7];
            }
        }
    }
    if (strTemp != "") {
        newString = leftString + rightString + strDigitArray[8];
        return newString;
    }
    else {
        return "";
    }
}

//RMB lowercase to uppercase function (with 0)
function ChangeRMBToCH(Num) {
    for (i = Num.length - 1; i >= 0; i--) {
        Num = Num.replace(",", "")//替换tomoney()中的“,”
        Num = Num.replace(" ", "")//替换tomoney()中的空格
    }
    Num = Num.replace("￥", "")//替换掉可能出现的￥字符
    if (isNaN(Num)) { //验证输入的字符是否为数字
        alert("请检查小写金额是否正确");
        return;
    }
    //---字符处理完毕，开始转换，转换采用前后两部分分别转换---//
    part = String(Num).split(".");
    newchar = "";
    //小数点前进行转化
    for (i = part[0].length - 1; i >= 0; i--) {
        if (part[0].length > 10) { alert("位数过大，无法计算"); return ""; } //若数量超过拾亿单位，提示
        tmpnewchar = ""
        perchar = part[0].charAt(i);
        switch (perchar) {
            case "0": tmpnewchar = "零" + tmpnewchar; break;
            case "1": tmpnewchar = "壹" + tmpnewchar; break;
            case "2": tmpnewchar = "贰" + tmpnewchar; break;
            case "3": tmpnewchar = "叁" + tmpnewchar; break;
            case "4": tmpnewchar = "肆" + tmpnewchar; break;
            case "5": tmpnewchar = "伍" + tmpnewchar; break;
            case "6": tmpnewchar = "陆" + tmpnewchar; break;
            case "7": tmpnewchar = "柒" + tmpnewchar; break;
            case "8": tmpnewchar = "捌" + tmpnewchar; break;
            case "9": tmpnewchar = "玖" + tmpnewchar; break;
        }
        switch (part[0].length - i - 1) {
            case 0: tmpnewchar = tmpnewchar + "元"; break;
            case 1: if (perchar != 0) tmpnewchar = tmpnewchar + "拾"; break;
            case 2: if (perchar != 0) tmpnewchar = tmpnewchar + "佰"; break;
            case 3: if (perchar != 0) tmpnewchar = tmpnewchar + "仟"; break;
            case 4: tmpnewchar = tmpnewchar + "万"; break;
            case 5: if (perchar != 0) tmpnewchar = tmpnewchar + "拾"; break;
            case 6: if (perchar != 0) tmpnewchar = tmpnewchar + "佰"; break;
            case 7: if (perchar != 0) tmpnewchar = tmpnewchar + "仟"; break;
            case 8: tmpnewchar = tmpnewchar + "亿"; break;
            case 9: tmpnewchar = tmpnewchar + "拾"; break;
        }
        newchar = tmpnewchar + newchar;
    }
    //小数点之后进行转化
    if (Num.indexOf(".") != -1) {
        if (part[1].length > 2) {
            alert("小数点之后只能保留两位,系统将自动截段");
            part[1] = part[1].substr(0, 2)
        }
        for (i = 0; i < part[1].length; i++) {
            tmpnewchar = ""
            perchar = part[1].charAt(i)
            switch (perchar) {
                case "0": tmpnewchar = "零" + tmpnewchar; break;
                case "1": tmpnewchar = "壹" + tmpnewchar; break;
                case "2": tmpnewchar = "贰" + tmpnewchar; break;
                case "3": tmpnewchar = "叁" + tmpnewchar; break;
                case "4": tmpnewchar = "肆" + tmpnewchar; break;
                case "5": tmpnewchar = "伍" + tmpnewchar; break;
                case "6": tmpnewchar = "陆" + tmpnewchar; break;
                case "7": tmpnewchar = "柒" + tmpnewchar; break;
                case "8": tmpnewchar = "捌" + tmpnewchar; break;
                case "9": tmpnewchar = "玖" + tmpnewchar; break;
            }
            if (i == 0) tmpnewchar = tmpnewchar + "角";
            if (i == 1) tmpnewchar = tmpnewchar + "分";
            newchar = newchar + tmpnewchar;
        }
    }

    if (newchar.search("分") != -1) {
        newchar = newchar.replace("零角", "零");
    }

    //替换所有无用汉字
    while (newchar.search("零零") != -1)
        newchar = newchar.replace("零零", "零");
    newchar = newchar.replace("零亿", "亿");
    newchar = newchar.replace("亿万", "亿");
    newchar = newchar.replace("零万", "万");
    newchar = newchar.replace("零元", "元");
    newchar = newchar.replace("零角", "");
    newchar = newchar.replace("零分", "");

    if (newchar.charAt(newchar.length - 1) == "元" || newchar.charAt(newchar.length - 1) == "角")
        newchar = newchar + "整";

    return newchar;
}

//将数字转换成三位逗号分隔的样式 
function formatNum(objName) {
    num = objName.value;
    if (!/^(\+|-)?(\d+)(\.\d+)?$/.test(num)) { alert("wrong!"); return num; }
    var a = RegExp.$1, b = RegExp.$2, c = RegExp.$3;
    var re = new RegExp().compile("(\\d)(\\d{3})(,|$)");
    while (re.test(b)) b = b.replace(re, "$1,$2$3");
    return a + "" + b + "" + c;
}

function CheckRadio(sradio, sMsg) {
    var selectedIndex = -1;
    for (var xx = 0; xx < document.all.length; xx++) {
        if (document.all[xx].tagName.toLowerCase() == "input") {
            var inputs = document.all[xx];
            if (inputs.type == "radio") {
                if (inputs.name == sradio) {
                    if (inputs.checked) { selectedIndex = 0; }
                }
            }
        }
    }
    if (selectedIndex < 0) {
        alert(sMsg);
        return false;
    }
    else {
        return true;
    }
}

function doReadOnlyStyle(e) {
    if (e.attributes["NotCssReadOnly"] == null) {
        if (e.id != 'linkGenPoastBack' && e.id != 'form1' && e.id != '__EVENTTARGET' && e.id != '__EVENTARGUMENT') {
            if (e.tagName.toLowerCase() == "input" || e.tagName.toLowerCase() == "textarea") {
                e.disabled = true;
            }
            else {
                e.disabled = true;
            }
            e.style.borderWidth = "0";
        }
    }

    if (e.attributes["ReadOnlyHide"] != null) {
        e.style.display = "none";
    }
}
//隐藏所有非空的红色标签
//前提用 <span NNLabel style="color: #ff0066">*</span>表示非空
function HideNNLabel(p_bIsHide) {
    var arrNNLabel;
    arrNNLabel = document.getElementsByTagName("span");
    for (i = 0; i < arrNNLabel.length; i++) {
        if (arrNNLabel[i].attributes["NNLabel"] != null) {
            if (p_bIsHide == true) {
                arrNNLabel[i].style["display"] = "none";
            }
            else {
                arrNNLabel[i].style["display"] = "";
            }
        }
    }
}

//获取浏览器
function userBrowser() {
    var browserName = navigator.userAgent.toLowerCase();
    if (/msie/i.test(browserName) && !/opera/.test(browserName)) {
        return false;
    } else if (/firefox/i.test(browserName)) {
        return true;
    } else if (/chrome/i.test(browserName) && /webkit/i.test(browserName) && /mozilla/i.test(browserName)) {
        return true;
    } else if (/opera/i.test(browserName)) {
        return true;
    } else if (/webkit/i.test(browserName) && !(/chrome/i.test(browserName) && /webkit/i.test(browserName) && /mozilla/i.test(browserName))) {
        return true;
    } else {
        return true;
    }
}

function DecimalOnly(obj) {
    if (obj.value.indexOf(".") != -1 && (event.keyCode == 110 || event.keyCode == 190)) {
        event.returnValue = false
    }

    if (!((event.keyCode >= 48 && event.keyCode <= 57) || (event.keyCode >= 96 && event.keyCode <= 105) || event.keyCode == 8 || event.keyCode == 9 || event.keyCode == 37 || event.keyCode == 39 || event.keyCode == 46 || event.keyCode == 110 || event.keyCode == 190))
        event.returnValue = false
    if (event.shiftKey && ((event.keyCode >= 48 && event.keyCode <= 57) || event.keyCode == 190)) event.returnValue = false
}
//获取Url链接参数
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



//将字符串格式化为货币格式
OutputMoney = function (number) {
    number = number.replace(/\,/g, "");
    number = number.replace(",", "");
    if (number == "") return "";
    if (number < 0) {
        return '-' + outputDollars(Math.floor(Math.abs(number) - 0) + '') + outputCents(Math.abs(number) - 0);
    } else {
        return outputDollars(Math.floor(number - 0) + '') + outputCents(number - 0);
    }
}

function outputDollars(number) {
    if (number.length <= 3) {
        return (number == '' ? '0' : number);
    } else {
        var mod = number.length % 3;
        var output = (mod == 0 ? '' : (number.substring(0, mod)));
        for (i = 0; i < Math.floor(number.length / 3); i++) {
            if ((mod == 0) && (i == 0)) {
                output += number.substring(mod + 3 * i, mod + 3 * i + 3);
            } else {
                output += ',' + number.substring(mod + 3 * i, mod + 3 * i + 3);
            }
        }
        return (output);
    }
}
function formatNumber(num, cent, isThousand) {
    num = num.toString().replace(/\$|\,/g, '');

    // 检查传入数值为数值类型   
    if (isNaN(num))
        num = "0";

    // 获取符号(正/负数)   
    sign = (num == (num = Math.abs(num)));

    num = Math.floor(num * Math.pow(10, cent) + 0.50000000001);  // 把指定的小数位先转换成整数.多余的小数位四舍五入   
    cents = num % Math.pow(10, cent);              // 求出小数位数值   
    num = Math.floor(num / Math.pow(10, cent)).toString();   // 求出整数位数值   
    cents = cents.toString();               // 把小数位转换成字符串,以便求小数位长度   

    // 补足小数位到指定的位数   
    while (cents.length < cent)
        cents = "0" + cents;

    if (isThousand) {
        // 对整数部分进行千分位格式化.   
        for (var i = 0; i < Math.floor((num.length - (1 + i)) / 3); i++)
            num = num.substring(0, num.length - (4 * i + 3)) + ',' + num.substring(num.length - (4 * i + 3));
    }

    if (cent > 0)
        return (((sign) ? '' : '-') + num + '.' + cents);
    else
        return (((sign) ? '' : '-') + num);
}

function outputCents(amount) {
    amount = Math.round(((amount) - Math.floor(amount)) * 100);
    return (amount < 10 ? '.0' + amount : '.' + amount);
}
jQuery(document).ready(function () {
    $("input[money=money]").each(function () {
        $(this).keyup(function () {
            this.value = this.value.replace(/[^0-9.+(\d{2})?$]/g, '');

        });
    });
});