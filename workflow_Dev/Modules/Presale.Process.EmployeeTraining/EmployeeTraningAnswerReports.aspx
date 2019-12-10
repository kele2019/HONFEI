<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="EmployeeTraningAnswerReports.aspx.cs" Inherits="Presale.Process.EmployeeTraining.EmployeeTraningAnswerReports" %>
<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
      <title>培训报表Training Reports</title>
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
            <h3> 培训报表Training Reports</h3>
        </td>
       
        <td align="right" style="padding-left:20;" width="250">
        </td>
    </tr>
</table>
<table class="table table-condensed table-bordered">
    <tr>
       <td  class="td-label">
      Date
       </td>
        <td  class="td-content"  colspan="2"  >
    <asp:TextBox runat="server" Width="40%" onclick="WdatePicker({readOnly:true,dateFmt: 'yyyy/MM/dd',maxDate:'#F{$dp.$D(\'txtEndDate\')}'})"  ID="txtStartDate"></asp:TextBox>
    To
    <asp:TextBox runat="server" Width="40%" onclick="WdatePicker({readOnly:true, dateFmt: 'yyyy/MM/dd',minDate:'#F{$dp.$D(\'txtStartDate\')}'})"  ID="txtEndDate"></asp:TextBox>
       </td>
        
        <td class="td-content">
       <asp:Button runat="server" ID="btnSearch"  style="margin-left:40px" CssClass="btn" 
                Text="Search" onclick="btnSearch_Click" />
       </td>
       </tr>
        </table>


      <div class="row">
        <%--  <p style="font-weight:bold;">Required Attendee</p>--%>
                <table class="table table-condensed table-bordered">
                 <tr>
        <td class="banner" colspan="6">Required Attendee </td>
        </tr>
                <tr>
                <th>序号<br />No.</th>
                <th>申请单号<br />RequestNo</th>
                <th>主题<br />Topic</th>
                <th>培训人员<br />Trainer</th>
                <th>培训类型<br />Training Type</th>
                <th>培训时间<br />Training Time</th>
                </tr>
               <asp:Repeater runat="server" ID="rpList">
               <ItemTemplate>
               <tr>
               <td > <asp:Label ID="fld_ROWID" Text='<%# Container.ItemIndex+1%>' runat="server"></asp:Label></td>
               <td>
               <a href='AnswerDetail.aspx?FormID=<%#Eval("FormID") %>' > <%#Eval("DOCUMENTNO")%> </a>
               </td>
               <td><%#Eval("TrainingPurpose")%></td>
               <td><%#Eval("TrainingTeacher")%></td>
               <td><%#Eval("TrainingType")%> </td>
               <td> <%#Convert.ToDateTime(Eval("StartDate").ToString()).ToShortDateString()%> To <%#Convert.ToDateTime(Eval("EndDate").ToString()).ToShortDateString()%> </td>
               </tr>
               </ItemTemplate>
               </asp:Repeater>

                 <tfoot>
                    <tr>
                    <td colspan="6">
                      <webdiyer:aspnetpager id="AspNetPager1" runat="server" custominfohtml="Count %RecordCount%"
                        horizontalalign="right" width="100%" cssclass="aspNetPager" onpagechanged="AspNetPager1_PageChanged"
                        pagesize="10" alwaysshow="true" submitbuttonstyle="display:none" inputboxstyle="display:none"
                        nextpagetext="Next" firstpagetext="Home" lastpagetext="Last" prevpagetext="Prev">
                    </webdiyer:aspnetpager>
                    </td>
                    </tr>
                    </tfoot>
                </table>
                </div>
</div>
</div>
    </form>
</body>
</html>
