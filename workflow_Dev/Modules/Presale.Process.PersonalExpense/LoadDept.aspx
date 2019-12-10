<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="LoadDept.aspx.cs" Inherits="Presale.Process.PersonalExpense.LoadDept" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
<base target="_self" />
    <title>部门列表 Dept List</title>
    <script type="text/javascript" src="/Assets/js/common.js"></script>
    <script type="text/javascript">
        function confirmPgaeR(obj) {
            var Rturnval = "";
            var Etr=$(obj).parent().parent();
            Rturnval += $(Etr).find("a").eq(0).text() + " " + $(Etr).find("a").eq(1).text();
//            $("#tableList").find("tr").each(function (i, Etr) {
//                var flag = $(Etr).find("input[type=radio]").attr("checked");
//                if (flag) {
//                    Rturnval += $(Etr).find("td").eq(1).html() + " " + $(Etr).find("td").eq(2).html();
//                }

          //  });
            //window.parent.
            //            var parent = window.dialogArguments;
            //            parent.document.getElementsByID("fld_RequestTravalNo").value = Rturnval;
            window.returnValue = Rturnval.substr(0, (Rturnval.length - 1));
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
    部门名称/Dept Name：</td>
    <td width="60%" style="border:0px;">
    <asp:TextBox runat="server" ID="txtDept" Width="100px"   ></asp:TextBox>
    <asp:Button runat="server" CssClass="btn" ID="btnSearch" Text="查询/Search" 
            onclick="btnSearch_Click" />
            </td>
            <td style="border:0px;">
            <input type="button"  class="btn" value="确认/Ok" style="display:none" onclick="confirmPgaeR()" />
            </td>
            </tr>
            </table>
    </div>
     <table  width="80%" class="table table-condensed table-bordered">
     <tr>
     <th>成本中心 Cost Center</th>
     <th>部门 Dept</th>
    <th>部门名称 Dept Name</th>
     </tr>
     <tbody id="tableList">
     <asp:Repeater runat="server" ID="RPList">
     <ItemTemplate>
     <tr>
     <td style="text-align:center">
     <%#Eval("EXT02") %>
     </td>
     <td style="text-align:left">
     <a href="#" onclick="confirmPgaeR(this)">
      <%#Eval("DEPARTMENTNAME")%>
      </a>
     </td>
     <td style="text-align:left">
    <a href="#" onclick="confirmPgaeR(this)">
     <%# Eval("EXT03")%> </a>
     </td>
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
