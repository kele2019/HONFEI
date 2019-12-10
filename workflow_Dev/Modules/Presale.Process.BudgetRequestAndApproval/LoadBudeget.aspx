<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="LoadBudeget.aspx.cs" Inherits="Presale.Process.BudgetRequestAndApproval.LoadBudeget" %>

<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Budget Account List</title>
     <script type="text/javascript" src="/Assets/js/common.js"></script>
     <script type="text/javascript">
         function LoadBudeget(obj) {
             var Rturnval = "";
             var Code = $(obj).html();
             var Type = $(obj).parent().next().next().find("input").val();
             var BudgetID= $(obj).next().val();
             Rturnval = Code + "," + Type + "," + BudgetID;
             window.returnValue = Rturnval;
             window.close();
          }
     </script>
</head>
<base target="_self" />
<body > 
    <form id="form1" runat="server">
      <div id="myDiv" class="container">
     <div style="text-align:center">  <h2>Budget List</h2> </div>
    <table class="table table-condensed table-bordered">
      <tr><th colspan="3" style="text-align:left; font-weight:bold;">Search </th> </tr>
    <tr>
    <th> Budget Account Code：</th>
    <td><asp:TextBox runat="server" ID="txtAccountCode"></asp:TextBox></td>
    <td>
    <asp:Button runat="server" ID="btnSerach" CssClass="btn" Text="Serach" 
            onclick="btnSerach_Click" />
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
    </tr>

    <asp:Repeater runat="server" ID="RPList">
    <ItemTemplate>
    <tr>
    <td><%#Eval("RN") %></td>
    <td><a href="#"  onclick="LoadBudeget(this)"> <%#Eval("BugetAccountNo")%> </a>
    <asp:HiddenField runat="server" ID="hidID" Value='<%#Eval("ID") %>' />
    </td>
    <td><%#Eval("BugetAccountDesc")%></td>
    <td><%#Eval("AccountType") %>
    <input type="hidden" value='<%#Eval("AccountType") %>' />
    </td>
    <td><%#Eval("IsActive").ToString()=="1"?"IsActive":"Block"%></td>
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