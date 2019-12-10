<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="LeaveReport.aspx.cs" Inherits="Ultimus.UWF.Report.LeaveReport" %>
<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
      <div class="row" >
                    <table class="table table-condensed table-bordered tablerequired" style="width:100%" id="tbDetail">
                        <tr>
                        <th>NO.</th>
                        <th>UserAccount</th>
                        <th>LeaveYear</th>
                        <th>Annual Leave</th>
                        <th>Full Pay Sick Leave</th>
                       
                       </tr>
                       <asp:Repeater runat="server" ID="rpList">
                       <ItemTemplate>
                        <tr>
                             <asp:Label ID="fld_ROWID" Text='<%# Container.ItemIndex+1%>' runat="server"></asp:Label>
                        </tr>
                        <tr>
                            <asp:Label ID="fld_UserAccount" runat="server" Text='<%#Eval("UserAccount") %>'></asp:Label>
                        </tr>
                        <tr>
                            <asp:Label ID="fld_LeaveYear" runat="server" Text='<%# Eval("LeaveYear") %>'></asp:Label>
                        </tr>
                        <tr>
                            <asp:Label ID="fld_AnnualLeave" runat="server" Text='<%#Eval("LeaveYearCount") %>'></asp:Label>
                        </tr>
                        <tr>
                            <asp:Label ID="fld_FullPaySickLeave" runat="server" Text='<%#Eval("FuallpaySick") %>'></asp:Label>
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
