<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SelectAllUser.aspx.cs" Inherits="Ultimus.UWF.OrgChart.SelectAllUser" %>
<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>
<%@ Import Namespace="Ultimus.UWF.Common.Logic" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <base target="_self"></base>
    <meta http-equiv="Expires" content="-1" />
    <meta http-equiv="PRAGMA" content="NO-CACHE" />
    <meta http-equiv="Cache-Control" content="no-cache" />
    <title>Select User</title>
     
    <script type="text/javascript" src="<%=WebUtil.GetRootPath()%>/Assets/js/common.js"></script>
    <script type="text/javascript">
        $(function () {
            $("input[id$=RadioButton1]").attr("name", "username");
        });
        function SinglePersonConfirm() {
            var returnJson = "";
            returnJson += "[";

            var isok = false;
            $("#tbody").find("tr").each(function () {
                if ($(this).find("td:eq(0)").children().attr("checked")) {
                    returnJson += "{'Name':'" + $.trim($(this).find("td:eq(2)").text()) + "',";
                    returnJson += "'Type':'USER',";
                    returnJson += "'LoginName':'" + $.trim($(this).find("td:eq(4)").text()) + "',";
                    returnJson += "'ID':'" + $.trim($(this).find("td:eq(5)").children().val()) + "'},";
                }
            });

            //returnJson = returnJson + tnJson;
            if (returnJson.lastIndexOf(",") > 0) {
                returnJson = returnJson.substring(0, returnJson.lastIndexOf(","));
            }

            returnJson += "]";
            window.returnValue = returnJson;
            window.close();
        }
        function CloseForm() {
            window.close();
        }
    </script>
</head>
<body style="overflow-x: hidden; overflow-y: hidden;">
    <form id="form1" runat="server" defaultbutton="btnSearch">
    <div>
            <div class="pt10 pb10 pr10" style="margin-bottom:20px; margin-top:20px;">
            &nbsp;&nbsp;&nbsp;&nbsp;
            <asp:TextBox ID="txtSearch" runat="server" Width="500"></asp:TextBox>&nbsp;&nbsp;&nbsp;&nbsp;
            <asp:Button ID="btnSearch" runat="server" Text="Search" CssClass="btn btn-primary" OnClick="btnSearch_Click" /></div>
     <div style="float: left; width: 99%; height: 370px;">
            <div style="overflow-x: hidden; overflow-y: hidden; height: 370px;" id="divUser"
                runat="server">
               
                <table class=" table table-hover table-bordered table-condensed listTable" style="width: 99%;
                    overflow: hidden;">
                    <thead>
                        <tr class="bg">
                            <th width="10%">
                                Option
                            </th>
                            <th width="30%" style="display:none;">
                                UserName
                            </th>
                            <th width="30%">
                                EnlishName
                            </th>
                            <th width="30%">
                               Email
                            </th>
                        </tr>
                    </thead>
                    <tbody id="tbody">
                        <asp:Repeater ID="Repeater1" runat="server"  >
                            <ItemTemplate>
                                <tr id='<%# Container.ItemIndex+1 %>' class="TableDataRow">
                                    <td width="29px">
                                        <asp:CheckBox ID="CheckBox1" runat="server"   Visible="false" />
                                        <asp:RadioButton ID="RadioButton1" runat="server" GroupName="username" />
                                    </td>
                                    <td width="148px" style="display:none;">
                                        <asp:Label ID="Label1" runat="server" Text='<%# Eval("UserName") %>'></asp:Label>
                                    </td>
                                    <td width="156px">
                                        <asp:Label ID="Label2" runat="server" Text='<%# Eval("EXT04") %>'></asp:Label>
                                    </td>
                                    <td width="212px">
                                        <asp:Label ID="Label3" runat="server" Text='<%# Eval("Email") %>'></asp:Label>
                                    </td>
                                    <td style="display: none;" >
                                        <asp:Label ID="UserAccount" runat="server" Text='<%# Eval("EmployeeID") %>' />
                                    </td>
                                    <td style="display: none;">
                                        <asp:HiddenField ID="UserID" runat="server" Value='<%# Eval("UserID") %>' />
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
            </div>
            </div>
              <div class="center">
             <asp:Button ID="btnOK" runat="server" Text="OK"   CssClass="btn btn-primary "
                 Style="height: 25px;" OnClientClick="return SinglePersonConfirm('');" />
                 &nbsp;&nbsp;&nbsp;
             <input type="button" value="Close" class="btn" onclick="CloseForm()" style="height: 25px;" />
             </div>
    </div>
    </form>
</body>
</html>
