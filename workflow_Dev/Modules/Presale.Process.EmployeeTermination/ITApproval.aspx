<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ITApproval.aspx.cs" Inherits="Presale.Process.EmployeeTermination.ITApproval" %>
<%@ Register Src="/Modules/Ultimus.UWF.Form.ProcessControl/UserInfo.ascx" TagName="UserInfo" TagPrefix="ui" %>
<%@ Register Src="/Modules/Ultimus.UWF.Form.ProcessControl/ApprovalHistory.ascx" TagName="ApprovalHistory" TagPrefix="ah" %>
<%@ Register Src="/Modules/Ultimus.UWF.Form.ProcessControl/Attachments.ascx" TagName="Attachments" TagPrefix="attach" %>
<%@ Register Src="/Modules/Ultimus.UWF.Form.ProcessControl/ButtonList.ascx" TagName="ButtonList" TagPrefix="btn" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta http-equiv="X-UA-Compatible"  content="IE=EmulateIE8" />
    <title>Employee Termination-Check Out List</title>
    <script type="text/javascript" src="/Assets/js/common.js"></script>
    <script type="text/javascript">
        function submitPageReview(obj) {
            $("#ButtonList1_btnSubmit").click();
        }
        function getButtonCheck(obj, index) {
            if (index == "1") {
                if ($(obj).attr("checked")) {
                    $("#fld_ITCSTACC").val("CollectedCancelled");
                    $("#ITCSTANA").attr("checked",false);
                }
                else {
                    $("#fld_ITCSTACC").val("");
                }
            }
            if (index == "2") {
                if ($(obj).attr("checked")) {
                    $("#fld_ITCSTANA").val("NA");
                    $("#ITCSTACC").attr("checked", false);
                }
                else {
                    $("#fld_ITCSTANA").val("");
                }
            }
            if (index == "3") {
                if ($(obj).attr("checked")) {
                    $("#fld_ITDesktopCC").val("CollectedCancelled");
                    $("#ITDesktopNA").attr("checked", false);
                }
                else {
                    $("#fld_ITDesktopCC").val("");
                }
            }
            if (index == "4") {
                if ($(obj).attr("checked")) {
                    $("#fld_ITDesktopNA").val("NA");
                    $("#ITDesktopCC").attr("checked", false);
                }
                else {
                    $("#fld_ITDesktopNA").val("");
                }
            }
            if (index == "5") {
                if ($(obj).attr("checked")) {
                    $("#fld_ITNSCC").val("CollectedCancelled");
                    $("#ITNSNA").attr("checked", false);
                }
                else {
                    $("#fld_ITNSCC").val("");
                }
            }
            if (index == "6") {
                if ($(obj).attr("checked")) {
                    $("#fld_ITNSNA").val("NA");
                    $("#ITNSCC").attr("checked", false);
                }
                else {
                    $("#fld_ITNSNA").val("");
                }
            }
            if (index == "7") {
                if ($(obj).attr("checked")) {
                    $("#fld_ITSLCC").val("CollectedCancelled");
                    $("#ITSLNA").attr("checked", false);
                }
                else {
                    $("#fld_ITSLCC").val("");
                }
            }
            if (index == "8") {
                if ($(obj).attr("checked")) {
                    $("#fld_ITSLNA").val("NA");
                    $("#ITSLCC").attr("checked", false);
                }
                else {
                    $("#fld_ITSLNA").val("");
                }
            }
            if (index == "9") {
                if ($(obj).attr("checked")) {
                    $("#fld_ITEATCC").val("CollectedCancelled");
                    $("#ITEATNA").attr("checked", false);
                }
                else {
                    $("#fld_ITEATCC").val("");
                }
            }
            if (index == "10") {
                if ($(obj).attr("checked")) {
                    $("#fld_ITEATNA").val("NA");
                    $("#ITEATCC").attr("checked", false);
                }
                else {
                    $("#fld_ITEATNA").val("");    
                }
            }
            if (index == "11") {
                if ($(obj).attr("checked")) {
                    $("#fld_ITOtherCC").val("CollectedCancelled");
                    $("#ITOtherNA").attr("checked", false);
                }
                else {
                    $("#fld_ITOtherCC").val("");
                }
            }
            if (index == "12") {
                if ($(obj).attr("checked")) {
                    $("#fld_ITOtherNA").val("NA");
                    $("#ITOtherCC").attr("checked", false);
                }
                else {
                    $("#fld_ITOtherNA").val("");
                }
            }
        }
        $(document).ready(function () {
            if ($("#fld_ITCSTACC").val() == "CollectedCancelled") { $("#ITCSTACC").attr("checked", true); }
            if ($("#fld_ITCSTANA").val() == "NA") { $("#ITCSTANA").attr("checked", true); }
            if ($("#fld_ITDesktopCC").val() == "CollectedCancelled") { $("#ITDesktopCC").attr("checked", true); }
            if ($("#fld_ITDesktopNA").val() == "NA") { $("#ITDesktopNA").attr("checked", true); }
            if ($("#fld_ITNSCC").val() == "CollectedCancelled") { $("#ITNSCC").attr("checked", true); }
            if ($("#fld_ITNSNA").val() == "NA") { $("#ITNSNA").attr("checked", true); }
            if ($("#fld_ITSLCC").val() == "CollectedCancelled") { $("#ITSLCC").attr("checked", true); }
            if ($("#fld_ITSLNA").val() == "NA") { $("#ITSLNA").attr("checked", true); }
            if ($("#fld_ITEATCC").val() == "CollectedCancelled") { $("#ITEATCC").attr("checked", true); }
            if ($("#fld_ITEATNA").val() == "NA") { $("#ITEATNA").attr("checked", true); }
            if ($("#fld_ITOtherCC").val() == "CollectedCancelled") { $("#ITOtherCC").attr("checked", true); }
            if ($("#fld_ITOtherNA").val() == "NA") { $("#ITOtherNA").attr("checked", true); }
            if (request("type") == "myapproval") {
                $("#btnDiv").hide();
                func1();
            }
        });
        function beforeSubmit() {
            var msg = "";
            if ($("#fld_ITCSTACC").val() == "" && $("#fld_ITDesktopNA").val() == "")
                msg = "Pls select Desktop\n";
            if ($("#fld_ITNSCC").val() == "" && $("#fld_ITNSNA").val() == "")
                msg += "Pls select Network Service\n";

            if ($("#fld_ITSLCC").val() == "" && $("#fld_ITSLNA").val() == "")
                msg += "Pls select Software Licenses\n";

            if ($("#fld_ITEATCC").val() == "" && $("#fld_ITEATNA").val() == "")
                msg += "Pls select Email Account Termination\n";

            if ($("#fld_ITOtherCC").val() == "" && $("#fld_ITOtherNA").val() == "")
                msg += "Pls select Email Others\n";
            if (msg != "") {
                alert(msg);
                return false;
            }
            else
                return true;
        }

        function func1() {
            $("#formdiv").each(function (i) {
                $(this).find('input[type="checkbox"]').attr("disabled", "disabled");
                $(this).find('input[type="text"]').attr("disabled", "disabled");
                $(this).find('textarea').attr("disabled", "disabled");

            });
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
     <div id="myDiv" class="container">
            <div class="row">
                <ui:userinfo id="UserInfo1" processtitle="Employee Termination-Check Out List" processprefix="ETCO" tablename="PROC_EmployeeTerminationCheckOut"
                    runat="server"  ></ui:userinfo>
            </div>
             <div class="row">
                <p style="font-weight:bold;">Termination Employee（"<span style=" background:red;">&nbsp;</span>" must write） </p>
                <table class="table table-condensed table-bordered">
                    <tr>
                        <td class="td-label">
                         <span style=" background:red;  height:30px; float:left;">&nbsp;</span>
                        <p style="text-align:center">Termination Employee</p>
                        </td>
                        <td class="td-content" colspan="7" >
                             
                            <asp:Label runat="server" ID="read_TerminationEmployee" style="display:block;"></asp:Label>
                            <asp:TextBox runat="server" ID="read_TerminationEmployeeValue" style="display:none;"></asp:TextBox>
                        </td>
                    </tr>
                </table>
            </div>

            <div id="formdiv" class="row">
                <p style="font-weight:bold;">IT Responsibilities</p>
                <table class="table table-condensed table-bordered">
                    <tr>
                        <th width="30%">
                         <span style="height:30px; float:left;">&nbsp;</span>
                        <p style="text-align:center">item</p>
                        </th>
                        <th width="15%">
                         <span style="height:30px; float:left;">&nbsp;</span>
                        <p style="text-align:center">Collected/Cancelled</p>
                        </th>
                        <th width="15%">
                         <span style="height:30px; float:left;">&nbsp;</span>
                        <p style="text-align:center" >N/A</p>
                        </th>
                        <%--<td class="td-label">
                         <span style="height:30px; float:left;"  >&nbsp;</span>
                        <p style="text-align:center;">Serial/Card</p>
                        </td>--%>
                        <th width="40%">
                         <span style="height:30px; float:left;"  >&nbsp;</span>
                        <p style="text-align:center;">Comments</p>
                        </th>
                    </tr>
                     <tr style="display:none">
                        <th style="vertical-align:middle">
                         <span style="height:30px; float:left;">&nbsp;</span>
                        <p style="text-align:center">Conferencing Service Termination-2011-AP Voice team->Genesys/AT&T calling card</p>
                        </th>
                        <td style="vertical-align:middle;text-align:center">
                            <span style="height:30px; float:left;">&nbsp;</span>
                            <asp:CheckBox runat="server"  ID="ITCSTACC" onclick="getButtonCheck(this,1)"></asp:CheckBox>
                            <asp:TextBox runat="server" ID="fld_ITCSTACC" style="display:none;"></asp:TextBox>
                        </td>
                        <td style="vertical-align:middle;text-align:center">
                            <span style="height:30px; float:left;">&nbsp;</span>
                            <asp:CheckBox runat="server"  ID="ITCSTANA" onclick="getButtonCheck(this,2)"></asp:CheckBox>
                            <asp:TextBox runat="server" ID="fld_ITCSTANA" style="display:none;"></asp:TextBox>
                        </td>
                        <%--<td class="td-content" style="vertical-align:middle">
                            <span style="margin-top:5px;  height:30px; float:left;">&nbsp;</span>
                            <asp:TextBox runat="server" ID="fld_ITCSTASC" CssClass="validate[required]"></asp:TextBox>
                        </td>--%>
                        <td style="vertical-align:middle">
                            <span style="height:30px; float:left;">&nbsp;</span>
                            <asp:TextBox runat="server" ID="fld_ITCSTARPS" Width="94%"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <th style="vertical-align:middle">
                         <span style="height:30px; float:left;">&nbsp;</span>
                        <p style="text-align:center">Desktop</p>
                        </th>
                        <td style="vertical-align:middle;text-align:center">
                            <span style="height:30px; float:left;">&nbsp;</span>
                            <asp:CheckBox runat="server"  ID="ITDesktopCC" onclick="getButtonCheck(this,3)"></asp:CheckBox>
                            <asp:TextBox runat="server" ID="fld_ITDesktopCC" style="display:none;"></asp:TextBox>
                        </td>
                        <td style="vertical-align:middle;text-align:center">
                            <span style="height:30px; float:left;">&nbsp;</span>
                            <asp:CheckBox runat="server"  ID="ITDesktopNA" onclick="getButtonCheck(this,4)"></asp:CheckBox>
                            <asp:TextBox runat="server" ID="fld_ITDesktopNA" style="display:none;"></asp:TextBox>
                        </td>
                        <%--<td class="td-content" style="vertical-align:middle">
                            <span style="margin-top:5px;  height:30px; float:left;">&nbsp;</span>
                            <asp:TextBox runat="server" ID="fld_ITDesktopSC" CssClass="validate[required]"></asp:TextBox>
                        </td>--%>
                        <td style="vertical-align:middle">
                            <span style="height:30px; float:left;">&nbsp;</span>
                            <asp:TextBox runat="server" ID="fld_ITDesktopRPS" Width="94%"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <th style="vertical-align:middle">
                         <span style="height:30px; float:left;">&nbsp;</span>
                        <p style="text-align:center">Network Service</p>
                        </th>
                        <td style="vertical-align:middle;text-align:center">
                            <span style="height:30px; float:left;">&nbsp;</span>
                            <asp:CheckBox runat="server"  ID="ITNSCC" onclick="getButtonCheck(this,5)"></asp:CheckBox>
                            <asp:TextBox runat="server" ID="fld_ITNSCC" style="display:none;"></asp:TextBox>
                        </td>
                        <td style="vertical-align:middle;text-align:center">
                            <span style="height:30px; float:left;">&nbsp;</span>
                            <asp:CheckBox runat="server"  ID="ITNSNA" onclick="getButtonCheck(this,6)"></asp:CheckBox>
                            <asp:TextBox runat="server" ID="fld_ITNSNA" style="display:none;"></asp:TextBox>
                        </td>
                        <%--<td class="td-content" style="vertical-align:middle">
                            <span style="margin-top:5px;  height:30px; float:left;">&nbsp;</span>
                            <asp:TextBox runat="server" ID="fld_ITNSSC"  CssClass="validate[required]"></asp:TextBox>
                        </td>--%>
                        <td style="vertical-align:middle">
                            <span style="height:30px; float:left;">&nbsp;</span>
                            <asp:TextBox runat="server" ID="fld_ITNSRPS" Width="94%"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <th style="vertical-align:middle">
                         <span style="height:30px; float:left;">&nbsp;</span>
                        <p style="text-align:center">Software Licenses</p>
                        </th>
                        <td class="td-content" style="vertical-align:middle;text-align:center">
                            <span style="height:30px; float:left;">&nbsp;</span>
                            <asp:CheckBox runat="server"  ID="ITSLCC" onclick="getButtonCheck(this,7)"></asp:CheckBox>
                            <asp:TextBox runat="server" ID="fld_ITSLCC" style="display:none;"></asp:TextBox>
                        </td>
                        <td class="td-content" style="vertical-align:middle;text-align:center">
                            <span style="height:30px; float:left;">&nbsp;</span>
                            <asp:CheckBox runat="server"  ID="ITSLNA" onClick="getButtonCheck(this,8)"></asp:CheckBox>
                            <asp:TextBox runat="server" ID="fld_ITSLNA" style="display:none;"></asp:TextBox>
                        </td>
                        <%--<td class="td-content" style="vertical-align:middle">
                            <span style="margin-top:5px;  height:30px; float:left;">&nbsp;</span>
                            <asp:TextBox runat="server" ID="fld_ITSLSC"  CssClass="validate[required]"></asp:TextBox>
                        </td>--%>
                        <td class="td-content" style="vertical-align:middle">
                            <span style="height:30px; float:left;">&nbsp;</span>
                            <asp:TextBox runat="server" ID="fld_ITSLRPS" Width="94%"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <th style="vertical-align:middle">
                         <span style="height:30px; float:left;">&nbsp;</span>
                        <p style="text-align:center">Email Account Termination</p>
                        </th>
                        <td style="vertical-align:middle;text-align:center">
                            <span style="height:30px; float:left;">&nbsp;</span>
                            <asp:CheckBox runat="server"  ID="ITEATCC" onclick="getButtonCheck(this,9)"></asp:CheckBox>
                            <asp:TextBox runat="server" ID="fld_ITEATCC" style="display:none;"></asp:TextBox>
                        </td>
                        <td style="vertical-align:middle;text-align:center">
                            <span style="height:30px; float:left;">&nbsp;</span>
                            <asp:CheckBox runat="server"  ID="ITEATNA" onclick="getButtonCheck(this,10)"></asp:CheckBox>
                            <asp:TextBox runat="server" ID="fld_ITEATNA" style="display:none;"></asp:TextBox>
                        </td>
                        <%--<td class="td-content" style="vertical-align:middle">
                            <span style="margin-top:5px;  height:30px; float:left;">&nbsp;</span>
                            <asp:TextBox runat="server" ID="fld_ITEATSC" CssClass="validate[required]"></asp:TextBox>
                        </td>--%>
                        <td style="vertical-align:middle">
                            <span style="height:30px; float:left;">&nbsp;</span>
                            <asp:TextBox runat="server" ID="fld_ITEATRPS" Width="94%"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <th style="vertical-align:middle">
                         <span style="height:30px; float:left;">&nbsp;</span>
                        <p style="text-align:center">Others</p>
                        </th>
                        <td style="vertical-align:middle;text-align:center">
                            <span style="height:30px; float:left;">&nbsp;</span>
                            <asp:CheckBox runat="server"  ID="ITOtherCC" onclick="getButtonCheck(this,11)"></asp:CheckBox>
                            <asp:TextBox runat="server" ID="fld_ITOtherCC" style="display:none;"></asp:TextBox>
                        </td>
                        <td style="vertical-align:middle;text-align:center">
                            <span style="height:30px; float:left;">&nbsp;</span>
                            <asp:CheckBox runat="server"  ID="ITOtherNA" onclick="getButtonCheck(this,12)"></asp:CheckBox>
                            <asp:TextBox runat="server" ID="fld_ITOtherNA" style="display:none;"></asp:TextBox>
                        </td>
                        <%--<td class="td-content" style="vertical-align:middle">
                            <span style="margin-top:5px;  height:30px; float:left;">&nbsp;</span>
                            <asp:TextBox runat="server" ID="fld_ITOtherSC" CssClass="validate[required]"></asp:TextBox>
                        </td>--%>
                        <td style="vertical-align:middle">
                            <span style="height:30px; float:left;">&nbsp;</span>
                            <asp:TextBox runat="server" ID="fld_ITOtherRPS" Width="94%"></asp:TextBox>
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






