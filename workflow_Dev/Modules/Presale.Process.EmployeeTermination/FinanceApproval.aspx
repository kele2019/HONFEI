<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="FinanceApproval.aspx.cs" Inherits="Presale.Process.EmployeeTermination.FinanceApproval" %>
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
        function getButtonCheck(obj, index) {
            if (index == "1") {
                if ($(obj).attr("checked")) {
                    $("#fld_FinanceRFSCC").val("CollectedCancelled");
                    $("#FinanceRFSNA").attr("checked", false);
                }
                else {
                    $("#fld_FinanceRFSCC").val("");
                }
            }
            if (index == "2") {
                if ($(obj).attr("checked")) {
                    $("#fld_FinanceRFSNA").val("NA");
                    $("#FinanceRFSCC").attr("checked", false);
                }
                else {
                    $("#fld_FinanceRFSNA").val("");
                }
            }
            if (index == "3") {
                if ($(obj).attr("checked")) {
                    $("#fld_FinanceLAACSCC").val("CollectedCancelled");
                    $("#FinanceLAACSNA").attr("checked", false);
                }
                else {
                    $("#fld_FinanceLAACSCC").val("");
                }
            }
            if (index == "4") {
                if ($(obj).attr("checked")) {
                    $("#fld_FinanceLAACSNA").val("NA");
                    $("#FinanceLAACSCC").attr("checked", false);
                }
                else {
                    $("#fld_FinanceLAACSNA").val("");
                }
            }
            if (index == "5") {
                if ($(obj).attr("checked")) {
                    $("#fld_FinanceOtherCC").val("CollectedCancelled");
                    $("#FinanceOtherNA").attr("checked", false);
                }
                else {
                    $("#fld_FinanceOtherCC").val("");
                }
            }
            if (index == "6") {
                if ($(obj).attr("checked")) {
                    $("#fld_FinanceOtherNA").val("NA");
                    $("#FinanceOtherCC").attr("checked", false);
                }
                else {
                    $("#fld_FinanceOtherNA").val("");
                }
            }
        }
        $(document).ready(function () { 
            if($("#fld_FinanceRFSCC").val() == "CollectedCancelled"){ $("#FinanceRFSCC").attr("checked",true); }
            if($("#fld_FinanceRFSNA").val() == "NA"){ $("#FinanceRFSNA").attr("checked",true); }
            if ($("#fld_FinanceLAACSCC").val() == "CollectedCancelled") { $("#FinanceLAACSCC").attr("checked", true); }
            if ($("#fld_FinanceLAACSNA").val() == "NA") { $("#FinanceLAACSNA").attr("checked", true); }
            if ($("#fld_FinanceOtherCC").val() == "CollectedCancelled") { $("#FinanceOtherCC").attr("checked", true); }
            if ($("#fld_FinanceOtherNA").val() == "NA") { $("#FinanceOtherNA").attr("checked", true); }
            if (request("type") == "myapproval") {
                func1();
            }
        });

        function func1() {
            $("#formdiv").each(function (i) {
                $(this).find('input[type="checkbox"]').attr("disabled", "disabled");
                $(this).find('input[type="text"]').attr("disabled", "disabled");
                $(this).find('textarea').attr("disabled", "disabled");

            });
        }

        function beforeSubmit() {
            var msg = "";
            if ($("#fld_FinanceRFSCC").val() == "" && $("#fld_FinanceRFSNA").val() == "")
                msg = "Pls select Reserved fund settled\n";
            if ($("#fld_FinanceLAACSCC").val() == "" && $("#fld_FinanceLAACSNA").val() == "")
                msg += "Pls select Loan and advanced cash settled\n";


            if ($("#fld_FinanceOtherCC").val() == "" && $("#fld_FinanceOtherNA").val() == "")
                msg += "Pls select Email Other\n";
            if (msg != "") {
                alert(msg);
                return false;
            }
            else
                return true;
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
                <p style="font-weight:bold;">Finance Responsibilities</p>
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
                        <p style="text-align:center">N/A</p>
                        </th>
                       <%-- <th width="30%">
                         <span style="height:30px; float:left;">&nbsp;</span>
                        <p style="text-align:center">Serial/Card</p>
                        </th>--%>
                        <th width="40%">
                         <span style="height:30px; float:left;">&nbsp;</span>
                        <p style="text-align:center">Comments</p>
                        </th>
                    </tr>
                     <tr>
                        <th style="vertical-align:middle">
                         <span style="height:30px; float:left;">&nbsp;</span>
                        <p style="text-align:center">Reserved fund settled</p>
                        </th>
                        <td style="vertical-align:middle">
                            <span style="height:30px; float:left;">&nbsp;</span>
                            <asp:CheckBox runat="server"  ID="FinanceRFSCC" onclick="getButtonCheck(this,1)" style="margin-left:50%;"></asp:CheckBox>
                            <asp:TextBox runat="server" ID="fld_FinanceRFSCC" style="display:none;"></asp:TextBox>
                        </td>
                        <td style="vertical-align:middle">
                            <span style="height:30px; float:left;">&nbsp;</span>
                            <asp:CheckBox runat="server"  ID="FinanceRFSNA" onclick="getButtonCheck(this,2)" style="margin-left:50%;"></asp:CheckBox>
                            <asp:TextBox runat="server" ID="fld_FinanceRFSNA" style="display:none;"></asp:TextBox>
                        </td>
                        <%--<td style="vertical-align:middle">
                            <span style="background:red;margin-top:5px;  height:30px; float:left;">&nbsp;</span>
                            <asp:TextBox runat="server" ID="fld_FinanceRFSSC" CssClass="validate[required]"></asp:TextBox>
                        </td>--%>
                        <td style="vertical-align:middle">
                            <span style="height:30px; float:left;">&nbsp;</span>
                            <asp:TextBox runat="server" ID="fld_FinanceRFSRPS" Width="94%"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <th style="vertical-align:middle">
                         <span style="height:30px; float:left;">&nbsp;</span>
                        <p style="text-align:center">Loan and advanced cash settled</p>
                        </th>
                        <td style="vertical-align:middle">
                            <span style="height:30px; float:left;">&nbsp;</span>
                            <asp:CheckBox runat="server"  ID="FinanceLAACSCC" onclick="getButtonCheck(this,3)" style="margin-left:50%;"></asp:CheckBox>
                            <asp:TextBox runat="server" ID="fld_FinanceLAACSCC" style="display:none;"></asp:TextBox>
                        </td>
                        <td style="vertical-align:middle">
                            <span style="height:30px; float:left;">&nbsp;</span>
                            <asp:CheckBox runat="server"  ID="FinanceLAACSNA" onclick="getButtonCheck(this,4)" style="margin-left:50%;"></asp:CheckBox>
                            <asp:TextBox runat="server" ID="fld_FinanceLAACSNA" style="display:none;"></asp:TextBox>
                        </td>
                       <%-- <td style="vertical-align:middle">
                            <span style="margin-top:5px;  height:30px; float:left;">&nbsp;</span>
                            <asp:TextBox runat="server" ID="fld_FinanceLAACSSC" CssClass="validate[required]"></asp:TextBox>
                        </td>--%>
                        <td style="vertical-align:middle">
                            <span style="height:30px; float:left;">&nbsp;</span>
                            <asp:TextBox runat="server" ID="fld_FinanceLAACSRPS" Width="94%"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <th style="vertical-align:middle">
                         <span style="height:30px; float:left;">&nbsp;</span>
                        <p style="text-align:center">Other</p>
                        </th>
                        <td style="vertical-align:middle">
                            <span style="height:30px; float:left;">&nbsp;</span>
                            <asp:CheckBox runat="server"  ID="FinanceOtherCC" onclick="getButtonCheck(this,5)" style="margin-left:50%;"></asp:CheckBox>
                            <asp:TextBox runat="server" ID="fld_FinanceOtherCC" style="display:none;"></asp:TextBox>
                        </td>
                        <td style="vertical-align:middle">
                            <span style="height:30px; float:left;">&nbsp;</span>
                            <asp:CheckBox runat="server"  ID="FinanceOtherNA" onclick="getButtonCheck(this,6)" style="margin-left:50%;"></asp:CheckBox>
                            <asp:TextBox runat="server" ID="fld_FinanceOtherNA" style="display:none;"></asp:TextBox>
                        </td>
                        <%--<td style="vertical-align:middle">
                            <span style="margin-top:5px;  height:30px; float:left;">&nbsp;</span>
                            <asp:TextBox runat="server" ID="fld_FinanceOtherSC"  CssClass="validate[required]"></asp:TextBox>
                        </td>--%>
                        <td style="vertical-align:middle">
                            <span style="height:30px; float:left;">&nbsp;</span>
                            <asp:TextBox runat="server" ID="fld_FinanceOtherRPS" Width="94%"></asp:TextBox>
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






