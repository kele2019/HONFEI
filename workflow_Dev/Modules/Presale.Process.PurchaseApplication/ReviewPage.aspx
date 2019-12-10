<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ReviewPage.aspx.cs" Inherits="Presale.Process.PurchaseApplication.ReviewPage" %>

 <%@ Register Src="/Modules/Ultimus.UWF.Form.ProcessControl/UserInfo.ascx" TagName="UserInfo" TagPrefix="ui" %>
<%@ Register Src="/Modules/Ultimus.UWF.Form.ProcessControl/ApprovalHistory.ascx" TagName="ApprovalHistory" TagPrefix="ah" %>
<%@ Register Src="/Modules/Ultimus.UWF.Form.ProcessControl/Attachments.ascx" TagName="Attachments" TagPrefix="attach" %>
<%@ Register Src="/Modules/Ultimus.UWF.Form.ProcessControl/ButtonList.ascx" TagName="ButtonList" TagPrefix="btn" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta http-equiv="X-UA-Compatible"  content="IE=EmulateIE8" />
    <title>Purchase Request Process</title>
    <script type="text/javascript" src="/Assets/js/common.js"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            if ($("#read_SingleSupplier").text() == "yes") {
                $("#singleSupplier").attr("checked", true);
                $("#reason").show();
                $("#supplierNameLabel").show();
                $("#supplierNameValue").show();
            }
            //            $("#read_Date").text(showtime($("#read_Date").text()));
            if ($("#read_VAT").text() == "Yes") {
                $("#vat1").attr("checked", true);
            }
            if ($("#read_VAT").text() == "No") {
                $("#vat2").attr("checked", true);
            }
            var PRType = $("#read_PRType").val();
            if (PRType == "1" || PRType == "")
                $("#spanPRType").text("Normal");
            if (PRType == "0")
                $("#spanPRType").text("Confidential");
            if (request("type") == "myapproval") {
                $("#btnDiv").hide();
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

            $("#ButtonList1_btnSubmit").val("Confirm Review?");
            $("#ApprovalHistory1_txtSpecialAction").val("Review");
            $("#ButtonList1_btnSubmit").click();
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <div id="myDiv" class="container">
            <div class="row">
                <ui:userinfo id="UserInfo1" processtitle="Purchase Request Process" processprefix="PURPR" tablename="PROC_Purchase"
                     tablenamedetail="PROC_Purchase_DT" runat="server"  ></ui:userinfo>
            </div>
            <div class="row">
                <p style="font-weight:bold;">Procurement information</p>
                <table class="table table-condensed table-bordered">
                    <tr>
                        <td class="td-label">
                            <span style="height:30px; float:left;">&nbsp;</span>
                            <p style="text-align:center;">Cost center</p>
                        </td>
                        <td class="td-content" colspan="3" >
                            <asp:Label runat="server" ID="read_CostCenterDisplay"></asp:Label>
                            <asp:Label runat="server" ID="read_CostCenterCode" style="display:none;"></asp:Label>
                        </td>
                        <%--<td class="td-label"> 
                            <span style="margin-top:5px;  height:30px; float:left;">&nbsp;</span>
                            <p style="text-align:center;">projet</p>
                        </td>
                        <td class="td-content"  colspan="3" >
                            <asp:Label runat="server"  ID="read_Projet"></asp:Label>
                        </td>--%>
                        <td class="td-label">
                            <span style="height:30px; float:left;">&nbsp;</span>
                            <p style="text-align:center;">Price terms</p>
                        </td>
                        <td class="td-content" colspan="3" >
                            <asp:Label runat="server" ID="read_PriceTerms"></asp:Label>
                        </td>
                        <%--<td class="td-label"> 
                            <span style="margin-top:5px;  height:30px; float:left;">&nbsp;</span>
                            <p style="text-align:center;">Date</p>
                        </td>
                        <td class="td-content"  colspan="3" >
                            <asp:Label runat="server"  ID="read_Date"></asp:Label>
                        </td>--%>
                    </tr>
                    <tr>
                        <td class="td-label">
                            <span style="height:30px; float:left;">&nbsp;</span>
                            <p style="text-align:center;">Currency</p>
                        </td>
                        <td class="td-content" colspan="3" >
                            <asp:Label runat="server" ID="read_Currency" ></asp:Label>
                        </td>
                        <td class="td-label"> 
                            <span style="margin-top:5px;  height:30px; float:left;">&nbsp;</span>
                            <p style="text-align:center;">VAT</p>
                        </td>
                        <td class="td-content"  colspan="3" >
                            <asp:RadioButton ID="vat1" runat="server" disabled="true" GroupName="Vat" Text="Yes"/>
                            <asp:RadioButton ID="vat2" runat="server" disabled="true" GroupName="Vat" Text="No"/>
                            <asp:Label runat="server"  ID="read_VAT" style="display:none;"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="td-label" style="vertical-align:middle">
                            <span style="height:30px; float:left;">&nbsp;</span>
                            <p style="text-align:center;">Remarks</p>
                        </td>
                        <td class="td-content" colspan="7" >
                            <asp:Label runat="server" ID="read_Remarks" ></asp:Label>
                        </td>
                    </tr>

                     <tr style="display:none">
                    <td class="td-label" style="vertical-align:middle">
                            <span style="height:30px; float:left;">&nbsp;</span>
                            <p style="text-align:center;">PR Type</p>
                        </td>
                        <td class="td-content" colspan="7" >
                          <span id="spanPRType"></span>
                       <asp:TextBox runat="server" ID="read_PRType"   style="display:none" ></asp:TextBox>
                        </td>
                    </tr>

                </table>
                <table class="table table-condensed table-bordered">
                    <tr>
                        <th width="1%"><p style="text-align:center">No.</p></th>
                        <%--<th width="10%"><p style="text-align:center">Items</p></th>--%>
                        <th width="10%"><p style="text-align:center">Material Description</p></th>
                        <th width="10%"><p style="text-align:center">Specification</p></th>
                        <th width="10%"><p style="text-align:center">Vendor</p></th>
                        <th width="10%"><p style="text-align:center">Request Date</p></th>
                        <th width="10%"><p style="text-align:center">Lead Time</p></th>
                        <th width="10%"><p style="text-align:center">Unit Price</p></th>
                        <th width="10%"><p style="text-align:center">Unit</p></th>
                        <th width="10%"><p style="text-align:center">Qty.</p></th>
                        <th width="10%"><p style="text-align:center">Amount</p></th>
                    </tr>
                    <asp:Repeater runat="server" ID="fld_detail_PROC_Purchase_DT">
                        <ItemTemplate>
                            <tr>
                                <th style="text-align:center">
                                    <asp:TextBox ID="fld_FORMID" Text='<%#Eval("FORMID") %>' runat="server" Style="display: none"></asp:TextBox>
                                    <asp:Label ID="fld_ROWID" Text='<%# Container.ItemIndex+1%>' runat="server"></asp:Label>
                                </th>
                                <td style="text-align:center">
                                    <%--<asp:Label runat="server" ID="read_Items" Text='<%#Eval("Items") %>' ></asp:Label>--%>
                                    <asp:Label runat="server" ID="read_Description" Text='<%#Eval("Description") %>' ></asp:Label>
                                </td>
                                <td style="text-align:center">
                                    <asp:Label runat="server" ID="read_Specification" Text='<%#Eval("Specification") %>'></asp:Label>
                                </td>
                                <td style="text-align:center">
                                    <asp:Label runat="server" ID="read_Vendor" Text='<%#Eval("Vendor") %>' ></asp:Label>
                                </td>
                                <td style="text-align:center">
                                    <asp:Label runat="server" ID="read_RequestDate" Text='<%#String.IsNullOrEmpty(Eval("RequestDate").ToString())? "":DateTime.Parse(Eval("RequestDate").ToString()).ToString("yyyy-MM-dd") %>' ></asp:Label>
                                </td>
                                <td style="text-align:center">
                                    <asp:Label runat="server" ID="read_LeadTime" Text='<%#String.IsNullOrEmpty(Eval("LeadTime").ToString())? "":DateTime.Parse(Eval("LeadTime").ToString()).ToString("yyyy-MM-dd")  %>'></asp:Label>
                                </td>
                                <td style="text-align:center">
                                <span class="money"><%#Eval("UnitPrice") %></span>
                                    <asp:Label runat="server" ID="read_UnitPrice" Text='<%#Eval("UnitPrice") %>' style="display:none" ></asp:Label>
                                </td>
                                <td style="text-align:center">
                                    <asp:Label runat="server" ID="read_Unit" Text='<%#Eval("Unit") %>' ></asp:Label>
                                </td>
                                <td style="text-align:center">
                                    <asp:Label runat="server" ID="read_Qty" Text='<%#Eval("Qty") %>' ></asp:Label>
                                </td>
                                <td style="text-align:center">
                                  <span class="money"><%#Eval("Amount")%></span>
                                    <asp:Label runat="server" ID="read_Amount" Text='<%#Eval("Amount") %>' style="display:none"></asp:Label>
                                </td>
                            </tr>
                        </ItemTemplate>
                    </asp:Repeater>
                    <tr>
                        <td colspan="9">
                            <p style="text-align:right">Total Amount</p>
                        </td>
                        <td style="text-align:center">
                            <asp:Label runat="server" CssClass="money" ID="read_TotalAmount"></asp:Label>
                        </td>
                    </tr>
                </table>
                <table class="table table-condensed table-bordered">
                    <tr>
                        <td class="td-label" style="text-align:center">
                            <p style="text-align:center">Single Supplier</p>
                        </td>
                        <td class="td-content" colspan="3">
                            <asp:CheckBox runat="server" ID="singleSupplier" disabled="true"/>
                            <asp:Label runat="server" ID="read_SingleSupplier" style="display:none;"></asp:Label>
                        </td>
                        <td class="td-label" style="text-align:center;display:none;" id="supplierNameLabel">
                            <p style="text-align:center">Supplier Name</p>
                        </td>
                        <td class="td-content" colspan="3" style="display:none;" id="supplierNameValue">
                            <asp:Label runat="server" ID="read_SupplierName"></asp:Label>
                        </td>
                    </tr>
                    <tr style="display:none;" id="reason">
                        <td class="td-label" style="text-align:center">
                            <p style="text-align:center">Reason</p>
                        </td>
                        <td class="td-content" colspan="7">
                            <asp:Label runat="server" ID="read_SingleSupplierReason"></asp:Label>
                        </td>
                    </tr>
                </table>
            </div>
            <div class="row">
                <attach:attachments id="Attachments1" ReadOnly="true" runat="server"></attach:attachments>
            </div>
            <div class="row">
                <ah:approvalhistory id="ApprovalHistory1" showaction="true" runat="server"></ah:approvalhistory>
            </div>
        </div>
            <div id="btnDiv"  >
            <table style="width: 100%;" >
                <tr>
                    <td align="center"  >
                        <table>
                            <tr>
                                <td> 
                                <input type="button" id="btnComplete"  class="btn" value="Review" onclick="submitPageReview('0')" />
                                </td>
                            </tr>
                       </table>
                    </td>
                 </tr>
            </table>
        </div>
        <div style="display:none">
        <btn:buttonlist id="ButtonList1" runat="server"></btn:buttonlist>
            <asp:TextBox runat="server" ID="fld_TRSummary"></asp:TextBox>
            <asp:HiddenField runat="server" ID="hdIncident" />
            <asp:HiddenField runat="server" ID="hdPrint" />
        </div>
    </form>
</body>
</html>
