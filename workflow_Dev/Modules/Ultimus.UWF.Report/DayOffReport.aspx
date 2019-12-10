<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="DayOffReport.aspx.cs" Inherits="Ultimus.UWF.Report.DayOffReport" %>
<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>DayOff Report</title>
    <script src="/Assets/js/common.js" type="text/javascript"></script>
    <script type="text/javascript">
        function getSelectBy_onclick(index) {
            if (index == "1") {
                $("#select").val("dept");
            }
            if (index == "2") {
                $("#select").val("employee");
            }
        }
    </script>`
</head>
<body style="text-align:center">
    <form id="form1" runat="server" style="margin-left:20%;" align="center">
    <div style="width:80%;">
        <div class="row">
            <table class="table table-condensed table-bordered" >
                <tr>
                    <td class="td-label" >Dept</td>
                    <td class="td-content" colspan="3"><asp:DropDownList runat="server" ID="dropDepartment"></asp:DropDownList></td>
                    <td class="td-label" >Employee Name</td>
                    <td class="td-content" colspan="3"><asp:DropDownList runat="server" ID="dropEmployee"></asp:DropDownList></td>
                </tr>
                <tr>
                    <td class="td-label" >Year</td>
                    <td class="td-content" colspan="3"><asp:DropDownList runat="server" ID="dropYear"></asp:DropDownList></td>
                    <td class="td-label" >Month</td>
                    <td class="td-content" colspan="3"><asp:DropDownList runat="server" ID="dropMonth"></asp:DropDownList></asp:DropDownList></td>
                 </tr>
                 <tr>
                    <td class="td-content"  colspan="4">
                        <asp:RadioButton ID="RadioButton1" GroupName="selectBy" Checked="true" runat="server" onclick="getSelectBy_onclick(1)"/>select by department
                        <asp:RadioButton ID="RadioButton2" GroupName="selectBy" runat="server" onclick="getSelectBy_onclick(2)"/>select by employee
                        <asp:TextBox runat="server" ID="select" style="display:none"></asp:TextBox>
                    </td>
                    <td style="text-align:center" colspan="4">
                        <asp:Button runat="server" CssClass="btn" ID="btnSearch" Text="Search" onclick="btnSearch_Click" />
                    </td>
                 </tr>
            </table>
     </div>
     <div class="row" >
                    <table class="table table-condensed table-bordered tablerequired" style="width:100%" id="tbDetail">
                        <tr>
                        <th>Dept</th>
                        <th>EmployeeName</th>
                        <th>Hours</th>
                       </tr>
                       <asp:Repeater runat="server" ID="rpList">
                       <ItemTemplate>
                       <tr>
                       <td>
                       <%#Eval("department")%>
                       </td>
                       <td>
                       <%#Eval("employee")%>
                       </td>
                        <td>
                       <%#Eval("hour")%>
                       </td>
                       </tr>
                       </ItemTemplate>
                       </asp:Repeater>
                       <tfoot>
                    <tr>
                    <td colspan="6">
                      <webdiyer:aspnetpager id="AspNetPager1" runat="server" custominfohtml="Count %RecordCount%"
                        horizontalalign="right" width="100%" cssclass="aspNetPager" onpagechanged="AspNetPager1_PageChanged"
                        pagesize="10" alwaysshow="true" submitbuttonstyle="display:none" inputboxstyle="display:none"
                        nextpagetext="Next" firstpagetext="Home" lastpagetext="Last" prevpagetext="Prev">
                    </webdiyer:aspnetpager>
                    </td>
                    </tr>
                    </tfoot>
                       </table>
                       </div>
    </div>
    </form>
</body>
</html>
