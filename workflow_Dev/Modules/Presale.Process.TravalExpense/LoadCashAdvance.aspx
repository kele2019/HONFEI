<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="LoadCashAdvance.aspx.cs" Inherits="Presale.Process.TravalExpense.LoadCashAdvance" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
<base target="_self" />
    <title>借款申请单列表 Cash Advance List</title>
    <script type="text/javascript" src="/Assets/js/common.js"></script>
    <script type="text/javascript">
        function confirmPgaeR() {
            var Rturnval = "[";
            var count = 0;
            $("#tableList").find("tr").each(function (i, Etr) {
                var flag = $(Etr).find("input[type=checkbox]").attr("checked");
                if (flag) {
                    Rturnval += "{'CANo':'" + $.trim($(this).find("td:eq(1)").html()) + "',";
                    Rturnval += "'Reverse':'" + $.trim($(this).find("td:eq(5)").children().val()) + "'},";
                    count++;
                }
            });
            if (Rturnval.lastIndexOf(",") > 0) {
                Rturnval = Rturnval.substring(0, Rturnval.lastIndexOf(","));
            }
            Rturnval += "]";
            if (count > 0) {
                window.returnValue = Rturnval;
                window.close();
            }
            else
                alert("未选中任何项\nNo Select Item");
        }
        function CheckReverseAmount(obj) {
            var Reverser = $(obj).val()-0;
            var Unreturn = $(obj).parent().prev().html()-0;
            if (Reverser > Unreturn) {
                alert("冲账金额不能大于未冲销金额!\nOffset Amount Cant't Greater Unclear Amount!");
                $(obj).focus();
            }
        }
        function selectAmount(obj) {
            var flag = $(obj).attr("checked");
            if (flag) {
                var unamount = $(obj).parent().parent().find("td:eq(4)").html();
                $(obj).parent().parent().find("td:eq(5)").children().val(unamount);
            }
            else {
                $(obj).parent().parent().find("td:eq(5)").children().val("");
            }
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
    
    <asp:Button runat="server" CssClass="btn" ID="btnSearch" Text="查询/Search" onclick="btnSearch_Click" />
            </td>
            <td style="border:0px;">
            <input type="button"  class="btn" value="确认/Ok"   onclick="confirmPgaeR()" />
            </td>
            </tr>
            </table>
    </div>
     <table  width="80%" class="table table-condensed table-bordered">
     <tr>
     <th>选项<br />Option</th>
     <th>申请单号<br />RequestNo</th>
    <th>标题<br />Title</th>
     <th>申请日期<br />RequestDate</th>
     <th>未冲销金额<br />Unclear Amount </th>
     <th>本次冲销金额<br />Offset Amount</th>
     </tr>
     <tbody id="tableList">
     <asp:Repeater runat="server" ID="RPList">
     <ItemTemplate>
     <tr>
     <td>
     <input type="checkbox" onclick="selectAmount(this)" />
     </td>
      <td style="text-align:center">
      <%#Eval("DOCUMENTNO") %>
     </td>
     <td style="text-align:center"><%# Eval("PROCESSSUMMARY")%></td>
     <td style="text-align:center"><%# Convert.ToDateTime(Eval("REQUESTDATE")).ToString("yyyy-MM-dd") %></td>
     <td style="text-align:center"> <%#Eval("UnReturn") %> </td>
     <td><input type="text" money="money" onblur="CheckReverseAmount(this)"  style="width:90%" /></td>
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
