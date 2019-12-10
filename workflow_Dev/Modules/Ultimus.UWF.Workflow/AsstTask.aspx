<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AsstTask.aspx.cs" Inherits="Ultimus.UWF.Workflow.AsstTask" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>协办/抄送</title>
    <base target="_self"></base>
    <meta http-equiv="Expires" content="-1" />
    <meta http-equiv="PRAGMA" content="NO-CACHE" />
    <meta http-equiv="Cache-Control" content="no-cache" />
    <script type="text/javascript" src="<%=Ultimus.UWF.Common.Logic.WebUtil.GetRootPath() %>/Assets/js/common.js"></script>
    <style>
        .btn
        {
            font-size: 9pt;
            font-family: 宋体;
        }
    </style>
    <script>
        function selectUser() {
            var val;
            val = window.showModalDialog(path + "/Modules/Ultimus.UWF.OrgChart/SelectUserFromAD.aspx?Type=2", null, "dialogWidth=900px;dialogHeight=500px");
            if (!val) {
                val = window.returnValue;
            }
            if (val) {
                var obj = eval(val);
                var names = "";
                var ids = "";
                if (obj) {
                    for (i = 0; i < obj.length; i++) {
                        if (i == 0) {
                            names += obj[i].Name.replace("[USER]","");
                            ids += obj[i].LoginName;
                        }
                        else {
                            names += "," + obj[i].Name.replace("[USER]", "");
                            ids += "," + obj[i].LoginName;
                        }
                    }
                }


                $("#txtUsers").val(names);
                $("#txtUserId").val(ids);
            }

            return false;
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <script type="text/javascript" language="javascript">
        $().ready(function () {
            $("#fileinfo td").each(function () {
                $(this).css("text-align", "center");
            });
        })
    </script>
    <table class="table table-condensed  ">
        <tr id="uploadrow" runat="server">
            <td>
                选择人员:
            </td>
            <td>
                <asp:TextBox ID="txtUsers" runat="server" onfocus="this.blur()"></asp:TextBox>
                <asp:Button ID="Button1" runat="server" Text="..." CssClass="btn" OnClientClick="return selectUser();" />
            </td>
        </tr>
        <tr>
            <td valign="top">
                备注:<font color="red">*</font>
                <br />(必填)
            </td>
            <td>
                <asp:TextBox ID="txtRemark" runat="server" TextMode="MultiLine" Height="116px" Width="90%"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td valign="top">
                 
            </td>
            <td>
                <asp:Button ID="btnOK" runat="server" Text="确定" CssClass="btn btn-primary" 
                    OnClick="btnOK_Click"  />
                <asp:Button ID="btnCancel" runat="server" Text="取消" CssClass="btn" 
                    onclick="btnCancel_Click" OnClientClick="window.close();return false;"  />
            </td>
        </tr>
    </table>
    <div style="display: none;">
        <asp:TextBox ID="txtUserId" runat="server"></asp:TextBox>
        <asp:TextBox ID="txtProcessName" runat="server"></asp:TextBox>
        <asp:TextBox ID="txtTaskId" runat="server"></asp:TextBox>
    </div>
    </form>
</body>
</html>
