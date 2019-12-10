﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AskApproval.aspx.cs" Inherits="Presale.Process.PersonalExpense.AskApproval" %>

 
<%@ Register Src="/Modules/Ultimus.UWF.Form.ProcessControl/UserInfo.ascx" TagName="UserInfo" TagPrefix="ui" %>
<%@ Register Src="/Modules/Ultimus.UWF.Form.ProcessControl/ApprovalHistory.ascx" TagName="ApprovalHistory" TagPrefix="ah" %>
<%@ Register Src="/Modules/Ultimus.UWF.Form.ProcessControl/Attachments.ascx" TagName="Attachments" TagPrefix="attach" %>
<%@ Register Src="/Modules/Ultimus.UWF.Form.ProcessControl/ButtonList.ascx" TagName="ButtonList" TagPrefix="btn" %>
<%@ Import Namespace="Ultimus.UWF.Common.Logic" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>个人报销申请/Personal Expense Request</title>
    <script type="text/javascript" src="/Assets/js/common.js"></script>
    <script type="text/javascript">

        $(document).ready(function () {
            $("#UserInfo1_fld_APPLICANT").parent().parent().parent().parent().attr("style", "margin-bottom:0px");
            //$("#UserInfo1_fld_PROCESSSUMMARY").val($("#UserInfo1_fld_APPLICANT").text() + "-出差报销单");
            $("#ButtonList1_btnAsk").hide();
            $("#ButtonList1_var_AskForAccount").val("No");
            $("#ButtonList1_btnBack").hide();
            changeBorrow();
            if (request("Type") == "myapproval") {
                $("#btnDiv").hide();
            }
           
        });
        function changeBorrow() {
            var BrrowYes = $("#read_BrrowYes").attr("checked");
            if (BrrowYes) {
                $("#trCash").show();
            }
            else {
                $("#trCash").hide();
            }
        }
        function submitPageReview(obj) {
            $("#ButtonList1_btnSubmit").val("确定提交/Confirm Submit？");
            $("#ApprovalHistory1_txtSpecialAction").val("Inquire");
            $("#ButtonList1_btnSubmit").click();
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div id="myDiv" class="container">
        <div class="row">
            <ui:userinfo id="UserInfo1" processtitle="个人报销流程（Personal Expense Request）" processprefix="PE" tablename="PROC_PersonalExpense"
                tablenamedetail="PROC_PersonalExpense_DT" runat="server"></ui:userinfo>
                <table class="table table-condensed table-bordered"   >
                 <tr>
                <td class="td-label">事由 Description：</td>
                <td colspan="3">
                <asp:Label runat="server" ID="read_Description"  style="margin-bottom:0px" ></asp:Label>
                </td>
                </tr>
       </table>
        </div>
        <div class="row">
            <table  class="table table-condensed table-bordered">
             <tr>
                    <td class="banner" colspan="11">
                       报销明细 Expense Detail
                    </td>
                </tr>
             <tr>
            <th width="1%">序号<br />No</th>
            <th width="50px" >币种<br />Currency</th>
            <th width="70px">开始日期 <br />Start Date</th>
            <th width="70px">结束日期 <br />End Date</th>
          <th  width="100px">承担部门<br />Responsible-Cost Center</th>
             <th width="150px" >类别<br /> Expense Type</th>
             <th width="250px" >项目<br />Item</th>
              <th width="40px">金额<br />Amount</th>
             <th>说明<br />Remarks</th>
             <th width="30px">汇率<br />Ex. Rate</th>
             <th width="40px">小计<br />Sub-total</th>
            </tr>
            <tbody id="detailtable">
            <asp:Repeater runat="server" ID="fld_detail_PROC_PersonalExpense_DT">
            <ItemTemplate>
            <tr>
            <td> <asp:TextBox ID="fld_FORMID" Text='<%#Eval("FORMID") %>' runat="server" Style="display: none"></asp:TextBox>
                 <asp:Label ID="fld_ROWID" Text='<%# Container.ItemIndex+1%>' runat="server"></asp:Label></td>
            <td  style="text-align:center">
             <asp:Label runat="server" ID="read_Currency"   Text='<%#Eval("Currency") %>'></asp:Label>
            </td>
            <td>
            <asp:Label runat="server" ID="read_PaymentDate"    Text='<%# String.IsNullOrEmpty(Eval("PaymentDate").ToString())? "":DateTime.Parse(Eval("PaymentDate").ToString()).ToString("yyyy-MM-dd")  %>'  ></asp:Label>
            </td>
            <td>
            <asp:Label runat="server" ID="read_PaymentEndDate" Text='<%# String.IsNullOrEmpty(Eval("PaymentEndDate").ToString())? "":DateTime.Parse(Eval("PaymentEndDate").ToString()).ToString("yyyy-MM-dd")  %>' ></asp:Label>
            </td>
              <td>
            <asp:Label runat="server" ID="fld_CostCenter"   Text='<%#Eval("CostCenter").ToString() %>'  ></asp:Label>
            </td>
            <td>
            <asp:Label runat="server" Text='<%#Eval("ExpenseType") %>'   ID="read_ExpenseType"></asp:Label>
            </td>
            <td>
            <asp:Label runat="server"    ID="read_ExpenseItem" Text='<%#Eval("ExpenseItem")%>'></asp:Label>
            </td>
            <td>
            <asp:Label runat="server" ID="read_subAmount"  Text='<%#Eval("subAmount")%>'   ></asp:Label>
            </td>
            
            <td><asp:Label runat="server"  ID="read_Remarks"    Text='<%#Eval("Remarks") %>'></asp:Label></td>
            <td><asp:Label runat="server"  ID="read_Rate"   Text='<%# Eval("Rate").ToString()==""?"1":Eval("Rate") %>'></asp:Label></td>
            <td><asp:Label runat="server"  ID="read_SubTotal"   Text='<%#Eval("SubTotal") %>'></asp:Label></td>
            </tr>
             </ItemTemplate>
            </asp:Repeater>
            </tbody>
            <tr>
            <td colspan="10" style="text-align:right">
            合计/Total：</td>
            <td  style="text-align:center">
            <asp:Label runat="server"  ID="read_CountSub"></asp:Label>
            </td>
            </tr>
            </table>
            </div>
             <div class="row" style="display:block">
             <table class="table table-condensed table-bordered">
                <tr>
                    <td class="banner" colspan="4">
                        借款信息 Borrow Money Info
                    </td>
                </tr>
                <tr>
                <td class="td-label" >
                是否有借款<br />Cash Advanced Before：
                </td>
                <td class="td-content" >
                <asp:RadioButton runat="server" ID="read_BrrowYes"  Enabled="false"  />是/Yes &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                <asp:RadioButton runat="server" ID="read_BrrowNo"   Enabled="false" />否/No
                </td>
                <td class="td-label">
                币种&nbsp;&nbsp;<br /> Currency：
                </td>
                <td>
                <asp:Label runat="server"  ID="read_Currency" Text="RMB"></asp:Label>
                </td>
                </tr>
                <tr id="trCash">
                 <td  class="td-label">
            借款单号 <br />Cash Advance No.：                                      
                </td>
                <td  >
                <asp:Label runat="server" ID="read_CashAdvanceNo"></asp:Label>
                </td>
                <td class="td-label">
                借款金额 (人民币)<br />Cash Advance (RMB)：                                     
                </td>
                <td>
                <asp:Label runat="server" ID="read_BorrowsAmount"  ></asp:Label>
                </td>
                
                </tr>
               
            </table>

        </div>
        <div class="row">
            <attach:attachments id="Attachments1" ReadOnly="false" runat="server"></attach:attachments>
        </div>
        <div class="row">
            <ah:approvalhistory id="ApprovalHistory1" showaction="true" runat="server"></ah:approvalhistory>
        </div>
        <div class="row">
        </div>
    </div>
       
          <div id="btnDiv" runat="server"   >
        <table style="width: 100%;" >
        <tr  width="500">
        <td align="center"  >
            <table>
                <tr>
                    <td> 
                    <input type="button"  class="btn" value="回答\Answer" onclick="submitPageReview('0')" />
                    </td>
                    </tr>
                    </table>
                    </td>
                    </tr>
                    </table>
        </div>
    <div style="display: none;">
     <btn:buttonlist id="ButtonList1" runat="server"></btn:buttonlist>
        <asp:HiddenField runat="server" ID="hdIncident" />
         <asp:HiddenField runat="server" ID="hdPrint"  Value="0" />
    </div>
    </form>
</body>
</html>
