<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ProcessEfficiency.aspx.cs"
    Inherits="BPM.ReportDesign.Report.ProcessEfficiency" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>流程实例效率统计报表</title>
    <link href="css/reportCss.css" rel="stylesheet" type="text/css" />
    <script src="script/js.js" type="text/javascript"></script>
    <script>
        window.onload = function () {
            var oBtn = document.getElementById("btnSearch");

        }
        function checkInfo() {
            var oProcessName = document.getElementById("txtProcessName");
            var oIncident = document.getElementById("txtIncident");
            if (oProcessName.value && oIncident.value) {
                return true;
            } else {
                alert("请将查询条件填写完整");
                return false;
            }
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div class="divHead">
        <h2>
            流程实例效率统计报表</h2>
    </div>
    <div id="divCondition" runat="server" class="div_default_condition">
        <fieldset>
            <legend>查询条件</legend>
            <div class="div_default_field">
                <div>
                    流程名称&nbsp;<span style="color: Red">*</span></div>
                <div>
                    <input type="text" name='' runat="server" id="txtProcessName" /></div>
            </div>
            <div class="div_default_field">
                <div>
                    实例号&nbsp;<span style="color: Red">*</span></div>
                <div>
                    <input type="text" name='' runat="server" id="txtIncident" /></div>
            </div>
            <div class="div_default_field">
                <asp:Button ID="btnSearch" runat="server" CssClass="bluebuttoncss fr" Text="查 询"
                    OnClick="btnSearch_Click" OnClientClick="return checkInfo()" /></div>
        </fieldset>
    </div>
    
    <div>
        <table width="99.5%" cellspacing="1" cellpadding="5" style="margin-left: 2px" border="1" bordercolor="#ccc">
            <tr>
                <th style="width: 5%">
                    序号
                </th>
                <th style="width: 15%">
                    步骤名称
                </th>
                <th style="width: 25%">
                    处理人
                </th>
                <th style="width: 15%">
                    任务状态
                </th>
                <th style="width: 15%">
                    任务处理时间
                </th>
                <th style="width: 15%">
                    耗时
                </th>
            </tr>
            <tbody>
                <asp:Repeater ID="rpSource" runat="server">
                    <ItemTemplate>
                        <tr onmouseover="currentcolor=this.style.backgroundColor;this.style.backgroundColor='#F3F3F3';" onmouseout="this.style.backgroundColor=currentcolor;" style='background-color:<%# (Container.ItemIndex + 1)%2==0?"#F9F9F9":"" %>;cursor: pointer;'>
                            <td>
                                <%#Container.ItemIndex+1 %>
                            </td>
                            <td>
                                <%#Eval("STEPLABEL")%>
                            </td>
                            <td>
                                <%#getTaskUserName(Eval("ASSIGNEDTOUSER").ToString())%>
                            </td>
                            <td>
                                <%#getTaskStatus(Eval("STATUS").ToString())%>
                            </td>
                            <td>
                                <%#Eval("ENDTIME")%>
                            </td>
                            <td>
                                <%#getCostTime(Eval("STARTTIME").ToString(), Eval("ENDTIME").ToString(), Eval("STATUS").ToString())%>
                            </td>
                        </tr>
                    </ItemTemplate>
                </asp:Repeater>
            </tbody>
        </table>
    </div>
    </form>
</body>
</html>
