<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ProcessRunningEff.aspx.cs"
    Inherits="BPM.ReportDesign.Report.ProcessRunningEff" %>

<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>流程运行效率统计表</title>
    <link href="css/reportCss.css" rel="stylesheet" type="text/css" />
    <link href="../../js/My97DatePicker/skin/WdatePicker.css" rel="stylesheet" type="text/css" />
    <script src="../../js/My97DatePicker/WdatePicker.js" type="text/javascript"></script>
    <script src="script/js.js" type="text/javascript"></script>
</head>
<body>
    <form id="form1" runat="server">
    <div class="divHead">
        <h2>
            流程运行效率统计表</h2>
    </div>
    <div id="divCondition" runat="server" class="div_default_condition">
        <fieldset>
            <legend>查询条件</legend>
            <div class="div_default_field" style="width: 280px">
                <div>
                    流程名称&nbsp;<span style="color: Red">*</span></div>
                <div>
                    <input type="text" name='' runat="server" id="txtProcessName" style="width: 150px" /></div>
            </div>
            <div class="div_default_field">
                <div>
                    开始时间&nbsp;</div>
                <div>
                    <input type="text" name='' runat="server" id="txtStartTime" class="Wdate" onclick="WdatePicker()" /></div>
            </div>
            <div class="div_default_field">
                <div>
                    结束时间&nbsp;</div>
                <div>
                    <input type="text" name='' runat="server" id="txtEndTime" class="Wdate" onclick="WdatePicker()" /></div>
            </div>
            <div class="div_default_field" style="width: 100px">
                <asp:Button ID="btnSearch" runat="server" CssClass="bluebuttoncss fr" Text="查 询"
                    OnClick="btnSearch_Click" /></div>
        </fieldset>
    </div>
    <div>
        <table width="99.5%" cellspacing="1" cellpadding="5" style="margin-left: 2px" border="1"
            bordercolor="#ccc">
            <tr>
                <th style="width: 5%">
                    序号
                </th>
                <th style="width: 25%">
                    流程名称
                </th>
                <th style="width: 5%">
                    实例号
                </th>
                <th style="width: 15%">
                    总共耗时
                </th>
                <th style="width: 25%">
                    最长耗时节点
                </th>
                <th style="width: 15%">
                    最长耗时
                </th>
                <th style="width: 10%">
                    操作
                </th>
            </tr>
            <tbody>
                <asp:Repeater ID="rpSource" runat="server">
                    <ItemTemplate>
                        <tr onmouseover="currentcolor=this.style.backgroundColor;this.style.backgroundColor='#F3F3F3';"
                            onmouseout="this.style.backgroundColor=currentcolor;" style='background-color: <%# (Container.ItemIndex + 1)%2==0?"#F9F9F9":"" %>;
                            cursor: pointer;'>
                            <td>
                                <%#Container.ItemIndex+1 %>
                            </td>
                            <td>
                                <%#Eval("PROCESSNAME")%>
                            </td>
                            <td>
                                <%#Eval("INCIDENT")%>
                            </td>
                            <td>
                                <%#getTime(Eval("totleSeconds").ToString ())%>
                            </td>
                            <td>
                                <%#Eval("longStep")%>
                            </td>
                            <td>
                                <%#getTime(Eval("longSec").ToString ())%>
                            </td>
                            <td>
                                <a href='javascript:' onclick="showTheDetail('<%# Eval("ProcessName").ToString().Trim()%>','<%#Eval("INCIDENT")%>')">
                                    查看</a>
                            </td>
                        </tr>
                    </ItemTemplate>
                </asp:Repeater>
                <script type="text/javascript">
                    function showTheDetail(processname, incdient) {
                        var url = 'ProcessEfficiency.aspx?view=1&ProcessName=' + encodeURIComponent(processname) + "&incident=" + incdient;
                        showDialog(url, 980, 500);
                    }
                </script>
                <tr>
                    <td colspan="8" style="text-align: right" align="right">
                        <span>共计<asp:Label ID="lbCount" runat="server" Text="0"></asp:Label>条记录</span>
                        <webdiyer:AspNetPager ID="pagination" runat="server" Width="100%" EnableTheming="true"
                            CustomInfoTextAlign="Left" HorizontalAlign="Right" NextPageText="下一页" PrevPageText="上一页"
                            FirstPageText="首页" LastPageText="末页" OnPageChanging="pagination_PageChanging"
                            PageIndexBoxType="DropDownList" ShowPageIndexBox="Always" TextBeforePageIndexBox="Go"
                            PageSize="24">
                        </webdiyer:AspNetPager>
                    </td>
                </tr>
            </tbody>
        </table>
    </div>
    </form>
</body>
</html>
