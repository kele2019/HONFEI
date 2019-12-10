<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="NewRequest.aspx.cs" Inherits="Presale.Process.WebsiteAccess.NewRequest" %>

<%@ Register Src="/Modules/Ultimus.UWF.Form.ProcessControl/UserInfo.ascx" TagName="UserInfo" TagPrefix="ui" %>
<%@ Register Src="/Modules/Ultimus.UWF.Form.ProcessControl/ApprovalHistory.ascx" TagName="ApprovalHistory" TagPrefix="ah" %>
<%@ Register Src="/Modules/Ultimus.UWF.Form.ProcessControl/Attachments.ascx" TagName="Attachments" TagPrefix="attach" %>
<%@ Register Src="/Modules/Ultimus.UWF.Form.ProcessControl/ButtonList.ascx" TagName="ButtonList" TagPrefix="btn" %>

<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
        <meta http-equiv="X-UA-Compatible"  content="IE=EmulateIE8" />
    <title>Website Access Request Process</title>
    <script type="text/javascript" src="/Assets/js/common.js"></script>  
    <script type="text/javascript">
        function beforeSubmit() {
            var summary = "Website Access Request Process";
            $("#UserInfo1_fld_PROCESSSUMMARY").val(summary);
            return true;
        }
        function beforeSave() {
            var summary = "Website Access Request Process";
            $("#UserInfo1_fld_PROCESSSUMMARY").val(summary);
            $("#fld_TRSummary").val(summary);
            return true;
        }
        function showtime(obj) {
            var time = new Date(obj.replace(/-/g,"/"));
            var year = time.getFullYear();
            var month = time.getMonth() + 1;
            var date = time.getDate();
            return (year + "-" + month + "-" + date) == "NaN-NaN-NaN" ? "" : (year + "-" + month + "-" + date);
        }
        $(document).ready(function () {
            $("#fld_ApplicantUser").val($("#UserInfo1_fld_APPLICANT").text());
            if ($("#fld_time").val() == "long time") {
                $("#longTime").attr("checked", true);
            }
            if ($("#fld_time").val() == "short time") {
                $("#ShortTime").attr("checked", true);
            }
            $("#fld_StartDate").val(showtime($("#fld_StartDate").val()));
            $("#fld_EndDate").val(showtime($("#fld_EndDate").val()));
            if ($("#hdIncident").val() != "") {
                $("#ButtonList1_btnSubmit").val("Submit");
                $("#ButtonList1_btnBack").hide();
                $("#ButtonList1_btnReject").show();
            }
            if ($("#hdUrgeTask").val() == "Yes") {
                $("#ReturnBackTask").show();
            }
        });
        function getButtonCheck(obj,index) {
            if (index == "1") {
                if ($(obj).attr("checked")) {
                    $("#fld_time").val("long time");
                    $("#ShortTime").attr("checked", false);
                    $("#fld_StartDate").val("");
                    $("#fld_EndDate").val("");
                }
            }
            if (index == "2") {
                if ($(obj).attr("checked")) {
                    $("#fld_time").val("short time");
                    $("#longTime").attr("checked", false);
                    $("#fld_StartDate").focus();
                }
            }
        }
        function shortTimeClick(index) {
            if (index == "1") {
                $("#ShortTime").attr("checked", "checked");
            }
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
     <div id="myDiv" class="container">
            <div class="row">
                <ui:userinfo id="UserInfo1" processtitle="Website Access Request Process" processprefix="ITWA" tablename="PROC_WebsiteAccess"
                    runat="server"  ></ui:userinfo>
                <asp:TextBox runat="server" ID="fld_DepartmentManager" style="display:none;"></asp:TextBox>
                <asp:TextBox runat="server" ID="fld_ApplicantUser" style="display:none;"></asp:TextBox>
              <%--  <asp:TextBox runat="server" ID="fld_deptLogin" style="display:none;"></asp:TextBox>
                <asp:TextBox runat="server" ID="fld_ApplierLogin" style="display:none;"></asp:TextBox>
                <asp:TextBox runat="server" ID="fld_ITMLogin" style="display:none;"></asp:TextBox>
                <asp:TextBox runat="server" ID="fld_ITHelpLogin" style="display:none;"></asp:TextBox>--%>
            </div>
            <div class="row">
                <p style="font-weight:bold;">Request require("<span style=" background:red;">&nbsp;</span>" must write）</p>
                <table class="table table-condensed table-bordered">
                   
                    <tr>
                        <td class="td-label">
                         <span style=" background:red;  height:30px; float:left;">&nbsp;</span>
                        <p style="text-align:center;">Application domain name</p>
                        </td>
                        <td class="td-content" colspan="7" >
                            <asp:TextBox runat="server" ID="fld_ADName" Width="98%"  CssClass="validate[required]"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="td-label">
                         <span style=" background:red;  height:30px; float:left;">&nbsp;</span>
                        <p style="text-align:center;">Reason</p>
                        </td>
                        <td class="td-content" colspan="7" >
                            <asp:TextBox runat="server" ID="fld_Reason" Width="98%"  CssClass="validate[required]"></asp:TextBox>
                        </td>
                    </tr>
                    <%--<tr>
                        <td class="td-label">
                         <span style=" background:red;  height:30px; float:left;">&nbsp;</span>
                        <p style="text-align:center;">Requirement</p>
                        </td>
                        <td class="td-content" colspan="7" >
                            <asp:TextBox runat="server" ID="fld_Requirement" Width="98%"  CssClass="validate[required]"></asp:TextBox>
                        </td>
                    </tr>--%>
                    <tr>
                        <td class="td-label" style="vertical-align:middle;">
                         <span style=" background:red;  height:30px; float:left;">&nbsp;</span>
                        <p style="text-align:center;">Use time</p>
                        </td>
                        <td class="td-content" colspan="7" >
                            <asp:CheckBox runat="server" ID="longTime" onclick="getButtonCheck(this,1)"/>&nbsp;&nbsp;long time
                            <hr style="width:100%"/>
                            <div style="width:100%;">
                                <asp:CheckBox runat="server" ID="ShortTime" onclick="getButtonCheck(this,2)"/>
                                &nbsp;&nbsp;From&nbsp;&nbsp;
                                <span><asp:TextBox runat="server"  ID="fld_StartDate" onfocus="shortTimeClick(1)"  Width="25%"  onclick="WdatePicker({readOnly:false,dateFmt: 'yyyy-MM-dd',maxDate:'#F{$dp.$D(\'fld_EndDate\')}'})"></asp:TextBox></span>
                                &nbsp;&nbsp;To &nbsp;&nbsp;
                                <asp:TextBox runat="server"  ID="fld_EndDate" onfocus="shortTimeClick(1)" Width="25%" onclick="WdatePicker({readOnly:false,dateFmt: 'yyyy-MM-dd',minDate:'#F{$dp.$D(\'fld_StartDate\')}'})"></asp:TextBox>
                            </div>
                            <asp:TextBox runat="server" ID="fld_time" style="display:none;"></asp:TextBox>
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


