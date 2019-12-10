﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="HSEFApprove.aspx.cs" Inherits="Presale.Process.NewEmployee.HSEFApprove" %>

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
//            if ($("#RadioButton1").attr("checked")) {
//                $("#fld_FinanceAdmin").val($("#read_FINM").text());
//            }
            if (!$("#RadioButton1").attr("checked")) {
                alert("please select HSEF");
                return false;
            }
            return true;
        }
        function showTime(obj) {
            var time = new Date(obj.replace(/-/g, "/"));
            var year = time.getFullYear();
            var month = time.getMonth() + 1;
            var date = time.getDate();
            return (year + "-" + month + "-" + date) == "NaN-NaN-NaN" ? "" : (year + "-" + month + "-" + date);
        }
        $(document).ready(function () {
            $("#read_OBDate").text(showTime($("#read_OBDate").text()));
            $("#fld_HSEFTrainDate").val(showTime($("#fld_HSEFTrainDate").val()));
                        if (request('myapproval')=="myapproval") {
                $("#RadioButton1").attr("checked", true);
            }
        });
    </script>
</head>
<body>
    <form id="form1" runat="server">
     <div id="myDiv" class="container">
            <div class="row">
                <ui:userinfo id="UserInfo1" processtitle="New Employee On-boardIng" processprefix="HREO" tablename="PROC_NewEmployee"
                    runat="server"  ></ui:userinfo>
            </div>
            <asp:TextBox runat="server" ID = "domin" style="display:none;" ></asp:TextBox>
            <div class="row">
                <p style="font-weight:bold;">Employee Information </p>
                <table class="table table-condensed table-bordered">
                    <tr>
                        <td class="td-label">
                         <span style=" height:30px; float:left;">&nbsp;</span>
                        <p style="text-align:center">Employee Name</p>
                        </td>
                        <td class="td-content" colspan="3" >
                        <asp:Label runat="server" ID="read_EmployeeName"></asp:Label>
                        </td>
                        <td class="td-label">
                         <span style="height:30px; float:left;">&nbsp;</span>
                        <p style="text-align:center">On-boarding date</p>
                        </td>
                        <td class="td-content" colspan="3" >
                        <asp:Label runat="server" ID="read_OBDate"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="td-label"> 
                       <span style=" margin-top:5px;  height:30px; float:left;">&nbsp;</span>
                        <p style="text-align:center;">On-boarding department</p>
                        </td>
                        <td class="td-content" colspan="3">
                            <asp:Label runat="server" ID="read_OBDepartment"></asp:Label>
                        </td>
                    
                        <%--<td class="td-label">
                         <span style="height:30px; float:left;">&nbsp;</span>
                        <p style="text-align:center">Post</p>
                        </td>
                        <td class="td-content" colspan="3" >
                        <asp:Label runat="server" ID="read_Post"></asp:Label>
                        </td>--%>
                        <td class="td-label"> 
                       <span style="height:30px; float:left;">&nbsp;</span>
                        <p style="text-align:center;">Training Place</p>
                        </td>
                        <td class="td-content" colspan="3">
                            <asp:Label runat="server" ID="read_TrainPlace"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="td-label" style="vertical-align:middle">
                         <span style="height:30px; float:left;">&nbsp;</span>
                        <p style="text-align:center">Training Content</p>
                        </td>
                        <td class="td-content" colspan="7" >
                        <asp:Label runat="server" ID="read_TrainContent"></asp:Label>
                        </td>
                    </tr>
                   
                </table>
                <p style="font-weight:bold;">Agenda of information（"<span style=" background:red;">&nbsp;</span>" must write） </p>
                <table class="table table-condensed table-bordered">
                    <tr>
                        <td class="td-label">
                         <span style=" background:red;  height:30px; float:left;">&nbsp;</span>
                        <p style="text-align:center">HSEF</p>
                        </td>
                        <td class="td-content">
                            <asp:CheckBox runat="server" ID="RadioButton1" Checked="true" /><asp:Label runat="server" ID="read_HSEF" ></asp:Label>
                            <%--<asp:RadioButton  runat="server" ID="RadioButton2" GroupName="FinanceAdmin" Value="Haobin.Duan" onclick="getButtonCheck(this,2)" />Haobin.Duan--%>
                          <%--  <asp:TextBox runat="server" ID="fld_FinanceAdmin" style="display:none;"></asp:TextBox>--%>
                        </td>
                    <%--</tr>
                    <tr>
                        <td class="td-label">
                         <span style=" background:red; height:30px; float:left;">&nbsp;</span>
                        <p style="text-align:center">Train Place</p>
                        </td>
                        <td class="td-content" colspan="3" >
                            <asp:TextBox runat="server" ID="fld_FinanceTrainPlace" Width="96%" CssClass="validate[required]"></asp:TextBox>
                        </td>--%>
                        <td class="td-label">
                         <span style=" background:red;  height:30px; float:left;">&nbsp;</span>
                        <p style="text-align:center">Training Date</p>
                        </td>
                        <td class="td-content">
                            <asp:TextBox runat="server" ID="fld_HSEFTrainDate" Width="96%" CssClass="validate[required]" onclick="WdatePicker({readOnly:false,dateFmt: 'yyyy-MM-dd'})"></asp:TextBox>
                        </td>
                    </tr>
                    <%--<tr>
                         <td class="td-label" style="vertical-align:middle">
                         <span style=" background:red;  height:30px; float:left;">&nbsp;</span>
                        <p style="text-align:center">Train Content</p>
                        </td>
                        <td class="td-content" colspan="7" >
                        <asp:TextBox runat="server" ID="fld_FinanceTrainContent" Rows="3" TextMode="MultiLine" Width="98%"  CssClass="validate[required]"></asp:TextBox>
                        </td>
                    </tr>--%>
                </table>
            </div>
            <%--<div class="row" style="display:none;">
                <attach:attachments id="Attachments1" runat="server"></attach:attachments>
            </div>--%>
            <div class="row">
             <ah:approvalhistory id="ApprovalHistory1" showaction="true" runat="server"></ah:approvalhistory>
            </div>
        </div>
        <btn:buttonlist id="ButtonList1" runat="server"></btn:buttonlist>
        <div style="display:none">
        <asp:TextBox runat="server" ID="fld_TRSummary"></asp:TextBox>
        <asp:HiddenField runat="server" ID="hdIncident" />
        <asp:HiddenField runat="server" ID="hdPrint" />
        </div>
    </form>
</body>
</html>
