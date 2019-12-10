<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="NewRequest.aspx.cs" Inherits="Presale.Process.Cash_Advance.NewRequest" %>

<%@ Register Src="/Modules/Ultimus.UWF.Form.ProcessControl/UserInfo.ascx" TagName="UserInfo" TagPrefix="ui" %>
<%@ Register Src="/Modules/Ultimus.UWF.Form.ProcessControl/ApprovalHistory.ascx" TagName="ApprovalHistory" TagPrefix="ah" %>
<%@ Register Src="/Modules/Ultimus.UWF.Form.ProcessControl/Attachments.ascx" TagName="Attachments" TagPrefix="attach" %>
<%@ Register Src="/Modules/Ultimus.UWF.Form.ProcessControl/ButtonList.ascx" TagName="ButtonList" TagPrefix="btn" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
        <meta http-equiv="X-UA-Compatible"  content="IE=EmulateIE8" />
    <title>借款申请 Cash Advance</title>
    <script type="text/javascript" src="/Assets/js/common.js"></script>
    <script type="text/javascript">
        function beforeSubmit() {
            var indexlen = $("#UserInfo1_fld_APPLICANT").text().indexOf('(');
            var UserName = $("#UserInfo1_fld_APPLICANT").text().substr(0, indexlen);
            var countsub = $("#fld_BorrowAmount").val();
            var summary = UserName+ " Cash Advance Request  ￥" + countsub;
            $("#UserInfo1_fld_PROCESSSUMMARY").val(summary);
            $("#fld_TRSummary").val(summary);
            if ((countsub - 0) <= 0) {
                alert("借款金额无效\nBorrow Amount invalid");
                return false;
            }
            return true;
        }
        function beforeSave() {
            var indexlen = $("#UserInfo1_fld_APPLICANT").text().indexOf('(');
            var UserName = $("#UserInfo1_fld_APPLICANT").text().substr(0, indexlen);
            var countsub = $("#fld_BorrowAmount").val();
            var summary = UserName + " " + "Cash Advance Request ￥" + countsub;
            $("#UserInfo1_fld_PROCESSSUMMARY").val(summary);
            $("#fld_TRSummary").val(summary);
            return true;
        }
        $(document).ready(function () {
            if ($("#hdIncident").val() != "") {
                $("#ButtonList1_btnSubmit").val("提交/Submit");
                $("#ButtonList1_btnBack").hide();
                $("#ButtonList1_btnReject").show();
                $("#ButtonList1_btnAsk").hide();
            }
//            if ($("#hdPrint").val() != "0") {
//                $("#divPrint").show();
            //            }
            if ($("#hdUrgeTask").val() == "Yes") {
                $("#ReturnBackTask").show();
            }
        });
        function CountAmount() {
            var countAmount = 0;
            $("#tabletbodyDetail").find("tr").each(function (i, Etr) {
                var SubAmount = $(Etr).find("td").eq(1).find("input").val() - 0;
                $(Etr).find("td").eq(1).find("input").val(SubAmount.toFixed(2));
                countAmount += (SubAmount-0).toFixed(2)-0;
            });
            $("#fld_BorrowAmount").val(countAmount);
        }

        function ChangeBorrowAmount() {
            $("#fld_BorrowAmount").val(($("#fld_BorrowAmount").val()-0).toFixed(2));
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
     <div id="myDiv" class="container">
            <div class="row">
                <ui:userinfo id="UserInfo1" processtitle="借款申请(Cash Advance Request)" processprefix="CA" tablename="PROC_CashAdvance"
                    runat="server" tablenamedetail="PROC_CashAdvance_DT"></ui:userinfo>
            </div>
            <div class="row">
                <table class="table table-condensed table-bordered">
                    <tr>
                        <td class="banner" colspan="6">详细信息 Cash Detail
                        </td>
                    </tr>
                    <tr>
                        <td class="td-label" style="width:100px">
                          借款原因
                            <br /> Cash Advance Reason：<span class="red">*</span>
                        </td>
                        <td class="td-content" colspan="3">
                        <asp:TextBox runat="server" ID="fld_BorrowReson" Width="98%"  CssClass="validate[required]"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="td-label"> 
                            借款金额 <br />
                           Cash Advance Applied (RMB)： <span class="red">*</span>
                        </td>
                        <td class="td-content" >
                            <asp:TextBox runat="server"  ID="fld_BorrowAmount" onblur="ChangeBorrowAmount()" CssClass="validate[required]"  money="money"      Width="80%"></asp:TextBox>
                        </td>
                        <td class="td-label"> 
                            币种 <br />
                           Currency： <span class="red">*</span>
                        </td>
                        <td class="td-content" >
                        RMB
                            <asp:TextBox runat="server" style="display:none" ID="fld_Currency"  Text="RMB"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="td-label"> 
                            银行卡号 <br />
                           Bank Account： <span class="red">*</span>
                        </td>
                        <td class="td-content" >
                            <asp:TextBox runat="server" ID="fld_BankNo" MaxLength="18"   CssClass="validate[required]"  Width="80%"></asp:TextBox>
                        </td>
                        <td class="td-label"> 
                            开户银行 <br />
                           Bank Name： <span class="red">*</span>
                        </td>
                        <td class="td-content" >
                            <asp:TextBox runat="server" ID="fld_BankName"   CssClass="validate[required]"  Width="80%"></asp:TextBox>
                        </td>
                    </tr>
                </table>
            </div>
            <div  style="display:none" >
                <div class="row" style="width: 120%">
                    <table class="table table-condensed table-bordered tablerequired" style="width:83%" id="tbDetail">
                        <tr>
                            <td class="banner" colspan="5">借款明细 Detail of Cash Advance
                            </td>
                        </tr>
                        <tr>
                            <th > 序号 
                               No.
                            </th>
                            <th >金额Amount
                                 <span class="red">*</span>
                            </th>
                            <th >项目Item
                            </th>
                            <th >备注 Comments
                            </th>
                             <th  ></th>
                        </tr>
                         <tbody id="tabletbodyDetail">
                        <asp:Repeater ID="fld_detail_PROC_CashAdvance_DT" runat="server" OnItemCommand="fld_detail_PROC_CashAdvance_DT_ItemCommand" OnItemDataBound="fld_detail_PROC_CashAdvance_DT_ItemDataBound" >
                            <ItemTemplate>
                                <tr>
                                    <td style="text-align: center;">
                                        <asp:TextBox ID="fld_FORMID" Text='<%#Eval("FORMID") %>' runat="server" Style="display: none"></asp:TextBox>
                                        <asp:Label ID="fld_ROWID" Text='<%# Container.ItemIndex+1%>' runat="server"></asp:Label>
                                    </td>
                                    <td style="text-align: center;">
                                        <asp:TextBox ID="fld_Amount"  onblur="CountAmount()" CssClass="validate[required]" Width="80%" money="money" Text='<%#Eval("Amount") %>' runat="server"></asp:TextBox>
                                    </td>
                                    <td style="text-align: center;">
                                        <asp:TextBox ID="fld_Item" runat="server"  CssClass="validate[required]" Width="80%"  Text='<%#Eval("Item").ToString() %>'></asp:TextBox>
                                    </td>
                                    <td style="text-align: center;">
                                       <asp:TextBox ID="fld_Comments" Text='<%#Eval("Comments") %>' runat="server"   Width="80%"  ></asp:TextBox>
                                    </td>
                                       <td style="text-align:center">
                                            <asp:Button ID="btnDelete" runat="server" Text="删除/Del" CssClass="btn" CommandName="del"
                                                ClientIDMode="Static" OnClientClick="return confirm('确认删除/Confirm Del？')" />
                                        </td>
                                </tr>
                            </ItemTemplate>
                        </asp:Repeater>
                          </tbody>
                          <tr>
                          <td colspan="5" style="text-align:left">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                          <asp:Button ID="btnAdd" runat="server" Text="增加/Add" CssClass="btn" CausesValidation="false"
                OnClick="btnAdd_Click"     />
                          </td>
                          </tr>
                    </table>
                </div>
            </div>
            <div class="row">
                <attach:attachments id="Attachments1" runat="server"></attach:attachments>
            </div>
            <div class="row">
             <ah:approvalhistory id="ApprovalHistory1" showaction="true" runat="server"></ah:approvalhistory>
            </div>
             <div id="ReturnBackTask" style="display:none; text-align:center">
        <asp:Button runat="server" ID="btnRevoke"  Visible="false" CssClass="btn" Text="Revoke" 
                onclick="btnRevoke_Click" />
        </div>
        </div>
        <btn:buttonlist id="ButtonList1" runat="server"></btn:buttonlist>
        <div style="display:none">
        <asp:TextBox runat="server" ID="fld_TRSummary"></asp:TextBox>
        <asp:HiddenField runat="server" ID="hdIncident" />
        <asp:HiddenField runat="server" ID="hdPrint" />
         <asp:HiddenField runat="server" ID="hdUrgeTask" />
        </div>
    </form>
</body>
</html>
