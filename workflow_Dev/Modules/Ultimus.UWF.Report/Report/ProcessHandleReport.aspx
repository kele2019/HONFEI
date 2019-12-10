<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ProcessHandleReport.aspx.cs"
    Inherits="BPM.ReportDesign.Report.ProcessHandleReport" %>

<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>流程办理统计表</title>
    <link href="css/reportCss.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="/bpm/selector_sapi.axd"></script>
    <script src="script/js.js" type="text/javascript"></script>
    <script type="text/javascript">
        window.onload = function () {
            var oPersonOrJob = document.getElementById("txtPerson");
            var oHide = document.getElementById("hideType");
            oPersonOrJob.onfocus = function () {
                var arr = getUserOrJob(1);
                if (arr[0]) {
                    this.value = arr[0];
                    this.title = arr[0];
                    oHide.value = arr[1];
                }
            }

        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div class="divHead">
        <h2>
            流程办理统计表</h2>
    </div>
    <div id="divCondition" runat="server" class="div_default_condition">
        <fieldset>
            <legend>查询条件</legend>
            <div class="div_default_field" style="width: 320px">
                <div>
                    操作人/岗位&nbsp;<span style="color: Red">*</span></div>
                <div>
                    <input type="text" name='' runat="server" id="txtPerson" style="width: 200px" title="" />
                    <asp:HiddenField ID="hideType" runat="server" />
                </div>
            </div>
            <div class="div_default_field">
                <div>
                    流程名称&nbsp;</div>
                <div>
                    <input type="text" name='' runat="server" id="txtProcessName" style="width: 150px" /></div>
            </div>
            <div class="div_default_field">
                <asp:Button ID="btnSearch" runat="server" CssClass="bluebuttoncss fr" Text="查 询"
                    OnClientClick="return checkInfo()" OnClick="btnSearch_Click" /></div>
            <script type="text/javascript">
                function checkInfo() {
                    var oProcessName = document.getElementById("txtPerson");
                    //var oIncident = document.getElementById("txtPerson");
                    if (oProcessName.value) {
                        return true;
                    } else {
                        alert("请将操作人填写完整");
                        return false;
                    }
                }
            </script>
        </fieldset>
    </div>
    <div>
        <table width="99.5%" cellspacing="1" cellpadding="5" style="margin-left: 2px" border="1"
            bordercolor="#ccc">
            <tr>
                <th style="width: 5%">
                    序号
                </th>
                <th style="width: 20%">
                    流程名称
                </th>
                <th style="width: 20%">
                    节点名称
                </th>
                <th style="width: 10%">
                    时限要求
                </th>
                <th style="width: 10%">
                    平均耗时
                </th>
                <th style="width: 10%">
                    最长耗时
                </th>
                <th style="width: 10%">
                    最短耗时
                </th>
                <th style="width: 15%">
                    操作人/岗位
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
                                <%#Eval("STEPLABEL").ToString()%>
                            </td>
                            <td>
                                <%#getTime(Eval("oversecond").ToString())%>
                            </td>
                            <td>
                                <%#getTime(Eval("avgSecond").ToString ())%>
                            </td>
                            <td>
                                <%#getTime(Eval("maxSecond").ToString ())%>
                            </td>
                            <td>
                                <%#getTime(Eval("minSecond").ToString())%>
                            </td>
                            <td>
                                <%#Eval("userjob")%>
                            </td>
                        </tr>
                    </ItemTemplate>
                </asp:Repeater>
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
