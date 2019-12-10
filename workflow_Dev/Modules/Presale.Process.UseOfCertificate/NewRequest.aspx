<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="NewRequest.aspx.cs" Inherits="Presale.Process.UseOfCertificate.NewRequest" %>

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
        function showtime(obj) {
            var time = new Date(obj.replace(/-/g, "/"));
            var year = time.getFullYear();
            var month = time.getMonth() + 1;
            var date = time.getDate();
            return (year + "-" + month + "-" + date) == "NaN-NaN-NaN" ? "" : (year + "-" + month + "-" + date);
        }
        $(document).ready(function () {
            $("#fld_BorrowTime").val(showtime($("#fld_BorrowTime").val()));
            $("#fld_ReturnTime").val(showtime($("#fld_ReturnTime").val()));
            if ($("#fld_CertificateType").val() == "Originals") {
                $("#radioOrigianls").attr("checked", true);
            }
            if ($("#fld_CertificateType").val() == "Copies") {
                $("#radioCopies").attr("checked", true);
                $("#qty").prev().attr("colspan", "3");
                $("#qty").show();
                $("#qtyValue").show();
                $("#returnlabel1").hide();
                $("#returnlabel2").hide();
                $("#fld_ReturnTime").hide();
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
        function getButtonCheck(obj, index) {
            if (index == "1") {
                $("#qty").prev().attr("colspan", "7");
                $("#qty").hide();
                $("#qtyValue").hide();
                $("#returnlabel1").show();
                $("#returnlabel2").show();
                $("#fld_ReturnTime").show();
            }
            if (index == "2") {
                $("#qty").prev().attr("colspan","3");
                $("#qty").show();
                $("#qtyValue").show();
                $("#returnlabel1").hide();
                $("#returnlabel2").hide();
                $("#fld_ReturnTime").hide();
            }
        }
        function beforeSubmit() {
            $("#fld_UserApplicant").val($("#UserInfo1_fld_APPLICANT").text());
                
            if ($("#radioOrigianls").attr("checked")) {
                $("#fld_CertificateType").val("Originals");
            }
            if ($("#radioCopies").attr("checked")) {
                $("#fld_CertificateType").val("Copies");
            }
            var summary = "Use of Certificate /Confidential Application";
            $("#UserInfo1_fld_PROCESSSUMMARY").val(summary);
            return true;
        }
        function beforeSave() {
            var summary = "Use of Certificate /Confidential Application";
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
                <ui:userinfo id="UserInfo1" processtitle="Use of Certificate /Confidential Application" processprefix="UOC" tablename="PROC_UseOfCertificate"
                    runat="server"  ></ui:userinfo>
              <%--  <asp:TextBox runat="server" ID="fld_AdminLogin" style="display:none;"></asp:TextBox>
                <asp:TextBox runat="server" ID="fld_DGMLogin" style="display:none;"></asp:TextBox>
                <asp:TextBox runat="server" ID="fld_GMLogin" style="display:none;"></asp:TextBox>--%>
            </div>
            <asp:TextBox runat="server" ID="fld_UserPost" style="display:none;"></asp:TextBox>
            <div class="row">
                <p style="font-weight:bold;" >Request require（"<span style=" background:red;">&nbsp;</span>" must write） </p>
                <table class="table table-condensed table-bordered">
                    <tr>
                                <td class="td-label">
                                    <span style="background:red;float:left;height:30px;">&nbsp;</span>
                                    <p style="text-align:center">Document Name</p>
                                </td>
                                <td class="td-content" colspan="7">
                                    <asp:TextBox runat="server" ID="fld_CertificateName" MaxLength="100" CssClass="validate[required]"   Width="98%"></asp:TextBox>
                                </td>
                                
                               <%-- <td class="td-content" rowspan="2" style="vertical-align:middle;" >
                                    <asp:Button ID="btnDelete" runat="server" Text="删除/Del" CssClass="btn" CommandName="del" ClientIDMode="Static" OnClientClick="return confirm('Confirm Del？')" />
                                </td>--%>
                            </tr>
                    <tr>
                        <td class="td-label">
                         <span style=" background:red;  height:30px; float:left;">&nbsp;</span>
                         <p style="text-align:center">Purpose</p>
                        </td>
                        <td class="td-content" colspan="7" >
                            <asp:TextBox runat="server" ID="fld_Purpose" Width="98%" MaxLength="100"  CssClass="validate[required]"></asp:TextBox>
                        </td>
                    </tr>
                     <tr>
                                <td class="td-label">
                                 <span style=" background:red; float:left;height:30px;">&nbsp;</span>
                                 <p style="text-align:center">Document Type</p>
                                </td>
                                <td class="td-content"colspan="7" >
                                    <asp:RadioButton runat="server" ID="radioOrigianls" checked="true" GroupName="certificateType" onclick="getButtonCheck(this,1)"/>Originals &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                    <asp:RadioButton runat="server" ID="radioCopies"  GroupName="certificateType" onclick="getButtonCheck(this,2)"/>Copies
                                    <asp:TextBox runat="server" ID="fld_CertificateType" style="display:none" ></asp:TextBox>
                                </td>
                                <td class="td-label" id="qty" style="display:none;">
                                 <span style=" background:red; float:left;height:30px;">&nbsp;</span>
                                 <p style="text-align:center">Qty</p>
                                </td>
                                <td class="td-content" id="qtyValue" style="display:none;" colspan="3">
                                    <asp:TextBox runat="server" Width="95%" ID="fld_QTY" MaxLength="5" CssClass="validate[required]"></asp:TextBox>
                                </td>
                            </tr>
                    <tr>
                        <td class="td-label"> 
                       <span style=" background:red;  height:30px; float:left;">&nbsp;</span>
                       <p style="text-align:center">Borrow time</p>
                        </td>
                        <td class="td-content"  colspan="3" >
                            <asp:TextBox runat="server"  ID="fld_BorrowTime" CssClass="validate[required]"   Width="95%" onclick="WdatePicker({readOnly:false,dateFmt: 'yyyy-MM-dd',maxDate:'#F{$dp.$D(\'fld_ReturnTime\')}'})"></asp:TextBox>
                        </td>
                        <td class="td-label"> 
                       <span style=" background:red;  height:30px; float:left;" id="returnlabel1">&nbsp;</span>
                       <p style="text-align:center" id="returnlabel2">Return time</p>
                        </td>
                        <td class="td-content"  colspan="3" >
                            <asp:TextBox runat="server"  ID="fld_ReturnTime" CssClass="validate[required]"   Width="95%" onclick="WdatePicker({readOnly:false,dateFmt: 'yyyy-MM-dd',minDate:'#F{$dp.$D(\'fld_BorrowTime\')}'})"></asp:TextBox>
                        </td>
                    </tr>
                </table>
               <%-- <table id="tabledetail" class="table table-condensed table-bordered">--%>
                    <%--<thead style="display:none;">
                        <tr>
                            <td  style="text-align:center; float:left;">
                                <asp:Button ID="btnAdd" runat="server" Text="新增/Add" CssClass="btn" CausesValidation="false" OnClick="btnAdd_Click"   />
                            </td>
                        </tr>
                    </thead>--%>
                    <%--<asp:Repeater runat="server" ID="fld_detail_PROC_UseOfCertificate_DT" OnItemCommand="fld_detail_PROC_UseOfCertificate_DT_ItemCommand" OnItemDataBound="fld_detail_PROC_UseOfCertificate_DT_ItemDataBound">
                        <ItemTemplate>
                           <div class="tabletbody">--%>

                            
                           
                           <%-- </div>
                        </ItemTemplate>
                    </asp:Repeater>--%>
                <%--</table>--%>
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


