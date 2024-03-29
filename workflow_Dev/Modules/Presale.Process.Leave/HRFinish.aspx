﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="HRFinish.aspx.cs" Inherits="Presale.Process.Leave.HRFinish" %>
<%@ Register Src="/Modules/Ultimus.UWF.Form.ProcessControl/UserInfo.ascx" TagName="UserInfo" TagPrefix="ui" %>
<%@ Register Src="/Modules/Ultimus.UWF.Form.ProcessControl/ApprovalHistory.ascx" TagName="ApprovalHistory" TagPrefix="ah" %>
<%@ Register Src="/Modules/Ultimus.UWF.Form.ProcessControl/Attachments.ascx" TagName="Attachments" TagPrefix="attach" %>
<%@ Register Src="/Modules/Ultimus.UWF.Form.ProcessControl/ButtonList.ascx" TagName="ButtonList" TagPrefix="btn" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta http-equiv="X-UA-Compatible"  content="IE=EmulateIE8" />
    <title>Leave Application Process</title>
    <script type="text/javascript" src="/Assets/js/common.js"></script>
    <script type="text/javascript">
        function submitPageReview(obj) {
            $("#ButtonList1_btnSubmit").click();
        }
        $(document).ready(function () {
            $("#leavedetails").find("tr").each(function (i, Etr) {

                if ($(Etr).find("td").eq(1).children().text() == "Annual Leave") {
                    $(Etr).find(".txthours").show();
                    var nodasy = $(Etr).find(".nodays").text();
                    $(Etr).find(".txthours:eq(0)").text((nodasy * 8).toFixed(2));
                }
                else {
                    $(Etr).find(".txthours").hide();
                }
            });
        });
    </script>
</head>
<body>
    <form id="form1" runat="server">
     <div id="myDiv" class="container">
            <div class="row">
                <ui:userinfo id="UserInfo1" processtitle="Leave Application Process" processprefix="HRLE" tablename="PROC_Leave" tablenamedetail="PROC_Leave_DT"
                    runat="server"  ></ui:userinfo>
                <asp:TextBox runat="server" ID="fld_departmentManager" style="display:none;"></asp:TextBox>
                <asp:Label runat="server" ID="read_nowAnnualLeave" style="display:none;"></asp:Label>
                <asp:Label runat="server" ID="read_applierLogin" style="display:none;"></asp:Label>
            </div>
            <div class="row">
                <p style="font-weight:bold;">Request require</p>
                 <p style="color:red;">
                “The minimum unit of annual leave consumed is 1 hour, 1 day annual leave=8 hours 年休假的最小计算单位为1小时。（一  天年假换算为8小时）”
                </p>
                <table class="table table-condensed table-bordered">
                    <tr>
                        <th width="25%"> 
                        <p style="text-align:center">Applying for</p>
                        </th>
                        <th width="25%">
                        <p style="text-align:center">From</p>
                        </th>
                        <th width="25%">
                        <p style="text-align:center">To</p>
                        </th>
                        <th width="25%">
                        <p style="text-align:center">Number of absence</p>
                        </th>
                    </tr>
                    <tbody id="leavedetails">
                    <asp:Repeater runat="server" ID="fld_detail_PROC_Leave_DT">
                        <ItemTemplate>
                            <tr>
                                 <td style="display:none">
                                    <span style=" background:red;">&nbsp;</span>
                                    <asp:Label ID="read_FORMID" Text='<%#Eval("FORMID") %>' runat="server" Style="display: none"></asp:Label>
                                    <asp:Label ID="read_ROWID" Text='<%# Container.ItemIndex+1%>' runat="server"></asp:Label>
                                    <asp:Label runat="server" ID="read_Usethisyear" Text='<%# String.IsNullOrEmpty( Eval("Usethisyear").ToString())? "0" : Eval("Usethisyear").ToString()%>' style="display:none" ></asp:Label>
                                    <asp:Label runat="server" ID="read_Uselastyear" Text='<%# String.IsNullOrEmpty( Eval("Uselastyear").ToString())? "0" : Eval("Uselastyear").ToString() %>' style="display:none" ></asp:Label>
                                </td>
                                <td style="text-align:center">
                                    <asp:Label runat="server" ID="read_Applying" Text='<%#Eval("Applying") %>'></asp:Label>
                                </td>
                                <td style="text-align:center">
                                    <asp:Label runat="server" ID="read_StartDate" Text='<%# String.IsNullOrEmpty(Eval("StartDate").ToString())? "":DateTime.Parse(Eval("StartDate").ToString()).ToString("yyyy-MM-dd") %>'></asp:Label>
                                    <asp:Label runat="server" ID="read_StartHours" Text='<%# String.IsNullOrEmpty(Eval("StartHours").ToString())? "":Eval("StartHours")%>'></asp:Label>:
                                    <asp:Label runat="server" ID="read_StartMinutes" Text='<%# String.IsNullOrEmpty(Eval("StartMinutes").ToString())? "":Eval("StartMinutes")%>'></asp:Label>
                                </td>
                                <td style="text-align:center">
                                    <asp:Label runat="server" ID="read_EndDate" Text='<%# String.IsNullOrEmpty(Eval("EndDate").ToString())? "":DateTime.Parse(Eval("EndDate").ToString()).ToString("yyyy-MM-dd") %>'></asp:Label>
                                    <asp:Label runat="server" ID="read_EndHours" Text='<%# String.IsNullOrEmpty(Eval("EndHours").ToString())? "":Eval("EndHours")%>'></asp:Label>:
                                    <asp:Label runat="server" ID="read_EndMinutes" Text='<%# String.IsNullOrEmpty(Eval("EndMinutes").ToString())? "":Eval("EndMinutes")%>'></asp:Label>
                                </td>
                                <td style="text-align:center">
                                      <span   class="txthours" style="display:none;width:50%;"> </span>
                                    <span class="txthours" style="display:none">Hours</span><br />
                                    <asp:Label runat="server" ID="read_NoODays" style="width:50%;"  CssClass="nodays" Text='<%#Eval("NoODays") %>'></asp:Label>
                                    <span class="txthours" style="display:none">Days</span>
                                </td>
                            </tr>
                        </ItemTemplate>
                    </asp:Repeater>
                    </tbody>
                </table>
                <table class="table table-condensed table-bordered">
                    <tr>
                        <td class="td-label" style="vertical-align:middle">
                            <p style="text-align:center">Remark</p>
                        </td>
                        <td class="td-content" colspan="4">
                            <asp:Label runat="server" ID="read_remark"></asp:Label>
                        </td>
                    </tr>
                </table>
            </div>
            <div class="row" style="display:none;">
                <attach:attachments id="Attachments1" runat="server"></attach:attachments>
            </div>
            <div class="row">
             <ah:approvalhistory id="ApprovalHistory1" showaction="true" runat="server"></ah:approvalhistory>
            </div>
        </div>
        <div id="btnDiv" runat="server"   >
            <table style="width: 100%;" >
                <tr>
                    <td align="center"  >
                        <table>
                            <tr>
                                <td> 
                                <input type="button"  class="btn" value="Complete" onclick="submitPageReview('0')" />
                                </td>
                            </tr>
                       </table>
                    </td>
                 </tr>
            </table>
        </div>
        <div style="display:none;">
        <btn:buttonlist id="ButtonList1" runat="server"></btn:buttonlist>
        </div>
        <div style="display:none">
        <asp:TextBox runat="server" ID="fld_TRSummary"></asp:TextBox>
        <asp:HiddenField runat="server" ID="hdIncident" />
        <asp:HiddenField runat="server" ID="hdPrint" />
        </div>
    </form>
</body>
</html>


