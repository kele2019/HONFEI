<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="NewRequest.aspx.cs" Inherits="Presale.Process.Key.NewRequest" %>

<%@ Register Src="/Modules/Ultimus.UWF.Form.ProcessControl/UserInfo.ascx" TagName="UserInfo" TagPrefix="ui" %>
<%@ Register Src="/Modules/Ultimus.UWF.Form.ProcessControl/ApprovalHistory.ascx" TagName="ApprovalHistory" TagPrefix="ah" %>
<%@ Register Src="/Modules/Ultimus.UWF.Form.ProcessControl/Attachments.ascx" TagName="Attachments" TagPrefix="attach" %>
<%@ Register Src="/Modules/Ultimus.UWF.Form.ProcessControl/ButtonList.ascx" TagName="ButtonList" TagPrefix="btn" %>

<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta http-equiv="X-UA-Compatible"  content="IE=EmulateIE8" />
    <title>Key Application Process</title>
    <script type="text/javascript" src="/Assets/js/common.js"></script>  
    <script type="text/javascript">
        $(document).ready(function () {
            var value = $("#fld_Access").val();
            var access = value.split(",");
            for (var i = 0; i < access.length; i++) {
                if (access[i] == "main door") {
                    $("#access1").attr("checked", true);
                }
                if (access[i] == "Conference room Zhou") {
                    $("#access2").attr("checked", true);
                }
                if (access[i] == "IT server room") {
                    $("#access3").attr("checked", true);
                }
                if (access[i] == "Eng.lab") {
                    $("#access4").attr("checked", true);
                }
            }

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
            var subttoal = "";
            if ($("#access1").attr("checked")) {
                var itemValue = $("#access1").val();
                subttoal += "," + itemValue;
            }
            if ($("#access2").attr("checked")) {
                var itemValue = $("#access2").val();
                subttoal += "," + itemValue;
            }
            if ($("#access3").attr("checked")) {
                var itemValue = $("#access3").val();
                subttoal += "," + itemValue;
                $("#fld_ITAccess").val("IT server room");
            }
            if ($("#access4").attr("checked")) {
                var itemValue = $("#access4").val();
                subttoal += "," + itemValue;
                $("#fld_EngAccess").val("Eng.lab");
            }
            if (subttoal == "") {
                alert("Please select the access.");
                return false;
            }
            if (subttoal.substr(0, 1) == ',') subttoal = subttoal.substr(1);
            $("#access4").next().val(subttoal);

            var summary = "Key Application Process";
            $("#UserInfo1_fld_PROCESSSUMMARY").val(summary);
            return true;
        }
        function beforeSave() {
            var summary = "Key Application Process";
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
                <ui:userinfo id="UserInfo1" processtitle="Key Application Process" processprefix="KEY" tablename="PROC_Key"
                    runat="server"  ></ui:userinfo>
             <%--   <asp:TextBox runat="server" ID="fld_AdminLogin" style="display:none;"></asp:TextBox>
                <asp:TextBox runat="server" ID="fld_DGMLogin" style="display:none;"></asp:TextBox>
                <asp:TextBox runat="server" ID="fld_GMLogin" style="display:none;"></asp:TextBox>--%>
            </div>
            <div class="row">
                <p style="font-weight:bold;">Request require（"<span style=" background:red;">&nbsp;</span>" must write）</p>
                <table class="table table-condensed table-bordered">
                    <tr>
                        <td class="td-label" style="vertical-align:middle;">
                         <span style=" background:red;  height:30px; float:left;">&nbsp;</span>
                         <p style="text-align:center">Reason</p>
                        </td>
                        <td class="td-content" colspan="7" >
                            <asp:TextBox runat="server" ID="fld_Reason" TextMode="MultiLine" Rows="3" MaxLength="100" Width="98%"  CssClass="validate[required]"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="td-label"> 
                       <span style=" background:red;  height:30px; float:left;">&nbsp;</span>
                        <p style="text-align:center">Access</p>
                        </td>
                        <td class="td-content"  colspan="7" >
                            <input id="access1" type="checkbox" value="main door" />main door &nbsp;&nbsp;&nbsp;
                            <input id="access2" type="checkbox" value="Conference room Zhou" />Conference room Zhou&nbsp;&nbsp;&nbsp;
                            <input id="access3" type="checkbox" value="IT server room" />IT server room&nbsp;&nbsp;&nbsp;
                            <input id="access4" type="checkbox" value="Eng.lab" />Eng.lab
                            <asp:TextBox runat="server" ID="fld_Access" style="display:none"/>
                            <asp:TextBox runat="server" ID="fld_ITAccess" style="display:none;"></asp:TextBox>
                            <asp:TextBox runat="server" ID="fld_EngAccess" style="display:none;"></asp:TextBox>
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


