<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ReportList.aspx.cs" Inherits="BPM.ReportDesign.ReportList" %>
<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>报表列表</title>
    <link href="CSS/css.css" rel="stylesheet" type="text/css" />
    <link href="CSS/global.css" rel="stylesheet" type="text/css" />
    <script src="jquery-1.7.1.min.js" type="text/javascript"></script>
    <link href="/bpm/App_Themes/Default/CSS/validationEngine.jquery.css" rel="stylesheet" type="text/css" />
    <link href="/bpm/App_Themes/Default/CSS/Jquery/jquery-ui-1.8.17.custom.css" rel="stylesheet" type="text/css" />
    <script src="jquery-ui-1.8.17.custom.min.js" type="text/javascript"></script>
    <script src="jquery.validationEngine.js" type="text/javascript"></script>
    <script src="jquery.validationEngine-zh_CN.js" type="text/javascript"></script>
    <script src="ImportScript/My97DatePicker/My97DatePicker/WdatePicker.js" type="text/javascript"></script>
    <script type="text/javascript" language="javascript">
        function SelectAll(obj) {
            if ($(obj).attr("checked")) {
                $("[name='DelCheckBox']:checkbox").attr("checked", "checked");
            } else {
                $("[name='DelCheckBox']:checkbox").removeAttr("checked");
            }
        }
        
        function DelTheReports() {
            if (jQuery("#tab").validationEngine("validate")) {
                var guids = "";
                $("#tab tr[class='dateRow']").each(function () {
                    if ($(this).find("td:eq(0)").children().attr("checked")) {
                        guids += "'" + $(this).find("td:eq(0)").children().val() + "',";
                    }
                });
                if (guids != "") {
                    guids = guids.substring(0, guids.lastIndexOf(","));
                }
                $("#Del_Guids").val(guids);
                return true;
            } else {
                $("#Del_Guids").val("");
                return false;
            }
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div class="place">
        <div class="placeleft">
            <i></i>您现在的位置：<a href="#">首页</a> >> <strong><b>报表列表</b></strong></div>
    </div>
    <div class="listTable_1">
        <div class="tableh3">
            报表列表</div>
        <table id="tab" border="1" style="border-collapse:separate" cellpadding="0" cellspacing="0" class="listTable3">
            <tr>
                <td colspan="5">
                    <input type="button" value="新建报表" class="btnBg" onclick="javascript:location.href = 'ReportIndex.aspx';" />
                    <asp:Button ID="DelReports" runat="server" Text="删除报表" CssClass="btnBg" OnClientClick="return DelTheReports()"
                        OnClick="DelReports_Click" />
                    <asp:HiddenField ID="Del_Guids" runat="server" />
                </td>
            </tr>
            <tr>
                <td colspan="2">
                    <label>报表名称:</label>
                    <asp:TextBox ID="ReportName" runat="server" CssClass="inputboder2"></asp:TextBox>
                </td>
                <td colspan="3">
                    <label>创建时间:</label>
                    <asp:TextBox ID="BeginDate" runat="server" onclick="WdatePicker()" CssClass="inputboder2" Width="120px"></asp:TextBox>
                    -
                    <asp:TextBox ID="EndDate" runat="server" onclick="WdatePicker()" CssClass="inputboder2" Width="120px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Button ID="Button1" runat="server" Text="查询" CssClass="btnBg" 
                        onclick="Button1_Click"/>
                </td>
                <td colspan="4">
                    <label>更新时间:</label>
                    <asp:TextBox ID="UpdateBeginDate" runat="server" onclick="WdatePicker()" CssClass="inputboder2" Width="120px"></asp:TextBox>
                    -
                    <asp:TextBox ID="UpdateEndDate" runat="server" onclick="WdatePicker()" CssClass="inputboder2" Width="120px"></asp:TextBox>
                </td>
            </tr>
            <tr class="listTablebg">
                <th style="width: 10px;">
                    <input type="checkbox" onclick="SelectAll(this)" />
                </th>
                <th>
                    报表名称
                </th>
                <%--<th>
                    创建人
                </th>--%>
                <th>
                    创建时间
                </th>
                <%--<th>
                    最后更新人
                </th>--%>
                <th>
                    最后更新时间
                </th>
                <th style="width:30px;">
                    操作
                </th>
            </tr>
            <asp:Repeater ID="ReportsList" runat="server">
                <ItemTemplate>
                    <tr class="dateRow">
                        <td>
                            <input type="checkbox" id="checkbox" name="DelCheckBox" class="validate[minCheckbox[1]]" value='<%# Eval("guid") %>'/>
                        </td>
                        <td>
                            <a style="color: Blue;" href="GenerateReportFiles/<%# Eval("REPORTPAGEPATH") %>">
                                <%# Eval("REPORTNAME")%></a>
                        </td>
                        <%--<td>
                            <%# Eval("USERNAME")%>
                        </td>--%>
                        <td>
                            <%# Eval("CREATDATE")%>
                        </td>
                        <%--<td>
                            <%# Eval("UPDATEUSERNAME")%>
                        </td>--%>
                        <td>
                            <%# Eval("UPDATEDATE")%>
                        </td>
                        <td>
                            <a style="color: Blue;" href="ReportIndex.aspx?Guid=<%# Eval("GUID") %>">修改</a>
                        </td>
                    </tr>
                </ItemTemplate>
            </asp:Repeater>
            <tr>
                <td colspan="7" style="text-align: right;">
                    <webdiyer:aspnetpager id="AspNetPager1" runat="server" custominfohtml="Count %RecordCount%"
                        horizontalalign="right" width="100%" cssclass="aspNetPager" onpagechanged="AspNetPager1_PageChanged"
                        pagesize="20" alwaysshow="true" submitbuttonstyle="display:none" inputboxstyle="display:none"
                        nextpagetext="下一页" firstpagetext="首页" lastpagetext="末页" prevpagetext="上一页">
                    </webdiyer:aspnetpager>
                </td>
            </tr>
        </table>
    </div>
    <div style="display: none;">
        <asp:LinkButton ID="LinkButton1" runat="server" OnClick="LinkButton1_Click">LoadForm</asp:LinkButton>
    </div>
    </form>
</body>
</html>
