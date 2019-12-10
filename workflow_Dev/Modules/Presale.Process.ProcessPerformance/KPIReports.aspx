<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="KPIReports.aspx.cs" Inherits="Presale.Process.ProcessPerformance.KPIReports" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>KPI Reports</title>
      <script type="text/javascript" src="/Assets/js/common.js"></script>
       <script type="text/javascript">
       $(document).ready(function () {
            $(".container").attr("style", "width:1400px");
            });
            </script>
</head>
<body>
    <form id="form1" runat="server">
      <div id="myDiv" class="container">
    <div style="text-align:center">
   <h2>KPI Reports</h2>
     <table class="table table-condensed table-bordered">
                    <tr>
                    <th>Search：</th>
                    <td>
                   
                   <asp:DropDownList runat="server" ID="dropYear"  Visible="false"
                            onselectedindexchanged="dropYear_SelectedIndexChanged"  >
                            <asp:ListItem> Year:</asp:ListItem>
                   </asp:DropDownList>
                     Version:
                       <asp:DropDownList runat="server" ID="DropVersion">
                   </asp:DropDownList>

                    <asp:Button runat="server" ID="btnSearch" Text="SearchNew" Visible="false" CssClass="btn" style="margin-left:20px"  onclick="btnSearch_Click" /> 
                    <asp:Button runat="server" ID="btnSeachNew" Text="Search" CssClass="btn" 
                            onclick="btnSeachNew_Click"   />
                     </td>
                    </tr>
                    </table>
                     
    </div>
     <table class="table table-condensed table-bordered">
                    <tr>
                        <th colspan="2" style="text-align:center"> 
                        Process
                        </th>
                       <th colspan="1" style="text-align:center"> 
                        Process Measurement 
                        </th>
                        <th>Goal</th>
                        <th>Definition</th>
                        <th>Calculationmethod</th>
                         <th>YTD</th>
                       <th>Jan</th>
                       <th>Feb</th>
                       <th>Mar</th>
                       <th>April</th>
                       <th>May</th>
                       <th>Jun</th>
                       <th>July</th>
                       <th>Aug</th>
                       <th>Sep</th>
                       <th>Oct</th>
                       <th>Nov</th>
                       <th>Dec</th>
                    </tr>
                    <asp:Repeater runat="server" ID="rpList">
                    <ItemTemplate>
                    <tr>
                    <td width="5%">
                   <%#Eval("DEPTMENTCODE")%>
                    </td>
                     <td width="10%">
                  <%#Eval("PROCESS")%>
                    </td>
                    <td><%#Eval("PROCESSMEA")%></td>
                    <td><%#Eval("STANDARD")%></td>
                    <td><%#Eval("Definition")%></td>
                    <td><%#Eval("Calculationmethod")%></td>
                    <td><%#Eval("YTD")%></td>
                    <td><%#Eval("Jan")%></td>
                    <td><%#Eval("Feb")%></td>
                    <td><%#Eval("Mar")%></td>
                    <td><%#Eval("April")%></td>
                    <td><%#Eval("May")%></td>
                    <td><%#Eval("Jun")%></td>
                    <td><%#Eval("July")%></td>
                    <td><%#Eval("Aug")%></td>
                    <td><%#Eval("Sep")%></td>
                    <td><%#Eval("Oct")%></td>
                    <td><%#Eval("Nov")%></td>
                    <td><%#Eval("Dece")%></td>
                    </tr>
                    </ItemTemplate>
                    </asp:Repeater>

                   </table>
                   </div>

    </form>
</body>
</html>
