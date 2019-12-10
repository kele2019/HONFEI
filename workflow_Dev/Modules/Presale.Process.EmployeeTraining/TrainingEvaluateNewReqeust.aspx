<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="TrainingEvaluateNewReqeust.aspx.cs" Inherits="Presale.Process.EmployeeTraining.TrainingEvaluateNewReqeust" %>
<%@ Register Src="/Modules/Ultimus.UWF.Form.ProcessControl/UserInfo.ascx" TagName="UserInfo" TagPrefix="ui" %>
<%@ Register Src="/Modules/Ultimus.UWF.Form.ProcessControl/ApprovalHistory.ascx" TagName="ApprovalHistory" TagPrefix="ah" %>
<%@ Register Src="/Modules/Ultimus.UWF.Form.ProcessControl/Attachments.ascx" TagName="Attachments" TagPrefix="attach" %>
<%@ Register Src="/Modules/Ultimus.UWF.Form.ProcessControl/ButtonList.ascx" TagName="ButtonList" TagPrefix="btn" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
      <title>Training Evaluation Form</title>
     <meta http-equiv="X-UA-Compatible"  content="IE=EmulateIE8" />
    <script type="text/javascript" src="/Assets/js/common.js"></script>
    <script type="text/javascript">


    </script>
</head>
<body>
    <form id="form1" runat="server">
     <div id="myDiv" class="container">
     <div class="row">
                <ui:userinfo id="UserInfo1" processtitle="Training Evaluation Form" processprefix="ETE" tablename="PROC_TrainingResultEvaluation"
                    runat="server"  ></ui:userinfo>
     </div> 
            
     <div class="row">
    
       <table class="table table-condensed table-bordered">
       <tr>
       <td class="td-label" style="vertical-align:middle; width:17%;">Course Name</td>
       <td style="vertical-align:middle; width:33%;">
       
       <asp:Label runat="server" ID="read_CourseName"></asp:Label></td>
       <td class="td-label" style="vertical-align:middle; width:17%;">Trainer</td>
       <td style="vertical-align:middle; width:33%;">
       <asp:Label runat="server" ID="read_TrainerName"></asp:Label></td>
       </tr>
       <tr>
       <td class="td-label" style="vertical-align:middle">Start and End Date</td>
       <td  >
       <asp:Label runat="server" ID="read_TrainingStart"></asp:Label>To
       <asp:Label runat="server" ID="read_TrainingEnd"></asp:Label>
       </td>
       <td class="td-label" style="vertical-align:middle">Training Hours</td>
       <td  >
       <asp:Label runat="server" ID="read_TrainingHours"></asp:Label>
       </td>
       </tr>
       <tr>
       <th colspan="4">Section 1 Knowledge and Skill Acquisition</th>
       </tr>
       <tr>
       <th colspan="4">
       How will the course be helpful in your daily job? In what way was it most helpful? 
      
       </th>
       </tr>
       <tr>
       <td colspan="4">
       <asp:TextBox runat="server" ID="fld_HelpComments"  TextMode="MultiLine" Rows="5" Width="98%"></asp:TextBox>
       </td>
       </tr>
       <tr>
       <th colspan="4">Section 2 Action Plan and Improvement Review</th>
       </tr>
       <tr>
       <th colspan="4">
       Which action will you take to improve you working in next 1 months after the training? Please discuss with your direct supervisor or manager and get his or her comments after the plan is done. If there is not enough space, please attach A4 papers.
       <br />Please complete this section within 1 months after the training.
       </th>
       </tr>
       <tr>
       <td class="td-label"  >   How to practice   </td>
       <td colspan="3">
          <asp:TextBox runat="server" ID="fld_PracticeComments"  TextMode="MultiLine" Rows="5" Width="98%"></asp:TextBox>
       </td>
       </tr>
      <tr>
      <td class="td-label" >Improvement Evaluation </td>
      <td colspan="3">
       <asp:TextBox runat="server" ID="fld_EmployeeComments"  TextMode="MultiLine" Rows="5" Width="98%"></asp:TextBox>
      </td>
      </tr>

       <tr>
      <td class="td-label" >Result Scale</td>
      <td colspan="3">
      Scale Definition (等级说明):  1.Poor(差), 2.Fair(一般), 3.Meet Expectations(达到期望),  4.Exceed Expectations(超过期望),5.Outstanding(优秀) 
     <br /> <input disabled="disabled" type="radio" />1&nbsp;<input disabled="disabled" type="radio" />2&nbsp;<input disabled="disabled" type="radio" />3&nbsp;<input disabled="disabled" type="radio" />4&nbsp;<input disabled="disabled" type="radio" />5
      <asp:Label runat="server" ID="read_ResultScale" style="display:none"></asp:Label>
      </td>
      </tr>
      <tr>
      <td class="td-label" >Comments </td>
       <td colspan="3">
       <asp:Label runat="server"  ID="read_LeaderComments"></asp:Label>
       </td>
      </tr>
       </table>
    </div>

      <div class="row">
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
