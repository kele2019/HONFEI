<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="YearLavelManager.aspx.cs" Inherits="Presale.Process.Leave.YearLavelManager" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <script type="text/javascript" src="/Assets/js/common.js"></script>
    <title>Year Leave Manager</title>
    <style type="text/css">
    .CheckNullStyle{
         border:red 1px solid;
        }
    </style>
    <script type="text/javascript">
        function CheckFormateData(obj) {
        var RowCount=0;
        if ($(obj).parent().prev().children().val() == "") {
            $(obj).parent().prev().children().attr("style", "border:red 1px solid;width:95%;");
            RowCount++;
        }
        else {
            $(obj).parent().prev().children().attr("style", "border:#cccccc 1px solid;width:95%;");
        }
            if ($(obj).parent().prev().prev().children().val() == "") {
                    $(obj).parent().prev().prev().children().attr("style", "border:red 1px solid;width:95%;");
                    RowCount++;
                }
                 else {
                     $(obj).parent().prev().prev().children().attr("style", "border:#cccccc 1px solid;width:95%;");
                 }
                 if (RowCount > 0)
                     return false;
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
     <div id="myDiv" class="container">

            <div class="row">
            <h2 style="text-align:center">Year Lavel Manager</h2>
             <table class="table table-condensed table-bordered">
             <tr>
             <th>Year：</th>
             <td><asp:DropDownList runat="server" ID="DropYearLavel">
             </asp:DropDownList>
             </td>
             <td>
             <asp:Button runat="server" ID="btnSearch" Text="Search" CssClass="btn" 
                     onclick="btnSearch_Click" />
             </td>
             </tr>
             </table>

              <table class="table table-condensed table-bordered">
                    <tr>
                    <th>NO</th>
                    <th>UserCode</th>
                    <th>ENName</th>
                    <th>CNName</th>
                    <th>Year</th>
                    <th>Year Leave(Days)</th>
                  <%--  <th>Last Leave(H)</th>--%>
                  <th>Sick Leave</th>
                    <th width="5%">Operat</th>
                    </tr>
                    <asp:Repeater runat="server" ID="RpList" onitemcommand="RpList_ItemCommand">
                    <ItemTemplate>
                    <tr>
                    <td><%# Container.ItemIndex+1%></td>
                    <td><%#Eval("USERCODE")%></td>
                    <td><%#Eval("EXT04")%></td>
                    <td><%#Eval("USERNAME")%></td>
                    <td><%#Eval("LYear")%></td>
                    <td>
                    <asp:TextBox runat="server" ID="txtYearCount" width="95%"  Text='<%#Eval("LeaveYearCount") %>'></asp:TextBox>
                    </td>
                   <%-- <td>
                     <%#Eval("LeaveLastYearHourCount")%>
                    </td>--%>
                    <td>
                     <asp:TextBox runat="server" ID="txtSickLeave" width="95%" Text='<%#Eval("FuallpaySick") %>'></asp:TextBox>
                    </td>
                    <td>
                    <asp:Button runat="server" ID="btnSave"  Text="Save" CssClass="btn" CommandName="btnSave"  OnClientClick="return CheckFormateData(this)" CommandArgument='<%#Eval("LOGINNAME") %>'/>
                    <asp:HiddenField runat="server" ID="hidYear"  Value='<%#Eval("LYear") %>'/>
                    <asp:HiddenField runat="server" ID="hidLastHour"  Value='<%#Eval("LeaveLastYearHourCount") %>'/>
                    <asp:HiddenField runat="server" ID="hidYearHourCount" Value='<%#Eval("LeaveYearHourCount") %>' />

                    </td>
                    </tr>
                    </ItemTemplate>
                    </asp:Repeater>
                    </table>

            </div>
    
    </div>
    </form>
</body>
</html>
