<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="HistoryAssignList.aspx.cs" Inherits="Ultimus.UWF.Workflow.HistoryAssignList" %>
<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <script type="text/javascript" src="/Assets/js/common.js"></script>
    <script src="/Assets/Layer/layer.js" type="text/javascript"></script>
    <link href="/Assets/Layer/skin/default/layer.css" rel="stylesheet" type="text/css" />

    <script type="text/javascript">
        function OpenDetail(obj) {
            var URL = "AssginDetailInfo.aspx?FORMID="+obj;
              layer.open({
                  type: 2,
                title: 'Authorization Letter',
                area: ['1000px', '550px'],
                content: ['' + URL + '', 'no'] //这里content是一个URL，如果你不想让iframe出现滚动条，你还可以content: ['http://sentsin.com', 'no']
            });  
        }
    </script>
    <title>AssignList</title>
</head>
<body>
    <form id="form1" runat="server">
     
      <div id="myDiv" class="container">
        <div class="row" style="text-align:center">
          <h2 style="text-align:center">Assign List</h2>
              <table class="table table-condensed table-bordered">
              <tr>
              <th>No</th>
              <th>AssignFrom</th>
              <th>AssignTo</th>
              <th>StartDate</th>
              <th>EndDate</th>
              <th>Contents</th>
              <th>CreateDate</th>
              </tr>
              <asp:Repeater runat="server" ID="RPList">
              <ItemTemplate>
              <tr>
              <td><%#Eval("RN") %></td>
              <td><%#Eval("AssginTaskUser")%></td>
               <td><%#Eval("AssginTaskTo")%></td>
              <td><%#Eval("AssginStartDate")%></td>
              <td><%#Eval("AssginEndDate")%></td>
              <td><a href="javascript:void(0)" onclick='OpenDetail(<%#Eval("ID") %>)' ><%#Eval("Comments")%></a></td>
              <td><%#Eval("CreateDate")%></td>
              </tr>
              </ItemTemplate>
              </asp:Repeater>
              </table>
              <asp:Label runat="server" ID="lbText"></asp:Label>
                <webdiyer:AspNetPager ID="AspNetPager1" runat="server" CustomInfoHTML="Count %RecordCount%"
                                    Width="100%" CssClass="aspNetPager" CurrentPageButtonClass="pageBtn" OnPageChanged="AspNetPager1_PageChanged"
                                    AlwaysShow="true" PageSize="20">
                                </webdiyer:AspNetPager>
        </div>
     </div>
    
    </form>
</body>
</html>
