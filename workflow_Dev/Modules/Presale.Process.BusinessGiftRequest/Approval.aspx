<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Approval.aspx.cs" Inherits="Presale.Process.BusinessGiftRequest.Approval" %>
<%@ Register Src="/Modules/Ultimus.UWF.Form.ProcessControl/UserInfo.ascx" TagName="UserInfo" TagPrefix="ui" %>
<%@ Register Src="/Modules/Ultimus.UWF.Form.ProcessControl/ApprovalHistory.ascx" TagName="ApprovalHistory" TagPrefix="ah" %>
<%@ Register Src="/Modules/Ultimus.UWF.Form.ProcessControl/Attachments.ascx" TagName="Attachments" TagPrefix="attach" %>
<%@ Register Src="/Modules/Ultimus.UWF.Form.ProcessControl/ButtonList.ascx" TagName="ButtonList" TagPrefix="btn" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta http-equiv="X-UA-Compatible"  content="IE=EmulateIE8" />
    <title>Business Gift Process</title>
    <script type="text/javascript" src="/Assets/js/common.js"></script>
</head>
<body>
    <form id="form1" runat="server">
        <div id="myDiv" class="container">
            <div class="row">
                <ui:userinfo id="UserInfo1" processtitle="Business Gift Request Process" processprefix="BG" tablename="PROC_BusinessGift" tablenamedetail="PROC_BusinessGift_DT"
                    runat="server"  ></ui:userinfo>
                  <%--  <asp:TextBox runat="server" ID="fld_rate" style="display:none;"></asp:TextBox>
                    <asp:TextBox runat="server" ID="fld_totalUSD" style="display:none;"></asp:TextBox>
                    <asp:TextBox runat="server" ID="fld_deptManagerLogin" style="display:none;"></asp:TextBox>
                    <asp:TextBox runat="server" ID="fld_DGM" style="display:none" ></asp:TextBox>
                    <asp:TextBox runat="server" ID="fld_GM" style="display:none" ></asp:TextBox>
                    <asp:TextBox runat="server" ID="fld_SupplierMLogin" style="display:none" ></asp:TextBox>
                    <asp:TextBox runat="server" ID="fld_FMLogin" style="display:none" ></asp:TextBox>
                    <asp:TextBox runat="server" ID="fld_DGMLogin" style="display:none;"></asp:TextBox>
                    <asp:TextBox runat="server" ID="fld_GMLogin" style="display:none;"></asp:TextBox>
                    <asp:TextBox runat="server" ID="fld_PURMLogin" style="display:none;"></asp:TextBox>
                    <asp:TextBox runat="server" ID="fld_CompareValue" value="5000.00" style="display:none;"></asp:TextBox>
                    <asp:TextBox runat="server" ID="fld_SupplierLogin" style="display:none;"></asp:TextBox>
                    <asp:TextBox runat="server" ID="fld_ApplierLogin" style="display:none;"></asp:TextBox>--%>
            </div>
            <div class="row">
                <%--<p style="font-weight:bold;">Reqeust information（"<span style=" background:red;">&nbsp;</span>" must write） </p>--%>
               <%-- <table class="table table-condensed table-bordered">
                    <tr>
                        <td class="td-label">
                            <span style=" background:red;  height:30px; float:left;">&nbsp;</span>
                            <p style="text-align:center;">Cost center</p>
                        </td>
                        <td class="td-content"   >
                            <asp:TextBox runat="server" ID="fld_CostCenterDisplay" onclick="costCenter_onclick(this)" style="Width:95%"></asp:TextBox>
                            <asp:TextBox runat="server" ID="fld_CostCenterCode" style="display:none;"></asp:TextBox>
                        </td>
                       
                        <td class="td-label">
                            <span style="height:30px; float:left;">&nbsp;</span>
                            <p style="text-align:center;">Price terms</p>
                        </td>
                        <td class="td-content" >
                            <asp:TextBox runat="server" ID="fld_PriceTerms" Width="95%"></asp:TextBox>
                        </td>
                        
                    </tr>
                    <tr>
                        <td class="td-label">
                            <span style=" background:red;  height:30px; float:left;">&nbsp;</span>
                            <p style="text-align:center;">Currency</p>
                        </td>
                        <td class="td-content"  >
                            <asp:DropDownList runat="server" ID="currency" onchange="currency_onchange(this)"></asp:DropDownList>
                            <asp:TextBox runat="server" ID="fld_Currency" style="display:none;"></asp:TextBox>
                        </td>
                        <td class="td-label"> 
                            <span style=" background:red; margin-top:5px;  height:30px; float:left;">&nbsp;</span>
                            <p style="text-align:center;">VAT</p>
                        </td>
                        <td class="td-content"   >
                            <asp:RadioButton ID="vat1" runat="server" GroupName="Vat" Text="Yes" onclick="getButtonCheck(this,1)" /> &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                            <asp:RadioButton ID="vat2" runat="server" GroupName="Vat" Text="No" onclick="getButtonCheck(this,2)" />
                            <asp:TextBox runat="server"  ID="fld_VAT" style="display:none;"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="td-label" style="vertical-align:middle">
                            <span style="height:30px; float:left;">&nbsp;</span>
                            <p style="text-align:center;">Remarks</p>
                        </td>
                        <td class="td-content" colspan="3" >
                            <asp:TextBox runat="server" ID="fld_Remarks" TextMode="MultiLine" Rows="3" Width="98%"></asp:TextBox>
                        </td>
                    </tr>
                    <tr style="display:none">
                    <td class="td-label" style="vertical-align:middle">
                            <span style="height:30px; float:left;">&nbsp;</span>
                            <p style="text-align:center;">PR Type</p>
                        </td>
                        <td class="td-content" colspan="3" >
                             <input type="radio" id="PRType1" checked="checked" name="PRType" onclick="LoadPRType('1')" /> Normal
                             <input type="radio" id="PRType2" name="PRType" style="margin-left:20px"  onclick="LoadPRType('0')"/> Confidential
                             <asp:TextBox runat="server" ID="fld_PRType"  Text="1" style="display:none" ></asp:TextBox>
                        </td>
                    </tr>
                </table>--%>
                <p style="font-weight:bold;">Details require  </p>
                <table class="table table-condensed table-bordered">
                    <tr>
                        <th width="1%"><p style="text-align:center">No.</p></th>
                        <th ><p style="text-align:center">Name</p></th>
                        <th ><p style="text-align:center">Reasons</p></th>
                   <%--     <th width="10%"><p style="text-align:center">Vendor</p></th>
                        <th width="9%"><p style="text-align:center">Request Date</p></th>
                        <th width="9%"><p style="text-align:center">Lead Time</p></th>
                        <th width="7%"><p style="text-align:center">Unit Price</p></th>
                        <th width="5%"><p style="text-align:center">Unit</p></th>
                        <th width="4%"><p style="text-align:center">Qty.</p></th>
                        <th width="10%"><p style="text-align:center">Amount</p></th>--%>
                        <th ></th>
                    </tr>
                    <tbody id="detail">
                    <asp:Repeater runat="server" ID="read_detail_PROC_BusinessGift_DT" >
                        <ItemTemplate>
                            <tr>
                                <td style="text-align:center; width:1%">
                                    <asp:TextBox ID="fld_FORMID" Text='<%#Eval("FORMID") %>' runat="server" Style="display: none"></asp:TextBox>
                                    <asp:Label ID="fld_ROWID" Text='<%# Container.ItemIndex+1%>' runat="server"></asp:Label>
                                </td>
                                <td>
                                    <asp:Label runat="server" ID="read_GiftName" Text='<%#Eval("GiftName") %>'   ></asp:Label>
                                </td>
                                <td>
                                    <asp:Label runat="server" ID="read_GiftComments" Text='<%#Eval("GiftComments") %>' ></asp:Label>
                                </td>
                            </tr>
                        </ItemTemplate>
                    </asp:Repeater>
                    </tbody>
                </table>
            </div>
            <div class="row">
                <attach:attachments id="Attachments1" runat="server" ReadOnly="true" ></attach:attachments>
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
              <asp:HiddenField runat="server" ID="hdUrgeTask" />
        </div>
    </form>
</body>
</html>


