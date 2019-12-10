<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="OtherDeptBudgetReports.aspx.cs" Inherits="Presale.Process.BudgetRequestAndApproval.OtherDeptBudgetReports" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Dept Budget Account Reports</title>
     <script type="text/javascript" src="/Assets/js/common.js"></script>
     <style type="text/css">
     .TRCount td
     {
         color:Red;
     }
      .TRActual td
     {
         color:Red;
     }
      .TableCount td
     {
         color:blue;
       <%--  font-size:large;--%>
         font-weight:bolder;
     }
      .AcctualTableCount td
     {
         color:blue;
         <%--font-size:large;--%>
         font-weight:bolder;
     }
     
     </style>
     <script type="text/javascript">

         $(document).ready(function () {
             var Jan = 0; var Feb = 0; var Mar = 0; var Apr = 0; var May = 0; var Jun = 0; var Jul = 0; var Aug = 0; var Sep = 0; var Oct = 0; var Nov = 0; var Dec = 0; var SubTotal = 0;
             var DJan = 0; var DFeb = 0; var DMar = 0; var DApr = 0; var DMay = 0; var DJun = 0; var DJul = 0; var DAug = 0; var DSep = 0; var DOct = 0; var DNov = 0; var DDec = 0; var DSubTotal = 0;

             var AccountCount = 0;
             $(".tbodydetail").find("tr").each(function () {
                 if ($(this).hasClass("TRCount")) {
                     AccountCount++;
                     $(this).find("td").eq(2).html(ChangeNaNData(Jan)); DJan += Jan; Jan = 0;
                     $(this).find("td").eq(3).html(ChangeNaNData(Feb)); DFeb += Feb; Feb = 0;
                     $(this).find("td").eq(4).html(ChangeNaNData(Mar)); DMar += Mar; Mar = 0;
                     $(this).find("td").eq(5).html(ChangeNaNData(Apr)); DApr += Apr; Apr = 0;
                     $(this).find("td").eq(6).html(ChangeNaNData(May)); DMay += May; May = 0;
                     $(this).find("td").eq(7).html(ChangeNaNData(Jun)); DJun += Jun; Jun = 0;
                     $(this).find("td").eq(8).html(ChangeNaNData(Jul)); DJul += Jul; Jul = 0;
                     $(this).find("td").eq(9).html(ChangeNaNData(Aug)); DAug += Aug; Aug = 0;
                     $(this).find("td").eq(10).html(ChangeNaNData(Sep)); DSep += Sep; Sep = 0;
                     $(this).find("td").eq(11).html(ChangeNaNData(Oct)); DOct += Oct; Oct = 0;
                     $(this).find("td").eq(12).html(ChangeNaNData(Nov)); DNov += Nov; Nov = 0;
                     $(this).find("td").eq(13).html(ChangeNaNData(Dec)); DDec += Dec; Dec = 0;
                     $(this).find("td").eq(14).html(ChangeNaNData(SubTotal)); DSubTotal += SubTotal; SubTotal = 0;
                 }
                 else {

                     if ($(this).attr("class") != "TRActual") {
                         $(this).attr("class", "TR" + AccountCount);
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
                 }
             });
             var tablecount = $(".TableCount");
             $(tablecount).find("td").eq(2).html(DJan); DJan = 0;
             $(tablecount).find("td").eq(3).html(DFeb); DFeb = 0;
             $(tablecount).find("td").eq(4).html(DMar); DMar = 0;
             $(tablecount).find("td").eq(5).html(DApr); DApr = 0;
             $(tablecount).find("td").eq(6).html(DMay); DMay = 0;
             $(tablecount).find("td").eq(7).html(DJun); DJun = 0;
             $(tablecount).find("td").eq(8).html(DJul); DJul = 0;
             $(tablecount).find("td").eq(9).html(DAug); DAug = 0;
             $(tablecount).find("td").eq(10).html(DSep); DSep = 0;
             $(tablecount).find("td").eq(11).html(DOct); DOct = 0;
             $(tablecount).find("td").eq(12).html(DNov); DNov = 0;
             $(tablecount).find("td").eq(13).html(DDec); DDec = 0;
             $(tablecount).find("td").eq(14).html(DSubTotal); DSubTotal = 0;



             $(".TRActual").each(function () {

                 DJan += $(this).find("td").eq(2).find("span").text() - 0;
                 DFeb += $(this).find("td").eq(3).find("span").text() - 0;
                 DMar += $(this).find("td").eq(4).find("span").text() - 0;
                 DApr += $(this).find("td").eq(5).find("span").text() - 0;
                 DMay += $(this).find("td").eq(6).find("span").text() - 0;
                 DJun += $(this).find("td").eq(7).find("span").text() - 0;
                 DJul += $(this).find("td").eq(8).find("span").text() - 0;
                 DAug += $(this).find("td").eq(9).find("span").text() - 0;
                 DSep += $(this).find("td").eq(10).find("span").text() - 0;
                 DOct += $(this).find("td").eq(11).find("span").text() - 0;
                 DNov += $(this).find("td").eq(12).find("span").text() - 0;
                 DDec += $(this).find("td").eq(13).find("span").text() - 0;
                 DSubTotal += $(this).find("td").eq(14).find("span").text() - 0;
             });

             var Atablecount = $(".AcctualTableCount");
             $(Atablecount).find("td").eq(2).html(DJan.toFixed(2));
             $(Atablecount).find("td").eq(3).html(DFeb.toFixed(2));
             $(Atablecount).find("td").eq(4).html(DMar.toFixed(2));
             $(Atablecount).find("td").eq(5).html(DApr.toFixed(2));
             $(Atablecount).find("td").eq(6).html(DMay.toFixed(2));
             $(Atablecount).find("td").eq(7).html(DJun.toFixed(2));
             $(Atablecount).find("td").eq(8).html(DJul.toFixed(2));
             $(Atablecount).find("td").eq(9).html(DAug.toFixed(2));
             $(Atablecount).find("td").eq(10).html(DSep.toFixed(2));
             $(Atablecount).find("td").eq(11).html(DOct.toFixed(2));
             $(Atablecount).find("td").eq(12).html(DNov.toFixed(2));
             $(Atablecount).find("td").eq(13).html(DDec.toFixed(2));
             $(Atablecount).find("td").eq(14).html(DSubTotal.toFixed(2));

             if ($("#hidUserOwn").val() != "1")
                 $(".CostCenter").hide();
         });

         function LoadCountData(obj) {

         }

         function ChangeNaNData(obj) {
             if (isNaN(obj))
                 return "0.00";
             else
                 return obj;
         }
         function ToggleBudegetDetail(obj) {
             if ($(obj).attr("value") == "" || $(obj).attr("value") == undefined) {
                 $(obj).attr("value", "1");
                 $(obj).attr("src", "/Assets/images/icon_t_o1.gif");

                 // var TRclass = $(obj).parent().parent().next().attr("class");
                 // $("." + TRclass + "").show();
                 $(obj).parent().parent().parent().next().find("tr").show();
             }
             else {
                 $(obj).attr("value", "");
                 $(obj).attr("src", "/Assets/images/icon_t_o3.gif");
                 //$(obj).parent().parent().parent().next().hide();
                 // var TRclass = $(obj).parent().parent().next().attr("class");
                 // $("." + TRclass + "").hide();
                 //$("." + TRclass + "").eq(0).show();
                 $(obj).parent().parent().parent().next().find('tr[class!="TRCount"]').hide();
             }

         }
     </script>
</head>
<body>
    <form id="form1" runat="server">
     <div id="myDiv"  >
    <div style="text-align:center">
   <h2>Department Budget Reports</h2>
     <table class="table table-condensed table-bordered">
                    <tr>
                    <th colspan="4" style="text-align:left;">  <b> Search</b> </th>
                    </tr>
                    <tr>
                    <th>Year：</th>
                    <td>
                   <asp:DropDownList runat="server" ID="dropYear">
                   </asp:DropDownList>
                     </td>
                     <th>Buget Type：</th>
                     <td>
                     <asp:DropDownList runat="server" ID="dropBudgetType">
                     <asp:ListItem Value="4+8">4+8</asp:ListItem>
                     <asp:ListItem Value="7+5">7+5</asp:ListItem>
                     </asp:DropDownList>
                     </td>
                    </tr>
                     
                    <tr>
                    <th   ><span class="CostCenter"> CostCenter：</span></th>
                     <td >
                     <div class="CostCenter">
                     <asp:DropDownList runat="server" ID="dropCostCenter">
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
                   </div>
                     </td>
                     <td colspan="2">
                       <asp:Button runat="server" ID="btnSearch" Text="Search" CssClass="btn" 
                            style="margin-left:20px" onclick="btnSearch_Click" /> 
                            <asp:HiddenField runat="server" ID="hidUserOwn" />
                            <asp:Button runat="server" ID="btnExport" Text="Export Excel" CssClass="btn" onclick="btnExport_Click" />
                     </td>
                    </tr>
                    </table>
          
          <table class="table table-condensed table-bordered">
                    <tr>
                    <th>Finace Accounts</th>
                    <th>Explain</th>
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
                    
                    <asp:Repeater runat="server" ID="RPAcountType">
                    <ItemTemplate>
                    <tr>
                    <td style="background-color:Red; font-weight:bold;" colspan="15">
                   <asp:Label runat="server" ID="lbAccountType" Text='<%#Eval("AccountType") %>'></asp:Label>
                    </td>
                    </tr>
                   
                   
                    <asp:Repeater runat="server" ID="RPCostCenter" onitemdatabound="RPCostCenter_ItemDataBound">
                    <ItemTemplate>
                    <tbody>
                    <tr>
                    <td style="background-color:Aqua; font-weight:bold;" colspan="15">
                    <img src="/Assets/images/icon_t_o3.gif" onclick="ToggleBudegetDetail(this)" />
                     <asp:HiddenField runat="server" ID="hidAccountID"   Value='<%#Eval("AccountID") %>' />
                     <asp:HiddenField runat="server" ID="hidBugetAccountNo" Value='<%#Eval("BugetAccountNo") %>' />
                    <%#Eval("BugetAccountNo")%>:
                     <%#Eval("BugetAccountDesc")%>
                     </td>
                     </tr>
                     </tbody>
                      <tbody class="tbodydetail">
                    <asp:Repeater runat="server" ID="RPlist">
                    <ItemTemplate>
                    <tr  style="display:none">
                    <td style="text-align:right"> 
                    <%#Eval("SubAccountDesc")%>
                    </td>
                    <td> <td><%#Eval("Explain")%></td></td>
                    <td><%#Eval("Jan")%></td>
                    <td><%#Eval("Feb")%></td>
                    <td><%#Eval("Mar")%></td>
                    <td><%#Eval("Apr")%></td>
                    <td><%#Eval("May")%></td>
                    <td><%#Eval("Jun")%></td>
                    <td><%#Eval("July")%></td>
                    <td><%#Eval("Aug")%></td>
                    <td><%#Eval("Sep")%></td>
                    <td><%#Eval("Oct")%></td>
                    <td><%#Eval("Nov")%></td>
                    <td><%#Eval("Dec")%></td>
                    <td><%#Eval("SubTotal")%></td>
                    </tr>
                    </ItemTemplate>
                    </asp:Repeater>
                    <tr class="TRCount">
                    <td>SumTotal：</td>
                    <td></td>
                    <td>1</td>
                    <td>2</td>
                    <td>3</td>
                    <td>4</td>
                    <td>5</td>
                    <td>6</td>
                    <td>7</td>
                    <td>8</td>
                    <td>9</td>
                    <td>10</td>
                    <td>11</td>
                    <td>12</td>
                    <td></td>
                    </tr>
                   </tbody>
                    <tr style="display:none" class="TRActual">
                    <td>Actual Data：</td>
                    <td></td>
                    <td><asp:Label runat="server" ID="lbJanMonth"></asp:Label> </td>
                    <td><asp:Label runat="server" ID="lbFebMonth"></asp:Label> </td>
                    <td><asp:Label runat="server" ID="lbMarMonth"></asp:Label> </td>
                    <td><asp:Label runat="server" ID="lbAprMonth"></asp:Label> </td>
                    <td><asp:Label runat="server" ID="lbMayMonth"></asp:Label> </td>
                    <td><asp:Label runat="server" ID="lbJunMonth"></asp:Label> </td>
                    <td><asp:Label runat="server" ID="lbJulyMonth"></asp:Label> </td>
                    <td><asp:Label runat="server" ID="lbAugMonth"></asp:Label> </td>
                    <td><asp:Label runat="server" ID="lbSepMonth"></asp:Label> </td>
                    <td><asp:Label runat="server" ID="lbOctMonth"></asp:Label> </td>
                     <td><asp:Label runat="server" ID="lbNovMonth"></asp:Label> </td>
                     <td><asp:Label runat="server" ID="lbDescMonth"></asp:Label> </td>
                     <td><asp:Label runat="server" ID="lbSubTotal"></asp:Label></td>
                    </tr>
                    </ItemTemplate>
                    </asp:Repeater>
                    <!-- 科目详情结束-->
                     </ItemTemplate>
                    </asp:Repeater>


                    <tr class="TableCount">
                    <td>Total：</td>
                    <td></td>
                    <td>1</td>
                    <td>2</td>
                    <td>3</td>
                    <td>4</td>
                    <td>5</td>
                    <td>6</td>
                    <td>7</td>
                    <td>8</td>
                    <td>9</td>
                    <td>10</td>
                    <td>11</td>
                    <td>12</td>
                    <td></td>
                    </tr>

                     <tr style="display:none" class="AcctualTableCount">
                    <td>Actual Total：</td>
                    <td></td>
                    <td>1</td>
                    <td>2</td>
                    <td>3</td>
                    <td>4</td>
                    <td>5</td>
                    <td>6</td>
                    <td>7</td>
                    <td>8</td>
                    <td>9</td>
                    <td>10</td>
                    <td>11</td>
                    <td>12</td>
                    <td></td>
                    </tr>
                    </table>
                     
    </div>
    
    </div>
    </form>
</body>
</html>