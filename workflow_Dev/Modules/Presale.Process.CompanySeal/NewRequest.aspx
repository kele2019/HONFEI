<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="NewRequest.aspx.cs" Inherits="Presale.Process.CompanySeal.NewRequest" %>

<%@ Register Src="/Modules/Ultimus.UWF.Form.ProcessControl/UserInfo.ascx" TagName="UserInfo" TagPrefix="ui" %>
<%@ Register Src="/Modules/Ultimus.UWF.Form.ProcessControl/ApprovalHistory.ascx" TagName="ApprovalHistory" TagPrefix="ah" %>
<%@ Register Src="/Modules/Ultimus.UWF.Form.ProcessControl/Attachments.ascx" TagName="Attachments" TagPrefix="attach" %>
<%@ Register Src="/Modules/Ultimus.UWF.Form.ProcessControl/ButtonList.ascx" TagName="ButtonList" TagPrefix="btn" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta http-equiv="X-UA-Compatible"  content="IE=EmulateIE8" />
    <title>Company Seal Request Process</title>
    <script type="text/javascript" src="/Assets/js/common.js"></script>
    <script type="text/javascript">
        function getSummary() {
            var IDHeader = "CS";
            var IDBody = $("#UserInfo1_fld_REQUESTDATE").text().replace("/","").replace("-","");
        }
        function beforeSubmit() {
//            $("#fld_UserApplicant").val($("#UserInfo1_fld_APPLICANT").text());
            $("#fld_SealName").val($("#sealName option:selected").text());
            if ($("#sealName option:selected").text() == "鸿翔飞控技术（西安）有限责任公司 Company") {
                $("#fld_SealNameUser").val($("#fld_AdminLogin").val());
            }
            if ($("#sealName option:selected").text() == "鸿翔飞控技术（西安）有限责任公司 合同专用章 Contract") {
                $("#fld_SealNameUser").val($("#fld_ControllerLogin").val());
            }
            if ($("#sealName option:selected").text() == "鸿翔飞控技术（西安）有限责任公司 人力资源部 HR") {
                $("#fld_SealNameUser").val($("#fld_HRLogin").val());
            }
            var summary = "Company Sale Request Process";
            $("#UserInfo1_fld_PROCESSSUMMARY").val(summary);
            return true;
        }
        $(document).ready(function () {
            $("#sealName").val($("#fld_SealName").val());
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
            var summary = "Company Sale Request Process";
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
                <ui:userinfo id="UserInfo1" processtitle="Company Seal Request Process" processprefix="CS" tablename="PROC_CompanySeal"
                    runat="server"  ></ui:userinfo>
                    <asp:TextBox runat="server" ID="fld_AdminLogin" style="display:none;"></asp:TextBox>
                    <asp:TextBox runat="server" ID="fld_ControllerLogin" style="display:none;"></asp:TextBox>
                    <asp:TextBox runat="server" ID="fld_HRLogin" style="display:none;"></asp:TextBox>
                    <asp:TextBox runat="server" ID="fld_GMLogin" style="display:none;"></asp:TextBox>
            </div>
            <asp:TextBox runat="server" ID="fld_UserPost" style="display:none;"></asp:TextBox>
            <div class="row">
                <p style="font-weight:bold;">Request require（"<span style=" background:red;">&nbsp;</span>" must write） </p>
                <table class="table table-condensed table-bordered">
                    <tr>
                        <td class="td-label">
                         <span style=" background:red;  height:30px; float:left;">&nbsp;</span>
                         <p style="text-align:center">Quantity</p>
                        </td>
                        <td class="td-content" colspan="3" >
                        <asp:TextBox runat="server" ID="fld_Quantity" MaxLength="5" Width="95%"  CssClass="validate[required]"></asp:TextBox>
                        </td>
                        <td class="td-label">
                         <span style=" background:red;  height:30px; float:left;">&nbsp;</span>
                         <p style="text-align:center">SealName</p>
                        </td>
                        <td class="td-content" colspan="3" >
                        <asp:DropDownList runat="server" ID="sealName" Width="99%"></asp:DropDownList>
                        <asp:TextBox runat="server" ID="fld_SealName" Width="95%" style="display:none;"></asp:TextBox>
                        <asp:TextBox runat="server" ID="fld_SealNameUser" style="display:none"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="td-label" style="vertical-align:middle"> 
                       <span style=" background:red; margin-top:5px;  height:30px; float:left;">&nbsp;</span>
                       <p style="text-align:center">Purpose</p>
                        </td>
                        <td class="td-content"  colspan="7" >
                            <asp:TextBox runat="server"  ID="fld_Purpose" MaxLength="100" Rows="3" TextMode="MultiLine"  CssClass="validate[required]"   Width="98%"></asp:TextBox>
                        </td>
                    </tr>
                </table>
            </div>
            <div class="row" style="display:block;">
                <attach:attachments id="Attachments1" runat="server"></attach:attachments>
            </div>
            <div class="row">
             <ah:approvalhistory id="ApprovalHistory1" showaction="true" runat="server"></ah:approvalhistory>
            </div>
              <div id="ReturnBackTask" style="display:none; text-align:center">
        <asp:Button runat="server" ID="btnRevoke"  Visible="false" CssClass="btn" Text="Revoke" 
                onclick="btnRevoke_Click" />
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


