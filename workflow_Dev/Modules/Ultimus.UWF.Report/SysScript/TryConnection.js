var isAlter = false;
$(function () {
    $.ajax({
        type: "POST",
        url: "AjaxPage/GetConnectionStringList.ashx?id=" + new Date().getTime() + "",
        async: false,
        success: function (date) {
            var json = eval('(' + date + ')');
            $("#ConnectionStringList").html("");
            for (var i = 0; i < json.length; i++) {
                $("#ConnectionStringList").append($("<option value=\"" + json[i].value + "\">" + json[i].name + "</option>"));
            }
        }
    });
    if (location.search != "") {//不为空，则是修改
        var alterGuid = location.search.split("=")[1];
        $.post("AjaxPage/GetAlterGuidInformation.ashx?id=" + new Date().getTime() + "", { "alterGuid": alterGuid }, function (date) {
            var json = eval('(' + date + ')');
            $("#AlterGuid").val(json.Guid);
            $("#hfProcessName").val(json.ProcessName);
            $("#hfTableName").val(json.TableName);
            $("#ConnectionStringHidden").val("").val(json.ConnectionString);
            $("#SelectedConnectionString").append($("#ConnectionStringList option[value='" + json.ConnectionString + "']"));
            $("#SelectTablesAndFieldHidden").val(json.TableNamesAndFieldNames);
            $("#Table_Field_RelationHidden").val(json.TableRelation);
            $("#Table_Field_RulesHidden").val(json.FieldRules);
            $("#SelectWhereHidden").val(json.SelectWhere);
            $("#SetSilasHidden").val(json.Alias);
            $("#ReportName").val(json.ReportName);
            $("#SortStyleHidden").val(json.SortStyle);
            $("#AuthorityHidden").val(json.UserAccountFieldName);
            $("#SelectViewUserNames").val(json.ViewReportUserName);
            $("#SelectViewUserAccounts").val(json.ViewReportUserAccount);
            $("#SaveReportButton").val("更新报表");
            if (json.WhetherPaging == "Y") {
                $("#WhetherPaging").attr("checked", "checked");
                $(".OpeningPageClass").show();
                $("#ArticleThatNumber").val(json.ArticlethatNumber);
            }
            $("[name='viewreport']:checkbox").removeAttr("checked");
            $("[name='viewreport'][value='" + json.ViewPrivilege + "']:checkbox").attr("checked", "checked");
            if (json.ViewPrivilege == "0") {
                $("#SelectViewRow").hide();
                $("#selectuserrow").hide();
            } else if (json.ViewPrivilege == "1") {
                $("#ViewUserTableNameHidden").val(json.ViewReportUserAccount.split('.')[0]);
                $("#ViewUserFieldNameHidden").val(json.ViewReportUserAccount.split('.')[1]);
                $("#SelectViewRow").show();
                $("#selectuserrow").hide();
            }
            else if (json.ViewPrivilege == "2") {
                $("#viewitems").val(json.ViewReportUserName);
                $("#ViewUserTableNameHidden").val(json.ViewReportUserAccount.split('.')[0]);
                $("#ViewUserFieldNameHidden").val(json.ViewReportUserAccount.split('.')[1]);
                $("#SelectViewRow").show();
                $("#selectuserrow").show();
            }
            $("#step1").fadeOut("slow", function () {
                $("#step2").fadeIn("slow");
                $("#BackSelectConnectionString").attr("disabled", "disabled");
                LoadDataBaseTables(); //加载数据库表名称
            });
        });
        isAlter = true;
    } else {
        $.ajax({
            type: "POST",
            url: "AjaxPage/GetConnectionStringList.ashx?id=" + new Date().getTime() + "",
            async: false,
            success: function (date) {
                var json = eval('(' + date + ')');
                $("#ConnectionStringList").html("");
                for (var i = 0; i < json.length; i++) {
                    $("#ConnectionStringList").append($("<option value=\"" + json[i].value + "\">" + json[i].name + "</option>"));
                }
            }
        });
    }
});
function ConnectionDataBase() {
    //验证数据库连接的信息是都填写完整
    if (jQuery("#step1tab").validationEngine("validate")) {
        var connectionString = $("#SelectedConnectionString option:eq(0)").val();
        $("#ConnectionStringHidden").val("").val(connectionString);
        $.ajax({
            type: "POST",
            url: "AjaxPage/TryConnection.ashx",
            data: "connectionString=" + connectionString + "",
            async: false,
            success: function (msg) {
                $("#ConnectionSucceed").val(msg);
            }
        });
    }
}

function TryConnectionDataBase() {
    ConnectionDataBase();
    if ($("#ConnectionSucceed").val() == "") {
        alert("测试连接未成功!");
    } else {
        alert("测试连接成功!");
    }
    return false;
}

function NextSelectTableName() {
    ConnectionDataBase();
    if ($("#ConnectionSucceed").val() == "") {
        alert("连接未成功!请检查.");
    } else {
        $("#step1").fadeOut("slow", function () {
            $("#step2").fadeIn("slow");
            LoadDataBaseTables();//加载数据库表名称
        });
    }
    return false;
}

function SetReportConnectionString() {
    var backitem = $("#SelectedConnectionString option:selected");
    $("#SelectedConnectionString option:selected").remove();
    $("#SelectedConnectionString").append($("#ConnectionStringList option:selected"));
    $("#ConnectionStringList").append(backitem);
}

function BackSetReportConnectionString() {
    $("#ConnectionStringList").append($("#SelectedConnectionString option:selected"));
}
