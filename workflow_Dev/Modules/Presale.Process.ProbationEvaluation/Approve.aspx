<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Approve.aspx.cs" Inherits="Presale.Process.ProbationEvaluation.Approve" %>
<%@ Register Src="/Modules/Ultimus.UWF.Form.ProcessControl/UserInfo.ascx" TagName="UserInfo" TagPrefix="ui" %>
<%@ Register Src="/Modules/Ultimus.UWF.Form.ProcessControl/ApprovalHistory.ascx" TagName="ApprovalHistory" TagPrefix="ah" %>
<%@ Register Src="/Modules/Ultimus.UWF.Form.ProcessControl/Attachments.ascx" TagName="Attachments" TagPrefix="attach" %>
<%@ Register Src="/Modules/Ultimus.UWF.Form.ProcessControl/ButtonList.ascx" TagName="ButtonList" TagPrefix="btn" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
 <meta http-equiv="X-UA-Compatible"  content="IE=EmulateIE8" />
    <title>Employee Probation Assessment Form</title>
    <script type="text/javascript" src="/Assets/js/common.js"></script>
    <script type="text/javascript">
        function CheckedValue(flag, vindex) {
            $('#fld_' + flag).val(vindex);
        }
        $(document).ready(function () {
            var REATINGValue = $("#fld_REATINGValue").val();
            if (REATINGValue != "")
                $("input[name^='REATINGValue']").eq((REATINGValue - 5) * (-1)).attr("checked", true);

            var Establishesvalue = $("#fld_Establishesvalue").val();
            if (Establishesvalue != "")
                $("input[name^='Establishesvalue']").eq((Establishesvalue - 5) * (-1)).attr("checked", true);

            var INITIATIVEDemonstratesvalue = $("#fld_INITIATIVEDemonstratesvalue").val();
            if (INITIATIVEDemonstratesvalue != "")
                $("input[name^='INITIATIVEDemonstratesvalue']").eq((INITIATIVEDemonstratesvalue - 5) * (-1)).attr("checked", true);

            var Organizesvalue = $("#fld_Organizesvalue").val();
            if (Organizesvalue != "")
                $("input[name^='Organizesvalue']").eq((Organizesvalue - 5) * (-1)).attr("checked", true);

            var Demonstrates1value = $("#fld_Demonstrates1value").val();
            if (Demonstrates1value != "")
                $("input[name^='Demonstrates1value']").eq((Demonstrates1value - 5) * (-1)).attr("checked", true);

            var DemonstratesValue = $("#fld_DemonstratesValue").val();
            if (DemonstratesValue != "")
                $("input[name^='DemonstratesValue']").eq((DemonstratesValue - 5) * (-1)).attr("checked", true);

            var DeptReusltValue = $("#fld_DeptReusltValue").val();
            // alert($("input[name^='DeptReuslt']").html());
            if (DeptReusltValue != "")
                $("input[name^='DeptReusltValue']").eq((DeptReusltValue - 3) * (-1)).attr("checked", true);
            var Type = request('type');
            if (Type != "mytask") {
                $("input[type='radio']").attr("disabled", true);
            }
            if ($("#read_EmployeeComments").text() != "") {
                $(".trem").show();
            }
        });
        function beforeSubmit() {
            var msg = "";
            if ($("#fld_DeptReusltValue").val() == "") {
                msg = "Pls select Manager's Result";
            }
            if (msg != "")
                return false;
            else
                return true;

        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
     <div id="myDiv" class="container">
        <div class="row">
            <ui:userinfo id="UserInfo1" processtitle="Employee Probation Assessment Form" processprefix="EP" tablename="PROC_ProbationEvaluation"
                tablenamedetail="" runat="server"></ui:userinfo>

                 <table class="table table-condensed table-bordered"   >
                 <tr>
                 <th  colspan="2">  JOB KNOWLEDGE </th>
                 <th>Rating</th>
                 </tr>
                 <tr>
                 <td colspan="2">Demonstrates good skills, understands the work environment, the job requirements and the customer needs. Completes assignments with minimum direction.</td>
                 <td><input type="radio" onclick="CheckedValue('DemonstratesValue','5')" name="DemonstratesValue" value="5" />5</td>
                
                 </tr>
                 <tr>
                 <td colspan="2">Thorough knowledge of most aspects of job, Well rounded job knowledge Requires little assistance.</td>
                 <td><input type="radio"  onclick="CheckedValue('DemonstratesValue','4')" name="DemonstratesValue"  value="4" />4</td>
                 </tr>
                 <tr>
                 <td colspan="2">Fairly good job knowledge. Requires some guidance.</td>
                 <td ><input type="radio"  onclick="CheckedValue('DemonstratesValue','3')" name="DemonstratesValue"   value="3" />3</td>
                 </tr>
                 <tr>
                 <td colspan="2">Limited job knowledge and makes no effort to improve.</td>
                 <td ><input type="radio"  onclick="CheckedValue('DemonstratesValue','2')" name="DemonstratesValue"  value="2" />2</td>
                 </tr>
                 <tr>
                 <td colspan="2">Knowledge inadequate to retain on the job.</td>
                 <td ><input type="radio" onclick="CheckedValue('DemonstratesValue','1')" name="DemonstratesValue"  value="1" />1</td>
                 </tr>
                 <tr>
                 <td colspan="3">
                 <asp:TextBox runat="server" ID="fld_Demonstrates" Width="98%" TextMode="MultiLine" Rows="3"></asp:TextBox>
                 <asp:TextBox runat="server" ID="fld_DemonstratesValue" style="display:none"></asp:TextBox>
                 </td>
                 </tr>
                 <tr>
                 <th colspan="2">QUALITY & ACCURACY</th>
                 <th>Rating</th>
               
                 </tr>
                 <tr>
                 <td colspan="2">Demonstrates a commitment to quality and the quality process. Completes assignments in an accurate and thorough manner. Produces work that meets standards </td>
                 <td ><input type="radio" value="5" onclick="CheckedValue('Demonstrates1value','5')" name="Demonstrates1value" />5</td>
               
                 </tr>

                 <tr>
                 <td colspan="2">Demonstrates a good attitude, Produces work that meets most of the job requirement.</td>
                 <td ><input type="radio" value="4"  onclick="CheckedValue('Demonstrates1value','4')" name="Demonstrates1value"/>4</td>
                 </tr>

                 <tr>
                 <td colspan="2">Acceptable standards, have a positive attitude, but less professional knowledge so that sometimes have few errors.</td>
                 <td ><input type="radio" value="3" onclick="CheckedValue('Demonstrates1value','3')" name="Demonstrates1value" />3</td>
                 </tr>

                 <tr>
                 <td colspan="2">Acceptable work less productivity, occasional errors.</td>
                 <td ><input type="radio" value="2" onclick="CheckedValue('Demonstrates1value','2')" name="Demonstrates1value" />2</td>
                 </tr>

                 <tr>
                 <td colspan="2">Work too poor to retain on the job.</td>
                 <td ><input type="radio" value="1" onclick="CheckedValue('Demonstrates1value','1')" name="Demonstrates1value" />1</td>
                 </tr>
                 <tr>
                 <td colspan="3">
                 <asp:TextBox runat="server" ID="fld_Demonstrates1" Width="98%" TextMode="MultiLine" Rows="3"></asp:TextBox>
                 <asp:TextBox runat="server" ID="fld_Demonstrates1value" style="display:none"></asp:TextBox>
                 </td>
                 </tr>
                 <tr>
                 <th colspan="2">QUALITY OF WORK</th>
                 <th>Rating</th>
                 
                 </tr>
                 <tr>
                 <td colspan="2">
                 Organizes work activities to reach the target of the job. Uses resources efficiency and effectively to accomplish established goals.
                 </td>
                 <td ><input type="radio" onclick="CheckedValue('Organizesvalue','5')" name="Organizesvalue" value="5" />5</td>
                 </tr>

                 <tr>
                 <td colspan="2">
                 Maintains a high level of output with satisfied conseuent most time.
                 </td>
                 <td ><input type="radio"  onclick="CheckedValue('Organizesvalue','4')" name="Organizesvalue"  value="4" />4</td>
                 </tr>

                 <tr>
                 <td colspan="2">
                 Average products output, medium achievement, which is essential, required.
                 </td>
                 <td ><input type="radio" onclick="CheckedValue('Organizesvalue','3')" name="Organizesvalue" value="3" />3</td>
                 </tr>

                 <tr>
                 <td colspan="2">
                 Output often below required quantity, not achieve result in most of time.
                 </td>
                 <td ><input type="radio"  onclick="CheckedValue('Organizesvalue','2')" name="Organizesvalue" value="2" />2</td>
                 </tr>

                  <tr>
                 <td colspan="2">
                 Totally inadequate to retain on the job.
                 </td>
                 <td ><input type="radio"  onclick="CheckedValue('Organizesvalue','1')" name="Organizesvalue" value="1" />1</td>
                 </tr>
                 <tr>
                 <td colspan="3">
                 <asp:TextBox runat="server" ID="fld_Organizes" Width="98%" TextMode="MultiLine" Rows="3"></asp:TextBox>
                 <asp:TextBox runat="server" ID="fld_Organizesvalue" Width="98%"  style="display:none"></asp:TextBox>
                 </td>
                 </tr>

                  <tr>
                 <th colspan="2">INITIATIVE</th>
                 <th>Rating</th>
                 </tr>
                   <tr>
                 <td colspan="2">
              Demonstrates high capability to think and act effectively.Originates new ideas or methods to improve the job or to complete assigned tasks with high standard
                 </td>
                 <td ><input type="radio"  onclick="CheckedValue('INITIATIVEDemonstratesvalue','5')" name="INITIATIVEDemonstratesvalue" value="5" />5</td>
                
                 </tr>
                   <tr>
                 <td colspan="2">
                 Demonstrates resourceful capability, acts voluntarily in many matters.	
                 </td>
                 <td ><input type="radio" onclick="CheckedValue('INITIATIVEDemonstratesvalue','4')" name="INITIATIVEDemonstratesvalue" value="4" />4</td>
                 </tr>

                    <tr>
                 <td colspan="2">
                  Carries out routine assignments with a little direction.  
                 </td>
                 <td ><input type="radio" onclick="CheckedValue('INITIATIVEDemonstratesvalue','3')" name="INITIATIVEDemonstratesvalue"  value="3" />3</td>
                 </tr>
                 
                 <tr>
                 <td colspan="2">
                 Often relies on others. Must be instructed.	
                 </td>
                 <td ><input type="radio" onclick="CheckedValue('INITIATIVEDemonstratesvalue','2')" name="INITIATIVEDemonstratesvalue" value="2" />2</td>
                 </tr>
                
                 <tr>
                 <td colspan="2">
                 Needs constant supervision and frequent absenteeism.
                 </td>
                 <td ><input type="radio" onclick="CheckedValue('INITIATIVEDemonstratesvalue','1')" name="INITIATIVEDemonstratesvalue" value="1"   />1</td>
                 </tr>
                 <tr>
                  <td colspan="3">
                  <asp:TextBox runat="server" ID="fld_INITIATIVEDemonstrates" Width="98%" TextMode="MultiLine" Rows="3"></asp:TextBox>
                  <asp:TextBox runat="server" ID="fld_INITIATIVEDemonstratesvalue" style="display:none" ></asp:TextBox>
                  </td>
                 </tr>
                 <tr>
                 <th colspan="2">COOPERATION</th>
                 <th>Rating</th>
                 </tr>
                  
                 <tr>
                 <td colspan="2">
                  Establishes and maintains cooperative and productive work relationship with All employees. Enhances the level of mutual cooperation and contributes to the achievement.
                 </td>
                 <td ><input  type="radio" onclick="CheckedValue('Establishesvalue','5')" name="Establishesvalue" value="5" />5</td>
                  </tr>
               
                  <tr>
                 <td colspan="2">
                 Good team worker. Always cooperative with supervisor and colleagues.
                 </td>
                 <td ><input type="radio" onclick="CheckedValue('Establishesvalue','4')" name="Establishesvalue" value="4" />4</td>
                 </tr>
               
                 <tr>
                 <td colspan="2">
                 Usually gets alone with others. Clash occasionally.  
                 </td>
                 <td ><input type="radio" onclick="CheckedValue('Establishesvalue','3')" name="Establishesvalue" value="3" />3</td>
                 </tr>

                  <tr>
                 <td colspan="2">
                 Frequent conflict with other.  
                 </td>
                 <td ><input type="radio" onclick="CheckedValue('Establishesvalue','2')" name="Establishesvalue"  value="2" />2</td>
                 </tr>

                 <tr>
                 <td colspan="2">
                 Total lack of cooperation.	
                 </td>
                 <td ><input type="radio" onclick="CheckedValue('Establishesvalue','1')" name="Establishesvalue"  value="1" />1</td>
                 </tr>
                 <tr>
                 <td colspan="3">
                 <asp:TextBox runat="server" ID="fld_Establishes" Width="98%" TextMode="MultiLine" Rows="3"></asp:TextBox>
                 <asp:TextBox runat="server" ID="fld_Establishesvalue" style="display:none"></asp:TextBox>
                 </td>
                 </tr>
                 <tr>
                 <th colspan="3">SUMMARY REATING (Check one)</th>
                 </tr>
                 <tr>
                 <td colspan="3">One summary rating should account for both performance results and demonstrated behaviors and skills</td>
                 </tr>
                 <tr>
                 <td colspan="3">
                 <input type="radio" onclick="CheckedValue('REATINGValue','5')" name="REATINGValue" />5     Outstanding. Achievement in all important areas of the job consistently exceeds expected level. Attains Superior results and meets unusually demanding objectives.</td>
                 </tr>

                 <tr>
                <td colspan="3">
                  <input type="radio" onclick="CheckedValue('REATINGValue','4')" name="REATINGValue" />4	Work is very competently handled. Major tasks satisfactorily completed.
                </td>
                 </tr>

                 <tr>
                <td colspan="3">
                  <input type="radio" onclick="CheckedValue('REATINGValue','3')" name="REATINGValue" />3	Performance consistently meets the standard required by the job but need assistance and a little improved.
                </td>
                 </tr>

                  <tr>
                <td colspan="3">
                  <input type="radio" onclick="CheckedValue('REATINGValue','2')" name="REATINGValue" />2	Did not demonstrate a good performance, below the standard required by the job. Below expected results.
                </td>
                 </tr>

                <tr>
                <td colspan="4">
                  <input type="radio" onclick="CheckedValue('REATINGValue','1')" name="REATINGValue" />1	Performance significantly below the standard required by the job. Missed expected results need a lot of improved.
                  <asp:TextBox runat="server" ID="fld_REATINGValue" style="display:none"></asp:TextBox>
                </td>
                </tr>

                <tr>
                <th colspan="3">Manager’s comments( Describe rational to support summary rating)</th>
                </tr>
                <tr>
                <td colspan="3">
                <asp:TextBox runat="server" ID="fld_DeptComments"  CssClass="validate[required]" TextMode="MultiLine" Rows="5" Width="98%"></asp:TextBox>
                </td>
                </tr>
                <tr>
                <th colspan="3">Result for Probation</th>
                </tr>
                <tr>
                <td colspan="3" style="height:100px">
                  <asp:TextBox runat="server" ID="fld_ResultProbation"  CssClass="validate[required]" TextMode="MultiLine" Rows="5" Width="98%"></asp:TextBox>
                </td>
                </tr>
                  <tr>
                <th colspan="3">Departmental Manager</th>
                </tr>
                <tr>
                <td colspan="4">
                  <input type="radio" name="DeptReusltValue" onclick="CheckedValue('DeptReusltValue','3')" />Completed &nbsp; &nbsp;&nbsp;
                  <input type="radio" name="DeptReusltValue" onclick="CheckedValue('DeptReusltValue','2')"/>Extended
                  <asp:TextBox runat="server" ID="fld_DeptExtendMonth" Width="50px"></asp:TextBox>
                  <asp:TextBox runat="server" ID="fld_DeptReusltValue" style="display:none"></asp:TextBox>
                  Month  &nbsp; &nbsp;&nbsp;
                  <input type="radio" name="DeptReusltValue" onclick="CheckedValue('DeptReusltValue','1')" />Unsatisfied, Dismissal
                </td>
                </tr>


                <tr class="trem" style="display:none">
                <th colspan="3">Employee Comments</th>
                </tr>
                <tr class="trem">
                <td colspan="3" style="height:100px">
                <%--<asp:TextBox runat="server" ID="fld_EmployeeComments" TextMode="MultiLine" Rows="5" Width="98%"></asp:TextBox>--%>
                <asp:Label runat="server" ID="read_EmployeeComments"></asp:Label>
                </td>
                </tr>


              

               <%-- <tr>
                <th colspan="3">Human Resources Manager</th>
                </tr>
                <tr>
                <td colspan="3">
                <input type="radio" />Completed &nbsp; &nbsp;&nbsp;
                  <input type="radio" />Extended
                  <asp:TextBox runat="server" ID="fld_HRExtendMonth" Width="50px"></asp:TextBox>
                  Month  &nbsp; &nbsp;&nbsp;
                  <input type="radio" />Unsatisfied, Dismissal
                  </td>
                </tr>--%>

                </table>
                </div>
    

      <div class="row" style="display:none">
            <attach:attachments id="Attachments1" runat="server"></attach:attachments>
        </div>
        <div class="row">
            <ah:approvalhistory id="ApprovalHistory1" showaction="true" runat="server"></ah:approvalhistory>
        </div>

         <btn:buttonlist id="ButtonList1" runat="server"></btn:buttonlist>
        </div>
    </form>
</body>
</html>
