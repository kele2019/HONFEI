<%@ Page Language="C#" AutoEventWireup="true" Inherits="TravalExpense.Approval" Codebehind="Approval.aspx.cs" %>
<%@ Register Src="/Modules/Ultimus.UWF.Form.ProcessControl/UserInfo.ascx" TagName="UserInfo" TagPrefix="ui" %>
<%@ Register Src="/Modules/Ultimus.UWF.Form.ProcessControl/ApprovalHistory.ascx" TagName="ApprovalHistory" TagPrefix="ah" %>
<%@ Register Src="/Modules/Ultimus.UWF.Form.ProcessControl/Attachments.ascx" TagName="Attachments" TagPrefix="attach" %>
<%@ Register Src="/Modules/Ultimus.UWF.Form.ProcessControl/ButtonList.ascx" TagName="ButtonList" TagPrefix="btn" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>出差报销申请/Travel Expense Request</title>
    <script type="text/javascript" src="/Assets/js/common.js"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            $("#UserInfo1_fld_APPLICANT").parent().parent().parent().parent().attr("style", "margin-bottom:0px");
            changeBorrow();
            //$("#ButtonList1_btnAsk").hide();
//            $("#detailtable").find("tr").each(function (i, Etr) {
//                var currency = $(Etr).find("td").eq(1).find("input").val();
//                if (currency != "") {
//                    $(Etr).find("td").eq(1).find("select").val(currency);
//                }
//            });
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
    </script>
</head>
<body>
     <form id="form1" runat="server">
    <div class="container">
        <div class="row">
            <ui:userinfo id="UserInfo1" processtitle="出差报销流程（Travel Expense Request）" processprefix="TE" tablename="PROC_TravalExpense"
                tablenamedetail="Proc_TravalExpense_DT" runat="server"></ui:userinfo>
                <table class="table table-condensed table-bordered">
                 <tr>
                <td class="td-label">事由 Description：</td>
                <td colspan="3">
                <asp:Label runat="server" ID="fld_Description"  ></asp:Label>
                </td>
                </tr>
                 <tr>
                <td class="td-label">出差申请单号<br />Traval Request No：</td>
                <td colspan="3">
                <asp:Label runat="server" ID="read_RequestTravalNo"    style="margin-bottom:0px" 
                        Width="50%" onprerender="read_RequestTravalNo_PreRender"></asp:Label>
                </td>
                </tr>
       </table>
        </div>
        <div class="row">
            <table id="detailtable" class="table table-condensed table-bordered">
             <tr>
                    <td class="banner" colspan="14">
                       报销明细 Expense Detail
                    </td>
                </tr>
            <tr>
            <th>序号<br />No</th>
            <th>币种<br />Currency</th>
             <th>开始日期 <br />Start Date</th>
            <th>结束日期 <br />End Date</th>
            <th style="display:none">费用承担部门<br />Responsible Cost Center</th>
            <th>住宿费(包括税费)<br />Hotel(incl. Tax)</th>
            <th> 餐费<br />Meals</th>
             <th>交通费<br />Transportation</th>
             <th>业务招待费<br />Entertainment</th>
             <th>礼品费<br />Gifts</th>
              <th >是否公司信用卡<br />
              Is corp. Credit Card</th>
             <th>说明<br />Remarks</th>
             <th>汇率<br />Ex. Rate(to RMB)</th>
             <th>小计<br />Sub-total</th>
            </tr>
              <tbody id="Tbody1">
            <asp:Repeater runat="server" ID="read_detail_Proc_TravalExpense_DT"   OnItemDataBound="fld_detail_PROC_TravalExpense_DT_ItemDataBound">
            <ItemTemplate>
            <tr>
            <td> <asp:TextBox ID="read_FORMID" Text='<%#Eval("FORMID") %>' runat="server" Style="display: none"></asp:TextBox>
                 <asp:Label ID="read_ROWID" Text='<%# Container.ItemIndex+1%>' runat="server"></asp:Label></td>
            <td> 
             <asp:Label runat="server" ID="fld_Currency"   Text='<%#Eval("Currency") %>'></asp:Label>
            </td>
            <td>
            <asp:Label runat="server" ID="read_PaymentDate"    Text='<%# String.IsNullOrEmpty(Eval("PaymentDate").ToString())? "":DateTime.Parse(Eval("PaymentDate").ToString()).ToString("yyyy-MM-dd")  %>'  ></asp:Label>
            </td>
             <td>
            <asp:Label runat="server" ID="fld_PaymentEndDate"   Text='<%# String.IsNullOrEmpty(Eval("PaymentEndDate").ToString())? "":DateTime.Parse(Eval("PaymentEndDate").ToString()).ToString("yyyy-MM-dd")  %>'  ></asp:Label>
            </td>
             <td style="display:none">
            <asp:Label runat="server" ID="read_CostCenter"    Text='<%#Eval("CostCenter") %>'></asp:Label>
            </td>
            <td>
            <asp:Label runat="server" ID="read_HotelCost"   Text='<%#Eval("HotelCost") %>'></asp:Label>
            </td>
              <td>
            <asp:Label runat="server" ID="read_Meals"  Text='<%#Eval("Meals") %>'></asp:Label>
            </td>
            <td><asp:Label runat="server"  ID="read_Transportation"   Text='<%#Eval("Transportation") %>'></asp:Label></td>
            <td><asp:Label runat="server"  ID="read_Other"   Text='<%#Eval("Other") %>'></asp:Label></td>
            <td><asp:Label runat="server"  ID="read_Gifts"  Text='<%#Eval("Gifts") %>'  ></asp:Label></td>
             <td> <asp:Label runat="server" ID="read_IsCreditCard"   Text='<%#Eval("IsCreditCard").ToString()==""?"是 Yes":Eval("IsCreditCard") %>'></asp:Label>
            </td>
            <td><asp:Label runat="server"  ID="read_Remarks"   Text='<%#Eval("Remarks") %>'></asp:Label></td>
            <td><asp:Label runat="server"  ID="read_Rate"  Text='<%#Eval("Rate") %>'></asp:Label></td>
            <td><asp:Label runat="server"  ID="read_SubTotal"    Text='<%#Eval("SubTotal") %>'></asp:Label></td>
            </tr>
             </ItemTemplate>
            </asp:Repeater>
            </tbody>
            <tr>
            <td colspan="12" style="text-align:right">合计/Total：</td>
            <td><asp:Label runat="server" ID="read_CountSub"></asp:Label>
            </td>
            </tr>
            </table>
            </div>
            <div class="row">
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
            <attach:attachments id="Attachments1" ReadOnly="true" runat="server"></attach:attachments>
        </div>
        <div class="row">
            <ah:approvalhistory id="ApprovalHistory1" showaction="true" runat="server"></ah:approvalhistory>
        </div>
        <div class="row">
        </div>
    </div>
    <btn:buttonlist id="ButtonList1" runat="server"></btn:buttonlist>
    <div style="display: none;">
        
    </div>
    </form>
</body>
</html>
