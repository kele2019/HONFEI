<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ProcessManagerPage.aspx.cs" Inherits="Ultimus.UWF.FunctionManager.ProcessManagerPage" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Process  Manager</title>
    <script type="text/javascript" src="/Assets/js/common.js"></script>

    <style type="text/css">
#menu {
font-size: 12px;
font-weight: bolder;
 
}
#menu li{
list-style-image: none;
list-style-type: none;
width:180px;
padding-right:5px;
height:60px;
float: left;
}

    </style>

</head>
<body>
    <form id="form1" runat="server">
     <div id="myDiv" class="container">
     <div style="text-align:center">  <h2>Process SendEmail Manager</h2> </div>
    
     <hr />
      <ul id="menu">
       <asp:Repeater runat="server" ID="RPList"  >
    <ItemTemplate>
   <li>
   <asp:CheckBox runat="server" ID="CbProcessName" Checked='<%#Eval("EXT01").ToString()=="1"? true:false %>'  />
   <asp:HiddenField runat="server" ID="HidID" Value='<%#Eval("ID") %>' />
   <span><%#Eval("PROCESSNAME")%></span>
   </li>
     
    </ItemTemplate>
    </asp:Repeater>
     </ul>
     
     </div>
     <div style="text-align:center;">
      <asp:Button runat="server" ID="btnSave" Text="Save" CssClass="btn" onclick="btnSave_Click" />
      </div>
    </form>
</body>
</html>
