<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Approval.aspx.cs" Inherits="Presale.Process.BudgetRequestAndApproval.Approval" %>

<%@ Register Src="/Modules/Ultimus.UWF.Form.ProcessControl/UserInfo.ascx" TagName="UserInfo" TagPrefix="ui" %>
<%@ Register Src="/Modules/Ultimus.UWF.Form.ProcessControl/ApprovalHistory.ascx" TagName="ApprovalHistory" TagPrefix="ah" %>
<%@ Register Src="/Modules/Ultimus.UWF.Form.ProcessControl/Attachments.ascx" TagName="Attachments" TagPrefix="attach" %>
<%@ Register Src="/Modules/Ultimus.UWF.Form.ProcessControl/ButtonList.ascx" TagName="ButtonList" TagPrefix="btn" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
 <meta http-equiv="X-UA-Compatible"  content="IE=EmulateIE9" />
    <title>Budget Application</title>
   <script type="text/javascript" src="/Assets/js/common.js"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            $(".container").attr("style", "width:1600px");
            $(".td-label").attr("style", "width:15%");
            $(".td-content").attr("style", "width:35%");
            TotalAmount();
            if (request("type").toLocaleLowerCase() == "myapproval") {
                DisableWebForm();
                $(":input[type=checkbox]").each(function () {
                    $(this).attr("disabled", "disabled");
                });
            }
            if ($("#read_BudgetType").text() != "Full Year") {
                // $(".TRActual").show();
                $(":input[type=checkbox]").each(function () {
                    $(this).attr("disabled", "disabled");
                });

                if ($("#read_BudgetType").text() == "4+8")
                    $(".input4").attr("onfocus", "this.blur()");
                if ($("#read_BudgetType").text() == "7+5")
                    $(".input7").attr("onfocus", "this.blur()");
            }
            var StepName = $("#hidStepName").val(); // decodeURI(request('StepName'));
            $("#fileinfo").find("tr").each(function () {
                var CStepName = $.trim($(this).find("td").eq(2).html());
//                console.log(CStepName);
//                console.log(StepName);
//                console.log(CStepName);
                if (StepName != CStepName)
                    $(this).hide();
            });
        });
         
        function TotalAmount() {
             var SubTotal1 = 0;
             $("#tdbodytable").find("tr").each(function () {
                 if ($(this).attr("id") == "MonthTable") {


                     var Jan = $(this).find("td").eq(4).find("input").val() - 0;
                     if(Jan<=0)
                     $(this).find("td").eq(4).find("input").val("0.00");
                     var Feb = $(this).find("td").eq(5).find("input").val() - 0;
                     if (Feb <= 0)
                         $(this).find("td").eq(5).find("input").val("0.00");
                     var Mar = $(this).find("td").eq(6).find("input").val() - 0;
                     if (Mar <= 0)
                         $(this).find("td").eq(6).find("input").val("0.00");
                     var Apr = $(this).find("td").eq(7).find("input").val() - 0;
                     if (Apr <= 0)
                         $(this).find("td").eq(7).find("input").val("0.00");

                     var May = $(this).find("td").eq(8).find("input").val() - 0;
                     if (May <= 0)
                         $(this).find("td").eq(8).find("input").val("0.00");
                     var Jun = $(this).find("td").eq(9).find("input").val() - 0;
                     if (Jun <= 0)
                         $(this).find("td").eq(9).find("input").val("0.00");
                     var Jul = $(this).find("td").eq(10).find("input").val() - 0;
                     if (Jul <= 0)
                         $(this).find("td").eq(10).find("input").val("0.00");
                     var Aug = $(this).find("td").eq(11).find("input").val() - 0;
                     if (Aug <= 0)
                         $(this).find("td").eq(11).find("input").val("0.00");

                     var Sep = $(this).find("td").eq(12).find("input").val() - 0;
                     if (Sep <= 0)
                         $(this).find("td").eq(12).find("input").val("0.00");
                     var Oct = $(this).find("td").eq(13).find("input").val() - 0;
                     if (Oct <= 0)
                         $(this).find("td").eq(13).find("input").val("0.00");
                     var Nov = $(this).find("td").eq(14).find("input").val() - 0;
                     if (Nov <= 0)
                         $(this).find("td").eq(14).find("input").val("0.00");
                     var Dec = $(this).find("td").eq(15).find("input").val() - 0;
                     if (Dec <= 0)
                         $(this).find("td").eq(15).find("input").val("0.00");

                     var Subtotal = Jan + Feb + Mar + Apr + May + Jun + Jul + Aug + Sep + Oct + Nov + Dec;
                     $(this).find("td").eq(16).find("span").text(formatNumber(Subtotal.toFixed(2), 2, 1));
                     $(this).find("td").eq(16).find("input").val(Subtotal);

                     SubTotal1 += $(this).find("td").eq(16).find("input").val() - 0;
                 }

             });
            $("#TotalAmount").text(formatNumber(SubTotal1.toFixed(2), 2, 1));
        }


        function CheckSumtotal(obj) {
            var CurrentTR = $(obj).parent().parent();
            if($(obj).val()!="")
            $(obj).val(($(obj).val()-0).toFixed(2));
//            var Jan = $(CurrentTR).find("td").eq(4).find("input").val() - 0;
//            var Feb = $(CurrentTR).find("td").eq(5).find("input").val() - 0;
//            var Mar = $(CurrentTR).find("td").eq(6).find("input").val() - 0;
//            var Apr = $(CurrentTR).find("td").eq(7).find("input").val() - 0;
//            var May = $(CurrentTR).find("td").eq(8).find("input").val() - 0;
//            var Jun = $(CurrentTR).find("td").eq(9).find("input").val() - 0;
//            var Jul = $(CurrentTR).find("td").eq(10).find("input").val() - 0;
//            var Aug = $(CurrentTR).find("td").eq(11).find("input").val() - 0;
//            var Sep = $(CurrentTR).find("td").eq(12).find("input").val() - 0;
//            var Oct = $(CurrentTR).find("td").eq(13).find("input").val() - 0;
//            var Nov = $(CurrentTR).find("td").eq(14).find("input").val() - 0;
//            var Dec = $(CurrentTR).find("td").eq(15).find("input").val() - 0;

//            var Subtotal = Jan + Feb + Mar + Apr + May + Jun + Jul + Aug + Sep + Oct + Nov + Dec;
//            $(CurrentTR).find("td").eq(16).find("span").text(formatNumber(Subtotal.toFixed(2), 2, 1));
//            $(CurrentTR).find("td").eq(16).find("input").val(Subtotal);
            TotalAmount();
        }
        function SelectCheckBox(obj) {
            var CurrentTR = $(obj).parent().parent();
            if ($(obj).attr("checked")) {
                var Jan = $(CurrentTR).find("td").eq(4).find("input").val();
                $(CurrentTR).find("td").eq(5).find("input").val(Jan);
                $(CurrentTR).find("td").eq(6).find("input").val(Jan);
                $(CurrentTR).find("td").eq(7).find("input").val(Jan);
                $(CurrentTR).find("td").eq(8).find("input").val(Jan);
                $(CurrentTR).find("td").eq(9).find("input").val(Jan);
                $(CurrentTR).find("td").eq(10).find("input").val(Jan);
                $(CurrentTR).find("td").eq(11).find("input").val(Jan);
                $(CurrentTR).find("td").eq(12).find("input").val(Jan);
                $(CurrentTR).find("td").eq(13).find("input").val(Jan);
                $(CurrentTR).find("td").eq(14).find("input").val(Jan);
                $(CurrentTR).find("td").eq(15).find("input").val(Jan);
            }
            else {
                $(CurrentTR).find("td").eq(5).find("input").val("");
                $(CurrentTR).find("td").eq(6).find("input").val("");
                $(CurrentTR).find("td").eq(7).find("input").val("");
                $(CurrentTR).find("td").eq(8).find("input").val("");
                $(CurrentTR).find("td").eq(9).find("input").val("");
                $(CurrentTR).find("td").eq(10).find("input").val("");
                $(CurrentTR).find("td").eq(11).find("input").val("");
                $(CurrentTR).find("td").eq(12).find("input").val("");
                $(CurrentTR).find("td").eq(13).find("input").val("");
                $(CurrentTR).find("td").eq(14).find("input").val("");
                $(CurrentTR).find("td").eq(15).find("input").val("");
            }
            CheckSumtotal(obj);
        }
        function SubimtFun() {
         $("#ButtonList1_btnSubmit").val("");
        $("#ButtonList1_btnSubmit").click();
        }

        function submitPageReview() {
        $("#ButtonList1_btnSubmit").click();
        }
         
        function submitSave() {
            $("#hidFlag").val("Save");
            $("#btnSave").click();
        }
        function CheckDataClearZero(obj) {
            if ( $(obj).val()== "0.00")
                $(obj).val("");
        }
        function myscroll(obj) {
 
              var viewH = document.getElementById('myTable').scrollTop;
            if (viewH > 15) { 
            $("#tableHeadTemp").show();
            }
            else
            {
                $("#tableHeadTemp").hide();
            }         
            //$("#tableHeadTemp").show();
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div id="myDiv" class="container">
            <div class="row">
                <ui:userinfo id="UserInfo1" processtitle="Budget Request and approval" processprefix="BUD" tablename="PROC_Budget" 
                    runat="server"  ></ui:userinfo>
            </div>
            <div class="row">
                <p style="font-weight:bold;">Summary time（"<span style=" background:red;">&nbsp;</span>" must write） </p>
                <table class="table table-condensed table-bordered">
                    <tr>
                        <td class="td-label">
                        <p style="text-align:center">Year</p>
                        </td>
                        <td class="td-content" colspan="3" >
                        <asp:Label runat="server" ID="read_Year" Width="94%" ></asp:Label>
                        </td>
                        <td class="td-label"> 
                       <span style="height:30px; float:left;">&nbsp;</span>
                        <p style="text-align:center">Budget type</p>
                        </td>
                        <td class="td-content"  colspan="3" >
                            <asp:Label runat="server"  ID="read_BudgetType" ></asp:Label>
                        </td>
                    </tr>
                    <tr>
                      <td class="td-label">
                        <p style="text-align:center">Submit Department</p>
                        </td>
                        <td class="td-content" colspan="7" >
                       <asp:Label runat="server" ID="lbDept"></asp:Label>
                        </td>
                    </tr>
                </table>
            </div>


              <table id="tableHeadTemp" style="display:none; width:99%; margin-bottom:5px;"  class="table table-condensed table-bordered"  >
              <tr>
            <th width="2%">No</th>
            <th width="165px">Item</th>
            <th width="130px">Explain</th>
            <th width="20px" ></th>
            <th width="100px">Jan</th>
            <th width="100px">Feb</th>
            <th width="100px">Mar</th>
            <th width="100px">Apr</th>
            <th width="100px">May</th>
            <th width="100px">Jun</th>
            <th width="100px">Jul</th>
            <th width="100px">Aug</th>
            <th width="100px">Sep</th>
            <th width="100px">Oct</th>
            <th width="100px">Nov</th>
            <th width="100px" >Dec</th>
            <th width="10px;">SubTotal</th>
          <%--  <th>Del</th>--%>
            </tr>
            </table>

            <div class="row" onscroll="myscroll()" id="myTable" style="height:500px; overflow-y:scroll" >
          
           
            <table  class="table table-condensed table-bordered"  >
              <tr>
            <th width="2%">No</th>
            <th width="150px">Item</th>
            <th width="130px">Explain</th>
            <th width="10px" ></th>
            <th width="100px">Jan</th>
            <th width="100px">Feb</th>
            <th width="100px">Mar</th>
            <th width="100px">Apr</th>
            <th width="100px">May</th>
            <th width="100px">Jun</th>
            <th width="100px">Jul</th>
            <th width="100px">Aug</th>
            <th width="100px">Sep</th>
            <th width="100px">Oct</th>
            <th width="100px">Nov</th>
            <th width="100px" >Dec</th>
            <th width="10px;">SubTotal</th>
          <%--  <th>Del</th>--%>
            </tr>
              <tbody id="tdbodytable" >
              <asp:Repeater runat="server" ID="RPAccountType">
              <ItemTemplate>
              <tr>
              <td colspan="17" style=" background:red">
             <asp:Label runat="server" ID="lbAccountType" Font-Bold="true" Text='<%#Eval("AccountType") %>'></asp:Label>
               <asp:Repeater runat="server" ID="RPList" OnItemDataBound="RPList_ItemDataBound">
              <ItemTemplate>
               <tr>
              <td colspan="17">BudgetCode：<span style="color:Red"><%#Eval("BugetAccountNo")%></span>&nbsp;&nbsp;
              BudgetDesc：
             <span style="color:Red"> <%#Eval("BugetAccountDesc")%></span>
              <asp:HiddenField runat="server" ID="hidAccountID" Value='<%#Eval("ID") %>' /> 
              <asp:HiddenField runat="server" ID="hdlbAccountType" Value='<%#Eval("AccountType") %>' /> 

              
              </td>
            
              </tr>
              <asp:Repeater runat="server" ID="fld_detail_PROC_Budget_DT" >
            <ItemTemplate>
            <tr id="MonthTable">
            <td>
             <asp:TextBox ID="fld_FORMID" Text='<%#Eval("FORMID") %>' runat="server" Style="display: none"></asp:TextBox>
             <asp:Label ID="fld_ROWID" Text='<%# Container.ItemIndex+1%>' runat="server"></asp:Label>
             <asp:HiddenField runat="server" ID="hidBugetAccountID" Value='<%#Eval("BugetAccountID") %>' />
             <asp:HiddenField runat="server" ID="hidID" Value='<%#Eval("ID") %>' />
            </td>
            <td >
            <asp:Label runat="server" onfocus="this.blur()" ID="fld_Item"   Text='<%#Eval("Item") %>'></asp:Label>
            </td>
             <td>
             <asp:TextBox runat="server" ID="fld_Explain"  MaxLength="50" Width="90%" Text='<%#Eval("Explain") %>'></asp:TextBox>
             </td>
            <td> <input type="checkbox" value="" onclick="SelectCheckBox(this)" /> </td>
            <td>
            <asp:TextBox runat="server" ID="fld_Jan" CssClass="input4 input7"  Width="80%"  money="money" onclick="CheckDataClearZero(this)"  onblur="CheckSumtotal(this)" Text='<%#Eval("Jan") %>'></asp:TextBox>
            </td>
             <td>
            <asp:TextBox runat="server" ID="fld_Feb"  CssClass="input4 input7"  Width="80%"  money="money" onclick="CheckDataClearZero(this)" onblur="CheckSumtotal(this)" Text='<%#Eval("Feb") %>'></asp:TextBox>
            </td>                                      
             <td>                                    
            <asp:TextBox runat="server" ID="fld_Mar"  CssClass="input4 input7"  Width="80%"  money="money"  onclick="CheckDataClearZero(this)" onblur="CheckSumtotal(this)" Text='<%#Eval("Mar") %>'></asp:TextBox>
            </td>                                    
             <td>                                     
            <asp:TextBox runat="server" ID="fld_Apr"  CssClass="input4 input7"  Width="80%"  money="money"  onclick="CheckDataClearZero(this)" onblur="CheckSumtotal(this)" Text='<%#Eval("Apr") %>'></asp:TextBox>
            </td>                                    
             <td>                                    
            <asp:TextBox runat="server" ID="fld_May"   CssClass="input7" Width="80%"  money="money"  onclick="CheckDataClearZero(this)" onblur="CheckSumtotal(this)" Text='<%#Eval("May") %>'></asp:TextBox>
            </td>                                      
             <td>                                      
            <asp:TextBox runat="server" ID="fld_Jun"  CssClass="input7" Width="80%"  money="money" onclick="CheckDataClearZero(this)"  onblur="CheckSumtotal(this)" Text='<%#Eval("Jun") %>'></asp:TextBox>
            </td>                                      
             <td>                                      
            <asp:TextBox runat="server" ID="fld_July" CssClass="input7" Width="80%"  money="money"  onblur="CheckSumtotal(this)" Text='<%#Eval("July") %>'></asp:TextBox>
            </td>                                     
             <td>                                     
            <asp:TextBox runat="server" ID="fld_Aug"  Width="80%"  money="money" onclick="CheckDataClearZero(this)"  onblur="CheckSumtotal(this)" Text='<%#Eval("Aug") %>'></asp:TextBox>
            </td>                                     
             <td>                                     
            <asp:TextBox runat="server" ID="fld_Sep"  Width="80%"  money="money" onclick="CheckDataClearZero(this)" onblur="CheckSumtotal(this)" Text='<%#Eval("Sep") %>'></asp:TextBox>
            </td>                                      
             <td>                                      
            <asp:TextBox runat="server" ID="fld_Oct"  Width="80%"  money="money" onclick="CheckDataClearZero(this)"  onblur="CheckSumtotal(this)"  Text='<%#Eval("Oct") %>'></asp:TextBox>
            </td>                                     
             <td>                                     
            <asp:TextBox runat="server" ID="fld_Nov"  Width="80%"  money="money" onclick="CheckDataClearZero(this)"  onblur="CheckSumtotal(this)" Text='<%#Eval("Nov") %>'></asp:TextBox>
            </td>                                      
            <td>                                       
            <asp:TextBox runat="server" ID="fld_Dec"  Width="80%"  money="money" onclick="CheckDataClearZero(this)"  onblur="CheckSumtotal(this)" Text='<%#Eval("Dec") %>'></asp:TextBox>
            </td>
            <td style="word-wrap:word-break:break-all;">
            <span style="width:10px;word-break:break-all;" ><%#Eval("SubTotal") %></span>
             <asp:HiddenField runat="server" ID="fld_SubTotal" Value='<%#Eval("SubTotal").ToString()==""?"0":Eval("SubTotal") %>' />
            </td>
            <%--<td>
               <asp:Button ID="btnDelete" runat="server" Text="Del" CssClass="btn" CommandName="del"
               ClientIDMode="Static" OnClientClick="return confirm('确认删除/Confirm Del？')" />
            </td>--%>
            </tr>
            </ItemTemplate>
            </asp:Repeater>

           <%-- <tr class="TRActual" style="display:none">
            <td colspan="3">Actual Data：</td>
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
            </tr>--%>
              </ItemTemplate>
              </asp:Repeater>
               </td>
               </tr>
              </ItemTemplate>
              </asp:Repeater>
              
            </tbody>

            <tr>
            <td colspan="15">
               <%-- <asp:Button ID="btnAdd" runat="server" Text="Add" CssClass="btn" CausesValidation="false"
                OnClick="btnAdd_Click"   />--%>
            </td>
            <td>Total：</td>
            <td>
             <span  class="money" id="TotalAmount"></span>
              
            </td>
            </tr>
            </table>
             
            </div>
            <div class="row" style="display:block;">
                <attach:attachments id="Attachments1" runat="server"></attach:attachments>
            </div>
            <div class="row">
             <ah:approvalhistory id="ApprovalHistory1" showaction="true" runat="server"></ah:approvalhistory>
            </div>
            <div class="row" style="margin-left:50%">
            <input type="button" value="Submit" class="btn" onclick="submitPageReview()"  />
            <input type="button" value="Save" class="btn" onclick="submitSave()"   />
          
             </div>
    </div>
    <div style="display:none">
    <asp:Button runat="server" ID="btnSave" Text="保存数据"  onclick="btnSave_Click" />
    <btn:buttonlist id="ButtonList1" runat="server"></btn:buttonlist>
    <asp:HiddenField runat="server" ID="hidFormID" />
    <asp:HiddenField runat="server" ID="hidCostCenter" />
    <asp:HiddenField runat="server" ID="hidFlag" />
    <asp:HiddenField runat="server" ID="hidBudgetYear" />
    <asp:HiddenField runat="server" ID="hidBudgetType" />
     <asp:HiddenField runat="server" ID="hidStepName" />
    </div>
    </form>
</body>
</html>
