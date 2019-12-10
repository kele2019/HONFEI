<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Approval.aspx.cs" Inherits="Presale.Process.HongfeiFilghtControlDR.Approval" %>

<%@ Register Src="/Modules/Ultimus.UWF.Form.ProcessControl/UserInfo.ascx" TagName="UserInfo" TagPrefix="ui" %>
<%@ Register Src="/Modules/Ultimus.UWF.Form.ProcessControl/ApprovalHistory.ascx" TagName="ApprovalHistory" TagPrefix="ah" %>
<%@ Register Src="/Modules/Ultimus.UWF.Form.ProcessControl/Attachments.ascx" TagName="Attachments" TagPrefix="attach" %>
<%@ Register Src="/Modules/Ultimus.UWF.Form.ProcessControl/ButtonList.ascx" TagName="ButtonList" TagPrefix="btn" %>

<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta http-equiv="X-UA-Compatible"  content="IE=EmulateIE8" />
    <title>IT Recovery Request Process</title>
    <script type="text/javascript" src="/Assets/js/common.js"></script>  
    
</head>
<body>
    <form id="form1" runat="server">
     <div id="myDiv" class="container">
            <div class="row">
                <ui:userinfo id="UserInfo1" processtitle="IT Recovery Request Process" processprefix="ITRR" tablename="PROC_ITRecovery"
                    runat="server"  ></ui:userinfo>
                <asp:TextBox runat="server" ID="fld_UserName" style="display:none;"></asp:TextBox>
            </div>
            <div class="row">
                <p style="font-weight:bold;">Section 1</p>
                <table class="table table-condensed table-bordered">
                   <%-- <tr>
                        <td class="td-label">
                         <span style="height:30px; float:left;">&nbsp;</span>
                          <p style="text-align:center">BR</p>
                        </td>
                        <td class="td-content" colspan="3" >
                            <asp:Label runat="server" ID="read_BR"></asp:Label>
                        </td>
                        <td class="td-label">
                         <span style="height:30px; float:left;">&nbsp;</span>
                          <p style="text-align:center">Phone</p>

                        </td>
                        <td class="td-content" colspan="3" >
                            <asp:Label runat="server" ID="read_Phone"></asp:Label>
                        </td>
                    </tr>--%>
                    <tr>
                        <td class="td-label" style="vertical-align:middle;"> 
                       <span style="height:30px; float:left;">&nbsp;</span>
                         <p style="text-align:center">Reason for the recovery</p>
                        </td>
                        <td class="td-content"  colspan="7" >
                            <asp:Label runat="server" ID="read_RFTB"></asp:Label>
                        </td>
                    </tr>
                    </table>
                    <p style="font-weight:bold;">Section 2（"<span style=" background:red;">&nbsp;</span>" must write）</p>
                    <table class="table table-condensed table-bordered">
                    <tr>
                        <td class="td-label">
                         <span style=" background:red;  height:30px; float:left;">&nbsp;</span>
                         <p style="text-align:center">System name</p>
                        </td>
                        <td class="td-content" colspan="3" >
                            <asp:TextBox runat="server" ID="fld_SHostName" Width="95%" MaxLength="40" CssClass="validate[required]"></asp:TextBox>
                        </td>
                        <td class="td-label">
                         <span style=" background:red;  height:30px; float:left;">&nbsp;</span>
                         <p style="text-align:center">System Classification</p>
                        </td>
                        <td class="td-content" colspan="3" >
                            <asp:TextBox runat="server" ID="fld_SClassification" Width="95%" MaxLength="40" CssClass="validate[required]"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="td-label">
                         <span style=" background:red;  height:30px; float:left;">&nbsp;</span>
                         <p style="text-align:center">Host Name/target host</p>
                        </td>
                        <td class="td-content" colspan="7" >
                            <asp:TextBox runat="server" ID="fld_HNTH" Width="98%" MaxLength="90" CssClass="validate[required]"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="td-label">
                         <span style=" background:red;  height:30px; float:left;">&nbsp;</span>
                         <p style="text-align:center">Restore from (Data/Time)</p>
                        
                        </td>
                        <td class="td-content" colspan="7" >
                            <asp:TextBox runat="server" ID="fld_Time" Width="98%" MaxLength="90" CssClass="validate[required]"></asp:TextBox>
                        <%-- &nbsp;&nbsp;From&nbsp;&nbsp;
                            <asp:TextBox runat="server"  ID="fld_RestoreStartDate" CssClass="validate[required]"   Width="30%" onclick="WdatePicker({readOnly:false,dateFmt: 'yyyy-MM-dd',maxDate:'#F{$dp.$D(\'fld_RestoreEndDate\')}'})"></asp:TextBox>
                         &nbsp;&nbsp;To&nbsp;&nbsp;
                            <asp:TextBox runat="server"  ID="fld_RestoreEndDate" CssClass="validate[required]"   Width="30%" onclick="WdatePicker({readOnly:false,dateFmt: 'yyyy-MM-dd',minDate:'#F{$dp.$D(\'fld_RestoreStartDate\')}'})"></asp:TextBox>--%>
                        </td>
                    </tr>
                    <tr>
                        <td class="td-label" style="vertical-align:middle;">
                         <span style=" background:red;  height:30px; float:left;">&nbsp;</span>
                         <p style="text-align:center">Database/File system details (type/version/Approx Size)</p>
                        
                        </td>
                        <td class="td-content" colspan="7" >
                            <asp:TextBox ID="fld_DFSD" runat="server" TextMode="MultiLine" Rows="3" MaxLength="90" Width="98%" CssClass="validate[required]" ></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="td-label" style="vertical-align:middle;">
                         <span style=" background:red;  height:30px; float:left;">&nbsp;</span>
                         <p style="text-align:center">Exclusions(If backup should exclude any directories/data)</p>
                       
                        </td>
                        <td class="td-content" colspan="7" >
                            <asp:TextBox ID="fld_Exclusions" runat="server" TextMode="MultiLine" Rows="3" MaxLength="90" Width="98%" CssClass="validate[required]"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="td-label" style="vertical-align:middle;">
                         <span style="height:30px; float:left;">&nbsp;</span>
                         <p style="text-align:center">Recommendation</p>
                       
                        </td>
                        <td class="td-content" colspan="7" >
                            <asp:TextBox ID="fld_Recommendation" runat="server" TextMode="MultiLine" Rows="3" MaxLength="90" Width="98%"></asp:TextBox>
                        </td>
                    </tr>
                 </table>
                    <%--<p style="font-weight:bold;">Section 2</p>
                    <table class="table table-condensed table-bordered">
                    <tr>
                        <td class="td-label">
                         <span style=" height:30px; float:left;">&nbsp;</span>
                         <p style="text-align:center">System name</p>
                        
                        </td>
                        <td class="td-content" colspan="3" >
                            <asp:Label runat="server" ID="read_SHostName"></asp:Label>
                        </td>
                        <td class="td-label">
                         <span style="height:30px; float:left;">&nbsp;</span>
                         <p style="text-align:center">System Classification</p>

                        </td>
                        <td class="td-content" colspan="3" >
                            <asp:Label runat="server" ID="read_SClassification"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="td-label" >
                         <span style="height:30px; float:left;">&nbsp;</span>
                         <p style="text-align:center">Host Name/target host</p>
                        
                        </td>
                        <td class="td-content" colspan="7" >
                            <asp:Label runat="server" ID="read_HNTH"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="td-label">
                         <span style="height:30px; float:left;">&nbsp;</span>
                        <p style="text-align:center">Restore from (Data/Time)</p>
                        </td>
                        <td class="td-content" colspan="7" >
                            &nbsp;&nbsp;From &nbsp;&nbsp;
                            <asp:Label ID="read_RestoreStartDate" runat="server"></asp:Label>
                            &nbsp;&nbsp;To &nbsp;&nbsp;
                            <asp:Label ID="read_RestoreEndDate" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="td-label" style="vertical-align:middle;">
                         <span style="height:30px; float:left;">&nbsp;</span>
                        <p style="text-align:center">Database/File system details (type/version/Approx Size)</p>
                        </td>
                        <td class="td-content" colspan="7" >
                            <asp:Label ID="read_DFSD" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="td-label" style="vertical-align:middle;">
                         <span style="height:30px; float:left;">&nbsp;</span>
                       <p style="text-align:center">Exclusions(If backup should exclude any directories/data)</p>
                        </td>
                        <td class="td-content" colspan="7" >
                            <asp:Label ID="read_Exclusions" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="td-label" style="vertical-align:middle;">
                         <span style="height:30px; float:left;">&nbsp;</span>
                       <p style="text-align:center">Recommendation</p>
                        </td>
                        <td class="td-content" colspan="7" >
                            <asp:Label ID="read_Recommendation" runat="server"></asp:Label>
                        </td>
                    </tr>
                 </table>--%>
            </div>
            <div class="row" style="display:none;">
                <attach:attachments id="Attachments1" ReadOnly="true" runat="server"></attach:attachments>
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


