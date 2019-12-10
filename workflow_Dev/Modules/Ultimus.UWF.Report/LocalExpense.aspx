<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="LocalExpense.aspx.cs" Inherits="Ultimus.UWF.Report.LocalExpense" %>
<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <script src="/Assets/js/common.js" type="text/javascript"></script>
    <script type="text/javascript">
        function documentno_click(obj) {
            var suburl = $(obj).parent().next().children().text();
            var url = "./LocalExpenseReport.aspx?FormID='" + suburl + "'"
            var open = window.open(url);
            open.focus();
        }
       
    </script>
</head>
<body>
    <form id="form1" runat="server" style="margin-left:20%;" align="center">
    <div style="width:80%;">
            <div class="row">
                <table class="table table-condensed table-bordered">
                <tr>
                <td colspan="3" class="banner">
                 Search
                </td>
                </tr>
                <tr>
                    <td class="td-label" >
                         DateTime
                    </td>
                    <td class="td-content"  style="width:50%">
                        From <asp:TextBox runat="server" ID="txtStartTime" width="100px" onclick="WdatePicker({readOnly:true,dateFmt: 'yyyy/MM/dd',maxDate:'#F{$dp.$D(\'txtEndTime\')}'})" ></asp:TextBox>
                        To<asp:TextBox runat="server" ID="txtEndTime" width="100px" onclick="WdatePicker({readOnly:true, dateFmt: 'yyyy/MM/dd',minDate:'#F{$dp.$D(\'txtStartTime\')}'})"></asp:TextBox>
                    </td>
                    <td style="text-align:center">
                        <asp:Button runat="server" CssClass="btn" ID="btnSearch" Text="Search" onclick="btnSearch_Click" />
                    </td>
                </tr>
        </table>
     </div>
        <div>
            <table class="table table-condensed table-bordered">
                <thead>
                    <tr>
                        <th width="4%">NO.</th>
                        <th width="48%">DocumentNO</th>
                        <th width="48%">Date</th>
                    </tr>
                </thead>
                <tbody id="travelexpensedetail">
                    <asp:Repeater runat="server" ID="travelExpenseReport">
                         <ItemTemplate>
                            <tr>
                                <td><p style="text-align:center"><%# Container.ItemIndex+1%></p></td>
                                <td><a class="documentno" onclick="documentno_click(this)"><p style="text-align:center"><%# Eval("DOCUMENTNO")%></p></a></td>
                                <td style="display:none;"><asp:Label runat="server" ID="FORMID" Text='<%# Eval("FORMID")%>'></asp:Label></td>
                                <td><p style="text-align:center"><%#  Convert.ToDateTime(Eval("REQUESTDATE")).ToString("yyyy/MM/dd")%></p></td>
                            </tr>
                        </ItemTemplate>
                    </asp:Repeater>
                </tbody>
                <tfoot>
                    <tr>
                    <td colspan="3">
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

