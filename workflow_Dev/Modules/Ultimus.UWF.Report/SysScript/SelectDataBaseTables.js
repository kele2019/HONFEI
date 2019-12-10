function LoadDataBaseTables() {
    var tablesjson;
    if ($("#SelectTablesAndFieldHidden").val() != "") {
        tablesjson = eval('(' + $("#SelectTablesAndFieldHidden").val() + ')');
    }
    $("#DataBaseTables").html("");
    $("#SelectTables").html("");
    $.post("AjaxPage/GetProcessName.ashx", function (data) {
        var names = eval('(' + data + ')');
        for (var n = 0; n < names.length; n++) {
            $("#dropProcessNames").append($("<option value=\"" + names[n].name + "\">" + names[n].name + "</option>"));
        }
    });

    $.post("AjaxPage/GetDataBaseTables.ashx", { "connectionString": $("#ConnectionStringHidden").val() }, function (date) {
        var json = eval('(' + date + ')');
        var tablenames = "";
        for (var i = 0; i < json.length; i++) {
            var item = $("<option value=\"" + json[i].tableName + "\">" + json[i].tableName + "</option>");
            var isSelectTable = false;
            if (isAlter) {
                for (var j = 0; j < tablesjson.length; j++) {
                    if (tablesjson[j].tableName == json[i].tableName) {
                        $("#SelectTables").append(item);
                        tablenames += json[i].tableName + ",";
                        isSelectTable = true;
                    }
                }
                if (!isSelectTable) {
                    $("#DataBaseTables").append(item);
                }
            } else {
                $("#DataBaseTables").append(item);
            }
        }
        tablenames = tablenames.substring(0, tablenames.length - 1);
        $("#tableName").val(tablenames);
if ($("#hfProcessName").val() != "") {
	//alert($("#hfProcessName").val());
        $("#dropProcessNames option[value='" + $("#hfProcessName").val() + "']").attr("selected", "selected");
    }
    if ($("#hfTableName").val() != "") {
        $("#tableName").val($("#hfTableName").val());
    }
    });

    
}

function DataBaseTablesSelected() {
    $("#tableName").val($("#DataBaseTables option:selected").val());
    $("#SelectTables").append($("#DataBaseTables option:selected"));
}
function DataBaseTablesSelectedAll() {
    var tablenames = "";
    $("#DataBaseTables option").each(function () {
        tablenames += $(this).val()+",";
    });
    tablenames = tablenames.substring(0, tablenames.length - 1);
    $("#tableName").val(tablenames);
    $("#SelectTables").append($("#DataBaseTables option"));
    $("#SelectTables option:eq(0)").attr("selected","selected");
}
function SelectTablesSelected() {
    $("#DataBaseTables").append($("#SelectTables option:selected"));
    var tablenames = "";
    $("#SelectTables option").each(function () {
        tablenames += $(this).val() + ",";
    });
    tablenames = tablenames.substring(0, tablenames.length - 1);
    $("#tableName").val(tablenames);
}
function SelectTablesSelectedAll() {
    $("#DataBaseTables").append($("#SelectTables option"));
    $("#tableName").val("");
}
function BackStep1() {
    $("#step2").fadeOut("slow", function () {
        $("#step1").fadeIn("slow");
    });
}

function NextTableRelation() {
    if (jQuery("#step2tab").validationEngine("validate")) {
        var selectTables = "[";
        $("#SelectTables option").each(function (item) {
            selectTables += "{'tableName':'" + $(this).val() + "'},";
        });
        selectTables = selectTables.substring(0, selectTables.lastIndexOf(','));
        selectTables += "]";
        $("#SelectTablesHidden").val(selectTables);
        $("#step2").fadeOut("slow", function () {
            $("#step3").fadeIn("slow");
            $("#tableInformationList").html("");
            var json = eval('(' + selectTables + ')');
            for (var i = 0; i < json.length; i++) {
                AddTableInformation(json[i].tableName);
            }
        });
    }
}
