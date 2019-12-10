<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="GridImport.aspx.cs" Inherits="Ultimus.UWF.Office.GridImport" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <base target="_self"></base>
    <script type="text/javascript" src="<%=Ultimus.UWF.Common.Logic.WebUtil.GetRootPath() %>/Assets/js/common.js"></script>
    <script type="text/javascript" language="javascript">

        function CheckPage() {
            var value = document.getElementById("FileUpload1").value;
            value = value.substring(value.lastIndexOf("."), value.length);
            if (value != ".xls" && value != ".xlsx") {
                alert("请选择.xls或者.xlsx的Excel文件");
                return false;
            } else {
                return true;
            }
        }

        function ClearRow(varName) {
            window.opener.ClearRow(varName);
        }

        function AddNew(varName) {
            window.opener.AddNewRow(varName);
        }

        function ReturnValue(varName, names,values,lines) {

            window.opener.SetImport(varName, names, values, lines);
            alert("导入成功!");
            window.close();
        }

        function refreshData() {
            window.opener.refreshData();
            window.close();
        }
    </script>
</head>
<body style="overflow: hidden;">
    <form id="form1" runat="server">
    <asp:HiddenField ID="ImportDate" runat="server" />
    <div class="row" style="padding-left:10px">
        <table class="listTable" border="0" cellpadding="0" cellspacing="0">
            <tr>
                <td colspan="4">
                    <h5 class="titleBG">
                        文件选择</h5>
                </td>
            </tr>
            <tr>
                <td colspan="3" style="white-space: nowrap; word-break: break-all;">
                    <asp:FileUpload ID="FileUpload1" runat="server" CssClass="btn" Width="100%" />
                </td>
                <td >
                &nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:Button ID="Button2" runat="server" Text="上传" CssClass="btn btn-primary" OnClientClick="return CheckPage()"
                        OnClick="Button2_Click" />
                </td>
            </tr>
            <tr>
                <td colspan="4">
                </td>
            </tr>
        </table>
    </div>
    <div style="display: none">
        <asp:TextBox ID="txtColName" runat="server"></asp:TextBox>
        <asp:TextBox ID="txtGrid" runat="server"></asp:TextBox>
        <asp:TextBox ID="txtVarName" runat="server"></asp:TextBox>
        <asp:Label ID="MsgessLabel" runat="server"></asp:Label></div>
    </form>
</body>
</html>
