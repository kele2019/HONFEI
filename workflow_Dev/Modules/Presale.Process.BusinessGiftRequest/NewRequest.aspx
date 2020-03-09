<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="NewRequest.aspx.cs" Inherits="Presale.Process.BusinessGiftRequest.NewRequest" %>
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
    <script type="text/javascript">
          
        function beforeSubmit() {
            //$("#Attachments1_txtMust").val("1");
            var summary = "Business Gift Process";
            $("#UserInfo1_fld_PROCESSSUMMARY").val(summary);
            
             return true;
        }

        $(document).ready(function () {
            if ($("#hdIncident").val() != "") {
                $("#ButtonList1_btnSubmit").val("Submit");
                $("#ButtonList1_btnBack").hide();
                $("#ButtonList1_btnReject").show();
            }
            if ($("#hdUrgeTask").val() == "Yes") {
                $("#ReturnBackTask").show();
            }
            countsubtoal();
        });
         
        function beforeSave() {
            var summary = "Business Gift Process";
            $("#UserInfo1_fld_PROCESSSUMMARY").val(summary);
            $("#fld_TRSummary").val(summary);
            return true;
        }
        function changeRat(obj) {
            $(obj).next().val($(obj).val());
            //$(obj).parent().find("input").val($(obj).find("option:selected").text());
            //countsub(obj);
        }
        function countsubtoal() {
            var subttoal = 0;
            $("#detail").find("tr").each(function (i, Etr) {
                var Textstr = $(Etr).find("td").eq(1).find("input").val();
                if (Textstr != "") {
                    $(Etr).find("td").eq(1).find("select option:contains('" + Textstr + "')").attr("selected", true);
                }
            });
        }

    </script>
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
                <p style="font-weight:bold;">Details require（"<span style=" background:red;">&nbsp;</span>" must write） </p>
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
                    <asp:Repeater runat="server" ID="fld_detail_PROC_BusinessGift_DT" OnItemCommand="fld_detail_PROC_BusinessGift_DT_ItemCommand" OnItemDataBound="fld_detail_PROC_BusinessGift_DT_ItemDataBound">
                        <ItemTemplate>
                            <tr>
                                <td style="text-align:center; width:1%">
                                    <span style=" background:red;">&nbsp;</span>
                                    <asp:TextBox ID="fld_FORMID" Text='<%#Eval("FORMID") %>' runat="server" Style="display: none"></asp:TextBox>
                                    <asp:Label ID="fld_ROWID" Text='<%# Container.ItemIndex+1%>' runat="server"></asp:Label>
                                </td>
                                <td>
                                    <asp:DropDownList runat="server" ID="Item" Class="item" onchange="changeRat(this)"  CssClass="validate[required]"   style="width:99%"></asp:DropDownList>
                                    <%--<asp:TextBox runat="server" ID="fld_Items" Text='<%#Eval("Items") %>' style="display:none;"></asp:TextBox>--%>
                                    <asp:TextBox runat="server" ID="fld_GiftName" Text='<%#Eval("GiftName") %>'  style="display:none;"></asp:TextBox>
                                </td>
                                <td>
                                    <asp:TextBox runat="server" ID="fld_GiftComments" Text='<%#Eval("GiftComments") %>' CssClass="validate[required]" width="95%"></asp:TextBox>
                                </td>
                               <%-- <td>
                                    <asp:TextBox runat="server" ID="fld_Vendor" Text='<%#Eval("Vendor") %>' width="84%"></asp:TextBox>
                                </td>
                                <td>
                                    <asp:TextBox runat="server" ID="fld_RequestDate" Text='<%#Eval("RequestDate").ToString()==""?"":Convert.ToDateTime(Eval("RequestDate").ToString()).ToString("yyy/MM/dd") %>' CssClass="validate[required]" onclick="WdatePicker({readOnly:false,dateFmt: 'yyyy-MM-dd'})" width="84%"></asp:TextBox>
                                </td>
                                <td>
                                    <asp:TextBox runat="server" ID="fld_LeadTime" Text='<%#Eval("LeadTime").ToString()==""?"":Convert.ToDateTime(Eval("LeadTime").ToString()).ToString("yyy/MM/dd") %>' CssClass="validate[required]" onclick="WdatePicker({readOnly:false,dateFmt: 'yyyy-MM-dd'})" width="84%"></asp:TextBox>
                                </td>
                                <td>
                                    <asp:TextBox runat="server" class="unitPrice" ID="fld_UnitPrice" Text='<%#Eval("UnitPrice") %>' onblur="getamount(this,'1')" CssClass="validate[required]" width="70%"></asp:TextBox>
                                </td>
                                <td>
                                    <asp:TextBox runat="server" ID="fld_Unit" Text='<%#Eval("Unit") %>' CssClass="validate[required]" width="53%"></asp:TextBox>
                                </td>
                                <td>
                                    <asp:TextBox runat="server" class="qty" ID="fld_Qty" Text='<%#Eval("Qty") %>' onblur="getamount(this,'2')" CssClass="validate[required]" width="53%"></asp:TextBox>
                                </td>
                                <td>
                                    <asp:Label runat="server"  class="money" ID="lb_Amount" Text='<%#Eval("Amount") %>' value="0.00" ></asp:Label>
                                    <asp:TextBox runat="server" class="amount" ID="fld_Amount" Text='<%#Eval("Amount") %>' value="0.00" CssClass="validate[required]" width="84%" onchange="amount_onchange(this)" style="display:none" ></asp:TextBox>
                                </td>--%>
                                <td  style=" text-align:center">
                                    <asp:Button ID="btnDelete" runat="server" Text="delete" CssClass="btn" CommandName="del" ClientIDMode="Static" OnClientClick="return confirm('Confirm Del？')" />
                                </td>
                            </tr>
                        </ItemTemplate>
                    </asp:Repeater>
                    </tbody>
                    <tr>
                        <td colspan="2" style="text-align:left">
                            <asp:Button ID="btnAdd" runat="server" Text="add" CssClass="btn" CausesValidation="false" OnClick="btnAdd_Click"/>
                        </td>
                        <td colspan="2">
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


