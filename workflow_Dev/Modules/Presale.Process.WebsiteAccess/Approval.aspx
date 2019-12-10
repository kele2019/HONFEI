<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Approval.aspx.cs" Inherits="Presale.Process.WebsiteAccess.Approval" %>

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
        $(document).ready(function () {
            if ($("#read_time").text() == "long time") {
                //                $("#longTime").attr("checked", true);
                //                $("#read_StartDate").text("  ");
                //                $("#read_EndDate").text("  ");
                $("#longtime").show();
            }
            if ($("#read_time").text() == "short time") {
                $("#shorttime").show();
                $("#read_StartDate").text(showtime($("#read_StartDate")));
                $("#read_EndDate").text(showtime($("#read_EndDate")));
            }
        });
        function showtime(obj) {
            var time = new Date(obj.text().replace(/-/g,"/"));
            var year = time.getFullYear();
            var month = time.getMonth() + 1;
            var date = time.getDate();
            return (year + "-" + month + "-" + date) == "NaN-NaN-NaN" ? "" : (year + "-" + month + "-" + date);
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
     <div id="myDiv" class="container">
            <div class="row">
                <ui:userinfo id="UserInfo1" processtitle="Website Access Request Process" processprefix="ITWA" tablename="PROC_WebsiteAccess"
                    runat="server"  ></ui:userinfo>
                <asp:TextBox runat="server" ID="fld_UserName" style="display:none;"></asp:TextBox>
            </div>
            <div class="row">
                <p style="font-weight:bold;">Request require</p>
                <table class="table table-condensed table-bordered">
                    <tr>
                        <td class="td-label">
                         <span style="height:30px; float:left;">&nbsp;</span>
                        <p style="text-align:center;">Application domain name</p>
                        </td>
                        <td class="td-content" colspan="7" >
                            <asp:Label runat="server" ID="read_ADName"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="td-label">
                         <span style="height:30px; float:left;">&nbsp;</span>
                        <p style="text-align:center;">Reason</p>
                        </td>
                        <td class="td-content" colspan="7" >
                            <asp:Label runat="server" ID="read_Reason"></asp:Label>
                        </td>
                    </tr>
                    <%--<tr>
                        <td class="td-label">
                         <span style="height:30px; float:left;">&nbsp;</span>
                        <p style="text-align:center;">Requirement</p>
                        
                        </td>
                        <td class="td-content" colspan="7" >
                            <asp:Label runat="server" ID="read_Requirement"></asp:Label>
                        </td>
                    </tr>--%>
                    <tr>
                        <td class="td-label" style="vertical-align:middle">
                         <span style="height:30px; float:left;">&nbsp;</span>
                        <p style="text-align:center;">Use time</p>
                        </td>
                        <td class="td-content" colspan="7" >
                            <%--<asp:CheckBox runat="server" ID="longTime" disabled="true"/>&nbsp;&nbsp;long time--%>
                            <p style="display:none;" id="longtime">long time</p>
                            <%--<hr style="width:100%"/>--%>
                            <div style="width:100%;display:none;" id="shorttime">
                                <%--<asp:CheckBox runat="server" ID="ShortTime" disabled="true" />--%>
                                &nbsp;&nbsp;From&nbsp;&nbsp;
                                <asp:Label runat="server"  width="15%" ID="read_StartDate" ></asp:Label>
                                &nbsp;&nbsp;To &nbsp;&nbsp;
                                <asp:Label runat="server"  width="15%"  ID="read_EndDate" ></asp:Label>
                            </div>
                            <asp:Label runat="server" ID="read_time" style="display:none;"/>
                        </td>
                    </tr>
                 </table>
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


