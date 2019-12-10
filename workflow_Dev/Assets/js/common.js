var path = "../..";

var r = document.getElementsByTagName("script");
for (var i = 0; i <= r.length; i++) {
    var obj = r[i];
    if (obj) {
        if (obj.src.toLowerCase().indexOf("common.js") > 0) {
            path = obj.src.toLowerCase().replace("/assets/js/common.js", "");
        }
    }
}
//load css
document.write("<link href='" + path + "/assets/css/bootstrap.css' type='text/css' rel='stylesheet' />");
document.write("<link href='" + path + "/assets/css/bootstrap-responsive.css' type='text/css' rel='stylesheet' />");
document.write("<link href='" + path + "/assets/css/bootstrap-custom.css' type='text/css' rel='stylesheet' />");
document.write("<link href='" + path + "/assets/css/validationEngine.jquery.css' type='text/css' rel='stylesheet' />");

//load js
document.write("<script language='javascript' src='" + path + "/assets/js/jquery.js'></script>");
document.write("<script language='javascript' src='" + path + "/assets/js/jquery.cookie.js'></script>");
document.write("<script language='javascript' src='" + path + "/assets/js/jquery.validationEngine.js'></script>");
document.write("<script language='javascript' src='" + path + "/assets/js/languages/jquery.validationEngine-zh_CN.js'></script>");
document.write("<script language='javascript' src='" + path + "/assets/js/bootstrap.js'></script>");
document.write("<script language='javascript' src='" + path + "/assets/js/selector.js'></script>");
document.write("<script language='javascript' src='" + path + "/assets/js/My97DatePicker/WdatePicker.js'></script>");
document.write("<script language='javascript' src='" + path + "/assets/js/autoSearch.js'></script>");
document.write("<script language='javascript' src='" + path + "/assets/js/BPMCommon.js'></script>");


///打开tab页  
//Title:打开的Tab页的描述
//Url:打开的地址
//TabName:打开的Tab页ID,
//closable:true可关闭 false不可关闭
function OpenForm(Title, Url, TabName, closable) {
    var t = typeof window.tabs == "undefined" ? window.parent.tabs : window.tabs;
    t.add(Title, Url, TabName, closable);
}
function preview(myDiv) {
//var newstr = document.all.item(myDiv).innerHTML;
//var oldstr = document.body.innerHTML;
//document.body.innerHTML = newstr;
//window.print();
//document.body.innerHTML = oldstr;
    //    return false;
    window.print();
}