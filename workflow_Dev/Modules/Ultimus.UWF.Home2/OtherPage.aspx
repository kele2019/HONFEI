<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="OtherPage.aspx.cs" Inherits="Ultimus.UWF.Home2.OtherPage" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
  <link href="css/workflow.css" rel="stylesheet" type="text/css" />
    <title></title>
    <script type="text/javascript">
        function LoadAssignTask() {
            var sheight = 300; // screen.height - 150;
            var swidth = 600; // screen.width - 10;
            var winoption = "left=500px,top=300px,height=" + sheight + ",width=" + swidth + ",toolbar=no,menubar=no,location=no,status=no,scrollbars=no,resizable=yes";
            window.open("../Ultimus.UWF.Workflow/Assign.aspx", "", winoption);
        }
        function RecallAssignTask() {
            var sheight = 800; // screen.height - 150;
            var swidth = 1200; // screen.width - 10;
            var winoption = "left=100px,top=300px,height=" + sheight + ",width=" + swidth + ",toolbar=no,menubar=no,location=no,status=no,scrollbars=yes,resizable=yes";
            window.open("../Ultimus.UWF.Workflow/AssignmentList.aspx", "", winoption);
        }
        
    </script>
</head>
<body>
    <form id="form1" runat="server">
     <div class="contatn_right textpage_contant" style="width:880px">
     <div class="app_box">
      <div class="box1">
      </div>
      <div class="box2">
      <%--<a href='javascript:void(0)'  onclick="LoadAssignTask()" target="_blank" class='app HR_6'  >Assign  </a>--%>
      <a href='javascript:void(0)'  onclick="RecallAssignTask()"   class='app HR_6'  >Assign&Recall</a>
         <a  target="_blank" href='../Ultimus.UWF.Workflow/HistoryAssignList.aspx'    class='app HR_6'  >AssignReport</a>

      <asp:Repeater runat="server" ID="RpList">
      <ItemTemplate>
        <a href='<%#Eval("URL") %>' target="_blank" class='<%#Eval("ICON") %>'  >
               <%#Eval("MENUNAME")%></a>
      </ItemTemplate>
      </asp:Repeater>

      </div>
      </div>
    </div>
    </form>
</body>
</html>
