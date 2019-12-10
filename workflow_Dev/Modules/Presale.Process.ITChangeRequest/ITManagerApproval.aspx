<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ITManagerApproval.aspx.cs" Inherits="Presale.Process.ITChangeRequest.ITManagerApproval" %>

<%@ Register Src="/Modules/Ultimus.UWF.Form.ProcessControl/UserInfo.ascx" TagName="UserInfo" TagPrefix="ui" %>
<%@ Register Src="/Modules/Ultimus.UWF.Form.ProcessControl/ApprovalHistory.ascx" TagName="ApprovalHistory" TagPrefix="ah" %>
<%@ Register Src="/Modules/Ultimus.UWF.Form.ProcessControl/Attachments.ascx" TagName="Attachments" TagPrefix="attach" %>
<%@ Register Src="/Modules/Ultimus.UWF.Form.ProcessControl/ButtonList.ascx" TagName="ButtonList" TagPrefix="btn" %>

<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta http-equiv="X-UA-Compatible"  content="IE=EmulateIE8" />
    <title>IT Change Request</title>
    <script type="text/javascript" src="/Assets/js/common.js"></script>  
    <script type="text/javascript">
        function beforeSave() {
            var summary = "IT Change Request";
            $("#UserInfo1_fld_PROCESSSUMMARY").val(summary);
            $("#fld_TRSummary").val(summary);
            return true;
        }
        $(document).ready(function () {
            if ($("#read_SeverityImpact").text() == "High") {
                $("#radioButton0").attr("checked", true);
            }
            if ($("#read_SeverityImpact").text() == "Medium") {
                $("#radioButton1").attr("checked", true);
            }
            if ($("#read_SeverityImpact").text() == "Low") {
                $("#radioButton2").attr("checked", true);
            }

            if ($("#hdIncident").val() != "") {
                $("#ButtonList1_btnSubmit").val("Submit");
                $("#ButtonList1_btnBack").hide();
                $("#ButtonList1_btnReject").show();
            }
        });
    </script>
</head>
<body>
    <form id="form1" runat="server">
     <div id="myDiv" class="container">
            <div class="row">
                <ui:userinfo id="UserInfo1" processtitle="IT Change Request" processprefix="ITCR" tablename="PROC_ITChange"
                    runat="server"  ></ui:userinfo>
            </div>
            <div class="row">
                <p style="font-weight:bold;">Section 1</p>
                <table class="table table-condensed table-bordered">
                    <tr>
                        <td class="td-label">
                         <span style="height:30px; float:left;">&nbsp;</span>
                         <p style="text-align:center">Change Request Name</p>

                        </td>
                        <td class="td-content" colspan="3" >
                            <asp:Label runat="server" ID="read_CRName"></asp:Label>
                        </td>
                        <td class="td-label"> 
                       <span style="height:30px; float:left;">&nbsp;</span>
                        <p style="text-align:center">CR</p>
                        </td>
                        <td class="td-content"  colspan="3" >
                            <asp:Label runat="server" ID="read_CR"></asp:Label>
                        </td>
                        <%--<td class="td-label">
                         <span style=" background:red;  height:30px; float:left;">&nbsp;</span>
                        <p style="text-align:center">Date</p>
                        </td>
                        <td class="td-content" colspan="3" >
                            <asp:TextBox runat="server"  ID="fld_Date" CssClass="validate[required]"   Width="95%" onclick="WdatePicker({readOnly:false,dateFmt: 'yyyy-MM-dd'})"></asp:TextBox>
                        </td>--%>
                    </tr>
                    <tr>
                        
                        <td class="td-label"> 
                       <span style="height:30px; float:left;">&nbsp;</span>
                       <p style="text-align:center">Request Name</p>

                        </td>
                        <td class="td-content"  colspan="3" >
                            <asp:Label runat="server" ID="read_RName"></asp:Label>
                        </td>
                        <%--<td class="td-label"> 
                       <span style=" background:red;  height:30px; float:left;">&nbsp;</span>
                        <p style="text-align:center">Phone</p>
                        </td>
                        <td class="td-content"  colspan="3" >
                            <asp:TextBox runat="server" ID="fld_Phone"  Width="95%"  CssClass="validate[required]"></asp:TextBox>
                        </td>--%>
                        <td class="td-label"> 
                       <span style="height:30px; float:left;">&nbsp;</span>
                       <p style="text-align:center">Authorizer Name</p>
                        </td>
                        <td class="td-content"  colspan="3" >
                            <asp:Label runat="server" ID="read_AName"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="td-label" style="vertical-align:middle;">
                         <span style="height:30px; float:left;">&nbsp;</span>
                       <p style="text-align:center">Description of Proposed Change</p>
                        </td>
                        <td class="td-content" colspan="7" >
                            <asp:Label ID="read_DOPC" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="td-label" style="vertical-align:middle;">
                         <span style="height:30px; float:left;">&nbsp;</span>
                       <p style="text-align:center">Root Cause of Proposed Change</p>
                        </td>
                        <td class="td-content" colspan="7" >
                            <asp:Label ID="read_RCOPC" runat="server"></asp:Label>
                        </td>
                    </tr>
                    </table>
                    <p style="font-weight:bold;">Section 2</p>
                    <table class="table table-condensed table-bordered">
                    <tr>
                        <td class="td-label">
                         <span style="height:30px; float:left;">&nbsp;</span>
                        <p style="text-align:center">Cost Impact($)</p>
                        </td>
                        <td class="td-content" colspan="3" >
                            <asp:Label runat="server" ID="read_CostImpact"></asp:Label>
                        </td>
                        <td class="td-label">
                         <span style="height:30px; float:left;">&nbsp;</span>
                        <p style="text-align:center">Duration Impact(days)</p>
                        </td>
                        <td class="td-content" colspan="3" >
                            <asp:Label runat="server" ID="read_DurationImpact"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="td-label" style="vertical-align:middle;">
                         <span style="height:30px; float:left;">&nbsp;</span>
                        <p style="text-align:center">Impact to other affected projects(including resources)</p>
                        </td>
                        <td class="td-content" colspan="7" >
                            <asp:Label runat="server" ID="read_ITOAP"></asp:Label>
                        </td>
                    </tr>
                    
                    <tr>
                        <td class="td-label" >
                         <span style="height:30px; float:left;">&nbsp;</span>
                       <p style="text-align:center">Scope Impact</p>
                        </td>
                        <td class="td-content" colspan="7" >
                            <asp:Label ID="read_ScopeImpact" runat="server" ></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="td-label">
                         <span style="height:30px; float:left;">&nbsp;</span>
                       <p style="text-align:center">Requirements Impact</p>
                        </td>
                        <td class="td-content" colspan="7" >
                            <asp:Label ID="read_RequirementsImpact" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="td-label" >
                         <span style="height:30px; float:left;">&nbsp;</span>
                      <p style="text-align:center">Design Impact</p>
                        </td>
                        <td class="td-content" colspan="7" >
                            <asp:Label ID="read_DesignImpact" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="td-label">
                         <span style="height:30px; float:left;">&nbsp;</span>
                       <p style="text-align:center">Code Impact</p>
                        </td>
                        <td class="td-content" colspan="7" >
                            <asp:Label ID="read_CodeImpact" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="td-label" style="vertical-align:middle;">
                         <span style="height:30px; float:left;">&nbsp;</span>
                      <p style="text-align:center">Baselined Documents Impacted</p>
                        </td>
                        <td class="td-content" colspan="7">
                            <asp:Label ID="read_BDImpacted" runat="server"></asp:Label>
                        </td>
                    </tr>
                    </table>
                    <p style="font-weight:bold;">Section 3("<span style=" background:red;">&nbsp;</span>" must write）</p>
                    <table class="table table-condensed table-bordered">
                    <tr>
                        <td class="td-label"  style="vertical-align:middle;">
                         <span style=" background:red;  height:30px; float:left;">&nbsp;</span>
                      <p style="text-align:center">IT team need (if we can get approval from JV leadership and Hon HGS team)</p>
                        </td>
                        <td class="td-content" colspan="7" >
                            <asp:TextBox ID="fld_ITTeamNeed" runat="server" TextMode="MultiLine" Rows="4" MaxLength="100" Width="98%" CssClass="validate[required]"></asp:TextBox>
                        </td>
                    </tr>
                   </table>
                   <table class="table table-condensed table-bordered">
                    <tr>
                        <td class="td-label">
                         <span style="height:30px; float:left;">&nbsp;</span>
                        <p style="text-align:center">Severity Impact</p>
                        </td>
                        <td class="td-content" colspan="7" >
                            <asp:CheckBox ID="radioButton0" runat="server" disabled="true"/>High &nbsp;&nbsp;
                            <asp:CheckBox ID="radioButton1" runat="server" disabled="true"/>Medium &nbsp;&nbsp;
                            <asp:CheckBox ID="radioButton2" runat="server" disabled="true"/>Low
                        </td>
                        <td style="display:none;">
                            <asp:Label runat="server" ID="read_SeverityImpact"></asp:Label>
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


