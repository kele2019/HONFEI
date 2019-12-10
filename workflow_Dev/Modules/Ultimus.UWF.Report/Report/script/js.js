function ReturnUserData(dat, single) {
    var rt = new Array();
    if (dat != null) {
        dat = dat.All;
        var a = "", b = "", c = "";
        for (var i = 0; i < dat.length; i++) {
            var user = dat[i];
            a += user.ID + ","; //ID
            if (single != undefined && single != null) { b += user.Name + ","; } //显示名
            else { b += user.Name.split("(")[0] + ","; } //显示名
            c += ("Ultimus/" + user.ShortName + ","); //登录名
        }
        //alert(a + "--" + b + "--" + c);
        rt[0] = b.substr(0, b.length - 1); //显示名
        rt[1] = c.substr(0, c.length - 1); //登录名
        rt[2] = (a.indexOf(",") != -1 ? a.substr(0, a.length - 1) : a);  //ID
    }
    //alert(rt[0] + "\r\n" + rt[1] + "\r\n" + rt[2]);
    if (rt.length == 0) rt = new Array("", "", "");
    return rt;
}
function getUserOrJob(single, mix) {
    var dat;
    if (single) {
        dat = selopen("", "0-1-1-0");  //人员单选
    } else if (mix) {
        dat = selopen("", "0-0-4-0");  //1表示人员，2表示公司，3表示部门，4表示岗位
    } else {
        dat = selopen(); //人员选择
    }
    return ReturnUserData(dat, single);
}
//弹出选择框
function showDialog(iPage,iPWidth, iPHeight) {
    try {
        var arrReturn = null;
        //var strUrl = "/UltimusClient_Juewei/popup/" + iPage + "?SQL=" + encodeURI(iSql) + "&Name=" + escape(iName) + "&Width=" + iWidth + "&Conn=" + iConn + "&Order=" + iSearch + "&Title=" + escape(iTitle);
        var strRtn = window.showModalDialog(iPage, "", "dialogWidth:" + iPWidth + "px; dialogHeight:" + iPHeight + "px; dialogLeft: status:no; directories:yes;scrollbars:auto;Resizable=no; ");
        if (strRtn != null) {
            var arrReturn = strRtn.split('~');
        }

        return arrReturn;
    }
    catch (exception) {

    }
}
function showDialog(ipage) {
    window.open(ipage);
}
function getPage(opt) {
    if (!opt.id) return false;
    var oDiv = document.getElementById(opt.id);
    var nowNum = opt.nowNum || 1;
    var totalNum = opt.totalNum || 5;
    var callBack = opt.callBack || function () { };
    //首页
    if (nowNum > 3) {
        var oA = document.createElement('a');
        oA.innerHTML = '首页';
        oA.href = '#1';
        oDiv.appendChild(oA);
    }
    //上一页
    if (nowNum > 1) {
        var oA = document.createElement('a');
        oA.innerHTML = '上一页';
        oA.href = '#' + (nowNum - 1);
        oDiv.appendChild(oA);
    }
    //如果总计条数 小于等于5，要进行特殊处理
    if (totalNum <= 5) {
        //console.log(totalNum);
        for (var i = 0; i < totalNum; i++) {
            var oA = document.createElement('a');
            if (nowNum == (i + 1)) {
                oA.innerHTML = (i + 1);
            } else {
                oA.innerHTML = '[' + (i + 1) + ']';
            }
            oA.href = '#' + (i + 1);
            oDiv.appendChild(oA);
        }
    } else {

        //中间部分
        for (var i = 0; i < 5; i++) {
            var oA = document.createElement('a');
            if (nowNum == 1 || nowNum == 2) {
                if ((i + 1) == nowNum) {
                    oA.innerHTML = (i + 1);
                } else {
                    oA.innerHTML = '[' + (i + 1) + ']';
                }
                oA.href = '#' + (i + 1);
            } else {
                if (nowNum == totalNum || nowNum == (totalNum - 1)) {
                    if (nowNum == (totalNum - 4 + i)) {
                        oA.innerHTML = nowNum;
                    } else {
                        oA.innerHTML = '[' + (totalNum - 4 + i) + ']';
                    }
                    oA.href = '#' + (totalNum - 4 + i);
                } else {
                    if (i == Math.floor(5 / 2)) {
                        oA.innerHTML = (nowNum - 2 + i);
                    } else {
                        oA.innerHTML = '[' + (nowNum - 2 + i) + ']';
                    }
                    oA.href = '#' + (nowNum - 2 + i);
                }
            }
            oDiv.appendChild(oA);
        }

    }
    //下一页
    if (nowNum < totalNum) {
        var oA = document.createElement('a');
        oA.innerHTML = '下一页';
        //console.log(nowNum + 1);
        oA.href = '#' + (nowNum + 1);
        oDiv.appendChild(oA);
    }
    //尾页
    if (nowNum <= (totalNum - 3) && nowNum >= 5) {
        var oA = document.createElement('a');
        oA.innerHTML = '尾页';
        oA.href = '#' + totalNum;
        oDiv.appendChild(oA);
    }
    //调用回调函数
    callBack(nowNum, totalNum);

    var aA = oDiv.getElementsByTagName('a');

    for (var i = 0; i < aA.length; i++) {
        aA[i].onclick = function () {
            var index = parseInt(this.getAttribute('href').substring(1));
            //console.log(index);
            oDiv.innerHTML = '';

            getPage({ id: opt.id, nowNum: index, totalNum: totalNum, callBack: callBack });
            return false;
        }

    }
}
