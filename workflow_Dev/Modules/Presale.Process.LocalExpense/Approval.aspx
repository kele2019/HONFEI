<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Approval.aspx.cs" Inherits="Presale.Process.LocalExpense.Approval" %>
<%@ Register Src="/Modules/Ultimus.UWF.Form.ProcessControl/UserInfo.ascx" TagName="UserInfo" TagPrefix="ui" %>
<%@ Register Src="/Modules/Ultimus.UWF.Form.ProcessControl/ApprovalHistory.ascx" TagName="ApprovalHistory" TagPrefix="ah" %>
<%@ Register Src="/Modules/Ultimus.UWF.Form.ProcessControl/Attachments.ascx" TagName="Attachments" TagPrefix="attach" %>
<%@ Register Src="/Modules/Ultimus.UWF.Form.ProcessControl/ButtonList.ascx" TagName="ButtonList" TagPrefix="btn" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta http-equiv="X-UA-Compatible"  content="IE=EmulateIE8" />
    <title>Local Expense Process</title>
    <script type="text/javascript" src="/Assets/js/common.js"></script>
</head>
<body>
    <form id="form1" runat="server">
     <div id="myDiv" class="container">
            <div class="row">
                <ui:userinfo id="UserInfo1" processtitle="Local Expense Process" processprefix="FINLE" tablename="PROC_LocalExpense"
                    tablenamedetail = "PROC_LocalExpense_DT,PROC_LocalExpenseThird_DT" runat="server"  ></ui:userinfo>
            </div>
            <asp:TextBox runat="server" ID="fld_ApprovalUserPost" style="display:none;"></asp:TextBox>
            <div class="row">
                <p style="font-weight:bold;">Request require</p>
                <table class="table table-condensed table-bordered">
                    <tr>
                        <td class="td-label"><p style="text-align:center">Employee</p></td>
                        <td class="td-content">
                            <asp:Label runat="server" ID="read_VendorDisplay"></asp:Label>
                        </td>
                        <td class="td-label"><p style="text-align:center">Cost Center</p></td>
                        <td class="td-content">
                            <asp:Label runat="server" ID="read_CostCenterSubject"></asp:Label>
                        </td>
                        <td class="td-label"><p style="text-align:center">Project</p></td>
                        <td class="td-content">
                            <asp:Label runat="server" ID="read_Project"></asp:Label>
                        </td>
                    </tr>
                      <tr>
                        <td class="td-label" colspan="2">
                        <span style="height:30px; float:left;">&nbsp;</span>
                            <p style="text-align:center">Purchase Request NO</p>
                        </td>
                        <td class="td-content" colspan="6">
                            <asp:Label runat="server" ID="read_PurchaseOrderNo"></asp:Label>
                        </td>
                    </tr>
                </table>
                <table class="table table-condensed table-bordered">
                    <tr>
                        <th width="5%"><p style="text-align:center">NO.</p></th>
                        <th width="25%"><p style="text-align:center">Date</p></th>
                        <th width="25%"><p style="text-align:center">Description</p></th>
                        <th width="25%"><p style="text-align:center">Comments</p></th>
                        <th width="25%"><p style="text-align:center">RMB</p></th>
                    </tr>
                    <tbody id="detailtable">
                    <asp:Repeater runat="server" ID="fld_detail_PROC_LocalExpense_DT">
                        <ItemTemplate>
                            <tr>
                                <th style="text-align:center">
                                    <span style="float:left">&nbsp;</span> 
                                    <asp:Label ID="read_ROWID" Text='<%# Container.ItemIndex+1%>' runat="server"></asp:Label>
                                </th>
                                <td style="text-align:center">
                                    <asp:Label ID="read_Date" Text='<%# String.IsNullOrEmpty(Eval("Date").ToString())? "":DateTime.Parse(Eval("Date").ToString()).ToString("yyyy-MM-dd") %>' runat="server"></asp:Label>
                                </td>
                                <td style="text-align:center">
                                    <asp:Label ID="read_SubjectName" Text='<%#Eval("SubjectName") %>' runat="server"></asp:Label>
                                     <asp:Label ID="read_SubjectCode" Text='<%#Eval("SubjectCode") %>' runat="server" style="display:none"></asp:Label>
                                    <asp:Label ID="read_ItemValue" Text='<%#Eval("ItemValue") %>' runat="server" style="display:none"></asp:Label>
                                </td>
                                <td style="text-align:center">
                                    <asp:Label ID="read_Comments" runat="server" Text='<%#Eval("Comments")%>'></asp:Label>
                                </td>
                                <td style="text-align:center">
                                  <span class="money"><%#Eval("RMB") %></span>
                                    <asp:Label ID="read_RMB"  Text='<%#Eval("RMB") %>' runat="server" style="display:none"></asp:Label>
                                </td>
                            </tr>
                        </ItemTemplate>
                    </asp:Repeater>
                </tbody>
                <tr>
                    <th></th>
                    <td colspan="3" style="text-align:right">
                        <p style="float:right">Sub-total</p>
                    </td>
                    <td style="text-align:center">
                        <asp:Label runat="server" CssClass="money" ID="read_RMB"></asp:Label>
                    </td>
                </tr>
                </table>


                  <p style="font-weight:bold;">ENTERTAINMENT (if space is insufficient,please attach a separate entertainment report)</p>
                <table class="table table-condensed table-bordered">
                    <tr>
                        <th width="10%"><p style="text-align:center">Date</p></th>
                        <th width="23%"><p style="text-align:center">Amount</p></th>
                        <th width="36%"><p style="text-align:center">Guest Name & Company</p></th>
                        <th width="23%"><p style="text-align:center">Business Purpose</p></th>
                    </tr>
                   
                    <asp:Repeater runat="server" ID="read_detail_PROC_LocalExpenseThird_DT" >
                        <ItemTemplate>
                        <tr>
                            <td>
                                <asp:Label runat="server" ID="read_Date"   Text='<%# String.IsNullOrEmpty(Eval("Date").ToString())? "":DateTime.Parse(Eval("Date").ToString()).ToString("yyyy-MM-dd") %>' Width="90%" ></asp:Label>
                            </td>
                            <td>
                                <asp:Label runat="server" ID="read_Amount"  Text='<%#Eval("Amount") %>' Width="92%" ></asp:Label>
                            </td>
                            <td>
                                <asp:Label runat="server" ID="read_GuestNameCompany"  Text='<%#Eval("GuestNameCompany") %>' Width="96%" ></asp:Label>
                            </td>
                            <td>
                                <asp:Label runat="server" ID="read_BusinessPurpose"    Text='<%#Eval("BusinessPurpose") %>' Width="92%" ></asp:Label>
                            </td>
                        </tr>
                        </ItemTemplate>
                     </asp:Repeater>
                     
                     
                </table>


                <table class="table table-condensed table-bordered">
                    <tr>
                        <td class="td-label" style="vertical-align:middle">
                            <p style="text-align:center">Remarks</p>
                        </td>
                        <td class="td-content" colspan="4">
                            <asp:Label runat="server" ID="read_Remarks"></asp:Label>
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
        <btn:buttonlist id="ButtonList1" runat="server"></btn:buttonlist>
        <div style="display:none">
        <asp:TextBox runat="server" ID="fld_TRSummary"></asp:TextBox>
        <asp:HiddenField runat="server" ID="hdIncident" />
        <asp:HiddenField runat="server" ID="hdPrint" />
        </div>
    </form>
</body>
</html>


