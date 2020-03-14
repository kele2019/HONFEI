<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Item.aspx.cs" Inherits="Presale.Process.FixedAssetApplication.Item" %>
<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <base target="_self"></base>
    <meta http-equiv="Expires" content="-1" />
    <meta http-equiv="PRAGMA" content="NO-CACHE" />
    <meta http-equiv="Cache-Control" content="no-cache" />
    <title>ItemDetails</title>
    <script type="text/javascript" src="/Assets/js/common.js"></script>
    <script type="text/javascript">
        function getButtonCheck(obj) {
            var ItemCodeValue = $(obj).parent().next().children().text();
            $("#ItemCodeValue").val(ItemCodeValue);
            var ItemNameValue = $(obj).parent().next().next().children().text();
            $("#ItemNameValue").val(ItemNameValue);
        }
        function SinglePersonConfirm() {
            var returnJson = "[{'ItemCodeValue':'";
            returnJson += $("#ItemCodeValue").val();
            returnJson += "'},{'ItemNameValue':'";
            returnJson += $("#ItemNameValue").val();
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
        <table class="table table-condensed table-bordered">
            <tr>
                <td class="td-label">
                    <p style="text-align:center">ItemCode</p>
                </td>
                <td class="td-content">
                    <asp:TextBox runat="server" ID="code"></asp:TextBox>
                </td>
                <td class="td-label">
                    <p style="text-align:center">ItemName</p>
                </td>
                <td class="td-content">
                    <asp:TextBox runat="server" ID="name"></asp:TextBox>
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
                <th style="Width:2%;"></th>
                <th style="Width:49%;">ItemCode</th>
                <th style="Width:49%;">ItemName</th>
            </tr>
            <tbody>
            <asp:Repeater runat="server" ID="ItemList">
                <ItemTemplate>
                    <tr>
                        <td style="text-align:center">
                            <%--<asp:CheckBox runat="server" onclick="getButtonCheck(this)"/>--%>
                            <input type="radio" id="rbtnSelect" name ="FlowCode" runat="server" onclick="selectSingleRadio(this,'FlowCode');" />
                        </td>
                        <td style="text-align:center">
                            <asp:Label runat="server" ID="ItemCode" Text='<%#Eval("ItemCode") %>'></asp:Label>
                        </td>
                        <td style="text-align:center">
                            <asp:Label runat="server" ID="ItemName" Text='<%#Eval("ItemName") %>'></asp:Label>
                        </td>
                    </tr>
                </ItemTemplate>
            </asp:Repeater>
            </tbody>
            <tfoot>
                    <tr>
                    <td colspan="3">
                      <webdiyer:aspnetpager id="AspNetPager1" runat="server" custominfohtml="Count %RecordCount%"
                        horizontalalign="right" width="100%" cssclass="aspNetPager" onpagechanged="AspNetPager1_PageChanged"
                        pagesize="10" alwaysshow="true" submitbuttonstyle="display:none" inputboxstyle="display:none"
                        nextpagetext="Next" firstpagetext="Home" lastpagetext="Last" prevpagetext="Prev">
                    </webdiyer:aspnetpager>
                    </td>
                    </tr>
                    </tfoot>
        </table>
        <asp:TextBox ID="ItemCodeValue" runat="server" style="display:none;"></asp:TextBox>
        <asp:TextBox ID="ItemNameValue" runat="server" style="display:none;"></asp:TextBox>
        </div>
        <div class="center">
            <asp:Button ID="btnSave" runat="server" Text="OK" onClientClick="SinglePersonConfirm()"   CssClass="btn btn-primary " />
            <input type="button" value="Cancle" class="btn" onclick="CloseForm()"   />
        </div>
    </div>
    </form>
</body>
</html>
