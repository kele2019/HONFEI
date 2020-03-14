<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Approval.aspx.cs" Inherits="Presale.Process.FixedAssetApplication.Approval" %>

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
        function showTime(obj) {
            var time = new Date(obj.text().replace(/-/g,"/"));
            var year = time.getFullYear();
            var month = time.getMonth() + 1;
            var date = time.getDate();
            return (year + "-" + month + "-" + date) == "NaN-NaN-NaN" ? "" : (year + "-" + month + "-" + date);
        }
        $(document).ready(function () {
            var ApproveType = request("Type");
            if (ApproveType == "myapproval") {
                $("#fld_FATagNo").attr("disabled", "disabled");
                $("#fld_PurchaseTime").attr("disabled", "disabled");
                $("#fld_OriginalCost").attr("disabled", "disabled");
                $("#fld_NetBookValue").attr("disabled", "disabled");
            }
        });
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div id="myDiv" class="container">
            <div class="row">
                <ui:userinfo id="UserInfo1" processtitle="Fixed Assets Disposal Request Process" processprefix="FA" tablename="PROC_FixedAssets"
                    runat="server"  ></ui:userinfo>
                
            </div>
            <div class="row">
                <p style="font-weight:bold;">Request require("<span style=" background:red;">&nbsp;</span>" must write） </p>
                <table class="table table-condensed table-bordered">
                  
                    <tr>
                        <td class="td-label">
                        <p style="text-align:center">FA Name</p>
                        </td>
                        <td class="td-content">
                            <asp:Label runat="server" ID="read_FAName"   Width="95%"  style="display:block;"></asp:Label>
                        </td>
                          <td class="td-label">
                         <span style=" background:red;  height:30px; float:left;">&nbsp;</span>
                        <p style="text-align:center">FA Tag No.</p>
                        </td>
                        <td class="td-content">
                            <asp:TextBox runat="server" ID="fld_FATagNo"  CssClass="validate[required]" Width="95%"   style="display:block;"></asp:TextBox>
                        </td>
                            
                           
                    </tr>
                    <tr>
                        <td class="td-label"> 
                        <p style="text-align:center;">FA Model</p>
                        </td>
                        <td class="td-content" >
                            <asp:Label runat="server" ID="read_FAModel"  Width="95%"  style="display:block;"></asp:Label>
                        </td>
                        <td class="td-label" > 
                       <p style="text-align:center">Manufacturer</p>
                        </td>
                        <td class="td-content"    >
                            <asp:Label runat="server" ID="read_Manufacturer"  Width="95%"   style="display:block;"></asp:Label>
                        </td>
                    </tr>
                    
                     <tr>
                        <td class="td-label"> 
                       <span style=" background:red;  height:30px; float:left;">&nbsp;</span>
                        <p style="text-align:center;">Date of Purchase</p>
                        </td>
                        <td class="td-content" >
                            <asp:TextBox runat="server" ID="fld_PurchaseTime" CssClass="validate[required]"  onclick="WdatePicker({readOnly:false,dateFmt: 'yyyy-MM-dd'})"  Width="95%"  style="display:block;"></asp:TextBox>
                        </td>
                        <td class="td-label" > 
                       <p style="text-align:center">Serial No.</p>
                        </td>
                        <td class="td-content"    >
                            <asp:Label runat="server" ID="read_SerialNo"   style="display:block;"></asp:Label>
                        </td>
                    </tr>
                    
                     <tr>
                        <td class="td-label"> 
                       <span style=" background:red;  height:30px; float:left;">&nbsp;</span>
                        <p style="text-align:center;">Original Cost</p>
                        </td>
                        <td class="td-content" >
                            <asp:TextBox runat="server" ID="fld_OriginalCost"  Width="95%" CssClass="validate[required]"   style="display:block;"></asp:TextBox>
                        </td>
                        <td class="td-label" > 
                       <span style=" background:red;  height:30px; float:left;">&nbsp;</span>
                       <p style="text-align:center">Net Book Value</p>
                        </td>
                        <td class="td-content" >
                            <asp:TextBox runat="server" ID="fld_NetBookValue"  Width="95%"  CssClass="validate[required]" style="display:block;"></asp:TextBox>
                        </td>
                    </tr>

                       <tr>
                        <td class="td-label"> 
                        <p style="text-align:center;">Disposal Method</p>
                        </td>
                        <td class="td-content"  colspan="3">
                            <asp:Label runat="server" ID="read_DisposalMethod"  Width="95%"  style="display:block;"></asp:Label>
                        </td>
                      </tr>

                    <tr>
                        <td class="td-label" style="vertical-align:middle;">
                        <p style="text-align:center">Assets status, disposal reasons and disposal plan  </p>
                        </td>
                        <td class="td-content" colspan="3" >
                            <asp:TextBox ID="read_AssetsComments" ReadOnly="true" TextMode="MultiLine" Rows="5" runat="server" Width="98%" CssClass="validate[required]"></asp:TextBox>
                        </td>
                    </tr>
                      <tr>
                        <td class="td-label" style="vertical-align:middle;">
                        <p style="text-align:center">Expected disposal income / expenses   </p>
                        </td>
                        <td class="td-content" colspan="3" >
                            <asp:Label ID="read_ExpectedExpens"  runat="server" Width="98%"  ></asp:Label>
                        </td>
                    </tr>
                      <tr>
                        <td class="td-label" style="vertical-align:middle;">
                        <p style="text-align:center">Remark</p>
                        </td>
                        <td class="td-content" colspan="3" >
                            <asp:TextBox ID="read_RequestRemark" ReadOnly="true" TextMode="MultiLine" Rows="5" runat="server" Width="98%"  ></asp:TextBox>
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



