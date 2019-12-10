<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CostCenter.aspx.cs" Inherits="Presale.Process.TravelExpenseReport.CostCenter" %>
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
            var cosetcenter = $(obj).parent().next().children().text();
            $("#cosetcenter").val(cosetcenter);
            var Description = $(obj).parent().next().next().children().text();
            $("#Description").val(Description);
        }
        function SinglePersonConfirm() {
            var returnJson = "[{'cosetcenter':'";
            returnJson += $("#cosetcenter").val();
            returnJson += "'},{'Description':'";
            returnJson += $("#Description").val();
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
                <th style="Width:2%;"></th>
                <th style="Width:49%;">CostCenter</th>
                <th style="Width:49%;">Description</th>
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
                            <asp:Label runat="server" ID="cosetcenter" Text='<%#Eval("cosetcenter") %>'></asp:Label>
                        </td>
                        <td style="text-align:center">
                            <asp:Label runat="server" ID="Description" Text='<%#Eval("Description") %>'></asp:Label>
                        </td>
                    </tr>
                </ItemTemplate>
            </asp:Repeater>
            </tbody>
        </table>
        <asp:TextBox ID="cosetcenter" runat="server" style="display:none;"></asp:TextBox>
        <asp:TextBox ID="Description" runat="server" style="display:none;"></asp:TextBox>
        </div>
        <div class="center">
            <asp:Button ID="btnSave" runat="server" Text="OK" onClientClick="SinglePersonConfirm()"   CssClass="btn btn-primary " />
            <input type="button" value="Cancle" class="btn" onclick="CloseForm()" style="height: 25px;" />
        </div>
    </div>
    </form>
</body>
</html>
