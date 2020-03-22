<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="NewRequest.aspx.cs" Inherits="Presale.Process.FixedAssetApplication.NewRequest" %>

<%@ Register Src="/Modules/Ultimus.UWF.Form.ProcessControl/UserInfo.ascx" TagName="UserInfo" TagPrefix="ui" %>
<%@ Register Src="/Modules/Ultimus.UWF.Form.ProcessControl/ApprovalHistory.ascx" TagName="ApprovalHistory" TagPrefix="ah" %>
<%@ Register Src="/Modules/Ultimus.UWF.Form.ProcessControl/Attachments.ascx" TagName="Attachments" TagPrefix="attach" %>
<%@ Register Src="/Modules/Ultimus.UWF.Form.ProcessControl/ButtonList.ascx" TagName="ButtonList" TagPrefix="btn" %>

<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
        <meta http-equiv="X-UA-Compatible"  content="IE=EmulateIE8" />
    <title>The Fixed Assets Disposal Application</title>
    <script type="text/javascript" src="/Assets/js/common.js"></script>  
    <script type="text/javascript">
        function beforeSubmit() {

            var summary = "Fixed Assets Disposal Request Process";
            $("#UserInfo1_fld_PROCESSSUMMARY").val(summary);
            return true;
        }
        
        function showTime(obj) {
            var time = new Date(obj.replace(/-/g,"/"));
            var year = time.getFullYear();
            var month = time.getMonth() + 1;
            var date = time.getDate();
            return (year + "-" + month + "-" + date) == "NaN-NaN-NaN" ? "" : (year + "-" + month + "-" + date);
        }
        $(document).ready(function () {
            if ($("#hdUrgeTask").val() == "Yes") {
                $("#ReturnBackTask").show();
            }
            if ($("#hdIncident").val() != "") {
                $("#ButtonList1_btnSubmit").val("Submit");
                $("#ButtonList1_btnBack").hide();
                if($("#hdIncident").val()>0)
                $("#ButtonList1_btnReject").show();
            }
        });
       
        function beforeSave() {
            var summary = "Fixed Assets Disposal Request Process";
            $("#UserInfo1_fld_PROCESSSUMMARY").val(summary);
            $("#fld_TRSummary").val(summary);
            return true;
        }
        function openITEMNO(obj) {
            var digStr = "dialogHeight:600px;dialogWidth:850px;";
            var ReturnValue = window.showModalDialog("./Item.aspx", "", digStr);
            if (ReturnValue != null) {
                var purchaseNo = eval("(" + ReturnValue + ")");
                var ItemCode = purchaseNo[0].ItemCodeValue;
                var ItemName = purchaseNo[1].ItemNameValue;
                $(obj).val(ItemCode);
                $("#fld_FAModel").val(ItemName);
               // $(obj).parent().next().children().val(ItemName);
            }
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
     <div id="myDiv" class="container">
            <div class="row">
                <ui:userinfo id="UserInfo1" processtitle="Fixed Assets Disposal Request Process" processprefix="FA" tablename="PROC_FixedAssets"
                    runat="server"  ></ui:userinfo>
               <%--<asp:TextBox runat="server" ID="fld_ApplicantUserName" style="display:none;"></asp:TextBox>
                <asp:TextBox runat="server" ID="fld_DepartmentManager" style="display:none;"></asp:TextBox>--%>
                   <%--  <asp:TextBox runat="server" ID="fld_DGMLogin" style="display:none;"></asp:TextBox>
                <asp:TextBox runat="server" ID="fld_GMLogin" style="display:none;"></asp:TextBox>
                <asp:TextBox runat="server" ID="fld_deptLogin" style="display:none;"></asp:TextBox>
                <asp:TextBox runat="server" ID="fld_ApplierLogin" style="display:none;"></asp:TextBox>
                <asp:TextBox runat="server" ID="fld_ITMLogin" style="display:none;"></asp:TextBox>
                <asp:TextBox runat="server" ID="fld_ITHelpLogin" style="display:none;"></asp:TextBox>
                <asp:TextBox runat="server" ID="fld_AdminLogin" style="display:none;"></asp:TextBox>--%>
            </div>
            <div class="row">
                <p style="font-weight:bold;">Request require("<span style=" background:red;">&nbsp;</span>" must write） </p>
                <table class="table table-condensed table-bordered">
                  
                    <tr>
                        <td class="td-label">
                         <span style=" background:red;  height:30px; float:left;">&nbsp;</span>
                        <p style="text-align:center">FA Name</p>
                        </td>
                        <td class="td-content">
                            <asp:TextBox runat="server" ID="fld_FAName"  CssClass="validate[required]" Width="95%"  style="display:block;" onclick="openITEMNO(this)"></asp:TextBox>
                        </td>
                          <td class="td-label">
                        <p style="text-align:center">FA Tag No.</p>
                        </td>
                        <td class="td-content">
                            <asp:Label runat="server" ID="read_FATagNo"   Width="95%"   style="display:block;"></asp:Label>
                        </td>
                            
                           
                    </tr>
                    <tr>
                        <td class="td-label"> 
                       <span style=" background:red;  height:30px; float:left;">&nbsp;</span>
                        <p style="text-align:center;">FA Model</p>
                        </td>
                        <td class="td-content" >
                            <asp:TextBox runat="server" ID="fld_FAModel"  Width="95%"  style="display:block;"></asp:TextBox>
                        </td>
                        <td class="td-label" > 
                       <span style=" background:red;  height:30px; float:left;">&nbsp;</span>
                       <p style="text-align:center">Manufacturer</p>
                        </td>
                        <td class="td-content"    >
                            <asp:TextBox runat="server" ID="fld_Manufacturer"  Width="95%"  CssClass="validate[required]" style="display:block;"></asp:TextBox>
                        </td>
                    </tr>
                    
                     <tr>
                        <td class="td-label"> 
                        <p style="text-align:center;">Date of Purchase</p>
                        </td>
                        <td class="td-content" >
                            <asp:Label runat="server" ID="read_PurchaseTime"    style="display:block;"></asp:Label>
                        </td>
                        <td class="td-label" > 
                       <span style=" background:red;  height:30px; float:left;">&nbsp;</span>
                       <p style="text-align:center">Serial No.</p>
                        </td>
                        <td class="td-content"    >
                            <asp:TextBox runat="server" ID="fld_SerialNo"  Width="95%"  CssClass="validate[required]" style="display:block;"></asp:TextBox>
                        </td>
                    </tr>
                    
                     <tr>
                        <td class="td-label"> 
                        <p style="text-align:center;">Original Cost</p>
                        </td>
                        <td class="td-content" >
                            <asp:Label runat="server" ID="read_OriginalCost"  Width="95%"  style="display:block;"></asp:Label>
                        </td>
                        <td class="td-label" > 
                       
                       <p style="text-align:center">Net Book Value</p>
                        </td>
                        <td class="td-content" >
                            <asp:Label runat="server" ID="read_NetBookValue"  Width="95%"    style="display:block;"></asp:Label>
                        </td>
                    </tr>

                       <tr>
                        <td class="td-label"> 
                       <span style=" background:red;  height:30px; float:left;">&nbsp;</span>
                        <p style="text-align:center;">Disposal Method</p>
                        </td>
                        <td class="td-content"  colspan="3">
                            <%--<asp:TextBox runat="server" ID="fld_DisposalMethod"  Width="95%"  style="display:block;"></asp:TextBox>--%>
                            <asp:DropDownList runat="server" ID="fld_DisposalMethod" Width="35%"  >
                            <asp:ListItem Value="Disposal">Disposal</asp:ListItem>
                            <asp:ListItem Value="Sales">Sales</asp:ListItem>
                            <asp:ListItem Value="Investment">Investment</asp:ListItem>
                            <asp:ListItem Value="Others">Others</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                      </tr>

                    <tr>
                        <td class="td-label" style="vertical-align:middle;">
                         <span style=" background:red;  height:30px; float:left;">&nbsp;</span>
                        <p style="text-align:center">Assets status, disposal reasons and disposal plan  </p>
                        </td>
                        <td class="td-content" colspan="3" >
                            <asp:TextBox ID="fld_AssetsComments" TextMode="MultiLine" Rows="5" runat="server" Width="98%" CssClass="validate[required]"></asp:TextBox>
                        </td>
                    </tr>
                      <tr>
                        <td class="td-label" style="vertical-align:middle;">
                         <span style=" background:red;  height:30px; float:left;">&nbsp;</span>
                        <p style="text-align:center">Expected disposal income / expenses   </p>
                        </td>
                        <td class="td-content" colspan="3" >
                            <asp:TextBox ID="fld_ExpectedExpens"  runat="server" Width="98%" CssClass="validate[required]"></asp:TextBox>
                        </td>
                    </tr>
                      <tr>
                        <td class="td-label" style="vertical-align:middle;">
                        <p style="text-align:center">Remark</p>
                        </td>
                        <td class="td-content" colspan="3" >
                            <asp:TextBox ID="fld_RequestRemark" TextMode="MultiLine" Rows="5" runat="server" Width="98%"></asp:TextBox>
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


