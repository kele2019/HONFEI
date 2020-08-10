<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ApprovalHistory.ascx.cs"
    Inherits="Ultimus.UWF.Form.ProcessControl.ApprovalHistory" %>

    <%--<tr>
        <td class="banner" colspan="4">
            <%=Ultimus.UWF.Form.ProcessControl.Resources.lang.ApprovalHistory%>
        </td>
    </tr>--%>
            <p style="font-weight:bold;">ApprovalHistory</p>
            <table class="table table-condensed table-bordered">
                <tr>
                    <th width="20">
                        No.
                    </th>
                    <%-- <th>职级</th>--%>
                    <th width="60">
                        <%--<%=Ultimus.UWF.Form.ProcessControl.Resources.lang.Approver%>--%>
                        ApproveName
                    </th>
                    <th width="80">
                        <%--<%=Ultimus.UWF.Form.ProcessControl.Resources.lang.StepName%>--%>
                        StepName
                    </th>
                    <th width="300">
                         <%--<%=Ultimus.UWF.Form.ProcessControl.Resources.lang.Comments%>--%>
                         Comments
                    </th>
                    <th width="80">
                        <%--<%=Ultimus.UWF.Form.ProcessControl.Resources.lang.ApproveResult%>--%>
                        ApproveResult
                    </th>
                    <th width="80">
                        <%--<%=Ultimus.UWF.Form.ProcessControl.Resources.lang.ApproveDate%>--%>
                        ApproveDate
                    </th>
                </tr>
                <asp:Repeater ID="ApprovalHistoryList" runat="server">
                    <ItemTemplate>
                        <tr>
                            <td align="center" style="text-align: center">
                                <%# Container.ItemIndex+1 %>
                            </td>
                            <%-- <td><%# Eval("Level") %></td>--%>
                            <td align="center" style="text-align: center">
                               <%--<%# Eval("AgentName")%><%# Eval("UserName")%>--%>
                                <%--<img   style="width:80px;height:50px;" alt='<%# Eval("UserName")%>' src='../../Modules/Ultimus.UWF.Form.ProcessControl/img/<%# Eval("UserName").ToString() %>.png' />--%>
                                <img style="width:80%" alt='<%# Eval("UserName")%>' src='../../Modules/Ultimus.UWF.Form.ProcessControl/img/<%# Eval("UserName").ToString() %>.png' />
                            </td>
                            <td align="center" style="text-align: center" class="stepname">
                                <%# Eval("StepName")%>
                            </td>
                            <td align="center" style="text-align: center">
                                <%# Eval("Comments")%>
                            </td>
                            <td align="center" style="text-align: center">
                                <%# Eval("Action")%>
                            </td>
                            <td style="text-align: center">
                                <%# Eval("CreateDate") %>
                            </td>
                        </tr>
                    </ItemTemplate>
                </asp:Repeater>
            </table>
    <table class="table table-condensed table-bordered">
    <tr id="trAction" style="display:none" runat="server">
        <td class="td-label" style="width:80px">
            <%=Ultimus.UWF.Form.ProcessControl.Resources.lang.ApproveAction%>：
        </td>
        <td class="td-content" colspan="3">
            <asp:RadioButton ID="rbApprove" runat="server" Text="Approve" GroupName="action" CssClass="pr50" />
            <asp:RadioButton  ID="rbReturn" runat="server" Text="Reject" GroupName="action" />
            <asp:RadioButton  ID="rbAbort" runat="server" Text="Abort" GroupName="action" />
        </td>
    </tr>
    <tr id="trIdear" runat="server">
        <td class="td-label" style="vertical-align:middle;">
<%--            <%=Ultimus.UWF.Form.ProcessControl.Resources.lang.Comments%>：--%>
            Approval Comments：
        </td>
        <td class="td-content" colspan="3">
            <span>( <%=Ultimus.UWF.Form.ProcessControl.Resources.lang.MaxLength%>：<asp:Label Text="0" runat="server"
                ID="reachChar" Font-Underline="true" ForeColor="Blue"> </asp:Label> <%=Ultimus.UWF.Form.ProcessControl.Resources.lang.Char%>：)<br />
            </span>
            <asp:TextBox ID="txtComments" runat="server" Width="98%" Height="100" TextMode="MultiLine"
                MaxLength="1000"></asp:TextBox>
        </td>
    </tr>
 
</table>
<div class="hidden">
    <asp:TextBox ID="txtShowAction" runat="server"></asp:TextBox>
    <asp:TextBox ID="txtSpecialAction" runat="server" Text="Approve" ></asp:TextBox>
</div>
<script type="text/javascript">
    String.prototype.trim = function () {
        return this.replace(/(^\s*)|(\s*$)/g, "");
    }
    function validateIdear() {

        var vatShowAction = $("#ApprovalHistory1_txtShowAction").val();
        //alert(vatShowAction);
        if (vatShowAction == "1" || vatShowAction == "") {
            if ($("#ApprovalHistory1_rbReturn").attr("checked") != "checked"
                && $("#ApprovalHistory1_rbApprove").attr("checked") != "checked" && $("#ApprovalHistory1_rbAbort").attr("checked") != "checked") {
                alert("<%=Ultimus.UWF.Form.ProcessControl.Resources.lang.ApproveActionRequire%>");
                return false;
            }
        }
        var f = $("#ApprovalHistory1_rbReturn").attr("checked");
        //alert(f);
        //alert($("#ApprovalHistory1_txtIdear").val());
        if (f == "checked") {
            if ($("#ApprovalHistory1_txtComments").val() == undefined || $("#ApprovalHistory1_txtComments").val().trim() == "") {
                alert("<%=Ultimus.UWF.Form.ProcessControl.Resources.lang.RequireComments%>");
                $("#ApprovalHistory1_txtComments").focus();
                return false;
            }
        }
        if ($("#ApprovalHistory1_txtComments").val() != undefined && getLength($("#ApprovalHistory1_txtComments").val()) > 4000) {
            alert("<%=Ultimus.UWF.Form.ProcessControl.Resources.lang.CommentsOverLength%>");
            return false;
        }
        return true;
    }

    $().ready(function () {
        $("#ApprovalHistory1_txtComments").keyup(function () {
            //alert(data.length);
            //alert($("#ApprovalHistory1_txtComments").val().length);
            // alert(getByteLen(getByteLen($("#ApprovalHistory1_txtComments").val())));
            $("#ApprovalHistory1_reachChar").text(getLength($("#ApprovalHistory1_txtComments").val()));
            if (getLength($("#ApprovalHistory1_txtComments").val()) > 4000) {
                $("#ApprovalHistory1_reachChar").css("color", "red");
            }
            else {
                $("#ApprovalHistory1_reachChar").css("color", "blue");
            }
        });
        $(".stepname").each(function(i, item) {
          //  console.log("|"+$(item).html()+"|");
            if ($(item).html() == "CTO  Reivew") {
                $(item).html("CTO Review");
            }
             if ($(item).html() == "Trainning") {
                 $(item).html("Training");
            }
        });
    });
    //function getByteLen(str) {
    function getLength(val) {
        var cArr = val.match(/[^\x00-\xff]/ig);
        var num = val.length + (cArr == null ? 0 : cArr.length);
        return num;
    } //为UTF-8时，非ASCII字符占用三个字节宽

    //}

</script>
