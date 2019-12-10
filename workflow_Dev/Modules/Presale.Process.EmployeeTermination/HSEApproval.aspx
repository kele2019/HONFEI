<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="HSEApproval.aspx.cs" Inherits="Presale.Process.EmployeeTermination.HSEApproval" %>
<%@ Register Src="/Modules/Ultimus.UWF.Form.ProcessControl/UserInfo.ascx" TagName="UserInfo" TagPrefix="ui" %>
<%@ Register Src="/Modules/Ultimus.UWF.Form.ProcessControl/ApprovalHistory.ascx" TagName="ApprovalHistory" TagPrefix="ah" %>
<%@ Register Src="/Modules/Ultimus.UWF.Form.ProcessControl/Attachments.ascx" TagName="Attachments" TagPrefix="attach" %>
<%@ Register Src="/Modules/Ultimus.UWF.Form.ProcessControl/ButtonList.ascx" TagName="ButtonList" TagPrefix="btn" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
   <meta http-equiv="X-UA-Compatible"  content="IE=EmulateIE8" />
    <title>Employee Termination-Check Out List</title>
    <script type="text/javascript" src="/Assets/js/common.js"></script>
    <script type="text/javascript">

        function getButtonCheck(obj, index) {
            if (index == "1") {
                if ($(obj).attr("checked")) {
                    $("#fld_HSEIHBCC").val("CollectedCancelled");
                    $("#AdminIHBNA").attr("checked", false);
                }
                else {
                    $("#fld_HSEIHBCC").val("");
                }
            }

            if (index == "2") {
                if ($(obj).attr("checked")) {
                    $("#fld_HSEIHBNA").val("NA");
                    $("#AdminIHBCC").attr("checked", false);
                }
                else {
                    $("#fld_HSEIHBNA").val("");
                }
            }


            if (index == "3") {
                if ($(obj).attr("checked")) {
                    $("#fld_HSECKCC").val("CollectedCancelled");
                    $("#AdminCKNA").attr("checked", false);
                }
                else {
                    $("#fld_HSECKCC").val("");
                }
            }

            if (index == "4") {
                if ($(obj).attr("checked")) {
                    $("#fld_HSECKNA").val("NA");
                    $("#AdminCKCC").attr("checked", false);
                }
                else {
                    $("#fld_HSECKNA").val("");
                }
            }


        }

        function beforeSubmit() {
            var msg = "";
            if ($("#fld_HSEIHBCC").val() == "" && $("#fld_HSEIHBNA").val() == "")
                msg = "Pls select PPE Return\n";
            if ($("#fld_HSECKCC").val() == "" && $("#fld_HSECKNA").val() == "")
                msg += "Pls select Other";
            if (msg != "") {
                alert(msg);
                return false;
            }
            else
            return true;
        }

        $(document).ready(function () {
            if ($("#fld_HSEIHBCC").val() == "CollectedCancelled") { $("#AdminIHBCC").attr("checked", true); }
            if ($("#fld_HSEIHBNA").val() == "NA") { $("#AdminIHBNA").attr("checked", true); }
            if ($("#fld_HSECKCC").val() == "CollectedCancelled") { $("#AdminCKNA").attr("checked", true); }
            if ($("#fld_HSECKNA").val() == "NA") { $("#AdminCKCC").attr("checked", true); }

            var TaskType = request('Type');
            if (TaskType != 'mytask') {
                func1();
            }
        });

        function func1() {
            $("#formdiv").each(function (i) {
                $(this).find('input[type="checkbox"]').attr("disabled", "disabled");
                $(this).find('input[type="text"]').attr("disabled", "disabled");
                $(this).find('textarea').attr("disabled", "disabled");
              
            });
        }


    </script>
</head>
<body>
    <form id="form1" runat="server">
     <div id="myDiv" class="container">
            <div class="row">
                <ui:userinfo id="UserInfo1" processtitle="Employee Termination-Check Out List" processprefix="ETCO" tablename="PROC_EmployeeTerminationCheckOut"
                    runat="server"  ></ui:userinfo>
            </div>
           <div   class="row">
                <p style="font-weight:bold;">Termination Employee（"<span style=" background:red;">&nbsp;</span>" must write） </p>
                <table class="table table-condensed table-bordered">
                    <tr>
                        <td class="td-label">
                         <span style=" background:red;  height:30px; float:left;">&nbsp;</span>
                        <p style="text-align:center">Termination Employee</p>
                        </td>
                        <td class="td-content" colspan="7" >
                             
                            <asp:Label runat="server" ID="read_TerminationEmployee" style="display:block;"></asp:Label>
                            <asp:TextBox runat="server" ID="read_TerminationEmployeeValue" style="display:none;"></asp:TextBox>
                        </td>
                    </tr>
                </table>
            </div>

               <div id="formdiv" class="row">
                <p style="font-weight:bold;">HSEF Responsibilities</p>
                <table class="table table-condensed table-bordered">
                       <tr>
                        <th width="30%">
                         <span style="height:30px; float:left;">&nbsp;</span>
                        <p style="text-align:center">item</p>
                        </th>
                        <th width="15%">
                         <span style="height:30px; float:left;">&nbsp;</span>
                        <p style="text-align:center">Collected/Cancelled</p>
                        </th>
                        <th width="15%">
                         <span style="height:30px; float:left;">&nbsp;</span>
                        <p style="text-align:center">N/A</p>
                        </th>
                        <th width="40%">
                         <span style="height:30px; float:left;">&nbsp;</span>
                        <p style="text-align:center">Comments</p>
                        </th>
                    </tr>
                     <tr>
                        <th style="vertical-align:middle">
                         <span style="height:30px; float:left;">&nbsp;</span>
                        <p style="text-align:center">PPE Return</p>
                        </th>
                        <td style="vertical-align:middle;text-align:center">
                            <span style="height:30px; float:left;">&nbsp;</span>
                            <asp:CheckBox runat="server"  ID="AdminIHBCC" onclick="getButtonCheck(this,1)"></asp:CheckBox>
                            <asp:TextBox runat="server" ID="fld_HSEIHBCC" style="display:none;"></asp:TextBox>
                        </td>
                        <td style="vertical-align:middle;text-align:center">
                            <span style="height:30px; float:left;">&nbsp;</span>
                            <asp:CheckBox runat="server"  ID="AdminIHBNA" onclick="getButtonCheck(this,2)"></asp:CheckBox>
                            <asp:TextBox runat="server" ID="fld_HSEIHBNA" style="display:none;"></asp:TextBox>
                        </td>
                        <td style="vertical-align:middle;text-align:center">
                            <span style="height:30px; float:left;">&nbsp;</span>
                            <asp:TextBox runat="server" ID="fld_HSEIHBRPS" Width="94%" MaxLength="200"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <th style="vertical-align:middle">
                         <span style="height:30px; float:left;">&nbsp;</span>
                        <p style="text-align:center">Other</p>
                        </th>
                        <td style="vertical-align:middle;text-align:center">
                            <span style="height:30px; float:left;">&nbsp;</span>
                            <asp:CheckBox runat="server"  ID="AdminCKCC" onclick="getButtonCheck(this,3)"></asp:CheckBox>
                            <asp:TextBox runat="server" ID="fld_HSECKCC" style="display:none;"></asp:TextBox>
                        </td>
                        <td style="vertical-align:middle;text-align:center">
                            <span style="height:30px; float:left;">&nbsp;</span>
                            <asp:CheckBox runat="server"  ID="AdminCKNA" onclick="getButtonCheck(this,4)"></asp:CheckBox>
                            <asp:TextBox runat="server" ID="fld_HSECKNA" style="display:none;"></asp:TextBox>
                        </td>
                        <td class="td-content" style="vertical-align:middle">
                            <span style="height:30px; float:left;">&nbsp;</span>
                            <asp:TextBox runat="server" ID="fld_HSECKRPS" Width="94%" MaxLength="200"></asp:TextBox>
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
    </div>
     <btn:buttonlist id="ButtonList1" runat="server"></btn:buttonlist>
    </form>
</body>
</html>
