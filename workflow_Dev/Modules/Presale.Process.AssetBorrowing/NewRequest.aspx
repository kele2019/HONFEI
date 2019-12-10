<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="NewRequest.aspx.cs" Inherits="Presale.Process.AssetBorrowing.NewRequest" %>

<%@ Register Src="/Modules/Ultimus.UWF.Form.ProcessControl/UserInfo.ascx" TagName="UserInfo" TagPrefix="ui" %>
<%@ Register Src="/Modules/Ultimus.UWF.Form.ProcessControl/ApprovalHistory.ascx" TagName="ApprovalHistory" TagPrefix="ah" %>
<%@ Register Src="/Modules/Ultimus.UWF.Form.ProcessControl/Attachments.ascx" TagName="Attachments" TagPrefix="attach" %>
<%@ Register Src="/Modules/Ultimus.UWF.Form.ProcessControl/ButtonList.ascx" TagName="ButtonList" TagPrefix="btn" %>

<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
        <meta http-equiv="X-UA-Compatible"  content="IE=EmulateIE8" />
    <title>IT Asset Borrowing Request Process</title>
    <script type="text/javascript" src="/Assets/js/common.js"></script>  
    <script type="text/javascript">
        function beforeSubmit() {
            if ($("#rea1").attr("checked")) {
                $("#fld_IANo").val("yes");
            }
            if ($("#rea2").attr("checked")) {
                $("#fld_IANo").val("no");
            }

            $("#fld_IAName").val($("#dropIAName option:selected").text());
            $("#fld_startHour").val($("#dropstarthour option:selected").text());
            $("#fld_endHour").val($("#dropendhour option:selected").text());
            var summary = "IT Asset Borrowing Request Process";
            $("#UserInfo1_fld_PROCESSSUMMARY").val(summary);
            return true;
        }
        function getITANameChange(obj) {
            if ($(obj).find("option:selected").text() == "laptop") {
                $("#iano").show();
                $("#ianoValue").show();
            }
            else {
                $("#iano").hide();
                $("#ianoValue").hide();
            }
        }
        function showTime(obj) {
            var time = new Date(obj.replace(/-/g,"/"));
            var year = time.getFullYear();
            var month = time.getMonth() + 1;
            var date = time.getDate();
            return (year + "-" + month + "-" + date) == "NaN-NaN-NaN" ? "" : (year + "-" + month + "-" + date);
        }
        $(document).ready(function () {
            $("#dropstarthour").val($("#fld_startHour").val());
            $("#dropendhour").val($("#fld_endHour").val());
            $("#fld_ApplicantUserName").val($("#UserInfo1_fld_APPLICANT").text());
            if ($("#fld_Receive").val() == "getting once") {
                $("#receive1").attr("checked", true);
            }
            if ($("#hdUrgeTask").val() == "Yes") {
                $("#ReturnBackTask").show();
            }
            if ($("#fld_Receive").val() == "borrowing") {
                $("#receive3").attr("checked", true);
                $("#time").show();
                $("#type").attr("colspan", "3");
            }
            if ($("#dropIAName option:selected").text() == "laptop") {
                $("#iano").show();
                $("#ianoValue").show();
            }
            $("#fld_borrowTimeStart").val(showTime($("#fld_borrowTimeStart").val()));
            $("#fld_borrowTimeEnd").val(showTime($("#fld_borrowTimeEnd").val()));
            if ($("#hdIncident").val() != "") {
                $("#ButtonList1_btnSubmit").val("Submit");
                $("#ButtonList1_btnBack").hide();
                $("#ButtonList1_btnReject").show();
            }
            if ($("#fld_IANo").val() == "yes") {
                $("#rea1").attr("checked", true);
            }
            if ($("#fld_IANo").val() == "no") {
                $("#rea2").attr("checked", true);
            }
        });
        function getButtonCheck(obj, index) {
            if (index == "1") {
                if ($(obj).attr("checked")) {
                    $(obj).parent().attr("colspan", "7");
                    $("#time").hide();
                    $("#receive3").attr("checked", false);
                    $("#fld_Receive").val("getting once");
                }
            }
//            if (index == "2") {
//                $("#receive1").attr("checked",false);
//                $("#receive3").attr("checked",false);
//            }
            if (index == "3") {
                if ($(obj).attr("checked")) {
                    $("#receive1").attr("checked", false);
                    $("#fld_Receive").val("borrowing");
                    $(obj).parent().attr("colspan", "3");
                    $("#time").show();
                }
                else {
                    $(obj).parent().attr("colspan", "7");
                    $("#time").hide();
                }
            }
        }
        function beforeSave() {
            var summary = "IT Asset Borrowing Request Process";
            $("#UserInfo1_fld_PROCESSSUMMARY").val(summary);
            $("#fld_TRSummary").val(summary);
            return true;
        }
        
        function drophour_onchange(obj){
            $(obj).next().val($(obj).val());
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
     <div id="myDiv" class="container">
            <div class="row">
                <ui:userinfo id="UserInfo1" processtitle="IT Asset Borrowing Request Process" processprefix="ITAB" tablename="PROC_AssetBorrowing"
                    runat="server"  ></ui:userinfo>
           <asp:TextBox runat="server" ID="fld_ApplicantUserName" style="display:none;"></asp:TextBox>
                <asp:TextBox runat="server" ID="fld_DepartmentManager" style="display:none;"></asp:TextBox>
                   <%--  <asp:TextBox runat="server" ID="fld_DGMLogin" style="display:none;"></asp:TextBox>
                <asp:TextBox runat="server" ID="fld_GMLogin" style="display:none;"></asp:TextBox>
                <asp:TextBox runat="server" ID="fld_deptLogin" style="display:none;"></asp:TextBox>
                <asp:TextBox runat="server" ID="fld_ApplierLogin" style="display:none;"></asp:TextBox>
                <asp:TextBox runat="server" ID="fld_ITMLogin" style="display:none;"></asp:TextBox>
                <asp:TextBox runat="server" ID="fld_ITHelpLogin" style="display:none;"></asp:TextBox>
                <asp:TextBox runat="server" ID="fld_AdminLogin" style="display:none;"></asp:TextBox>--%>
            </div>
            <div class="row">
                <p style="font-weight:bold;">Request require("<span style=" background:red;">&nbsp;</span>" must write） </p>
                <table class="table table-condensed table-bordered">
                   <%-- <tr>
                        <td class="td-label">
                         <span style=" background:red;  height:30px; float:left;">&nbsp;</span>
                        <p style="text-align:center">Phone</p>
                        </td>
                        <td class="td-content" colspan="7" >
                            <asp:TextBox runat="server" ID="fld_Phone" Width="98%"  CssClass="validate[required]"></asp:TextBox>
                        </td>
                    </tr>--%>
                    <tr>
                        <td class="td-label">
                         <span style=" background:red;  height:30px; float:left;">&nbsp;</span>
                        <p style="text-align:center">Type</p>
                        </td>
                        <td class="td-content" colspan="7" id="type">
                            <asp:CheckBox runat="server" ID="receive1" value="getting once" onclick="getButtonCheck(this,1)" />getting once &nbsp;&nbsp;
                           <%-- <asp:CheckBox runat="server" ID="receive2" value="往复领取" onclick="receiveClick(2)" />往复领取 &nbsp;&nbsp;--%>
                            <asp:CheckBox runat="server" ID="receive3" value="borrowing" onclick="getButtonCheck(this,3)"/>borrowing
                            <asp:TextBox runat="server" ID="fld_Receive"  CssClass="validate[required]" style="display:none;"></asp:TextBox>
                        </td>
                            
                            <td id="time" style="display:none;" colspan="4" class="td-content" >
                                <%--<p style="float:left">From</p>--%><span style=" background:red;  height:30px; float:left;">&nbsp;</span>
                                From&nbsp;&nbsp;<span><asp:TextBox runat="server" ID="fld_borrowTimeStart" CssClass="validate[required]" onclick="WdatePicker({readOnly:false,dateFmt: 'yyyy-MM-dd',maxDate:'#F{$dp.$D(\'fld_borrowTimeEnd\')}'})" width="20%"></asp:TextBox></span>&nbsp;&nbsp;
                                <asp:DropDownList runat="server" ID="dropstarthour" onchange="drophour_onchange(this)" Width="15%">
                                        <asp:ListItem Selected="True">00</asp:ListItem>
                                        <asp:ListItem>01</asp:ListItem>
                                        <asp:ListItem>02</asp:ListItem>
                                        <asp:ListItem>03</asp:ListItem>
                                        <asp:ListItem>04</asp:ListItem>
                                        <asp:ListItem>05</asp:ListItem>
                                        <asp:ListItem>06</asp:ListItem>
                                        <asp:ListItem>07</asp:ListItem>
                                        <asp:ListItem>08</asp:ListItem>
                                        <asp:ListItem>09</asp:ListItem>
                                        <asp:ListItem>10</asp:ListItem>
                                        <asp:ListItem>11</asp:ListItem>
                                        <asp:ListItem>12</asp:ListItem>
                                        <asp:ListItem>13</asp:ListItem>
                                        <asp:ListItem>14</asp:ListItem>
                                        <asp:ListItem>15</asp:ListItem>
                                        <asp:ListItem>16</asp:ListItem>
                                        <asp:ListItem>17</asp:ListItem>
                                        <asp:ListItem>18</asp:ListItem>
                                        <asp:ListItem>19</asp:ListItem>
                                        <asp:ListItem>20</asp:ListItem>
                                        <asp:ListItem>21</asp:ListItem>
                                        <asp:ListItem>22</asp:ListItem>
                                        <asp:ListItem>23</asp:ListItem>
                                </asp:DropDownList>
                               <asp:TextBox runat="server" ID="fld_startHour" style="display:none;"></asp:TextBox>
                               <%-- <p style="float:left">To</p>--%>
                               To&nbsp;&nbsp;<span><asp:TextBox runat="server" ID="fld_borrowTimeEnd" CssClass="validate[required]" onclick="WdatePicker({readOnly:false,dateFmt: 'yyyy-MM-dd',minDate:'#F{$dp.$D(\'fld_borrowTimeStart\')}'})"  width="20%"></asp:TextBox></span>&nbsp;&nbsp;
                               <asp:DropDownList runat="server" ID="dropendhour" onchange="drophour_onchange(this)" Width="15%">
                                        <asp:ListItem Selected="True">00</asp:ListItem>
                                        <asp:ListItem>01</asp:ListItem>
                                        <asp:ListItem>02</asp:ListItem>
                                        <asp:ListItem>03</asp:ListItem>
                                        <asp:ListItem>04</asp:ListItem>
                                        <asp:ListItem>05</asp:ListItem>
                                        <asp:ListItem>06</asp:ListItem>
                                        <asp:ListItem>07</asp:ListItem>
                                        <asp:ListItem>08</asp:ListItem>
                                        <asp:ListItem>09</asp:ListItem>
                                        <asp:ListItem>10</asp:ListItem>
                                        <asp:ListItem>11</asp:ListItem>
                                        <asp:ListItem>12</asp:ListItem>
                                        <asp:ListItem>13</asp:ListItem>
                                        <asp:ListItem>14</asp:ListItem>
                                        <asp:ListItem>15</asp:ListItem>
                                        <asp:ListItem>16</asp:ListItem>
                                        <asp:ListItem>17</asp:ListItem>
                                        <asp:ListItem>18</asp:ListItem>
                                        <asp:ListItem>19</asp:ListItem>
                                        <asp:ListItem>20</asp:ListItem>
                                        <asp:ListItem>21</asp:ListItem>
                                        <asp:ListItem>22</asp:ListItem>
                                        <asp:ListItem>23</asp:ListItem>
                               </asp:DropDownList>
                               <asp:TextBox runat="server" ID="fld_endHour" style="display:none;"></asp:TextBox>
                            </td>
                    </tr>
                    <tr>
                        <td class="td-label"> 
                       <span style=" background:red;  height:30px; float:left;">&nbsp;</span>
                        <p style="text-align:center;">IT Assets Name</p>
                        </td>
                        <td class="td-content"  colspan="3" id="iaName">
                            <asp:DropDownList ID="dropIAName" runat="server" style="width:98%;" onchange="getITANameChange(this)"></asp:DropDownList>
                            <asp:TextBox runat="server" ID="fld_IAName" style="display:none;"></asp:TextBox>
                        </td>
                        <td class="td-label" > 
                        <div style="display:none;" id="iano">
                       <span style=" background:red;  height:30px; float:left;">&nbsp;</span>
                       <p style="text-align:center">Remote Email Access</p>
                       </div>
                        </td>
                        <td class="td-content"  colspan="3" >
                            <div style="display:none" id="ianoValue">
                            <asp:RadioButton runat="server" ID="rea1" GroupName="RemoteEA" />yes&nbsp;&nbsp;&nbsp;&nbsp;
                            <asp:RadioButton runat="server" ID="rea2" GroupName="RemoteEA" />no
                            <asp:TextBox runat="server" ID="fld_IANo"  Width="95%"  CssClass="validate[required]" style="display:none;"></asp:TextBox>
                            </div>
                        </td>
                    </tr>
                    
                    <tr>
                        <td class="td-label" style="vertical-align:middle;">
                         <span style=" background:red;  height:30px; float:left;">&nbsp;</span>
                        <p style="text-align:center">Reason</p>
                        </td>
                        <td class="td-content" colspan="7" >
                            <asp:TextBox ID="fld_ReceiveReason" runat="server" Width="98%" CssClass="validate[required]"></asp:TextBox>
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


