<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="POList.aspx.cs" Inherits="Presale.Process.PaymentRequestForm.POList" %>

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
        <p style="font-weight:bold">Search Information</p>
        <table class="table table-condensed table-bordered" >
                <tr>
                    <td class="td-label">PO No.</td>
                    <td class="td-content"> 
                    <asp:TextBox runat="server" ID="txtPONo"></asp:TextBox>
                    </td>
                    <td class="td-label">GR No.</td>
                    <td class="td-content">
                    <asp:TextBox runat="server" ID="txtGRNo"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td style="text-align:center" colspan="4">
                        <asp:Button runat="server" CssClass="btn" ID="btnSearch" Text="Search" onclick="btnSearch_Click" style="float:right"/>
                    </td>
                 </tr>
            </table>
        <table class="table table-condensed table-bordered">
            <tr>
                <th width="9%"></th>
                <th width="10%" style="text-align:center">PO No.</th>
                <th width="30%" style="text-align:center">PO Remark</th>
                <th width="10%" style="text-align:center">GR No.</th>
                <th width="30%" style="text-align:center">GR Remark</th>
                <th width="10%" style="text-align:center">GR Total</th>
            </tr>
            <tbody>
            <asp:Repeater runat="server" ID="purchaseList">
                <ItemTemplate>
                    <tr>
                        <td style="text-align:center">
                            <%--<asp:CheckBox runat="server" onclick="getButtonCheck(this)"/>--%>
                            <input type="radio" id="rbtnSelect" name ="FlowCode" runat="server" onclick="selectSingleRadio(this,'FlowCode');" />

                        </td>
                        <td   style="text-align:center">
                            <asp:Label runat="server" ID="PONum" Text='<%#Eval("PONum") %>'></asp:Label>
                        </td>
                        <td style="text-align:center">
                            <asp:Label runat="server" ID="PORemark" Text='<%#Eval("PORemark") %>'></asp:Label>
                        </td>
                        <td style="text-align:center">
                            <asp:Label runat="server" ID="PDNum" Text='<%#Eval("PDNum") %>'></asp:Label>
                        </td>
                        <td style="text-align:center">
                            <asp:Label runat="server" ID="PDRemark" Text='<%#Eval("PDRemark") %>'></asp:Label>
                        </td>
                       
                        <td style="text-align:center">
                            <asp:Label runat="server" ID="PDDocTotal" Text='<%#Eval("PDDocTotal") %>'></asp:Label>
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
