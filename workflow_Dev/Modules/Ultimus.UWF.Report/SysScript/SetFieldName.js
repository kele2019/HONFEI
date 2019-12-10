function LoadTablesField(obj) {
    if (obj != null) {
        $("[name='AliasCheckBox']:checkbox").removeAttr("checked");
        $(obj).attr("checked", "checked");
    }
    var SaveTablesJson = $("#SelectTablesAndFieldHidden").val();
    var json = eval('(' + SaveTablesJson + ')');
    var tr = ""; var rowIndex = 1;
    for (var i = 0; i < json.length; i++) {
        for (var j = 0; j < json[i].data.length; j++) {
            tr += "<tr>"
                + " <td width=\"10px\" style=\"text-align:center;\">"
                + "     " + rowIndex + ""
                + " </td>"
                + " <td width=\"150px\" style=\"text-align:center;\">"
                + "     " + json[i].tableName + ""
                + " </td>"
                + " <td width=\"150px\" style=\"text-align:center;\">"
                + "     " + json[i].data[j].ColumnName + ""
                + " </td>"
                + " <td width=\"100px\" style=\"text-align:center;\">"
                + "     <input value=\"" + (obj != null && $.trim($(obj).val()) == "取数据库备注列" ? json[i].data[j].ColumnDesc : json[i].data[j].ColumnName) + "\" class=\"inputboder2\"/>"
                + " </td>"
                + " <td width=\"200px\" style=\"text-align:center;\">"
                + "     <textarea class=\"inputboder2\" style=\"height:50px;\" value=\"" + json[i].data[j].openUrl + "\"></textarea>"
                + " </td>"
                + " <td width=\"100px\" style=\"text-align:center;\">"
                + "     <input type=\"checkbox\" " + (json[i].data[j].isHide=="Y"?"checked=\"checked\"":"") + "/>"
                + " </td>"
                + " <td width=\"100px\" style=\"text-align:center;\">"
                + "     <input />(%)"
                + " </td>"
                + " <td width=\"100px\" style=\"text-align:center;\">"
                + "     <input type=\"button\" value=\"↑\" onclick=\"getTop(this)\"/>"
                + "     <input type=\"button\" value=\"↓\" onclick=\"getButton(this)\"/>"
                + " </td>"
                + "</tr>";
            rowIndex++;
        }
    }
    $("#AliasList").html("").append(tr);
}

function BackSetSelectWhere() {
    $("#step6").fadeOut("slow", function () {
        $("#step5").fadeIn("slow");
    });
}

function NextSetOther() {
    var slias = "[";
    var names = "";
    var checkName = true;
    var errorIndex = 0;
    for (var i = 0; i < $("#AliasList tr").length; i++) {
        var tr = $("#AliasList tr:eq(" + i + ")");
        if (names.indexOf($.trim($(tr).find("td:eq(3)").children().val())) >= 0) {
            errorIndex = i;
            checkName = false;
            break;
        }
        names += $.trim($(tr).find("td:eq(3)").children().val()) + ",";
        slias += "{'tableName':'" + $.trim($(tr).find("td:eq(1)").text()) + "',"
               + "'fieldName':'" + $.trim($(tr).find("td:eq(2)").text()) + "',"
               + "'sliasName':'" + $.trim($(tr).find("td:eq(3)").children().val()) + "',"
               + "'openUrl':'" + $.trim($(tr).find("td:eq(4)").children().val()) + "',"
               + "'isHide':'" + ($(tr).find("td:eq(5)").children().attr("checked") ? "Y" : "N") + "',"
               + "'width':'" + $.trim($(tr).find("td:eq(6)").children().val()) + "'},";
    }
    if (checkName) {
        slias = slias.substring(0, slias.lastIndexOf(","));
        slias += "]";
        $("#SetSilasHidden").val("").val(slias);
        $("#step6").fadeOut("slow", function () {
            $("#step7").fadeIn("slow", function () {
                LoadTableName();
            });
        });
    } else {
        alert("别名重复,请检查!");
        $("#AliasList tr:eq(" + errorIndex + ")").find("td:eq(3)").children().focus();
        $("#AliasList tr:eq(" + errorIndex + ")").find("td:eq(3)").children().select();
    }
}

function LoadAliasList() {
    var aliasjson;
    if ($("#SetSilasHidden").val() != "") {
        aliasjson = eval('(' + $("#SetSilasHidden").val() + ')');
    }
    var selectTables;
    if ($("#SelectTablesAndFieldHidden").val() != "") {
        selectTables = eval('(' + $("#SelectTablesAndFieldHidden").val() + ')');
    }
    var tr = "";
    var rowIndex = 1;
    var showAliasName = "";
    var openUrl = "";
    var ishide = "";
    var width = "";
    for (var i = 0; i < selectTables.length; i++) {
        for (var j = 0; j < selectTables[i].data.length; j++) {
            showAliasName = selectTables[i].data[j].ColumnName;
            if (aliasjson != null) {
                for (var k = 0; k < aliasjson.length; k++) {
                    if (selectTables[i].tableName == aliasjson[k].tableName && selectTables[i].data[j].ColumnName == aliasjson[k].fieldName) {
                        showAliasName = aliasjson[k].sliasName;
                        openUrl = aliasjson[k].openUrl;
                        ishide = aliasjson[k].isHide;
                        try{
                        	width = aliasjson[j].width;
			}catch(e){
				width="";
			}
                    }
                }
            }
            tr += "<tr>"
                + " <td width=\"10px\" style=\"text-align:center;\">"
                + "     " + rowIndex + ""
                + " </td>"
                + " <td width=\"150px\" style=\"text-align:center;\">"
                + "     " + selectTables[i].tableName + ""
                + " </td>"
                + " <td width=\"150px\" style=\"text-align:center;\">"
                + "     " + selectTables[i].data[j].ColumnName + ""
                + " </td>"
                + " <td width=\"100px\" style=\"text-align:center;\">"
                + "     <input value=\"" + showAliasName + "\" class=\"inputboder2\"/>"
                + " </td>"
                + " <td width=\"200px\" style=\"text-align:center;\">"
                + "     <textarea class=\"inputboder2\" style=\"height:50px;width:200px;\" value=\"" + openUrl + "\">" + openUrl + "</textarea>"
                + " </td>"
                + " <td width=\"100px\" style=\"text-align:center;\">"
                + "     <input type=\"checkbox\" " + (ishide == "Y" ? "checked=\"checked\"" : "") + "/>"
                + " </td>"
                + " <td width=\"100px\" style=\"text-align:center;\">"
                + "     <input value=\"" + width + "\"/>(%)"
                + " </td>"
                + " <td width=\"100px\" style=\"text-align:center;\">"
                + "     <input type=\"button\" value=\"↑\" onclick=\"getTop(this)\"/>"
                + "     <input type=\"button\" value=\"↓\" onclick=\"getButton(this)\"/>"
                + " </td>"
                + "</tr>";
            rowIndex++;
        }
    }
    $("#AliasList").html("").append(tr);
    $("#TakeDatabaseListing").removeAttr("checked");
}

function getTop(obj) {
    var tr = $(obj).parent().parent();
    var prant = tr.prev().prev();
    if ($(prant).html() != null && $(prant).html() != undefined && $(prant).html() != "undefined") {
        prant.after(tr.clone());
        tr.remove();
    } else {
        alert("已经是第一行");
    }
}

function getButton(obj) {
    var tr = $(obj).parent().parent();
    var next = tr.next();
    if ($(next).html() != null && $(next).html() != undefined && $(next).html() != "undefined") {
        next.after(tr.clone());
        tr.remove();
    } else {
        alert("已经是最后一行");
    }
    
}
