<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ReportIndex.aspx.cs" Inherits="BPM.ReportDesign.ReportIndex" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>报表设计器</title>
    <link href="CSS/css.css" rel="stylesheet" type="text/css" />
    <link href="CSS/global.css" rel="stylesheet" type="text/css" />
    <script src="ImportScript/jquery-1.7.1.min.js" type="text/javascript"></script>
    <link href="../App_Themes/Default/CSS/validationEngine.jquery.css" rel="stylesheet" type="text/css" />
    <link href="../App_Themes/Default/CSS/Jquery/jquery-ui-1.8.17.custom.css" rel="stylesheet" type="text/css" />
    <script src="ImportScript/jquery.validationEngine-zh_CN.js" type="text/javascript"></script>
    <script src="ImportScript/jquery.validationEngine.js" type="text/javascript"></script>
    <script src="SysScript/TryConnection.js" type="text/javascript"></script>
    <script src="SysScript/AddTablesAndField.js" type="text/javascript"></script>
    <script src="SysScript/SaveReport.js" type="text/javascript"></script>
    <script src="SysScript/SelectDataBaseTables.js" type="text/javascript"></script>
    <script src="SysScript/SetFieldName.js" type="text/javascript"></script>
    <script src="SysScript/SetOther.js" type="text/javascript"></script>
    <script src="SysScript/SetSelectWhere.js" type="text/javascript"></script>
    <script src="SysScript/TableFieldRelation.js" type="text/javascript"></script>
    <script>
        
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div class="place">
        <div class="placeleft">
            <i></i>您现在的位置：<a href="#">首页</a> >> <strong><b>报表设计器</b></strong></div>
        <div class="listTable_1">
            <div class="tableh3">
                报表设计器</div>
            <div id="step1">
                <table id="step1tab" border="0" cellpadding="0" cellspacing="0" class="listTable">
                    <tr>
                        <td colspan="3">
                            <h3 class="titleBG">
                                设置连接</h3>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align:right;width:10%;">
                            <input type="button" value="测试连接" class="btnBg" onclick="TryConnectionDataBase()" />
                            <asp:HiddenField ID="ConnectionSucceed" runat="server" />
                        </td>
                        <td></td>
                        <td style="text-align:left;">
                            <input type="button" value="下一步" class="btnBg" onclick="NextSelectTableName()" />
                            <asp:HiddenField ID="ConnectionStringHidden" runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align:right;">
                            <select id="ConnectionStringList" style="width:150px;" size="15" ondblclick="SetReportConnectionString()">
                                
                            </select>
                        </td>
                        <td style="width:60px;">
                            <div style="width: 100%; overflow-x: hidden; float: left;">
                                <input type="button" value=">" class="btnBg" style="width: 100%;" onclick="SetReportConnectionString()" />
                            </div>
                            <div style="width: 100%; overflow-x: hidden; float: left; height: 20px;">
                            </div>
                            <div style="width: 100%; overflow-x: hidden; float: left;">
                                <input type="button" value="<" class="btnBg" style="width: 100%;" onclick="BackSetReportConnectionString()" />
                            </div>
                        </td>
                        <td style="text-align:left;">
                            <select id="SelectedConnectionString" style="width:150px;" size="15" ondblclick="BackSetReportConnectionString()">
                                
                            </select>
                        </td>
                    </tr>
                    
                </table>
            </div>
            <div id="step2" style="display: none;">
                <table id="step2tab" border="0" cellpadding="0" cellspacing="0" class="listTable">
                    <tr>
                        <td colspan="3">
                            <h3 class="titleBG">
                                数据表选择</h3>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="3">
                            流程名称:<select id="dropProcessNames"></select>
                            表名称:<input id="tableName" class="inputboder2" style="width:150px;"/>
                            <asp:HiddenField ID="hfProcessName" runat="server"/>
                            <asp:HiddenField ID="hfTableName" runat="server"/>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: right;">
                            <input type="button" class="btnBg" id="BackSelectConnectionString" value="上一步" onclick="BackStep1()" />
                        </td>
                        <td>
                        </td>
                        <td style="text-align: left;">
                            <input type="button" class="btnBg" value="下一步" onclick="NextTableRelation()" />
                            <asp:HiddenField ID="SelectTablesHidden" runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: right;width:20%;" align="right">
                            <select id="DataBaseTables" multiple size="20" style="width: 300px;" ondblclick="DataBaseTablesSelected()">
                            </select>
                        </td>
                        <td width="5%">
                            <div style="width: 100%; overflow-x: hidden; float: left;">
                                <input type="button" value=">" class="btnBg" style="width: 100%;" onclick="DataBaseTablesSelected()" />
                            </div>
                            <div style="width: 100%; overflow-x: hidden; float: left; height: 20px;">
                            </div>
                            <div style="width: 100%; overflow-x: hidden; float: left;">
                                <input type="button" value=">>>" class="btnBg" style="width: 100%;" onclick="DataBaseTablesSelectedAll()" />
                            </div>
                            <div style="width: 100%; overflow-x: hidden; float: left; height: 20px;">
                            </div>
                            <div style="width: 100%; overflow-x: hidden; float: left;">
                                <input type="button" value="<" class="btnBg" style="width: 100%;" onclick="SelectTablesSelected()" />
                            </div>
                            <div style="width: 100%; overflow-x: hidden; float: left; height: 20px;">
                            </div>
                            <div style="width: 100%; overflow-x: hidden; float: left;">
                                <input type="button" value="<<<" class="btnBg" style="width: 100%;" onclick="SelectTablesSelectedAll()" />
                            </div>
                        </td>
                        <td style="text-align: left;" align="left">
                            <select id="SelectTables" multiple size="20" style="width: 300px;" ondblclick="SelectTablesSelected()">
                            </select>
                        </td>
                    </tr>
                </table>
            </div>
            <div id="step3" style="display: none;">
                <table id="step3tab" border="0" cellpadding="0" cellspacing="0" class="listTable">
                    <tr>
                        <td colspan="4">
                            <h3 class="titleBG">
                                字段设置</h3>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="4">
                            <input type="button" class="btnBg" value="上一步" onclick="BackStep2()" />
                            <input type="button" class="btnBg" value="下一步" onclick="NextSetTableRelation()" />
                            <asp:HiddenField ID="SelectTablesAndFieldHidden" runat="server"/>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="4">
                            <div id="tableInformationList" style="width: 100%; float: left; height: 450px; overflow-y: scroll;
                                text-align: center; border: 1px #cccccc solid;">
                            </div>
                        </td>
                    </tr>
                </table>
            </div>
            <div id="step4" style="display: none;">
                <table id="step4tab" border="0" cellpadding="0" cellspacing="0" class="listTable" width="100%">
                    <tr>
                        <td colspan="4">
                            <h3 class="titleBG">
                                表关系关系设置</h3>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            <input type="button" value="上一步" class="btnBg" onclick="BackSetTablesField()"/>
                            <input type="button" value="下一步" class="btnBg" onclick="NextSetSelectWhere()"/>
                            <asp:HiddenField ID="Table_Field_RelationHidden" runat="server"/>
                            <asp:HiddenField ID="Table_Field_RulesHidden" runat="server"/>
                            <input id="AddTableRelation" type="button" disabled="disabled" value="添加对应关系" class="btnBg" onclick="AddTableRelations()"/>
                        </td>
                        <td colspan="2">
                            <input type="button" value="添加字段规则" class="btnBg" onclick="AddField()"/>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2" width="60%">
                            <div id="" style="border:1px #CCCCCC solid;">
                                <table border="0" cellpadding="0" cellspacing="0" class="listTable3" style="width:97.5%;">
                                    <tr class="listTablebg">
                                        <th width="20px" style="text-align:center;"></th>
                                        <th width="150px" style="text-align:center;">表名</th>
                                        <th width="50px" style="text-align:center;">对应关系</th>
                                        <th width="150px" style="text-align:center;">表名</th>
                                        <th width="70px" style="text-align:center;">字段</th>
                                        <th width="30px" style="text-align:center;">操作</th>
                                    </tr>
                                </table>
                                <div style="overflow-y:scroll;height:200px;">
                                    <table id="TableRelation_List" border="0" cellpadding="0" cellspacing="0" class="listTable3" style="width:100%;">
                                        
                                    </table>
                                </div>
                            </div>
                        </td>
                        <td colspan="2" width="40%">
                            <div id="" style="border: 1px #CCCCCC solid;">
                                <table border="0" cellpadding="0" cellspacing="0" class="listTable3" style="width:96.2%;">
                                    <tr class="listTablebg">
                                        <th width="150px" style="text-align:center;">表名</th>
                                        <th width="150px" style="text-align:center;">字段名</th>
                                        <th width="100px" style="text-align:center;">规则</th>
                                        <th width="30px" style="text-align:center;">操作</th>
                                    </tr>
                                </table>
                            </div>
                            <div style="overflow-y: scroll; height: 200px;">
                                <table id="Field_List" border="0" cellpadding="0" cellspacing="0" class="listTable3" style="width: 100%;">
                                    
                                </table>
                            </div>
                        </td>
                    </tr>
                </table>
            </div>
            <div id="step5" style="display: none;">
                <table id="step5tab" border="0" cellpadding="0" cellspacing="0" class="listTable" style="width:100%;">
                    <tr>
                        <td colspan="4">
                            <h3 class="titleBG">
                                报表查询条件设置</h3>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="4">
                            <div style="color:Red;">
                                起始结束日期查询选择后系统会自动生成开始和结束时间的查询条件
                            </div>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="4">
                            <input type="button" class="btnBg" value="上一步" onclick="BackTableRelation()"/>
                            <input type="button" class="btnBg" value="下一步" onclick="NextSetFieldName()"/>
                            <asp:HiddenField ID="SelectWhereHidden" runat="server"/>
                            <input type="button" value="添加条件" class="btnBg" onclick="AddSelectWhere()"/>
                            <asp:HiddenField ID="SelectTablesWhereHidden" runat="server"/>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="4">
                            <div style="border:1px #cccccc solid;">
                                <table border="0" cellpadding="0" cellspacing="0" class="listTable3" style="width:100%;">
                                    <thead>
                                        <tr class="listTablebg">
                                            <th width="5%" style="text-align:center;white-space:nowrap;word-break:break-all;">序号</th>
                                            <th width="20%" style="text-align:center;white-space:nowrap;word-break:break-all;">表名</th>
                                            <th width="20%" style="text-align:center;white-space:nowrap;word-break:break-all;">字段名</th>
                                            <th width="20%" style="text-align:center;white-space:nowrap;word-break:break-all;">显示列名</th>
                                            <th width="15%" style="text-align:center;white-space:nowrap;word-break:break-all;">是否模糊查询</th>
                                            <th width="20%" style="text-align:center;white-space:nowrap;word-break:break-all;">起始结束日期查询</th>
                                            <th width="15%" style="text-align:center;white-space:nowrap;word-break:break-all;">范围查询</th>
                                            <th width="10%" style="text-align:center;white-space:nowrap;word-break:break-all;">操作</th>
                                        </tr>
                                    </thead>
                                    <tbody id="SelectWhereList">
                                        
                                    </tbody>
                                </table>
                            </div>
                        </td>
                    </tr>
                </table>
            </div>
            <div id="step6" style="display: none;">
                <table id="step6tab" border="0" cellpadding="0" cellspacing="0" class="listTable" style="width:100%;">
                    <tr>
                        <td colspan="4">
                            <h3 class="titleBG">
                                报表显示字段名称设置</h3>
                        </td>
                    </tr>
                     <tr>
                        <td colspan="4">
                            <div style="color:Red;">
                                如果需要打开其他页面，请添加外部连接，格式为：http://10.10.0.131/bpm/popup/TaskForm.aspx?processname={流程名称}&incidentno={实例号}
                                系统将自动在URL中加入添加外部接连的参数，如在a字段加了外部连接，系统生成的格式为：http://10.10.0.131/bpm/popup/TaskForm.aspx?a=a字段的值
                                若需要自定义参数格式为：http://10.10.0.131/bpm/popup/TaskForm.aspx?date={数据库的列名(如果设置了显示字段名，请改为显示字段名)}
                            </div>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="4">
                            <input type="button" class="btnBg" value="上一步" onclick="BackSetSelectWhere()"/>
                            <input type="button" class="btnBg" value="下一步" onclick="NextSetOther()"/>
                            <asp:HiddenField ID="SetSilasHidden" runat="server"/>

                            <input id="TakeDatabaseListing" type="checkbox" checked="checked" value="取数据库列名" onclick="LoadTablesField(this)" name="AliasCheckBox"/>
                            <label for="TakeDatabaseListing" style="text-align:left;cursor:pointer;">取数据库列名</label>

                            <input id="TakeTheDatabaseRemarkColumn" type="checkbox" value="取数据库备注列" onclick="LoadTablesField(this)" name="AliasCheckBox"/>
                            <label for="TakeTheDatabaseRemarkColumn" style="text-align:left;cursor:pointer;">取数据库备注列</label>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="4">
                            <div style="border:1px #cccccc solid;">
                                <table border="0" cellpadding="0" cellspacing="0" class="listTable3" style="width:98.7%;">
                                    <tr class="listTablebg">
                                        <th width="20px" style="text-align:center;white-space:nowrap;word-break:break-all;">序号</th>
                                        <th width="150px" style="text-align:center;white-space:nowrap;word-break:break-all;">表名</th>
                                        <th width="150px" style="text-align:center;white-space:nowrap;word-break:break-all;">字段名</th>
                                        <th width="100px" style="text-align:center;white-space:nowrap;word-break:break-all;">显示列名</th>
                                        <th width="200px" style="text-align:center;white-space:nowrap;word-break:break-all;">外部链接</th>
                                        <th width="100px" style="text-align:center;white-space:nowrap;word-break:break-all;">隐藏列</th>
                                        <th width="100px" style="text-align:center;white-space:nowrap;word-break:break-all;">列宽(%)</th>
                                        <th width="100px" style="text-align:center;white-space:nowrap;word-break:break-all;">显示顺序</th>
                                    </tr>
                                </table>
                                <div style="overflow-y:scroll;height:320px;">
                                    <table id="AliasList" border="0" cellpadding="0" cellspacing="0" class="listTable3" style="width:100%;">
                                    </table>
                                </div>
                            </div>
                        </td>
                    </tr>
                </table>
            </div>
            <div id="step7" style="display: none;">
                <table id="step7tab" border="0" cellpadding="0" cellspacing="0" class="listTable" style="width:100%;">
                    <tr>
                        <td colspan="4">
                            <h3 class="titleBG">
                                其他设置</h3>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <label>报表名称</label>
                            <input class="inputboder2 validate[required]" id="ReportName"/>
                        </td>
                        <td width="25%">
                            <input type="checkbox" id="WhetherPaging" style="cursor:pointer;" onclick="OpeningPage(this)"/>
                            <label for="WhetherPaging" style="margin:0px;padding:0px;width:50px;cursor:pointer;">启用分页</label>
                            <label class="OpeningPageClass" style="display:none;">每页显示条数</label>
                            <input id="ArticleThatNumber" class="inputboder2 OpeningPageClass" style="width:30px;display:none;"/>
                        </td>
                        <td colspan="2">
                            <label>排序规则</label>
                            <select id="SortTableName" style="width:150px;" onchange="GetOtherTableField(this,'SortFieldName')">
                            </select>
                            <select id="SortFieldName" style="width:150px;">
                            </select>
                            <select id="SortType" style="width:50px;">
                                <option value="ASC">升序</option>
                                <option value="DESC">倒序</option>
                            </select>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="4">
                            <input id="checkbox2" type="checkbox" name="viewreport" onclick="ShowViewRow(1)" value="1" style="cursor:pointer;" checked="checked"/>
                            <label for="checkbox2" style="cursor:pointer;white-space:nowrap;word-break:break-all;width:150px;">匹配当前用户及下属数据</label>
                            &nbsp;&nbsp;
                            <input id="checkbox1" type="checkbox" name="viewreport" onclick="ShowViewRow(0)" value="0" style="cursor:pointer;"/>
                            <label for="checkbox1" style="cursor:pointer;white-space:nowrap;word-break:break-all;">所有人可以查看</label>
                            &nbsp;&nbsp;
                            <input id="checkbox3" type="checkbox" name="viewreport" onclick="ShowViewRow(2)" value="2" style="cursor:pointer;display:none"/>
                            <label for="checkbox3" style="cursor:pointer;white-space:nowrap;word-break:break-all;display:none">指定人可以查看</label>
                        </td>
                    </tr>
                    <tr id="SelectViewRow" style="display:block;">
                        <td colspan="4" style="white-space:nowrap;word-break:break-all;">
                            人员帐号字段:
                            <select id="ViewUserTableName" style="width:150px;" onchange="GetOtherTableField(this,'ViewUserFieldName')">
                            </select>
                            <select id="ViewUserFieldName" style="width:150px;">
                            </select>
                            <asp:HiddenField runat="server" ID="ViewUserTableNameHidden"/>
                            <asp:HiddenField runat="server" ID="ViewUserFieldNameHidden"/>
                        </td>
                    </tr>
                    <tr id="selectuserrow" style="display:none;">
                        <td colspan="4" style="white-space:nowrap;word-break:break-all;">
                            <input type="button" class="btnBg" id="viewreportbutton" value="人员选择" onclick="getPersons('', null, true)"/>
                            <textarea class="inputboder2" style="width:85%;height:100px;" id="viewitems"></textarea>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="4" style="white-space:nowrap;word-break:break-all;">
                            <input type="button" class="btnBg" value="上一步" onclick="BackSetFieldName()"/>
                            <input id="SaveReportButton" type="button" class="btnBg" value="生成报表" onclick="NextSaveReport()"/>
                            <asp:HiddenField ID="AlterGuid" runat="server"/>
                            <asp:HiddenField ID="AuthorityHidden" runat="server"/>
                            <asp:HiddenField ID="SortStyleHidden" runat="server"/>
                        </td>
                    </tr>
                    
                </table>
            </div>
        </div>
    </div>
    </form>
</body>
</html>
