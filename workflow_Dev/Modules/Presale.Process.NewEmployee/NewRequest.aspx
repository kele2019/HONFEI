<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="NewRequest.aspx.cs" Inherits="Presale.Process.NewEmployee.NewRequest" %>

<%@ Register Src="/Modules/Ultimus.UWF.Form.ProcessControl/UserInfo.ascx" TagName="UserInfo" TagPrefix="ui" %>
<%@ Register Src="/Modules/Ultimus.UWF.Form.ProcessControl/ApprovalHistory.ascx" TagName="ApprovalHistory" TagPrefix="ah" %>
<%@ Register Src="/Modules/Ultimus.UWF.Form.ProcessControl/Attachments.ascx" TagName="Attachments" TagPrefix="attach" %>
<%@ Register Src="/Modules/Ultimus.UWF.Form.ProcessControl/ButtonList.ascx" TagName="ButtonList" TagPrefix="btn" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta http-equiv="X-UA-Compatible"  content="IE=EmulateIE8" />
    <title>New Employee On-boardIng</title>
    <script type="text/javascript" src="/Assets/js/common.js"></script>
    <script type="text/javascript">
        function beforeSubmit() {
            $("#fld_OBDepartment").val($("#dropDepartment option:selected").text());
 
            if ($("#Trainer1").attr("checked")) { $("#fld_Trainer1").val($("#fld_CTO").text()); }
            if ($("#Trainer2").attr("checked")) { $("#fld_Trainer2").val($("#fld_FINM").text()); }
            if ($("#Trainer3").attr("checked")) { $("#fld_Trainer3").val($("#fld_HR").text()); }
            if ($("#Trainer4").attr("checked")) { $("#fld_Trainer4").val($("#fld_QM").text()); }
            if ($("#Trainer5").attr("checked")) { $("#fld_Trainer5").val($("#fld_PURM").text()); }
            if ($("#Trainer6").attr("checked")) { $("#fld_Trainer6").val($("#fld_AdminORG").text()); }
            if ($("#Trainer7").attr("checked")) { $("#fld_Trainer7").val($("#fld_PM").text()); }
            if ($("#Trainer8").attr("checked")) { $("#fld_Trainer8").val($("#fld_HSEF").text()); }

//            if ($("#Trainer1").attr("checked")) { $("#fld_Trainer1").val("Jack Shue"); }
//            if ($("#Trainer2").attr("checked")) { $("#fld_Trainer2").val("LI Quan"); }
//            if ($("#Trainer3").attr("checked")) { $("#fld_Trainer3").val("Sharon Zhao"); }
//            if ($("#Trainer4").attr("checked")) { $("#fld_Trainer4").val("Wayne Zhang"); }
//            if ($("#Trainer5").attr("checked")) { $("#fld_Trainer5").val("Eason Sun"); }
//            if ($("#Trainer6").attr("checked")) { $("#fld_Trainer6").val("Nico Zhang"); }
//            if ($("#Trainer7").attr("checked")) { $("#fld_Trainer7").val("Sophia Nie"); }
 
//            if ($("#fld_Admin").val() == "") {
//                alert("please select admin");
//                return false;
//            }
//            if ($("#fld_ITAdmin").val() == "") {
//                alert("please select IT");
//                return false;
//            }
//            if ($("#fld_FinanceAdmin").val() == "") {
//                alert("please select Finance");
//                return false;
//            }
//            if ($("#fld_HRGAdmin").val() == "") {
//                alert("please select HRG");
//                return false;
//            }
//            if ($("#fld_HRAdmin").val() == "") {
//                alert("please select HR");
//                return false;
//            }
//            if ($("#fld_QualityAdmin").val() == "") {
//                alert("please select Quality");
//                return false;
//            }

            var summary = "New Employee On-boardIng";
            $("#UserInfo1_fld_PROCESSSUMMARY").val(summary);
            return true;
        }
        function beforeSave() {
            var summary = "New Employee On-boardIng";
            $("#UserInfo1_fld_PROCESSSUMMARY").val(summary);
            $("#fld_TRSummary").val(summary);
            return true;
        }
        function showTime(obj) {
            var time = new Date(obj.replace(/-/g,"/"));
            var year = time.getFullYear();
            var month = time.getMonth() + 1;
            var date = time.getDate();
            return (year + "-" + month + "-" + date) == "NaN-NaN-NaN" ? "" : (year + "-" + month + "-" + date);
        }
        $(document).ready(function () {

            if ($("#fld_Trainer1").val() == $("#fld_CTO").text()) { $("#Trainer1").attr("checked", true); }
            if ($("#fld_Trainer2").val() == $("#fld_FINM").text()) { $("#Trainer2").attr("checked", true); }
            if ($("#fld_Trainer3").val() == $("#fld_HR").text()) { $("#Trainer3").attr("checked", true); }
            if ($("#fld_Trainer4").val() == $("#fld_QM").text()) { $("#Trainer4").attr("checked", true); }
            if ($("#fld_Trainer5").val() == $("#fld_PURM").text()) { $("#Trainer5").attr("checked", true); }
            if ($("#fld_Trainer6").val() == $("#fld_AdminORG").text()) { $("#Trainer6").attr("checked", true); }
            if ($("#fld_Trainer7").val() == $("#fld_PM").text()) { $("#Trainer7").attr("checked", true); }
            if ($("#fld_Trainer8").val() == $("#fld_HSEF").text()) { $("#Trainer8").attr("checked", true); }

             $("#dropEmployeeLoginName").val($("#fld_EmployeeLoginName").val());

            //if ($("#fld_Trainer1").val() == "Jack Shue") { $("#Trainer1").attr("checked", true); }
            //if ($("#fld_Trainer2").val() == "LI Quan") { $("#Trainer2").attr("checked", true); }
            //if ($("#fld_Trainer3").val() == "Sharon Zhao") { $("#Trainer3").attr("checked", true); }
            //if ($("#fld_Trainer4").val() == "Wayne Zhang") { $("#Trainer4").attr("checked", true); }
            //if ($("#fld_Trainer5").val() == "Eason Sun") { $("#Trainer5").attr("checked", true); }
            //if ($("#fld_Trainer6").val() == "Nico Zhang") { $("#Trainer6").attr("checked", true); }
            //if ($("#fld_Trainer7").val() == "Sophia Nie") { $("#Trainer7").attr("checked", true); }
            //
            //$("#dropDepartment").val($("#fld_OBDepartment").val());
            //$("#dropDepartment")($("#fld_OBDepartment").val());
            var OBDepartment = $("#fld_OBDepartment").val();
            $("#dropDepartment").find("option[text='" + OBDepartment + "']").attr("selected", true);

            $("#fld_OBDate").val(showTime($("#fld_OBDate").val()));
            if ($("#hdIncident").val() != "") {
                $("#ButtonList1_btnSubmit").val("Submit");
                $("#ButtonList1_btnBack").hide();
                $("#ButtonList1_btnReject").show();
            }
            if ($("#hdUrgeTask").val() == "Yes") {
                $("#ReturnBackTask").show();
            }
        });
        function getButtonCheck(obj, index) {
            var domin = $("#domin").val();
            if (index == "1") {
                if ($(obj).attr("checked")) {
                    $("#fld_Admin").val($("#fld_AdminORG").text());
                }
                else {
                    $("#fld_Admin").val("");
                }
            }
            if (index == "2") {
                if ($(obj).attr("checked")) {
                    $("#fld_ITAdmin").val($("#fld_ITM").text());
                }
                else {
                    $("#fld_ITAdmin").val("");
                }
            }
//            if (index == "3") {
//                if ($(obj).attr("checked")) {
//                    $("#fld_ITAdmin").avl(domin + "Andy.Chu".toLowerCase() + "|USER");
//                }
//            }
            if (index == "4") {
                if ($(obj).attr("checked")) {
                    $("#fld_EngineerAdmin").val($("#fld_CTO").text());
                }
                else {
                    $("#fld_EngineerAdmin").val("");
                }
            }
//            if (index == "5") {
//                if ($(obj).attr("checked")) {
//                    $("#fld_HRGAdmin").val(domin + "Jason.Hu".toLowerCase() + "|USER");
//                }
//            }
            if (index == "6") {
                if ($(obj).attr("checked")) {
                    $("#fld_FinanceAdmin").val($("#fld_FINM").text());
                }
                else {
                    $("#fld_FinanceAdmin").val("");
                }
            }
//            if (index == "7") {
//                if ($(obj).attr("checked")) {
//                    $("#fld_FinanceAdmin").val(domin + "Haobin.Duan".toLowerCase() + "|USER");
//                }
//            }
            if (index == "8") {
                if ($(obj).attr("checked")) {
                    $("#fld_HRAdmin").val($("#fld_HR").text());
                }
                else {
                    $("#fld_HRAdmin").val("");
                }
            }
            if (index == "9") {
                if ($(obj).attr("checked")) {
                    $("#fld_QualityAdmin").val($("#fld_QM").text());
                }
                else {
                    $("#fld_QualityAdmin").val("");
                }
            }
//            if(index == "10") {
//                if ($(obj).attr("checked")) {
//                    $("#fld_QualityAdmin").val(domin + "Angel Sheng".toLowerCase() + "|USER");
//                }
            //            }

        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
     <div id="myDiv" class="container">
            <div class="row">
                <ui:userinfo id="UserInfo1" processtitle="New Employee On-boardIng" processprefix="HREO" tablename="PROC_NewEmployee" runat="server"  ></ui:userinfo>
            <asp:TextBox runat="server" ID="fld_ITMLogin" style="display:none;"></asp:TextBox>
            <asp:TextBox runat="server" ID="fld_AdminLogin" style="display:none;"></asp:TextBox>
            <asp:TextBox runat="server" ID="fld_CTOLogin" style="display:none;"></asp:TextBox>
            <asp:TextBox runat="server" ID="fld_FINMLogin" style="display:none;"></asp:TextBox>
            <asp:TextBox runat="server" ID="fld_HRLogin" style="display:none;"></asp:TextBox>
            <asp:TextBox runat="server" ID="fld_PMLogin" style="display:none;"></asp:TextBox>
            <asp:TextBox runat="server" ID="fld_QMLogin" style="display:none;"></asp:TextBox>
            <asp:TextBox runat="server" ID="fld_PURMLogin" style="display:none;"></asp:TextBox>
            <asp:TextBox runat="server" ID="fld_ITM" style="display:none;"></asp:TextBox>
              <asp:TextBox runat="server" ID="fld_HSEFLogin" style="display:none;"></asp:TextBox>
            </div>
            <asp:TextBox runat="server" ID="domin" style="display:none;"></asp:TextBox>
            <div class="row">
                <p style="font-weight:bold;">Employee Information（"<span style=" background:red;">&nbsp;</span>" must write） </p>
                <table class="table table-condensed table-bordered">
                    <tr>
                        <td class="td-label">
                         <span style=" background:red;  height:30px; float:left;">&nbsp;</span>
                        <p style="text-align:center">Employee Name</p>
                        </td>
                        <td class="td-content" colspan="3" >
                        <asp:DropDownList runat="server" ID="dropEmployeeLoginName" CssClass="validate[required]" Width="98%">
                        </asp:DropDownList>
                        <asp:TextBox runat="server" ID="fld_EmployeeName" Width="96%" style="display:none;"  ></asp:TextBox>
                        <asp:TextBox runat="server" ID="fld_EmployeeLoginName" Width="96%" style="display:none;"  ></asp:TextBox>

                        </td>
                        <td class="td-label">
                         <span style=" background:red; height:30px; float:left;">&nbsp;</span>
                        <p style="text-align:center">On-boarding date</p>
                        </td>
                        <td class="td-content" colspan="3" >
                        <asp:TextBox runat="server" ID="fld_OBDate" Width="96%" onclick="WdatePicker({readOnly:false,dateFmt: 'yyyy-MM-dd'})"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="td-label"> 
                       <span style=" background:red; margin-top:5px;  height:30px; float:left;">&nbsp;</span>
                        <p style="text-align:center;">On-boarding department</p>
                        </td>
                        <td class="td-content"  colspan="3" >
                             <asp:DropDownList ID="dropDepartment" runat="server" style="width:100%;"></asp:DropDownList>
                            <asp:TextBox runat="server" ID="fld_OBDepartment" style="display:none;"></asp:TextBox>
                        </td>
                        <td class="td-label"> 
                       <span style=" background:red; margin-top:5px;  height:30px; float:left;">&nbsp;</span>
                        <p style="text-align:center;">Training Place</p>
                        </td>
                        <td class="td-content"  colspan="3" >
                            <asp:TextBox runat="server" ID="fld_TrainPlace" Width="96%" ></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="td-label" style="vertical-align:middle">
                         <span style=" background:red;  height:30px; float:left;">&nbsp;</span>
                        <p style="text-align:center">Training Content</p>
                        </td>
                        <td class="td-content" colspan="7" >
                        <asp:TextBox runat="server" ID="fld_TrainContent" Rows="3" TextMode="MultiLine" Width="98%"  CssClass="validate[required]"></asp:TextBox>
                        </td>
                    </tr>
 
                </table>
                <%--<table class="table table-condensed table-bordered">
                    <tr>
                        <td rowspan="3" class="td-label" style="vertical-align:middle">
                            <span style=" background:red;  height:30px; float:left;">&nbsp;</span>
                            <p style="text-align:center">Trainer</p>
                        </td>
                        <td style="border-bottom:none;">
                            <asp:CheckBox ID="Trainer1" runat="server" />Jack Shue
                            <asp:TextBox runat="server" ID="fld_Trainer1" style="display:none;"></asp:TextBox>
                        </td>
                        <td style="border:none;">
                            <asp:CheckBox ID="Trainer2" runat="server" />LI Quan
                            <asp:TextBox runat="server" ID="fld_Trainer2" style="display:none;"></asp:TextBox>
                        </td>
                        <td style="border:none;">
                            <asp:CheckBox ID="Trainer3" runat="server" />Sharon Zhao
                            <asp:TextBox runat="server" ID="fld_Trainer3" style="display:none;"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td style="border-top:none;">
                            <asp:CheckBox ID="Trainer4" runat="server" />Wayne Zhang
                            <asp:TextBox runat="server" ID="fld_Trainer4" style="display:none;"></asp:TextBox>
                        </td>
                        <td style="border:none;">
                            <asp:CheckBox ID="Trainer5" runat="server" />Eason Sun
                            <asp:TextBox runat="server" ID="fld_Trainer5" style="display:none;"></asp:TextBox>
                        </td>
                        <td style="border:none;">
                            <asp:CheckBox ID="Trainer6" runat="server" />Nico Zhang
                            <asp:TextBox runat="server" ID="fld_Trainer6" style="display:none;"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td style="border-top:none;">
                            <asp:CheckBox ID="Trainer7" runat="server" />Sophia Nie
                            <asp:TextBox runat="server" ID="fld_Trainer7" style="display:none;"></asp:TextBox>
                        </td>
                    </tr>
                </table>--%>
                        <%--<td class="td-label">
                         <span style="  height:30px; float:left;">&nbsp;</span>
                        </td>
                        <td class="td-content" colspan="3" >
                        </td>--%>
                        <%--<td class="td-label" >
                         <span style="height:30px; float:left;">&nbsp;</span>
                        <p style="text-align:center">Contact</p>
                        </td>
                        <td class="td-content" colspan="3" >
                        <asp:TextBox runat="server" ID="fld_Contact" Width="96%"></asp:TextBox>
                        </td>--%>
                    
 
                <table class="table table-condensed table-bordered">
                    <tr>
                        <td rowspan="3" class="td-label" style="vertical-align:middle">
                            <span style=" background:red;  height:30px; float:left;">&nbsp;</span>
                            <p style="text-align:center">Trainer</p>
                        </td>
                        <td style="border-bottom:none;">
                            <asp:CheckBox ID="Trainer1" runat="server"/><asp:Label runat="server" ID="fld_CTO" ></asp:Label>
                            <asp:TextBox runat="server" ID="fld_Trainer1" style="display:none;"></asp:TextBox>
                        </td>
                        <td style="border:none;">
                            <asp:CheckBox ID="Trainer2" runat="server" /><asp:Label runat="server" ID="fld_FINM" ></asp:Label>
                            <asp:TextBox runat="server" ID="fld_Trainer2" style="display:none;"></asp:TextBox>
                        </td>
                        <td style="border:none;">
                            <asp:CheckBox ID="Trainer3" runat="server" /><asp:Label runat="server" ID="fld_HR" ></asp:Label>
                            <asp:TextBox runat="server" ID="fld_Trainer3" style="display:none;"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td style="border-top:none;">
                            <asp:CheckBox ID="Trainer4" runat="server" /><asp:Label runat="server" ID="fld_QM" ></asp:Label>
                            <asp:TextBox runat="server" ID="fld_Trainer4" style="display:none;"></asp:TextBox>
                        </td>
                        <td style="border:none;">
                            <asp:CheckBox ID="Trainer5" runat="server" /><asp:Label runat="server" ID="fld_PURM" ></asp:Label>
                            <asp:TextBox runat="server" ID="fld_Trainer5" style="display:none;"></asp:TextBox>
                        </td>
                        <td style="border:none;">
                            <asp:CheckBox ID="Trainer6" runat="server" /><asp:Label runat="server" ID="fld_AdminORG" ></asp:Label>
                            <asp:TextBox runat="server" ID="fld_Trainer6" style="display:none;"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td style="border-top:none;">
                            <asp:CheckBox ID="Trainer7" runat="server" /><asp:Label runat="server" ID="fld_PM" ></asp:Label>
                            <asp:TextBox runat="server" ID="fld_Trainer7" style="display:none;"></asp:TextBox>
                        </td>
                         <td style="border-top:none;">
                            <asp:CheckBox ID="Trainer8" runat="server" /><asp:Label runat="server" ID="fld_HSEF" ></asp:Label>
                            <asp:TextBox runat="server" ID="fld_Trainer8" style="display:none;"></asp:TextBox>
                        </td>
                    </tr>
                </table>
                        
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


