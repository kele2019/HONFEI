<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AdminConfirmApproval.aspx.cs" Inherits="Presal.Process.UseOfCertificate.AdminConfirmApproval" %>

<%@ Register Src="/Modules/Ultimus.UWF.Form.ProcessControl/UserInfo.ascx" TagName="UserInfo" TagPrefix="ui" %>
<%@ Register Src="/Modules/Ultimus.UWF.Form.ProcessControl/ApprovalHistory.ascx" TagName="ApprovalHistory" TagPrefix="ah" %>
<%@ Register Src="/Modules/Ultimus.UWF.Form.ProcessControl/Attachments.ascx" TagName="Attachments" TagPrefix="attach" %>
<%@ Register Src="/Modules/Ultimus.UWF.Form.ProcessControl/ButtonList.ascx" TagName="ButtonList" TagPrefix="btn" %>

<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
        <meta http-equiv="X-UA-Compatible"  content="IE=EmulateIE8" />
    <title>Use of Certificate /Confidential Application</title>
    <script type="text/javascript" src="/Assets/js/common.js"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            $("#read_BorrowTime").text(showtime($("#read_BorrowTime").text()));
            $("#read_ReturnTime").text(showtime($("#read_ReturnTime").text()));
            if ($("#read_CertificateType").text() == "Copies") {
                $("#read_CertificateType").parent().attr("colspan", "3");
                $("#qty").show();
                $("#qtyValue").show();
            }
        });
        function showtime(obj) {
            var time = new Date(obj.replace(/-/g, "/"));
            var year = time.getFullYear();
            var month = time.getMonth() + 1;
            var date = time.getDate();
            return (year + "-" + month + "-" + date) == "NaN-NaN-NaN" ? "" : (year + "-" + month + "-" + date);
        }
        function submitPageReview(obj) {
            $("#ButtonList1_btnSubmit").click();
            return true;
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
     <div id="myDiv" class="container">
            <div class="row">
                <ui:userinfo id="UserInfo1" processtitle="Use of Certificate /Confidential Application" processprefix="UOC" tablename="PROC_UseOfCertificate" tablenamedetail="PROC_UseOfCertificate_DT"
                    runat="server"  ></ui:userinfo>
            </div>
            <div class="row">
                <p style="font-weight:bold;" >Request require</p>
                <table class="table table-condensed table-bordered">
                    <tr>
                                <td class="td-label">
                                 <span style="height:30px;float:left;">&nbsp;</span>
                                 <p style="text-align:center">Document Name</p>
                                </td>
                                <td class="td-content"  colspan="7" >
                                    <asp:Label runat="server"  ID="read_CertificateName"></asp:Label>
                                </td>
                            </tr>
                    <tr>
                        <td class="td-label"  style="width:100px">
                         <span style="height:30px; float:left;">&nbsp;</span>
                         <p style="text-align:center">Purpose</p>
                        </td>
                        <td class="td-content" colspan="7" >
                            <asp:Label runat="server" ID="read_Purpose"></asp:Label>
                        </td>
                    </tr>
                     <tr>
                                <td class="td-label">
                                 <span style="height:30px;float:left;">&nbsp;</span>
                                 <p style="text-align:center">Document Type</p>
                                </td>
                                <td class="td-content" colspan="7" >
                                    <asp:Label runat="server" class="certificateType" ID="read_CertificateType"></asp:Label>
                                </td>
                                <td class="td-label" style="display:none">
                                 <span style="height:30px;float:left;">&nbsp;</span>
                                 <p style="text-align:center">Qty</p>
                                </td>
                                <td class="td-content" colspan="3" style="display:none;">
                                    <asp:Label runat="server"  ID="read_QTY"></asp:Label>
                                </td>
                            </tr>
                    <tr>
                        <td class="td-label"> 
                       <span style="height:30px; float:left;">&nbsp;</span>
                       <p style="text-align:center">Borrow time</p>
                        </td>
                        <td class="td-content"  colspan="3" >
                            <asp:Label runat="server"  ID="read_BorrowTime"></asp:Label>
                        </td>
                        <td class="td-label"> 
                       <span style="height:30px; float:left;">&nbsp;</span>
                       <p style="text-align:center">Return time</p>
                        </td>
                        <td class="td-content"  colspan="3" >
                            <asp:Label runat="server"  ID="read_ReturnTime"></asp:Label>
                        </td>
                    </tr>
                </table>
                <table id="tabledetail" class="table table-condensed table-bordered">
                    
                    <%--<asp:Repeater runat="server" ID="fld_detail_PROC_UseOfCertificate_DT">
                        <ItemTemplate>
                        <tbody>--%>
                            
                           
                             <tr>
                                <td class="td-label">
                                <span style="height:30px; float:left;">&nbsp;</span>
                                <p style="text-align:center">Have Original Document</p>
                                </td>
                                <td class="td-content"colspan="7">
                                    <asp:Label runat="server" ID="read_YJ"></asp:Label>
                                </td>
                            </tr>
                       <%-- </tbody>
                        </ItemTemplate>
                    </asp:Repeater>--%>
                </table>
            </div>
            <div class="row" style="display:none;">
                <attach:attachments id="Attachments1" ReadOnly="true" runat="server"></attach:attachments>
            </div>
            <div class="row">
             <ah:approvalhistory id="ApprovalHistory1" showaction="true" runat="server"></ah:approvalhistory>
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


