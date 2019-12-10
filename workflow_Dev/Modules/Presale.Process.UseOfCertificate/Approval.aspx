<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Approval.aspx.cs" Inherits="Presal.Process.UseOfCertificate.Approval" %>

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
            if ($("#read_CertificateType").text() == "Copies") {<a href="Approval.aspx">Approval.aspx</a>
                $("#read_CertificateType").parent().attr("colspan", "3");
                $("#qty").show();
                $("#qtyValue").show();
                $("#returnlabel1").hide();
                $("#returnlabel2").hide();
                $("#read_ReturnTime").hide();
            }

            var BorrowTime = $("#read_BorrowTime").text();
            $("#read_BorrowTime").text(dateChange(BorrowTime));
            var ReturnTime = $("#read_ReturnTime").text();
            $("#read_ReturnTime").text(dateChange(ReturnTime));
            //            var year = str.split('')[0] + str.split('')[1] + str.split('')[2] + str.split('')[3];
            //            var month = str.split('')[5] + str.split('')[6];
            ////            var day = str.split('')[08] + str.split('')[9];
            //            var day = str.split('')[08];
            //            alert(year + "-" + month + "-" + day);
        });
        function dateChange(strOrg) {
            var time = new Date(strOrg.replace(/-/g, "/"));
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
                <ui:userinfo id="UserInfo1" processtitle="Use of Certificate /Confidential Application" processprefix="UOC" tablename="PROC_UseOfCertificate"
                    runat="server"  ></ui:userinfo>
            </div>
            <asp:TextBox runat="server" ID="fld_UserPost" style="display:none;"></asp:TextBox>
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
                        <td class="td-label">
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
                                <td class="td-label" id="qty" style="display:none">
                                 <span style="height:30px;float:left;">&nbsp;</span>
                                 <p style="text-align:center">Qty</p>
                                </td>
                                <td class="td-content" id="qtyValue" colspan="3" style="display:none;">
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
                       <span style="height:30px; float:left;" id="returnlabel1">&nbsp;</span>
                       <p style="text-align:center" id="returnlabel2">Return time</p>
                        </td>
                        <td class="td-content"  colspan="3" >
                            <asp:Label runat="server"  ID="read_ReturnTime"></asp:Label>
                        </td>
                    </tr>
                </table>
               <%-- <table id="tabledetail" class="table table-condensed table-bordered">--%>
                    
                   <%-- <asp:Repeater runat="server" ID="fld_detail_PROC_UseOfCertificate_DT">
                        <ItemTemplate>--%>
                        
                       <%-- </ItemTemplate>
                    </asp:Repeater>--%>
                <%--</table>--%>
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


