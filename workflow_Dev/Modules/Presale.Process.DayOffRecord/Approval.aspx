<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Approval.aspx.cs" Inherits="Presale.Process.DayOffRecord.Approval" %>

<%@ Register Src="/Modules/Ultimus.UWF.Form.ProcessControl/UserInfo.ascx" TagName="UserInfo" TagPrefix="ui" %>
<%@ Register Src="/Modules/Ultimus.UWF.Form.ProcessControl/ApprovalHistory.ascx" TagName="ApprovalHistory" TagPrefix="ah" %>
<%@ Register Src="/Modules/Ultimus.UWF.Form.ProcessControl/Attachments.ascx" TagName="Attachments" TagPrefix="attach" %>
<%@ Register Src="/Modules/Ultimus.UWF.Form.ProcessControl/ButtonList.ascx" TagName="ButtonList" TagPrefix="btn" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
        <meta http-equiv="X-UA-Compatible"  content="IE=EmulateIE8" />
    <title>Day-Off Record Application Process</title>
    <script type="text/javascript" src="/Assets/js/common.js"></script>
    <script language="javascript" type="text/javascript">
        function showTime(obj) {
            var time = new Date(obj.replace(/-/g,"/"));
            var year = time.getFullYear();
            var month = time.getMonth() + 1;
            var date = time.getDate();
            return (year + "-" + month + "-" + date) == "NaN-NaN-NaN" ? "" : (year + "-" + month + "-" + date);
        }
        $(document).ready(function () {
            $("#read_StartDate").text(showTime($("#read_StartDate").text()));
            $("#read_EndDate").text(showTime($("#read_EndDate").text()));
            Calculateclass();
        });


        function Calculateclass() {
            var DetailCount = $("#leavedetails").find("tr").length;
            if (DetailCount > 0) {
                $(".OldDate").hide();
            }
            else {
                $("#ListTableDetail").hide();
            }
            $("#SPSH").text($("#read_SumHour").text());
        }



    </script>
</head>
<body>
    <form id="form1" runat="server">
     <div id="myDiv" class="container">
            <div class="row">
                <ui:userinfo id="UserInfo1" processtitle="Day-Off Record Application Process" processprefix="DOR" tablename="PROC_DayOffRecord"  tablenamedetail="PROC_DayOffRecord_DT"
                    runat="server"  ></ui:userinfo>
                <asp:TextBox runat="server" ID="fld_UserName" style="display:none;"></asp:TextBox>
                <asp:Label runat="server" ID="read_ApplierLogin" style="display:none;" ></asp:Label>
                <asp:Label runat="server" ID="read_DayOffYear" style="display:none;"></asp:Label>
                <asp:Label runat="server" ID="read_DayOffMonth" style="display:none;" ></asp:Label>
            </div>
            <div class="row">
                <p style="font-weight:bold;">Request require</p>
                <table class="table table-condensed table-bordered">
                    <tr>
                        <td class="td-label"  style="width:100px">
                         <span style="height:30px; float:left;">&nbsp;</span>
                        <p style="text-align:center">Day-Off Reason</p>
                        </td>
                        <td class="td-content" colspan="7" >
                        <asp:Label runat="server" ID="read_Reason" ></asp:Label>
                        </td>
                    </tr>
                    <tr class="OldDate">
                        <td class="td-label"> 
                       <span style="margin-top:5px;  height:30px; float:left;">&nbsp;</span>
                        <p style="text-align:center">From</p>
                        </td>
                        <td class="td-content"  colspan="3" >
                            <asp:Label runat="server"  ID="read_StartDate"></asp:Label>
                            <asp:Label runat="server" ID="read_StartHours"></asp:Label>:
                            <asp:Label runat="server" ID="read_StartMinutes"></asp:Label>
                        </td>
                         <td class="td-label"> 
                       <span style="margin-top:5px;  height:30px; float:left;">&nbsp;</span>
                        <p style="text-align:center">To</p>
                        </td>
                        <td class="td-content"  colspan="3" >
                            <asp:Label runat="server" ID="read_EndDate" ></asp:Label>
                            <asp:Label runat="server" ID="read_EndHours"></asp:Label>:
                            <asp:Label runat="server" ID="read_EndMinutes"></asp:Label>
                        </td>
                    </tr>
                    <tr class="OldDate">
                        <td class="td-label"  style="width:100px">
                         <span style=" height:30px; float:left;">&nbsp;</span>
                        <p style="text-align:center">Sum</p>
                        </td>
                        <td class="td-content" colspan="7" >
                        <asp:Label runat="server" ID="read_SumHour"></asp:Label>hours
                        </td>
                    </tr>
                </table>
                 
            </div>


            <div class="row" id="ListTableDetail" style="display:block">

                    <table class="table table-condensed table-bordered">
                    <tr>
                        <th width="33%"> 
                        <p style="text-align:center">
                        From</p>
                        </th>
                        <th width="33%">
                        <p style="text-align:center">To</p>
                        </th>
                        <th width="33%">
                        <p style="text-align:center">Sub sumhours</p>
                        </th>
                        </tr>

                          <tbody id="leavedetails">
                    <asp:Repeater runat="server" ID="read_detail_PROC_DayOffRecord_DT"  >
                        <ItemTemplate>
                           <tr>
                                <td style="display:none">
                                    <asp:Label ID="read_FORMID" Text='<%#Eval("FORMID") %>' runat="server" Style="display: none"></asp:Label>
                                    <asp:Label ID="read_ROWID" Text='<%# Container.ItemIndex+1%>' runat="server"></asp:Label>
                                </td>
                                 <td style="text-align:center">
                            <asp:Label runat="server" ID="read_StartDate"  Text='<%#String.IsNullOrEmpty(Eval("StartDate").ToString())? "":DateTime.Parse(Eval("StartDate").ToString()).ToString("yyyy-MM-dd") %>'  ></asp:Label>
                            <asp:Label runat="server" ID="read_StartHours"   Text='<%# Eval("StartHours").ToString().Length==1?"0"+ Eval("StartHours"): Eval("StartHours") %>'   ></asp:Label>
                           :<asp:Label runat="server" ID="read_StartMinutes"   Text='<%#Eval("StartMinutes").ToString().Length==1?"0"+ Eval("StartMinutes"): Eval("StartMinutes") %>' ></asp:Label>
                                   </td>
                                   <td style="text-align:center">
                                   <asp:Label runat="server"  ID="read_EndDate"   Text='<%#String.IsNullOrEmpty(Eval("EndDate").ToString())? "":DateTime.Parse(Eval("EndDate").ToString()).ToString("yyyy-MM-dd") %>' ></asp:Label>
                                   <asp:Label runat="server" ID="read_EndHours"   Text='<%#Eval("EndHours").ToString().Length==1?"0"+ Eval("EndHours"): Eval("EndHours") %>'   ></asp:Label>
                                  :<asp:Label runat="server" ID="read_EndMinutes"  Text='<%#Eval("EndMinutes").ToString().Length==1?"0"+ Eval("EndMinutes"): Eval("EndMinutes") %>' ></asp:Label>
                                   </td>
                                   <td style="text-align:center">
                                   <asp:Label runat="server" ID="read_SumHour"   Text='<%#Eval("SumHour") %>'   ></asp:Label>
                                   </td>
                                </tr>
                                </ItemTemplate>
                                </asp:Repeater>
                                </tbody>

                    <tr>
                    <td colspan="2">
                    </td>
                    <td>
                    Sum Hours: <span id="SPSH" style="margin-left:30%;"></span>
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
        <btn:buttonlist id="ButtonList1" runat="server"></btn:buttonlist>
        <div style="display:none">
        <asp:TextBox runat="server" ID="fld_TRSummary"></asp:TextBox>
        <asp:HiddenField runat="server" ID="hdIncident" />
        <asp:HiddenField runat="server" ID="hdPrint" />
        </div>
    </form>
</body>
</html>


