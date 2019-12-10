<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Template.aspx.cs" Inherits="BPM.ReportDesign.GenerateReportFiles.Template" %>

<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>$ReportName$</title>
    <link href="../CSS/css.css" rel="stylesheet" type="text/css" />
    <link href="../CSS/global.css" rel="stylesheet" type="text/css" />
    <script src="../ImportScript/jquery-1.7.1.min.js" type="text/javascript"></script>
    <link href="../ImportScript/My97DatePicker/My97DatePicker/skin/WdatePicker.css" rel="stylesheet"
        type="text/css" />
    <script src="../ImportScript/My97DatePicker/My97DatePicker/WdatePicker.js" type="text/javascript"></script>
    <script type="text/javascript" language="javascript">
        var actionRow, actionColor, actionFolat;
        function Report_Print() {
            var WebBrowser = '<OBJECT ID="WebBrowser1" WIDTH="0" HEIGHT="0" CLASSID="CLSID:8856F961-340A-11D0-A96B-00C04FD705A2"></OBJECT>';
            document.body.insertAdjacentHTML('beforeEnd', WebBrowser); //在body标签内加入html(WebBrowser activeX控件)
            WebBrowser1.ExecWB(7, 1); //打印预览
        }
        function SetColor(obj) {
            if (actionRow == null) {
                actionRow = obj;
                actionColor = obj.style.backgroundColor;
                actionFolat = obj.style.color;
                obj.style.backgroundColor = "#28C787";
                obj.style.color = "#FFFFFF";
            } else {
                actionRow.style.backgroundColor = actionColor;
                actionRow.style.color = actionFolat;
                actionRow = obj;
                actionColor = obj.style.backgroundColor;
                actionFolat = obj.style.color;
                obj.style.backgroundColor = "#28C787";
                obj.style.color = "#FFFFFF";
            }
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <div class="place">
            <div class="placeleft">
                <i></i>您现在的位置：<a href="#">首页</a> >> <strong><b>$ReportName$</b></strong>
            </div>
            <div class="globalBtn">
                <input type="image" src="../images/btn3.gif" style="cursor: pointer;"
                    onclick="Report_Print()" />
            </div>
        </div>
        <div class="listTable_1">
            <div class="tableh3">
                $ReportName$</div>
            <table border="0" cellpadding="0" cellspacing="0" class="listTable">
                $SelectWhereHead$ $SelectWhere$
                <tr>
                    <td colspan="3" style="text-align: center;">
                        $SearchButton$
                        <asp:Button ID="Button1" runat="server" Text="导出Excel" CssClass="btnBg" OnClick="ExportToExcel_Click" />
                    </td>
                </tr>
            </table>
            <div class="divTableList">
                <table border="1" cellpadding="3" cellspacing="0" style="width: $TableWidth$%; border-collapse: collapse;">
                    <tr style="background-color:#F2F4F6;text-align:Left;padding-top:1px;padding-bottom:1px;border-top-width:0px;border-left-width:0px;">
                        $TemplateTableHead$
                    </tr>
                    <tr>
                        <td colspan="$CellCount$">
                            <asp:Repeater ID="ReportList" runat="server">
                                <ItemTemplate>
                                    <tr onmouseover="currentcolor=this.style.backgroundColor;this.style.backgroundColor='#F3F3F3';" onmouseout="this.style.backgroundColor=currentcolor;" style='background-color:<%# (Container.ItemIndex + 1)%2==0?"#F9F9F9":"" %>;cursor: pointer;'>
                                        $RepeaterBingRow$
                                    </tr>
                                </ItemTemplate>
                            </asp:Repeater>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="$CellCount$" style="text-align: left;padding:5px;">
                            $whetherPaging$
                        </td>
                    </tr>
                </table>
            </div>
        </div>
    </div>
    </form>
</body>
</html>
