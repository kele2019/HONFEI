<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ReviewPage.aspx.cs" Inherits="Presale.Process.BudgetRequestAndApproval.ReviewPage" %>

 <%@ Register Src="/Modules/Ultimus.UWF.Form.ProcessControl/UserInfo.ascx" TagName="UserInfo" TagPrefix="ui" %>
<%@ Register Src="/Modules/Ultimus.UWF.Form.ProcessControl/ApprovalHistory.ascx" TagName="ApprovalHistory" TagPrefix="ah" %>
<%@ Register Src="/Modules/Ultimus.UWF.Form.ProcessControl/Attachments.ascx" TagName="Attachments" TagPrefix="attach" %>
<%@ Register Src="/Modules/Ultimus.UWF.Form.ProcessControl/ButtonList.ascx" TagName="ButtonList" TagPrefix="btn" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
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

            var StepName = decodeURI(request('StepName'));
            switch (StepName) {
                case "FIN Approve2":
                    StepName = "CTO Approve";
                    break;
                case "FIN Approve3":
                    StepName = "GM Approve";
                    break;
                case "FIN Approve4":
                    StepName = "IT Approve";
                    break;
                case "FIN Approve5":
                    StepName = "Admin Approve";
                    break;
                case "FIN Approve6":
                    StepName = "Quality Approve";
                    break;
                case "FIN Approve7":
                    StepName = "HR Approve";
                    break;
                case "FIN Approve8":
                    StepName = "Engineering Approve";
                    break;
                case "FIN Approve9":
                    StepName = "FIN Approve";
                    break;
                case "FIN Approve1":
                    StepName = "OP Approve";
                    break;
                default:
                    break;
            }

            $("#fileinfo").find("tr").each(function () {
                var CStepName = $.trim($(this).find("td").eq(2).html());
                if (StepName != CStepName)
                    $(this).hide();
            });

        });
        function TotalAmount() {
            var SubTotal = 0;
            $("#tdbodytable").find("tr").each(function () {
                if ($(this).attr("id") == "MonthTable") {
                    SubTotal += $(this).find("td").eq(15).find("input").val() - 0;
                
                }
            });
            $("#TotalAmount").text(formatNumber(SubTotal.toFixed(2), 2, 1));
        }
        function CheckSumtotal(obj) {
            var CurrentTR = $(obj).parent().parent();
            if ($(obj).val() != "")
                $(obj).val(($(obj).val() - 0).toFixed(2));
            var Jan = $(CurrentTR).find("td").eq(3).find("input").val() - 0;
            var Feb = $(CurrentTR).find("td").eq(4).find("input").val() - 0;
            var Mar = $(CurrentTR).find("td").eq(5).find("input").val() - 0;
            var Apr = $(CurrentTR).find("td").eq(6).find("input").val() - 0;
            var May = $(CurrentTR).find("td").eq(7).find("input").val() - 0;
            var Jun = $(CurrentTR).find("td").eq(8).find("input").val() - 0;
            var Jul = $(CurrentTR).find("td").eq(9).find("input").val() - 0;
            var Aug = $(CurrentTR).find("td").eq(10).find("input").val() - 0;
            var Sep = $(CurrentTR).find("td").eq(11).find("input").val() - 0;
            var Oct = $(CurrentTR).find("td").eq(12).find("input").val() - 0;
            var Nov = $(CurrentTR).find("td").eq(13).find("input").val() - 0;
            var Dec = $(CurrentTR).find("td").eq(14).find("input").val() - 0;

            var Subtotal = Jan + Feb + Mar + Apr + May + Jun + Jul + Aug + Sep + Oct + Nov + Dec;
            $(CurrentTR).find("td").eq(15).find("span").text(formatNumber(Subtotal.toFixed(2), 2, 1));
            $(CurrentTR).find("td").eq(15).find("input").val(Subtotal);
            TotalAmount();
        }
        function SelectCheckBox(obj) {
            var CurrentTR = $(obj).parent().parent();
            if ($(obj).attr("checked")) {
                var Jan = $(CurrentTR).find("td").eq(3).find("input").val();
                $(CurrentTR).find("td").eq(4).find("input").val(Jan);
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
            }
            else {
                $(CurrentTR).find("td").eq(4).find("input").val("");
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
            }
            CheckSumtotal(obj);
        }
        function submitPageReview() {
            $("#ButtonList1_btnSubmit").click();
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
                        <asp:Label runat="server" ID="lbDept" Width="94%" ></asp:Label>
                        </td>
                    </tr>
                </table>
            </div>
            <div class="row">
          

            <table  class="table table-condensed table-bordered">
            <tr>
            <th width="2%">No</th>
            <th width="150px">Item</th>
             <th width="100px">Explain</th>
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
            <th width="100px;">SubTotal</th>
          <%--  <th>Del</th>--%>
            </tr>
              <tbody id="tdbodytable">

                  <asp:Repeater runat="server" ID="RPAccountType">
              <ItemTemplate>
              <tr>
              <td colspan="17" style=" background:red">
             <asp:Label runat="server" ID="lbAccountType" Font-Bold="true" Text='<%#Eval("AccountType") %>'></asp:Label>


              <asp:Repeater runat="server" ID="RPList" OnItemDataBound="RPList_ItemDataBound">
              <ItemTemplate>
               <tr>
              <td colspan="16">BudgetCode：<span style="color:Red"><%#Eval("BugetAccountNo")%></span>&nbsp;&nbsp;
              BudgetDesc：
             <span style="color:Red"> <%#Eval("BugetAccountDesc")%></span>
              <asp:HiddenField runat="server" ID="hidAccountID" Value='<%#Eval("ID") %>' /> 
               <asp:HiddenField runat="server" ID="hdlbAccountType" Value='<%#Eval("AccountType") %>' /> 
              </td>
              </tr>
              <asp:Repeater runat="server" ID="read_detail_PROC_Budget_DT" >
            <ItemTemplate>
            <tr id="MonthTable">
            <td>
             <asp:TextBox ID="read_FORMID" Text='<%#Eval("FORMID") %>' runat="server" Style="display: none"></asp:TextBox>
             <asp:Label ID="read_ROWID" Text='<%# Container.ItemIndex+1%>' runat="server"></asp:Label>
             <asp:HiddenField runat="server" ID="hidBugetAccountID" Value='<%#Eval("BugetAccountID") %>' />
             <asp:HiddenField runat="server" ID="hidID" Value='<%#Eval("ID") %>' />

            </td>
            <td >
            <asp:Label runat="server" onfocus="this.blur()" ID="read_Item" Width="90%" Text='<%#Eval("Item") %>'></asp:Label>
            </td>
              <td>
             <asp:Label runat="server" ID="read_Explain"   Text='<%#Eval("Explain") %>'></asp:Label>
             </td>
            <td>
            <asp:Label runat="server" ID="read_Jan"  CssClass="money"  Text='<%#Eval("Jan") %>'></asp:Label>
            </td>
             <td>
            <asp:Label runat="server" ID="read_Feb" CssClass="money"  Text='<%#Eval("Feb") %>'></asp:Label>
            </td>                                      
             <td>                                    
            <asp:Label runat="server" ID="read_Mar"  CssClass="money"  Text='<%#Eval("Mar") %>'></asp:Label>
            </td>                                    
             <td>                                     
            <asp:Label runat="server" ID="read_Apr" CssClass="money"   Text='<%#Eval("Apr") %>'></asp:Label>
            </td>                                    
             <td>                                    
            <asp:Label runat="server" ID="read_May" CssClass="money"   Text='<%#Eval("May") %>'></asp:Label>
            </td>                                      
             <td>                                      
            <asp:Label runat="server" ID="read_Jun"  CssClass="money"  Text='<%#Eval("Jun") %>'></asp:Label>
            </td>                                      
             <td>                                      
            <asp:Label runat="server" ID="read_July"  CssClass="money" Text='<%#Eval("July") %>'></asp:Label>
            </td>                                     
             <td>                                     
            <asp:Label runat="server" ID="read_Aug" CssClass="money"  Text='<%#Eval("Aug") %>'></asp:Label>
            </td>                                     
             <td>                                     
            <asp:Label runat="server" ID="read_Sep"  CssClass="money"  Text='<%#Eval("Sep") %>'></asp:Label>
            </td>                                      
             <td>                                      
            <asp:Label runat="server" ID="read_Oct"  CssClass="money"  Text='<%#Eval("Oct") %>'></asp:Label>
            </td>                                     
             <td>                                     
            <asp:Label runat="server" ID="read_Nov"  CssClass="money" Text='<%#Eval("Nov") %>'></asp:Label>
            </td>                                      
            <td>                                       
            <asp:Label runat="server" ID="read_Dec" CssClass="money"   Text='<%#Eval("Dec") %>'></asp:Label>
            </td>
            <td style="word-wrap:word-break:break-all;">
            <span style="width:10px;word-break:break-all;" class="money" ><%#Eval("SubTotal") %></span>
           <input type="hidden" value='<%#Eval("SubTotal") %>' />
            </td>
            </tr>
            </ItemTemplate>
            </asp:Repeater>
              </ItemTemplate>
              </asp:Repeater>

              </td>
              </tr>
              </ItemTemplate>
              </asp:Repeater>
            </tbody>

            <tr>
            <td colspan="14">
            </td>
            <td>Total：</td>
            <td>
             <span  class="money" id="TotalAmount"></span>
            </td>
            </tr>
            </table>
            </div>
            <div class="row" style="display:block;">
                <attach:attachments id="Attachments1" ReadOnly="true" runat="server"></attach:attachments>
            </div>
            <div class="row">
             <ah:approvalhistory id="ApprovalHistory1" showaction="true" runat="server"></ah:approvalhistory>
            </div>
          <%--  <div class="row" style="margin-left:50%">
            <input type="button" value="Complete" class="btn" onclick="submitPageReview()"  />
             </div>--%>
            
    </div>
      <btn:buttonlist id="ButtonList1" runat="server"></btn:buttonlist>
    <div style="display:none">
   
    <asp:HiddenField runat="server" ID="hidFormID" />
    <asp:HiddenField runat="server" ID="hidCostCenter" />
    </div>
    </form>
</body>
</html>
