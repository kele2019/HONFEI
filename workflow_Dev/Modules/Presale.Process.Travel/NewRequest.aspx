<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="NewRequest.aspx.cs" Inherits="Presale.Process.Travel.NewRequest" %>

<%@ Register Src="/Modules/Ultimus.UWF.Form.ProcessControl/UserInfo.ascx" TagName="UserInfo" TagPrefix="ui" %>
<%@ Register Src="/Modules/Ultimus.UWF.Form.ProcessControl/ApprovalHistory.ascx" TagName="ApprovalHistory" TagPrefix="ah" %>
<%@ Register Src="/Modules/Ultimus.UWF.Form.ProcessControl/Attachments.ascx" TagName="Attachments" TagPrefix="attach" %>
<%@ Register Src="/Modules/Ultimus.UWF.Form.ProcessControl/ButtonList.ascx" TagName="ButtonList" TagPrefix="btn" %>
<%@ Import Namespace="Ultimus.UWF.Common.Logic" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta http-equiv="X-UA-Compatible"  content="IE=EmulateIE8" />
    <title>Travel Request Process</title>
    <script type="text/javascript" src="/Assets/js/common.js"></script>
    <script type="text/javascript">
        function beforeSubmit() {
            
            $("#Attachments1_txtMust").val("1");
            var summary = "Travel Request Process";
            $("#UserInfo1_fld_PROCESSSUMMARY").val(summary);
            var PurposeTrip = $("#Purposetrip").find("tr").length;
            if (PurposeTrip <= 1) {
                alert("Purpose of trip invalid");
                return false;
            }
            return true;
        }
        function beforeSave() {
            var summary = "Travel Request Process";
            $("#UserInfo1_fld_PROCESSSUMMARY").val(summary);
            $("#fld_TRSummary").val(summary);
            return true;
        }
        $(document).ready(function () {
            if ($("#hdIncident").val() != "") {
                $("#ButtonList1_btnSubmit").val("Submit");
                $("#ButtonList1_btnBack").hide();
                $("#ButtonList1_btnReject").show();
            }
            if ($("#hdUrgeTask").val() == "Yes") {
                $("#ReturnBackTask").show();
            }
            var rate = $("#fld_Rate").val();
            getTotalMoney(rate);
        });
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
                WdatePicker({ dateFmt: 'yyyy-MM-dd', minDate: StartDate });
            }
            else {
                WdatePicker({ dateFmt: 'yyyy-MM-dd', maxDate: EndDate });
            }
        }
        function getDays(obj, index) {
            if (index == "1") {
                var date1 = new Date($(obj).val().replace(/-/g, '/'));
                var date2 = new Date($(obj).parent().next().children().val().replace(/-/g, '/'));
                var time = date2.getTime() - date1.getTime();
                var dates = Math.floor(time / (24 * 60 * 60 * 1000));
                $(obj).parent().next().next().children().val((dates + 1).toString() == "NaN" ? "" : (dates + 1).toString());
                $(obj).parent().next().next().next().children().val(dates.toString() == "NaN" ? "" : dates.toString());
            }
            if (index == "2") {
                var date1 = new Date($(obj).parent().prev().children().val().replace(/-/g, '/'));
                var date2 = new Date($(obj).val().replace(/-/g, '/'));
                var time = date2.getTime() - date1.getTime();
                var dates = Math.floor(time / (24 * 60 * 60 * 1000));
                $(obj).parent().next().children().val((dates + 1).toString() == "NaN" ? "" : (dates + 1).toString());
                $(obj).parent().next().next().children().val(dates.toString() == "NaN" ? "" : dates.toString());
            }
        }
        function getUSD(obj) {
            var RMB = $(obj).val() - 0; ;
            $(obj).val(RMB.toFixed(2));
            var rate = $("#fld_Rate").val();
            var USD = RMB / rate;
            $(obj).parent().next().children().val(USD.toFixed(2));
            getTotalMoney(rate)
        }
        function getRMB(obj) {
            var USD = $(obj).val() - 0 ;
            $(obj).val(USD.toFixed(2));
            var rate = $("#fld_Rate").val();
            var RMB = USD * rate;
            $(obj).parent().prev().children().val(RMB.toFixed(2));
            getTotalMoney(rate);
        }
        function getTotalMoney(rate) {
            var airline = $("#fld_AirlineRMB").val()-0;
            var hotel = $("#fld_HotelRMB").val()-0;
            var groundTrans = $("#fld_GroundTransRMB").val()-0;
            var meal = $("#fld_MealRMB").val()-0;
            var costs = $("#fld_CostsRMB").val()-0;
            var totalRMB = airline + hotel + groundTrans + meal + costs;
            var totalUSD = totalRMB / rate;
            $("#fld_TotalRMB").val(totalRMB.toFixed(2));
            $("#fld_TotalUSD").val(totalUSD.toFixed(2));
            $("#lb_TotalRMB").text(formatNumber(totalRMB.toFixed(2), 2, 1));
            $("#lb_TotalUSD").text(formatNumber(totalUSD.toFixed(2), 2, 1));
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div id="myDiv" class="container">
        <div class="row">
            <ui:userinfo id="UserInfo1" processtitle="Travel Request Process" processprefix="TR" tablename="PROC_Travel"
                tablenamedetail="PROC_TravelOne_DT,PROC_TravelSecond_DT" runat="server"></ui:userinfo>
            <asp:TextBox runat="server" ID="fld_USERPOST" style="display:none;"></asp:TextBox>
            <asp:TextBox runat="server" ID="fld_DGMLogin" style="display:none;"></asp:TextBox>
            <asp:TextBox runat="server" ID="fld_GMLogin" style="display:none;"></asp:TextBox>
            <asp:TextBox runat="server" ID="fld_deptLogin" style="display:none"></asp:TextBox>
            <asp:TextBox runat="server" ID="fld_CompareValue" value="1500.00" style="display:none;"></asp:TextBox>
            <p style="font-weight:bold">Traval Detail（"<span style=" background:red;">&nbsp;</span>" must write）</p>
            <table class="table table-condensed table-bordered">
                <tr>
                    <th width="1%"><p style="text-align:center">NO.</p></th>
                    <th colspan="2" width="26%"><p style="text-align:center">Schedule</p></th>
                    <th colspan="2" width="26%"><p style="text-align:center">Date</p></th>
                    <th width="13%"><p style="text-align:center">Dur ation</p></th>
                    <th width="13%"><p style="text-align:center">Hotel Room</p></th>
                    <th width="13%"><p style="text-align:center">Remark</p></th>
                    <th width="10%"></th>
                </tr>
                <tr>
                    <th></th>
                    <th><p style="text-align:center">From</p></th>
                    <th><p style="text-align:center">To</p></th>
                    <th><p style="text-align:center">From</p></th>
                    <th><p style="text-align:center">To</p></th>
                    <th><p style="text-align:center">Days</p></th>
                    <th><p style="text-align:center">Night</p></th>
                    <th></th>
                    <th></th>
                </tr>
            <tbody id="detailtable">
                <asp:Repeater runat="server" ID="fld_detail_PROC_TravelOne_DT" OnItemCommand="fld_detail_PROC_TravelOne_DT_ItemCommand" OnItemDataBound="fld_detail_PROC_TravelOne_DT_ItemDataBound">
                    <ItemTemplate>
                        <tr>
                            <th style="text-align:center"> 
                                <span style=" background:red;">&nbsp;</span>
                                <asp:TextBox ID="fld_FORMID" Text='<%#Eval("FORMID") %>' runat="server" Style="display: none"></asp:TextBox>
                                <asp:Label ID="fld_ROWID" Text='<%# Container.ItemIndex+1%>' runat="server"></asp:Label>
                            </th>
                            <td>
                                <asp:TextBox ID="fld_ScheduleFrom" Text='<%#Eval("ScheduleFrom") %>' MaxLength="14" runat="server" Width="88%"></asp:TextBox>
                            </td>
                            <td>
                                <asp:TextBox ID="fld_ScheduleTo" Text='<%#Eval("ScheduleTo") %>' MaxLength="14" runat="server" Width="88%"></asp:TextBox>
                            </td>
                            <td>
                                <asp:TextBox ID="fld_DateFrom" onfocus="ShowTime(this,'1');" onblur="getDays(this,1)" Text='<%# String.IsNullOrEmpty(Eval("DateFrom").ToString())? "":DateTime.Parse(Eval("DateFrom").ToString()).ToString("yyyy-MM-dd") %>' runat="server" CssClass="validate[required]" Width="88%"></asp:TextBox>
                            </td>
                            <td>
                                <asp:TextBox ID="fld_DateTo" onfocus="ShowTime(this,'2');" onblur="getDays(this,2)" Text='<%# String.IsNullOrEmpty(Eval("DateTo").ToString())? "":DateTime.Parse(Eval("DateTo").ToString()).ToString("yyyy-MM-dd") %>' runat="server" CssClass="validate[required]" Width="88%"></asp:TextBox>
                            </td>
                            <td>
                                <asp:TextBox ID="fld_Days" Text='<%#Eval("Days") %>' runat="server" CssClass="validate[required]" Width="88%"></asp:TextBox>
                            </td>
                            <td>
                                <asp:TextBox ID="fld_Night" Text='<%#Eval("Night") %>' runat="server" CssClass="validate[required]" Width="88%"></asp:TextBox>
                            </td>
                            <td>
                                <asp:TextBox ID="fld_Remark" Text='<%#Eval("Remark") %>' MaxLength="14" runat="server" Width="88%"></asp:TextBox>
                            </td>
                            <td>
                                <asp:Button ID="btnDelete" runat="server" Text="Delete" CssClass="btn" CommandName="del" ClientIDMode="Static" OnClientClick="return confirm('Confirm Del？')" />
                            </td>
                        </tr>
                    </ItemTemplate>
                </asp:Repeater>
            </tbody>
                <tr>
                    <th></th>
                    <td colspan="8" style="text-align:center">
                        <asp:Button ID="btnAdd" runat="server" Text="Add" CssClass="btn" CausesValidation="false" OnClick="btnAdd_Click" style="float:left" />
                    </td>
                </tr>
            </table>
            </div>
            <div class="row">
                <p style="font-weight:bold">Purpose of trip（"<span style=" background:red;">&nbsp;</span>" must write）</p>
                <table class="table table-condensed table-bordered" id="Purposetrip">
                    <asp:Repeater runat="server" ID="fld_detail_PROC_TravelSecond_DT" OnItemCommand="fld_detail_PROC_TravelSecond_DT_ItemCommand" OnItemDataBound="fld_detail_PROC_TravelSecond_DT_ItemDataBound">
                        <ItemTemplate>
                            <tr>
                                <th width="3%" style="text-align:center"> 
                                    <span style=" background:red;">&nbsp;</span>
                                    <asp:TextBox ID="fld_FORMID" Text='<%#Eval("FORMID") %>' runat="server" Style="display: none"></asp:TextBox>
                                    <asp:Label ID="fld_ROWID" Text='<%# Container.ItemIndex+1%>' runat="server"></asp:Label>
                                </th>
                                <td>
                                    <asp:TextBox runat="server" ID="fld_PurposeOfTrip" Text='<%#Eval("PurposeOfTrip") %>' MaxLength="100"  CssClass="validate[required]" Width="98%"></asp:TextBox>
                                </td>
                                <td width="7%" align="center">
                                    <asp:Button ID="btnDelete" runat="server" Text="Delete" CssClass="btn" CommandName="del" ClientIDMode="Static" OnClientClick="return confirm('Confirm Del？')" />
                                </td>
                            </tr>
                        </ItemTemplate>
                    </asp:Repeater>
                    <tr>
                        <th width="3%"></th>
                        <td colspan="2">
                            <asp:Button ID="Button1" runat="server" Text="Add" CssClass="btn" CausesValidation="false" OnClick="btnAddPurpost_Click"   />
                        </td>
                    </tr>
                </table>
            </div>
            <div class="row">
                <p style="font-weight:bold;">Traval Application</p>
                <table class="table table-condensed table-bordered">
                    <tr>
                        <th width="15%"></th>
                        <th width="45%"><p style="text-align:center;">Remark</p></th>
                        <th><p style="text-align:center">Day</p></th>
                        <th><p style="text-align:center">RMB</p></th>
                        <th><p style="text-align:center">USD</p></th>
                    </tr>
                    <tr>
                        <th><p style="text-align:center">Airline/Train</p></th>
                        <td>
                            <asp:TextBox runat="server" ID="fld_AirlineFCRG" MaxLength="50" Width="97%"></asp:TextBox>
                        </td>
                        <td>
                            <asp:TextBox runat="server" ID="fld_AirlineDay"  Width="90%"></asp:TextBox>
                        </td>
                        <td>
                            <asp:TextBox runat="server" ID="fld_AirlineRMB" value="0.00" onblur="getUSD(this)" Width="90%"></asp:TextBox>
                        </td>
                        <td>
                            <asp:TextBox runat="server" ID="fld_AirlineUSD" value="0.00" onblur="getRMB(this)" Width="90%"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <th><p style="text-align:center">Hotel</p></th>
                        <td>
                            <asp:TextBox runat="server" ID="fld_HotelFCRG" MaxLength="50" Width="97%"></asp:TextBox>
                        </td>
                        <td>
                            <asp:TextBox runat="server" ID="fld_HotelDay"  Width="90%"></asp:TextBox>
                        </td>
                        <td>
                            <asp:TextBox runat="server" ID="fld_HotelRMB" value="0.00" onblur="getUSD(this)" Width="90%"></asp:TextBox>
                        </td>
                        <td>
                            <asp:TextBox runat="server" ID="fld_HotelUSD" value="0.00" onblur="getRMB(this)" Width="90%"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <th><p style="text-align:center">Ground Transportation</p></th>
                        <td>
                            <asp:TextBox runat="server" ID="fld_GroundTransFCRG" MaxLength="50" Width="97%"></asp:TextBox>
                        </td>
                        <td>
                            <asp:TextBox runat="server" ID="fld_GroundTransDay" Width="90%"></asp:TextBox>
                        </td>
                        <td>
                            <asp:TextBox runat="server" ID="fld_GroundTransRMB" value="0.00" onblur="getUSD(this)"  Width="90%"></asp:TextBox>
                        </td>
                        <td>
                            <asp:TextBox runat="server" ID="fld_GroundTransUSD" value="0.00" onblur="getRMB(this)"  Width="90%"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <th><p style="text-align:center">Meal</p></th>
                        <td>
                            <asp:TextBox runat="server" ID="fld_MealFCRG" MaxLength="50" Width="97%"></asp:TextBox>
                        </td>
                        <td>
                            <asp:TextBox runat="server" ID="fld_MealDay" Width="90%"></asp:TextBox>
                        </td>
                        <td>
                            <asp:TextBox runat="server" ID="fld_MealRMB" value="0.00" onblur="getUSD(this)"  Width="90%"></asp:TextBox>
                        </td>
                        <td>
                            <asp:TextBox runat="server" ID="fld_MealUSD" value="0.00" onblur="getRMB(this)" Width="90%"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <th><p style="text-align:center">Misc Costs</p></th>
                        <td>
                            <asp:TextBox runat="server" ID="fld_CostsFCRG" MaxLength="50"  Width="97%"></asp:TextBox>
                        </td>
                        <td>
                            <asp:TextBox runat="server" ID="fld_CostsDay"  Width="90%"></asp:TextBox>
                        </td>
                        <td>
                            <asp:TextBox runat="server" ID="fld_CostsRMB" value="0.00" onblur="getUSD(this)" Width="90%"></asp:TextBox>
                        </td>
                        <td>
                            <asp:TextBox runat="server" ID="fld_CostsUSD" value="0.00" onblur="getRMB(this)" Width="90%"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <th colspan="3"><p style="text-align:right">Estimated Total</p></th>
                        <td>
                            <asp:Label runat="server" ID="lb_TotalRMB" Text="0.00"></asp:Label>
                            <asp:TextBox runat="server" ID="fld_TotalRMB" value="0.00" Width="90%" style="display:none"></asp:TextBox>
                        </td>
                        <td>
                          <asp:Label runat="server" ID="lb_TotalUSD" Text="0.00"></asp:Label>
                            <asp:TextBox runat="server" ID="fld_TotalUSD" value="0.00"  Width="90%" style="display:none"></asp:TextBox>
                        </td>
                    </tr>
                    <tr style="display:none;">
                        <th colspan="3"><p style="text-align:right">Ex Rate</p></th>
                        <td colspan="2">
                            <asp:TextBox runat="server" ID="fld_Rate" Width="95%"></asp:TextBox>
                        </td>
                    </tr>
                </table>
                
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
