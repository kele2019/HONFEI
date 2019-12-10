<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ITAdminApproval.aspx.cs" Inherits="Presale.Process.ITChangeRequest.ITAdminApproval" %>

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
        function beforeSubmit() {
            if ($("#radioButton0").attr("checked")) {
                $("#fld_SeverityImpact").val("High");
            }
            if ($("#radioButton1").attr("checked")) {
                $("#fld_SeverityImpact").val("Medium");
            }
            if ($("#radioButton2").attr("checked")) {
                $("#fld_SeverityImpact").val("Low");
            }
            if ($("#fld_SeverityImpact").val() == "") {
                alert("Please select the severity impact");
                return false;
            }
            $("#fld_ITAdmin").val($("#ITAdmin").val());
            var summary = "IT Change Request";
            $("#UserInfo1_fld_PROCESSSUMMARY").val(summary);
            return true;
        }
        function setValue(value) {
            $("#fld_ITAdmin").val(value);
        }
        $(document).ready(function () {
            if ($("#ITAdmin").val() == $("#read_ITM").text()) {
                $("#section3").show();
            }
            if ($("#fld_SeverityImpact").val() == "High") {
                $("#radioButton0").attr("checked", true);
            }
            if ($("#fld_SeverityImpact").val() == "Medium") {
                $("#radioButton1").attr("checked", true);
            }
            if ($("#fld_SeverityImpact").val() == "Low") {
                $("#radioButton2").attr("checked", true);
            }
            if ($("#hdIncident").val() != "") {
                $("#ButtonList1_btnSubmit").val("Submit");
                $("#ButtonList1_btnBack").hide();
                $("#ButtonList1_btnReject").show();
            }
            //            var items = $("#read_RIP").text().split(",");
            //            for (var i = 0; i < items.length; i++) {
            //                if (items[i] == "Requirements") {
            //                    $("#RIPOrg1").attr("checked", "checked");
            //                }
            //                if (items[i] == "Build/Test") {
            //                    $("#RIPOrg2").attr("checked", "checked");
            //                }
            //                if (items[i] == "Design") {
            //                    $("#RIPOrg3").attr("checked", "checked");
            //                }
            //                if (items[i] == "Accept/Implement") {
            //                    $("#RIPOrg4").attr("checked", "checked");
            //                }
            //                if (items[i] == "Warranty") {
            //                    $("#RIPOrg5").attr("checked", "checked");
            //                }
            //            }
        });
        function getButtonCheck(obj, index) {
            if (index == "0") {
                if ($(obj).attr("checked")) {
                    $("#radioButton1").attr("checked", false);
                    $("#radioButton2").attr("checked", false);
                }
            }
            if (index == "1") {
                if ($(obj).attr("checked")) {
                    $("#radioButton0").attr("checked", false);
                    $("#radioButton2").attr("checked", false);
                }
            }
            if (index == "2") {
                if ($(obj).attr("checked")) {
                    $("#radioButton0").attr("checked", false);
                    $("#radioButton1").attr("checked", false);
                }
            }
        }      
    </script>
</head>
<body>
    <form id="form1" runat="server">
     <div id="myDiv" class="container">
            <div class="row">
                <ui:userinfo id="UserInfo1" processtitle="IT Change Request" processprefix="ITCR" tablename="PROC_ITChange"
                    runat="server"  ></ui:userinfo>
                <asp:TextBox runat="server" ID="ITAdmin" style="display:none;"></asp:TextBox>
                <asp:TextBox runat="server" ID="fld_ITAdmin" style="display:none;"></asp:TextBox>
                <asp:Label runat="server" ID="read_ITM" style="display:none;"></asp:Label>
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
                       <span style=" height:30px; float:left;">&nbsp;</span>
                         <p style="text-align:center">CR</p>
                        
                        </td>
                        <td class="td-content"  colspan="3" >
                            <asp:Label runat="server" ID="read_CR"></asp:Label>
                        </td>
                        <%--<td class="td-label">
                         <span style="height:30px; float:left;">&nbsp;</span>
                         <p style="text-align:center">Date</p>
                        
                        </td>
                        <td class="td-content" colspan="3" >
                            <asp:Label runat="server"  ID="read_Date" ></asp:Label>
                        </td>--%>
                    </tr>
                    <tr>
                        
                        <td class="td-label"> 
                       <span style=" height:30px; float:left;">&nbsp;</span>
                         <p style="text-align:center">Request Name</p>
                       
                        </td>
                        <td class="td-content"  colspan="3" >
                            <asp:Label runat="server" ID="read_RName"></asp:Label>
                        </td>
                        <%--<td class="td-label"> 
                       <span style="height:30px; float:left;">&nbsp;</span>
                         <p style="text-align:center">Phone</p>
                        
                        </td>
                        <td class="td-content"  colspan="3" >
                            <asp:Label runat="server" ID="read_Phone" ></asp:Label>
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
                   <%-- <p style="font-weight:bold;">Section 2</p>
                    <table class="table table-condensed table-bordered">
                    <tr>
                        <td class="td-label">
                         <span style="height:30px; float:left;">&nbsp;</span>
                         <p style="text-align:center">Requested in Phase</p>
                        
                        </td>
                        <td class="td-content" colspan="7" >
                                <asp:CheckBox runat="server" ID="RIPOrg1" disabled="diabled" />Requirements &nbsp;&nbsp;
                                <asp:CheckBox runat="server" ID="RIPOrg2" disabled="disabled" />Design &nbsp;&nbsp;
                                <asp:CheckBox runat="server" ID="RIPOrg3" disabled="disabled" />Build/Test &nbsp;&nbsp;
                                <asp:CheckBox runat="server" ID="RIPOrg4" disabled="disabled" />Accept/Implement &nbsp;&nbsp;
                                <asp:CheckBox runat="server" ID="RIPOrg5" disabled="disabled"/>Warranty
                                <asp:Label runat="server" ID="read_RIP" style="display:none"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="td-label">
                         <span style="height:30px; float:left;">&nbsp;</span>
                         <p style="text-align:center">Cost Impact($)</p>
                        
                        </td>
                        <td class="td-content" colspan="3" >
                            <asp:Label runat="server" ID="read_CostImpact" ></asp:Label>
                        </td>
                        <td class="td-label">
                         <span style=" height:30px; float:left;">&nbsp;</span>
                         <p style="text-align:center">Duration Impact(days)</p>
                        
                        </td>
                        <td class="td-content" colspan="3" >
                            <asp:Label runat="server" ID="read_DurationImpact"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="td-label" style="vertical-align:middle;">
                         <span style="  height:30px; float:left;">&nbsp;</span>
                         <p style="text-align:center">Impact to other affected projects(including resources)</p>
                        
                        </td>
                        <td class="td-content" colspan="7" >
                            <asp:Label runat="server" ID="read_ITOAP" ></asp:Label>
                        </td>
                    </tr>
                    
                    <tr>
                        <td class="td-label" >
                         <span style=" height:30px; float:left;">&nbsp;</span>
                         <p style="text-align:center">Scope Impact</p>
                       
                        </td>
                        <td class="td-content" colspan="7" >
                            <asp:Label ID="read_ScopeImpact" runat="server" ></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="td-label">
                         <span style="    height:30px; float:left;">&nbsp;</span>
                         <p style="text-align:center">Requirements Impact</p>
                       
                        </td>
                        <td class="td-content" colspan="7" >
                            <asp:Label ID="read_RequirementsImpact" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="td-label" >
                         <span style="  height:30px; float:left;">&nbsp;</span>
                         <p style="text-align:center">Design Impact</p>
                       
                        </td>
                        <td class="td-content" colspan="7" >
                            <asp:Label ID="read_DesignImpact" runat="server" ></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="td-label">
                         <span style="  height:30px; float:left;">&nbsp;</span>
                         <p style="text-align:center">Code Impact</p>
                       
                        </td>
                        <td class="td-content" colspan="7" >
                            <asp:Label ID="read_CodeImpact" runat="server" ></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="td-label" style="vertical-align:middle;">
                         <span style="   height:30px; float:left;">&nbsp;</span>
                         <p style="text-align:center">Baselined Documents Impacted</p>
                      
                        </td>
                        <td class="td-content" colspan="7" >
                            <asp:Label ID="read_BDImpacted" runat="server"></asp:Label>
                        </td>
                    </tr>
                    </table>--%>
                    <p style="font-weight:bold;">Section 2("<span style=" background:red;">&nbsp;</span>" must write） </p>
                    <table class="table table-condensed table-bordered">
                    <%--<tr>
                        <td class="td-label">
                         <span style=" background:red;  height:30px; float:left;">&nbsp;</span>
                        <p style="text-align:center">Requested in Phase</p>
                        </td>
                        <td class="td-content" colspan="7" >
                                <asp:CheckBox runat="server" ID="RIPOrg1" value="Requirements"/>Requirements &nbsp;&nbsp;
                                <asp:CheckBox runat="server" ID="RIPOrg2" value="Design" />Design &nbsp;&nbsp;
                                <asp:CheckBox runat="server" ID="RIPOrg3" value="Build/Test" />Build/Test &nbsp;&nbsp;
                                <asp:CheckBox runat="server" ID="RIPOrg4" value="Accept/Implement" />Accept/Implement &nbsp;&nbsp;
                                <asp:CheckBox runat="server" ID="RIPOrg5" value="Warranty" />Warranty
                                <asp:TextBox runat="server" ID="fld_RIP" style="display:none"></asp:TextBox>
                        </td>
                    </tr>--%>
                    <tr>
                        <td class="td-label">
                         <span style=" background:red;  height:30px; float:left;">&nbsp;</span>
                        <p style="text-align:center">Cost Impact($)</p>
                        </td>
                        <td class="td-content" colspan="3" >
                            <asp:TextBox runat="server" ID="fld_CostImpact" Width="95%" MaxLength="40" CssClass="validate[required]"></asp:TextBox>
                        </td>
                        <td class="td-label">
                         <span style=" background:red;  height:30px; float:left;">&nbsp;</span>
                        <p style="text-align:center">Duration Impact(days)</p>
                        </td>
                        <td class="td-content" colspan="3" >
                            <asp:TextBox runat="server" ID="fld_DurationImpact" Width="95%" MaxLength="40" CssClass="validate[required]"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="td-label" style="vertical-align:middle;">
                         <span style=" background:red;  height:30px; float:left;">&nbsp;</span>
                        <p style="text-align:center">Impact to other affected projects(including resources)</p>
                        </td>
                        <td class="td-content" colspan="7" >
                            <asp:TextBox runat="server" ID="fld_ITOAP" Width="98%" MaxLength="90" CssClass="validate[required]"></asp:TextBox>
                        </td>
                    </tr>
                    
                    <tr>
                        <td class="td-label" >
                         <span style=" background:red;  height:30px; float:left;">&nbsp;</span>
                       <p style="text-align:center">Scope Impact</p>
                        </td>
                        <td class="td-content" colspan="7" >
                            <asp:TextBox ID="fld_ScopeImpact" runat="server" Width="98%" MaxLength="90" CssClass="validate[required]" ></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="td-label">
                         <span style=" background:red;  height:30px; float:left;">&nbsp;</span>
                       <p style="text-align:center">Requirements Impact</p>
                        </td>
                        <td class="td-content" colspan="7" >
                            <asp:TextBox ID="fld_RequirementsImpact" runat="server" Width="98%" MaxLength="90" CssClass="validate[required]" ></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="td-label" >
                         <span style=" background:red;  height:30px; float:left;">&nbsp;</span>
                      <p style="text-align:center">Design Impact</p>
                        </td>
                        <td class="td-content" colspan="7" >
                            <asp:TextBox ID="fld_DesignImpact" runat="server" Width="98%" MaxLength="90" CssClass="validate[required]" ></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="td-label">
                         <span style=" background:red;  height:30px; float:left;">&nbsp;</span>
                       <p style="text-align:center">Code Impact</p>
                        </td>
                        <td class="td-content" colspan="7" >
                            <asp:TextBox ID="fld_CodeImpact" runat="server" Width="98%" MaxLength="90" CssClass="validate[required]" ></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="td-label" style="vertical-align:middle;">
                         <span style=" background:red;  height:30px; float:left;">&nbsp;</span>
                      <p style="text-align:center">Baselined Documents Impacted</p>
                        </td>
                        <td class="td-content" colspan="7">
                            <asp:TextBox ID="fld_BDImpacted" runat="server" Width="98%" MaxLength="90" CssClass="validate[required]"></asp:TextBox>
                        </td>
                    </tr>
                    </table>
                    <table class="table table-condensed table-bordered">
                    <tr>
                        <td class="td-label">
                         <span style=" background:red;  height:30px; float:left;">&nbsp;</span>
                        <p style="text-align:center">Severity Impact</p>
                        </td>
                        <td class="td-content" colspan="7" >
                            <asp:CheckBox ID="radioButton0" runat="server" onclick="getButtonCheck(this,0)"/>High &nbsp;&nbsp;
                            <asp:CheckBox ID="radioButton1" runat="server" onclick="getButtonCheck(this,1)"/>Medium &nbsp;&nbsp;
                            <asp:CheckBox ID="radioButton2" runat="server" onclick="getButtonCheck(this,2)"/>Low
                        </td>
                        <td style="display:none;">
                            <asp:TextBox runat="server" ID="fld_SeverityImpact"></asp:TextBox>
                        </td>
                    </tr>
                 </table>
                    <div style="display:none;" id="section3">
                    <p style="font-weight:bold;">Section 3("<span style=" background:red;">&nbsp;</span>" must write)</p>
                    <table class="table table-condensed table-bordered">
                    <tr>
                        <td class="td-label"  style="vertical-align:middle;">
                         <span style=" background:red;  height:30px; float:left;">&nbsp;</span>
                         <p style="text-align:center">IT team need (if we can get approval from JV leadership and Hon HGS team)</p>
                      
                        </td>
                        <td class="td-content" colspan="7" >
                            <asp:TextBox ID="fld_ITTeamNeed" runat="server" TextMode="MultiLine" Rows="3" MaxLength="100" Width="98%" CssClass="validate[required]"></asp:TextBox>
                        </td>
                    </tr>
                    <%--<tr>
                        <td class="td-label">
                         <span style=" background:red;  height:30px; float:left;">&nbsp;</span>
                         <p style="text-align:center">Severity Impact</p>
                        
                        </td>
                        <td class="td-content" colspan="7" >
                            <asp:CheckBox ID="radioButton0" runat="server" onclick="getButtonCheck(this,0)"/>High &nbsp;&nbsp;
                            <asp:CheckBox ID="radioButton1" runat="server" onclick="getButtonCheck(this,1)"/>Medium &nbsp;&nbsp;
                            <asp:CheckBox ID="radioButton2" runat="server" onclick="getButtonCheck(this,2)"/>Low
                        </td>
                        <td style="display:none;">
                            <asp:TextBox runat="server" ID="fld_SeverityImpact"></asp:TextBox>
                        </td>
                    </tr>--%>
                 </table>
                 </div>
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


