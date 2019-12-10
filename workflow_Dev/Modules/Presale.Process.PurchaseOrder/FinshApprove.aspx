<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="FinshApprove.aspx.cs" Inherits="Presale.Process.PurchaseOrder.FinshApprove" %>


<%@ Register Src="/Modules/Ultimus.UWF.Form.ProcessControl/UserInfo.ascx" TagName="UserInfo" TagPrefix="ui" %>
<%@ Register Src="/Modules/Ultimus.UWF.Form.ProcessControl/ApprovalHistory.ascx" TagName="ApprovalHistory" TagPrefix="ah" %>
<%@ Register Src="/Modules/Ultimus.UWF.Form.ProcessControl/Attachments.ascx" TagName="Attachments" TagPrefix="attach" %>
<%@ Register Src="/Modules/Ultimus.UWF.Form.ProcessControl/ButtonList.ascx" TagName="ButtonList" TagPrefix="btn" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta http-equiv="X-UA-Compatible"  content="IE=EmulateIE8" />
    <title>Purchase Order Process</title>
    <script type="text/javascript" src="/Assets/js/common.js"></script>
    <script type="text/javascript">
        function submitPageReview(obj) {
            $("#ButtonList1_btnSubmit").click();
        }
        $(document).ready(function () {
            if ($("#hdIncident").val() != "") {
                $("#ButtonList1_btnSubmit").val("Submit");
                $("#ButtonList1_btnBack").hide();
                $("#ButtonList1_btnReject").show();
            }
        });
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <div id="myDiv" class="container">
            <div class="row">
                <ui:userinfo id="UserInfo1" processtitle="PURCHASE ORDER" processprefix="PURPO" tablename="PROC_PurchaseOrder" tablenamedetail="PROC_PurchaseOrder_DT" runat="server"  ></ui:userinfo>
            </div>
        <div class="row">
            <p style="font-weight:bold;">Request require</p>
            <table class="table table-condensed table-bordered">
                <tr>
                    <td class="td-label" style="vertical-align:middle;">
                        <span style="height:30px; float:left;">&nbsp;</span>
                        <p style="text-align:center">SUPPLIER</p>
                    </td>
                    <td class="td-content" colspan="3">
                        <asp:Label runat="server" ID="read_SUPPLIER" ></asp:Label>
                    </td>
                    <td class="td-label"> 
                        <p style="text-align:center">CURRENCY</p>
                    </td>
                    <td class="td-content" colspan="3"> 
                        <asp:Label ID="read_Currency" runat="server"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td class="td-label" style="vertical-align:middle;"> 
                        <span style="margin-top:5px;  height:30px; float:left;">&nbsp;</span>
                        <p style="text-align:center">BUYER</p>
                    </td>
                    <td class="td-content"  colspan="3" >
                        <asp:Label runat="server"  ID="read_BUYER"></asp:Label>
                    </td>
                    <td class="td-label" style="vertical-align:middle;"> 
                        <span style="height:30px; float:left;">&nbsp;</span>
                        <p style="text-align:center">WAREHOUSE</p>
                    </td>
                    <td class="td-content"  colspan="3">
                        <asp:Label runat="server"  ID="read_WAREHOUSE" ></asp:Label>
                    </td>
                </tr>
                
                <tr>
                    <td class="td-label">
                        <p style="text-align:center">PUR. REQUEST NO</p>
                    </td>
                    <td class="td-content" colspan="7">
                        <asp:Label runat="server" ID="read_PurchaseOrderNo"></asp:Label>
                    </td>
                </tr>
            </table>
            <p style="font-weight:bold">Request require</p>
            <table class="table table-condensed table-bordered">
                <tr>
                    <th>LINE</th><th>ITEMNO.</th><th>ITEMdESCRIPTION</th><th>DETAILDESCRIPTION</th>
                <th>MFRNO.</th><th>PRCODE</th><th>DEL.DATE</th><th>REO.DATE</th><th>QTY</th>
                <th>UNIT PRICE</th><th>UNIT</th><th>TAXCODE</th><th>COSTCENTER</th><th>PROJECT</th>
                </tr>
                <tbody>
                    <asp:Repeater runat="server" ID="fld_detail_PROC_PurchaseOrder_DT" OnItemDataBound="fld_detail_PROC_PurchaseOrder_DT_ItemDataBound" >
                        <ItemTemplate>
                            <tr>
                                <td style="text-align:center">
                                    <asp:TextBox ID="fld_FORMID" Text='<%#Eval("FORMID") %>' runat="server" Style="display: none"></asp:TextBox>
                                    <asp:Label ID="read_ROWID" Text='<%# Container.ItemIndex+1%>' runat="server"></asp:Label>
                                </td>
                                  <td>
                                    <asp:Label ID="read_ITEMNO" runat="server" Text='<%#Eval("ITEMNO") %>'></asp:Label>
                                </td>
                                <td>
                                    <asp:Label ID="read_ITEMDESCRIPTON" runat="server" Text='<%#Eval("ITEMDESCRIPTON") %>' Width="84%"></asp:Label>
                                </td>
                                <td>
                                    <asp:Label ID="read_DETAILDESCRIPTION" runat="server" Text='<%#Eval("DETAILDESCRIPTION") %>' Width="84%"></asp:Label>
                                </td>
                                <td>
                                    <asp:Label ID="read_MFRNO" runat="server" Text='<%#Eval("MFRNO") %>' Width="84%"></asp:Label>
                                </td>
                                <td>
                                    <asp:Label ID="read_PRCFACODE" runat="server" Text='<%#Eval("PRCFACODE") %>'></asp:Label>
                                </td>
                                <td>
                                    <asp:Label ID="read_DELDATE" runat="server" Text='<%# String.IsNullOrEmpty(Eval("DELDATE").ToString())? "":DateTime.Parse(Eval("DelDate").ToString()).ToString("yyyy-MM-dd") %>' onclick="WdatePicker({readOnly:false,dateFmt: 'yyyy-MM-dd'})" Width="84%"></asp:Label>
                                </td>
                                <td>
                                    <asp:Label ID="read_REODATE" runat="server" Text='<%# String.IsNullOrEmpty(Eval("REODATE").ToString())? "":DateTime.Parse(Eval("ReoDate").ToString()).ToString("yyyy-MM-dd") %>' onclick="WdatePicker({readOnly:false,dateFmt: 'yyyy-MM-dd'})" Width="84%"></asp:Label>
                                </td>
                                <td>
                                    <asp:Label ID="read_QTY" runat="server" Text='<%#Eval("QTY") %>' Width="30%"></asp:Label>
                                </td>
                                <td>
                                    <asp:Label ID="read_UNITPRICE" runat="server" Text='<%#Eval("UNITPRICE") %>' Width="84%"></asp:Label>
                                </td>
                                <td>
                                    <asp:Label ID="read_UNIT" runat="server" Text='<%#Eval("UNIT") %>' ></asp:Label>
                                </td>
                                 <td>
                                   <asp:Label ID="read_taxcodeYc" runat="server" Text='<%#Eval("taxcodeYc") %>' style="display:none;"></asp:Label>
                                    <asp:Label ID="read_TAXCODE" runat="server" Text='<%#Eval("TAXCODE") %>' Width="84%" style="display:none"></asp:Label>
                                     <asp:Label ID="latexcde" runat="server"></asp:Label>
                                </td>
                                  <td>
                                   <asp:Label ID="read_CostCenter" runat="server" Text='<%#Eval("CostCenter") %>' style="display:none;"></asp:Label>
                                    <asp:Label ID="read_COSTCENTERDISPLAY" runat="server" Text='<%#Eval("COSTCENTERDISPLAY") %>' Width="84%" ></asp:Label>
                                </td>
                                  <td>
                                   <asp:Label ID="read_Project" runat="server" Text='<%#Eval("Project") %>' style="display:none;"></asp:Label>
                                    <asp:Label ID="read_ProjectDisplay" runat="server" Text='<%#Eval("ProjectDisplay") %>' Width="84%" ></asp:Label>
                                </td>
                            </tr>
                        </ItemTemplate>
                    </asp:Repeater>
                    <tr>
                        <td colspan="10">
                            <p style="text-align:right;">TOTAL VALUE EXCLUDING TAX</p>
                        </td>
                        <td colspan="4" style="text-align:center">
                            <asp:Label ID="read_TotalValue" CssClass="money" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="10">
                            <p style="text-align:right;">TAX AMOUNT</p>
                        </td>
                        <td colspan="4" style="text-align:center">
                            <asp:Label ID="read_TaxAmount" CssClass="money" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="10">
                            <p style="text-align:right;">TOTAL VALUE WITH TAX</p>
                        </td>
                        <td colspan="4" style="text-align:center">
                            <asp:Label ID="read_TotalValueWithTax" CssClass="money" runat="server"></asp:Label>
                        </td>
                    </tr>
                     
                </tbody>
            </table>
               <table class="table table-condensed table-bordered">
                <tr>
                        <td class="td-label" style="vertical-align:middle">
                            <span style="height:30px; float:left;">&nbsp;</span>
                            <p style="text-align:center;">Remarks</p>
                        </td>
                        <td class="td-content" colspan="7" >
                            <asp:Label runat="server" ID="read_Remark" ></asp:Label>
                        </td>
                </tr>
            </table>
        </div>
          <div class="row">
            <attach:attachments id="Attachments1" ReadOnly="false"  runat="server"></attach:attachments>
        </div>
        <div class="row">
            <ah:approvalhistory id="ApprovalHistory1" showaction="true" runat="server"></ah:approvalhistory>
        </div>
            <btn:buttonlist id="ButtonList1" runat="server"></btn:buttonlist>
        <div style="display:none;">
        </div>
        <div style="display:none">
            <asp:TextBox runat="server" ID="fld_TRSummary"></asp:TextBox>
            <asp:HiddenField runat="server" ID="hdIncident" />
            <asp:HiddenField runat="server" ID="hdPrint" />
        </div>
    </form>
</body>
</html>



