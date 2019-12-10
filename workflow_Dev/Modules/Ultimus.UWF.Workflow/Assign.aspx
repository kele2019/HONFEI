<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Assign.aspx.cs" Inherits="Ultimus.UWF.Workflow.Assign" %>
<%@ Import Namespace="Ultimus.UWF.Common.Logic" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <base target="_self"></base>
    <title>
        <%=Lang.Get("Assign_Title") %></title>
        <script type="text/javascript" src="<%=Ultimus.UWF.Common.Logic.WebUtil.GetRootPath() %>/Assets/js/common.js"></script>
   <script type="text/javascript" src="<%=Ultimus.UWF.Common.Logic.WebUtil.GetRootPath()%>/Assets/js/selectorNew.js"></script>
    <script type="text/javascript" language="javascript">
        $().ready(function () {
            $("input[type=radio][name=AssignType]").each(function (index) {

                $(this).click(function () {
                    if ($(this).attr("checked") == "checked") {

                        if ($(this).attr("id") == "RadioButton3") {
                            $("#trFuture").css("display", "block");
                            $("tr[idx=trprocessname]").css("display", "none");
                        }
                        else if ($(this).attr("id") == "RadioButton4") {
                            $("#trFuture").css("display", "none");
                            $("tr[idx=trprocessname]").css("display", "block");
                        }
                        else {
                            $("#trFuture").css("display", "none");
                            $("tr[idx=trprocessname]").css("display", "none");
                        }
                    }
                });
            });
        });
    </script>
    <script type="text/javascript" language="javascript">
//        window.moveTo(0, 0);
//        window.resizeTo(screen.availWidth, screen.availHeight);
//        window.outerWidth = screen.availWidth;
//        window.outerHeight = screen.availHeight;        
    </script>
</head>
<body style="overflow: hidden;">
    <form id="form1" runat="server">
    <div>
        <table class="TableData" width="98%">
            <tr>
                <td colspan="3" class="banner">
                    <%=Lang.Get("Assign_Type")%>
                </td>
            </tr>
            <tr style="text-align: left; height: 20px; display:none;">
                <td width="20">
                    <asp:RadioButton ID="RadioButton1" runat="server" GroupName="AssignType" Checked="true" />
                </td>
                <td>
                    <label for="RadioButton1" style="cursor: pointer;">
                        <%=Lang.Get("Assign_SelectTaskAssign")%></label>
                </td>
                <td>
                </td>
            </tr>
            <tr class="TableDataRow" style="text-align: left; height: 20px;">
                <td>
                    <asp:RadioButton ID="RadioButton2" runat="server" GroupName="AssignType" />
                </td>
                <td>
                    <label for="RadioButton2" style="cursor: pointer;">
                        <%=Lang.Get("Assign_AllTaskAssign")%></label>
                </td>
            </tr>
            <tr class="TableDataRow" style="text-align: left; height: 20px;">
                <td>
                    <asp:RadioButton ID="RadioButton3" runat="server" GroupName="AssignType" />
                    <script type="text/javascript" language="javascript">
                        function furClick() {
                            //alert(1);
                            //alert($("#RadioButton3").attr("checked"));
                        }
                    </script>
                </td>
                <td>
                    <label for="RadioButton3" style="cursor: pointer;">
                       <%=Lang.Get("Assign_FutureTaskAssign")%> </label>
                </td>
                <td>
                </td>
            </tr>
            <tr id="trFuture" style="height: 20px; display: none;">
                <td>
                </td>
                <td colspan="2">
                    <div style="border: 1px;">
                        <%=Lang.Get("Assign_FutureTaskAssignDate")%>
                        <span class="red">*</span>
                        <asp:TextBox ID="txtFutureTaskDate" runat="server" Width="150" CssClass="Wdate" Height="20"
                            onclick="WdatePicker()"></asp:TextBox>
                    </div>
                </td>
            </tr>
            <tr class="TableDataRow <%=EnableProcessAssign%>" style="text-align: left; height: 20px; display:block">
                <td>
                    <asp:RadioButton ID="RadioButton4" runat="server" GroupName="AssignType" />
                </td>
                <td>
                    <label for="RadioButton4" style="cursor: pointer;">
                        <%=Lang.Get("Assign_ProcessAssign")%></label>
                </td>
            </tr>
            <tr idx="trprocessname" class="TableDataRow" style="text-align: left; height: 20px;
                display: none;">
                <td>
                </td>
                <td>
                    <%=Lang.Get("frm_Queue_process")%><span class="red">*</span>
                    <asp:DropDownList ID="dropProcessName" runat="server" Width="250">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr idx="trprocessname" class="TableDataRow" style="text-align: left; height: 20px;
                display: none;">
                <td>
                </td>
                <td>
                   <%=Lang.Get("Assign_Date")%> <span class="red">*</span>
                    <asp:TextBox ID="txtBegin" runat="server" CssClass="Wdate" Height="20px" Width="100"
                        onclick="WdatePicker()"></asp:TextBox>
                    &nbsp;&nbsp;-
                    <asp:TextBox ID="txtEnd" runat="server" CssClass="Wdate" Height="20px" Width="100"
                        onclick="WdatePicker({minDate:'#F{$dp.$D(\'txtBegin\')}'})"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td colspan="3" class="banner">
                   <%=Lang.Get("Assign_Touser")%> 
                </td>
            </tr>
            <tr>
                <td>
                </td>
                <td>
                    <%=Lang.Get("Assign_AssignUser")%><span class="red">*</span>&nbsp;&nbsp;&nbsp;&nbsp;
                    <input id="AssignUserName" style="width: 200px;" runat="server" class="TextSearch" />
                    <input type="button" value="..." class="btn Button" onclick="OpenSelectUser()" />
                    <script type="text/javascript" language="javascript">
                        function OpenSelectUser() {
//                            SelectUser({ type: '1', txtName: 'AssignUserName', txtId: 'AssignUserAccount' });
                            //selectUser('','');
                            //selectUser(0, 'AssignUserName', 'AssignUserAccount');

                            var returnJson = window.showModalDialog("../Ultimus.UWF.OrgChart/SelectUser.aspx", "javascript", "dialogHeight=450px;dialogWidth=800px;scroll=no;");
                                                        if (returnJson != null) {
                                                            var json = eval('(' + returnJson + ')');
                                                            $("#AssignUserName").val(json[0].Name);
                                                            $("#AssignUserAccount").val(json[0].ID);
                                                        }
                        }
                    </script>
                    <input id="AssignUserAccount" type="hidden" runat="server" />
                </td>
            </tr>
            <tr>
                <td>
                &nbsp;</td>
            </tr>
            <tr class="TableDataRow" style="text-align: left; height: 20px;">
                <td colspan="2" align="center">
                    <asp:Button ID="Button1" runat="server" CssClass="btn  Button" OnClientClick="return CheckPage()"
                        OnClick="Button1_Click" />
                    <script type="text/javascript" language="javascript">
                        function CheckPage() {
                            var flag = true;
                            if ($("#AssignUserAccount").val() == "") {
                                flag = false;
                                alert('<%=Lang.Get("Assign_SelectUserMsg") %>');
                            } else if ($("#RadioButton1").attr("checked")) {
                                if ($("#TaskIDs").val() == "") {
                                    flag = false;
                                    alert('<%=Lang.Get("Assign_SelectTaskMsg") %>');
                                }
                            } else if ($("#RadioButton4").attr("checked")) {
                                if ($("#dropProcessName option:selected").val() == "") {
                                    flag = false;
                                    alert('<%=Lang.Get("Assign_SelectProcessMsg") %>');
                                } else if ($("#txtBegin").val() == "") {
                                    flag = false;
                                    alert('<%=Lang.Get("Assign_SelectBeginDateMsg") %>');
                                } else if ($("#txtBegin").val() == "") {
                                    flag = false;
                                    alert('<%=Lang.Get("Assign_SelectEndDateMsg") %>');
                                }
                            }
                            return flag;
                        }
                    </script>
                    <input type="button" class="btn Button" value='<%=Lang.Get("Assign_CloseButton") %>'
                        onclick="window.close()" />
                </td>
            </tr>
        </table>
    </div>
    <asp:HiddenField ID="TaskIDs" runat="server" />
    </form>
</body>
</html>
