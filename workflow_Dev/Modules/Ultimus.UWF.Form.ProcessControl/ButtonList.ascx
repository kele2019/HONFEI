<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ButtonList.ascx.cs"
    Inherits="Ultimus.UWF.Form.ProcessControl.ButtonList" %>
<script type="text/javascript" language="javascript">
    var submitTimes = 0;
    function submitForm(obj) {
        if (obj == "1") {
            $("#ApprovalHistory1_rbReturn").attr("checked", true);
        }
        if (obj == "0") {
            $("#ApprovalHistory1_rbApprove").attr("checked", true);
        }
        if (obj == "2") {
            $("#ApprovalHistory1_rbAbort").attr("checked", true);
        }
        //判断审批意见
        if (typeof (validateIdear) == "function") {
            if (!validateIdear()) {
                return false;
            }
        }
        //加个客户端方法beforeSubmit
        if (typeof (beforeSubmit) == "function") {
            var flag = beforeSubmit();
            if (!flag) {
                submitTimes = 0;
                return false;
            }
        }

        //判断明细行
        var count = 0

        if (!$(".tablerequired").is(":hidden")) {
            $(".tablerequired").each(function (index, ele) {

                if ($(ele).find("tr").length <= 1) {
                    count++;
                    alert('<%=Ultimus.UWF.Form.ProcessControl.Resources.lang.MustInputDetail %>');
                    return false;
                }
            });
        }
        if (count > 0) {
            return false;
        }
        //alert();
        if ($("#Attachments1_txtReadonly").val() != "1" && $("#Attachments1_FilePath").val() != "") {
            //if (!confirm('<%=Ultimus.UWF.Form.ProcessControl.Resources.lang.HaveFile %>')) {
                //return false;
            //}
        }
        //判断是否是必须上传附件
        if ($("#Attachments1_txtMust").val() == "1") {
            if ($("#fileinfo tr").size() <= 0) {
                alert('<%=Ultimus.UWF.Form.ProcessControl.Resources.lang.AttachmentRequire %>');
                return false;
            }
        };


        if (count == 0) {
            var confirmmsg = "";
            if ($("#ButtonList1_btnSubmit").val() == "Submit") {
                confirmmsg = "Confirm Submit？";
            }
            if ($("#ButtonList1_btnSubmit").val() == "Approve") {
                confirmmsg = "Confirm Approve？";
            }
            if (obj == "1") {
                confirmmsg = "Confirm Reject？";
            }
            if (obj == "2") {
                confirmmsg = "Confirm Cancel？";
            }
            if (confirmmsg == "") {
                confirmmsg = $("#ButtonList1_btnSubmit").val();
            }
            if (confirmmsg != "") {
                if (!confirm(confirmmsg)) {
                    return false;
                }
            }
            var AskFor = $("#ButtonList1_var_AskFor").val();
            if ($("#hdAskFlag").val() == "1") {
                if (AskFor != "") {
                    $("#ApprovalHistory1_txtComments").val($("#txtReason").val());
                    $("#ApprovalHistory1_txtSpecialAction").val("Inquire");
                }
            }
            else {
                $("#divAskFor").hide();
                $("#ButtonList1_var_AskFor").val("");
                $("#hdAskFlag").val("");
                $("#txtReason").val("");
            }
            //showDiv();
            if (submitTimes > 0) {
                alert("<%=Ultimus.UWF.Form.ProcessControl.Resources.lang.Submiting %>");
                return false;
            }
            submitTimes++;
            return true;
        }
        else {
            return false;
        }
    }

    function submitSuccess() {
        if (window.opener != null) {
            try {
                window.open('', '_self');
                // window.opener.location.href = "../Ultimus.UWF.Home2/ToDoList.aspx?Type=mytask";
                try {
                    var Flag = window.opener.parent.document.getElementById("hdFlag").value;
                    window.opener.location.href = "../Ultimus.UWF.Home2/ToDoList.aspx?Type=mytask";
                }
                catch (e) {
                    window.opener.location.href = "../Ultimus.UWF.Home2/Index.aspx#DBRW";
                }
                //window.opener.parent.location.href = "/Modules/Ultimus.UWF.Home2/Index.aspx#DBRW";
                alert('<%=Ultimus.UWF.Form.ProcessControl.Resources.lang.SubmitSuccess %>');
                closeDiv();
            }
            catch (e) {
            }
        }
        window.close();
    }

    function saveSuccess() {
        alert('<%=Ultimus.UWF.Form.ProcessControl.Resources.lang.SaveSuccess %>');
        if (window.opener != null) {
            window.opener.location.href = window.opener.location.href;
        }
    }
    function RevokSuccess() {
        alert('撤回成功！\nRevoke Success!');
        window.opener.location.href = "../Ultimus.UWF.Home2/List.aspx?Type=myrequest";
        window.close();
    }
    function SessionExpire() {
        alert('Session has expired,Please Login Again')
    }

    function closeWin() {
        window.close();
        return false;
    }


    function showAsst(processName) {

        var val;
        val = window.showModalDialog(path + '/Modules/Ultimus.UWF.Workflow/AsstTask.aspx?taskId=<%=Request["TaskID"] %>&ProcessName=' + escape(processName), null, "dialogWidth=600px;dialogHeight=300px");
    }

    function showGoto(processName) {

        var val;
        val = window.showModalDialog(path + '/Modules/Presale.Process.Exp/GotoStep.aspx?taskId=<%=Request["TaskID"] %>&Incident=<%=Request["Incident"] %>&ProcessName=<%=Server.UrlEncode(Convert.ToString( Request["ProcessName"])) %>', null, "dialogWidth=600px;dialogHeight=300px");
        window.close();
        return false;
    }
    function SaveDraft() {
        if (typeof (beforeSave) == "function") {
            var flag = beforeSave();
            if (!flag) {
                submitTimes = 0;
                return false;
            }
        }
        showDiv();
    }
    function CanceldivAskFor(obj) {
        closeDiv();
        $("#divAskFor").hide();
        $("#ButtonList1_btnSubmit").val("Approve");
        $("#ButtonList1_var_AskFor").val("");
        $("#hdAskFlag").val("");
        if (obj != undefined) {
            $("#ButtonList1_var_AskForAccount").val("");
        }
        $("#txtReason").val("");
    }
    function SubmitAskFor() {
        var AskFor = $("#var_AskFor").val();
        if (AskFor == "") {
            alert("Please Select Inquire");
            return false;
        }
        $("#hdAskFlag").val("1");
        $("#ButtonList1_btnSubmit").val("Submit");
        $('#ButtonList1_btnSubmit').click();
    }
    function ShowAskdiv () {
        $('#divAskFor').toggle();
        showDiv();
        var RequestAccount = $("#UserInfo1_fld_APPLICANTACCOUNT").val() + "|USER";
        var RequestName = $("#UserInfo1_fld_APPLICANT").text();
        $("#ButtonList1_var_AskForAccount").val(RequestAccount);
        $("#ButtonList1_var_AskFor").val(RequestName);
    }
    
</script>
<!--弹出层时背景层DIV-->
<div id="fade" class="black_overlay">
</div>
<div id="loadingdiv" class="white_content">
    <center>
        <img src="/Modules/Ultimus.UWF.Form.ProcessControl/img/loading.gif" /></center>
</div>
<table style="width: 100%;">
    <tr  width="500">
        <td align="center"  >
            <table>
                <tr>
                <td>
                <input type="button" value="Inquire" visible="false"  style="display:none" id="btnAsk" runat="server" onclick="ShowAskdiv()"  class="btn" />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                <input  type="hidden" id="hdAskFlag" />
                </td>
                    <td><asp:Button ID="btnSubmit" runat="server" CssClass="btn" Text="Submit" OnClientClick="return submitForm('0');"
                OnClick="btnSubmit_Click" />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                <asp:Button ID="btnBack" runat="server" CssClass="btn" Text="Reject"  Visible="false" OnClientClick="return submitForm('1');"
                OnClick="btnSubmit_Click" />
                 <asp:Button ID="btnReject" runat="server" CssClass="btn" Text="Cancel"   style="display:none" OnClientClick="return submitForm('2');"
                OnClick="btnSubmit_Click" />
                    </td>
                    <td>
                     <div id="divGoto" runat="server" visible="false">
                 <input type="button" id="btnGoto" runat="server" class="btn btn-primary" value="跳转" onclick="showGoto('');return false;" style=" "
                 /></div>
                    </td>
                    <td>
                    <asp:Button ID="btnSaveDraft" runat="server" CssClass="btn" Text="Save" OnClientClick="return SaveDraft();"
                OnClick="btnSaveDraft_Click" />
                    </td>
                    <td>
                    <input type="button" id="btnXieban" runat="server" visible="false" class="btn btn-info" value="协办" onclick="showAsst('协办流程');return false;" style=" "
                 />
                    </td>
                    <td>
                    <input type="button" id="btnCopy" runat="server" visible="false" class="btn btn-info" value="抄送" onclick="showAsst('抄送流程');return false;" style=" "
                 />
                    </td>
                    <td>
                     <asp:Button ID="btnClose" runat="server"   Visible="false"  Text="关闭" CssClass="btn " OnClientClick="return closeWin();" />
                    </td>
                </tr>
            </table>
        </td>
    </tr>
</table>

<div id="divAskFor" class="row" style="position:fixed; z-index:300001; padding-top:0px; top:40%; left:30%;display:none;  width:600px;background-color:White;">
<table  class="table table-condensed table-bordered">
<tr><td class="banner" style="text-align:center" colspan="2">
询问信息 Inquire Info
</td>
</tr>
   <tr  >
    <td class="td-label">
          咨询人 Inquire：
        </td>
        <td class="td-content" >
        <asp:TextBox runat="server" ID="var_AskFor" onfocus="this.blur()"></asp:TextBox>
        <asp:TextBox runat="server" ID="var_AskForAccount" style="display:none" ></asp:TextBox>
        <input type="button" value="..." class="btn" onclick="selectUser('1','ButtonList1_var_AskFor','ButtonList1_var_AskForAccount')" />
        </td>
    </tr>
    <tr>
    <td class="td-label">意见 Comments：</td>
    <td class="td-content">
    <textarea rows="5" id="txtReason" cols="40" style="width:90%"></textarea>
    </td>
    </tr>
    <tr>
    <td    colspan="2">
    <div style="width:80%; text-align:right;">
   <input type="button" class="btn" value="OK" onclick="return SubmitAskFor()" />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
   &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
   <input type="button"  class="btn" value="Cancel"  onclick="CanceldivAskFor('1')" />
   </div>
    </td>
    </tr>
</table>
</div>