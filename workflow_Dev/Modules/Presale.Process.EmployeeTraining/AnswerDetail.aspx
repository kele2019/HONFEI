<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AnswerDetail.aspx.cs" Inherits="Presale.Process.EmployeeTraining.AnswerDetail" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>答题信息 Answer Info</title>
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
            <h3> 培训答题信息 Training Answer Info</h3>
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
      <asp:Label runat="server" ID="lbMsg" style="color:Red; margin:40%;"></asp:Label>
      <table  class="table table-condensed table-bordered">
      <tbody>
      <asp:Repeater runat="server" ID="rpUserInfo">
      <ItemTemplate>
      <tr>
     <td style="vertical-align:middle"><%#Eval("EXT04") %></td>
     <td>
     <asp:HiddenField runat="server" ID="hdUserID" Value='<%#Eval("USERID") %>' />
     <asp:Repeater runat="server" ID="rpQuestiongDetail">
     <ItemTemplate>
     <table  class="table-bordered" style="width:100%">
     <tr>
     <td width="80px">
   <%# Container.ItemIndex+1%>、 <%#Eval("Question") %>
     </td>
     <td width="30%" >正确答案：<%#Eval("answer")%></td>
     <td width="30%">用户答案:<%#Eval("UAnswer")%></td>
     </tr>
     </table>
     </ItemTemplate>
     </asp:Repeater>
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
