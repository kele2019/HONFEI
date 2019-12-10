<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="LoadTR.aspx.cs" Inherits="Presale.Process.TravalExpense.LoadTR" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<base target="_self" />
    <title>出差申请单列表 Travel Request List</title>
    <script type="text/javascript" src="/Assets/js/common.js"></script>
    <script type="text/javascript">
        function confirmPgaeR() {
            var Rturnval = "";
            $("#tableList").find("tr").each(function (i, Etr) {
                var flag = $(Etr).find("input[type=checkbox]").attr("checked");
                if (flag) {
                    Rturnval += $(Etr).find("td").eq(1).html() + "," ;
                }
            });
            //window.parent.
//            var parent = window.dialogArguments;
            //            parent.document.getElementsByID("fld_RequestTravalNo").value = Rturnval;
            window.returnValue = Rturnval.substr(0,(Rturnval.length-1));
            window.close();
        }
    </script>
</head>

<body>

    <form id="form1" runat="server">
      <div class="container">
      <br />
    <div class="row">
    <div>
      <table  style="border:0px;"class="table table-condensed table-bordered"     >
                 <tr >
                <td  width="16%" style="border:0px;">
    申请日期/RequestDate：</td>
    <td width="60%" style="border:0px;">
    <asp:TextBox runat="server" ID="txtSDate" Width="100px" onclick="WdatePicker({readOnly:true,dateFmt: 'yyyy-MM-dd',maxDate:'#F{$dp.$D(\'txtEDate\')}'})" ></asp:TextBox>
    &nbsp;&nbsp;&nbsp;至/To&nbsp;&nbsp;&nbsp;
    <asp:TextBox runat="server" ID="txtEDate"  Width="100px" onclick="WdatePicker({readOnly:true, dateFmt: 'yyyy-MM-dd',minDate:'#F{$dp.$D(\'txtSDate\')}'})" ></asp:TextBox>
    
    <asp:Button runat="server" CssClass="btn" ID="btnSearch" Text="查询/Search" 
            onclick="btnSearch_Click" />
            </td>
            <td style="border:0px;">
            <input type="button"  class="btn" value="确认/Ok" onclick="confirmPgaeR()" />
            </td>
            </tr>
            </table>
    </div>
     <table  width="80%" class="table table-condensed table-bordered">
     <tr>
     <th>选项/Option</th>
     <th>申请单号/RequestNo</th>
    <th>标题/Title</th>
     <th>申请日期/RequestDate</th>
     </tr>
     <tbody id="tableList">
     <asp:Repeater runat="server" ID="RPList">
     <ItemTemplate>
     <tr>
     <td style="text-align:center"><asp:CheckBox runat="server" ID="cblist" />
     </td>
     <td style="text-align:center">
     <%#Eval("DOCUMENTNO") %>
     </td>
     <td style="text-align:center"><%# Eval("PROCESSSUMMARY")%></td>
     <td style="text-align:center"><%# Convert.ToDateTime(Eval("REQUESTDATE")).ToString("yyyy-MM-dd") %></td>
     </tr>
     </ItemTemplate>
     </asp:Repeater>
     </tbody>
     </table>
    </div>
    </div>
    </form>
</body>
</html>
