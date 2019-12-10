<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="EvaluationDetailPage.aspx.cs" Inherits="Presale.Process.EmployeeTraining.EvaluationDetailPage" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
     <title>培训评价详情 Evaluation Detail</title>
    <script type="text/javascript" src="/Assets/js/common.js"></script>
</head>
<body>
    <form id="form1" runat="server">
    <div class="container">
    
       <div class="row">
     <table align="center" style="width:100%;margin-top:0.5%;margin-bottom:0.5%;"  >
       <tr>
        <td >
            <asp:Image ID="Image1" runat="server" ImageUrl="../../assets/img/logo.gif" Height="50px"
                Visible="true" />
        </td>
        <td style="text-align: center; padding-top: 10px; " width="450">
            <h3> 培训评价详情 Evaluation Detail</h3>
        </td>
       
        <td align="right" style="padding-left:20;" width="250">
        </td>
    </tr>
</table>
 </div>
       <div class="row">
       <table class="table table-condensed table-bordered">
       <tr>
       <td  class="td-label">培训专题Topic </td>
       <td class="td-content" colspan="3">
       <asp:Label runat="server" ID="lbTopic"></asp:Label>
       </td>
       </tr>
       <tr>
       <td class="td-label"> 培训师Trainer </td>
       <td>
       <asp:Label runat="server" ID="lbTrainer"></asp:Label>
       </td>
       <td  class="td-label">
       培训学时 Training Duration
       </td>
        <td  class="td-content" >
       <asp:Label runat="server" ID="lbTrainingDur"></asp:Label>
       </td>
       </tr>
       <tr>
       <td class="td-label"> 日期Date（YY-MM-DD) </td>
       <td>
       <asp:Label runat="server" ID="lbTrainingDate"></asp:Label>
       </td>
       <td  class="td-label">
       地点Location
       </td>
        <td class="td-content" >
       <asp:Label runat="server" ID="lbLocation"></asp:Label>
       </td>
       </tr>
        </table>
       </div>
       <div class="row">
      <table  class="table table-condensed table-bordered">
     <thead>
     <tr>
      <th rowspan="2" style="vertical-align:middle">User</th>
     <th colspan="2">Course Content</th>
     <th colspan="2">Course Arrangement</th>
     <th colspan="3">Trainer</th>
     <th>Overall Comments</th>
     </tr>
     <tr>
     <th>Working Requirement</th>
     <th>Applicable value</th>
     <th>Time arrangement</th>
     <th>Material Quality</th>
     <th>Power of Expression</th>
     <th>Motivation</th>
     <th>Response to question</th>
     <th>Appraisal of the Course</th>
     </tr>
     </thead>
     <tbody>
     <asp:Repeater runat="server" ID="RPList">
     <ItemTemplate>
     <tr>
     <td rowspan="2" style="vertical-align:middle"><%#Eval("USERNAME") %></td>
     <td><%#Eval("WorkingRequirement")%></td>
     <td><%#Eval("ApplicableValue")%></td>
     <td><%#Eval("TimeArrangement")%></td>
     <td><%#Eval("MaterialQuality")%></td>
     <td><%#Eval("PowerOE")%></td>
     <td><%#Eval("Motivation")%></td>
     <td><%#Eval("ResponseTQ")%></td>
     <td><%#Eval("AOTCourse")%></td>
     </tr>
     <tr>
     <th colspan="2">What needs to improve in your opinion</th>
     <td colspan="6">
      <%#Eval("Opinion")%>
     </td>
     </tr>
     </ItemTemplate>
     </asp:Repeater>
     </tbody>
      </table>
      </div>




     <div class="row" style="text-align:center">
      <input type="button" class="btn" value="返回 Back" onclick="javascript:history.go(-1)" />
      </div>

    </div>
    </form>
</body>
</html>
