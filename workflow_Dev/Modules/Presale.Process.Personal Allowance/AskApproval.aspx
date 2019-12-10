<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AskApproval.aspx.cs" Inherits="Presale.Process.Personal_Allowance.AskApproval" %>

 
<%@ Register Src="/Modules/Ultimus.UWF.Form.ProcessControl/UserInfo.ascx" TagName="UserInfo" TagPrefix="ui" %>
<%@ Register Src="/Modules/Ultimus.UWF.Form.ProcessControl/ApprovalHistory.ascx" TagName="ApprovalHistory" TagPrefix="ah" %>
<%@ Register Src="/Modules/Ultimus.UWF.Form.ProcessControl/Attachments.ascx" TagName="Attachments" TagPrefix="attach" %>
<%@ Register Src="/Modules/Ultimus.UWF.Form.ProcessControl/ButtonList.ascx" TagName="ButtonList" TagPrefix="btn" %>
<%@ Import Namespace="Ultimus.UWF.Common.Logic" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>个人补贴申请/Personal Allowance Request</title>
    <script type="text/javascript" src="/Assets/js/common.js"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            $("#ButtonList1_btnAsk").hide();
            $("#ButtonList1_var_AskForAccount").val("No");
            $("#ButtonList1_btnBack").hide();
            if (request("Type") == "myapproval") {
                $("#btnDiv").hide();
            }
        });
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
            <ui:userinfo id="UserInfo1" processtitle="个人补贴流程（Personal Allowance Request）" processprefix="PA" tablename="PROC_PersonalAllownce"
                tablenamedetail="PROC_PersonalAllownce_DT" runat="server"></ui:userinfo>
        </div>
        <div class="row">
            <table  class="table table-condensed table-bordered">
             <tr>
                    <td class="banner" colspan="11">
                       补贴明细 Allowance Detail
                    </td>
                </tr>
             <tr>
            <th width="1%">序号<br />No</th>
            <th >币种<br />Currency</th>
            <th>开始日期 <br />Start Date</th>
            <%--<th>结束日期 <br />End Date</th>--%>
            <th >市内交通费<br />Local-Transportation</th>
            <th style="width:60px">房租费<br />House Rental</th>
            <th> 电话费<br />Phone</th>
             <th>说明<br />Remark</th>
             <th>汇率<br />Ex. Rate</th>
             <th>小计<br />Sub-total</th>
            </tr>
            <tbody id="detailtable">
            <asp:Repeater runat="server" ID="fld_detail_PROC_PersonalAllownce_DT"   >
            <ItemTemplate>
            <tr>
            <td> <asp:TextBox ID="read_FORMID" Text='<%#Eval("FORMID") %>' runat="server" Style="display: none"></asp:TextBox>
                 <asp:Label ID="read_ROWID" Text='<%# Container.ItemIndex+1%>' runat="server"></asp:Label></td>
            <td  style="text-align:center">
         
             <asp:Label runat="server" ID="read_Currency"    Text='<%#Eval("Currency") %>'></asp:Label>
            </td>
            <td>
            <asp:Label runat="server" ID="read_PaymentDate"  Text='<%# String.IsNullOrEmpty(Eval("PaymentDate").ToString())? "":DateTime.Parse(Eval("PaymentDate").ToString()).ToString("yyyy-MM-dd")  %>' ></asp:Label>
            </td>
           <%-- <td>
            <asp:Label runat="server" ID="read_PaymentEndDate"    Text='<%# String.IsNullOrEmpty(Eval("PaymentEndDate").ToString())? "":DateTime.Parse(Eval("PaymentEndDate").ToString()).ToString("yyyy-MM-dd")  %>'  ></asp:Label>
            </td>--%>
            <td><asp:Label runat="server"  ID="read_Transportation"  Text='<%#Eval("Transportation") %>' ></asp:Label></td>
            <td>
            <asp:Label runat="server" ID="read_HouseRental" Text='<%#Eval("HouseRental") %>' Width="40px"  ></asp:Label>
            </td>
             <td>
            <asp:Label runat="server" ID="read_Phone" Text='<%#Eval("Phone") %>' ></asp:Label>
            </td>
            <td><asp:Label runat="server"  ID="read_Remarks"  Text='<%#Eval("Remarks") %>'></asp:Label></td>
            <td><asp:Label runat="server"  ID="read_Rate" Text='<%# Eval("Rate").ToString()==""?"1":Eval("Rate") %>'></asp:Label></td>
            <td><asp:Label runat="server"  ID="read_SubTotal"   Width="60px" Text='<%#Eval("SubTotal") %>'></asp:Label></td>
            </tr>
             </ItemTemplate>
            </asp:Repeater>
            </tbody>
            <tr>
            <td colspan="8" style="text-align:right">
            合计/Total：</td>
            <td  style="text-align:center"> 
            <asp:Label runat="server" ID="read_CountSub"></asp:Label>
            </td>
            </tr>
            </table>
            </div>
             <div class="row" style="display:none">
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
                <td  width="30%" >
                <asp:RadioButton runat="server" ID="fld_BrrowYes"  GroupName="brrowType" onclick="changeBorrow()"  />是/Yes &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                <asp:RadioButton runat="server" ID="fld_BrrowNo"  GroupName="brrowType" onclick="changeBorrow()"  />否/No
                </td>
                <td class="td-label">
币种&nbsp;&nbsp;<br /> Currency：
                </td>
                <td>
                <asp:Label runat="server"  ID="fld_Currency" Text="RMB"></asp:Label>
                </td>
                </tr>
                <tr id="trCash">
                <td class="td-label">
                借款金额 (人民币)<br />Cash Advance (RMB)：                                     
                </td>
                <td >
                <asp:TextBox runat="server" ID="fld_BorrowsAmount" money="money" Width="96%"></asp:TextBox>
                </td>
                <td  class="td-label">
                <%--金额(小写)<br />Payment Amount：--%>                                         
                </td>
                <td  >
                <%--<asp:TextBox  runat="server" ID="fld_PaymentAmout" Width="96%"></asp:TextBox>--%>
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

