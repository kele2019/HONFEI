<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ProcessConfig.aspx.cs" Inherits="MobileClientBackground.ProcessConfig" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Mobile phone approval background configuration</title>
    <script src="Script/jquery-1.8.3.min.js" type="text/javascript"></script>
    <script src="Script/My97DatePicker/WdatePicker.js" type="text/javascript"></script>
    <link href="Script/My97DatePicker/skin/WdatePicker.css" rel="stylesheet" type="text/css" />
    <link href="Css/bootstrap.min.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server" class="well form-search">
        <table class="table table-bordered">
            <tr>
                <td width="150px"><%= Resources.Resource.Default_Search_ProcessName %></td>
                <td><asp:Label ID="lbProcessName" runat="server"></asp:Label></td>
                <td>&nbsp;</td>
            </tr>
            <tr>
                <td><%= Resources.Resource.ProcessConfig_LoGo %></td>
                <td>
                    <asp:Label ID="lbLogo" runat="server"></asp:Label>
                    <a href="javascript:void(0)" target="_blank" class="btn" id="viewimage" runat="server"><%= Resources.Resource.ProcessConfig_ViewImage%></a>
                    <script type="text/javascript" language="javascript">
                        $(document).ready(function () {
                            $("#viewimage").attr("href", "UpLoadFile/" + $("#lbLogo").text());
                        });
                    </script>
                </td>
                <td>
                    <asp:Button ID="Button1" runat="server" Text="<%$ Resources:Resource,ProcessConfig_UpLoadButton %>" 
                        CssClass="btn btn-primary"  OnClientClick="return CheckUpLoadFileType()" onclick="Button1_Click"/>
                    <asp:FileUpload ID="FileUpload1" runat="server" CssClass="input-file"/>
                    <script type="text/javascript" language="javascript">
                        function CheckUpLoadFileType() {
                            var filetype = $("#FileUpload1").val().substring($("#FileUpload1").val().indexOf(".") + 1).toLowerCase();
                            var systemFileType = "<%= LoGoFileType %>";
                            if (systemFileType.indexOf(filetype) < 0 || filetype == "") {
                                alert("<%= Resources.Resource.ProcessConfig_CheckFileMessage1 %>" + systemFileType + "<%= Resources.Resource.ProcessConfig_CheckFileMessage2 %>");
                                return false;
                            } else {
                                return true;
                            }
                        }
                    </script>
                </td>
            </tr>
            <tr>
                <td colspan="3">
                    <a href="javascript:void(0);" class="btn" onclick="OpenConfigStepPage()"><%= Resources.Resource.ProcessConfig_ConfigStep %></a>
                    <script type="text/javascript" language="javascript">
                        function OpenConfigStepPage() {
                            location.href = "ProcessStepList.aspx?ID=" + $("#ProcessID").val() + "&ProcessName=" + encodeURI($("#lbProcessName").text()) + "";
                        }
                    </script>
                    <a class="btn" href="Default.aspx"><%= Resources.Resource.FormConfiguration_List_GoBackButton %></a>
                </td>
            </tr>
        </table>
        <asp:HiddenField runat="server" ID="ProcessID"/>
    </form>
</body>
</html>