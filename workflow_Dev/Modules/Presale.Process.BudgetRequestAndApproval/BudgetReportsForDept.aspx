<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="BudgetReportsForDept.aspx.cs" Inherits="Presale.Process.BudgetRequestAndApproval.BudgetReportsForDept" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
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
        <%-- font-size:large;--%>
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

             //alert($("#hidUserOwn").val());
             if ($("#hidUserOwn").val() != "1")
                 $(".CostCenter").hide();

             $(".tbodyBudgetInfo").find("tr").each(function () {


                 var BudegetNo = $(this).find("input").eq(1).val();
                 if (BudegetNo == "7119800010") {
                     var SpecailTR = $(this).parent().next();
                     var YearData = $("#dropYear").val();
                     $(SpecailTR).find("tr").each(function () {
                         if ($(this).attr("class") != "TRCount") {
                             // alert($(this).find("td").eq(0).html());
                             var CostCenter = $(this).find("td").eq(0).html();
                             var AppendInfo = $(this);
                             $.ajax({ url: 'LoadBudegetInfo.ashx',
                                 data: { "CostCenter": CostCenter, "YearData": YearData, "BudegetNo": BudegetNo },
                                 type: 'POST',
                                 async:false,
                                 success: function (msg) {
                                     if (msg != '') {
                                         $(AppendInfo).after(msg);
                                     }
                                 }
                             });
                         }
                     });
                 }
             });
              
             $(".money").each(function () {
                 var totalCount = $(this).html();
                 var ThoudsData = formatNumber(totalCount, 2, 1);
                 $(this).html(ThoudsData);

             });





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
             if ($(obj).attr("value") == "" || $(obj).attr("value") ==undefined) {
                 $(obj).attr("value", "1");
                 $(obj).attr("src", "/Assets/images/icon_t_o1.gif");
                 
                // var TRclass = $(obj).parent().parent().next().attr("class");
                // $("." + TRclass + "").show();
                 $(obj).parent().parent().parent().next().find("tr").show();
             }
             else {
                 $(obj).attr("value", "");
                 $(obj).attr("src","/Assets/images/icon_t_o3.gif");
                 //$(obj).parent().parent().parent().next().hide();
                 // var TRclass = $(obj).parent().parent().next().attr("class");
                 // $("." + TRclass + "").hide();
                 //$("." + TRclass + "").eq(0).show();
                 $(obj).parent().parent().parent().next().find('tr[class!="TRCount"]').hide();
             }

         }



        var idTmr;  
          function  getExplorer() {  
              var explorer = window.navigator.userAgent ;  
              //ie  
              if (explorer.indexOf("MSIE") >= 0) {  
                  return 'ie';  
              }  
              //firefox  
              else if (explorer.indexOf("Firefox") >= 0) {  
                  return 'Firefox';  
              }  
              //Chrome  
              else if(explorer.indexOf("Chrome") >= 0){  
                  return 'Chrome';  
              }  
              //Opera  
              else if(explorer.indexOf("Opera") >= 0){  
                  return 'Opera';  
              }  
              //Safari  
              else if(explorer.indexOf("Safari") >= 0){  
                  return 'Safari';  
              }  
          }  
          function method5(tableid) {  
              if(getExplorer()=='ie')  
              {  
                  var curTbl = document.getElementById(tableid);  
                  var oXL = new ActiveXObject("Excel.Application");  
                  var oWB = oXL.Workbooks.Add();  
                  var xlsheet = oWB.Worksheets(1);  
                  var sel = document.body.createTextRange();  
                  sel.moveToElementText(curTbl);  
                  sel.select();  
                  sel.execCommand("Copy");  
                  xlsheet.Paste();  
                  oXL.Visible = true;  
    
                  try {  
                      var fname = oXL.Application.GetSaveAsFilename("Excel.xls", "Excel Spreadsheets (*.xls), *.xls");  
                  } catch (e) {  
                      print("Nested catch caught " + e);  
                  } finally {  
                      oWB.SaveAs(fname);  
                      oWB.Close(savechanges = false);  
                      oXL.Quit();  
                      oXL = null;  
                      idTmr = window.setInterval("Cleanup();", 1);  
                  }  
    
              }  
              else  
              {  
                  tableToExcel(tableid)  
              }  
          }  
          function Cleanup() {  
              window.clearInterval(idTmr);  
              CollectGarbage();  
          }  
          var tableToExcel = (function() {  
              var uri = 'data:application/vnd.ms-excel;base64,',  
                      template = '<html><head><meta charset="UTF-8"></head><body><table>{table}</table></body></html>',  
                      base64 = function(s) { return window.btoa(unescape(encodeURIComponent(s))) },  
                      format = function(s, c) {  
                          return s.replace(/{(\w+)}/g,  
                                  function(m, p) { return c[p]; }) }  
              return function(table, name) {  
                  if (!table.nodeType) table = document.getElementById(table)  
                  var ctx = {worksheet: name || 'Worksheet', table: table.innerHTML}  
                  window.location.href = uri + base64(format(template, ctx))  
              }  
          })()  


         


     </script>
</head>
<body>
    <form id="form1" runat="server">
     <div id="myDiv" >
    <div style="text-align:center">
   <h2>Department Budget Reports</h2>
     <table class="table table-condensed table-bordered">
                    <tr>
                    <th>Search：</th>
                    <td>
                   <asp:DropDownList runat="server" ID="dropYear">
                   </asp:DropDownList>
                     </td>
                     <th   class="CostCenter">CostCenter：</th>
                     <td class="CostCenter">
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
                     </td>
                     <td>
                       <asp:Button runat="server" ID="btnSearch" Text="Search" CssClass="btn" 
                            style="margin-left:20px" onclick="btnSearch_Click" /> 
                            <asp:HiddenField runat="server" ID="hidUserOwn" />
                            <asp:Button runat="server" Visible="false" ID="btnExport" Text="Export Excel" CssClass="btn" onclick="btnExport_Click" />
                            <input type="button" value="Export Excel" class="btn" onclick="method5('htmltable')" />
                     </td>
                    </tr>
                    </table>
          
          <table id="htmltable" class="table table-condensed table-bordered">
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
                     <tbody>
                    <tr>
                    <td style="background-color:Red; font-weight:bold;" colspan="15">
                   <asp:Label runat="server" ID="lbAccountType" Text='<%#Eval("AccountType") %>'></asp:Label>
                    </td>
                    </tr>
                   </tbody>
                   
                    <asp:Repeater runat="server" ID="RPCostCenter" onitemdatabound="RPCostCenter_ItemDataBound">
                    <ItemTemplate>
                    <tbody class="tbodyBudgetInfo">
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
                    <td style="text-align:right"><%#Eval("SubAccountDesc")%></td>
                    <td><%#Eval("Explain")%></td>
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
                    </ItemTemplate>
                    </asp:Repeater>
                    <tr class="TRCount">
                    <td>SumTotal：</td>
                    <td></td>
                    <td class="money">1</td>
                    <td class="money">2</td>
                    <td class="money">3</td>
                    <td class="money">4</td>
                    <td class="money">5</td>
                    <td class="money">6</td>
                    <td class="money">7</td>
                    <td class="money">8</td>
                    <td class="money">9</td>
                    <td class="money">10</td>
                    <td class="money">11</td>
                    <td class="money">12</td>
                    <td class="money"></td>
                    </tr>
                   </tbody>
                    <tr class="TRActual">
                    <td>Actual Data：</td>
                    <td></td>
                    <td><asp:Label runat="server" class="money" ID="lbJanMonth"></asp:Label> </td>
                    <td><asp:Label runat="server" class="money" ID="lbFebMonth"></asp:Label> </td>
                    <td><asp:Label runat="server" class="money" ID="lbMarMonth"></asp:Label> </td>
                    <td><asp:Label runat="server" class="money" ID="lbAprMonth"></asp:Label> </td>
                    <td><asp:Label runat="server" class="money" ID="lbMayMonth"></asp:Label> </td>
                    <td><asp:Label runat="server" class="money" ID="lbJunMonth"></asp:Label> </td>
                    <td><asp:Label runat="server" class="money" ID="lbJulyMonth"></asp:Label> </td>
                    <td><asp:Label runat="server" class="money" ID="lbAugMonth"></asp:Label> </td>
                    <td><asp:Label runat="server" class="money" ID="lbSepMonth"></asp:Label> </td>
                    <td><asp:Label runat="server" class="money" ID="lbOctMonth"></asp:Label> </td>
                     <td><asp:Label runat="server" class="money" ID="lbNovMonth"></asp:Label> </td>
                     <td><asp:Label runat="server" class="money" ID="lbDescMonth"></asp:Label> </td>
                     <td><asp:Label runat="server" class="money" ID="lbSubTotal"></asp:Label></td>
                    </tr>
                    </ItemTemplate>
                    </asp:Repeater>
                    <!-- 科目详情结束-->
                     </ItemTemplate>
                    </asp:Repeater>


                    <tr class="TableCount">
                    <td>Total：</td>
                    <td></td>
                    <td class="money">1</td>
                    <td class="money">2</td>
                    <td class="money">3</td>
                    <td class="money">4</td>
                    <td class="money">5</td>
                    <td class="money">6</td>
                    <td class="money">7</td>
                    <td class="money">8</td>
                    <td class="money">9</td>
                    <td class="money">10</td>
                    <td class="money">11</td>
                    <td class="money">12</td>
                    <td class="money"></td>
                    </tr>

                     <tr class="AcctualTableCount">
                    <td>Actual Total：</td>
                    <td></td>
                    <td class="money">1</td>
                    <td class="money">2</td>
                    <td class="money">3</td>
                    <td class="money">4</td>
                    <td class="money">5</td>
                    <td class="money">6</td>
                    <td class="money">7</td>
                    <td class="money">8</td>
                    <td class="money">9</td>
                    <td class="money">10</td>
                    <td class="money">11</td>
                    <td class="money">12</td>
                    <td class="money"></td>
                    </tr>
                    </table>
                     
    </div>
    
    </div>
    </form>
</body>
</html>
