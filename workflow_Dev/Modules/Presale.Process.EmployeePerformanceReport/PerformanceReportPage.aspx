<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PerformanceReportPage.aspx.cs" Inherits="Presale.Process.EmployeePerformanceReport.PerformanceReportPage" %>
<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
 <head id="Head1" runat="server">
    <title>Employee Performance Report</title>
     <script type="text/javascript" src="/Assets/js/common.js"></script>
</head>
<body>
    <form id="form1" runat="server">
      <div id="myDiv" class="container" style="width:1200px">
    
     <div class="row">
       <table align="center" style="width:100%;margin-top:0.5%;margin-bottom:0.5%;"  >
       <tr>
        <td width="30%" >
            <asp:Image ID="Image1" runat="server" ImageUrl="../../assets/img/logo.gif" Height="50px"
                Visible="true" />
        </td>
        <td style="text-align: center; padding-top: 10px; " width="450">
            <h3> Employee Performance Report  </h3>
        </td>
        <td align="right" style="padding-left:20;" width="250">
        </td>
    </tr>
    </table>
    </div>

    
      <table class="table table-condensed table-bordered">
       <tr>
       <td  class="td-label">Report Type:</td>
       <td class="td-content">
         <asp:DropDownList runat="server" ID="DropReportType">
         <asp:ListItem Value="">--Please Select--</asp:ListItem>
         <asp:ListItem Value="Begin-Year goal plan">Begin-Year goal plan</asp:ListItem>
         <asp:ListItem Value="Mid-Year Update">Mid-Year Update</asp:ListItem>
         <asp:ListItem Value="End-Year Performance">End-Year Performance</asp:ListItem>
         </asp:DropDownList>
         </td>
        <td  class="td-label">Applicant:</td>
       <td class="td-content">
         <asp:DropDownList runat="server" ID="DropEmployeeUser">
         <asp:ListItem>--Please Select--</asp:ListItem>
         </asp:DropDownList>
       </td>
       </tr>
       <tr>
        <td  class="td-label">Report Year:</td>
       <td class="td-content"  >
         <asp:DropDownList runat="server" ID="DropReportYear">
         <asp:ListItem>--Please Select--</asp:ListItem>
         </asp:DropDownList>
        
       </td>
       <td  class="td-label">Department Name:</td>
       <td class="td-content"  >
         <asp:TextBox runat="server" ID="txtDept"></asp:TextBox>
        <asp:Button runat="server" ID="btnSearch" Text="Search" CssClass="btn" style="margin-left:50px" OnClick="btnSearch_Click" />
       </td>

       </tr>
       </table>
     <div class="row">
        <table class="table table-condensed table-bordered">
       <%--  <tr>
        <td class="banner" colspan="15">Payment Report</td>
        </tr>--%>
        <thead>
        <tr>
        <th>No</th>
        <th>Document No.</th>
        <th>RequestDate</th>
        <th>Applicant</th>
        <th>Department</th>
        <th>ReportYear</th>
        <th>ReportType</th>
        <th>Department Manager</th>
        <th>Department Manager ApproveDate</th>
        <th>Second Department Manager</th>
        <th>Second Department Manager ApproveDate</th>
        <th>Applicant FeedBack Date</th>
        <th>HRApproveDate</th>
        <th>Status</th>
        </tr>
        </thead>
        
        <tbody>
        <asp:Repeater runat="server" ID="RPlist">
        <ItemTemplate>
       <tr>
       <td><%#Container.ItemIndex + 1 %></td>
      <%-- <td><a href='Approval.aspx?Incident=<%#Eval("INCIDENT") %>&Type=myapproval&ProcessName=Employee Performance Report' target="_blank"><%#Eval("DOCUMENTNO")%></a></td>--%>
       <td><a href='<%#OpenPageURL(Eval("ReportType"))%>?Incident=<%#Eval("INCIDENT") %>&Type=myapproval&ProcessName=Employee Performance Report'   target="_blank"><%#Eval("DOCUMENTNO")%></a>
       </td>
       <td><%#Convert.ToDateTime(Eval("RequestDate")).ToShortDateString() %></td>
       <td><%#Eval("APPLICANT")%></td>
       <td><%#Eval("DEPARTMENT")%></td>
       <td><%#Eval("ReportYear")%></td>
       <td><%#Eval("ReportType")%></td>
       <td><%#Eval("DeptManager")%></td>
       <td><%#Convert.ToDateTime(Eval("DeptManagerDate").ToString()).Year<=2000 ? "" : Convert.ToDateTime(Eval("DeptManagerDate")).ToShortDateString()%></td>
       <td><%#Eval("SDeptManager")%></td>
       <td><%#Convert.ToDateTime(Eval("SDeptManagerDate").ToString()).Year<=2000? "" : Convert.ToDateTime(Eval("SDeptManagerDate")).ToShortDateString()%></td>
       <td><%#Convert.ToDateTime(Eval("ApplicantFeedDate").ToString()).Year<=2000 ? "" : Convert.ToDateTime(Eval("ApplicantFeedDate")).ToShortDateString()%></td>
       <td><%#Convert.ToDateTime(Eval("HRApproveDate").ToString()).Year<=2000? "" : Convert.ToDateTime(Eval("HRApproveDate")).ToShortDateString()%></td>
       <td><%#Eval("ProcessStatus").ToString() == "1" ? "N" : "Y"%></td>
       </tr>
        </ItemTemplate>
        </asp:Repeater>
        </tbody>
         <tfoot>
                    <tr>
                    <td colspan="15">
                      <webdiyer:aspnetpager id="AspNetPager1" runat="server" custominfohtml="Count %RecordCount%"
                        horizontalalign="right" width="100%" cssclass="aspNetPager" onpagechanged="AspNetPager1_PageChanged"
                        pagesize="30" alwaysshow="true" submitbuttonstyle="display:none" inputboxstyle="display:none"
                        nextpagetext="Next" firstpagetext="Home" lastpagetext="Last" prevpagetext="Prev">
                    </webdiyer:aspnetpager>
                    </td>
                    </tr>
         </tfoot>


        </table>
        </div>



    </div>
    </form>
</body>
</html>
