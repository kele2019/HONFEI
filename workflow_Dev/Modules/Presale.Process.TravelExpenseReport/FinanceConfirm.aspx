<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="FinanceConfirm.aspx.cs" Inherits="Presale.Process.TravelExpenseReport.FinanceConfirm" %>
<%@ Register Src="/Modules/Ultimus.UWF.Form.ProcessControl/UserInfo.ascx" TagName="UserInfo" TagPrefix="ui" %>
<%@ Register Src="/Modules/Ultimus.UWF.Form.ProcessControl/ApprovalHistory.ascx" TagName="ApprovalHistory" TagPrefix="ah" %>
<%@ Register Src="/Modules/Ultimus.UWF.Form.ProcessControl/Attachments.ascx" TagName="Attachments" TagPrefix="attach" %>
<%@ Register Src="/Modules/Ultimus.UWF.Form.ProcessControl/ButtonList.ascx" TagName="ButtonList" TagPrefix="btn" %>
<%@ Import Namespace="Ultimus.UWF.Common.Logic" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta http-equiv="X-UA-Compatible"  content="IE=EmulateIE8" />
    <title>Travel Expense Request Process</title>
    <script type="text/javascript" src="/Assets/js/common.js"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            $("#ButtonList1_btnSubmit").val("Complete");
            $("#detail").find("tbody").each(function (i, Etr) {
                var paidByMeal = $(Etr).find("tr").eq(2).find("td").eq(1).children().last().text();
                if (paidByMeal == "Paid By Company") {
                    $(Etr).find("tr").eq(2).find("td").eq(1).children().find("input").attr("checked", true);
                }
                var paidByBME = $(Etr).find("tr").eq(2).find("td").eq(2).children().last().text();
                if (paidByBME == "Paid By Company") {
                    $(Etr).find("tr").eq(2).find("td").eq(2).children().find("input").attr("checked", true);
                }
                var paidByAir = $(Etr).find("tr").eq(2).find("td").eq(3).children().last().text();
                if (paidByAir == "Paid By Company") {
                    $(Etr).find("tr").eq(2).find("td").eq(3).children().find("input").attr("checked", true);
                }
                var paidByHotel = $(Etr).find("tr").eq(2).find("td").eq(4).children().last().text();
                if (paidByHotel == "Paid By Company") {
                    $(Etr).find("tr").eq(2).find("td").eq(4).children().find("input").attr("checked", true);
                }
                var paidByTB = $(Etr).find("tr").eq(2).find("td").eq(5).children().last().text();
                if (paidByTB == "Paid By Company") {
                    $(Etr).find("tr").eq(2).find("td").eq(5).children().find("input").attr("checked", true);
                }
                var paidByLau = $(Etr).find("tr").eq(2).find("td").eq(6).children().last().text();
                if (paidByLau == "Paid By Company") {
                    $(Etr).find("tr").eq(2).find("td").eq(6).children().find("input").attr("checked", true);
                }
                var paidByTT = $(Etr).find("tr").eq(2).find("td").eq(7).children().last().text();
                if (paidByTT == "Paid By Company") {
                    $(Etr).find("tr").eq(2).find("td").eq(7).children().find("input").attr("checked", true);
                }
                var paidByOthers = $(Etr).find("tr").eq(2).find("td").eq(8).children().last().text();
                if (paidByOthers == "Paid By Company") {
                    $(Etr).find("tr").eq(2).find("td").eq(8).children().find("input").attr("checked", true);
                }
            });
        });
        function submitPageReview(obj) {
            $("#ButtonList1_btnSubmit").click();
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div id="myDiv" class="container">
        <div class="row">
            <ui:userinfo id="UserInfo1" processtitle="Travel Expense Request Process" processprefix="FINTE" tablename="PROC_TravelExpense"
                tablenamedetail="PROC_TravelExpenseDetails_DT,PROC_TravelExpenseThird_DT,PROC_TravelExpenseForth_DT" runat="server"></ui:userinfo>
            <asp:TextBox runat="server" ID="fld_ApprovalUserPost" style="display:none;"></asp:TextBox>
            <p style="font-weight:bold">Travel Detail(BM/E=Bussiness Meals/Entert,T/B=Taxi/Bus,TT=Train Ticket)</p>
            <table class="table table-condensed table-bordered" id="detail">
                <thead>
                <tr>
                    <th colspan="3" style="vertical-align:middle;width:27%"><span style="float:left">&nbsp;</span><p style="text-align:center">BusIness Purpose of Trip</p></th>
                    <td colspan="8">
                        <asp:Label runat="server" ID="read_PurposeOfTrip"></asp:Label>
                    </td>
                    <th style="vertical-align:middle;"><p style="text-align:center">Rate</p></th>
                    <td style="text-align:center"><asp:Label runat="server" ID="read_Rate"></asp:Label></td>
                </tr>
                <tr>
                        <td class="td-label" colspan="3" >
                            <span style="float:left">&nbsp;</span>
                            <p style="text-align:center">Employee</p>
                        </td>
                        <td class="td-content" colspan="2">
                            <asp:Label runat="server" ID="read_EmployeeDisplay"></asp:Label>
                        </td>
                        <td class="td-label" colspan="2" style="text-align:center"><p style="text-align:center">Cost Center</p></td>
                        <td class="td-content" colspan="3">
                            <asp:Label runat="server" ID="read_CostCenterDisplay"></asp:Label>
                        </td>
                        <td class="td-label"><p style="text-align:center">Project</p></td>
                        <td class="td-content" colspan="2">
                            <asp:Label runat="server" ID="read_Project"></asp:Label>
                        </td>
                    </tr>
                <tr>
                    <th width="1%"><p style="text-align:center">NO.</p></th>
                    <th width="8.3%"><p style="text-align:center">Location</p></th>
                    <th width="8.3%"><p style="text-align:center">Date</p></th>
                    <th width="7.7%"><p style="text-align:center">Meal</p></th>
                    <th width="7.7%"><p style="text-align:center">BM/E</p></th>
                    <th width="7.7%"><p style="text-align:center">Airfare</p></th>
                    <th width="7.7%"><p style="text-align:center">Hotel</p></th>
                    <th width="7.7%"><p style="text-align:center">T/B</p></th>
                    <th width="7.7%"><p style="text-align:center">Laundry</p></th>
                    <th width="7.7%"><p style="text-align:center">TT</p></th>
                    <th width="7.7%"><p style="text-align:center">Others</p></th>
                    <th width="10%"><p style="text-align:center">T.USD</p></th>
                    <th width="10%"><p style="text-align:center">T.RMB</p></th>
                </tr>
                </thead>
            <asp:Repeater runat="server" ID="read_detail_PROC_TravelExpenseDetails_DT">
                    <ItemTemplate>
                        <tbody>
                        <tr>
                            <th style="text-align:center">
                                <span style="float:left">&nbsp;</span> 
                                <asp:TextBox ID="fld_FORMID" Text='<%#Eval("FORMID") %>' runat="server" Style="display: none"></asp:TextBox>
                                <asp:Label ID="fld_ROWID" Text='<%# Container.ItemIndex+1%>' runat="server"></asp:Label>
                            </th>
                            <td style="text-align:center">
                                <asp:Label ID="read_TravelFrom" Text='<%#Eval("TravelFrom") %>' runat="server"></asp:Label>
                            </td>
                            <td style="text-align:center">
                                <asp:Label ID="read_ItemValue" runat="server" Text='<%# String.IsNullOrEmpty(Eval("ItemValue").ToString())? "":DateTime.Parse(Eval("ItemValue").ToString()).ToString("yyyy-MM-dd") %>'></asp:Label>
                            </td>
                            <td style="text-align:center">
                                <asp:Label ID="read_Meal" Text='<%#Eval("Meal") %>' runat="server" ></asp:Label>
                            </td>
                            <td style="text-align:center">
                                <asp:Label ID="read_BME" Text='<%#Eval("BME") %>' runat="server"></asp:Label>
                            </td>
                            <td style="text-align:center">
                                <asp:Label ID="read_Air" Text='<%#Eval("Air") %>' runat="server"></asp:Label>
                            </td>
                            <td style="text-align:center">
                                <asp:Label ID="read_Hotel" Text='<%#Eval("Hotel") %>' runat="server"></asp:Label>
                            </td>
                            <td style="text-align:center">
                                <asp:Label ID="read_TB" Text='<%#Eval("TB") %>' runat="server"></asp:Label>
                            </td>
                            <td style="text-align:center">
                                <asp:Label ID="read_Lau" Text='<%#Eval("Lau") %>' runat="server"></asp:Label>
                            </td>
                             <td style="text-align:center">
                                <asp:Label ID="read_TT" Text='<%#Eval("TT") %>' runat="server"></asp:Label>
                            </td>
                             <td style="text-align:center">
                                <asp:Label ID="read_Others" Text='<%#Eval("Others") %>' runat="server"></asp:Label>
                            </td>
                            <td style="text-align:center">
                                <asp:Label ID="read_USD" Text='<%#Eval("USD") %>' runat="server"></asp:Label>
                            </td>
                             <td style="text-align:center">
                                <asp:Label ID="read_RMB" Text='<%#Eval("RMB") %>' runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <th></th>
                            <td colspan="2"><p style="text-align:right">Currency</p></td>
                            <td style="text-align:center">
                                <asp:Label runat="server" ID="read_CurrencyMeal" Text='<%#Eval("CurrencyMeal") %>'></asp:Label>
                            </td>
                            <td style="text-align:center">
                                <asp:Label runat="server" ID="read_CurrencyBME" Text='<%#Eval("CurrencyBME") %>'></asp:Label>
                            </td>
                            <td style="text-align:center">
                                <asp:Label runat="server" ID="read_CurrencyAir" Text='<%#Eval("CurrencyAir") %>'></asp:Label>
                            </td>
                            <td style="text-align:center">
                                <asp:Label runat="server" ID="read_CurrencyHotel" Text='<%#Eval("CurrencyHotel") %>'></asp:Label>
                            </td>
                            <td style="text-align:center">
                                <asp:Label runat="server" ID="read_CurrencyTB" Text='<%#Eval("CurrencyTB") %>'></asp:Label>
                            </td>
                            <td style="text-align:center">
                                <asp:Label runat="server" ID="read_CurrencyLau" Text='<%#Eval("CurrencyLau") %>'></asp:Label>
                            </td>
                            <td style="text-align:center">
                                <asp:Label runat="server" ID="read_CurrencyTT" Text='<%#Eval("CurrencyTT") %>'></asp:Label>
                            </td>
                            <td style="text-align:center">
                                <asp:Label runat="server" ID="read_CurrencyOther" Text='<%#Eval("CurrencyOther") %>'></asp:Label>
                            </td>
                            <td colspan="2"></td>
                        </tr>
                        <tr>
                            <th><span style="float:left">&nbsp;</span> </th>
                            <td colspan="2"><p style="float:right">Paid By Company</p></td>
                            <td style="text-align:center">
                                <asp:CheckBox ID="CheckBox1" class="CheckBox1" runat="server" Enabled="false" onclick="getButtonCheck(this)"></asp:CheckBox>
                                <asp:Label ID="read_payMeal" Text='<%#Eval("payMeal") %>' runat="server" style="display:none;"></asp:Label>
                            </td>
                            <td style="text-align:center">
                               <asp:CheckBox ID="CheckBox2" class="CheckBox2" runat="server" Enabled="false" onclick="getButtonCheck(this)"></asp:CheckBox>
                                <asp:Label ID="read_payBME" Text='<%#Eval("payBME") %>' runat="server" style="display:none;"></asp:Label>
                            </td>
                            <td style="text-align:center">
                               <asp:CheckBox ID="CheckBox3" class="CheckBox3" runat="server" Enabled="false" onclick="getButtonCheck(this)"></asp:CheckBox>
                                <asp:Label ID="read_payAir" Text='<%#Eval("payAir") %>' runat="server" style="display:none;"></asp:Label>
                            </td>
                            <td style="text-align:center">
                                <asp:CheckBox ID="CheckBox4" class="CheckBox4" runat="server" Enabled="false" onclick="getButtonCheck(this)"></asp:CheckBox>
                                <asp:Label ID="read_payHotel" Text='<%#Eval("payHotel") %>' runat="server" style="display:none;"></asp:Label>
                            </td>
                            <td style="text-align:center">
                                <asp:CheckBox ID="CheckBox5" class="CheckBox5" runat="server" Enabled="false" onclick="getButtonCheck(this)"></asp:CheckBox>
                                <asp:Label ID="read_payTB" Text='<%#Eval("payTB") %>' runat="server" style="display:none;"></asp:Label>
                            </td>
                            <td style="text-align:center">
                                <asp:CheckBox ID="CheckBox6" class="CheckBox6" runat="server" Enabled="false" onclick="getButtonCheck(this)"></asp:CheckBox>
                                <asp:Label ID="read_payLau" Text='<%#Eval("payLau") %>' runat="server" style="display:none;"></asp:Label>
                            </td>
                            <td style="text-align:center">
                                <asp:CheckBox ID="CheckBox7" class="CheckBox8" runat="server" Enabled="false" onclick="getButtonCheck(this)"></asp:CheckBox>
                                <asp:Label ID="read_payTT" Text='<%#Eval("payTT") %>' runat="server" style="display:none;"></asp:Label>
                            </td>
                             <td style="text-align:center">
                               <asp:CheckBox ID="CheckBox8" class="CheckBox9" runat="server" Enabled="false" onclick="getButtonCheck(this)"></asp:CheckBox>
                                <asp:Label ID="read_payOthers" Text='<%#Eval("payOthers") %>' runat="server" style="display:none;"></asp:Label>
                            </td>
                            <td colspan="2"></td>
                        </tr>
                        </tbody>
                    </ItemTemplate>
                </asp:Repeater>
                <tfoot>
              <tr>
                    <th></th>
                    <td colspan="2">
                        <p style="text-align:right;">Total</p>
                    </td>
                    <td>
                      <asp:Label ID="read_TotalMeal" runat="server"></asp:Label>
                    </td>
                    <td>
                       <asp:Label ID="read_TotalBME" runat="server"></asp:Label>
                    </td>
                     <td>
                        <asp:Label ID="read_TotalAirfare" runat="server"></asp:Label>
                    </td> 
                    <td>
                        <asp:Label ID="read_TotalHotel" runat="server"></asp:Label>
                    </td>
                    <td>
                        <asp:Label ID="read_TotalTB" runat="server"></asp:Label>
                    </td>
                    <td>
                       <asp:Label ID="read_TotalLaundry" runat="server"></asp:Label>
                    </td>
                    <td>
                       <asp:Label ID="read_TotalTT" runat="server"></asp:Label>
                    </td>
                    <td>
                        <asp:Label ID="read_TotalOthers" runat="server"></asp:Label>
                    </td>
                    <td style="text-align:center">
                        <asp:Label ID="read_TotalUSD" runat="server"></asp:Label>
                    </td>
                    <td style="text-align:center">
                        <asp:Label ID="read_TotalRMB" runat="server"></asp:Label>
                    </td>
                </tr>
                 <tr>
                    <th></th>
                    <td colspan="11"><p style="float:right">Total(RMB)</p></td>
                    <%--<td> <asp:TextBox ID="fld_PaidByCompanyUSD" runat="server" value="0.00" width="84%"></asp:TextBox></td>--%>
                     <td>
                        <asp:Label ID="read_RMB" runat="server"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <th></th>
                    <td colspan="11"><p style="float:right">Paid By Company(RMB)</p></td>
                     <td style="text-align:center">
                        <asp:Label ID="read_PaidByCompanyRMB" runat="server"></asp:Label>
                    </td>
                    
                </tr>
                <tr>
                    <th></th>
                    <td colspan="11"><p style="float:right">Paid By Employee(RMB)</p></td>
                      <td style="text-align:center">
                                <asp:Label ID="read_PaidByEmployeeRMB" runat="server"></asp:Label>
                            </td>
                </tr>
                </tfoot>
            </table>
        </div>
            <div class="row">
             <table class="table table-condensed table-bordered">
                    <tr>
                        <td class="td-label">
                        <span style="height:30px; float:left;">&nbsp;</span>
                            <p style="text-align:center">Travel Request NO.</p>
                        </td>
                        <td class="td-content" colspan="3">
                            <asp:Label runat="server" ID="read_TravelRequestNo"></asp:Label>
                        </td>
                    </tr>
                </table>
                <table class="table table-condensed table-bordered">
                <tr>
                    <td class="td-label" style="vertical-align:middle"><p style="text-align:center">Remark</p></td>
                    <td class="td-content" colspan="3">
                        <asp:Label runat="server" ID="read_Remark"></asp:Label>
                    </td>
                </tr>
                </table>
            </div>
            <div class="row">
                <p style="font-weight:bold;">ENTERTAINMENT (if space is insufficient,please attach a separate entertainment report)</p>
                <table class="table table-condensed table-bordered">
                    <tr>
                        <th width="24.5"><p style="text-align:center">Date</p></th>
                        <th width="24.5"><p style="text-align:center">Amount</p></th>
                        <th width="24.5"><p style="text-align:center">Guest Name & Company</p></th>
                        <th width="24.5"><p style="text-align:center">Business Purpose</p></th>
                    </tr>
                    <asp:Repeater runat="server" ID="read_detail_PROC_TravelExpenseThird_DT">
                        <ItemTemplate>
                        <tr>
                            <td style="text-align:center">
                                <asp:Label runat="server" ID="read_Date" Text='<%# String.IsNullOrEmpty(Eval("Date").ToString())? "":DateTime.Parse(Eval("Date").ToString()).ToString("yyyy-MM-dd") %>'></asp:Label>
                            </td>
                            <td style="text-align:center">
                                <asp:Label runat="server" ID="read_Amount" Text='<%#Eval("Amount") %>'></asp:Label>
                            </td>
                            <td style="text-align:center">
                                <asp:Label runat="server" ID="read_GuestNameCompany" Text='<%#Eval("GuestNameCompany") %>'></asp:Label>
                            </td>
                            <td style="text-align:center">
                                <asp:Label runat="server" ID="read_BusinessPurpose" Text='<%#Eval("BusinessPurpose") %>'></asp:Label>
                            </td>
                        </tr>
                        </ItemTemplate>
                     </asp:Repeater>
                 </table>
                 <table class="table table-condensed table-bordered">
                     <tr>
                        <th colspan="4"><p style="text-align:left;font-weight:bold;">Special Case:</p></th>
                     </tr>
                     <tr>
                        <th width="24.5%"><p style="text-align:center">Itme</p></th>
                        <th width="24.5%"><p style="text-align:center">Date</p></th>
                        <th width="24.5%"><p style="text-align:center">Amount</p></th>
                        <th width="24.5%"><p style="text-align:center">Reason</p></th>
                    </tr>
                     <asp:Repeater runat="server" ID="read_detail_PROC_TravelExpenseForth_DT">
                        <ItemTemplate>
                        <tr>
                            <td style="text-align:center">
                                <asp:Label runat="server" ID="read_Itme" Text='<%#Eval("Itme") %>'></asp:Label>
                            </td>
                            <td style="text-align:center">
                                <asp:Label runat="server" ID="read_Date" Text='<%# String.IsNullOrEmpty(Eval("Date").ToString())? "":DateTime.Parse(Eval("Date").ToString()).ToString("yyyy-MM-dd") %>'></asp:Label>
                            </td>
                            <td style="text-align:center">
                                <asp:Label runat="server" ID="read_Amount" Text='<%#Eval("Amount") %>'></asp:Label>
                            </td>
                            <td style="text-align:center">
                                <asp:Label runat="server" ID="read_Reason" Text='<%#Eval("Reason") %>'></asp:Label>
                            </td>
                        </tr>
                        </ItemTemplate>
                     </asp:Repeater>
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
            <asp:TextBox runat="server" ID="fld_TRSummary"></asp:TextBox>
            <asp:HiddenField runat="server" ID="hdIncident" />
            <asp:HiddenField runat="server" ID="hdPrint"  Value="0" />
        </div>
    </form>
</body>
</html>

     
