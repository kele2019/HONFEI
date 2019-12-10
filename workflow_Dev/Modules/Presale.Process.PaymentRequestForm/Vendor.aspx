<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Vendor.aspx.cs" Inherits="Presale.Process.PaymentRequestForm.Vendor" %>
<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <base target="_self"></base>
    <meta http-equiv="Expires" content="-1" />
    <meta http-equiv="PRAGMA" content="NO-CACHE" />
    <meta http-equiv="Cache-Control" content="no-cache" />
    <title>VendorDetails</title>
    <script type="text/javascript" src="/Assets/js/common.js"></script>
    <script type="text/javascript">
        function getButtonCheck(obj) {
            var VendorCode = $(obj).parent().next().children().text();
            $("#VendorCode").val(VendorCode);
            var VendorName = $(obj).parent().next().next().children().text();
            $("#VendorName").val(VendorName);
            var VendorDesp = $(obj).parent().next().next().next().children().text();
            $("#VendorDesp").val(VendorDesp);
            var Currency = $(obj).parent().next().next().next().next().children().text();
            $("#Currency").val(Currency);
            var GroupNum = $(obj).parent().next().next().next().next().next().children().text();
            $("#GroupNum").val(GroupNum);
            var VendorBank = $(obj).parent().next().next().next().next().next().next().children().text();
            $("#VendorBank").val(VendorBank);
            var DfiAccount = $(obj).parent().next().next().next().next().next().next().next().children().text();
            $("#DfiAccount").val(DfiAccount);
            var Swift = $(obj).parent().next().next().next().next().next().next().next().next().children().text();
            $("#Swift").val(Swift);
        }
        function SinglePersonConfirm() {
            var returnJson = "[{'VendorCode':'";
            returnJson += $("#VendorCode").val();
            returnJson += "'},{'VendorName':'";
            returnJson += $("#VendorName").val();
            returnJson += "'},{'VendorDesp':'";
            returnJson += $("#VendorDesp").val();
            returnJson += "'},{'Currency':'";
            returnJson += $("#Currency").val();
            returnJson += "'},{'GroupNum':'";
            returnJson += $("#GroupNum").val();
            returnJson += "'},{'VendorBank':'";
            returnJson += $("#VendorBank").val();
            returnJson += "'},{'DfiAccount':'";
            returnJson += $("#DfiAccount").val();
            returnJson += "'},{'Swift':'";
            returnJson += $("#Swift").val();
            returnJson += "'}]";
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
      <table class="table table-condensed table-bordered">
            <tr>
                    <td class="td-label">Vendor Name</td> 
                  
                    <td class="td-content"><asp:TextBox runat="server" ID="ByVendorName" Width="40%"></asp:TextBox> 
                    </td>
                    <td class="td-label" >Vendor Description</td>
                      <td class="td-content"><asp:TextBox runat="server" ID="ByVendorCode"  Width="40%"></asp:TextBox>
                        <asp:Button runat="server" CssClass="btn" ID="btnSearch" Text="Search" onclick="btnSearch_Click" style="float:right"/>
                      </td>

                 </tr>
         </table>
        <table class="table table-condensed table-bordered">
            <tr>
                <th style="Width:2%;"></th>
                <th style="Width:10%;">Vendor Code</th>
                <th style="Width:16%;">Vendor Name</th>
                <th style="Width:20%;">Vendor Description</th>
                <th style="Width:8">Currency</th>
                <th style="Width:10%;">Pay Term</th>
                <th style="Width:12%">Bank Name</th>
                <th style="Width:14%">Bank Account</th>
                <th style="Width:8">Swift</th>
            </tr>
            <tbody>
            <asp:Repeater runat="server" ID="EmployeeList">
                <ItemTemplate>
                    <tr>
                        <td style="text-align:center">
                            <%--<asp:CheckBox runat="server" onclick="getButtonCheck(this)"/>--%>
                            <input type="radio" id="rbtnSelect" name ="FlowCode" runat="server" onclick="selectSingleRadio(this,'FlowCode');" />
                        </td>
                        <td style="text-align:center">
                            <asp:Label runat="server" ID="VendorCode" Text='<%#Eval("VendorCode") %>'></asp:Label>
                        </td>
                        <td style="text-align:center">
                            <asp:Label runat="server" ID="VendorName" Text='<%#Eval("VendorName") %>'></asp:Label>
                        </td>
                        <td style="text-align:center">
                            <asp:Label runat="server" ID="VendorDesp" Text='<%#Eval("VendorDesp") %>'></asp:Label>
                        </td>
                        <td style="text-align:center">
                            <asp:Label runat="server" ID="Currency" Text='<%#Eval("Currency") %>'></asp:Label>
                        </td>
                        <td style="text-align:center">
                            <asp:Label runat="server" ID="GroupNum" Text='<%#Eval("GroupNum") %>'></asp:Label>
                        </td>
                        <td style="text-align:center">
                            <asp:Label runat="server" ID="VendorBank" Text='<%#Eval("VendorBank") %>'></asp:Label>
                        </td>
                        <td style="text-align:center">
                            <asp:Label runat="server" ID="DfiAccount" Text='<%#Eval("DfiAccount") %>'></asp:Label>
                        </td>
                        <td style="text-align:center">
                            <asp:Label runat="server" ID="Label1" Text='<%#Eval("SWIFT") %>'></asp:Label>
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
        <asp:TextBox ID="VendorCode" runat="server" style="display:none;"></asp:TextBox>
        <asp:TextBox ID="VendorName" runat="server" style="display:none;"></asp:TextBox>
        <asp:TextBox ID="VendorDesp" runat="server" style="display:none;"></asp:TextBox>
        <asp:TextBox ID="Currency" runat="server" style="display:none;"></asp:TextBox>
          <asp:TextBox ID="GroupNum" runat="server" style="display:none;"></asp:TextBox>
        <asp:TextBox ID="VendorBank" runat="server" style="display:none;"></asp:TextBox>
        <asp:TextBox ID="DfiAccount" runat="server" style="display:none;"></asp:TextBox>
        <asp:TextBox ID="Swift" runat="server" style="display:none;"></asp:TextBox>
        </div>
        <div class="center">
            <asp:Button ID="btnSave" runat="server" Text="OK" onClientClick="SinglePersonConfirm()"   CssClass="btn btn-primary " />
            <input type="button" value="Cancle" class="btn" onclick="CloseForm()" />
        </div>
    </div>
    </form>
</body>
</html>
