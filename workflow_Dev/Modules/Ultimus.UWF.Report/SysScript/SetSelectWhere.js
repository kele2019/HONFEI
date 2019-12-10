function AddSelectWhere() {
    var tableNames = ""; var tableItems = ""; var firstFieldName = "";
    var SaveTablesJson = $("#SelectTablesAndFieldHidden").val();
    var json = eval('(' + SaveTablesJson + ')');
    for (var i = 0; i < json.length; i++) {
        if (i == 0) {
            tableItems = GetTableField(json[i].tableName);
            firstFieldName = GetTableFirstField(json[i].tableName);
        }
        tableNames += "<option value=\"" + json[i].tableName + "\">" + json[i].tableName + "</option>";
    }
    var tr = "<tr>"
           + "  <td width=\"4%\" style=\"text-align:center;\">"
           + "      " + ($("#SelectWhereList tr").length + 1) + ""
           + "  </td>"
           + "  <td width=\"19%\" style=\"text-align:center;\">"
           + "      <select style=\"width:100%;\" onchange=\"GetSelectTableField(this)\">"
           + "          " + tableNames + ""
           + "      </select>"
           + "  </td>"
           + "  <td width=\"24%\" style=\"text-align:center;\">"
           + "      <select style=\"width:100%;\" onchange=\"GetSelectFieldName(this)\">"
           + "          " + tableItems + ""
           + "      </select>"
           + "  </td>"
           + "  <td width=\"24%\" style=\"text-align:center;\">"
           + "      <input class=\"inputboder2\" style=\"width:98%;\" value=\"" + firstFieldName + "\"/>"
           + "  </td>"
           + "  <td width=\"14%\" style=\"text-align:center;\">"
           + "      <input type=\"checkbox\"/>"
           + "  </td>"
           + "  <td width=\"17%\" style=\"text-align:center;\">"
           + "      <input type=\"checkbox\"/>"
           + "  </td>"
           + "  <td width=\"17%\" style=\"text-align:center;\">"
           + "      <input type=\"checkbox\"/>"
           + "  </td>"
           + "  <td width=\"10%\" style=\"text-align:center;\">"
           + "      <a href=\"javascript:void(0);\" onclick=\"removeRow(this)\" style=\"color:Blue;\">删除</a>"
           + "  </td>"
           + "</tr>";
    $("#SelectWhereList").append(tr);
}

function GetTableField(tableName) {
    var tableItems = "";
    var SaveTablesJson = $("#SelectTablesAndFieldHidden").val();
    var json = eval('(' + SaveTablesJson + ')');
    for (var i = 0; i < json.length; i++) {
        for (var j = 0; j < json[i].data.length; j++) {
            if (json[i].tableName == tableName) {
                tableItems += "<option value=\"" + json[i].data[j].ColumnName + "\">" + json[i].data[j].ColumnName + "</option>";
            }
        }
    }
    return tableItems;
}
function GetTableFirstField(tableName) {
    var FirstFieldName = "";
    var SaveTablesJson = $("#SelectTablesAndFieldHidden").val();
    var json = eval('(' + SaveTablesJson + ')');
    for (var i = 0; i < json.length; i++) {
        for (var j = 0; j < 1; j++) {
            if (json[i].tableName == tableName) {
                FirstFieldName = json[i].data[j].ColumnName;
            }
        }
    }
    return FirstFieldName;
}
function removeRow(obj) {
    $(obj).parent().parent().remove();
}

function GetSelectTableField(obj) {
    var tr = $(obj).parent().parent();
    var tableName = $(tr).find("td:eq(1)").find("select option:selected").val();
    var FieldItems = $(tr).find("td:eq(2)").find("select").html("");
    var SaveTablesJson = $("#SelectTablesAndFieldHidden").val();
    var json = eval('(' + SaveTablesJson + ')');
    for (var i = 0; i < json.length; i++) {
        for (var j = 0; j < json[i].data.length; j++) {
            if (json[i].tableName == tableName) {
                FieldItems.append("<option value=\"" + json[i].data[j].ColumnName + "\">" + json[i].data[j].ColumnName + "</option>");
            }
        }
    }
    GetSelectFieldName(obj);
}

function GetSelectFieldName(obj) {
    var tr = $(obj).parent().parent();
    $(tr).find("td:eq(3)").children().val($(tr).find("td:eq(2)").children().val());
}

function BackTableRelation() {
    $("#step5").fadeOut("slow", function () {
        $("#step4").fadeIn("slow");
    });
}
function NextSetFieldName() {
    var SelectWhere = "";
    if ($("#SelectWhereList tr").length > 0) {
        SelectWhere += "[";
        $("#SelectWhereList tr").each(function () {
            SelectWhere += "{'tableName':'" + $(this).find("td:eq(1)").children().val() + "',"
                        + "'tableField':'" + $(this).find("td:eq(2)").children().val() + "',"
                        + "'displayListing':'" + $(this).find("td:eq(3)").children().val() + "',"
                        + "'fuzzyInquires':'" + ($(this).find("td:eq(4)").children().attr("checked") == "checked" ? "Y" : "N") + "',"
                        + "'timeSelect':'" + ($(this).find("td:eq(5)").children().attr("checked") == "checked" ? "Y" : "N") + "',"
                        + "'moneySelect':'" + ($(this).find("td:eq(6)").children().attr("checked") == "checked" ? "Y" : "N") + "'},";
        });
        SelectWhere = SelectWhere.substring(0, SelectWhere.lastIndexOf(","));
        SelectWhere += "]";
        $("#SelectWhereHidden").val("").val(SelectWhere);
    }
    $("#step5").fadeOut("slow", function () {
        if (!isAlter) {
            LoadTablesField(null);
        }
        $("#step6").fadeIn("slow", function () {
            LoadAliasList();
        });
    });
}

function LoadSelectWhere() {
    var tableNames = ""; var fieldNames = "";
    var SaveTablesJson = $("#SelectTablesAndFieldHidden").val();
    var json = eval('(' + SaveTablesJson + ')');
    var selectwherejson;
    if ($("#SelectWhereHidden").val() != "") {
        selectwherejson = eval('(' + $("#SelectWhereHidden").val() + ')');
    }
    if (selectwherejson != null) {
        $("#SelectWhereList").html("");
        var isDel = true;

        for (var i = 0; i < selectwherejson.length; i++) {
            for (var j = 0; j < json.length; j++) {
                tableNames += "<option value=\"" + json[j].tableName + "\" " + (selectwherejson[i].tableName == json[j].tableName ? "selected=\"selected\"" : "") + ">" + json[j].tableName + "</option>";
                if (selectwherejson[i].tableName == json[j].tableName) {
                    for (var h = 0; h < json[j].data.length; h++) {
                        fieldNames += "<option value=\"" + json[j].data[h].ColumnName + "\" " + (selectwherejson[i].tableField == json[j].data[h].ColumnName ? "selected=\"selected\"" : "") + ">" + json[j].data[h].ColumnName + "</option>";
                    }
                    var tr = "<tr>"
                           + "  <td width=\"5%\" style=\"text-align:center;\">"
                           + "      " + ($("#SelectWhereList tr").length + 1) + ""
                           + "  </td>"
                           + "  <td width=\"20%\" style=\"text-align:center;\">"
                           + "      <select style=\"width:100%;\" onchange=\"GetSelectTableField(this)\">"
                           + "          " + tableNames + ""
                           + "      </select>"
                           + "  </td>"
                           + "  <td width=\"25%\" style=\"text-align:center;\">"
                           + "      <select style=\"width:100%;\" onchange=\"GetSelectFieldName(this)\">"
                           + "          " + fieldNames + ""
                           + "      </select>"
                           + "  </td>"
                           + "  <td width=\"25%\" style=\"text-align:center;\">"
                           + "      <input class=\"inputboder2\" style=\"width:98%;\" value=\"" + selectwherejson[i].displayListing + "\"/>"
                           + "  </td>"
                           + "  <td width=\"15%\" style=\"text-align:center;\">"
                           + "      <input type=\"checkbox\" " + (selectwherejson[i].fuzzyInquires == "Y" ? "checked=\"checked\"" : "") + "/>"
                           + "  </td>"
                           + "  <td width=\"20%\" style=\"text-align:center;\">"
                           + "      <input type=\"checkbox\" " + (selectwherejson[i].timeSelect == "Y" ? "checked=\"checked\"" : "") + "/>"
                           + "  </td>"
                           + "  <td width=\"20%\" style=\"text-align:center;\">"
                           + "      <input type=\"checkbox\" " + (selectwherejson[i].moneySelect == "Y" ? "checked=\"checked\"" : "") + "/>"
                           + "  </td>"
                           + "  <td width=\"10%\" style=\"text-align:center;\">"
                           + "      <a href=\"javascript:void(0);\" onclick=\"removeRow(this)\" style=\"color:Blue;\">删除</a>"
                           + "  </td>"
                           + "</tr>";
                    $("#SelectWhereList").append(tr);
                }
            }
        }
    }
}
