//添加对应表关系
function AddTableRelations() {
    var SaveTablesJson = $("#SelectTablesAndFieldHidden").val();
    var json = eval('(' + SaveTablesJson + ')');
    var tableNameItems = "";
    var RelationItems = "<select style=\"width:100%;\">"
                              + "    <option value=\"LEFT JOIN\">左连接</option>"
                              + "    <option value=\"INNER JOIN\">内连接</option>"
                              + "    <option value=\"RIGHT JOIN\">右连接</option>"
                              + "</select>";
    for (var i = 0; i < json.length; i++) {
        tableNameItems += "<option value=\"" + json[i].tableName + "\">" + json[i].tableName + "</option>";
    }
    var tr = "<tr id=\"" + ($("#TableRelation_List tr").length + 1) + "\" class=\"TableRelation_List_Row\">"
           + "  <td width=\"20px\" style=\"text-align: center;\">"
           + "      <a href=\"javascript:void(0);\" onclick=\"HideOrShowInformation(this)\" style=\"color:Blue;\"></a>"
           + "  </td>"
           + "  <td width=\"150px\" style=\"text-align: center;\">"
           + "      <select style=\"width:100%;\">"
           + "          " + tableNameItems + ""
           + "      </select>"
           + "  </td>"
           + "  <td width=\"50px\" style=\"text-align: center;\">"
           + "      " + RelationItems + ""
           + "  </td>"
           + "  <td width=\"150px\" style=\"text-align: center;\">"
           + "      <select style=\"width:100%;\">"
           + "          " + tableNameItems + ""
           + "      </select>"
           + "  </td>"
           + "  <td width=\"70px\" style=\"text-align: center;\">"
           + "      <a href=\"javascript:void(0);\" style=\"color:Blue;\" onclick=\"AddRelationField(this)\">添加对应字段</a>"
           + "  </td>"
           + "  <td width=\"30px\" style=\"text-align: center;\">"
           + "      <a href=\"javascript:void(0);\" style=\"color:Blue;\" onclick=\"RemoveRelationField(this)\">删除</a>"
           + "  </td>"
           + "</tr>";
    $("#TableRelation_List").append(tr);
}

function RemoveRelationField(obj) {
    var tr = $(obj).parent().parent();
    var rowID = tr.attr("id");
    $("#" + rowID + "_FieldRelation_Information").remove();
    $(tr).remove();
}

//添加对应字段
function AddRelationField(obj) {
    var tr = $(obj).parent().parent();
    var tableName1 = tr.find("td:eq(1)").children().val();
    var tableName2 = tr.find("td:eq(3)").children().val();
    if (tableName1 != tableName2) {
        var tableName1Items = ""; var tableName2Items = "";
        var SaveTablesJson = $("#SelectTablesAndFieldHidden").val();
        var json = eval('(' + SaveTablesJson + ')');
        for (var i = 0; i < json.length; i++) {
            for (var j = 0; j < json[i].data.length; j++) {
                if (json[i].tableName == tableName1) {
                    tableName1Items += "<option value=\"" + json[i].data[j].ColumnName + "\">" + json[i].data[j].ColumnName + "</option>";
                } else if (json[i].tableName == tableName2) {
                    tableName2Items += "<option value=\"" + json[i].data[j].ColumnName + "\">" + json[i].data[j].ColumnName + "</option>";
                }
            }
        }
        var RelationItems = "<select style=\"width:100%;\">"
                          + "   <option value=\" = \"> = </option>"
                          + "   <option value=\" < \"> < </option>"
                          + "   <option value=\" > \"> > </option>"
                          + "   <option value=\" <> \"> <> </option>"
                          + "</select>";
        var item = "";
        if ($("#" + tr.attr("id") + "_FieldRelation_Information tr").length == 0) {
            item = "<tr>"
                 + "  <td colspan=\"6\">"
                 + "      <table id=\"" + tr.attr("id") + "_FieldRelation_Information\" border=\"0\" cellpadding=\"0\" cellspacing=\"0\" class=\"listTable\" style=\"width:100%;\">"
                 + "          <tr class=\"listTablebg\">"
                 + "              <th width=\"5%\" style=\"text-align:center;\"></th>"
                 + "              <th width=\"20%\" style=\"text-align:center;\">表名</th>"
                 + "              <th width=\"20%\" style=\"text-align:center;\">字段名</th>"
                 + "              <th width=\"10%\" style=\"text-align:center;\">对应关系</th>"
                 + "              <th width=\"20%\" style=\"text-align:center;\">表名</th>"
                 + "              <th width=\"20%\" style=\"text-align:center;\">字段名</th>"
                 + "              <th width=\"5%\" style=\"text-align:center;\">操作</th>"
                 + "          </tr>"
                 + "          <tr>"
                 + "              <td></td>"
                 + "              <td style=\"text-align:center;\">" + tableName1 + "</td>"
                 + "              <td style=\"text-align:center;\"><select style=\"width:100%;\">" + tableName1Items + "</select></td>"
                 + "              <td style=\"text-align:center;\">" + RelationItems + "</td>"
                 + "              <td style=\"text-align:center;\">" + tableName2 + "</td>"
                 + "              <td style=\"text-align:center;\"><select style=\"width:100%;\">" + tableName2Items + "</select></td>"
                 + "              <td style=\"text-align:center;\"><a href=\"javascript:void(0);\" onclick=\"DeleteField(this)\" style=\"color:Blue;\">删除</a></td>"
                 + "          </tr>"
                 + "      </table>"
                 + "  </td>"
                 + "</tr>";
            $(tr).after(item);
            $(tr).find("td:eq(0)").find("a").html("-");
            if ($(tr).css("background-color") == "red") {
                $(tr).css({ "background-color": "White" });
            }
        } else {
            item = "<tr>"
                 + "  <td></td>"
                 + "  <td style=\"text-align:center;\">" + tableName1 + "</td>"
                 + "  <td style=\"text-align:center;\"><select style=\"width:100%;\">" + tableName1Items + "</select></td>"
                 + "  <td style=\"text-align:center;\">" + RelationItems + "</td>"
                 + "  <td style=\"text-align:center;\">" + tableName2 + "</td>"
                 + "  <td style=\"text-align:center;\"><select style=\"width:100%;\">" + tableName2Items + "</select></td>"
                 + "  <td style=\"text-align:center;\"><a href=\"javascript:void(0);\" onclick=\"DeleteField(this)\" style=\"color:Blue;\">删除</a></td>"
                 + "</tr>";
            $("#" + tr.attr("id") + "_FieldRelation_Information").fadeIn("slow", function () {
                $("#" + tr.attr("id") + "_FieldRelation_Information").append(item);
                $(tr).find("td:eq(0)").find("a").html("-");
                if ($(tr).css("background-color") == "red") {
                    $(tr).css({ "background-color": "White" });
                }
            });
        }
    } else {
        alert("不能选择同一张表.");
    }
}

function HideOrShowInformation(obj) {
    var tr = $(obj).parent().parent();
    if ($(tr).find("td:eq(0)").find("a").html() == "-") {
        $("#" + tr.attr("id") + "_FieldRelation_Information").fadeOut("slow", function () {
            $(tr).find("td:eq(0)").find("a").html("+");
        });
    } else if ($(tr).find("td:eq(0)").find("a").html() == "+") {
        $("#" + tr.attr("id") + "_FieldRelation_Information").fadeIn("slow", function () {
            $(tr).find("td:eq(0)").find("a").html("-");
        });
    }
}

function AddField() {
    var SaveTablesJson = $("#SelectTablesAndFieldHidden").val();
    var json = eval('(' + SaveTablesJson + ')');
    var tableNameItems = "<select style=\"width:98%;\" onchange=\"LoadTableField('" + $("#Field_List tr").length + "_Field_List')\">";
    for (var i = 0; i < json.length; i++) {
        tableNameItems += "<option value=\"" + json[i].tableName + "\">" + json[i].tableName + "</option>";
    }
    tableNameItems += "</select>";
    var regulation = "<select style=\"width:100%;\">"
                   + "<option value=\"SUM\">求和</option>"
                   + "</select>";
    var RowID = "" + $("#Field_List tr").length + "_Field_List";
    var tr = "<tr id=\"" + RowID + "\">"
           + "  <td width=\"150px\" style=\"text-align:center;\">"
           + "      " + tableNameItems + ""
           + "  </td>"
           + "  <td width=\"130px\" style=\"text-align:center;\">"
           + "      <select style=\"width:98%;\"></select>"
           + "  </td>"
           + "  <td width=\"100px\" style=\"text-align:center;\">"
           + "      " + regulation + ""
           + "  </td>"
           + "  <td width=\"30px\" style=\"text-align:center;\">"
           + "      <a href=\"javascript:void(0);\" onclick=\"DeleteField(this)\" style=\"color:Blue;\">删除</a>"
           + "  </td>"
           + "</tr>";

    $("#Field_List").append(tr);
    LoadTableField(RowID);
}

function DeleteField(obj) {
    $(obj).parent().parent().remove();
}

function LoadTableField(actionRowID) {
    var tr = $("#" + actionRowID);
    var tableName = tr.find("td:eq(0)").children().val();
    var SaveTablesJson = $("#SelectTablesAndFieldHidden").val();
    var json = eval('(' + SaveTablesJson + ')');
    var Fields = "";
    for (var i = 0; i < json.length; i++) {
        for (var j = 0; j < json[i].data.length; j++) {
            if (json[i].tableName == tableName) {
                Fields += "<option value=\"" + json[i].data[j].ColumnName + "\">" + json[i].data[j].ColumnName + "</option>";
            }
        }
    }
    tr.find("td:eq(1)").children().html("").append(Fields);
}

function BackSetTablesField() {
    $("#step4").fadeOut("slow", function () {
        $("#step3").fadeIn("slow");
    });
}

function NextSetSelectWhere() {
    var TableRelationJson = ""; var checkFieldLength = true;
    var FieldRules = "";
    if ($("#TableRelation_List tr[class='TableRelation_List_Row']").length > 0) {
        TableRelationJson += "[";
        $("#TableRelation_List tr[class='TableRelation_List_Row']").each(function () {
            TableRelationJson += "{'tableName1':'" + $(this).find("td:eq(1)").children().val() + "',"
                               + "'relation':'" + $(this).find("td:eq(2)").children().val() + "',"
                               + "'tableName2':'" + $(this).find("td:eq(3)").children().val() + "',"
                               + "fields:[";
            var rowID = $(this).attr("id");
            if (checkFieldLength) {
                if ($("#" + rowID + "_FieldRelation_Information tr").length < 1) {
                    alert("请添加关系字段.");
                    $(this).css({ "background-color": "Red" });
                    checkFieldLength = false;
                    TableRelationJson = "";
                } else {
                    $("#" + rowID + "_FieldRelation_Information tr:eq(1)").each(function () {
                        TableRelationJson += "{'tableName1':'" + $(this).find("td:eq(1)").text() + "',"
                                           + "'table1FieldName':'" + $(this).find("td:eq(2)").children().val() + "',"
                                           + "'fieldRelation':'" + $(this).find("td:eq(3)").children().val() + "',"
                                           + "'tableName2':'" + $(this).find("td:eq(4)").text() + "',"
                                           + "'table2FieldName':'" + $(this).find("td:eq(5)").children().val() + "'},";
                    });
                    TableRelationJson = TableRelationJson.substring(0, TableRelationJson.lastIndexOf(","));
                    TableRelationJson += "]},";
                }
            }
        });
        TableRelationJson = TableRelationJson.substring(0, TableRelationJson.lastIndexOf(","));
        TableRelationJson += "]";
        $("#Table_Field_RelationHidden").val("").val(TableRelationJson);
    }
    if ($("#Field_List tr").length > 0) {
        FieldRules += "[";
        $("#Field_List tr").each(function () {
            FieldRules += "{'tableName':'" + $(this).find("td:eq(0)").children().val() + "',"
                        + "'fieldName':'" + $(this).find("td:eq(1)").children().val() + "',"
                        + "'rules':'" + $(this).find("td:eq(2)").children().val() + "'},";
        });
        FieldRules = FieldRules.substring(0, FieldRules.lastIndexOf(","));
        FieldRules += "]";
        $("#Table_Field_RulesHidden").val("").val(FieldRules);
    }
    if (checkFieldLength) {
        $("#step4").fadeOut("slow", function () {
            $("#step5").fadeIn("slow", function () {
                LoadSelectWhere();
            });
        });
    }
}


function LoadTableRelation() {
    var Relationjson;
    if ($("#Table_Field_RelationHidden").val() != "") {
        Relationjson = eval('(' + $("#Table_Field_RelationHidden").val() + ')');
    }
    var SaveTablesJson = $("#SelectTablesAndFieldHidden").val();
    var tablesjson = eval('(' + SaveTablesJson + ')');
    var rowID;
    if (Relationjson != null) {
        $("#TableRelation_List").html("");
        for (var i = 0; i < Relationjson.length; i++) {
            var isDel = true;
            for (var k = 0; k < tablesjson.length; k++) {
                if (Relationjson[i].tableName1 != tablesjson[k].tableName && Relationjson[i].tableName2 != tablesjson[k].tableName) {
                    isDel = false;
                    break;
                } else if (Relationjson[i].tableName1 == tablesjson[k].tableName || Relationjson[i].tableName2 == tablesjson[k].tableName) {
                    break;
                }
            }
            if (!isDel) {
                continue;
            }
            rowID = $("#TableRelation_List tr").length + 1;
            var tableNameItems1 = ""; var tableNameItems2 = "";
            var RelationItems = "<select style=\"width:100%;\">"
                              + "    <option value=\"LEFT JOIN\" " + (Relationjson[i].relation == "LEFT JOIN" ? "selected=\"selected\"" : "") + ">左连接</option>"
                              + "    <option value=\"INNER JOIN\" " + (Relationjson[i].relation == "INNER JOIN" ? "selected=\"selected\"" : "") + ">内连接</option>"
                              + "    <option value=\"RIGHT JOIN\" " + (Relationjson[i].relation == "RIGHT JOIN" ? "selected=\"selected\"" : "") + ">右连接</option>"
                              + "</select>";
            for (var j = 0; j < tablesjson.length; j++) {
                tableNameItems1 += "<option value=\"" + tablesjson[j].tableName + "\" " + (Relationjson[i].tableName1 == tablesjson[j].tableName ? "selected=\"selected\"" : "") + ">" + tablesjson[j].tableName + "</option>";
                tableNameItems2 += "<option value=\"" + tablesjson[j].tableName + "\" " + (Relationjson[i].tableName2 == tablesjson[j].tableName ? "selected=\"selected\"" : "") + ">" + tablesjson[j].tableName + "</option>";
            }
            var tr = "<tr id=\"" + rowID + "\" class=\"TableRelation_List_Row\">"
                   + "  <td width=\"20px\" style=\"text-align: center;\">"
                   + "      <a href=\"javascript:void(0);\" onclick=\"HideOrShowInformation(this)\" style=\"color:Blue;\"></a>"
                   + "  </td>"
                   + "  <td width=\"150px\" style=\"text-align: center;\">"
                   + "      <select style=\"width:100%;\">"
                   + "          " + tableNameItems1 + ""
                   + "      </select>"
                   + "  </td>"
                   + "  <td width=\"50px\" style=\"text-align: center;\">"
                   + "      " + RelationItems + ""
                   + "  </td>"
                   + "  <td width=\"150px\" style=\"text-align: center;\">"
                   + "      <select style=\"width:100%;\">"
                   + "          " + tableNameItems2 + ""
                   + "      </select>"
                   + "  </td>"
                   + "  <td width=\"70px\" style=\"text-align: center;\">"
                   + "      <a href=\"javascript:void(0);\" style=\"color:Blue;\" onclick=\"AddRelationField(this)\">添加对应字段</a>"
                   + "  </td>"
                   + "  <td width=\"30px\" style=\"text-align: center;\">"
                   + "      <a href=\"javascript:void(0);\" style=\"color:Blue;\" onclick=\"RemoveRelationField(this)\">删除</a>"
                   + "  </td>"
                   + "</tr>";
            $("#TableRelation_List").append(tr);

            for (var u = 0; u < Relationjson[i].fields.length; u++) {
                var tableName1 = Relationjson[i].tableName1;
                var tableName2 = Relationjson[i].tableName2;
                var tableName1Items = ""; var tableName2Items = "";
                var SaveTablesJson = $("#SelectTablesAndFieldHidden").val();
                var json = eval('(' + SaveTablesJson + ')');
                for (var h = 0; h < json.length; h++) {
                    for (var g = 0; g < json[h].data.length; g++) {
                        if (json[h].tableName == tableName1) {
                            tableName1Items += "<option value=\"" + json[h].data[g].ColumnName + "\" " + (Relationjson[i].fields[u].table1FieldName == json[h].data[g].ColumnName ? "selected=\"selected\"" : "") + ">" + json[h].data[g].ColumnName + "</option>";
                        } else if (json[h].tableName == tableName2) {
                            tableName2Items += "<option value=\"" + json[h].data[g].ColumnName + "\" " + (Relationjson[i].fields[u].table2FieldName == json[h].data[g].ColumnName ? "selected=\"selected\"" : "") + ">" + json[h].data[g].ColumnName + "</option>";
                        }
                    }
                }
                var RelationItems = "<select style=\"width:100%;\">"
                                  + "   <option value=\" = \" " + ($.trim(Relationjson[i].fields[u].fieldRelation) == "=" ? "selected=\"selected\"" : "") + "> = </option>"
                                  + "   <option value=\" < \" " + ($.trim(Relationjson[i].fields[u].fieldRelation) == "<" ? "selected=\"selected\"" : "") + "> < </option>"
                                  + "   <option value=\" > \" " + ($.trim(Relationjson[i].fields[u].fieldRelation) == ">" ? "selected=\"selected\"" : "") + "> > </option>"
                                  + "   <option value=\" <> \" " + ($.trim(Relationjson[i].fields[u].fieldRelation) == "<>" ? "selected=\"selected\"" : "") + "> <> </option>"
                                  + "</select>";
                var item = "";
                if ($("#" + rowID + "_FieldRelation_Information tr").length == 0) {
                    item = "<tr>"
                         + "  <td colspan=\"6\">"
                         + "      <table id=\"" + rowID + "_FieldRelation_Information\" border=\"0\" cellpadding=\"0\" cellspacing=\"0\" class=\"listTable\" style=\"width:100%;\">"
                         + "          <tr class=\"listTablebg\">"
                         + "              <th width=\"5%\" style=\"text-align:center;\"></th>"
                         + "              <th width=\"20%\" style=\"text-align:center;\">表名</th>"
                         + "              <th width=\"20%\" style=\"text-align:center;\">字段名</th>"
                         + "              <th width=\"10%\" style=\"text-align:center;\">对应关系</th>"
                         + "              <th width=\"20%\" style=\"text-align:center;\">表名</th>"
                         + "              <th width=\"20%\" style=\"text-align:center;\">字段名</th>"
                         + "              <th width=\"5%\" style=\"text-align:center;\">操作</th>"
                         + "          </tr>"
                         + "          <tr>"
                         + "              <td></td>"
                         + "              <td style=\"text-align:center;\">" + tableName1 + "</td>"
                         + "              <td style=\"text-align:center;\"><select style=\"width:100%;\">" + tableName1Items + "</select></td>"
                         + "              <td style=\"text-align:center;\">" + RelationItems + "</td>"
                         + "              <td style=\"text-align:center;\">" + tableName2 + "</td>"
                         + "              <td style=\"text-align:center;\"><select style=\"width:100%;\">" + tableName2Items + "</select></td>"
                         + "              <td style=\"text-align:center;\"><a href=\"javascript:void(0);\" onclick=\"DeleteField(this)\" style=\"color:Blue;\">删除</a></td>"
                         + "          </tr>"
                         + "      </table>"
                         + "  </td>"
                         + "</tr>";
                    if ($("#" + rowID + "_FieldRelation_Information").length < 1) {
                        $("#" + rowID).after(item);
                        $("#" + rowID).find("td:eq(0)").find("a").html("-");
                        if ($("#" + rowID).css("background-color") == "red") {
                            $("#" + rowID).css({ "background-color": "White" });
                        }
                    }
                } else {
                    item = "<tr>"
                         + "  <td></td>"
                         + "  <td style=\"text-align:center;\">" + tableName1 + "</td>"
                         + "  <td style=\"text-align:center;\"><select style=\"width:100%;\">" + tableName1Items + "</select></td>"
                         + "  <td style=\"text-align:center;\">" + RelationItems + "</td>"
                         + "  <td style=\"text-align:center;\">" + tableName2 + "</td>"
                         + "  <td style=\"text-align:center;\"><select style=\"width:100%;\">" + tableName2Items + "</select></td>"
                         + "  <td style=\"text-align:center;\"><a href=\"javascript:void(0);\" onclick=\"DeleteField(this)\" style=\"color:Blue;\">删除</a></td>"
                         + "</tr>";
                    $("#" + rowID + "_FieldRelation_Information").fadeIn("slow", function () {
                        $("#" + rowID + "_FieldRelation_Information").append(item);
                        $("#" + rowID).find("td:eq(0)").find("a").html("-");
                        if ($("#" + rowID).css("background-color") == "red") {
                            $("#" + rowID).css({ "background-color": "White" });
                        }
                    });
                }
            }
        }
    }
}

function LoadRules() {
    var rulesjson;
    if ($("#Table_Field_RulesHidden").val() != "") {
        rulesjson = eval('(' + $("#Table_Field_RulesHidden").val() + ')');
    }
    var SaveTablesJson = $("#SelectTablesAndFieldHidden").val();
    var json = eval('(' + SaveTablesJson + ')');
    if (rulesjson != null) {
        $("#Field_List").html("");
        for (var j = 0; j < rulesjson.length; j++) {
            var tableNameItems = "<select style=\"width:98%;\" onchange=\"LoadTableField('" + $("#Field_List tr").length + "_Field_List')\">";
            var rulesField = "";
            for (var i = 0; i < json.length; i++) {
                for (var k = 0; k < json[i].data.length; k++) {
                    if (json[i].tableName == rulesjson[j].tableName) {
                        rulesField += "<option value=\"" + json[i].data[k].ColumnName + "\" " + (rulesjson[j].fieldName == json[i].data[k].ColumnName ? "selected=\"selected\"" : "") + ">" + json[i].data[k].ColumnName + "</option>";
                    }
                }
                tableNameItems += "<option value=\"" + json[i].tableName + "\" " + (rulesjson[j].tableName == json[i].tableName ? "selected=\"selected\"" : "") + ">" + json[i].tableName + "</option>";
            }
            tableNameItems += "</select>";
            var regulation = "<select style=\"width:100%;\">"
                   + "<option value=\"SUM\">求和</option>"
                   + "</select>";
            var RowID = "" + $("#Field_List tr").length + "_Field_List";
            var tr = "<tr id=\"" + RowID + "\">"
                   + "  <td width=\"150px\" style=\"text-align:center;\">"
                   + "      " + tableNameItems + ""
                   + "  </td>"
                   + "  <td width=\"130px\" style=\"text-align:center;\">"
                   + "      <select style=\"width:98%;\">"
                   + "          " + rulesField + ""
                   + "      </select>"
                   + "  </td>"
                   + "  <td width=\"100px\" style=\"text-align:center;\">"
                   + "      " + regulation + ""
                   + "  </td>"
                   + "  <td width=\"30px\" style=\"text-align:center;\">"
                   + "      <a href=\"javascript:void(0);\" onclick=\"DeleteField(this)\" style=\"color:Blue;\">删除</a>"
                   + "  </td>"
                   + "</tr>";
            $("#Field_List").append(tr);
        }
    }
}
