<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="NewRequest.aspx.cs" Inherits="Presale.Process.HelpDeskRegistration.NewRequest" %>

<%@ Register Src="/Modules/Ultimus.UWF.Form.ProcessControl/UserInfo.ascx" TagName="UserInfo" TagPrefix="ui" %>
<%@ Register Src="/Modules/Ultimus.UWF.Form.ProcessControl/ApprovalHistory.ascx" TagName="ApprovalHistory" TagPrefix="ah" %>
<%@ Register Src="/Modules/Ultimus.UWF.Form.ProcessControl/Attachments.ascx" TagName="Attachments" TagPrefix="attach" %>
<%@ Register Src="/Modules/Ultimus.UWF.Form.ProcessControl/ButtonList.ascx" TagName="ButtonList" TagPrefix="btn" %>

<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta http-equiv="X-UA-Compatible"  content="IE=EmulateIE8" />
    <title>HelpDesk Registration</title>
    <script type="text/javascript" src="/Assets/js/common.js"></script> 
    <script type="text/javascript">
        function showTime(obj) {
            var time = new Date(obj.replace("-", "/"));
            var year = time.getFullYear();
            var month = time.getMonth() + 1;
            var date = time.getDate();
            var value = year + "-" + month + "-" + date
            return value == "NaN-NaN-NaN" ? " " : value;
        }
        function beforeSubmit() {
            $("#fld_QuestionType").val($("#questionType option:selected").text());
            var summary = "IT HelpDesk Registration Process";
            $("#UserInfo1_fld_PROCESSSUMMARY").val(summary);
            return true;
        }
        $(document).ready(function () {
            $("#fld_AppllicantUser").val($("#UserInfo1_fld_APPLICANT").text());
            $("#fld_ProcessingTime").val(showTime($("#fld_ProcessingTime").val()));
            $("#fld_EndTime").val(showTime($("#fld_EndTime").val()));
            if ($("#hdIncident").val() != "") {
                $("#ButtonList1_btnSubmit").val("Submit");
                $("#ButtonList1_btnBack").hide();
                $("#ButtonList1_btnReject").show();
            }
            if ($("#hdUrgeTask").val() == "Yes") {
                $("#ReturnBackTask").show();
            }
        });
        function beforeSave() {
            var summary = "IT HelpDesk Registration Process";
            $("#UserInfo1_fld_PROCESSSUMMARY").val(summary);
            $("#fld_TRSummary").val(summary);
            return true;
        }
        
        function questionType_onchange(obj) {
            $(obj).next().val($(obj).val());
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
     <div id="myDiv" class="container">
            <div class="row">
                <ui:userinfo id="UserInfo1" processtitle="IT HelpDesk Registration Process" processprefix="ITHD" tablename="PROC_HelpDeskRegistration"
                    runat="server"  ></ui:userinfo>
               <%-- <asp:TextBox runat="server" ID="fld_AppllicantUser" style="display:none;"></asp:TextBox>
                 <asp:TextBox runat="server" ID="fld_ApplierLogin" style="display:none;"></asp:TextBox>
                <asp:TextBox runat="server" ID="fld_ITMLogin" style="display:none;"></asp:TextBox>--%>
            </div>
            <div class="row">
                <p style="font-weight:bold;">Request require</p>
                <table class="table table-condensed table-bordered">
                    <tr>
                        <td class="td-label">
                             <span style="height:30px; float:left;">&nbsp;</span>
                            <p style="text-align:center;">Contact person</p>
                        </td>
                        <td class="td-content" colspan="3">
                            <asp:TextBox runat="server" ID="fld_ContactPerson" CssClass="validate[required]" Width="95%"></asp:TextBox>
                        </td>
                        <td class="td-label">
                             <span style="height:30px; float:left;">&nbsp;</span>
                            <p style="text-align:center;">Question type</p>
                        </td>
                        <td class="td-content" colspan="3">
                            <asp:DropDownList runat="server" id="questionType" Width="98%" onchange="questionType_onchange(this)"></asp:DropDownList>
                            <asp:TextBox runat="server" ID="fld_QuestionType" CssClass="validate[required]" style="display:none;"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="td-label" style="vertical-align:middle">
                             <span style="height:30px; float:left;">&nbsp;</span>
                            <p style="text-align:center;">Question describe</p>
                        </td>
                        <td class="td-content" colspan="7">
                            <asp:TextBox runat="server" ID="fld_Description" TextMode="MultiLine" Rows="3" MaxLength="100" CssClass="validate[required]" Width="98%"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="td-label">
                             <span style="height:30px; float:left;">&nbsp;</span>
                            <p style="text-align:center;">Deal with people</p>
                        </td>
                        <td class="td-content" colspan="3">
                            <asp:TextBox runat="server" ID="fld_DealWithPeople" CssClass="validate[required]" Width="95%"></asp:TextBox>
                        </td>
                        <td class="td-label">
                             <span style="height:30px; float:left;">&nbsp;</span>
                            <p style="text-align:center;">Processing time</p>
                        </td>
                        <td class="td-content" colspan="3">
                            <asp:TextBox runat="server" ID="fld_ProcessingTime" CssClass="validate[required]" Width="95%" onclick="WdatePicker({readOnly:false,dateFmt: 'yyyy-MM-dd'})"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="td-label">
                             <span style="height:30px; float:left;">&nbsp;</span>
                            <p style="text-align:center;">End time</p>
                        </td>
                        <td class="td-content" colspan="7">
                            <asp:TextBox runat="server" ID="fld_EndTime" CssClass="validate[required]" Width="98%" onclick="WdatePicker({readOnly:false,dateFmt: 'yyyy-MM-dd'})"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="td-label" style="vertical-align:middle">
                             <span style="height:30px; float:left;">&nbsp;</span>
                            <p style="text-align:center;">Result</p>
                        </td>
                        <td class="td-content" colspan="7">
                            <asp:TextBox runat="server" ID="fld_Result" TextMode="MultiLine" Rows="3" CssClass="validate[required]" Width="98%"></asp:TextBox>
                        </td>
                    </tr>
                </table>
            </div>
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



