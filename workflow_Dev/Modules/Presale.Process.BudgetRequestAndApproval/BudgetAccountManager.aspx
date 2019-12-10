<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="BudgetAccountManager.aspx.cs" Inherits="Presale.Process.BudgetRequestAndApproval.BudgetAccountManager" %>
<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Budget Account Manager</title>
     <script type="text/javascript" src="/Assets/js/common.js"></script>
</head>
<body>
    <form id="form1" runat="server">
      <div id="myDiv" class="container">
     <div style="text-align:center">  <h2>Budget Account Manager</h2> </div>
    <table class="table table-condensed table-bordered">
      <tr><th colspan="3" style="text-align:left; font-weight:bold;">Search </th> </tr>
    <tr>
    <th> Budget Account Code：</th>
    <td><asp:TextBox runat="server" ID="txtAccountCode"></asp:TextBox></td>
    <td>
    <asp:Button runat="server" ID="btnSerach" CssClass="btn" Text="Search" 
            onclick="btnSerach_Click" />
    <input type="button" class="btn" onclick=javascript:location.href='BudgetAccountEdit.aspx?BudegetID=0' style="margin-left:10px;" 　 value="Add New" />
    </td>
    </tr>
    </table>

   <table class="table table-condensed table-bordered">
    <tr>
    <th>No</th>
    <th>Code</th>
    <th>Desc</th>
    <th>Type</th>
    <th>Status</th>
    <th>Opertator</th>
    </tr>

    <asp:Repeater runat="server" ID="RPList" onitemcommand="RPList_ItemCommand">
    <ItemTemplate>
    <tr>
    <td><%#Eval("RN") %></td>
    <td><%#Eval("BugetAccountNo")%></td>
    <td><%#Eval("BugetAccountDesc")%></td>
    <td><%#Eval("AccountType") %></td>
    <td><%#Eval("IsActive").ToString()=="1"?"Active":"Block"%></td>
    <td>
   <asp:LinkButton runat="server" ID="lbdisbaled" Text="Disabled" CommandName="Disabled" OnClientClick="return confirm('Confirm Disable ？')" CommandArgument='<%#Eval("ID") %>' Visible='<%#Eval("IsActive").ToString()=="1"?true:false %>' ></asp:LinkButton>
   <asp:LinkButton runat="server" ID="Linkenabled" Text="Enable" CommandName="Enable" CommandArgument='<%#Eval("ID") %>' OnClientClick="return confirm('Confirm Enable ？')"   Visible='<%#Eval("IsActive").ToString()=="1"?false:true %>'></asp:LinkButton>
    <a style=" float:right;margin-right:30px;" href='BudgetAccountEdit.aspx?BudegetID=<%#Eval("ID") %>'>Edit</a>
    </td>
    </tr>
    </ItemTemplate>
    </asp:Repeater>
    </table>
      <webdiyer:aspnetpager id="AspNetPager1" runat="server" custominfohtml="Count %RecordCount%"
                        horizontalalign="right" width="100%" cssclass="aspNetPager" onpagechanged="AspNetPager1_PageChanged"
                        pagesize="20" alwaysshow="true" submitbuttonstyle="display:none" inputboxstyle="display:none"
                        nextpagetext="Next" firstpagetext="Home" lastpagetext="End" prevpagetext="Prev">
                    </webdiyer:aspnetpager>
    </div>
    </form>
</body>
</html>
