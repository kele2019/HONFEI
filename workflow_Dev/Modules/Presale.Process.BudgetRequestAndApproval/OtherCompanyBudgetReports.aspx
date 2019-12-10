<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="OtherCompanyBudgetReports.aspx.cs" Inherits="Presale.Process.BudgetRequestAndApproval.OtherCompanyBudgetReports" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
     <title>Company Budget Account Reports</title>
     <script type="text/javascript" src="/Assets/js/common.js"></script>
     <script type="text/javascript">

         $(document).ready(function () {
             $(".container").attr("style", "width:1200px");
             $(".td-label").attr("style", "width:15%");
             $(".td-content").attr("style", "width:35%");



             var Jan = 0; var Feb = 0; var Mar = 0; var Apr = 0; var May = 0; var Jun = 0; var Jul = 0; var Aug = 0; var Sep = 0; var Oct = 0; var Nov = 0; var Dec = 0; var SubTotal = 0;
             var AJan = 0; var AFeb = 0; var AMar = 0; var AApr = 0; var AMay = 0; var AJun = 0; var AJul = 0; var AAug = 0; var ASep = 0; var AOct = 0; var ANov = 0; var ADec = 0; var ASubTotal = 0;

             $("#tbodyDetail").find("tr").each(function () {
                 if ($(this).attr("class") != "TRActual") {
                     Jan += $(this).find("td").eq(2).html() - 0; //一月
                     Feb += $(this).find("td").eq(3).html() - 0; //二月
                     Mar += $(this).find("td").eq(4).html() - 0; //三月
                     Apr += $(this).find("td").eq(5).html() - 0; //四月
                     May += $(this).find("td").eq(6).html() - 0; //五月
                     Jun += $(this).find("td").eq(7).html() - 0; //六月
                     Jul += $(this).find("td").eq(8).html() - 0; //七月
                     Aug += $(this).find("td").eq(9).html() - 0; //八月
                     Sep += $(this).find("td").eq(10).html() - 0; //九月
                     Oct += $(this).find("td").eq(11).html() - 0; //十月
                     Nov += $(this).find("td").eq(12).html() - 0; //十一月
                     Dec += $(this).find("td").eq(13).html() - 0; //十二月
                     SubTotal += $(this).find("td").eq(14).html() - 0; //小计
                 }
                 else {
                     AJan += $(this).find("td").eq(2).html() - 0; //一月
                     AFeb += $(this).find("td").eq(3).html() - 0; //二月
                     AMar += $(this).find("td").eq(4).html() - 0; //三月
                     AApr += $(this).find("td").eq(5).html() - 0; //四月
                     AMay += $(this).find("td").eq(6).html() - 0; //五月
                     AJun += $(this).find("td").eq(7).html() - 0; //六月
                     AJul += $(this).find("td").eq(8).html() - 0; //七月
                     AAug += $(this).find("td").eq(9).html() - 0; //八月
                     ASep += $(this).find("td").eq(10).html() - 0; //九月
                     AOct += $(this).find("td").eq(11).html() - 0; //十月
                     ANov += $(this).find("td").eq(12).html() - 0; //十一月
                     ADec += $(this).find("td").eq(13).html() - 0; //十二月
                     ASubTotal += $(this).find("td").eq(14).html() - 0; //小计
                 }
             });
             $(".TRCount").find("td").eq(2).html(Jan); Jan = 0;
             $(".TRCount").find("td").eq(3).html(Feb); Feb = 0;
             $(".TRCount").find("td").eq(4).html(Mar); Mar = 0;
             $(".TRCount").find("td").eq(5).html(Apr); Apr = 0;
             $(".TRCount").find("td").eq(6).html(May); May = 0;
             $(".TRCount").find("td").eq(7).html(Jun); Jun = 0;
             $(".TRCount").find("td").eq(8).html(Jul); Jul = 0;
             $(".TRCount").find("td").eq(9).html(Aug); Aug = 0;
             $(".TRCount").find("td").eq(10).html(Sep); Sep = 0;
             $(".TRCount").find("td").eq(11).html(Oct); Oct = 0;
             $(".TRCount").find("td").eq(12).html(Nov); Nov = 0;
             $(".TRCount").find("td").eq(13).html(Dec); Dec = 0;
             $(".TRCount").find("td").eq(14).html(SubTotal); SubTotal = 0;

             $(".ActuralCount").find("td").eq(2).html(AJan); AJan = 0;
             $(".ActuralCount").find("td").eq(3).html(AFeb); AFeb = 0;
             $(".ActuralCount").find("td").eq(4).html(AMar); AMar = 0;
             $(".ActuralCount").find("td").eq(5).html(AApr); AApr = 0;
             $(".ActuralCount").find("td").eq(6).html(AMay); AMay = 0;
             $(".ActuralCount").find("td").eq(7).html(AJun); AJun = 0;
             $(".ActuralCount").find("td").eq(8).html(AJul); AJul = 0;
             $(".ActuralCount").find("td").eq(9).html(AAug); AAug = 0;
             $(".ActuralCount").find("td").eq(10).html(ASep); ASep = 0;
             $(".ActuralCount").find("td").eq(11).html(AOct); AOct = 0;
             $(".ActuralCount").find("td").eq(12).html(ANov); ANov = 0;
             $(".ActuralCount").find("td").eq(13).html(ADec); ADec = 0;
             $(".ActuralCount").find("td").eq(14).html(ASubTotal); ASubTotal = 0;

             $(".money").each(function () {
                 var totalCount = $(this).html();
                 var ThoudsData = formatNumber(totalCount, 2, 1);
                 $(this).html(ThoudsData);

             });
         });
     </script>
       <style type="text/css">
     .TRCount td
     {
         color:blue;
         font-weight:bolder;
     }
      .TRActual td
     {
         color:Red;
     }
       .ActuralCount td
     {
         color:blue;
         font-weight:bolder;
     }
     </style>
</head>
<body>
    <form id="form1" runat="server">
      <div id="myDiv"  >
    <div style="text-align:center">
   <h2>Company Budget Reports</h2>
     <table class="table table-condensed table-bordered">
                    <tr>
                    <th colspan="4" style="text-align:left"><b>Search</b></th>
                    </tr>
                    <tr>
                    <th>Year：</th>
                    <td>
                   <asp:DropDownList runat="server" ID="dropYear">
                   </asp:DropDownList>
                     </td>
                      <th>Type：</th>
                    <td>
                     <asp:DropDownList runat="server" ID="dropAccountType">
                     <asp:ListItem Value="SG&A">SG&A</asp:ListItem>
                     <asp:ListItem Value="RDE">RDE</asp:ListItem>
                     <asp:ListItem Value="Capital">Capital</asp:ListItem>
                   </asp:DropDownList>
                    </td>
                   
                    </tr>
                    <tr>
                    <th>BudegetType：</th>
                    <td>
                      <asp:DropDownList runat="server" ID="dropBudgetType">
                     <asp:ListItem Value="4+8">4+8</asp:ListItem>
                     <asp:ListItem Value="7+5">7+5</asp:ListItem>
                     </asp:DropDownList>
                    </td>
                     <td colspan="2">
                    <asp:Button runat="server" ID="btnSearch" Text="Search" CssClass="btn" 
                            style="margin-left:20px" onclick="btnSearch_Click" /> 
                            <asp:Button runat="server" ID="btnExport" Text="Export Excel" CssClass="btn"  style="margin-left:20px" onclick="btnExport_Click" />
                    </td>
                    </tr>
                    </table>
          
          <table class="table table-condensed table-bordered">
                    <tr>
                    <th>Finace Accounts</th>
                    <th style="display:none"></th>
                    <th>Jan</th>
                    <th>Feb</th>
                    <th>Mar</th>
                    <th>Apr</th>
                    <th>May</th>
                    <th>Jun</th>
                    <th>July</th>
                    <th>Aug</th>
                    <th>Sep</th>
                    <th>Oct</th>
                    <th>Nov</th>
                    <th>Dec</th>
                    <th>Total</th>
                    </tr>
                    <tbody id="tbodyDetail">
                    <asp:Repeater runat="server" ID="RPlist">
                    <ItemTemplate>
                    <tr>
                    <td><%#Eval("BugetAccountNo")%><br />
                    <%#Eval("BugetAccountDesc")%>
                    <asp:HiddenField runat="server" ID="hidBugetAccountNo" Value='<%#Eval("BugetAccountNo") %>' />
                    </td>
                    <td  style="display:none"></td>
                    <td class="money"><%#Eval("Jan")%></td>
                    <td class="money"><%#Eval("Feb")%></td>
                    <td class="money"><%#Eval("Mar")%></td>
                    <td class="money"><%#Eval("Apr")%></td>
                    <td class="money"><%#Eval("May")%></td>
                    <td class="money"><%#Eval("Jun")%></td>
                    <td class="money"><%#Eval("July")%></td>
                    <td class="money"><%#Eval("Aug")%></td>
                    <td class="money"><%#Eval("Sep")%></td>
                    <td class="money"><%#Eval("Oct")%></td>
                    <td class="money"><%#Eval("Nov")%></td>
                    <td class="money"><%#Eval("Dec")%></td>
                    <td class="money"><%#Eval("SubTotal")%></td>
                    </tr>
                    <tr class="TRActual" >
                    <td colspan="1"  >Actual Data：</td>
                     <td style="display:none"></td>
                    <td class="money"><%#Eval("AJan")%></td>
                    <td class="money"><%#Eval("AFeb")%></td>
                    <td class="money"><%#Eval("AMar")%></td>
                    <td class="money"><%#Eval("AApr")%></td>
                    <td class="money"><%#Eval("AMay")%></td>
                    <td class="money"><%#Eval("AJun")%></td>
                    <td class="money"><%#Eval("AJuly")%></td>
                    <td class="money"><%#Eval("AAug")%></td>
                    <td class="money"><%#Eval("ASep")%></td>
                    <td class="money"><%#Eval("AOct")%></td>
                    <td class="money"><%#Eval("ANov")%></td>
                    <td class="money"><%#Eval("ADec")%></td>
                    <td class="money"><%#Eval("ASubTotal")%></td>
                    </tr>
                    </ItemTemplate>
                    </asp:Repeater>
                    </tbody>
                    <tr class="TRCount">
                    <td>Total：</td>
                    <td style="display:none"></td>
                     <td class="money"></td>
                     <td class="money"></td>
                     <td class="money"></td>
                     <td class="money"></td>
                     <td class="money"></td>
                     <td class="money"></td>
                     <td class="money"></td>
                     <td class="money"></td>
                     <td class="money"></td>
                     <td class="money"></td>
                     <td class="money"></td>
                     <td class="money"></td>
                     <td class="money"></td>
                    </tr>
                    <tr class="ActuralCount">
                    <td>Actual Total：</td>
                    <td style="display:none"></td>
                     <td class="money"></td>
                     <td class="money"></td>
                     <td class="money"></td>
                     <td class="money"></td>
                     <td class="money"></td>
                     <td class="money"></td>
                     <td class="money"></td>
                     <td class="money"></td>
                     <td class="money"></td>
                     <td class="money"></td>
                     <td class="money"></td>
                     <td class="money"></td>
                     <td class="money"></td>
                    </tr>
                    </table>
                     
    </div>
    
    </div>
    </form>
</body>
</html>