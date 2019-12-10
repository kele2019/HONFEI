function SaveReport() {
    $.post("AjaxPage/GenerateReports.ashx", { 'connectionString': $("#ConnectionStringHidden").val(), 'tablesAndFields': $("#SelectTablesAndFieldHidden").val(), 'tableRelation': $("#Table_Field_RelationHidden").val(), 'fieldRules': $("#Table_Field_RulesHidden").val(), 'selectWhere': $("#SelectWhereHidden").val(), 'whetherPaging': ($("#WhetherPaging").attr("checked") == "checked" ? "Y" : "N"), 'articleThatNumber': $("#ArticleThatNumber").val(), 'alias': $("#SetSilasHidden").val(), 'SortStyle': $("#SortStyleHidden").val(), 'reportName': $("#ReportName").val(), 'isAlter': (isAlter == false ? "N" : "Y"), 'AlterGuid': $("#AlterGuid").val(), 'ViewType': $("[name='viewreport'][checked]:checkbox").val(), 'ViewUser': $("#AuthorityHidden").val(), 'ViewUserName': $("#viewitems").val(), 'UserAccount': $("#ViewUserTableName option:selected").val() + "." + $("#ViewUserFieldName option:selected").val(), 'tableName': $("#tableName").val(), 'processName': $("#dropProcessNames option:selected").val() }, function (date) {
        if (date == "") {
            if (isAlter) {
                alert("报表更新成功.");
            } else {
                alert("报表生成成功.");
            }
            location.href = "ReportList.aspx";
            window.close();
        } else {
            alert(date);
        }
    });
}
