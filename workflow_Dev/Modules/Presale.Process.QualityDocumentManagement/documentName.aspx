<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="documentName.aspx.cs" Inherits="Presale.Process.QualityDocumentManagement.documentName" %>
<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>DocumentDetail</title>
    <base target="_self"/>
    <meta http-equiv="Expires" content="-1" />
    <meta http-equiv="PRAGMA" content="NO-CACHE" />
    <meta http-equiv="Cache-Control" content="no-cache" />
    <script type="text/javascript" src="/Assets/js/common.js"></script>
    <script type="text/javascript">
        function getButtonCheck(obj) {
            var documentName = $(obj).parent().next().children().text();
            $("#documentNameValue").val(documentName);
            var documentUrl = $(obj).parent().next().next().next().children().text();
            $("#documentUrl").val(documentUrl);
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
        function SinglePersonConfirm() {
            var returnJson = "[{'documentName':'";
            returnJson += $("#documentNameValue").val();
            returnJson += "'},{'documentUrl':'";
            returnJson += $("#documentUrl").val();
            returnJson += "'}]";
            //            alert(returnJson);
            window.returnValue = returnJson;
            window.close();
        }
        function CloseForm() {
            window.close();
        }
    </script>
</head>
<body>
    <form id="form1" runat="server" style="width:95%;margin-top:5%;margin-left:2.5%">
        <div>
            <div>
                <table class=" table table-hover table-bordered table-condensed listTable" style="width: 99%;overflow: hidden;">
                    <thead>
                        <tr class="bg">
                            <th width="5%">No.</th>
                            <%--<th width="10%">Title</th>--%>
                            <th width="35%">Name</th>
                            <th width="30%">OwnerLoginName</th>
                            <th width="30%">Url</th>
                        </tr>
                    </thead>
                    <tbody id="tbody">
                        <asp:Repeater ID="Repeater1" runat="server"  >
                            <ItemTemplate>
                                <tr id='<%# Container.ItemIndex+1 %>' class="TableDataRow">
                                    <td>
                                        <input type="radio" id="rbtnSelect" name ="FlowCode" runat="server" onclick="selectSingleRadio(this,'FlowCode');" />
                                    </td>
                                    <%--<td width="148px">
                                        <asp:Label ID="Title" runat="server" Text='<%# Eval("Title") %>'></asp:Label>
                                    </td>--%>
                                    <td>
                                        <asp:Label ID="Name" runat="server" Text='<%# Eval("Name") %>'></asp:Label>
                                    </td>
                                    <td>
                                        <asp:Label ID="OwnerLoginName" runat="server" Text='<%# Eval("OwnerLoginName") %>'></asp:Label>
                                    </td>
                                    <td>
                                        <asp:Label ID="Url" runat="server" Text='<%# Eval("Url") %>' />
                                    </td>
                                    <%--<td>
                                        <asp:HiddenField ID="CreateDate" runat="server" Value='<%# Eval("CreateDate") %>' />
                                    </td>
                                    <td>
                                        <asp:HiddenField ID="CreateUserLoginName" runat="server" Value='<%# Eval("CreateUserLoginName") %>' />
                                    </td>
                                    <td>
                                        <asp:HiddenField ID="UpdateDate" runat="server" Value='<%# Eval("UpdateDate") %>' />
                                    </td>
                                    <td>
                                        <asp:HiddenField ID="UpdateUserLoginName" runat="server" Value='<%# Eval("UpdateUserLoginName") %>' />
                                    </td>--%>
                                </tr>
                            </ItemTemplate>
                        </asp:Repeater>
                    </tbody>
                    <tfoot>
                        <tr>
                            <td colspan="9">
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
        <asp:TextBox ID="documentNameValue" runat="server" style="display:none;"></asp:TextBox>
        <asp:TextBox ID="documentUrl" runat="server" style="display:none;"></asp:TextBox>
        <div class="center">
             <asp:Button ID="btnOK" runat="server" Text="OK"   CssClass="btn btn-primary " Style="height: 25px;" OnClientClick="return SinglePersonConfirm('');" />&nbsp;&nbsp;&nbsp;
             <input type="button" value="Close" class="btn" onclick="CloseForm()" style="height: 25px;" />
        </div>
    </form>
</body>
</html>

