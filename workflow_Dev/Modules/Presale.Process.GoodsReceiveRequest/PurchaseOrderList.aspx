<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PurchaseOrderList.aspx.cs" Inherits="Presale.Process.GoodsReceiveRequest.PurchaseOrderList" %>

<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <base target="_self"></base>
    <meta http-equiv="Expires" content="-1" />
    <meta http-equiv="PRAGMA" content="NO-CACHE" />
    <meta http-equiv="Cache-Control" content="no-cache" />
    <title>PurchaseDetails</title>
    <script type="text/javascript" src="/Assets/js/common.js"></script>
    <script type="text/javascript">
        function getButtonCheck(obj) {
            var ApplicantAccount = $(obj).parent().next().children().text() + ",";
            $("#ApplicantAccount").val(ApplicantAccount);
            var value = $(obj).parent().next().next().children().text();
            $("#AllPurchase").val(value);
        }
        function SinglePersonConfirm() {
            var returnJson = "";
            $("#tbodylist").find("tr").each(function (index,item) {
                var cbflag = $(item).find('input[type="checkbox"]').attr("checked");
                var ItemCode = $(item).find('.ItemCode').text();
                var Quantity = $(item).find('.Quantity').text();
                var PriceAfVAT = $(item).find('.PriceAfVAT').text();
                var VatRate = $(item).find('.VatRate').text();
                var CostCenter = $(item).find('.CostCenter').text();
                var UOM = $(item).find('.UOM').text();
                var U_M_PRNo = $(item).find('.U_M_PRNo').text();
                var U_M_Rqst = $(item).find('.U_M_Rqst').text();
                var U_M_DetD = $(item).find('.U_M_DetD').text();
                var U_M_ReqD = $(item).find('.U_M_ReqD').text();
                var DocNum = $(item).find('.DocNum').text();
                var LineNum = $(item).find('.LineNum').text();
                var U_M_PRLi = $(item).find('.U_M_PRLi').text();
                var WhsCode = $(item).find('.WhsCode').text();
                var Project = $(item).find('.Project').text();
                var ItemName = $(item).find('.ItemName').text();

                if (cbflag) {
                    returnJson += "{'ITEMNO':'" + ItemCode;
                    returnJson += "','QUANTITY':'" + Quantity;
                    returnJson += "','UnitPrice':'" + PriceAfVAT;
                    returnJson += "','TaxCode':'" + VatRate;
                    returnJson += "','CostCenter':'" + CostCenter;
                    returnJson += "','UOMCode':'" + UOM;
                    returnJson += "','PRNo':'" + U_M_PRNo;
                    returnJson += "','Requester':'" + U_M_Rqst;
                    returnJson += "','DetailDescription':'" + U_M_DetD;
                    returnJson += "','RequestDate':'" + U_M_ReqD;
                    returnJson += "','PONo':'" + DocNum;
                    returnJson += "','POLineNo':'" + LineNum;
                    returnJson += "','PRLineNo':'" + U_M_PRLi;
                    returnJson += "','WhsCode':'" + WhsCode;
                    returnJson += "','Project':'" + Project;
                    returnJson += "','ItemName':'" + ItemName;

                    returnJson += "'},";
                }
            });
            window.returnValue = returnJson;
            window.close();
        }
        function CloseForm() {
            window.close();
        }
        function selectSingleRadio(rbtn1, GroupName) {//控制repeater单选
            $("input[type=radio]").each(function (i) {
                if (this.name.substring(this.name.length - GroupName.length) == GroupName) {
                    this.checked = false;
                }
            })
            rbtn1.checked = true;
            getButtonCheck(rbtn1);
        }
    </script>
</head>
<body>
    <form id="form1" runat="server" style="width:95%;margin-top:5%;margin-left:2.5%">
    <div>
        <div>
        <p style="font-weight:bold">PO Information</p>
       <%-- <table class="table table-condensed table-bordered" >
                <tr>
                    <td class="td-label">Dept</td>
                    <td class="td-content"><asp:DropDownList runat="server" 
                            ID="dropDepartment" 
                            onselectedindexchanged="dropDepartment_SelectedIndexChanged" 
                            AutoPostBack="True" ></asp:DropDownList></td>
                    <td class="td-label" >Employee Name</td>
                    <td class="td-content"><asp:DropDownList ID="dropEmployee" runat="server"></asp:DropDownList></td>
                </tr>
                <tr>
                    <td style="text-align:center" colspan="4">
                        <asp:Button runat="server" CssClass="btn" ID="btnSearch" Text="Search" onclick="btnSearch_Click" style="float:right"/>
                    </td>
                 </tr>
            </table>--%>
        <table class="table table-condensed table-bordered">
            <tr>
                <td  ></td>
                <td width="4%" style="text-align:center">ItemCode</td>
                <td width="4%" style="text-align:center">Quantity</td>
                <td width="4%" style="text-align:center">UnitPrice</td>
                <td width="4%" style="text-align:center">VatRate</td>
                <td width="4%" style="text-align:center">Cost center</td>
                <td width="4%" style="text-align:center">UOM</td>
                <td width="4%" style="text-align:center">PR No.</td>
                <td width="4%" style="text-align:center">Requester</td>
                <td width="4%" style="text-align:center">Detail Description</td>
                <td width="4%" style="text-align:center">Require Date</td>
                <td width="4%" style="text-align:center">PO No.</td>
                <td width="4%" style="text-align:center">PO LineNo.</td>
                <td width="4%" style="text-align:center">PR LineNo.</td>

            </tr>
            <tbody id="tbodylist">
            <asp:Repeater runat="server" ID="purchaseList">
                <ItemTemplate>
                    <tr>
                        <td style="text-align:center">
                            <asp:CheckBox runat="server"/>
                            <%--<input type="radio" id="rbtnSelect" name ="FlowCode" runat="server" onclick="selectSingleRadio(this,'FlowCode');" />--%>
                        </td>
                        <td >
                            <asp:Label runat="server" ID="ItemCode" CssClass="ItemCode" Text='<%#Eval("ItemCode") %>'></asp:Label>
                        </td>
                        <td style="text-align:center">
                            <asp:Label runat="server" ID="Quantity" CssClass="Quantity" Text='<%#Eval("Quantity") %>'></asp:Label>
                        </td>
                        <td style="text-align:center">
                            <asp:Label runat="server" ID="PriceAfVAT" CssClass="PriceAfVAT" Text='<%#Eval("PriceAfVAT") %>'></asp:Label>
                        </td>
                        <td style="text-align:center">
                            <asp:Label runat="server" ID="VatRate" CssClass="VatRate" Text='<%#Eval("VatRate") %>'></asp:Label>
                        </td>
                        <td style="text-align:center">
                            <asp:Label runat="server" ID="CostCenter" CssClass="CostCenter" Text='<%#Eval("CostCenter") %>'></asp:Label>
                        </td>
                        <td style="text-align:center">
                            <asp:Label runat="server" ID="UOM" CssClass="UOM" Text='<%#Eval("UOM") %>'></asp:Label>
                        </td>
                        <td >
                            <asp:Label runat="server" ID="U_M_PRNo" CssClass="U_M_PRNo" Text='<%#Eval("U_M_PRNo") %>'></asp:Label>
                        </td>
                        <td style="text-align:center">
                            <asp:Label runat="server" ID="U_M_Rqst" CssClass="U_M_Rqst" Text='<%#Eval("U_M_Rqst") %>'></asp:Label>
                        </td>
                        <td style="text-align:center">
                            <asp:Label runat="server" ID="U_M_DetD" CssClass="U_M_DetD" Text='<%#Eval("U_M_DetD") %>'></asp:Label>
                        </td>
                        <td style="text-align:center">
                            <asp:Label runat="server" ID="U_M_ReqD" CssClass="U_M_ReqD" Text='<%#Eval("U_M_ReqD") %>'></asp:Label>
                        </td>
                        
                         <td style="text-align:center">
                            <asp:Label runat="server" ID="DocNum" CssClass="DocNum" Text='<%#Eval("DocNum") %>'></asp:Label>
                        </td>

                        <td style="text-align:center">
                            <asp:Label runat="server" ID="LineNum" CssClass="LineNum" Text='<%#Eval("LineNum") %>'></asp:Label>
                        </td>
                        <td style="text-align:center">
                            <asp:Label runat="server" ID="U_M_PRLi" CssClass="U_M_PRLi" Text='<%#Eval("U_M_PRLi") %>'></asp:Label>
                            <asp:Label runat="server" ID="WhsCode" CssClass="WhsCode" style="display:none" Text='<%#Eval("WhsCode") %>'></asp:Label>
                            <asp:Label runat="server" ID="Project" CssClass="Project" style="display:none" Text='<%#Eval("Project") %>'></asp:Label>
                            <asp:Label runat="server" ID="ItemName" CssClass="ItemName" style="display:none" Text='<%#Eval("ItemName") %>'></asp:Label>

                        </td>
                    </tr>
                </ItemTemplate>
            </asp:Repeater>
            </tbody>
           <%-- <tfoot>
                    <tr>
                    <td colspan="10">
                      <webdiyer:aspnetpager id="AspNetPager1" runat="server" custominfohtml="Count %RecordCount%"
                        horizontalalign="right" width="100%" cssclass="aspNetPager" onpagechanged="AspNetPager1_PageChanged"
                        pagesize="10" alwaysshow="true" submitbuttonstyle="display:none" inputboxstyle="display:none"
                        nextpagetext="Next" firstpagetext="Home" lastpagetext="Last" prevpagetext="Prev">
                    </webdiyer:aspnetpager>
                    </td>
                    </tr>
                    </tfoot>--%>
        </table>
        </div>
        <div class="center">
            <asp:Button ID="btnSave" runat="server" Text="OK" onClientClick="SinglePersonConfirm()"   CssClass="btn btn-primary " />
            <input type="button" value="Cancle" class="btn" onclick="CloseForm()"   />
        </div>
    </div>
    </form>
</body>
</html>
