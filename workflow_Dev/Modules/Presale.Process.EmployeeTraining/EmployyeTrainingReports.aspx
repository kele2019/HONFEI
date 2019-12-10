<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="EmployyeTrainingReports.aspx.cs" Inherits="Presale.Process.EmployeeTraining.EmployyeTrainingReports" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
     <title>培训签到表Training Attendee Sheet</title>
    <script type="text/javascript" src="/Assets/js/common.js"></script>
    <script>
        $(document).ready(function () {
            var CountMonthCount = 0;
            var CountyearCount = 0;
            $("#tobdydetail").find('tr[class="TrainTR"]').each(function () {
                var MonthCount = $(this).find("td").eq(4).html() - 0;
                var YearCount = $(this).find("td").eq(5).html() - 0;
                CountMonthCount = (CountMonthCount - 0) + (MonthCount - 0);
                CountyearCount = (CountyearCount + YearCount) - 0;
               // console.log(CountMonthCount);
            });
            $("#spanMcount").text(CountMonthCount);
            $("#span1YCount").text(CountyearCount);

        });


        function ToggleBudegetDetail(obj) {
           
            if ($(obj).attr("value") == "" || $(obj).attr("value") == undefined) {
                $(obj).attr("value", "1");
                $(obj).attr("src", "/Assets/images/icon_t_o1.gif");
                $(obj).parent().parent().next().show("slow");
            }
            else {
                $(obj).attr("value", "");
                $(obj).attr("src", "/Assets/images/icon_t_o3.gif");
                $(obj).parent().parent().next().hide("slow");
            }

        }
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
            <h3> 培训统计表Training Attendee Sheet  </h3>
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
       <td class="td-label"> User </td>
       <td>
       <asp:TextBox runat="server" ID="txtUserInfo"></asp:TextBox>
       </td>
       </tr>
       <tr>
       <td  class="td-label">
      Date
       </td>
        <td  class="td-content" >
     <asp:DropDownList runat="server" Width="35%" ID="dropYear">
     </asp:DropDownList>
     Year
     <asp:DropDownList runat="server" Width="35%"  ID="dropMonth">
     </asp:DropDownList>
     Month
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
        <td class="banner" colspan="6">Required Attendee </td>
        </tr>
                <tr>
                <th>序号<br />No.</th>
                <th>员工编号<br />UID</th>
                <th>员工姓名<br />Name</th>
                <th>职务<br />Title</th>
                <th>月度培训时间合计<br /> Monthly training time</th>
                <th>年度培训时间合计<br />Annual training time</th>
                </tr>
                <tbody id="tobdydetail">
               <asp:Repeater runat="server" ID="rpList">
               <ItemTemplate>
               <tr class="TrainTR">
               <td> 
                 <img src="/Assets/images/icon_t_o3.gif" onclick="ToggleBudegetDetail(this)" />
               <asp:Label ID="fld_ROWID" Text='<%# Container.ItemIndex+1%>' runat="server"></asp:Label>
               
               </td>
               <td><%#Eval("USERCODE")%></td>
               <td><%#Eval("USERNAME") %></td>
               <td><%#Eval("EXT03") %></td>
               <td>
                <%#Eval("MonthCount")%>
               </td>
               <td> <%#Eval("YearCount")%></td>
               </tr>
              
              <tr style="display:none">
              <td colspan="6">
              <asp:HiddenField runat="server" ID="hdUserID" Value='<%#Eval("USERID") %>' />
              <table class="table table-condensed table-bordered">
                 <tr>
                 <th>序号<br />No.</th>
                  <th>培训单号<br />Training DocumentNo</th>
                 <th>培训内容<br />Training Title</th>
                 <th>培训时间<br />traing time</th>
                 </tr>
                 <asp:Repeater runat="server" ID="RPTrainingDetail">
                 <ItemTemplate>
                 <tr>
                 <td><asp:Label ID="DetailROWID" Text='<%# Container.ItemIndex+1%>' runat="server"></asp:Label></td>
                 <td><%#Eval("TrainDocmentNo")%></td>
                 <td><%#Eval("TrainingPurpose") %></td>
                  <td><%#Eval("SumDate")%></td>
                 </tr>
                 </ItemTemplate>
                 <FooterTemplate>
                 <tr>
                 <td colspan="5"></td>
                 </tr>
                 </FooterTemplate>
                 </asp:Repeater>
                 </table>



              </td>
              </tr>

               </ItemTemplate>
               </asp:Repeater>
                </tbody>
               <tr>
               <td colspan="4" style="text-align:right">Total</td>
               <td><span id="spanMcount"></span></td>
               <td><span id="span1YCount"></span></td>
               </tr>
                </table>
                </div>
</div>
    </form>
</body>
</html>
