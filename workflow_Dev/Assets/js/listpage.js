var path = "../..";

var r = document.getElementsByTagName("script");
for (var i = 0; i <= r.length; i++) {
    var obj = r[i];
    if (obj) {
        if (obj.src.toLowerCase().indexOf("listpage.js") > 0) {
            path = obj.src.toLowerCase().replace("/assets/js/listpage.js", "");
        }
    }
}
//load css
document.write("<link href='" + path + "/assets/css/listpage.css' type='text/css' rel='stylesheet' />");
