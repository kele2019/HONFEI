<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WorkHolidayManager.aspx.cs" Inherits="Ultimus.UWF.FunctionManager.WorkHolidayManager" %>

<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Work Holiday Manager</title>
    <script type="text/javascript" src="/Assets/js/common.js"></script>
       <script src="/Assets/Layer/layer.js" type="text/javascript"></script>
    <link href="/Assets/Layer/skin/default/layer.css" rel="stylesheet" type="text/css" />

     <script type="text/javascript">
         function OpenEdit(obj) {
             if (obj == undefined)
                 obj = "";
                 var URL = "HolidayEdit.aspx?ID=" + obj;
             
             layer.open({
                 type: 2,
                 title: 'Holiday Edit',
                 area: ['500px', '250px'],
                 content: ['' + URL + '', 'no'] //这里content是一个URL，如果你不想让iframe出现滚动条，你还可以content: ['http://sentsin.com', 'no']
             });
         }
    </script>

</head>
<body>
    <form id="form1" runat="server">
   <div id="myDiv" class="container">
    
     <div style="text-align:center">  <h2>Work Holiday Manager</h2> </div>
     <div style="text-align:right; margin-bottom:5px;"><input type="button" value="Add" class="btn" onclick="OpenEdit()" /></div>
    <table class="table table-condensed table-bordered">
     <tr>
    <th width="10%">No</th>
    <th>Date</th>
    <th>Type</th>
    <th width="10%">Opertator</th>
    </tr>

      <asp:Repeater runat="server" ID="RPList" onitemcommand="RPList_ItemCommand">
    <ItemTemplate>
    <tr>
    <td><%#Eval("RN") %></td>
    <td><%#Eval("DicText")%></td>
    <td><%#Eval("DicValue")%></td>
    <td>
     <asp:LinkButton runat="server" ID="lbdisbaled" Text="Delete" CommandName="Delete" OnClientClick="return confirm('Confirm Delete ？')" CommandArgument='<%#Eval("ID") %>'   ></asp:LinkButton>
   &nbsp;&nbsp;<a href="#" onclick='OpenEdit(<%#Eval("ID") %>)'>Edit</a>
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
