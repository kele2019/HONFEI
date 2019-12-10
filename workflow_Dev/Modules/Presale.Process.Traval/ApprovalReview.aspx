<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ApprovalReview.aspx.cs" Inherits="Presale.Process.Traval.ApprovalReview" %>


<%@ Register Src="/Modules/Ultimus.UWF.Form.ProcessControl/UserInfo.ascx" TagName="UserInfo" TagPrefix="ui" %>
<%@ Register Src="/Modules/Ultimus.UWF.Form.ProcessControl/ApprovalHistory.ascx" TagName="ApprovalHistory" TagPrefix="ah" %>
<%@ Register Src="/Modules/Ultimus.UWF.Form.ProcessControl/Attachments.ascx" TagName="Attachments" TagPrefix="attach" %>
<%@ Register Src="/Modules/Ultimus.UWF.Form.ProcessControl/ButtonList.ascx" TagName="ButtonList" TagPrefix="btn" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>出差申请流程/TravelRequestProcess</title>
        <script type="text/javascript" src="/Assets/js/common.js"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            if ($("#read_YWXY").attr("checked")) {
                $("#clmd").append("业务需要 Business imperative");
            }
            if ($("#read_PX").attr("checked")) {
                $("#clmd").append("培训 Training ");
            }
            if ($("#read_QT").attr("checked")) {
                $("#clmd").append(" 其他 Other");
            }
            if ($("#read_JPYDYes").attr("checked")) {
                $("#tdydjp").append("是，公司预订<br />Yes,company reserves");
                $("#YDTR").show();
            }
            if ($("#read_JPYDNo").attr("checked")) {
                $("#tdydjp").append("否，理由<br />No,reasons");
            }
            if ($("#read_YDJDYes").attr("checked")) {
                $("#tdydjd").append("是，公司预订<br />Yes,company reserves");
                $("#JDTR").show();
            }
            if ($("#read_YDJDNo").attr("checked")) {
                $("#tdydjd").append("否，理由<br />No,reasons");
            }
            if ($("#read_TravalType").text() == "1") {
                $("#read_TravalType").text("国内 Domestic");
            }
            else {
                $("#read_TravalType").text("国外 Abroad");
            }

        });
        function submitPageReview(obj) {
            $("#hdAllNo").val("");
            if (obj == "1") {
                $("#hdAllNo").val(obj);
            }
            //$("#ButtonList1_btnSubmit").click();
            $("#ApprovalHistory1_txtSpecialAction").val("");
            if (obj != "1") {
                $("#ButtonList1_btnSubmit").val("Confirm Send to Admin？");
            }
            else {
                $("#ButtonList1_btnSubmit").val("Confirm Book by Ownself？");
            }
            $("#ButtonList1_btnSubmit").click();
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <div class="container">
            <div class="row">
                <ui:userinfo id="UserInfo1"  processtitle="出差申请流程(Travel Request Form)" processprefix="TR" tablename="PROC_TRAVEL"
                    runat="server" tablenamedetail="PROC_Travel_DT"></ui:userinfo>
                 <%--tablenamedetail="PROC_TravelExpense_DT"--%>
            </div>
            <div class="row">
                <table class="table table-condensed table-bordered">
                    <tr>
                        <td class="banner" colspan="6">Travel Application详细信息
                        </td>
                    </tr>
                    <tr>
                        <td class="td-label" style="width:100px">
                          差旅目的
                            <br /> Purpose of the trip 
                        </td>
                        <td class="td-content" id="clmd" colspan="3">
                        
                        
                        </td>
                    </tr>
                    <tr>
                        <td class="td-label">Destination Area<br />
                            请简明描述您的差旅目的 <br />
                            Please describe your intention of this business trip:
                        </td>
                        <td class="td-content" colspan="3">
                            <asp:Label runat="server" ID="read_TravalComments"   ></asp:Label>
                        </td>
                    </tr>
                </table>
            </div>
            <div >
                <div class="row" style="width: 120%">

                    <table class="table table-condensed table-bordered tablerequired" style="width:83%" id="tbDetail">
                        <tr>
                            <td class="banner" colspan="8">列表详情
                            </td>
                        </tr>
                        <tr>
                            <th></th>
                            <th colspan="6">行程安排（将作为机票/火车票定购信息）<br />
Schedule(regarded as the information of tickets ordering)
                            </th>
                            <th align="center">请写明每次行程<br />
Please specify each trip</th>
                        </tr>
                        <tr>
                            <th rowspan="2">No.<br />
                                序号
                            </th>
                            
                            <th colspan="2">目的地<br />
                                Destination
                                 <span class="red">*</span>
                            </th>

                            <th colspan="2">出发 <br />
                                Departure
                                 <span class="red">*</span>
                            </th>
                            <th colspan="2">到达 <br />
                                Arrival
                                <span class="red">*</span>
                            </th>
                            <th rowspan="2" align="center" width="50px">将要拜访的客户或公司  <br />
Customers or companies that you will visit
                               <span class="red">*</span>
                            </th>
                        </tr>
                        <tr>
                            <th>从<br />
From</th>
                            <th>
                                至<br />
To
                            </th>
                            <th>日期<br />
Date</th>
                            <th>时间<br />
Time</th>
                            <th>日期<br />
Date</th>
                            <th>时间<br />
Time</th>
                        </tr>
                        <asp:Repeater ID="fld_detail_PROC_Travel_DT" runat="server"  >
                            <ItemTemplate>
                                <tr>
                                    <td style="text-align: center;">
                                        <asp:TextBox ID="read_FORMID" Text='<%#Eval("FORMID") %>' runat="server" Style="display: none"></asp:TextBox>
                                        <asp:Label ID="read_ROWID" Text='<%# Container.ItemIndex+1%>' runat="server"></asp:Label>
                                    </td>
                                    <td style="text-align: center;">
                                        <asp:Label ID="read_SAdress" CssClass="CategoryText" Text='<%#Eval("SAdress") %>' runat="server"></asp:Label>
                                    </td>
                                    <td style="text-align: center;">
                                        <asp:Label ID="read_EAdress" runat="server"  Text='<%#Eval("EAdress").ToString() %>'></asp:Label>
                                    </td>
                                    <td style="text-align: center;">
                                        <asp:Label ID="read_SDate" Text='<%# String.IsNullOrEmpty(Eval("SDate").ToString())? "":DateTime.Parse(Eval("SDate").ToString()).ToString("yyyy-MM-dd") %>' runat="server"
                                                ></asp:Label>
                                    </td>
                                    <td style="text-align: center;">
                                       <asp:Label ID="read_ETime" Text='<%#Eval("ETime")%>' runat="server"
                                                ></asp:Label>
                                    </td>
                                    <td style="text-align: center;">
                                       <asp:Label ID="read_ASDate" Text='<%# String.IsNullOrEmpty(Eval("ASDate").ToString())? "":DateTime.Parse(Eval("ASDate").ToString()).ToString("yyyy-MM-dd") %>' runat="server"
                                                ></asp:Label>
                                    </td>
                                    <td style="text-align: center;">
                                        <asp:Label ID="read_AETime" Text='<%#Eval("AETime")%>' runat="server"
                                             ></asp:Label>
                                    </td>
                                      <td style="text-align: center;">
                                        <asp:Label ID="read_Comments" Text='<%# String.IsNullOrEmpty(Eval("Comments").ToString())? "":Eval("Comments").ToString() %>' runat="server"
                                            Width="180px"    ></asp:Label>
                                    </td>
                                </tr>
                            </ItemTemplate>
                        </asp:Repeater>
                    </table>

                </div>

            </div>
                  <div class="row">
                <table class="table table-condensed table-bordered">
                    <tr>
                        <td class="banner" colspan="6">预定 Reservation
                        </td>
                    </tr>
                     <tr>
                        <td class="td-label">出差类别：<br />Traval Type</td>
                        <td> 
                            <asp:Label runat="server" ID="read_TravalType"></asp:Label>
                            </td>
                        <td class="td-label"></td>
                        <td><asp:Label runat="server" ID="read_TravalCount" Visible="false"></asp:Label></td>
                    </tr>
                     <tr>
                         <td class="td-label">预订机票/火车票？<br />Ticket Reservation?</td>
                         <td  width="30%" id="tdydjp"></td>
                         <td colspan="3"><asp:Label runat="server" ID="read_YDNo"></asp:Label></td>
                     </tr>
                      <tr id="YDTR" style="display:none">
                     <td class="td-label">备注<br />Comments</td>
                     <td colspan="3"><asp:Label  runat="server" ID="read_YDComments" width="70%"></asp:Label></td>
                     </tr>
                      <tr>
                         <td class="td-label" >预订酒店？<br />Hotel Reservation?</td>
                         <td  width="30%" id="tdydjd"></td>
                         <td colspan="3"><asp:Label runat="server" ID="read_JDNo"></asp:Label></td>
                     </tr>
                     <tr id="JDTR" style="display:none">
                      <td class="td-label">备注<br />Comments</td>
                     <td colspan="3"><asp:Label  runat="server" ID="read_YDJDComments"  width="70%"></asp:Label>
                       <br />
                     城市 City：
                <asp:Label runat="server"   ID="read_HotelCity"    />&nbsp;&nbsp;&nbsp;&nbsp;
                 酒店 Hotel：
                <asp:Label runat="server"   ID="read_CityHotel" />
                     </td>
                     </tr>
                   
                    </table>
            </div>
            <div class="row">
                <attach:attachments id="Attachments1" Readonly="true"  runat="server"></attach:attachments>
            </div>


            <div class="row">
                <ah:approvalhistory id="ApprovalHistory1" showaction="false" runat="server"></ah:approvalhistory>
            </div>
        </div>
        <div id="btnDiv" runat="server" visible="false" >
        <table style="width: 100%;">
    <tr  width="500">
        <td align="center"  >
            <table>
                <tr>
                    <td> 
                    <input type="button"  class="btn" value="Send to Admin" onclick="submitPageReview('0')" />
               &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                     <input type="button"  class="btn" value="Book by Ownself" onclick="submitPageReview('1')" />
                    </td>
                    </tr>
                    </table>
                    </td>
                    </tr>
                    </table>
        </div>
        <div style="display:none">
        <btn:buttonlist id="ButtonList1" runat="server"></btn:buttonlist>
        </div>
        <div style="display: none;">
        <asp:CheckBox runat="server" ID="read_YWXY" /> <label>&nbsp;&nbsp;&nbsp;业务需要<br />&nbsp;&nbsp;&nbsp;&nbsp;Business imperative</label> 
                       &nbsp; &nbsp;&nbsp;&nbsp; <asp:CheckBox runat="server" ID="read_PX" />  <label>&nbsp;&nbsp;&nbsp;培训<br />&nbsp;&nbsp;&nbsp;Training </label> 
                      &nbsp; &nbsp;&nbsp;&nbsp;   <asp:CheckBox runat="server" ID="read_QT" />  <label>&nbsp;&nbsp;&nbsp;其他<br />&nbsp;&nbsp;&nbsp;others</label> 
                      <asp:CheckBox runat="server" ID="read_JPYDYes" /> 是，公司预订机票
                      <asp:CheckBox runat="server" ID="read_JPYDNo" />否，理由机票
                      <asp:CheckBox runat="server" ID="read_YDJDYes" />是，公司预订酒店
                      <asp:CheckBox runat="server" ID="read_YDJDNo" />否，理由酒店
                      <asp:HiddenField runat="server" ID="hdAllNo" />
        </div>
    </form>
</body></html>