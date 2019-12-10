function BackStep2() {
    $("#step3").fadeOut("slow", function () {
        $("#step2").fadeIn("slow");
    });
}

function removeTables(removeDivID, tableName) {
    if (confirm("你确定删除吗?")) {
        $("#" + removeDivID).remove();
        $("#DataBaseTables").append($("#SelectTables option[value=" + tableName + "]"));
    }
}

function AddTableInformation(tableName) {
    $.ajax({
        type: "POST",
        url: "AjaxPage/GetTableInformcation.ashx?id=" + new Date().getTime() + "",
        data: "connectionString=" + $("#ConnectionStringHidden").val() + "&tableName=" + tableName + "",
        success: function (date) {
            var json = eval('(' + date + ')');
            var tablesjson;
            if ($("#SelectTablesAndFieldHidden").val() != "") {
                tablesjson = eval('(' + $("#SelectTablesAndFieldHidden").val() + ')');
            }
            var addDiv = "<div id=\"Div_" + tableName + "\" style=\"border:1px #cccccc solid;float:left;margin:5px 5px 0px 5px;\">"
                       + "  <table class=\"listTable3\" style=\"width:95%\">"
                       + "      <tr>"
                       + "           <td colspan=\"4\">"
                       + "               <h3 class=\"titleBG\">"
                       + "                   " + tableName + ""
                       + "                    <a href=\"javascript:void(0);\" onclick=\"removeTables('Div_" + tableName + "','" + tableName + "')\" style=\"color:Blue;\">删除</a>"
                       + "               </h3>"
                       + "           </td>"
                       + "      </tr>"
                       + "      <tr class=\"listTablebg\">"
                       + "           <th width=\"30px\"><input type=\"checkbox\" " + (tablesjson == null ? "checked=\"checked\"" : "") + " onclick=\"SelectThisTableAll(this,'" + tableName + "')\"/></th>"
                       + "           <th width=\"100px\">字段名</th>"
                       + "           <th width=\"100px\">字段说明</th>"
                       + "       </tr>"
                       + "  </table>"
                       + "  <div>"
                       + "      <table class=\"InformationTable\" width=\"100%\" id=\"" + tableName + "\">";
            for (var i = 0; i < json.length; i++) {
                var checkString = "";
                if (tablesjson != null) {
                    for (var j = 0; j < tablesjson.length; j++) {
                        for (var h = 0; h < tablesjson[j].data.length; h++) {
                            if (tablesjson[j].tableName == tableName) {
                                if (tablesjson[j].data[h].ColumnName == json[i].ColumnName) {
                                    checkString = "checked=\"checked\"";
                                }
                            }
                        }
                    }
                } else {
                    checkString = "checked=\"checked\"";
                }
                addDiv += "<tr>"
                        + "<td width=\"30px\"><input type=\"checkbox\" " + checkString + " name=\"RelationTableList_CheckBox\" id=\"" + tableName + "_" + json[i].ColumnName + "\"/></td>"
                        + "<td width=\"100px\" title=\"" + json[i].ColumnName + "\">" + (json[i].ColumnName.length > 15 ? json[i].ColumnName.substring(0, 15) : json[i].ColumnName) + "</td>"
                        + "<td width=\"100px\" title=\"" + json[i].DataPrecision + "\">" + (json[i].DataPrecision.length > 7 ? json[i].DataPrecision.substring(0, 7) : json[i].DataPrecision) + "</td></tr>";
            }
            addDiv += "</table>"
                    + "</div>"
                    + "</div>";
            $("#tableInformationList").append(addDiv);
        }
    });
}

function NextSetTableRelation() {
    var SelectTablesAndFieldJson = ""; var checkSelectField = true; var checkField = false;
    var error = "";
    if ($("#tableInformationList table[class='InformationTable']").length > 0) {
        SelectTablesAndFieldJson += "[";
        $("#tableInformationList table[class='InformationTable']").each(function (i, item) {
            if (checkSelectField) {
                SelectTablesAndFieldJson += "{'tableName':'" + $(this).attr("id") + "',data:[";
                $(this).find("tr").each(function () {
                    if ($(this).find("td:eq(0)").children().attr("checked")) {
                        checkField = true;
                        SelectTablesAndFieldJson += "{'ColumnName':'" + $(this).find("td:eq(1)").attr("title") + "',"
                                                  + "'ColumnDesc':'" + $(this).find("td:eq(2)").attr("title") + "'},";
                    }
                });
                checkSelectField = checkField;
                checkField = false;
                if (checkSelectField) {
                    SelectTablesAndFieldJson = SelectTablesAndFieldJson.substring(0, SelectTablesAndFieldJson.lastIndexOf(","));
                    SelectTablesAndFieldJson += "]},";
                } else {
                    error += $(item).attr("id") + "没有选择字段.";
                }
            }
            else {
                error += $(item).attr("id") + "没有选择字段.";
            }
        });
        if (checkSelectField) {
            SelectTablesAndFieldJson = SelectTablesAndFieldJson.substring(0, SelectTablesAndFieldJson.lastIndexOf(","));
            SelectTablesAndFieldJson += "]";
            //$("#Table_Field_RelationHidden").val("");
            $("#SelectTablesAndFieldHidden").val("").val(SelectTablesAndFieldJson);
            $("#step3").fadeOut("slow", function () {
                if ($("#tableInformationList table[class='InformationTable']").length > 1) {
                    $("#AddTableRelation").removeAttr("disabled");
                } else {
                    $("#AddTableRelation").attr("disabled", "disabled");
                }
                $("#step4").fadeIn("slow", function () {
                    if (isAlter) {
                        LoadTableRelation();
                        LoadRules();
                    }
                });
            });
        } else {
            checkSelectField = true;
            alert(error);
        }
    } else {
        alert("请选择需要查询的表和字段.");
    }
}

function SelectThisTableAll(obj, tableID) {
    if ($(obj).attr("checked")) {
        $("#" + tableID).find("input[type=checkbox][name='RelationTableList_CheckBox']").attr("checked", "checked");
    } else {
        $("#" + tableID).find("input[type=checkbox][name='RelationTableList_CheckBox']").removeAttr("checked");
    }
}

