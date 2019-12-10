<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="NewRequest.aspx.cs" Inherits="Presale.Process.QualityDocumentManagement.NewRequest" %>

<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>
<%@ Register Src="/Modules/Ultimus.UWF.Form.ProcessControl/UserInfo.ascx" TagName="UserInfo" TagPrefix="ui" %>
<%@ Register Src="/Modules/Ultimus.UWF.Form.ProcessControl/ApprovalHistory.ascx" TagName="ApprovalHistory" TagPrefix="ah" %>
<%@ Register Src="/Modules/Ultimus.UWF.Form.ProcessControl/Attachments.ascx" TagName="Attachments" TagPrefix="attach" %>
<%@ Register Src="/Modules/Ultimus.UWF.Form.ProcessControl/ButtonList.ascx" TagName="ButtonList" TagPrefix="btn" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
 <meta http-equiv="X-UA-Compatible"  content="IE=EmulateIE8" />
    <title>Quality Document Management</title>
     <script type="text/javascript" src="/Assets/js/common.js"></script>
        <script type="text/javascript">
            $(document).ready(function () {
                if ($("#fld_deptMan").val() == "yes") {
                    $("#dept1").attr("checked", true);
                }
                if ($("#fld_deptIT").val() == "yes") {
                    $("#dept2").attr("checked", true);
                }

                if ($("#fld_deptHR").val() == "yes") {
                    $("#dept3").attr("checked", true);
                }
                if ($("#fld_deptEng").val() == "yes") {
                    $("#dept4").attr("checked", true);
                }
                if ($("#fld_deptFin").val() == "yes") {
                    $("#dept5").attr("checked", true);
                }
                if ($("#fld_deptQM").val() == "yes") {
                    $("#dept6").attr("checked", true);
                }
                if ($("#fld_deptAdmin").val() == "yes") {
                    $("#dept7").attr("checked", true);
                }
                if ($("#fld_deptPUR").val() == "yes") {
                    $("#dept8").attr("checked", true);
                }
                if ($("#fld_deptPM").val() == "yes") {
                    $("#dept9").attr("checked", true);
                }
                if ($("#fld_deptOP").val() == "yes") {
                    $("#dept10").attr("checked", true);
                }
                if ($("#fld_deptGM").val() == "yes") {
                    $("#dept11").attr("checked", true);
                }
                if ($("#fld_deptMarketing").val() == "yes") {
                    $("#detp12").attr("checked", true);
                }
                if (request('Type') == 'myrequest') {
                    $("#deptinfo").find("input[type='checkbox']").attr("disabled", "disabled");
                }
                $("#dropDocType option:selected").text($("#fld_DOCtype").val());
                $("#dropOperMode option:selected").text($("#fld_OperMode").val());
                if ($("#hdIncident").val() != "") {
                    $("#ButtonList1_btnSubmit").val("Submit");
                    $("#ButtonList1_btnBack").hide();
                    $("#ButtonList1_btnReject").show();
                }
                if ($("#hdUrgeTask").val() == "Yes") {
                    $("#ReturnBackTask").show();
                }
                changeOperMode();
            });
            function beforeSubmit() {
                if ($("#dropDocType").val() == "1、2level document") {
                    $("#deptinfo").find("input[type='checkbox']").each(function () {
                        $(this).attr("checked", true);
                    });
                }
                else {
                var SelectCount=0;
                $("#deptinfo").find("input[type='checkbox']").each(function () {
                    if ($(this).attr("checked"))
                        SelectCount++;
                });
                if (SelectCount <= 0) {
                    alert("Department of information sign Invalid");
                    return false;
                }
                }

                var deptManager = "";//  $("#fld_ApprovalArr_CheckDeptManager").val();
                if ($("#dept1").attr("checked") || $("#dept6").attr("checked")) {
                    if($("#dept1").attr("checked")){
                        $("#fld_deptMan").val("yes");
                    }
                    if($("#dept6").attr("checked")){
                        $("#fld_deptQM").val("yes");
                    }
                    deptManager += $("#fld_QAMLogin").val() + ",";
                }
                if ($("#dept2").attr("checked")) {
                    $("#fld_deptIT").val("yes");
                    deptManager += $("#fld_ITMLogin").val() + ",";
                }
                if ($("#dept3").attr("checked")) {
                    $("#fld_deptHR").val("yes");
                    deptManager += $("#fld_HRMLogin").val() + ",";
                }
                if ($("#dept4").attr("checked")) {
                    $("#fld_deptEng").val("yes");
                    deptManager += $("#fld_CTOLogin").val() + ",";
                }
                if ($("#dept5").attr("checked")) {
                    $("#fld_deptFin").val("yes");
                    deptManager += $("#fld_CFOLogin").val() + ",";
                }
                if ($("#dept7").attr("checked")) {
                    $("#fld_deptAdmin").val("yes");
                    deptManager += $("#fld_AdimLogin").val() + ",";
                }
                if ($("#dept8").attr("checked")) {
                    $("#fld_deptPUR").val("yes");
                    deptManager += $("#fld_SupplierMLogin").val() + ",";
                }
                if ($("#dept9").attr("checked")) {
                    $("#fld_deptPM").val("yes");
                    deptManager += $("#fld_PMLogin").val() + ",";
                }
                if ($("#dept10").attr("checked")) {
                    $("#fld_deptOP").val("yes");
                    deptManager += $("#fld_DGMLogin").val() + ",";
                }
                if ($("#dept11").attr("checked")) {
                    $("#fld_deptGM").val("yes");
                    deptManager += $("#fld_GMLogin").val() + ",";
                }
                if ($("#detp12").attr("checked")) {
                    $("#fld_deptMarketing").val("yes");
                    deptManager += $("#fld_deptMarketingLogin").val() + ",";
                }
                $("#fld_ApprovalArr_CheckDeptManager").val(deptManager);
                $("#fld_DOCtype").val($("#dropDocType").val());
                $("#fld_OperMode").val($("#dropOperMode").val());
//                if ($("#fld_documentName").val() == "") {
//                    alert("You must full document name");
//                    return false;
//                }
                var summary = "Quality Document Management";
                $("#UserInfo1_fld_PROCESSSUMMARY").val(summary);
                return true;
            }
            function beforeSave() {
                var summary = "Quality Document Management";
                $("#UserInfo1_fld_PROCESSSUMMARY").val(summary);
                $("#fld_TRSummary").val(summary);
                return true;
            }
            function changeDocType(obj) {
                $(obj).next().val($(obj).find("option:selected").text());
                $("#fld_documentName").val("");
                if ($("#dropDocType").val() == "Other") {
                    $("#Attachments1_txtMust").val("1");
                }
                else
                    $("#Attachments1_txtMust").val("");

                if ($("#dropDocType").val() == "1、2level document") {
                    $("#deptinfo").find("input[type='checkbox']").each(function () {
                        $(this).attr("checked", true);
//                        $("#dept1").attr("disabled", "disabled");
//                        $("#dept6").attr("disabled", "disabled");
                    });
                }
                else {
                    $("#deptinfo").find("input[type='checkbox']").each(function () {
                        $(this).parent().attr("disabled", false);
                        $(this).attr("disabled", false);
                    });
                }
            }
            function changeOperMode() {
                $("#dropOperMode").next().val($("#dropOperMode").find("option:selected").text());
                if ($("#dropOperMode").find("option:selected").text() == "modify document") {
                    $("#majorchange").show();
                }
                else
                    $("#majorchange").hide();

            }
            function documentName_onclick(obj) {
            }
        </script>
</head>
<body>
    <form id="form1" runat="server">
        <div id="myDiv" class="container">
            <div class="row">
                <ui:userinfo id="UserInfo1" processtitle="Quality Document Management" processprefix="QMS" tablename="PROC_QualityDocumentManagement" runat="server"></ui:userinfo>
                <asp:TextBox runat="server" ID="fld_ApplicantUserLogin" style="display:none;"></asp:TextBox>
                <asp:TextBox runat="server" ID="fld_ApplicantUser" style="display:none;"></asp:TextBox>
                <asp:TextBox runat="server" ID="fld_ApplicantDeptManager" style="display:none;"></asp:TextBox>
                <asp:TextBox runat="server" ID="fld_QAMLogin" style="display:none;"></asp:TextBox>
                <asp:TextBox runat="server" ID="fld_ITMLogin" style="display:none;"></asp:TextBox>
                <asp:TextBox runat="server" ID="fld_HRMLogin" style="display:none;"></asp:TextBox>
                <asp:TextBox runat="server" ID="fld_CTOLogin" style="display:none;"></asp:TextBox>
                <asp:TextBox runat="server" ID="fld_CFOLogin" style="display:none;"></asp:TextBox>
                <asp:TextBox runat="server" ID="fld_AdimLogin" style="display:none;"></asp:TextBox>
                <asp:TextBox runat="server" ID="fld_SupplierMLogin" style="display:none;"></asp:TextBox>
                <asp:TextBox runat="server" ID="fld_PMLogin" style="display:none;"></asp:TextBox>
                <asp:TextBox runat="server" ID="fld_DGMLogin" style="display:none;"></asp:TextBox>
                <asp:TextBox runat="server" ID="fld_GMLogin" style="display:none;"></asp:TextBox>
                <asp:TextBox runat="server" ID="fld_deptMarketingLogin" style="display:none;"></asp:TextBox>
            </div>
            <div class="row">
                <p style="font-weight:bold">Document Information （<span style=" background:red">&nbsp;</span> must write）</p>
                <table class="table table-condensed table-bordered">
                    <tr>
                        <td class="td-label">
                            <span style=" background:red;float:left;">&nbsp;</span> 
                            <p style="text-align:center">Document Type </p>
                        </td>
                        <td class="td-content">
                            <asp:DropDownList ID="dropDocType" CssClass="validate[required]" runat="server"  onchange="changeDocType(this)">
                                <asp:ListItem Selected="True">--Selecte Please--</asp:ListItem>
                                <asp:ListItem >1、2level document</asp:ListItem>
                                <asp:ListItem>3、4level document</asp:ListItem>
                                <asp:ListItem>Other</asp:ListItem>
                            </asp:DropDownList>
                            <asp:TextBox ID="fld_DOCtype" value="--Selecte Please--" runat="server" Style="display: none"></asp:TextBox>
                        </td>
                        <td class="td-label">
                            <span style=" background:red;float:left;">&nbsp;</span> 
                            <p style="text-align:center">Oper Mode</p>
                        </td>
                        <td class="td-content">
                            <asp:DropDownList ID="dropOperMode" runat="server"  CssClass="validate[required]" onchange="changeOperMode()">
                                <asp:ListItem Selected="True" Value="">--Selecte Please--</asp:ListItem>
                                <asp:ListItem >create document</asp:ListItem>
                                <asp:ListItem>modify document</asp:ListItem>
                                <asp:ListItem>abolish document</asp:ListItem>
                            </asp:DropDownList>
                            <asp:TextBox ID="fld_OperMode" value="--Selecte Please--" runat="server" Style="display: none"></asp:TextBox>
                        </td>
                    </tr>
                </table>
                <table class="table table-condensed table-bordered">   
                    <tr>
                        <td class="td-label">
                            <span style="background: red; float: left;">&nbsp;</span>
                            <p style="text-align:center">Document Name</p>
                        </td>
                        <td class="td-content" colspan="3">
                            <asp:TextBox runat="server" ID="fld_documentName" Width="98%"  CssClass="validate[required]" onclick="documentName_onclick(this)"></asp:TextBox>
                        </td>
                    </tr>
                </table>
                <table class="table table-condensed table-bordered">
                    <tr>
                        <td class="td-label">
                            <span style="background: red; float: left;">&nbsp;</span>
                            <p style="text-align:center">Document No.</p>
                        </td>
                        <td class="td-content" colspan="3">
                            <asp:TextBox runat="server" ID="fld_documentNumber"  CssClass="validate[required]" Width="98%"></asp:TextBox>
                        </td>
                    </tr>
                </table>
                <table class="table table-condensed table-bordered">
                    <tr>
                        <td class="td-label">
                            <span style="background: red; float: left;">&nbsp;</span>
                            <p style="text-align:center">Document Owner</p>
                        </td>
                        <td class="td-content" colspan="3">
                            <asp:TextBox runat="server" ID="fld_documentOwner"  CssClass="validate[required]" Width="98%"></asp:TextBox>
                        </td>
                    </tr>
                </table>
                <table class="table table-condensed table-bordered">
                    <tr>
                        <td class="td-label" style="vertical-align:middle;">
                            <span style=" background:red; float:left">&nbsp;</span> 
                            <p style="text-align:center">Description of Document</p> 
                        </td>
                        <td class="td-content" colspan="3">
                            <asp:TextBox ID="fld_DOCDescription" Rows="3" runat="server" Width="98%"  CssClass="validate[required]" TextMode="MultiLine"></asp:TextBox>
                        </td>
                    </tr>
                </table>
                <table class="table table-condensed table-bordered" id="majorchange" style="display:block;">
                    <tr>
                        <td class="td-label" style="vertical-align:middle;">
                            <span style=" background:red; float:left">&nbsp;</span> 
                            <p style="text-align:center">major change</p> 
                        </td>
                        <td class="td-content" colspan="3">
                            <asp:TextBox ID="fld_MajorChange" Rows="3" runat="server" Width="98%"  CssClass="validate[required]" TextMode="MultiLine"></asp:TextBox>
                        </td>
                    </tr>
                </table>
                </div>
            <div class="row" id="deptinfo" style="display:block;">
                <p style="font-weight:bold">Department of information sign（<span style=" background:red">&nbsp;</span> must write）</p>
                <table class="table table-condensed table-bordered">
                    <tr>
                        <td style="border-right:none;border-bottom:none;">
                            <asp:CheckBox runat="server"     id="dept1"/>R&E
                            <asp:TextBox runat="server" ID="fld_deptMan" style="display:none;"></asp:TextBox>
                        </td>
                        <td style="border:none;">
                            <asp:CheckBox runat="server"  id="dept2"/>IT
                            <asp:TextBox runat="server" ID="fld_deptIT" style="display:none;"></asp:TextBox>
                        </td>
                        <td style="border:none;">
                            <asp:CheckBox runat="server"   id="dept3"/>Human Reaource
                            <asp:TextBox runat="server" ID="fld_deptHR" style="display:none;"></asp:TextBox>
                        </td>
                        <td style="border-left:none;border-bottom:none;border-top:none;">
                            <asp:CheckBox runat="server"  id="dept4"/>Engineering
                            <asp:TextBox runat="server" ID="fld_deptEng" style="display:none;"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td style="border-top:none;">
                            <asp:CheckBox runat="server" id="dept5"/>Finance
                            <asp:TextBox runat="server" ID="fld_deptFin" style="display:none;"></asp:TextBox>
                        </td>
                        <td style="border:none;">
                            <asp:CheckBox runat="server"   id="dept6"/>Quality
                            <asp:TextBox runat="server" ID="fld_deptQM" style="display:none;"></asp:TextBox>
                        </td>
                        <td style="border-right:none;border-top:none;border-left:none;">
                            <asp:CheckBox runat="server"  id="dept7"/>HSE
                            <asp:TextBox runat="server" ID="fld_deptAdmin" style="display:none;"></asp:TextBox>
                        </td>
                        <td style="border-right:none;border-left:none;border-top:none;">
                            <asp:CheckBox runat="server"  id="dept8"/>Material Management
                            <asp:TextBox runat="server" ID="fld_deptPUR" style="display:none;"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td style="border-right:none;border-top:none;">
                            <asp:CheckBox runat="server"  ID="dept9" />Program management
                            <asp:TextBox runat="server" ID="fld_deptPM" style="display:none;"></asp:TextBox>
                        </td>
                        <td style="border-right:none;border-top:none;border-left:none;">
                            <asp:CheckBox runat="server"  ID="dept10" />Operation/Production
                            <asp:TextBox runat="server" ID="fld_deptOP" style="display:none;"></asp:TextBox>
                        </td>
                        <td style="border:none;">
                          <asp:CheckBox runat="server"  ID="dept11" />GM
                            <asp:TextBox runat="server" ID="fld_deptGM" style="display:none;"></asp:TextBox>
                        </td>
                        <td style="border:none;">
                          <asp:CheckBox runat="server"  ID="detp12" />Marketing
                            <asp:TextBox runat="server" ID="fld_deptMarketing" style="display:none;"></asp:TextBox>
                        </td>
                    </tr>
                </table>
            </div>
            <asp:TextBox Class="departlist" ID="fld_departlist" runat="server" style="display:none;"></asp:TextBox>
            <asp:TextBox runat="server" ID="fld_ApprovalArr_CheckDeptManager" style="display:none;"></asp:TextBox>
            
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
