<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Approval.aspx.cs" Inherits="Presale.Process.Travel.Approval" %>

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
    <script type="text/javascript" >
//        function beforeSubmit() {
//            alert("test");
//            alert($("#fld_ApprovalUserPost").val());
//        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div id="myDiv" class="container">
        <div class="row">
            <ui:userinfo id="UserInfo1" processtitle="Travel Request Process" processprefix="TR" tablename="PROC_Travel"
                tablenamedetail="PROC_TravelOne_DT,PROC_TravelSecond_DT" runat="server"></ui:userinfo>
            <asp:TextBox runat="server" ID="fld_ApprovalUserPost" style="display:none;"></asp:TextBox>
            <p style="font-weight:bold">Travel Detail</p>
            <table class="table table-condensed table-bordered">
                <tr>
                    <th width="1%"><p style="text-align:center">NO.</p></th>
                    <th colspan="2" width="28%"><p style="text-align:center">Schedule</p></th>
                    <th colspan="2" width="28%"><p style="text-align:center">Date</p></th>
                    <th width="14.2%"><p style="text-align:center">Dur ation</p></th>
                    <th width="14.2%"><p style="text-align:center">Hotel Room</p></th>
                    <th width="14.2%"><p style="text-align:center">Remark</p></th>
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
                </tr>
            <tbody id="detailtable">
                <asp:Repeater runat="server" ID="fld_detail_PROC_TravelOne_DT">
                    <ItemTemplate>
                        <tr>
                            <th style="text-align:center"> 
                                <asp:TextBox ID="read_FORMID" Text='<%#Eval("FORMID") %>' runat="server" Style="display: none"></asp:TextBox>
                                <asp:Label ID="read_ROWID" Text='<%# Container.ItemIndex+1%>' runat="server"></asp:Label>
                            </th>
                            <td style="text-align:center">
                                <asp:Label ID="read_ScheduleFrom" Text='<%#Eval("ScheduleFrom") %>' runat="server"></asp:Label>
                            </td>
                            <td style="text-align:center">
                                <asp:Label ID="read_ScheduleTo" Text='<%#Eval("ScheduleTo") %>' runat="server"></asp:Label>
                            </td>
                            <td style="text-align:center">
                                <asp:Label ID="read_DateFrom" Text='<%# String.IsNullOrEmpty(Eval("DateFrom").ToString())? "":DateTime.Parse(Eval("DateFrom").ToString()).ToString("yyyy-MM-dd") %>' runat="server"></asp:Label>
                            </td>
                            <td style="text-align:center">
                                <asp:Label ID="read_DateTo" Text='<%# String.IsNullOrEmpty(Eval("DateTo").ToString())? "":DateTime.Parse(Eval("DateTo").ToString()).ToString("yyyy-MM-dd") %>' runat="server"></asp:Label>
                            </td>
                            <td style="text-align:center">
                                <asp:Label ID="read_Days" Text='<%#Eval("Days") %>' runat="server"></asp:Label>
                            </td>
                            <td style="text-align:center">
                                <asp:Label ID="read_Night" Text='<%#Eval("Night") %>' runat="server"></asp:Label>
                            </td>
                            <td style="text-align:center">
                                <asp:Label ID="read_Remark" Text='<%#Eval("Remark") %>' runat="server"></asp:Label>
                            </td>
                        </tr>
                    </ItemTemplate>
                </asp:Repeater>
            </tbody>
            </table>
            </div>
            <div class="row">
                <p style="font-weight:bold">Purpose of trip</p>
                <table class="table table-condensed table-bordered">
                    <asp:Repeater runat="server" ID="fld_detail_PROC_TravelSecond_DT">
                        <ItemTemplate>
                            <tr>
                                <th style="text-align:center;width:3%"> 
                                    <asp:TextBox ID="read_FORMID" Text='<%#Eval("FORMID") %>' runat="server" Style="display: none"></asp:TextBox>
                                    <asp:Label ID="read_ROWID" Text='<%# Container.ItemIndex+1%>' runat="server"></asp:Label>
                                </th>
                                <td>
                                    <asp:Label runat="server" ID="read_PurposeOfTrip" Text='<%#Eval("PurposeOfTrip") %>'></asp:Label>
                                </td>
                            </tr>
                        </ItemTemplate>
                    </asp:Repeater>
                </table>
            </div>
            <div class="row">
                <p style="font-weight:bold;">Traval Application</p>
                <table class="table table-condensed table-bordered">
                    <tr>
                        <th width="15%"></th>
                        <th width="45%"><p style="text-align:center">Remark</p></th>
                        <th><p style="text-align:center">Day</p></th>
                        <th><p style="text-align:center">RMB</p></th>
                        <th><p style="text-align:center">USD</p></th>
                    </tr>
                     <tr>
                        <th><p style="text-align:center">Airline/Train</p></th>
                        <td style="text-align:center">
                            <asp:Label runat="server" ID="read_AirlineFCRG"></asp:Label>
                        </td>
                        <td style="text-align:center">
                            <asp:Label runat="server" ID="read_AirlineDay"></asp:Label>
                        </td>
                        <td style="text-align:center">
                            <asp:Label runat="server" CssClass="money" ID="read_AirlineRMB"></asp:Label>
                        </td>
                        <td style="text-align:center">
                            <asp:Label runat="server" CssClass="money" ID="read_AirlineUSD"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <th><p style="text-align:center">Hotel</p></th>
                        <td style="text-align:center">
                            <asp:Label runat="server" ID="read_HotelFCRG"></asp:Label>
                        </td>
                        <td style="text-align:center">
                            <asp:Label runat="server" ID="read_HotelDay"></asp:Label>
                        </td>
                        <td style="text-align:center">
                            <asp:Label runat="server" CssClass="money" ID="read_HotelRMB"></asp:Label>
                        </td>
                        <td style="text-align:center">
                            <asp:Label runat="server" CssClass="money" ID="read_HotelUSD"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <th><p style="text-align:center">Ground Transportation</p></th>
                        <td style="text-align:center">
                            <asp:Label runat="server" ID="read_GroundTransFCRG"></asp:Label>
                        </td>
                        <td style="text-align:center">
                            <asp:Label runat="server" ID="read_GroundTransDay"></asp:Label>
                        </td>
                        <td style="text-align:center">
                            <asp:Label runat="server"  CssClass="money" ID="read_GroundTransRMB"></asp:Label>
                        </td>
                        <td style="text-align:center">
                            <asp:Label runat="server" CssClass="money" ID="read_GroundTransUSD"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <th><p style="text-align:center">Meal</p></th>
                        <td style="text-align:center">
                            <asp:Label runat="server" ID="read_MealFCRG"></asp:Label>
                        </td>
                        <td style="text-align:center">
                            <asp:Label runat="server" ID="read_MealDay"></asp:Label>
                        </td>
                        <td style="text-align:center">
                            <asp:Label runat="server" CssClass="money" ID="read_MealRMB"></asp:Label>
                        </td>
                        <td style="text-align:center">
                            <asp:Label runat="server" CssClass="money" ID="read_MealUSD"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <th><p style="text-align:center">Misc Costs</p></th>
                        <td style="text-align:center">
                            <asp:Label runat="server" ID="read_CostsFCRG"></asp:Label>
                        </td>
                        <td style="text-align:center">
                            <asp:Label runat="server" ID="read_CostsDay"></asp:Label>
                        </td>
                        <td style="text-align:center">
                            <asp:Label runat="server" CssClass="money"  ID="read_CostsRMB"></asp:Label>
                        </td>
                        <td style="text-align:center">
                            <asp:Label runat="server" CssClass="money" ID="read_CostsUSD"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <th colspan="3"><p style="text-align:right">Estimated Total</p></th>
                        <td style="text-align:center">
                            <asp:Label runat="server" CssClass="money" ID="read_TotalRMB"></asp:Label>
                        </td>
                        <td style="text-align:center">
                            <asp:Label runat="server" CssClass="money" ID="read_TotalUSD"></asp:Label>
                        </td>
                    </tr>
                    <tr style="display:none;">
                        <th colspan="3"><p style="text-align:right">Ex Rate</p></th>
                        <td colspan="2" style="text-align:center">
                            <asp:Label runat="server" ID="read_Rate"></asp:Label>
                        </td>
                    </tr>
                </table>
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
