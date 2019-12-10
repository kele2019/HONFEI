<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="NewRequest.aspx.cs" Inherits="Presale.Process.OutgoingDocumentSignature.NewRequest" %>

<%@ Register Src="/Modules/Ultimus.UWF.Form.ProcessControl/UserInfo.ascx" TagName="UserInfo" TagPrefix="ui" %>
<%@ Register Src="/Modules/Ultimus.UWF.Form.ProcessControl/ApprovalHistory.ascx" TagName="ApprovalHistory" TagPrefix="ah" %>
<%@ Register Src="/Modules/Ultimus.UWF.Form.ProcessControl/Attachments.ascx" TagName="Attachments" TagPrefix="attach" %>
<%@ Register Src="/Modules/Ultimus.UWF.Form.ProcessControl/ButtonList.ascx" TagName="ButtonList" TagPrefix="btn" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
        <meta http-equiv="X-UA-Compatible"  content="IE=EmulateIE8" />
    <title>Outgoing Document Signature Application Process</title>
    <script type="text/javascript" src="/Assets/js/common.js"></script>
    <script type="text/javascript">
        $(document).ready(function () {
           if ($("#hdIncident").val() != "") {
                $("#ButtonList1_btnSubmit").val("Submit");
                $("#ButtonList1_btnBack").hide();
                $("#ButtonList1_btnReject").show();
            }
            if ($("#hdUrgeTask").val() == "Yes") {
                $("#ReturnBackTask").show();
            }
        });

        function beforeSubmit() {
            $("#fld_UserApplicant").val($("#UserInfo1_fld_APPLICANT").text());
            var summary = "Outgoing Document Signature Application Process";
            $("#UserInfo1_fld_PROCESSSUMMARY").val(summary);
            return true;
        }
        function beforeSave() {
            var summary = "Outgoing Document Signature Application Process";
            $("#UserInfo1_fld_PROCESSSUMMARY").val(summary);
            $("#fld_TRSummary").val(summary);
            return true;
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
     <div id="myDiv" class="container">
            <div class="row">
                <ui:userinfo id="UserInfo1" processtitle="Outgoing Document Signature Application Process" processprefix="ODS" tablename="PROC_OutgoingDocumentSignature"
                    runat="server"  ></ui:userinfo>
              <%--  <asp:TextBox runat="server" ID="fld_AdminLogin" style="display:none;"></asp:TextBox>
                <asp:TextBox runat="server" ID="fld_DGMLogin" style="display:none;"></asp:TextBox>
                <asp:TextBox runat="server" ID="fld_GMLogin" style="display:none;"></asp:TextBox>--%>
            </div>
            <div class="row">
                <p style="font-weight:bold;" >Request require（"<span style=" background:red;">&nbsp;</span>" must write）</p>
                <table class="table table-condensed table-bordered">
                    <tr>
                        <td class="td-label">
                         <span style=" background:red;  height:30px; float:left;">&nbsp;</span>
                         <p style="text-align:center">Document name</p>
                        </td>
                        <td class="td-content" colspan="3" >
                            <asp:TextBox runat="server" ID="fld_DocumentName" Width="98%" MaxLength="100"  CssClass="validate[required]"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="td-label"> 
                       <span style=" background:red; margin-top:5px;  height:30px; float:left;">&nbsp;</span>
                       <p style="text-align:center">Document purpose</p>
                        </td>
                        <td class="td-content"  colspan="3" >
                            <asp:TextBox runat="server"  ID="fld_DocumentPurpose" MaxLength="100" CssClass="validate[required]"   Width="98%"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="td-label"> 
                       <span style=" background:red; margin-top:5px;  height:30px; float:left;">&nbsp;</span>
                       <p style="text-align:center">Where sent to</p>
                        </td>
                        <td class="td-content"  colspan="3" >
                            <asp:TextBox runat="server"  ID="fld_DocToDepartment" MaxLength="100" CssClass="validate[required]"   Width="98%"></asp:TextBox>
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

