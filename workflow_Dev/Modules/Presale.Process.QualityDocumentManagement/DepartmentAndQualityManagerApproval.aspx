<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="DepartmentAndQualityManagerApproval.aspx.cs" Inherits="Presale.Process.QualityDocumentManagement.DepartmentAndQualityManagerApproval" %>

<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>
<%@ Register Src="/Modules/Ultimus.UWF.Form.ProcessControl/UserInfo.ascx" TagName="UserInfo" TagPrefix="ui" %>
<%@ Register Src="/Modules/Ultimus.UWF.Form.ProcessControl/ApprovalHistory.ascx" TagName="ApprovalHistory" TagPrefix="ah" %>
<%@ Register Src="/Modules/Ultimus.UWF.Form.ProcessControl/Attachments.ascx" TagName="Attachments" TagPrefix="attach" %>
<%@ Register Src="/Modules/Ultimus.UWF.Form.ProcessControl/ButtonList.ascx" TagName="ButtonList" TagPrefix="btn" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Quality Document Management</title>
     <script type="text/javascript" src="/Assets/js/common.js"></script>
        <script type="text/javascript">
            $(document).ready(function () {
                if ($("#read_DOCtype").val() == "1、2level document") {
                    $("#deptinfo").show();
                }
                if ($("#read_deptMan").text().trim() == "yes") { $("#dept1").attr("checked", true); }
                if ($("#read_deptIT").text().trim() == "yes") { $("#dept2").attr("checked", true); }
                if ($("#read_deptHR").text().trim() == "yes") { $("#dept3").attr("checked", true); }
                if ($("#read_deptEng").text().trim() == "yes") { $("#dept4").attr("checked", true); }
                if ($("#read_deptFin").text().trim() == "yes") { $("#dept5").attr("checked", true); }
                if ($("#read_deptQM").text().trim() == "yes") { $("#dept6").attr("checked", true); }
                if ($("#read_deptAdmin").text().trim() == "yes") { $("#dept7").attr("checked", true); }
                if ($("#read_deptPUR").text().trim() == "yes") { $("#dept8").attr("checked", true); }
                if ($("#read_deptPM").text().trim() == "yes") { $("#dept9").attr("checked", true); }
                if ($("#read_deptOP").text().trim() == "yes") { $("#dept10").attr("checked", true); }
                if ($("#read_OperMode").text() == "modify document") {
                    $("#majorchange").show();
//                    $("#AOfdocumentUrl").show();
                    $("#documentName").text($("#read_documentName").text());
                    var documentUrl = $("#read_documentName").text();
//                    $("#AOfdocumentUrl").attr("href", documentUrl);
                }
                if ($("#read_OperMode").text() == "create document" || $("#read_OperMode").text() == "abolish document") {
                    $("#read_documentName").show();
                }
            });
        </script>
</head>
<body>
    <form id="form1" runat="server">
    <div id="myDiv" class="container">
        <div class="row">
            <ui:userinfo id="UserInfo1" processtitle="Quality Document Management" processprefix="QMS" tablename="PROC_QualityDocumentManagement" runat="server"></ui:userinfo>
        </div>
        <div class="row">
            <p style="font-weight:bold">Document Information</p>
            <table class="table table-condensed table-bordered">
                <tr>
                    <td class="td-label">
                        <span style="float:left;">&nbsp;</span> 
                        <p style="text-align:center">Document Type </p>
                    </td>
                    <td class="td-content" colspan="3">
                        <asp:Label ID="read_DOCtype" runat="server"></asp:Label>
                    </td>
                     <td class="td-label">
                     <span style="float:left;">&nbsp;</span> 
                    <p style="text-align:center">Oper Mode </p></td>
                    <td class="td-content" colspan="3">
                        <asp:Label ID="read_OperMode" runat="server"></asp:Label>
                    </td>
                </tr>
            </table>
             <table class="table table-condensed table-bordered">
                <tr>
                    <td class="td-label">
                        <span style="float: left;">&nbsp;</span>
                        <p style="text-align:center">Document Name</p>
                    </td>
                    <td class="td-content" colspan="7">
                        <asp:Label runat="server" ID="read_documentName" ></asp:Label>
                       <%-- <a id="AOfdocumentUrl" style="display:none"><asp:Label runat="server" ID="documentName" ></asp:Label></a>--%>
                    </td>
                </tr>
            </table>
            <table class="table table-condensed table-bordered">
                <tr>
                    <td class="td-label">
                        <span style="float: left;">&nbsp;</span>
                        <p style="text-align:center" id="P1">Document No.</p>
                    </td>
                    <td class="td-content" colspan="7">
                        <asp:Label runat="server" ID="read_documentNumber"></asp:Label>
                    </td>
                </tr>
            </table>
            <table class="table table-condensed table-bordered">
                <tr>
                    <td class="td-label">
                        <span style="float: left;">&nbsp;</span>
                        <p style="text-align:center" id="P2">Document Owner</p>
                    </td>
                    <td class="td-content" colspan="7">
                        <asp:Label runat="server" ID="read_documentOwner"></asp:Label>
                    </td>
                </tr>
            </table>
            <table class="table table-condensed table-bordered">
                <tr>
                    <td class="td-label" style="vertical-align:middle;">
                        <span style="float:left">&nbsp;</span> 
                        <p style="text-align:center">Description of Document</p> 
                    </td>
                    <td class="td-content" colspan="7">
                        <asp:Label ID="read_DOCDescription" runat="server"></asp:Label>
                    </td>
                </tr>
            </table>
            <table class="table table-condensed table-bordered" id="majorchange" style="display:block;">
                <tr>
                    <td class="td-label" style="vertical-align:middle;">
                        <span style="float:left">&nbsp;</span> 
                        <p style="text-align:center">major change</p> 
                    </td>
                    <td class="td-content" colspan="3">
                        <asp:Label ID="read_MajorChange" runat="server"></asp:Label>
                    </td>
                </tr>
            </table>
            <div style="display:none;" id="deptinfo">
            <p style="font-weight:bold">Department of information sign</p>
            <table class="table table-condensed table-bordered">
                <tr>
                    <td style="border-right:none;border-bottom:none;">
                        <asp:CheckBox runat="server" id="dept1" Enabled="false"/>R&E
                        <asp:Label runat="server" ID="read_deptMan" style="display:none;"></asp:Label>
                    </td>
                    <td style="border:none;">
                        <asp:CheckBox runat="server" id="dept2" Enabled="false"/>IT
                        <asp:Label runat="server" ID="read_deptIT" style="display:none;"></asp:Label>
                    </td>
                    <td style="border:none;">
                        <asp:CheckBox runat="server" id="dept3" Enabled="false"/>Human Reaource
                        <asp:Label runat="server" ID="read_deptHR" style="display:none;"></asp:Label>
                    </td>
                    <td style="border-left:none;border-bottom:none;border-top:none;">
                        <asp:CheckBox runat="server" id="dept4" Enabled="false"/>Engineering
                        <asp:Label runat="server" ID="read_deptEng" style="display:none;"></asp:Label>
                    </td>
                </tr>
                <tr>
                    
                    <td style="border-top:none;">
                        <asp:CheckBox runat="server" id="dept5" Enabled="false"/>Finance
                        <asp:Label runat="server" ID="read_deptFin" style="display:none;"></asp:Label>
                    </td>
                    <td style="border:none;">
                        <asp:CheckBox runat="server" id="dept6" Enabled="false"/>Quality
                        <asp:Label runat="server" ID="read_deptQM" style="display:none;"></asp:Label>
                    </td>
                    <td style="border-right:none;border-top:none;border-left:none;">
                        <asp:CheckBox runat="server" id="dept7" Enabled="false"/>HSE
                        <asp:Label runat="server" ID="read_deptAdmin" style="display:none;"></asp:Label>
                    </td>
                    <td style="border-right:none;border-left:none;border-top:none;">
                        <asp:CheckBox runat="server" id="dept8" Enabled="false"/>Material Management
                        <asp:Label runat="server" ID="read_deptPUR" style="display:none;"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td style="border-right:none;border-top:none;">
                        <asp:CheckBox runat="server" ID="dept9" Enabled="false"/>Program management
                        <asp:Label runat="server" ID="read_deptPM" style="display:none;"></asp:Label>
                    </td>
                    <td style="border-right:none;border-top:none;border-left:none;">
                        <asp:CheckBox runat="server" ID="dept10" Enabled="false"/>Operation/Production
                        <asp:Label runat="server" ID="read_deptOP" style="display:none;"></asp:Label>
                    </td>
                    <td style="border:none;"></td>
                    <td style="border:none;"></td>
                </tr>
            </table>
        </div>
            <asp:Label ID="read_departlist" runat="server" style="display:none;"></asp:Label>
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

