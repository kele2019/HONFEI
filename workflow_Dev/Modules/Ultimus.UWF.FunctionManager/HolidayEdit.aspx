<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="HolidayEdit.aspx.cs" Inherits="Ultimus.UWF.FunctionManager.HolidayEdit" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Work Holiday Edit</title>
    <script type="text/javascript" src="/Assets/js/common.js"></script>
    <script src="/Assets/Layer/layer.js" type="text/javascript"></script>
    <link href="/Assets/Layer/skin/default/layer.css" rel="stylesheet" type="text/css" />

    <script type="text/javascript">
        function ClosePage() {
            var index = parent.layer.getFrameIndex(window.name);
            parent.location.reload(); 
            parent.layer.close(index);
        }
        function CompleteData(obj) {
            if (obj == "0") {
                layer.msg('This Date Exits,Pls Confirm');
            }
            else {
                if (obj == "1") {

                    layer.msg('Save Sucess');
                    ClosePage();
                }
                else {
                    layer.msg(obj);
                }
            }
        }
        function ValildData() {
            if ($("#txtHolidayDate").val() == "") {
                layer.alert('Pls input Date')
               // layer.msg('Save Sucess');
                return false;
            }
            else {
                return true;
            }
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
     <div id="myDiv" class="container">
    <table class="table table-condensed table-bordered">
    <tr><td colspan="2" style="text-align:center; font-size:larger; font-weight:bolder;">Edit Data</td></tr>
    <tr>
    <th width="35%">Date:</th>
    <td><asp:TextBox runat="server" ID="txtHolidayDate"  onclick="WdatePicker({dateFmt: 'yyyy-MM-dd'})"></asp:TextBox></td>
    </tr>
    <tr>
      <th>Type:</th>
    <td>
    <asp:DropDownList runat="server" ID="dropWorkType">
    <asp:ListItem Value="Holiday">Holiday</asp:ListItem>
    <asp:ListItem Value="Work">Work</asp:ListItem>
    </asp:DropDownList>
    </td>
    </tr>
    <tr>
    <td colspan="2" style="text-align:center">
    <input type="button" value="test"  onclick="ClosePage()"/>
    <asp:Button runat="server" ID="btnSave" Text="Save" CssClass="btn" 
            onclick="btnSave_Click" OnClientClick="return ValildData()" />
    
    </td>
    </tr>
    </table>
    </div>
    </form>
</body>
</html>
