<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="NewRequest.aspx.cs" Inherits="Presale.Process.VoluntaryResignation.NewRequest" %>
<%@ Register Src="/Modules/Ultimus.UWF.Form.ProcessControl/UserInfo.ascx" TagName="UserInfo" TagPrefix="ui" %>
<%@ Register Src="/Modules/Ultimus.UWF.Form.ProcessControl/ApprovalHistory.ascx" TagName="ApprovalHistory" TagPrefix="ah" %>
<%@ Register Src="/Modules/Ultimus.UWF.Form.ProcessControl/Attachments.ascx" TagName="Attachments" TagPrefix="attach" %>
<%@ Register Src="/Modules/Ultimus.UWF.Form.ProcessControl/ButtonList.ascx" TagName="ButtonList" TagPrefix="btn" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta http-equiv="X-UA-Compatible"  content="IE=EmulateIE8" />
    <title>Voluntary Resignation Application Process</title>
    <script type="text/javascript" src="/Assets/js/common.js"></script>
    <script type="text/javascript">
        function showTime(obj) {
            var time = new Date(obj.replace(/-/g, "/"));
            var year = time.getFullYear();
            var month = time.getMonth() + 1;
            var date = time.getDate();
            return (year + "-" + month + "-" + date) == "NaN-NaN-NaN" ? "" : (year + "-" + month + "-" + date);
        }
        function beforeSubmit() {
            var summary = "Voluntary Resignation Application Process";
            $("#UserInfo1_fld_PROCESSSUMMARY").val(summary);
            return true;
        }
        function beforeSave() {
            var summary = "Voluntary Resignation Application Process";
            $("#UserInfo1_fld_PROCESSSUMMARY").val(summary);
            $("#fld_TRSummary").val(summary);
            return true;
        }
        $(document).ready(function () {
            $("#fld_LWDate").val(showTime($("#fld_LWDate").val()));
            $("#fld_HireDate").val(showTime($("#fld_HireDate").val()));
            $("#fld_ResignationDate").val(showTime($("#fld_ResignationDate").val()));
            if ($("#hdIncident").val() != "") {
                $("#ButtonList1_btnSubmit").val("Submit");
                $("#ButtonList1_btnBack").hide();
                $("#ButtonList1_btnReject").show();
            }
            if ($("#hdUrgeTask").val() == "Yes") {
                $("#ReturnBackTask").show();
            }
        });
        function openparent() {
            digStr = "dialogHeight:500px;dialogWidth:800px;"
            var ReturnValue = window.showModalDialog("../Ultimus.UWF.OrgChart/SelectAllUser.aspx", null, digStr);
            if (ReturnValue != null) {
                var returnValue = eval("(" + ReturnValue + ")");
                $("#fld_JHOP").val(returnValue[0].Name);
//                var loginName = returnValue[0].LoginName;
//                loginName = loginName.substring(8);
//                $("#agentLoginName").val(loginName);
            }
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
     <div id="myDiv" class="container">
            <div class="row">
                <ui:userinfo id="UserInfo1" processtitle="Voluntary Resignation Application Process" processprefix="VR" tablename="PROC_VoluntaryResignation"
                    runat="server"  ></ui:userinfo>
                <asp:TextBox runat="server" ID="fld_UserName" style="display:none;"></asp:TextBox>
               <%-- <asp:TextBox runat="server" ID="fld_HR" style="display:none;"></asp:TextBox>
                <asp:TextBox runat="server" ID="fld_HRLogin" style="display:none;"></asp:TextBox>
                <asp:TextBox runat="server" ID="fld_DGMLogin" style="display:none;"></asp:TextBox>
                <asp:TextBox runat="server" ID="fld_GMLogin" style="display:none;"></asp:TextBox>--%>
            </div>
            <div class="row">
                <p style="font-weight:bold;">Employee responsible for the following part（"<span style=" background:red;">&nbsp;</span>" must write） </p>
                <table class="table table-condensed table-bordered">
                    <tr>
                        <td class="td-label">
                            <span style=" background:red;  height:30px; float:left;">&nbsp;</span>
                            <p style="text-align:center">Chinese Name</p>
                        </td>
                        <td class="td-content" colspan="3">
                            <asp:TextBox runat="server" ID="fld_ChineseName" Width="94%" MaxLength="40" CssClass="validate[required]"></asp:TextBox>
                        </td>
                         <td class="td-label">
                            <span style=" background:red;  height:30px; float:left;">&nbsp;</span>
                            <p style="text-align:center">English Name</p>
                        </td>
                        <td class="td-content" colspan="3">
                            <asp:TextBox runat="server" ID="fld_EnglishName" MaxLength="40" Width="94%" CssClass="validate[required]"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="td-label">
                            <span style=" background:red;  height:30px; float:left;">&nbsp;</span>
                            <p style="text-align:center">ID#</p>
                        </td>
                        <td class="td-content" colspan="3">
                            <asp:TextBox runat="server" ID="fld_IDNumber" Width="94%" MaxLength="40" CssClass="validate[required]"></asp:TextBox>
                        </td>
                         <td class="td-label">
                            <span style=" background:red;  height:30px; float:left;">&nbsp;</span>
                            <p style="text-align:center">Department</p>
                        </td>
                        <td class="td-content" colspan="3">
                            <asp:TextBox runat="server" ID="fld_ApplicantDepartment" MaxLength="40" Width="94%" CssClass="validate[required]"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="td-label">
                            <span style=" background:red;  height:30px; float:left;">&nbsp;</span>
                            <p style="text-align:center">Location</p>
                        </td>
                        <td class="td-content" colspan="3">
                            <asp:TextBox runat="server" ID="fld_ApplicantLocation" Width="94%" MaxLength="40" CssClass="validate[required]"></asp:TextBox>
                        </td>
                         <td class="td-label">
                            <span style=" background:red;  height:30px; float:left;">&nbsp;</span>
                            <p style="text-align:center">Title</p>
                        </td>
                        <td class="td-content" colspan="3">
                            <asp:TextBox runat="server" ID="fld_ApplicantTitle" Width="94%" MaxLength="40" CssClass="validate[required]"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="td-label">
                            <span style=" background:red;  height:30px; float:left;">&nbsp;</span>
                            <p style="text-align:center">HireDate</p>
                        </td>
                        <td class="td-content" colspan="3">
                            <asp:TextBox runat="server" ID="fld_HireDate" Width="94%" CssClass="validate[required]" onclick="WdatePicker({readOnly:false,dateFmt: 'yyyy-MM-dd'})"></asp:TextBox>
                        </td>
                         <td class="td-label">
                            <span style=" background:red;  height:30px; float:left;">&nbsp;</span>
                            <p style="text-align:center">ResignationDate</p>
                        </td>
                        <td class="td-content" colspan="3">
                            <asp:TextBox runat="server" ID="fld_ResignationDate" Width="94%" CssClass="validate[required]" onclick="WdatePicker({readOnly:false,dateFmt: 'yyyy-MM-dd'})"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="td-label" style="vertical-align:middle">
                             <span style=" background:red;  height:30px; float:left;">&nbsp;</span>
                             <p style="text-align:center">Reasons of Resignation</p>
                        </td>
                        <td class="td-content" colspan="7" >
                            <asp:TextBox runat="server" ID="fld_Reason" TextMode="MultiLine" Rows="3" MaxLength="100" Width="98%"  CssClass="validate[required]"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="td-label"> 
                       <span style=" background:red; margin-top:5px;  height:30px; float:left;">&nbsp;</span>
                        <p style="text-align:center">Job Handover of People</p>
                        </td>
                        <td class="td-content"  colspan="3" >
                            <asp:TextBox runat="server" onclick="openparent()"  ID="fld_JHOP" MaxLength="40" CssClass="validate[required]"   Width="94%"></asp:TextBox>
                            <asp:TextBox runat="server" ID="fld_JHOPLogin" style="display:none"></asp:TextBox>
                        </td>
                        <td class="td-label"> 
                       <span style=" background:red; margin-top:5px;  height:30px; float:left;">&nbsp;</span>
                        <p style="text-align:center">Last Working Day</p>
                        </td>
                        <td class="td-content"  colspan="3" >
                            <asp:TextBox runat="server"  ID="fld_LWDate" CssClass="validate[required]"   Width="94%" onclick="WdatePicker({readOnly:false,dateFmt: 'yyyy-MM-dd'})"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td width="100%" class="td-content" colspan="8">
                            <p style="float:left;width:100%;">重要提示 IMPORTANCE:</p>
                            <p style="float:left;width:100%;"">1. 员工应提前三十日用此表书面通知公司其辞职决定。</p>
                            <p style="float:left;width:100%;"">The Employee may inform the Employer his/her resignation decision upon this 30 days' prior written form.</p>
                            <p style="float:left;width:100%;"">2. 员工应事先与直属经理沟通并确认工作交接人和最后工作日。</p>
                            <p style="float:left;width:100%;"">The Employee should communicate with Immediate Manager to confirm the job handover of people and the last working day.</p>
                            <p style="float:left;width:100%;"">3. 员工承诺在最后工作日之前完成所有的工作交接和离职清单。</p>
                            <p style="float:left;width:100%;"">The Employee agrees that he/she finishes all the job handover and completes the Exit Clearance Process by the last working day.</p>
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


