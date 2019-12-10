<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PersonalExpenseReport.aspx.cs" Inherits="Ultimus.UWF.Report.PersonalExpenseReport" %>
<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Personal Expense Report</title>
    <script src="/Assets/js/common.js" type="text/javascript"></script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
       <div class="row">
                <table width="100%" class="table table-condensed table-bordered">
                <tr>
                <td colspan="3" class="banner">
                查询 Search
                </td>
                </tr>
                    <tr>
                        <td class="td-label" >
                        时间 DateTime
                        </td>
                        <td class="td-content"  style="width:50%">
                       从 From <asp:TextBox runat="server" ID="txtStartTime" width="100px" onclick="WdatePicker({readOnly:true,dateFmt: 'yyyy/MM/dd',maxDate:'#F{$dp.$D(\'txtEndTime\')}'})" ></asp:TextBox>
                       到 To
                       <asp:TextBox runat="server" ID="txtEndTime" width="100px" onclick="WdatePicker({readOnly:true, dateFmt: 'yyyy/MM/dd',minDate:'#F{$dp.$D(\'txtStartTime\')}'})"></asp:TextBox>
                        </td>
                        <td style="text-align:center">
                        <asp:Button runat="server" CssClass="btn" ID="btnSearch" Text="查询 Search" 
                                onclick="btnSearch_Click" />
                                <asp:Button runat="server" ID="btnExport" CssClass="btn" Text="导出 Export" 
                                onclick="btnExport_Click"   />
                        </td>
                        </tr>
                        </table>
     </div>
   
 
     <div class="row" >
                    <table class="table table-condensed table-bordered tablerequired" style="width:100%" id="tbDetail">
                        <tr>
                        <th>序号<br />No.</th>
                        <th>单号<br />DocumentNo</th>
                        <th>标题<br />Title</th>
                        <th>申请人<br />APPLICANT</th>
                        <th>申请时间<br />RequestDate</th>
                        <th>金额<br />Amount</th>
                       </tr>
                       <asp:Repeater runat="server" ID="rpList">
                       <ItemTemplate>
                       <tr>
                       <td><%# Container.ItemIndex+1%></td>
                       <td>
                       <%#Eval("DOCUMENTNO")%>
                       </td>
                       <td>
                       <%#Eval("TRSummary")%>
                       </td>
                        <td>
                       <%#Eval("APPLICANT")%>
                       </td>
                        <td>
                       <%#  Convert.ToDateTime(Eval("REQUESTDATE")).ToString("yyyy/MM/dd")%>
                       </td>
                          <td>
                       <%#Eval("CountSub")%>
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
