<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AdministrationApproval.aspx.cs" Inherits="Presale.Process.EmployeeTermination.QualityApproval" %>
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
        function getButtonCheck(obj,index) {
            if (index == "1") {
                if ($(obj).attr("checked")) {
                    $("#fld_AdminIHBCC").val("CollectedCancelled");
                    $("#AdminIHBNA").attr("checked", false);
                }
                else {
                    $("#fld_AdminIHBCC").val("");
                }
            }
            if (index == "2") {
                if ($(obj).attr("checked")) {
                    $("#fld_AdminIHBNA").val("NA");
                    $("#AdminIHBCC").attr("checked", false);
                }
                else {
                    $("#fld_AdminIHBNA").val("");
                }
            }
            if (index == "3") {
                if ($(obj).attr("checked")) {
                    $("#fld_AdminCKCC").val("CollectedCancelled");
                    $("#AdminCKNA").attr("checked", false);
                }
                else {
                    $("#fld_AdminCKCC").val("");
                }
            }
            if (index == "4") {
                if ($(obj).attr("checked")) {
                    $("#fld_AdminCKNA").val("NA");
                    $("#AdminCKCC").attr("checked", false);
                }
                else {
                    $("#fld_AdminCKNA").val("");
                }
            }
            if (index == "5") {
                if ($(obj).attr("checked")) {
                    $("#fld_AdminCBCCC").val("CollectedCancelled");
                    $("#AdminCBCNA").attr("checked", false);
                }
                else {
                    $("#fld_AdminCBCCC").val("");
                }
            }
            if (index == "6") {
                if ($(obj).attr("checked")) {
                    $("#fld_AdminCBCNA").val("NA");
                    $("#AdminCBCCC").attr("checked", false);
                }
                else {
                    $("#fld_AdminCBCNA").val("");
                }
            }
            if (index == "7") {
                if ($(obj).attr("checked")) {
                    $("#fld_AdminCSCC").val("CollectedCancelled");
                    $("#AdminCSNA").attr("checked", false);
                }
                else {
                    $("#fld_AdminCSCC").val("");
                }
            }
            if (index == "8") {
                if ($(obj).attr("checked")) {
                    $("#fld_AdminCSNA").val("NA");
                    $("#AdminCSCC").attr("checked", false);
                }
                else {
                    $("#fld_AdminCSNA").val("");
                }
            }
        }
        $(document).ready(function () {
            if ($("#fld_AdminIHBCC").val() == "CollectedCancelled") { $("#AdminIHBCC").attr("checked", true); }
            if ($("#fld_AdminIHBNA").val() == "NA") { $("#AdminIHBNA").attr("checked", true); }
            if ($("#fld_AdminCKCC").val() == "CollectedCancelled") { $("#AdminCKCC").attr("checked", true); }
            if ($("#fld_AdminCKNA").val() == "NA") { $("#AdminCKNA").attr("checked", true); }
            if ($("#fld_AdminCBCCC").val() == "CollectedCancelled") { $("#AdminCBCCC").attr("checked", true); }
            if ($("#fld_AdminCBCNA").val() == "NA") { $("#AdminCBCNA").attr("checked", true); }
            if ($("#fld_AdminCSCC").val() == "CollectedCancelled") { $("#AdminCSCC").attr("checked", true); }
            if ($("#fld_AdminCSNA").val() == "NA") { $("#AdminCSNA").attr("checked", true); }
            if (request("type") == "myapproval") {
                $("#btnDiv").hide();
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
            if ($("#fld_AdminIHBCC").val() == "" && $("#fld_AdminIHBNA").val() == "")
                msg = "Pls select ID/HonFei Badge\n";
            if ($("#fld_AdminCKCC").val() == "" && $("#fld_AdminCKNA").val() == "")
                msg += "Pls select Cabinet keys\n";

            if ($("#fld_AdminCBCCC").val() == "" && $("#fld_AdminCBCNA").val() == "")
                msg += "Pls select Company Business Cards\n";

            if ($("#fld_AdminCSCC").val() == "" && $("#fld_AdminCSNA").val() == "")
                msg += "Pls select Collect Stationary\n";
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
                <p style="font-weight:bold;">Administration Responsibilities</p>
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
                        <%--<th>
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
                        <p style="text-align:center">ID/HonFei Badge</p>
                        </th>
                        <td style="vertical-align:middle;text-align:center">
                            <span style="height:30px; float:left;">&nbsp;</span>
                            <asp:CheckBox runat="server"  ID="AdminIHBCC" onclick="getButtonCheck(this,1)"></asp:CheckBox>
                            <asp:TextBox runat="server" ID="fld_AdminIHBCC" style="display:none;"></asp:TextBox>
                        </td>
                        <td style="vertical-align:middle;text-align:center">
                            <span style="height:30px; float:left;">&nbsp;</span>
                            <asp:CheckBox runat="server"  ID="AdminIHBNA" onclick="getButtonCheck(this,2)"></asp:CheckBox>
                            <asp:TextBox runat="server" ID="fld_AdminIHBNA" style="display:none;"></asp:TextBox>
                        </td>
                        <%--<td style="vertical-align:middle;text-align:center">
                            <span style="background:red;margin-top:5px;  height:30px; float:left;">&nbsp;</span>
                            <asp:TextBox runat="server" ID="fld_AdminIHBSC"></asp:TextBox>
                        </td>--%>
                        <td style="vertical-align:middle;text-align:center">
                            <span style="height:30px; float:left;">&nbsp;</span>
                            <asp:TextBox runat="server" ID="fld_AdminIHBRPS" Width="94%"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <th style="vertical-align:middle">
                         <span style="height:30px; float:left;">&nbsp;</span>
                        <p style="text-align:center">Cabinet keys</p>
                        </th>
                        <td style="vertical-align:middle;text-align:center">
                            <span style="height:30px; float:left;">&nbsp;</span>
                            <asp:CheckBox runat="server"  ID="AdminCKCC" onclick="getButtonCheck(this,3)"></asp:CheckBox>
                            <asp:TextBox runat="server" ID="fld_AdminCKCC" style="display:none;"></asp:TextBox>
                        </td>
                        <td style="vertical-align:middle;text-align:center">
                            <span style="height:30px; float:left;">&nbsp;</span>
                            <asp:CheckBox runat="server"  ID="AdminCKNA" onclick="getButtonCheck(this,4)"></asp:CheckBox>
                            <asp:TextBox runat="server" ID="fld_AdminCKNA" style="display:none;"></asp:TextBox>
                        </td>
                        <%--<td class="td-content" style="vertical-align:middle">
                            <span style="background:red;margin-top:5px;  height:30px; float:left;">&nbsp;</span>
                            <asp:TextBox runat="server" ID="fld_AdminCKSC" CssClass="validate[required]"></asp:TextBox>
                        </td>--%>
                        <td class="td-content" style="vertical-align:middle">
                            <span style="height:30px; float:left;">&nbsp;</span>
                            <asp:TextBox runat="server" ID="fld_AdminCKRPS" Width="94%"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <th class="td-label" style="vertical-align:middle">
                         <span style="height:30px; float:left;">&nbsp;</span>
                        <p style="text-align:center">Company Business Cards</p>
                        </th>
                        <td style="vertical-align:middle;text-align:center">
                            <span style="height:30px; float:left;">&nbsp;</span>
                            <asp:CheckBox runat="server"  ID="AdminCBCCC" onclick="getButtonCheck(this,5)" ></asp:CheckBox>
                            <asp:TextBox runat="server" ID="fld_AdminCBCCC" style="display:none;"></asp:TextBox>
                        </td>
                        <td style="vertical-align:middle;text-align:center">
                            <span style="height:30px; float:left;">&nbsp;</span>
                            <asp:CheckBox runat="server"  ID="AdminCBCNA" onclick="getButtonCheck(this,6)"></asp:CheckBox>
                            <asp:TextBox runat="server" ID="fld_AdminCBCNA" style="display:none;"></asp:TextBox>
                        </td>
                        <%--<td class="td-content" style="vertical-align:middle">
                            <span style="background:red;margin-top:5px;  height:30px; float:left;">&nbsp;</span>
                            <asp:TextBox runat="server" ID="fld_AdminCBCSC"  CssClass="validate[required]"></asp:TextBox>
                        </td>--%>
                        <td style="vertical-align:middle">
                            <span style="height:30px; float:left;">&nbsp;</span>
                            <asp:TextBox runat="server" ID="fld_AdminCBCRPS"  Width="94%"></asp:TextBox>
                        </td>
                    </tr>
                     <tr>
                        <th style="vertical-align:middle">
                         <span style="height:30px; float:left;">&nbsp;</span>
                        <p style="text-align:center">Collect Stationary</p>
                        </th>
                        <td style="vertical-align:middle;text-align:center">
                            <span style="height:30px; float:left;">&nbsp;</span>
                            <asp:CheckBox runat="server"  ID="AdminCSCC" onclick="getButtonCheck(this,7)"></asp:CheckBox>
                            <asp:TextBox runat="server" ID="fld_AdminCSCC" style="display:none;"></asp:TextBox>
                        </td>
                        <td style="vertical-align:middle;text-align:center">
                            <span style="height:30px; float:left;">&nbsp;</span>
                            <asp:CheckBox runat="server"  ID="AdminCSNA" onclick="getButtonCheck(this,8)"></asp:CheckBox>
                            <asp:TextBox runat="server" ID="fld_AdminCSNA" style="display:none;"></asp:TextBox>
                        </td>
                        <%--<td class="td-content" style="vertical-align:middle">
                            <span style="background:red;margin-top:5px;  height:30px; float:left;">&nbsp;</span>
                            <asp:TextBox runat="server" ID="fld_AdminCSSC"  CssClass="validate[required]"></asp:TextBox>
                        </td>--%>
                        <td style="vertical-align:middle;text-align:center">
                            <span style="height:30px; float:left;">&nbsp;</span>
                            <asp:TextBox runat="server" ID="fld_AdminCSRPS" Width="94%"></asp:TextBox>
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






