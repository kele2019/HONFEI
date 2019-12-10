<%@ Page Language="C#" AutoEventWireup="true" Inherits="TravalExpense.NewRequest" Codebehind="NewRequest.aspx.cs" %>
<%@ Register Src="/Modules/Ultimus.UWF.Form.ProcessControl/UserInfo.ascx" TagName="UserInfo" TagPrefix="ui" %>
<%@ Register Src="/Modules/Ultimus.UWF.Form.ProcessControl/ApprovalHistory.ascx" TagName="ApprovalHistory" TagPrefix="ah" %>
<%@ Register Src="/Modules/Ultimus.UWF.Form.ProcessControl/Attachments.ascx" TagName="Attachments" TagPrefix="attach" %>
<%@ Register Src="/Modules/Ultimus.UWF.Form.ProcessControl/ButtonList.ascx" TagName="ButtonList" TagPrefix="btn" %>
<%@ Import Namespace="Ultimus.UWF.Common.Logic" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
 <meta http-equiv="X-UA-Compatible"  content="IE=EmulateIE8" />
    <title>出差报销申请/Travel Expense Request</title>
    <script type="text/javascript" src="/Assets/js/common.js"></script>
    <script type="text/javascript">
        function countsubtoal() {
        var subttoal=0;
        $("#detailtable").find("tr").each(function (i, Etr) {
            var currency = $(Etr).find("td").eq(1).find("input").val();
            if (currency != "") {
                $(Etr).find("td").eq(1).find("select option:contains('" + currency + "')").attr("selected", true);
            }
            var IsCredit = $(Etr).find("td").eq(10).find("input").val();
            if (IsCredit != "") {
                $(Etr).find("td").eq(10).find("select option:contains('" + IsCredit + "')").attr("selected", true);
            }

            var sub = $(Etr).find("td").eq(13).find("input").val(); //$(Etr).find("input[money=money]").val();
            if (sub != undefined && sub != NaN)
                subttoal += (sub - 0);
        });
        $("#fld_CountSub").val(subttoal);
        $("#lbCountSub").text(subttoal);
    }
    $(document).ready(function () {
        $(".container").attr("style", "width:1050px");
        $(".td-label").attr("style", "width:15%");
        $(".td-content").attr("style", "width:35%");
        var totoalsub = $("#fld_CountSub").val();
        $("#lbCountSub").text(totoalsub);
        countsubtoal();
        changeBorrow();
        
        $("#UserInfo1_fld_APPLICANT").parent().parent().parent().parent().attr("style", "margin-bottom:0px");
        //$("#UserInfo1_fld_PROCESSSUMMARY").val($("#UserInfo1_fld_APPLICANT").text() + "-出差报销单");
        if ($("#hdIncident").val() != "") {
            $("#ButtonList1_btnSubmit").val("提交/Submit");
            $("#ButtonList1_btnBack").hide();
            $("#ButtonList1_btnReject").show();
            $("#ButtonList1_btnAsk").hide();
        }
        if ($("#hdPrint").val() != "0") {
            $("#divPrint").show();
        }
        if ('<%=BorrowCount%>' != "0") {
            alert("您有未清账的借款单,请注意\nYou Have Unclear  CashAdvance Form,Please Note");
        }
        if ($("#hdUrgeTask").val() == "Yes") {
            $("#ReturnBackTask").show();
        }
    });
    function changeRat(obj) {
        // $(obj).val();
        $(obj).parent().parent().find("td").eq(12).find("input").val($(obj).val());
        $(obj).parent().find("input").val($(obj).find("option:selected").text());
        countsub(obj);
    }
    function countsub(obj, type) {
        if (type != "1") {
            $(obj).val(($(obj).val() - 0).toFixed(2));
        }
        var Etr = $(obj).parent().parent();
        var HotelCost = $(Etr).find("td").eq(5).find("input").val()-0;
        var Meals = $(Etr).find("td").eq(6).find("input").val() - 0;
        var Transportation = $(Etr).find("td").eq(7).find("input").val() - 0;
        var Other = $(Etr).find("td").eq(8).find("input").val() - 0;
        var Gifts = $(Etr).find("td").eq(9).find("input").val() - 0;
        var Rat = $(Etr).find("td").eq(12).find("input").val() - 0;
        var counttotalsub = HotelCost + Meals + Transportation + Other + Gifts;
        var count = (counttotalsub * Rat).toFixed(2);
        $(Etr).find("td").eq(13).find("input").val(count);
        countsubtoal();
    }
    function changeBorrow() {
        var BrrowYes = $("#fld_BrrowYes").attr("checked");
        if (BrrowYes) {
            $("#trCash").show();
            $("#fld_CashAdvanceNo").attr("class", "validate[required]");
            $("#fld_BorrowsAmount").attr("class", "validate[required]");
        }
        else {
            $("#trCash").hide();
            $("#fld_CashAdvanceNo").val("");
            $("#fld_BorrowsAmount").val("");
            $("#fld_CashAdvanceNo").attr("class", "");
            $("#fld_BorrowsAmount").attr("class", "");
        }
    }
    function showPage() {
        var strUrl = "LoadTR.aspx";
        var width = "1000px";
        var height = "500px";
        var RturnVal = window.showModalDialog(strUrl, "", "dialogWidth:" + width + "px; dialogHeight:" + height + "px; dialogLeft: status:no; directories:yes;scrollbars:auto;Resizable=no;scroll:yes; ");
        if (RturnVal != undefined) {
            $("#fld_RequestTravalNo").val(RturnVal);
        }
    }

    function LoadCashAdvanceInfo() {
        var strUrl = "LoadCashAdvance.aspx";
        var width = "1000px";
        var height = "500px";
        var RturnVal = window.showModalDialog(strUrl, "", "dialogWidth:" + width + "px; dialogHeight:" + height + "px; dialogLeft: status:no; directories:yes;scrollbars:auto;Resizable=no;scroll:yes; ");
        if (RturnVal != undefined) {
            var AmountCount = 0;
            var CANo = "";
            var CAAmount = "";
            var obj = eval(RturnVal);
            if (obj) {
                for (i = 0; i < obj.length; i++) {
                    //  alert(obj[i].Reverse);
                    //  alert(obj[i].CANo);
                    AmountCount += obj[i].Reverse - 0;
                    CANo += obj[i].CANo + ",";
                    CAAmount += obj[i].CANo + "," + obj[i].Reverse + "|";
                }
                if (CANo.lastIndexOf(",") > 0) {
                    CANo = CANo.substring(0, CANo.lastIndexOf(","));
                }
                $("#fld_ReverseAmount").val(CAAmount);
                $("#fld_CashAdvanceNo").val(CANo);
                $("#fld_BorrowsAmount").val(AmountCount);
            }
        }
    }
    function beforeSubmit() {
        var indexlen = $("#UserInfo1_fld_APPLICANT").text().indexOf('(');
        var UserName = $("#UserInfo1_fld_APPLICANT").text().substr(0, indexlen);
        var countsub=$("#lbCountSub").text();
        var summary = UserName + " " + "Travel Expense ￥：" + countsub;

        $("#UserInfo1_fld_PROCESSSUMMARY").val(summary);
        $("#fld_TRSummary").val(summary);
        $("#Attachments1_txtMust").val("1");
        var BrrowYes = $("#fld_BrrowYes").attr("checked");
        if (BrrowYes) {
            var BorrowAmount = $("#fld_BorrowsAmount").val() - 0;
            var ExpeseAmount = $("#fld_CountSub").val() - 0;
            if (BorrowAmount > ExpeseAmount) {
                alert("报销金额必须大于等于冲账金额\nReimbursement Amount Must Equal Or Greater  Than Offset Amount");
                return false;
            }
        }
        return true;
    }
    function beforeSave() {
        var indexlen = $("#UserInfo1_fld_APPLICANT").text().indexOf('(');
        var UserName = $("#UserInfo1_fld_APPLICANT").text().substr(0, indexlen);
        var countsub = $("#lbCountSub").text();
        var summary = UserName + " " + "Travel Expense ￥：" + countsub;

        $("#UserInfo1_fld_PROCESSSUMMARY").val(summary);
        $("#fld_TRSummary").val(summary);
        return true;
    }
    function ShowTime(obj, index) {
        var mintime = $(obj).parent().prev().find("input").val();
        var maxtime = $(obj).parent().next().find("input").val();
        var StartDate = "";
        var EndDate = "";
        if (index == 1 && maxtime != undefined) {
            EndDate = maxtime
        }
        if (mintime != undefined && index == 2) {
            StartDate = mintime;
        }
        if (index == 2) {
            WdatePicker({dateFmt: 'yyyy-MM-dd', minDate: StartDate });
        }
        else {

            WdatePicker({dateFmt: 'yyyy-MM-dd', maxDate: EndDate });
        }
    }
    function changeCreditInfo(obj) {
        $(obj).next().val($(obj).val());
    }
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div id="myDiv" class="container">
        <div class="row">
            <ui:userinfo id="UserInfo1" processtitle="出差报销流程（Travel Expense Request）" processprefix="EX" tablename="PROC_TravalExpense"
                tablenamedetail="Proc_TravalExpense_DT" runat="server"></ui:userinfo>
                <table class="table table-condensed table-bordered"   >
                 <tr>
                <td class="td-label">事由 Description：</td>
                <td colspan="3">
                <asp:TextBox runat="server" ID="fld_Description"  style="margin-bottom:0px" Width="98%"></asp:TextBox>
                </td>
                </tr>
                <tr>
                <td class="td-label">出差申请单号<br />Traval Request No：</td>
                <td colspan="3">
                <asp:TextBox runat="server" ID="fld_RequestTravalNo"    style="margin-bottom:0px" Width="50%"></asp:TextBox>
                <input type="button" value="..." class="btn"  onclick="showPage()"/>
                </td>
                </tr>
       </table>
        </div>
        <div class="row">
            <table  class="table table-condensed table-bordered">
             <tr>
                    <td class="banner" colspan="14">
                       报销明细 Expense Detail
                    </td>
                </tr>
             <tr>
            <th width="1%">序号<br />No</th>
            <th >币种<br />Currency</th>
            <th>开始日期 <br />Start Date<span style="color:Red">*</span></th>
            <th>结束日期 <br />End Date<span style="color:Red">*</span></th>
            <th style="display:none">费用承担部门<br />Responsible Cost Center</th>
            <th style="width:60px"> 住宿费(包括税费)<br />Hotel(incl. Tax)</th>
            <th> 餐费<br />Meals</th>
             <th>交通费<br />Transp-<br />ortation</th>
             <th>业务招待费<br />Entert-<br />ainment</th>
             <th>礼品费<br />Gifts</th>
             <th >是否公司信用卡<br />
              Is corp. Credit Card</th>
             <th>说明<br />Remarks</th>
             <th>汇率<br />Ex. Rate</th>
             <th>小计<br />Sub-total</th>
             <th></th>
            </tr>
            <tbody id="detailtable">
            <asp:Repeater runat="server" ID="fld_detail_Proc_TravalExpense_DT" OnItemCommand="fld_detail_PROC_TravalExpense_DT_ItemCommand" OnItemDataBound="fld_detail_PROC_TravalExpense_DT_ItemDataBound">
            <ItemTemplate>
            <tr>
            <td> <asp:TextBox ID="fld_FORMID" Text='<%#Eval("FORMID") %>' runat="server" Style="display: none"></asp:TextBox>
                 <asp:Label ID="fld_ROWID" Text='<%# Container.ItemIndex+1%>' runat="server"></asp:Label></td>
            <td  style="text-align:center">
            <asp:DropDownList runat="server"  Width="60px" ID="dropCurrency" onchange="changeRat(this)">
            </asp:DropDownList>
             <asp:TextBox runat="server" ID="fld_Currency"  style="display:none" Text='<%#Eval("Currency").ToString()==""?"RMB":Eval("Currency") %>'></asp:TextBox>
            </td>
            <td>
            <asp:TextBox runat="server" ID="fld_PaymentDate" CssClass="validate[required]"  Width="60px" Text='<%# String.IsNullOrEmpty(Eval("PaymentDate").ToString())? "":DateTime.Parse(Eval("PaymentDate").ToString()).ToString("yyyy-MM-dd")  %>' onfocus="ShowTime(this,'1');"></asp:TextBox>
            </td>
            <td>
            <asp:TextBox runat="server" ID="fld_PaymentEndDate" CssClass="validate[required]"  Width="60px" Text='<%# String.IsNullOrEmpty(Eval("PaymentEndDate").ToString())? "":DateTime.Parse(Eval("PaymentEndDate").ToString()).ToString("yyyy-MM-dd")  %>' onfocus="ShowTime(this,'2');"></asp:TextBox>
            </td>
             <td style="display:none">
            <asp:TextBox runat="server" ID="fld_CostCenter" Width="60px" onfocus="this.blur()" Text='<%#Eval("CostCenter") %>'></asp:TextBox>
            </td>
            <td>
            <asp:TextBox runat="server" ID="fld_HotelCost" money="money" Width="40px" Text='<%#Eval("HotelCost") %>' onblur="countsub(this)"></asp:TextBox>
            </td>
              <td>
            <asp:TextBox runat="server" ID="fld_Meals" Width="40px" money="money" Text='<%#Eval("Meals") %>' onblur="countsub(this)"></asp:TextBox>
            </td>
            <td><asp:TextBox runat="server"  ID="fld_Transportation" money="money" Width="40px" Text='<%#Eval("Transportation") %>' onblur="countsub(this)"></asp:TextBox></td>
            <td><asp:TextBox runat="server"  ID="fld_Other" Width="40px" money="money" Text='<%#Eval("Other") %>' onblur="countsub(this)"></asp:TextBox></td>
            <td><asp:TextBox runat="server"  ID="fld_Gifts" Width="40px" money="money" Text='<%#Eval("Gifts") %>' onblur="countsub(this)"></asp:TextBox></td>
             <td> 
             <asp:DropDownList runat="server" ID="dropCreditCard"  Width="80px" onchange="changeCreditInfo(this)">
             <asp:ListItem  Value="是 Yes">是 Yes</asp:ListItem>
            <asp:ListItem  Value="否 No">否 No</asp:ListItem>
            </asp:DropDownList>
            <asp:TextBox runat="server" ID="fld_IsCreditCard"  style="display:none" Text='<%#Eval("IsCreditCard").ToString()==""?"是 Yes":Eval("IsCreditCard") %>'></asp:TextBox>
            </td>

            <td><asp:TextBox runat="server"  ID="fld_Remarks"  Width="100px" Text='<%#Eval("Remarks") %>'></asp:TextBox></td>
            <td><asp:TextBox runat="server"  ID="fld_Rate" Width="40px" money="money" onblur="countsub(this,1)" Text='<%# Eval("Rate").ToString()==""?"1":Eval("Rate") %>'></asp:TextBox></td>
            <td><asp:TextBox runat="server"  ID="fld_SubTotal" money="money" onfocus="this.blur()" onblur="countsubtoal()" Width="60px" Text='<%#Eval("SubTotal") %>'></asp:TextBox></td>
            <td>  
              <asp:Button ID="btnDelete" runat="server" Text="删除/Del" CssClass="btn" CommandName="del"
               ClientIDMode="Static" OnClientClick="return confirm('确认删除/Confirm Del？')" />
                </td>
            </tr>
             </ItemTemplate>
            </asp:Repeater>
            </tbody>
            <tr>
            <td colspan="2" style="text-align:center">
            <asp:Button ID="btnAdd" runat="server" Text="增加/Add" CssClass="btn" CausesValidation="false"
                OnClick="btnAdd_Click"   />
            </td>
            <td colspan="10" style="text-align:right">
            合计 Total：</td>
            <td  style="text-align:center"><asp:Label runat="server" ID="lbCountSub"></asp:Label>
            <asp:TextBox runat="server" style="display:none" ID="fld_CountSub"></asp:TextBox>
            <asp:TextBox runat="server" ID="fld_ReverseAmount" style="display:none"></asp:TextBox>
            </td>
            <td></td>
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
                  <td  class="td-label">
            借款单号 <br />Cash Advance No.：                                      
                </td>
                <td  >
                <asp:TextBox  runat="server" ID="fld_CashAdvanceNo" onfocus="this.blur()" Width="75%"></asp:TextBox>
                <input type="button" value="..." class="btn" onclick="LoadCashAdvanceInfo()" />
                </td>
                <td class="td-label">
             冲账金额 (人民币)<br />Offset Amount(RMB)：                                     
                </td>
                <td >
                <asp:TextBox runat="server" ID="fld_BorrowsAmount" style="border:0px" onfocus="this.blur()" Width="96%"></asp:TextBox>
                </td>
                </tr>
               
            </table>

        </div>
        <div class="row">
            <attach:attachments id="Attachments1" runat="server"></attach:attachments>
        </div>
        <div class="row">
            <ah:approvalhistory id="ApprovalHistory1" showaction="true" runat="server"></ah:approvalhistory>
        </div>
        <div class="row">
        </div>
         <div id="ReturnBackTask" style="display:none; text-align:center">
        <asp:Button runat="server" ID="btnRevoke"  Visible="false" CssClass="btn" Text="Revoke" 
                onclick="btnRevoke_Click" />
        </div>
    </div>
      <div id="divPrint" style="display:none; text-align:center">
        
        <input type="button"  class="btn1" value="打印/Print" id="btnprint" onclick="preview('myDiv')" />
         
        </div>
    <btn:buttonlist id="ButtonList1" runat="server"></btn:buttonlist>
    <div style="display: none;">
        <asp:TextBox runat="server" ID="fld_TRSummary"></asp:TextBox>
        <asp:HiddenField runat="server" ID="hdIncident" />
         <asp:HiddenField runat="server" ID="hdPrint"  Value="0" />
           <asp:HiddenField runat="server" ID="hdUrgeTask" />
    </div>
    </form>
</body>
</html>
