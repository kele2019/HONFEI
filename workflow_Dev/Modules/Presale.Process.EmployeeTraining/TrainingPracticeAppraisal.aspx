<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="TrainingPracticeAppraisal.aspx.cs" Inherits="Presale.Process.EmployeeTraining.TrainingPracticeAppraisal" %>

<%@ Register Src="/Modules/Ultimus.UWF.Form.ProcessControl/UserInfo.ascx" TagName="UserInfo" TagPrefix="ui" %>
<%@ Register Src="/Modules/Ultimus.UWF.Form.ProcessControl/ApprovalHistory.ascx" TagName="ApprovalHistory" TagPrefix="ah" %>
<%@ Register Src="/Modules/Ultimus.UWF.Form.ProcessControl/Attachments.ascx" TagName="Attachments" TagPrefix="attach" %>
<%@ Register Src="/Modules/Ultimus.UWF.Form.ProcessControl/ButtonList.ascx" TagName="ButtonList" TagPrefix="btn" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <base target="_self"></base>
    <meta http-equiv="Expires" content="-1" />
    <meta http-equiv="PRAGMA" content="NO-CACHE" />
    <meta http-equiv="Cache-Control" content="no-cache" />
    <title>Training Practice Appraisal</title>
    <script type="text/javascript" src="/Assets/js/common.js"></script>
   
    <script type="text/javascript">
        function SinglePersonConfirm() {
                jQuery("#form1").validationEngine('attach', {
                    onValidationComplete: function (form, status) {
                        if (status == false) {
                        }
                    }
                });
                $("#completeEvaluation").val("Yes");
                var returnJson = "[{'CompleteEvalueation':'Yes'}]";
                window.returnValue = returnJson;
                window.close();
        }
        function checkOption()
        {
            if ($("#AgreeOrNot2").attr("checked") && $("#fld_Opinion").val() == "") {
                alert("Please Input What needs to improve in your opinion");
                return false;
             }
            
        }

        function CloseForm() {
        
                $("#completeEvaluation").val("No");
                var returnJson = "[{'CompleteEvalueation':'No'}]";
                window.returnValue = returnJson;
                window.close();
           
        }
        function getButtonCheck(obj, index) {
            if (index == "1") {
                if ($(obj).attr("checked")) {
                    $("#fld_ApplicableValue").val("Bad");
                    $("#ApplicableValue2").attr("checked", false);
                    $("#ApplicableValue3").attr("checked", false);
                }
            }
            if (index == "2") {
                if ($(obj).attr("checked")) {
                    $("#fld_ApplicableValue").val("Medium");
                    $("#ApplicableValue1").attr("checked", false);
                    $("#ApplicableValue3").attr("checked", false);
                }
            }
            if (index == "3") {
                if ($(obj).attr("checked")) {
                    $("#fld_ApplicableValue").val("Very Well");
                    $("#ApplicableValue1").attr("checked", false);
                    $("#ApplicableValue2").attr("checked", false);
                }
            }
            if (index == "4") {
                if ($(obj).attr("checked")) {
                    $("#fld_WorkingRequirement").val("Bad");
                    $("#WorkingRequirement2").attr("checked", false);
                    $("#WorkingRequirement3").attr("checked", false);
                }
            }
            if (index == "5") {
                if ($(obj).attr("checked")) {
                    $("#fld_WorkingRequirement").val("Medium");
                    $("#WorkingRequirement1").attr("checked", false);
                    $("#WorkingRequirement3").attr("checked", false);
                }
            }
            if (index == "6") {
                if ($(obj).attr("checked")) {
                    $("#fld_WorkingRequirement").val("Very Well");
                    $("#WorkingRequirement1").attr("checked", false);
                    $("#WorkingRequirement2").attr("checked", false);
                }
            }
            if (index == "7") {
                if ($(obj).attr("checked")) {
                    $("#fld_TimeArrangement").val("Bad");
                    $("#TimeArrangement2").attr("checked", false);
                    $("#TimeArrangement3").attr("checked", false);
                }
            }
            if (index == "8") {
                if ($(obj).attr("checked")) {
                    $("#fld_TimeArrangement").val("Medium");
                    $("#TimeArrangement1").attr("checked", false);
                    $("#TimeArrangement3").attr("checked", false);
                }
            }
            if (index == "9") {
                if ($(obj).attr("checked")) {
                    $("#fld_TimeArrangement").val("Very Well");
                    $("#TimeArrangement1").attr("checked", false);
                    $("#TimeArrangement2").attr("checked", false);
                }
            }
            if (index == "10") {
                if ($(obj).attr("checked")) {
                    $("#fld_MaterialQuality").val("Bad");
                    $("#MaterialQuality2").attr("checked", false);
                    $("#MaterialQuality3").attr("checked", false);
                }
            }
            if (index == "11") {
                if ($(obj).attr("checked")) {
                    $("#fld_MaterialQuality").val("Medium");
                    $("#MaterialQuality1").attr("checked", false);
                    $("#MaterialQuality3").attr("checked", false);
                }
            }
            if (index == "12") {
                if ($(obj).attr("checked")) {
                    $("#fld_MaterialQuality").val("Very Well");
                    $("#MaterialQuality1").attr("checked", false);
                    $("#MaterialQuality2").attr("checked", false);
                }
            }
            if (index == "13") {
                if ($(obj).attr("checked")) {
                    $("#fld_PowerOE").val("Bad");
                    $("#PowerOE2").attr("checked", false);
                    $("#PowerOE3").attr("checked", false);
                }
            }
            if (index == "14") {
                if ($(obj).attr("checked")) {
                    $("#fld_PowerOE").val("Medium");
                    $("#PowerOE1").attr("checked", false);
                    $("#PowerOE3").attr("checked", false);
                }
            }
            if (index == "15") {
                if ($(obj).attr("checked")) {
                    $("#fld_PowerOE").val("Very Well");
                    $("#PowerOE1").attr("checked", false);
                    $("#PowerOE2").attr("checked", false);
                }
            }
            if (index == "16") {
                if ($(obj).attr("checked")) {
                    $("#fld_Motivation").val("Bad");
                    $("#Motivation2").attr("checked", false);
                    $("#Motivation3").attr("checked", false);
                }
            }
            if (index == "17") {
                if ($(obj).attr("checked")) {
                    $("#fld_Motivation").val("Medium");
                    $("#Motivation1").attr("checked", false);
                    $("#Motivation3").attr("checked", false);
                }
            }
            if (index == "18") {
                if ($(obj).attr("checked")) {
                    $("#fld_Motivation").val("Very Well");
                    $("#Motivation1").attr("checked", false);
                    $("#Motivation2").attr("checked", false);
                }
            }
            if (index == "19") {
                if ($(obj).attr("checked")) {
                    $("#fld_ResponseTQ").val("Bad");
                    $("#ResponseTQ2").attr("checked", false);
                    $("#ResponseTQ3").attr("checked", false);
                }
            }
            if (index == "20") {
                if ($(obj).attr("checked")) {
                    $("#fld_ResponseTQ").val("Medium");
                    $("#ResponseTQ1").attr("checked", false);
                    $("#ResponseTQ3").attr("checked", false);
                }
            }
            if (index == "21") {
                if ($(obj).attr("checked")) {
                    $("#fld_ResponseTQ").val("Very Well");
                    $("#ResponseTQ1").attr("checked", false);
                    $("#ResponseTQ2").attr("checked", false);
                }
            }
            if (index == "22") {
                if ($(obj).attr("checked")) {
                    $("#fld_AOTCourse").val("Bad");
                    $("#AOTCourse2").attr("checked", false);
                    $("#AOTCourse3").attr("checked", false);
                }
            }
            if (index == "23") {
                if ($(obj).attr("checked")) {
                    $("#fld_AOTCourse").val("Medium");
                    $("#AOTCourse1").attr("checked", false);
                    $("#AOTCourse3").attr("checked", false);
                }
            }
            if (index == "24") {
                if ($(obj).attr("checked")) {
                    $("#fld_AOTCourse").val("Very Well");
                    $("#AOTCourse1").attr("checked", false);
                    $("#AOTCourse2").attr("checked", false);
                }
            }
            if (index == "25") {
                if ($(obj).attr("checked")) {
                    $("#fld_AgreeOrNot").val("Agree");
                    $("#AgreeOrNot2").attr("checked",false);
                }
            }
            if (index == "26") {
                if ($(obj).attr("checked")) {
                    $("#fld_AgreeOrNot").val("Not agree, please state the reason.");
                    $("#AgreeOrNot1").attr("checked", false);
                }
            }
        }
    </script>
</head>
<body>
    <form id="form1" runat="server" style="width:90%;margin-left:5%; margin-top:5%;margin-bottom:5%;">
     <div id="myDiv" style="width:98%;" align="center">
            <div style="width:98%" align="center">
                <%--<p style="font-weight:bold;">Request require（"<span style=" background:red;">&nbsp;</span>" must write） </p>--%>
                <table  style="width:90%" class="table table-condensed table-bordered">
                    <tr>
                        <th style="width:20%">
                            <p style="text-align:center">Topic</p>
                        </th>
                        <td colspan="3" >
                            <asp:Label runat="server" ID="fld_CourseName" ></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <th style="width:20%"> 
                            <p style="text-align:center">Trainer</p>
                        </th>
                        <td>
                            <asp:Label runat="server"  ID="fld_Trainer"  ></asp:Label>
                        </td>
                        <th style="width:20%"> 
                           
                            <p style="text-align:center">Date</p>
                        </th>
                        <td >
                            <asp:Label runat="server"  ID="fld_Date"    ></asp:Label>
                        </td>
                    </tr>
                </table>
                <%--<p style="font-weight:bold;">Course Content（"<span style=" background:red;">&nbsp;</span>" must write） </p>--%>
                <table style="width:90%" class="table table-condensed table-bordered" >
                    <tr>
                        <th>
                            <p style="text-align:center">Course Content</p>
                        </th>
                        <th>
                            <p style="text-align:center">Bad</p>
                        </th>
                        <th>
                            <p style="text-align:center">Medium</p>
                        </th>
                        <th>
                            <p style="text-align:center">Very Well</p>
                        </th>
                    </tr>
                    <tr>
                        <th style="width:35%">
                            <span style=" background:red;  height:30px; float:left;">&nbsp;</span>
                            <p style="text-align:center">Applicable value</p>
                        </th>
                        <td  style="text-align:center">
                            <asp:CheckBox runat="server" ID="ApplicableValue1" onclick="getButtonCheck(this,1)" />
                        </td>
                        <td style="text-align:center">
                            <asp:CheckBox runat="server" ID="ApplicableValue2" onclick="getButtonCheck(this,2)"/>
                        </td>
                        <td style="text-align:center">
                            <asp:CheckBox runat="server" ID="ApplicableValue3" onclick="getButtonCheck(this,3)"/>
                            <asp:TextBox runat="server" ID="fld_ApplicableValue" style="display:none;"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <th style="width:35%"> 
                            <span style=" background:red; margin-top:5px;  height:30px; float:left;">&nbsp;</span>
                            <p style="text-align:center">Working Requirement</p>
                        </th>
                         <td style="text-align:center">
                            <asp:CheckBox runat="server" ID="WorkingRequirement1" onclick="getButtonCheck(this,4)"/>
                        </td>
                        <td style="text-align:center">
                            <asp:CheckBox runat="server" ID="WorkingRequirement2" onclick="getButtonCheck(this,5)" />
                        </td>
                        <td style="text-align:center">
                            <asp:CheckBox runat="server" ID="WorkingRequirement3" onclick="getButtonCheck(this,6)"/>
                            <asp:TextBox runat="server" ID="fld_WorkingRequirement" style="display:none;"></asp:TextBox>
                        </td>
                    </tr>
                </table>
                <table style="width:90%" class="table table-condensed table-bordered">
                    <tr>
                        <th>
                            <p style="text-align:center">Course Arrangement</p>
                        </th>
                        <th>
                            <p style="text-align:center">Bad</p>
                        </th>
                        <th>
                            <p style="text-align:center">Medium</p>
                        </th>
                        <th>
                            <p style="text-align:center">Very Well</p>
                        </th>
                    </tr>
                     <tr>
                        <th style="width:35%"> 
                            <span style=" background:red; margin-top:5px;  height:30px; float:left;">&nbsp;</span>
                            <p style="text-align:center">Time arrangement</p>
                        </th>
                        <td style="text-align:center">
                            <asp:CheckBox runat="server" ID="TimeArrangement1" onclick="getButtonCheck(this,7)"/>
                        </td>
                        <td style="text-align:center">
                            <asp:CheckBox runat="server" ID="TimeArrangement2" onclick="getButtonCheck(this,8)"/>
                        </td>
                        <td style="text-align:center">
                            <asp:CheckBox runat="server" ID="TimeArrangement3" onclick="getButtonCheck(this,9)"/>
                            <asp:TextBox runat="server" ID="fld_TimeArrangement" style="display:none;"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <th style="width:35%"> 
                            <span style=" background:red; margin-top:5px;  height:30px; float:left;">&nbsp;</span>
                            <p style="text-align:center">Material Quality</p>
                        </th>
                        <td style="text-align:center">
                            <asp:CheckBox runat="server" ID="MaterialQuality1" onclick="getButtonCheck(this,10)"/>
                        </td>
                        <td style="text-align:center">
                            <asp:CheckBox runat="server" ID="MaterialQuality2" onclick="getButtonCheck(this,11)"/>
                        </td>
                        <td style="text-align:center">
                            <asp:CheckBox runat="server" ID="MaterialQuality3" onclick="getButtonCheck(this,12)"/>
                            <asp:TextBox runat="server" ID="fld_MaterialQuality" style="display:none;"></asp:TextBox>
                        </td>
                    </tr>
                </table>
                <table style="width:90%" class="table table-condensed table-bordered">
                    <tr style="width:35%">
                        <th>
                            <p style="text-align:center">Trainer</p>
                        </th>
                        <th>
                            <p style="text-align:center">Bad</p>
                        </th>
                        <th>
                            <p style="text-align:center">Medium</p>
                        </th>
                        <th>
                            <p style="text-align:center">Very Well</p>
                        </th>
                    </tr>
                    <tr>
                        <th style="width:35%"> 
                            <span style=" background:red; margin-top:5px;  height:30px; float:left;">&nbsp;</span>
                            <p style="text-align:center">Power of Expression</p>
                        </th>
                         <td style="text-align:center">
                            <asp:CheckBox runat="server" ID="PowerOE1" onclick="getButtonCheck(this,13)"/>
                        </td>
                        <td style="text-align:center">
                            <asp:CheckBox runat="server" ID="PowerOE2" onclick="getButtonCheck(this,14)"/>
                        </td>
                        <td style="text-align:center">
                            <asp:CheckBox runat="server" ID="PowerOE3" onclick="getButtonCheck(this,15)"/>
                            <asp:TextBox runat="server" ID="fld_PowerOE" style="display:none;"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <th style="width:35%"> 
                            <span style=" background:red; margin-top:5px;  height:30px; float:left;">&nbsp;</span>
                            <p style="text-align:center">Motivation</p>
                        </th>
                        <td style="text-align:center">
                            <asp:CheckBox runat="server" ID="Motivation1" onclick="getButtonCheck(this,16)"/>
                        </td>
                        <td style="text-align:center">
                            <asp:CheckBox runat="server" ID="Motivation2" onclick="getButtonCheck(this,17)"/>
                        </td>
                        <td style="text-align:center">
                            <asp:CheckBox runat="server" ID="Motivation3" onclick="getButtonCheck(this,18)"/>
                            <asp:TextBox runat="server" ID="fld_Motivation" style="display:none;"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <th style="width:35%"> 
                            <span style=" background:red; margin-top:5px;  height:30px; float:left;">&nbsp;</span>
                            <p style="text-align:center">Response to question</p>
                        </th>
                        <td style="text-align:center">
                            <asp:CheckBox runat="server" ID="ResponseTQ1" onclick="getButtonCheck(this,19)"/>
                        </td>
                        <td style="text-align:center">
                            <asp:CheckBox runat="server" ID="ResponseTQ2" onclick="getButtonCheck(this,20)"/>
                        </td>
                        <td style="text-align:center">
                            <asp:CheckBox runat="server" ID="ResponseTQ3" onclick="getButtonCheck(this,21)"/>
                            <asp:TextBox runat="server" ID="fld_ResponseTQ" style="display:none;"></asp:TextBox>
                        </td>
                    </tr>
                </table>
                <table style="width:90%" class="table table-condensed table-bordered">
                    <tr>
                        <th>
                            <p style="text-align:center">Overall Comments</p>
                        </th>
                        <th>
                            <p style="text-align:center">Bad</p>
                        </th>
                        <th>
                            <p style="text-align:center">Medium</p>
                        </th>
                        <th>
                            <p style="text-align:center">Very Well</p>
                        </th>
                    </tr>
                    <tr>
                        <th style="width:35%"> 
                            <span style=" background:red; margin-top:5px;  height:30px; float:left;">&nbsp;</span>
                            <p style="text-align:center">Appraisal of the Course</p>
                        </th>
                         <td style="text-align:center">
                            <asp:CheckBox runat="server" ID="AOTCourse1" onclick="getButtonCheck(this,22)"/>
                        </td>
                        <td style="text-align:center">
                            <asp:CheckBox runat="server" ID="AOTCourse2" onclick="getButtonCheck(this,23)"/>
                        </td>
                        <td style="text-align:center">
                            <asp:CheckBox runat="server" ID="AOTCourse3" onclick="getButtonCheck(this,24)"/>
                            <asp:TextBox runat="server" ID="fld_AOTCourse" style="display:none;"></asp:TextBox>
                        </td>
                    </tr>
                </table>
                 <table style="width:90%" class="table table-condensed table-bordered">
                    <tr>
                        <th style="vertical-align:middle;width:35%">
                            <span style=" background:red;  height:30px; float:left;">&nbsp;</span>
                            <p style="text-align:center;width:90%">What needs to improve in your opinion?</p>
                        </th>
                        <td colspan="3">
                            <asp:TextBox runat="server" ID="fld_Opinion" TextMode="MultiLine" Rows="5" Width="96%"></asp:TextBox>
                        </td>
                    </tr>
                </table>
                <table style="width:90%" class="table table-condensed table-bordered">
                    <tr>
                        <th style="vertical-align:middle;width:50%">
                             <span style=" background:red;  height:30px; float:left;">&nbsp;</span>
                             <p style="text-align:center">After the mentioned training, I have understood the content of the course and sure will apply it into the my actual work.</p>
                        </th>
                        <td style="vertical-align:middle;" colspan="3" class="td-content">
                            <asp:CheckBox runat="server" ID="AgreeOrNot1" onclick="getButtonCheck(this,25)" Text="Agree" /><br />
                            <asp:CheckBox runat="server" ID="AgreeOrNot2" onclick="getButtonCheck(this,26)" Text="Not agree, please state the reason." />
                            <asp:TextBox runat="server" ID="fld_AgreeOrNot" style="display:none;"></asp:TextBox>
                        </td>
                    </tr>
                </table>
            </div>
            <asp:TextBox ID="fld_FormID" runat="server" style="display:none;" ></asp:TextBox>
            <asp:TextBox ID="completeEvaluation" runat="server" style="display:none;"></asp:TextBox>
            <div  style="width:90%" align="center">
                 <asp:Button ID="btnSave" runat="server" Text="OK" OnClientClick="return checkOption()" onclick="btnSave_Clcik"  CssClass="btn btn-primary"  />
                 <input type="button" value="Cancel" class="btn" onclick="CloseForm()"  />
                 <asp:HiddenField runat="server" ID="hdUserID" />
             </div>
        </div>
    </form>
</body>
</html>



