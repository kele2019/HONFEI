<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="NewRequest.aspx.cs" Inherits="Presale.Process.PurchaseApplication.NewRequest" %>
<%@ Register Src="/Modules/Ultimus.UWF.Form.ProcessControl/UserInfo.ascx" TagName="UserInfo" TagPrefix="ui" %>
<%@ Register Src="/Modules/Ultimus.UWF.Form.ProcessControl/ApprovalHistory.ascx" TagName="ApprovalHistory" TagPrefix="ah" %>
<%@ Register Src="/Modules/Ultimus.UWF.Form.ProcessControl/Attachments.ascx" TagName="Attachments" TagPrefix="attach" %>
<%@ Register Src="/Modules/Ultimus.UWF.Form.ProcessControl/ButtonList.ascx" TagName="ButtonList" TagPrefix="btn" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
        <meta http-equiv="X-UA-Compatible"  content="IE=EmulateIE8" />
    <title>Purchase Request Process</title>
    <script type="text/javascript" src="/Assets/js/common.js"></script>
    <script type="text/javascript">
        function beforeSubmit() {
//            $("#detail").find("tr").each(function (i, Etr) {
//                var ItemValue = $(Etr).find("td").find(".item").val();
//                $(Etr).find("td").eq(0).children().last().val(ItemValue);
            //            });
            if ($("#singleSupplier").attr("checked")) {
                if ($("#fld_SingleSupplierReason").val() == "") {
                    alert("You have checked Single Supplier,so must Fill in Reason");
                    return false;
                }
                if ($("#fld_SupplierName").val() == "") {
                    alert("You have checked Single Supplier,so must Fill in Supplier Name");
                    return false;
                }
            }
            $("#fld_Currency").val($("#currency").find("option:selected").text());
            if ($("#fld_Currency").val().replace(/(^\s*)|(\s*$)/g, "") == "RMB") {
                $("#fld_totalUSD").val((($("#fld_TotalAmount").val() - 0) / ($("#fld_rate").val() - 0)).toFixed(2));
            }
            if ($("#fld_Currency").val().replace(/(^\s*)|(\s*$)/g, "") == "USD") {
                $("#fld_totalUSD").val(($("#fld_TotalAmount").val() - 0).toFixed(2));
            }
            if ($("#fld_CostCenterNumber").val() == "") {
                alert("Please select cost center");
                return false;
            }
            if ($("#fld_Currency").val() == "") {
                alert("Please select currency");
                return false;
            }
            if ($("#fld_VAT").val() == "") {
                alert("Please select VAT");
                return false;
            }
            if ($("#fld_IsBudgetyear").val() == "") {
                alert("Please select Budget of this year");
                return false;
            }
            var summary = "Purchase Request Process";
            $("#UserInfo1_fld_PROCESSSUMMARY").val(summary);
            return true;
        }
        function beforeSave() {
            var summary = "Purchase Request Process";
            $("#UserInfo1_fld_PROCESSSUMMARY").val(summary);
            $("#fld_TRSummary").val(summary);
            return true;
        }
        $(document).ready(function () {
            if ($("#fld_SingleSupplier").val() == "yes") {
                $("#singleSupplier").attr("checked", true);
                $("#supplierNameLabel").show();
                $("#supplierNameValue").show();
                $("#reason").show();
            }
            //            $("#fld_Date").val(showtime($("#fld_Date").val()));
            $("#detail").find("tr").each(function (i, Etr) {
                var date1 = $(Etr).find("td").eq(3).children().val();
                var date2 = $(Etr).find("td").eq(4).children().val();
                $(Etr).find("td").eq(3).children().val(showtime(date1));
                $(Etr).find("td").eq(4).children().val(showtime(date2));
            });
            if ($("#fld_VAT").val() == "Yes") {
                $("#vat1").attr("checked", true);
            }
            if ($("#fld_VAT").val() == "No") {
                $("#vat2").attr("checked", true);
            }
            if ($("#fld_IsBudgetyear").val() == "Yes") {
                $("#RadioButton1").attr("checked", true);
            }
            if ($("#fld_IsBudgetyear").val() == "No") {
                $("#RadioButton2").attr("checked", true);
            }
            $("#costCenter").val($("#fld_CostCenterNumber").val());
            $("#currency").val($("#fld_Currency").val());
            amount_onchange();
            if ($("#hdIncident").val() != "") {
                $("#ButtonList1_btnSubmit").val("Submit");
                $("#ButtonList1_btnBack").hide();
                $("#ButtonList1_btnReject").show();
            }
            if ($("#hdUrgeTask").val() == "Yes") {
                $("#ReturnBackTask").show();
            }

            var PRType = $("#fld_PRType").val();
            if (PRType == "") {
                PRType == "1";
                $("#fld_PRType").val("1");
            }
            LoadPRType(PRType);
        });
        function getButtonCheck(obj, index) {
            if (index == "1") {
                if ($(obj).attr("checked")) {
                    $("#fld_VAT").val("Yes");
                }
                else {
                    $("#fld_VAT").val("");
                }
            }
            if (index == "2") {
                if ($(obj).attr("checked")) {
                    $("#fld_VAT").val("No");
                }
                else {
                    $("#fld_VAT").val("");
                }
            }
        }

        function getButtonCheck1(obj, index) {
            if (index == "1") {
                if ($(obj).attr("checked")) {
                    $("#fld_IsBudgetyear").val("Yes");
                }
                else {
                    $("#fld_IsBudgetyear").val("");
                }
            }
            if (index == "2") {
                if ($(obj).attr("checked")) {
                    $("#fld_IsBudgetyear").val("No");
                }
                else {
                    $("#fld_IsBudgetyear").val("");
                }
            }
        }

        function costCenter_onclick(obj) {
            var digStr = "dialogHeight:500px;dialogWidth:850px;";
            var ReturnValue = window.showModalDialog("./CostCenter.aspx", "", digStr);
            if (ReturnValue != null) {
                var cosetcenterDetail = eval("(" + ReturnValue + ")");
                var CostCenter = cosetcenterDetail[0].cosetcenter;
                var Description = cosetcenterDetail[1].Description;
                $(obj).val(CostCenter + "-" + Description);
                $(obj).next().val(CostCenter);

            }
        }
        function currency_onchange(obj) {
            $(obj).next().val($(obj).find("option:selected").text());
        }
        function amount_onchange() {
            var totalamount = 0;
            $("#detail").find("tr").each(function (i, Etr) {
                var Amount = ($(Etr).find("td").eq(8).children().eq(1).val() - 0);
                totalamount += Amount;
            });
            $("#fld_TotalAmount").val(totalamount.toString() == "NaN" ? "0" : totalamount.toFixed(2));
            $("#lb_TotalAmount").text(totalamount.toString() == "NaN" ? "0" : formatNumber(totalamount.toFixed(2), 2, 1));
        }
        function getamount(obj, flag) {
            var amount = 0.00; var unitPrice = 0.00;var qty = 0.00;
            if (flag == "1") {
                unitPrice = $(obj).val();
                qty = $(obj).parent().parent().find("td:eq(7)").children().val();
                amount = (unitPrice * qty).toFixed(2);
                $(obj).parent().parent().find("td:eq(8)").children().eq(0).text(formatNumber(amount, 2, 1));
                $(obj).parent().parent().find("td:eq(8)").children().eq(1).val(amount);
            }
            if (flag == "2") {
                unitPrice = $(obj).parent().parent().find("td:eq(5)").children().val();
                qty = $(obj).val();
                amount = (unitPrice * qty).toFixed(2);
                $(obj).parent().parent().find("td:eq(8)").children().eq(0).text(formatNumber(amount, 2, 1));
                $(obj).parent().parent().find("td:eq(8)").children().eq(1).val(amount);
            }
//            $("#detail").find("tr").each(function (i, Etr) {
//                var amount = 0;
//                var unitPrice = ($(Etr).find("td").eq(5).children().val() - 0);
//                var qty = ($(Etr).find("td").eq(7).children().val() - 0);
//                amount = unitPrice * qty;
//                alert(amount);
//                $(Etr).find("td").eq(8).children().eq(1).val(amount.toFixed(2));
//                $(Etr).find("td").eq(8).children().eq(0).text(formatNumber(amount.toFixed(2), 2, 1));
//            });
            amount_onchange();
        }
        function showtime(obj) {
            var time = new Date(obj.replace(/-/g, "/"));
            var year = time.getFullYear();
            var month = time.getMonth() + 1;
            var date = time.getDate();
            return (year + "-" + month + "-" + date) == "NaN-NaN-NaN" ? "" : (year + "-" + month + "-" + date);
        }
        function singleSupplier_onclick(obj) {
            if ($(obj).attr("checked")) {
                $(obj).next().val("yes");
                $("#supplierNameLabel").show();
                $("#supplierNameValue").show();
                $("#reason").show();
            }
            else {
                $(obj).next().val("");
                $("#supplierNameLabel").hide();
                $("#supplierNameValue").hide();
                $("#reason").hide();
                $("#supplierNameLabel").val("");
                $("#supplierNameValue").val("");
                $("#reason").val("");
            }
        }
        function LoadPRType(obj) {
            if (obj == "1") {
                $("#PRType1").attr("checked", true);
                $("#fld_PRType").val("1");
            }
            if (obj == "0") {
                $("#PRType2").attr("checked", true);
                $("#fld_PRType").val("0");
            }
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <div id="myDiv" class="container">
            <div class="row">
                <ui:userinfo id="UserInfo1" processtitle="Purchase Request Process" processprefix="PURPR" tablename="PROC_Purchase" tablenamedetail="PROC_Purchase_DT"
                    runat="server"  ></ui:userinfo>
                    <asp:TextBox runat="server" ID="fld_rate" style="display:none;"></asp:TextBox>
                    <asp:TextBox runat="server" ID="fld_totalUSD" style="display:none;"></asp:TextBox>
                    <asp:TextBox runat="server" ID="fld_deptManagerLogin" style="display:none;"></asp:TextBox>
                    <asp:TextBox runat="server" ID="fld_DGM" style="display:none" ></asp:TextBox>
                    <asp:TextBox runat="server" ID="fld_GM" style="display:none" ></asp:TextBox>
                    <asp:TextBox runat="server" ID="fld_SupplierMLogin" style="display:none" ></asp:TextBox>
                    <asp:TextBox runat="server" ID="fld_FMLogin" style="display:none" ></asp:TextBox>
                    <asp:TextBox runat="server" ID="fld_DGMLogin" style="display:none;"></asp:TextBox>
                    <asp:TextBox runat="server" ID="fld_GMLogin" style="display:none;"></asp:TextBox>
                    <asp:TextBox runat="server" ID="fld_PURMLogin" style="display:none;"></asp:TextBox>
                    <asp:TextBox runat="server" ID="fld_CompareValue" value="5000.00" style="display:none;"></asp:TextBox>
                    <asp:TextBox runat="server" ID="fld_SupplierLogin" style="display:none;"></asp:TextBox>
                    <asp:TextBox runat="server" ID="fld_ApplierLogin" style="display:none;"></asp:TextBox>
            </div>
            <div class="row">
                <p style="font-weight:bold;">Procurement information（"<span style=" background:red;">&nbsp;</span>" must write） </p>
                <table class="table table-condensed table-bordered">
                    <tr>
                        <td class="td-label">
                            <span style=" background:red;  height:30px; float:left;">&nbsp;</span>
                            <p style="text-align:center;">Cost center</p>
                        </td>
                        <td class="td-content"   >
                            <asp:TextBox runat="server" ID="fld_CostCenterDisplay" onclick="costCenter_onclick(this)" style="Width:95%"></asp:TextBox>
                            <asp:TextBox runat="server" ID="fld_CostCenterCode" style="display:none;"></asp:TextBox>
                        </td>
                       
                        <td class="td-label">
                            <span style="height:30px; float:left;">&nbsp;</span>
                            <p style="text-align:center;">Price terms</p>
                        </td>
                        <td class="td-content" >
                            <asp:TextBox runat="server" ID="fld_PriceTerms" Width="95%"></asp:TextBox>
                        </td>
                        
                    </tr>
                    <tr>
                        <td class="td-label">
                            <span style=" background:red;  height:30px; float:left;">&nbsp;</span>
                            <p style="text-align:center;">Currency</p>
                        </td>
                        <td class="td-content"  >
                            <asp:DropDownList runat="server" ID="currency" onchange="currency_onchange(this)"></asp:DropDownList>
                            <asp:TextBox runat="server" ID="fld_Currency" style="display:none;"></asp:TextBox>
                        </td>
                        <td class="td-label"> 
                            <span style=" background:red; margin-top:5px;  height:30px; float:left;">&nbsp;</span>
                            <p style="text-align:center;">VAT</p>
                        </td>
                        <td class="td-content"   >
                            <asp:RadioButton ID="vat1" runat="server" GroupName="Vat" Text="Yes" onclick="getButtonCheck(this,1)" /> &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                            <asp:RadioButton ID="vat2" runat="server" GroupName="Vat" Text="No" onclick="getButtonCheck(this,2)" />
                            <asp:TextBox runat="server"  ID="fld_VAT" style="display:none;"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="td-label" style="vertical-align:middle">
                            <span style="height:30px; float:left;">&nbsp;</span>
                            <p style="text-align:center;">Remarks</p>
                        </td>
                        <td class="td-content" colspan="3" >
                            <asp:TextBox runat="server" ID="fld_Remarks" TextMode="MultiLine" Rows="3" Width="98%"></asp:TextBox>
                        </td>
                    </tr>
                    <tr style="display:none">
                    <td class="td-label" style="vertical-align:middle">
                            <span style="height:30px; float:left;">&nbsp;</span>
                            <p style="text-align:center;">PR Type</p>
                        </td>
                        <td class="td-content" colspan="3" >
                             <input type="radio" id="PRType1" checked="checked" name="PRType" onclick="LoadPRType('1')" /> Normal
                             <input type="radio" id="PRType2" name="PRType" style="margin-left:20px"  onclick="LoadPRType('0')"/> Confidential
                             <asp:TextBox runat="server" ID="fld_PRType"  Text="1" style="display:none" ></asp:TextBox>
                        </td>
                    </tr>
                </table>
                <p style="font-weight:bold;">Request require（"<span style=" background:red;">&nbsp;</span>" must write） </p>
                <table class="table table-condensed table-bordered">
                    <tr>
                        <th width="1%"><p style="text-align:center">No.</p></th>
                        <%--<th width="10%"><p style="text-align:center">Items</p></th>--%>
                        <th width="18%"><p style="text-align:center">Material Description</p></th>
                        <th width="18%"><p style="text-align:center">Specification</p></th>
                        <th width="10%"><p style="text-align:center">Vendor</p></th>
                        <th width="9%"><p style="text-align:center">Request Date</p></th>
                        <th width="9%"><p style="text-align:center">Lead Time</p></th>
                        <th width="7%"><p style="text-align:center">Unit Price</p></th>
                        <th width="5%"><p style="text-align:center">Unit</p></th>
                        <th width="4%"><p style="text-align:center">Qty.</p></th>
                        <th width="10%"><p style="text-align:center">Amount</p></th>
                        <th width="9%"></th>
                    </tr>
                    <tbody id="detail">
                    <asp:Repeater runat="server" ID="fld_detail_PROC_Purchase_DT" OnItemCommand="fld_detail_PROC_Purchase_DT_ItemCommand" OnItemDataBound="fld_detail_PROC_Purchase_DT_ItemDataBound">
                        <ItemTemplate>
                            <tr>
                                <th style="text-align:center; width:1%">
                                    <span style=" background:red;">&nbsp;</span>
                                    <asp:TextBox ID="fld_FORMID" Text='<%#Eval("FORMID") %>' runat="server" Style="display: none"></asp:TextBox>
                                    <asp:Label ID="fld_ROWID" Text='<%# Container.ItemIndex+1%>' runat="server"></asp:Label>
                                </th>
                                <td>
                                    <%--<asp:DropDownList runat="server" ID="Item" Class="item"></asp:DropDownList>
                                    <asp:TextBox runat="server" ID="fld_Items" Text='<%#Eval("Items") %>' style="display:none;"></asp:TextBox>--%>
                                    <asp:TextBox runat="server" ID="fld_Description" Text='<%#Eval("Description") %>' CssClass="validate[required]" width="90%"></asp:TextBox>
                                </td>
                                <td>
                                    <asp:TextBox runat="server" ID="fld_Specification" Text='<%#Eval("Specification") %>' CssClass="validate[required]" width="90%"></asp:TextBox>
                                </td>
                                <td>
                                    <asp:TextBox runat="server" ID="fld_Vendor" Text='<%#Eval("Vendor") %>' width="84%"></asp:TextBox>
                                </td>
                                <td>
                                    <asp:TextBox runat="server" ID="fld_RequestDate" Text='<%#Eval("RequestDate").ToString()==""?"":Convert.ToDateTime(Eval("RequestDate").ToString()).ToString("yyy/MM/dd") %>' CssClass="validate[required]" onclick="WdatePicker({readOnly:false,dateFmt: 'yyyy-MM-dd'})" width="84%"></asp:TextBox>
                                </td>
                                <td>
                                    <asp:TextBox runat="server" ID="fld_LeadTime" Text='<%#Eval("LeadTime").ToString()==""?"":Convert.ToDateTime(Eval("LeadTime").ToString()).ToString("yyy/MM/dd") %>' CssClass="validate[required]" onclick="WdatePicker({readOnly:false,dateFmt: 'yyyy-MM-dd'})" width="84%"></asp:TextBox>
                                </td>
                                <td>
                                    <asp:TextBox runat="server" class="unitPrice" ID="fld_UnitPrice" Text='<%#Eval("UnitPrice") %>' onblur="getamount(this,'1')" CssClass="validate[required]" width="70%"></asp:TextBox>
                                </td>
                                <td>
                                    <asp:TextBox runat="server" ID="fld_Unit" Text='<%#Eval("Unit") %>' CssClass="validate[required]" width="53%"></asp:TextBox>
                                </td>
                                <td>
                                    <asp:TextBox runat="server" class="qty" ID="fld_Qty" Text='<%#Eval("Qty") %>' onblur="getamount(this,'2')" CssClass="validate[required]" width="53%"></asp:TextBox>
                                </td>
                                <td>
                                    <asp:Label runat="server"  class="money" ID="lb_Amount" Text='<%#Eval("Amount") %>' value="0.00" ></asp:Label>
                                    <asp:TextBox runat="server" class="amount" ID="fld_Amount" Text='<%#Eval("Amount") %>' value="0.00" CssClass="validate[required]" width="84%" onchange="amount_onchange(this)" style="display:none" ></asp:TextBox>
                                </td>
                                <td>
                                    <asp:Button ID="btnDelete" runat="server" Text="delete" CssClass="btn" CommandName="del" ClientIDMode="Static" OnClientClick="return confirm('Confirm Del？')" />
                                </td>
                            </tr>
                        </ItemTemplate>
                    </asp:Repeater>
                    </tbody>
                    <tr>
                        <td colspan="2" style="text-align:center">
                            <asp:Button ID="btnAdd" runat="server" Text="add" CssClass="btn" CausesValidation="false" OnClick="btnAdd_Click"/>
                        </td>
                        <td colspan="7">
                            <p style="text-align:right">Total Amount</p>
                        </td>
                        <td width="11%">
                            <asp:Label runat="server" CssClass="money" ID="lb_TotalAmount" value="0.00"></asp:Label>
                            <asp:TextBox runat="server" ID="fld_TotalAmount" value="0.00" Width="84%" style="display:none"></asp:TextBox>
                        </td>
                        <td></td>
                    </tr>
                   

                </table>
                 <table class="table table-condensed table-bordered">

                  <tr>
                    <td class="td-label" style="text-align:center">Department  budget of this year</td>
                    <td colspan="3" class="td-content" >
                    <asp:RadioButton ID="RadioButton1" runat="server" GroupName="Vatbudget" Text="Yes" onclick="getButtonCheck1(this,1)" />  &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 
                    <asp:RadioButton ID="RadioButton2" runat="server" GroupName="Vatbudget" Text="No" onclick="getButtonCheck1(this,2)" />
                    <asp:TextBox runat="server"  ID="fld_IsBudgetyear" style="display:none;"></asp:TextBox>
                    </td>
                    </tr>
                     <tr>
                    <td class="td-label" style="text-align:center">Budget Comments</td>
                    <td colspan="3">
                     <asp:TextBox runat="server" ID="fld_BudetComments" TextMode="MultiLine" Rows="3" Width="98%"></asp:TextBox>
                    </td>
                    </tr>


                    <tr>
                        <td class="td-label" style="text-align:center">
                            <p style="text-align:center">Single Supplier</p>
                        </td>
                        <td class="td-content" colspan="3">
                            <asp:CheckBox runat="server" ID="singleSupplier" onclick="singleSupplier_onclick(this)"/>
                            <asp:TextBox runat="server" ID="fld_SingleSupplier" style="display:none;"></asp:TextBox>
                        </td>
                        <td class="td-label" style="text-align:center;display:none;" id="supplierNameLabel">
                            <p style="text-align:center">Supplier Name</p>
                        </td>
                        <td class="td-content" colspan="3" style="display:none;" id="supplierNameValue">
                            <asp:TextBox runat="server" ID="fld_SupplierName" Width="90%"></asp:TextBox>
                        </td>
                    </tr>
                    <tr style="display:none;" id="reason">
                        <td class="td-label" style="text-align:center">
                            <p style="text-align:center">Reason</p>
                        </td>
                        <td class="td-content" colspan="7">
                            <asp:TextBox runat="server" ID="fld_SingleSupplierReason" Width="98%"></asp:TextBox>
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


