function BackSetFieldName() {
    $("#step7").fadeOut("slow", function () {
        $("#step6").fadeIn("slow");
    });
}

function OpeningPage(obj) {
    if ($(obj).attr("checked")) {
        $(".OpeningPageClass").fadeIn("slow", function () {
            $("#ArticleThatNumber").addClass("validate[required,custom[number]]");
        });
    } else {
        $(".OpeningPageClass").fadeOut("slow", function () {
            $("#ArticleThatNumber").removeClass("validate[required,custom[number]]");
        });
    }
}

function ShowViewRow(index) {
    if (index == 2 || index == "2") {
        $("#viewreportbutton").val("人员选择");
        $("#SelectViewRow").fadeIn("slow", function () {
            $("[name='viewreport'][value!='" + index + "']:checkbox").removeAttr("checked");
        });
        $("#selectuserrow").fadeIn("slow");
        $("#AuthorityHidden").val("");
        $("#viewitems").val("");
    } else if (index == 1 || index == "1") {
        $("#SelectViewRow").fadeIn("slow", function () {
            $("#selectuserrow").hide();
            $("[name='viewreport'][value!='" + index + "']:checkbox").removeAttr("checked");
        });
        $("#AuthorityHidden").val("");
        $("#viewitems").val("");
    } else {
        $("#SelectViewRow").fadeOut("slow", function () {
            $("#selectuserrow").fadeOut("slow");
            $("#AuthorityHidden").val("");
            $("#viewitems").val("");
            $("[name='viewreport'][value!='" + index + "']:checkbox").removeAttr("checked");
        });
    }
}

function LoadTableName() {
    var SaveTablesJson = $("#SelectTablesAndFieldHidden").val();
    var json = eval('(' + SaveTablesJson + ')');
    var sortstylejson;
    if ($("#SortStyleHidden").val() != "") {
        sortstylejson = eval('(' + $("#SortStyleHidden").val() + ')');
    }
    var Authority;
    if ($("#ViewUserTableNameHidden").val() != "") {
        Authority = $("#ViewUserTableNameHidden").val();
    }
    var SortfirstTableName;
    var AuthorityfirstTableName;
    var SortTableNameItems = "";
    var AuthorityTableNameItems = "";
    for (var i = 0; i < json.length; i++) {
        if (i == 0) {
            if (sortstylejson != null) {
                SortfirstTableName = sortstylejson.tableName;
            }else {
                SortfirstTableName = json[i].tableName;
            }
            if (Authority != null) {
                AuthorityfirstTableName = Authority;
            } else {
                AuthorityfirstTableName = json[i].tableName;
            }
        }
        SortTableNameItems += "<option value=\"" + json[i].tableName + "\" " + (sortstylejson != null && sortstylejson.tableName == json[i].tableName ? "selected=\"selected\"" : "") + ">" + json[i].tableName + "</option>";
        AuthorityTableNameItems += "<option value=\"" + json[i].tableName + "\" " + (Authority != null && Authority[0] == json[i].tableName ? "selected=\"selected\"" : "") + ">" + json[i].tableName + "</option>";
    }
    $("#SortTableName").append(SortTableNameItems);
    $("#ViewUserTableName").append(AuthorityTableNameItems);

    LoadFirstTableFieldItems(SortfirstTableName, AuthorityfirstTableName);
}

function LoadFirstTableFieldItems(SortTableName, AuthorityTableName) {
    var SaveTablesJson = $("#SelectTablesAndFieldHidden").val();
    var json = eval('(' + SaveTablesJson + ')');
    var sortstylejson;
    if ($("#SortStyleHidden").val() != "") {
        sortstylejson = eval('(' + $("#SortStyleHidden").val() + ')');
    }
    var Authority;
    if ($("#ViewUserFieldNameHidden").val() != "") {
        Authority = $("#ViewUserFieldNameHidden").val();
    }
    var SotyFieldItems = "";
    var AuthorityItems = "";
    for (var i = 0; i < json.length; i++) {
        for (var j = 0; j < json[i].data.length; j++) {
            if (json[i].tableName == SortTableName) {
                SotyFieldItems += "<option value=\"" + json[i].data[j].ColumnName + "\" " + (sortstylejson != null && sortstylejson.fieldName == json[i].data[j].ColumnName ? "selected=\"selected\"" : "") + ">" + json[i].data[j].ColumnName + "</option>";
            }
            if (json[i].tableName == AuthorityTableName) {
                AuthorityItems += "<option value=\"" + json[i].data[j].ColumnName + "\" " + (Authority != null && Authority == json[i].data[j].ColumnName ? "selected=\"selected\"" : "") + ">" + json[i].data[j].ColumnName + "</option>";
            }
        }
    }
    $("#SortFieldName").append(SotyFieldItems);
    $("#ViewUserFieldName").append(AuthorityItems);
    if (sortstylejson != null) {
        $("#SortType option[value='" + sortstylejson.sortStyle + "']").attr("selected", "selected");
    }
    
}

function GetOtherTableField(obj, selectID) {
    var SaveTablesJson = $("#SelectTablesAndFieldHidden").val();
    var json = eval('(' + SaveTablesJson + ')');
    var fieldItems = "";
    for (var i = 0; i < json.length; i++) {
        for (var j = 0; j < json[i].data.length; j++) {
            if (json[i].tableName == $(obj).val()) {
                fieldItems += "<option value=\"" + json[i].data[j].ColumnName + "\">" + json[i].data[j].ColumnName + "</option>";
            }
        }
    }
    $("#" + selectID).html("").append(fieldItems);
}

function NextSaveReport() {
    var sortStyle = "{'tableName':'" + $("#SortTableName").val() + "',"
                  + "'fieldName':'" + $("#SortFieldName").val() + "',"
                  + "'sortStyle':'" + $("#SortType").val() + "'}";
    $("#SortStyleHidden").val("").val(sortStyle);
    if (jQuery("#step7tab").validationEngine("validate")) {
        SaveReport();
    }
}
jQuery(document).ready(function () {
    var jsid = 'jsSelector';
    if (!document.getElementById(jsid)) {
        var js = document.createElement('script');
        js.setAttribute('id', jsid);
        js.setAttribute('src', "/bpm/selector_sapi.axd");
        js.setAttribute('type', 'text/javascript');
        (document.body ? document.body : document).appendChild(js);
    }
});
function getPersons(ids, single, mix) {
    var namePrefix = "Ultimus/";
    var rt = new Array();
    var dat;
    dat = selopen();
    if (dat) {
        dat = dat.All;
        var a = "", b = "", c = "";
        for (var i = 0; i < dat.length; i++) {
            var user = dat[i];
            a += user.ID + ","; //ID
            if (single != undefined && single != null) { b += user.Name + ","; } //显示名
            else { b += user.Name.split("(")[0] + ","; } //显示名
            c += ("'Ultimus/" + user.ShortName + "',"); //登录名
        }
        $("#AuthorityHidden").val("").val(c.substr(0, c.length - 1));
        $("#viewitems").val("").val(b.substr(0, c.length - 1));
    }
}