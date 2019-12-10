<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="TrainingReports.aspx.cs" Inherits="Presale.Process.EmployeeTraining.TrainingReports" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>培训签到表Training Attendee Sheet</title>
    <script type="text/javascript" src="/Assets/js/common.js"></script>

</head>
<body>
    <form id="form1" runat="server">
    <div id="myDiv" class="container">
     <div class="row">

     <table align="center" style="width:100%;margin-top:0.5%;margin-bottom:0.5%;"  >
       <tr>
        <td >
            <asp:Image ID="Image1" runat="server" ImageUrl="../../assets/img/logo.gif" Height="50px"
                Visible="true" />
        </td>
        <td style="text-align: center; padding-top: 10px; " width="450">
            <h3> 培训签到表Training Attendee Sheet  </h3>
        </td>
       
        <td align="right" style="padding-left:20;" width="250">
        </td>
    </tr>
</table>


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
        <%--  <p style="font-weight:bold;">Required Attendee</p>--%>
                <table class="table table-condensed table-bordered">
                 <tr>
        <td class="banner" colspan="7">Required Attendee </td>
        </tr>
                <tr>
                <th>序号<br />No.</th>
                <th>员工编号<br />UID</th>
                <th>员工姓名<br />Name</th>
                <th>职务<br />Title</th>
                <th>签名<br />Signature</th>
                <th>签到时间<br />Signature Date</th>
                <th>备注<br />Remark</th>
                </tr>
               
               <asp:Repeater runat="server" ID="rpList">
               <ItemTemplate>
               <tr>
               <td> <asp:Label ID="fld_ROWID" Text='<%# Container.ItemIndex+1%>' runat="server"></asp:Label></td>
               <td><%#Eval("USERCODE")%></td>
               <td><%#Eval("USERNAME") %></td>
               <td><%#Eval("EXT03") %></td>
               <td>
                <img style="height:40px;display:<%#Eval("TrainDate").ToString()==""?"none":"block"%>" src='../../Modules/Ultimus.UWF.Form.ProcessControl/img/<%#Eval("EXT04")%>.png' />
               </td>
               <td><%#Eval("TrainDate") %></td>
               <td></td>
               </tr>
               </ItemTemplate>
               </asp:Repeater>
                </table>
                </div>
                 <div class="row" style="text-align:center">
      <input type="button" class="btn" value="返回 Back" onclick="javascript:history.go(-1)" />
      </div>
    </div>
    </form>
</body>
</html>
