<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="BudgetDeptAccountManager.aspx.cs" Inherits="Presale.Process.BudgetRequestAndApproval.BudgetDeptAccountManager" %>

<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Budget Dept Account Manager</title>
     <script type="text/javascript" src="/Assets/js/common.js"></script>
</head>
<body>
    <form id="form1" runat="server">
      <div id="myDiv" class="container">
     <div style="text-align:center">  <h2>Budget Dept Account Manager</h2> </div>
    <table class="table table-condensed table-bordered">
      <tr><th colspan="3" style="text-align:left; font-weight:bold;">Search </th> </tr>
    <tr>
    <th> Budget Account Code：</th>
    <td><asp:TextBox runat="server" ID="txtAccountCode"></asp:TextBox></td>
    <th>CostCenter：</th>
    <td>
    <asp:DropDownList runat="server" ID="dropCostcenter">
    <asp:ListItem Value="">--Pls Select--</asp:ListItem>
    <asp:ListItem Value="50805000">50805000 General Management</asp:ListItem>
   <asp:ListItem Value="50800510">50800510 HSE&F</asp:ListItem>
   <asp:ListItem Value="50801500">50801500 Information Technology</asp:ListItem>
   <asp:ListItem  Value="50801010">50801010 Admin</asp:ListItem>
    <asp:ListItem  Value="50806500">50806500 Quality</asp:ListItem>
    <asp:ListItem  Value="50801000">50801000 Human Resources</asp:ListItem>
    <asp:ListItem  Value="50806200">50806200 Engineering-Mgt.</asp:ListItem>
    <asp:ListItem  Value="50803000">50803000 Finance</asp:ListItem>
    <asp:ListItem  Value="50808510">50808510 Operation</asp:ListItem>
    <asp:ListItem  Value="50805500">50805500 CTO/DCTO</asp:ListItem>
   <asp:ListItem Value="50807010">50807010 HOS</asp:ListItem>
   <asp:ListItem Value="50807500">50807500 Manufacturing</asp:ListItem>
   <asp:ListItem Value="50808520">50808520 Supply chain</asp:ListItem>
    </asp:DropDownList>
    </td>
    </tr>
    <tr>
    <th>Type：</th>
    <td>
     <asp:DropDownList runat="server" ID="dropType" Width="85%">
    <asp:ListItem Value="">--Pls Select--</asp:ListItem>
    <asp:ListItem Value="SG&A">SG&A</asp:ListItem>
    <asp:ListItem Value="RDE">RDE</asp:ListItem>
    <asp:ListItem Value="Capital">Capital</asp:ListItem>
    </asp:DropDownList>
    </td>
    <td colspan="2">
    <asp:Button runat="server" ID="btnSerach" CssClass="btn" Text="Search" 
            onclick="btnSerach_Click" />
    <input type="button" class="btn" onclick=javascript:location.href='BudgetDeptAccountEdit.aspx?BudegetID=0' style="margin-left:10px;" 　 value="Add New" />
    </td>
    </tr>
    </table>

   <table class="table table-condensed table-bordered">
    <tr>
    <th>No</th>
    <th>Code</th>
    <th>Desc</th>
    <th>CostCenter</th>
    <th>Type</th>
    <th>Status</th>
    <th>Opertator</th>
    </tr>

    <asp:Repeater runat="server" ID="RPList" onitemcommand="RPList_ItemCommand">
    <ItemTemplate>
    <tr>
    <td><%#Eval("RN") %></td>
    <td><%#Eval("BugetAccountNo")%></td>
    <td><%#Eval("SubAccountDesc")%></td>
    <td><%#Eval("CostCenter")%></td>
    <td><%#Eval("AccountType") %></td>
    <td><%#Eval("IsActive").ToString()=="1"?"Active":"Block"%></td>
    <td>
   <asp:LinkButton runat="server" ID="lbdisbaled" Text="Disabled" CommandName="Disabled" OnClientClick="return confirm('Confirm Disable ？')" CommandArgument='<%#Eval("ID") %>' Visible='<%#Eval("IsActive").ToString()=="1"?true:false %>' ></asp:LinkButton>
   <asp:LinkButton runat="server" ID="Linkenabled" Text="Enable" CommandName="Enable" CommandArgument='<%#Eval("ID") %>' OnClientClick="return confirm('Confirm Enable ？')"   Visible='<%#Eval("IsActive").ToString()=="1"?false:true %>'></asp:LinkButton>
    <a style=" float:right;margin-right:30px;" href='BudgetDeptAccountEdit.aspx?BudegetID=<%#Eval("ID") %>'>Edit</a>
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
