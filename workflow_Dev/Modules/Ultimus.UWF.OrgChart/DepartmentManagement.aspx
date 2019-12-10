<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="DepartmentManagement.aspx.cs"
    Inherits="Ultimus.UWF.OrgChart.DepartmentManagement" %>
<%@ Import Namespace="Ultimus.UWF.Common.Logic" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <meta http-equiv="Pragma" content="no-cache">
    <meta http-equiv="Cache-Control" content="no-cache">
    <meta http-equiv="Expires" content="0">
    <base target="_self" />
     <script type="text/javascript" src="<%=WebUtil.GetRootPath()%>/Assets/js/common.js"></script>
</head>
<body>
    <form id="form1" runat="server">
    <div class="pt10">
        <div class="row-fluid">
            <div class="span4">
               

                <div style="overflow: scroll; border: 1px solid; height: 400px;" >
                    <asp:TreeView ID="tvResource" runat="server" ShowExpandCollapse="true" 
                        ShowLines="true" onselectednodechanged="tvResource_SelectedNodeChanged">
                        <NodeStyle BorderStyle="None"  ImageUrl="../../assets/images/DepartIcon.png" />
                <SelectedNodeStyle Font-Bold="True" ImageUrl="../../assets/images/DepartIcon.png" />
                    </asp:TreeView>
                </div>
                <asp:Button ID="btnAdd" runat="server" Text="新增下级" visible="false" CssClass="btn" 
                    onclick="btnAdd_Click" />

                    <asp:Button ID="btnAddSame" runat="server" Text="复制" visible="false" CssClass="btn" 
                    onclick="btnAddSame_Click" />
            </div>
            <div class="span4" id="divInfo" runat="server" visible="false">
                <table class="table table-condensed table-bordered">
                    <tr>
                        <td class="banner" colspan="2">
                            基本信息
                        </td>
                    </tr>
                    <tr>
                        <td class="td-label">
                            部门ID：
                        </td>
                        <td class="td-content">
                            <asp:TextBox ID="txtResourceId" ReadOnly="true" runat="server"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="td-label">
                            上级部门ID：
                        </td>
                        <td class="td-content">
                            <asp:TextBox ID="txtParentId" runat="server"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="td-label">
                            部门名称：
                        </td>
                        <td class="td-content">
                            <asp:TextBox ID="txtCNName" runat="server"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="td-label">
                            英文名称：
                        </td>
                        <td class="td-content">
                            <asp:TextBox ID="txtExt30" runat="server"></asp:TextBox>
                        </td>
                    </tr>

                    <tr>
                        <td class="td-label">
                            部门类型：
                        </td>
                        <td class="td-content">
                            <asp:TextBox ID="txtType" runat="server"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="td-label">
                            虚拟部门：
                        </td>
                        <td class="td-content">
                            <asp:CheckBox ID="cbxExt29" runat="server"></asp:CheckBox>
                        </td>
                    </tr>


                </table>
                <asp:Button ID="btnSave" runat="server" Text="保存" CssClass="btn btn-primary" onclick="btnSave_Click"                      />
                <asp:Button ID="btnDelete" runat="server" Text="删除" CssClass="btn " 
                    onclick="btnDelete_Click"        OnClientClick="return confirm('您确定要删除吗?');"               />
            </div>
            <div class="span4" style="overflow: scroll; border: 1px solid; height: 420px;"  id="divExt" runat="server" visible="false">
                <table class="table table-condensed table-bordered">
                    <tr>
                        <td class="banner" colspan="2">
                            扩展信息
                        </td>
                    </tr>
                    <tr>
                        <td class="td-label">
                            <asp:Label ID="lblExt1" runat="server" Text="扩展1："></asp:Label>
                        </td>
                        <td class="td-content">
                            <asp:TextBox ID="txtExt01" runat="server"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="td-label">
                            <asp:Label ID="lblExt2" runat="server" Text="扩展2："></asp:Label>
                            
                        </td>
                        <td class="td-content">
                            <asp:TextBox ID="txtExt02" runat="server"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="td-label">
                            <asp:Label ID="lblExt3" runat="server" Text="扩展3："></asp:Label>
                            
                        </td>
                        <td class="td-content">
                            <asp:TextBox ID="txtExt03" runat="server"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="td-label">
                            <asp:Label ID="lblExt4" runat="server" Text="扩展4："></asp:Label>
                            
                        </td>
                        <td class="td-content">
                            <asp:TextBox ID="txtExt04" runat="server"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="td-label">
                            <asp:Label ID="lblExt5" runat="server" Text="扩展5："></asp:Label>
                           
                        </td>
                        <td class="td-content">
                            <asp:TextBox ID="txtExt05" runat="server"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="td-label">
                            <asp:Label ID="lblExt6" runat="server" Text="扩展6："></asp:Label>
                           
                        </td>
                        <td class="td-content">
                            <asp:TextBox ID="txtExt06" runat="server"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="td-label">
                            <asp:Label ID="lblExt7" runat="server" Text="扩展7："></asp:Label>
                            
                        </td>
                        <td class="td-content">
                            <asp:TextBox ID="txtExt07" runat="server"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="td-label">
                            <asp:Label ID="lblExt8" runat="server" Text="扩展8："></asp:Label>
                            
                        </td>
                        <td class="td-content">
                            <asp:TextBox ID="txtExt08" runat="server"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="td-label">
                            <asp:Label ID="lblExt9" runat="server" Text="扩展9："></asp:Label>
                            
                        </td>
                        <td class="td-content">
                            <asp:TextBox ID="txtExt09" runat="server"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="td-label">
                            <asp:Label ID="lblExt10" runat="server" Text="扩展10："></asp:Label>
                            
                        </td>
                        <td class="td-content">
                            <asp:TextBox ID="txtExt10" runat="server"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="td-label">
                            <asp:Label ID="lblExt11" runat="server" Text="扩展11："></asp:Label>
                            
                        </td>
                        <td class="td-content">
                            <asp:TextBox ID="txtExt11" runat="server"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="td-label">
                            <asp:Label ID="lblExt12" runat="server" Text="扩展12："></asp:Label>
                            
                        </td>
                        <td class="td-content">
                            <asp:TextBox ID="txtExt12" runat="server"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="td-label">
                            <asp:Label ID="lblExt13" runat="server" Text="扩展13："></asp:Label>
                            
                        </td>
                        <td class="td-content">
                            <asp:TextBox ID="txtExt13" runat="server"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="td-label">
                            <asp:Label ID="lblExt14" runat="server" Text="扩展14："></asp:Label>
                           
                        </td>
                        <td class="td-content">
                            <asp:TextBox ID="txtExt14" runat="server"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="td-label">
                            <asp:Label ID="lblExt15" runat="server" Text="扩展15："></asp:Label>
                           
                        </td>
                        <td class="td-content">
                            <asp:TextBox ID="txtExt15" runat="server"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="td-label">
                            <asp:Label ID="lblExt16" runat="server" Text="扩展16："></asp:Label>
                            
                        </td>
                        <td class="td-content">
                            <asp:TextBox ID="txtExt16" runat="server"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="td-label">
                            <asp:Label ID="lblExt17" runat="server" Text="扩展17："></asp:Label>
                            
                        </td>
                        <td class="td-content">
                            <asp:TextBox ID="txtExt17" runat="server"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="td-label">
                            <asp:Label ID="lblExt18" runat="server" Text="扩展18："></asp:Label>
                            
                        </td>
                        <td class="td-content">
                            <asp:TextBox ID="txtExt18" runat="server"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="td-label">
                            <asp:Label ID="lblExt19" runat="server" Text="扩展19："></asp:Label>
                            
                        </td>
                        <td class="td-content">
                            <asp:TextBox ID="txtExt19" runat="server"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="td-label">
                            <asp:Label ID="lblExt20" runat="server" Text="扩展20："></asp:Label>
                            
                        </td>
                        <td class="td-content">
                            <asp:TextBox ID="txtExt20" runat="server"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="td-label">
                            <asp:Label ID="lblExt21" runat="server" Text="扩展21："></asp:Label>
                            
                        </td>
                        <td class="td-content">
                            <asp:TextBox ID="txtExt21" runat="server"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="td-label">
                            <asp:Label ID="lblExt22" runat="server" Text="扩展22："></asp:Label>
                            
                        </td>
                        <td class="td-content">
                            <asp:TextBox ID="txtExt22" runat="server"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="td-label">
                            <asp:Label ID="lblExt23" runat="server" Text="扩展23："></asp:Label>
                            
                        </td>
                        <td class="td-content">
                            <asp:TextBox ID="txtExt23" runat="server"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="td-label">
                            <asp:Label ID="lblExt24" runat="server" Text="扩展24："></asp:Label>
                            
                        </td>
                        <td class="td-content">
                            <asp:TextBox ID="txtExt24" runat="server"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="td-label">
                            <asp:Label ID="lblExt25" runat="server" Text="扩展25："></asp:Label>
                            
                        </td>
                        <td class="td-content">
                            <asp:TextBox ID="txtExt25" runat="server"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="td-label">
                            <asp:Label ID="lblExt26" runat="server" Text="扩展26："></asp:Label>
                            
                        </td>
                        <td class="td-content">
                            <asp:TextBox ID="txtExt26" runat="server"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="td-label">
                            <asp:Label ID="lblExt27" runat="server" Text="扩展27："></asp:Label>
                            
                        </td>
                        <td class="td-content">
                            <asp:TextBox ID="txtExt27" runat="server"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="td-label">
                            <asp:Label ID="lblExt28" runat="server" Text="扩展28："></asp:Label>
                            
                        </td>
                        <td class="td-content">
                            <asp:TextBox ID="txtExt28" runat="server"></asp:TextBox>
                        </td>
                    </tr>
                     
                </table>
            </div>
        </div>
        <div style="display: none;">
            <asp:TextBox ID="txtShowExt" runat="server"></asp:TextBox>
            <asp:TextBox ID="txtNew" runat="server"></asp:TextBox>
        </div>
    </form>
    <script type="text/javascript">
        $(document).ready(function () {
            //parent.document.getElementById('frmContent').height = 1000;
        });
    </script>
</body>
</html>
