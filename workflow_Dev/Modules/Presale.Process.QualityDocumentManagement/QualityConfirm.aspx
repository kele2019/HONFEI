<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="QualityConfirm.aspx.cs" Inherits="Presale.Process.QualityDocumentManagement.QualityConfirm" %>
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
    <script language="javascript" type="text/javascript">
        $(document).ready(function () {
            $("#Docname").text($("read_Docname").text());
//            if ($("#read_OperMode").text() == "create document") {
//                $("#adddocument").show();
//            }
            if ($("#read_OperMode").text() == "modify document") {
                $("#majorchange").show();
                $("#modifydocument").show();
                var documentName = $("#read_documentName").text();
                var index = documentName.lastIndexOf("/");
                $("#documentName").text(documentName.substring(index + 1));
            }
            if (request('Type') == "myapproval") {
                $("#btnDiv").hide();
            }
        });
        function downLoad_Click() {
            var url = $("#read_documentName").text();
//            var index = url.search(name);
//            url = url.substring(0, index - 1);
            var sharepoint = window.open(url, "");
            sharepoint.focus();
        }
        function submitPageReview(obj) {
            $("#ButtonList1_btnSubmit").click();
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div id="myDiv" class="container">
        <div class="row">
            <ui:userinfo id="UserInfo1" processtitle="Quality Document Management" processprefix="QD" tablename="PROC_QualityDocumentManagement" runat="server"></ui:userinfo>
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
                        <asp:Label runat="server" ID="read_documentName"></asp:Label>
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
            <div id="modifydocument" style="display:none;">
            <%--<p style="font-weight:bold">Current Document</p>
            <table class="table table-condensed table-bordered">
                <tr>
                    <td class="td-label" style="vertical-align:middle;">
                        <span style="float:left">&nbsp;</span> 
                        <p style="text-align:center">document</p> 
                    </td>
                    <td>
                        <asp:Label runat="server" ID="documentName"></asp:Label>
                        <input id="Button1" type="button" runat="server" value="Browse" onclick="downLoad_Click(this)"/>
                    </td>
                </tr>
            </table>--%>
            <table class="table table-condensed table-bordered">
                <tr>
                    <td class="td-label" style="vertical-align:middle;">
                        <span style="float:left">&nbsp;</span> 
                        <p style="text-align:center">Follow-Up Action</p> 
                    </td>
                    <td class="td-content" colspan="7">
                        <asp:Label runat="server" ID="read_FollowUPAction" ></asp:Label>
                    </td>
                </tr>
            </table>
            </div>
            <div id="adddocument" style="display:block;">
                <%--<p style="font-weight:bold;">Add Document（<span style=" background:red">&nbsp;</span> must write）</p>--%>
                <attach:attachments id="Attachments1" ReadOnly="true" runat="server"></attach:attachments>
            </div>
            <div class="row">
             <ah:approvalhistory id="ApprovalHistory1" showaction="true" runat="server"></ah:approvalhistory>
            </div>
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

