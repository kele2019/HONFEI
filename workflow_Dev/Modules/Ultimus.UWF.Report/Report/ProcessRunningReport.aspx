<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ProcessRunningReport.aspx.cs" Inherits="BPM.ReportDesign.Report.ProcessRunningReport" %>

<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>流程运行统计报表</title>
    <link href="css/reportCss.css" rel="stylesheet" type="text/css" />
    <link href="../../js/My97DatePicker/skin/WdatePicker.css"  rel="stylesheet" type="text/css" />
    <script src="../../js/My97DatePicker/WdatePicker.js"  type="text/javascript"></script>       
    <script src="script/js.js" type="text/javascript"></script>
</head>
<body>
    <form id="form1" runat="server">
    <div class="divHead">
        <h2>
            流程运行统计报表</h2>
    </div>
    <div>
        <div id="divCondition" runat="server" class="div_default_condition">
            <fieldset>
                <legend>查询条件</legend>
                <div class="div_default_field" style="width:320px">
                    <div>
                        <label style="white-space: nowrap; word-break: break-all;">
                            流程名称&nbsp;<span style="color:Red">*</span></label></div>
                    <div>
                        <asp:TextBox ID="txtProcessName" runat="server" CssClass="inputboder2" Width="150" />
                    </div>
                </div>
                <div class="div_default_field">
                    <div>
                        <label style="white-space: nowrap; word-break: break-all;">
                            开始时间</label></div>
                    <div>
                        <asp:TextBox ID="txtStartDate" runat="server" onclick="WdatePicker()" CssClass="inputboder2 Wdate"
                            ></asp:TextBox>
                    </div>
                </div>
                <div class="div_default_field">
                    <div>
                        <label style="white-space: nowrap; word-break: break-all;">
                            结束时间</label>
                    </div>
                    <div>
                        <asp:TextBox ID="txtEndDate" runat="server" onclick="WdatePicker()" CssClass="inputboder2 Wdate"
                             ></asp:TextBox>
                    </div>
                </div>
                <div class="div_default_field">
                    <asp:Button ID="btSearch" runat="server" CssClass="bluebuttoncss fr" Text="查询" 
                    OnClick="btSearch_Click" OnClientClick="return CheckValue();" />       
                    <script type="text/javascript">
                        function CheckValue() {                            
                            var oProcessName = document.getElementById("txtProcessName");
                            
                            if (oProcessName.value) {
                                return true;
                            } else {
                                alert("请将填写流程名称！！");
                                return false;
                            }
                        }
                    </script>              
                    </div>
            </fieldset>
        </div>
        <div>
            <table width="99.5%" cellspacing="1" cellpadding="5" style="margin-left: 2px" border="1"
                bordercolor="#ccc">
                <tr>
                    <th style="width: 5%">
                        序号
                    </th>
                    <th style="width: 5%">
                        流程名称
                    </th>
                    <th style="width: 5%">
                        实例数
                    </th>
                    <th style="width: 5%">
                        最长耗时
                    </th>
                    <th style="width: 5%">
                        最短耗时
                    </th>
                    <th style="width: 5%">
                        平均耗时
                    </th>
                    <th style="width: 5%">
                      操作
                    </th>
                </tr>
                <tbody>
                    <asp:Repeater ID="ReportList" runat="server">
                        <ItemTemplate>
                            <tr onmouseover="currentcolor=this.style.backgroundColor;this.style.backgroundColor='#F3F3F3';"
                                onmouseout="this.style.backgroundColor=currentcolor;" style='background-color: <%# (Container.ItemIndex + 1)%2==0?"#F9F9F9":"" %>;
                                cursor: pointer;'>
                                <td>
                                    <%# Container.ItemIndex + 1%>
                                </td>
                                <td>
                                    <%# Eval("ProcessName")%>
                                </td>
                                <td>
                                    <%# Eval("allIncident")%>
                                </td>
                                <td>
                                    <%#getTimeFromMi(Eval("maxSec").ToString())%>
                                </td>
                                <td>
                                    <%#getTimeFromMi(Eval("minSec").ToString())%>
                                </td>
                                <td>
                                   <%#getTimeFromMi(Eval("avgSec").ToString())%>
                                </td>
                                <td>
                                    <a href='javascript:' onclick="showTheDetail('<%# Eval("ProcessName").ToString().Trim()%>')">查看</a>
                                </td>
                            </tr>
                        </ItemTemplate>
                    </asp:Repeater>
                </tbody>     
                 <script type="text/javascript">
                     function showTheDetail(processname) {
                         var url = 'ProcessRunningEff.aspx?view=1&ProcessName=' + encodeURIComponent(processname);
                         var startdate = document.getElementById("txtStartDate");
                         var enddate = document.getElementById("txtEndDate");
                         if (startdate.value) {
                             url += "&startdate="+startdate.value;
                         }
                         if (enddate.value) {
                             url += "&enddate="+enddate.value;
                         }
                         // showDialog(url, 1180, 600);
                         showDialog(url);
                     }
                 </script>
                <tr>
                    <td colspan="7" style="text-align:right" align="right">
                    <span>共计<asp:Label ID="lbTotal" runat="server" Text="0"></asp:Label>条记录</span>
                        <webdiyer:AspNetPager ID="pagination" runat="server" Width="100%" EnableTheming="true"
                            CustomInfoTextAlign="Left" HorizontalAlign="Right" NextPageText="下一页" PrevPageText="上一页"
                            FirstPageText="首页" LastPageText="末页" OnPageChanging="pagination_PageChanging"
                            PageIndexBoxType="DropDownList" ShowPageIndexBox="Always" TextBeforePageIndexBox="Go"
                            PageSize="24">
                        </webdiyer:AspNetPager>
                    </td>
                </tr>
            </table>
        </div>
    </div>
    </form>
</body>
</html>
