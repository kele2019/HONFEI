<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="NewRequest.aspx.cs" Inherits="Presale.Process.BadgeLostPeadjustment.NewRequest" %>

<%@ Register Src="/Modules/Ultimus.UWF.Form.ProcessControl/UserInfo.ascx" TagName="UserInfo" TagPrefix="ui" %>
<%@ Register Src="/Modules/Ultimus.UWF.Form.ProcessControl/ApprovalHistory.ascx" TagName="ApprovalHistory" TagPrefix="ah" %>
<%@ Register Src="/Modules/Ultimus.UWF.Form.ProcessControl/Attachments.ascx" TagName="Attachments" TagPrefix="attach" %>
<%@ Register Src="/Modules/Ultimus.UWF.Form.ProcessControl/ButtonList.ascx" TagName="ButtonList" TagPrefix="btn" %>

<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta http-equiv="X-UA-Compatible"  content="IE=EmulateIE8" />
    <title>Badge Lost/Readjustment Application</title>
    <script type="text/javascript" src="/Assets/js/common.js"></script>  
    <script type="text/javascript">
        $(document).ready(function () {
            var reason = $("#fld_Reason").val();
            if (reason == "Role Change" || reason == "Role Change / New Access") {
                $("#Reason1").attr("checked", true);
            }
            if ($("#hdUrgeTask").val() == "Yes") {
                $("#ReturnBackTask").show();
            }
            if (reason == "New Access/Lost/Damage" || reason == "Lost/Damage") {
                $("#Reason2").attr("checked", true);
                $("#original").hide();
                $("#current").hide();
            }
            $("#dropOriginalRole option:selected").text($("#fld_OriginalRole").val());
            $("#dropCurrentRole option:selected").text($("#fld_CurrentRole").val());
            var value = $("#fld_Access").val();
            var access = value.split(",");
            for (var i = 0; i < access.length; i++) {
                if (access[i] == "main door") {
                    $("#divmain").show();
                    $("#access1").attr("checked", true);
                }
                if (access[i] == "office open area") {
                    $("#access2").attr("checked", true);
                }
                if (access[i] == "IT server room") {
                    $("#access3").attr("checked", true);
                }
                if (access[i] == "Eng.lab") {
                    $("#access4").attr("checked", true);
                }
                if (access[i] == "Production Area") {
                    $("#access5").attr("checked", true);
                }
                if (access[i] == "Warehouse") {
                    $("#access6").attr("checked", true);
                }
                if (access[i] == "Internal Guest Area") {
                    $("#access7").attr("checked", true);
                }
            }

            if ($("#hdIncident").val() != "") {
                $("#ButtonList1_btnSubmit").val("Submit");
                $("#ButtonList1_btnBack").hide();
                $("#ButtonList1_btnReject").show();
            }
        });
        function beforeSubmit() {
            var subttoal = "";
            if ($("#access1").attr("checked")) {
                var itemValue = $("#access1").val();
                subttoal += "," + itemValue;
            }
            if ($("#access2").attr("checked")) {
                var itemValue = $("#access2").val();
                subttoal += "," + itemValue;
            }
            if ($("#access3").attr("checked")) {
                var itemValue = $("#access3").val();
                subttoal += "," + itemValue;
                $("#fld_ITAccess").val("IT server room");
            }
            if ($("#access4").attr("checked")) {
                var itemValue = $("#access4").val();
                subttoal += "," + itemValue;
                $("#fld_EngAccess").val("Eng.lab");
            }
            if ($("#access5").attr("checked")) {
                var itemValue = $("#access5").val();
                subttoal += "," + itemValue;
//                $("#fld_EngAccess").val("Eng.lab");
            }
            if ($("#access6").attr("checked")) {
                var itemValue = $("#access6").val();
                subttoal += "," + itemValue;
            }
            if ($("#access7").attr("checked")) {
                var itemValue = $("#access7").val();
                subttoal += "," + itemValue;
            }
            if (subttoal == "") {
                alert("Please select the access.");
                return false;
            }
            if (subttoal.substr(0, 1) == ',') subttoal = subttoal.substr(1);
            $("#fld_Access").val(subttoal);

            if ($("#Reason1").attr("checked")) {
                $("#Reason2").next().val("Role Change");
            }
            if ($("#Reason2").attr("checked")) {
                $("#Reason2").next().val("New Access/Lost/Damage");
            }
            $("#dropOriginalRole").next().val($("#dropOriginalRole option:selected").text());
            $("#dropCurrentRole").next().val($("#dropCurrentRole option:selected").text());

            var summary = "Badge LostReadjustment Application";
            $("#UserInfo1_fld_PROCESSSUMMARY").val(summary);
            return true;
        }
//        function openparent(obj) {
//            digStr = "dialogHeight:500px;dialogWidth:800px;"
//            var ReturnValue = window.showModalDialog("../Ultimus.UWF.OrgChart/SelectAllUser.aspx", null, digStr);
//            if (ReturnValue != null) {
//                var returnValue = eval("(" + ReturnValue + ")");
//                $(obj).val(returnValue[0].Name);
//                $(obj).next().val((returnValue[0].LoginName).replace('\\', '/')+"|USER");
//            }
//        }
        function getManager(obj, index) {
            var mangaer = "";
            var LoginName = "";
            if ($(obj).find("option:selected").text() == "IT") { manager = $("#fld_ITManager").val(); LoginName = $("#fld_ITManagerLogin").val() }
            if ($(obj).find("option:selected").text() == "HR") { manager = $("#fld_HRManager").val(); LoginName = $("#fld_HRManagerLogin").val(); }
            if ($(obj).find("option:selected").text() == "Engineering") { manager = $("#fld_ENGManager").val(); LoginName = $("#fld_ENGManagerLogin").val(); }
            if ($(obj).find("option:selected").text() == "Finance") { manager = $("#fld_FINManager").val(); LoginName = $("#fld_FINManagerLogin").val(); }
            if ($(obj).find("option:selected").text() == "Quality") { manager = $("#fld_QManager").val(); LoginName = $("#fld_QManagerLogin").val(); }
            if ($(obj).find("option:selected").text() == "Administration") { manager = $("#fld_AdminManager").val(); LoginName = $("#fld_AdminManagerLogin").val(); }
            if ($(obj).find("option:selected").text() == "Purchase") { manager = $("#fld_PURManager").val(); LoginName = $("#fld_PURManagerLogin").val(); }
            if ($(obj).find("option:selected").text() == "Supply Chain") { manager = $("#fld_SCManager").val(); LoginName = $("#fld_SCManagerLogin").val(); }

            var ManagerText = $(obj).val();
            manager = ManagerText.split('|')[0];
            LoginName = ManagerText.split('|')[1] + "|USER";
            if (index == "1") {
//                console.log(manager);
//                console.log($("#UserInfo1_fld_APPLICANT").text());

                if (manager == $("#UserInfo1_fld_APPLICANT").text()) {
                    $("#fld_OriginalManager").val($("#fld_HighManager").val());
                    $("#fld_OriginalManagerLogin").val($("#fld_HighManagerLogin").val());
                }
                else {
                    $("#fld_OriginalManager").val(manager);
                    $("#fld_OriginalManagerLogin").val(LoginName);
                }
            }
            if (index == "2") {
                $("#fld_CurrentManager").val(manager);
                $("#fld_CurrentManagerLogin").val(LoginName);
//                if (manager == $("#UserInfo1_fld_APPLICANT").text()) {
//                    $("#fld_CurrentManager").val($("#fld_HighManager").val());
//                    $("#fld_CurrentManagerLogin").val($("#fld_HighManagerLogin").val());
//                }
//                else {
//                    $("#fld_CurrentManager").val(manager);
//                    $("#fld_CurrentManagerLogin").val(LoginName);
//                }
            }
        }
//        var xmlHttp;
//        function createXMLHttp() {
//            if (window.ActiveXObject) {
//                xmlHttp = new ActiveXObject("Microsoft.XMLHTTP");
//            }
//            else if (window.XMLHttpRequest) {
//                xmlHttp = new XMLHttpRequest();
//            }
//        }
//        function getManager(obj,index) {
//            //            alert("test");
//            var xmlHttp = createXMLHttp();
//            xmlHttp.open("get", "NewRequest.aspx?deparementName=" + $(obj).val(), true);
//            xmlHttp.onreadystatechange = setManager(obj,index);
//            xmlHttp.send(null);
//        }
//        function setManager(obj, index) {
//            if (xmlHttp.readyState == "4") {
//                if (xmlHttp.status == "200") {
//                    if (index == "1") {
//                        $("#fld_OriginalManager").val(xmlHttp.responseText);
//                    }
//                    if (index == "2") {
//                        $("#fld_CurrentManager").val(xmlHttp.responseText);
//                    }
//                }
//            }
//        }
        function roleChange(obj) {
            $(obj).parent().parent().next().show();
            $(obj).parent().parent().next().next().show();  
        }
        function lostDamage(obj) {
            $(obj).parent().parent().next().hide();
            $(obj).parent().parent().next().next().hide();
        }
        function beforeSave() {
            var summary = "Badge LostReadjustment Application";
            $("#UserInfo1_fld_PROCESSSUMMARY").val(summary);
            $("#fld_TRSummary").val(summary);
            return true;
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
     <div id="myDiv" class="container">
            <div class="row">
                <ui:userinfo id="UserInfo1" processtitle="Badge Application  Process" processprefix="BLP" tablename="PROC_BadgeLostPeadjustment"
                    runat="server"  ></ui:userinfo>
                <asp:TextBox runat="server" ID="fld_ITManager" style="display:none;"></asp:TextBox>
                <asp:TextBox runat="server" ID="fld_ITManagerLogin" style="display:none;"></asp:TextBox>
                <asp:TextBox runat="server" ID="fld_HRManager" style="display:none;"></asp:TextBox>
                <asp:TextBox runat="server" ID="fld_HRManagerLogin" style="display:none;"></asp:TextBox>
                <asp:TextBox runat="server" ID="fld_ENGManager" style="display:none;"></asp:TextBox>
                <asp:TextBox runat="server" ID="fld_ENGManagerLogin" style="display:none;"></asp:TextBox>
                <asp:TextBox runat="server" ID="fld_FINManager" style="display:none;"></asp:TextBox>
                <asp:TextBox runat="server" ID="fld_FINManagerLogin" style="display:none;"></asp:TextBox>
                <asp:TextBox runat="server" ID="fld_QManager" style="display:none;"></asp:TextBox>
                <asp:TextBox runat="server" ID="fld_QManagerLogin" style="display:none;"></asp:TextBox>
                <asp:TextBox runat="server" ID="fld_AdminManager" style="display:none;"></asp:TextBox>
                <asp:TextBox runat="server" ID="fld_AdminManagerLogin" style="display:none;"></asp:TextBox>
                <asp:TextBox runat="server" ID="fld_PURManager" style="display:none;"></asp:TextBox>
                <asp:TextBox runat="server" ID="fld_PURManagerLogin" style="display:none;"></asp:TextBox>
                <asp:TextBox runat="server" ID="fld_SCManager" style="display:none;"></asp:TextBox>
                <asp:TextBox runat="server" ID="fld_SCManagerLogin" style="display:none;"></asp:TextBox>
                <asp:TextBox runat="server" ID="fld_HighManager" style="display:none;"></asp:TextBox>
                <asp:TextBox runat="server" ID="fld_HighManagerLogin" style="display:none;"></asp:TextBox>
            </div>
            <div class="row">
                <p style="font-weight:bold;" >Request require（"<span style=" background:red;">&nbsp;</span>" must write） </p>
                <table class="table table-condensed table-bordered">
                    <tr>
                        <td class="td-label">
                         <span style=" background:red;  height:30px; float:left;">&nbsp;</span>
                        <p style="text-align:center">Reason</p>
                        </td>
                        <td class="td-content" colspan="7" >
                         
                           <input type="radio" name="reason"   value="Role change" id="Reason1" onclick="roleChange(this)"/>Role Change&nbsp;&nbsp;
                           <input type="radio" name="reason" value="Lost/Damage" id="Reason2" onclick="lostDamage(this)"/>New Access/Lost/Damage&nbsp;&nbsp;
                           <asp:TextBox runat="server" ID="fld_Reason" style="display:none;"></asp:TextBox>
                           <%--<asp:HiddenField runat="server" ID="fld_Reason" />--%>
                        </td>
                    </tr>
                    <tr id="original">
                        <td class="td-label">
                         <span style=" background:red;  height:30px; float:left;">&nbsp;</span>
                        <p style="text-align:center">Original role</p>
                        </td>
                        <td class="td-content" colspan="3" >
                            <asp:DropDownList ID="dropOriginalRole" runat="server" style="width:98%;" onchange="getManager(this,1)"></asp:DropDownList>
                            <asp:TextBox runat="server" ID="fld_OriginalRole" style="display:none;"></asp:TextBox>
                        </td>
                        <td class="td-label">
                         <span style=" background:red;  height:30px; float:left;">&nbsp;</span>
                         <p style="text-align:center">Original Manager</p>
                        </td>
                        <td class="td-content" colspan="3" >
                            <asp:TextBox runat="server" ID="fld_OriginalManager" style="width:96%;"></asp:TextBox>
                            <asp:TextBox runat="server" ID="fld_OriginalManagerLogin" style="display:none;"></asp:TextBox>
                        </td>
                    </tr>
                    <tr id="current">
                        <td class="td-label" >
                         <span style=" background:red;  height:30px; float:left;">&nbsp;</span>
                        <p style="text-align:center">Current role</p>
                        </td>
                        <td class="td-content" colspan="3" >
                            <asp:DropDownList ID="dropCurrentRole" runat="server" style="width:98%;" onchange="getManager(this,2)"></asp:DropDownList>
                            <asp:TextBox runat="server" ID="fld_CurrentRole" style="display:none;"></asp:TextBox>
                        </td>
                        <td class="td-label">
                         <span style=" background:red;  height:30px; float:left;">&nbsp;</span>
                        <p style="text-align:center">Current Manager</p>
                        </td>
                        <td class="td-content" colspan="3" >
                            <asp:TextBox runat="server" ID="fld_CurrentManager" style="width:96%"></asp:TextBox>
                            <asp:TextBox runat="server" ID="fld_CurrentManagerLogin" style="display:none;"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="td-label"> 
                       <span style=" background:red;  height:30px; float:left;">&nbsp;</span>
                        <p style="text-align:center">Access</p>
                        </td>
                        <td class="td-content"  colspan="7" >
                        <div   id="divmain" style="">
                            <input id="access1" type="checkbox" value="main door" style=" float:left" /> 
                            <span style="float: left;margin-right:10px;" >main door</span>
                            </div>
                            <input id="access2" type="checkbox" value="office open area" />office open area&nbsp;&nbsp;&nbsp;
                            <input id="access3" type="checkbox" value="IT server room" />IT server room&nbsp;&nbsp;&nbsp;
                            <input id="access4" type="checkbox" value="Eng.lab" />Eng.lab&nbsp;&nbsp;&nbsp;
                            <input id="access5" type="checkbox" value="Production Area" />Production Area&nbsp;&nbsp;&nbsp;
                            <input id="access6" type="checkbox" value="Warehouse" />Warehouse&nbsp;&nbsp;&nbsp;
                            <input id="access7" type="checkbox" value="Internal Guest Area" />Internal Guest Area&nbsp;&nbsp;&nbsp;
                            <asp:TextBox runat="server" ID="fld_Access" style="display:none;"/>
                        </td>
                    </tr>
                    <tr>
                    
                     <td class="td-label"> 
                       <span style=" background:red;  height:30px; float:left;">&nbsp;</span>
                        <p style="text-align:center">Comments</p>
                        </td>
                        <td class="td-content"  colspan="7" >
                        <asp:TextBox runat="server" ID="fld_Comments" TextMode="MultiLine"  Rows="5"  Width="90%"></asp:TextBox>
                        </td>


                    </tr>
                 </table>
            </div>
            <div class="row" style="display:none;">
                <attach:attachments id="Attachments1" runat="server"></attach:attachments>
            </div>
            <div class="row">
             <ah:approvalhistory id="ApprovalHistory1" showaction="true" runat="server"></ah:approvalhistory>
            </div>
            <div id="ReturnBackTask" style="display:none; text-align:center">
        <asp:Button runat="server" ID="btnRevoke"  Visible="false" CssClass="btn" Text="Revoke" 
                onclick="btnRevoke_Click" />
        </div>
        </div>
        <btn:buttonlist id="ButtonList1" runat="server"></btn:buttonlist>
        <div style="display:none">
        <asp:TextBox runat="server" ID="fld_TRSummary"></asp:TextBox>
        <asp:HiddenField runat="server" ID="hdIncident" />
        <asp:HiddenField runat="server" ID="hdPrint" />
          <asp:HiddenField runat="server" ID="hdUrgeTask" />
        </div>
    </form>
   
</body>
</html>



