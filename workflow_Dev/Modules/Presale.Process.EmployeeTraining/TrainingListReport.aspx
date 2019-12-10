<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="TrainingListReport.aspx.cs" Inherits="Presale.Process.EmployeeTraining.TrainingListReport" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
     <title>培训表Training List Report</title>
    <script type="text/javascript" src="/Assets/js/common.js"></script>
     <script type="text/javascript">
        $(document).ready(function () {
            $(".container").attr("style", "width:1300px");
            });
            </script>
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
            <h3> 培训表Training List Report  </h3>
        </td>
       
        <td align="right" style="padding-left:20;" width="250">
        </td>
    </tr>
</table>
<table class="table table-condensed table-bordered">
       <tr>
       <td  class="td-label">Department </td>
       <td class="td-content" >
        <asp:DropDownList runat="server" ID="dropDept">
        </asp:DropDownList>
       </td>
       <td class="td-label"> Organizer </td>
       <td>
       <asp:TextBox runat="server" ID="txtUserInfo"></asp:TextBox>
       </td>
       </tr>
       <tr>
       <td  class="td-label">
      Date
       </td>
        <td  class="td-content" >
     Year <asp:DropDownList runat="server" Width="35%" ID="dropYear">
     </asp:DropDownList>
     Month
     <asp:DropDownList runat="server" Width="35%"  ID="dropMonth">
     </asp:DropDownList>
    
       </td>
      
        
        <td class="td-content" colspan="2" >
       <asp:Button runat="server" ID="btnSearch"  style="margin-left:40px" CssClass="btn" 
                Text="Search" onclick="btnSearch_Click"  />
       </td>
       </tr>
        </table>


      <div class="row">
        <%--  <p style="font-weight:bold;">Required Attendee</p>--%>
                <table class="table table-condensed table-bordered">
                 <tr>
        <td class="banner" colspan="9">Required Attendee </td>
        </tr>
                <tr>
                <th>Item</th>
                <th>DocumentNo</th>
                <th>Organizer</th>
                <th>Topic</th>
                <th>Trainer</th>
                <th>Training Type</th>
                <th>Training Date</th>
                <th>Training Time</th>
                <th>Location</th>
                </tr>
               <asp:Repeater runat="server" ID="rpList">
               <ItemTemplate>
               <tr>
               <td rowspan="3"> <asp:Label ID="fld_ROWID" Text='<%# Container.ItemIndex+1%>' runat="server"></asp:Label></td>
                <td><%#Eval("DOCUMENTNO")%></td>
               <td><%#Eval("APPLICANT")%></td>
               <td><%#Eval("TrainingPurpose")%></td>
               <td><%#Eval("TrainingTeacher")%></td>
               <td><%#Eval("TrainingType")%> </td>
               <td>From <%# Convert.ToDateTime(Eval("StartDate")).ToShortDateString()%> To <%#Convert.ToDateTime(Eval("EndDate")).ToShortDateString()%></td>
               <td><%#Eval("TrainingDuration") %></td>
               <td><%#Eval("TrainingLocation") %></td>
               </tr>
               <tr>
               <th>Plan Training users</th>
             <td colspan="7">
             <%#Eval("TrainingUser") %>
             </td>
               </tr>

                 <tr>
               <th>Actual Training users</th>
             <td colspan="7"><%#Eval("ActualUsers") %></td>
               </tr>
               </ItemTemplate>
               </asp:Repeater>
                </table>
                </div>
</div>
</div>
    </form>
</body>
</html>
