<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Purchase.aspx.cs" Inherits="Presale.Process.PurchaseOrder.Purchase" %>
<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
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
            var returnJson = "[{'PurchaseNo':'";
            returnJson += $("#AllPurchase").val();
            returnJson += "'},{'Applicant':'";
            returnJson += $("#ApplicantAccount").val();
            returnJson += "'}]";
//            alert(returnJson);
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
        <p style="font-weight:bold">Applicant Information</p>
        <table class="table table-condensed table-bordered" >
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
            </table>
        <table class="table table-condensed table-bordered">
            <tr>
                <td width="9%"></td>
                <td width="9%" style="text-align:center">PO.NO.</td>
                <td width="9%" style="text-align:center">Applier</td>
                <td width="9%" style="text-align:center">Department</td>
                <td width="9%" style="text-align:center">Cost center</td>
                <td width="9%" style="text-align:center">Price terms</td>
                <td width="9%" style="text-align:center">Currency</td>
                <td width="9%" style="text-align:center">Remarks</td>
                <td width="9%" style="text-align:center">Date</td>
                <td width="9%" style="text-align:center">Total Amount</td>
            </tr>
            <tbody>
            <asp:Repeater runat="server" ID="purchaseList">
                <ItemTemplate>
                    <tr>
                        <td style="text-align:center">
                            <%--<asp:CheckBox runat="server" onclick="getButtonCheck(this)"/>--%>
                            <input type="radio" id="rbtnSelect" name ="FlowCode" runat="server" onclick="selectSingleRadio(this,'FlowCode');" />

                        </td>
                        <td style="display:none;">
                            <asp:Label runat="server" ID="APPLICANTACCOUNT" Text='<%#Eval("APPLICANTACCOUNT") %>'></asp:Label>
                        </td>
                        <td style="text-align:center">
                            <asp:Label runat="server" ID="DOCUMENTNO" Text='<%#Eval("DOCUMENTNO") %>'></asp:Label>
                        </td>
                        <td style="text-align:center">
                            <asp:Label runat="server" ID="APPLICANT" Text='<%#Eval("APPLICANT") %>'></asp:Label>
                        </td>
                        <td style="text-align:center">
                            <asp:Label runat="server" ID="DEPARTMENT" Text='<%#Eval("DEPARTMENT") %>'></asp:Label>
                        </td>
                        <td style="text-align:center">
                            <asp:Label runat="server" ID="CostCenterDisplay" Text='<%#Eval("CostCenterDisplay") %>'></asp:Label>
                        </td>
                        <td style="display:none;">
                            <asp:Label runat="server" ID="CostCenterCode" Text='<%#Eval("CostCenterCode") %>'></asp:Label>
                        </td>
                        <td style="text-align:center">
                            <asp:Label runat="server" ID="PriceTerms" Text='<%#Eval("PriceTerms") %>'></asp:Label>
                        </td>
                        <td style="text-align:center">
                            <asp:Label runat="server" ID="Currency" Text='<%#Eval("Currency") %>'></asp:Label>
                        </td>
                        <td style="text-align:center">
                            <asp:Label runat="server" ID="Remarks" Text='<%#Eval("Remarks") %>'></asp:Label>
                        </td>
                        
                        <td style="text-align:center">
                            <asp:Label runat="server" ID="Date" Text='<%# String.IsNullOrEmpty(Eval("Date").ToString())? "":DateTime.Parse(Eval("Date").ToString()).ToString("yyyy-MM-dd") %>'></asp:Label>
                        </td>
                        <td style="text-align:center">
                            <asp:Label runat="server" ID="TotalAmount" Text='<%#Eval("TotalAmount") %>'></asp:Label>
                        </td>
                    </tr>
                </ItemTemplate>
            </asp:Repeater>
            </tbody>
            <tfoot>
                    <tr>
                    <td colspan="10">
                      <webdiyer:aspnetpager id="AspNetPager1" runat="server" custominfohtml="Count %RecordCount%"
                        horizontalalign="right" width="100%" cssclass="aspNetPager" onpagechanged="AspNetPager1_PageChanged"
                        pagesize="10" alwaysshow="true" submitbuttonstyle="display:none" inputboxstyle="display:none"
                        nextpagetext="Next" firstpagetext="Home" lastpagetext="Last" prevpagetext="Prev">
                    </webdiyer:aspnetpager>
                    </td>
                    </tr>
                    </tfoot>
        </table>
        <asp:TextBox ID="AllPurchase" runat="server" style="display:none;"></asp:TextBox>
        <asp:TextBox ID="ApplicantAccount" runat="server" style="display:none;"></asp:TextBox>
        </div>
        <div class="center">
            <asp:Button ID="btnSave" runat="server" Text="OK" onClientClick="SinglePersonConfirm()"   CssClass="btn btn-primary " />
            <input type="button" value="Cancle" class="btn" onclick="CloseForm()"   />
        </div>
    </div>
    </form>
</body>
</html>
