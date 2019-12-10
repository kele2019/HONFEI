<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AttachmentControl.aspx.cs" Inherits="Ultimus.UWF.Workflow.AttachmentControl" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
        <script type="text/javascript" src="<%=Ultimus.UWF.Common.Logic.WebUtil.GetRootPath() %>/Assets/js/jquery.js"></script>
        <style>
        .btn
        {
            font-size:9pt;
            font-family:宋体;
            }
        body
        {
            font-size:9pt;
            font-family:宋体;
            }
        
        </style>
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
            <asp:FileUpload ID="FilePath" runat="server" CssClass="btn" Width="150" 
                BackColor="White"/> 
            <asp:TextBox ID="FileComments" runat="server" Style="margin: 0; display: none;"></asp:TextBox>
            <asp:Button ID="Button1" runat="server" Text="上传" CssClass="btn" OnClientClick="return CheckFile()"
                OnClick="Button1_Click" BackColor="White" Font-Overline="False" 
                Font-Strikeout="False" ForeColor="Black"  />
            <script type="text/javascript" language="javascript">
                function CheckFile() {
                    if ($("#<%= FilePath.ClientID %>").val() == "") {
                        alert("<%=Ultimus.UWF.Form.ProcessControl.Resources.lang.RequireUpload%>");
                        return false;
                    }
                    
                    return true;
                }

                $().ready(function () {
                    if ($("#Attachments1_txtReadonly").val() == "1") {
                        $("#Attachments1_actionRow").hide();
                    }
                });
            </script>
        </td>
    </tr>
    <tr>
        <td>
            <table class="table table-condensed table-bordered">
                <tr style="display:none">
                    <th>
                        <asp:Label ID="Label2" runat="server" Text="No."></asp:Label>
                    </th>
                    <th>
                        <asp:Label ID="Label3" runat="server" Text="文件名称"><%=Ultimus.UWF.Form.ProcessControl.Resources.lang.FileName%></asp:Label>
                    </th>
                      
                    <th id="actionRow" runat="server" visible='<%# ReadOnly %>'>
                        <asp:Label ID="Label7" runat="server" Text="操作"><%=Ultimus.UWF.Form.ProcessControl.Resources.lang.Operation%></asp:Label>
                    </th>
                </tr>
                <tbody id="fileinfo">
                    <asp:Repeater ID="Repeater1" runat="server" OnItemCommand="Repeater1_ItemCommand">
                        <ItemTemplate>
                            <tr>
                                <td>
                                    <%# Container.ItemIndex+1 %>
                                </td>
                                <td>
                                    <asp:LinkButton ID="LinkButton2" runat="server" CommandArgument='<%# Eval("NEWNAME") %>'
                                        CommandName="Download"><%# Eval("FileName")%></asp:LinkButton>
                                </td>
                                
                                <td id="Td1" runat="server" visible='<%# ReadOnly %>'>
                                    <asp:LinkButton ID="LinkButton1" runat="server" Visible='<%# ReadOnly %>' OnClientClick="return confirm('确定要删除吗?');"
                                        CommandArgument='<%# Eval("NEWNAME") %>' CommandName="Delete"><%=Ultimus.UWF.Form.ProcessControl.Resources.lang.Delete%></asp:LinkButton>
                                </td>
                            </tr>
                        </ItemTemplate>
                    </asp:Repeater>
                </tbody>
            </table>
        </td>
    </tr>
</table>
<div style="display: none;">
    <asp:TextBox ID="txtMust" runat="server"></asp:TextBox>
    <asp:TextBox ID="txtProcessName" runat="server"></asp:TextBox>
    <asp:TextBox ID="txtIncident" runat="server"></asp:TextBox>
    <asp:TextBox ID="txtType" runat="server"></asp:TextBox>
    <asp:TextBox ID="txtFormId" runat="server"></asp:TextBox>
    <asp:TextBox ID="txtTaskUser" runat="server"></asp:TextBox>
    <asp:TextBox ID="txtISWord" runat="server"></asp:TextBox>
    <asp:TextBox ID="txtHasAtt" runat="server"></asp:TextBox>
    <asp:TextBox ID="txtReadonly" runat="server"></asp:TextBox>
</div>

    </form>
</body>
</html>
