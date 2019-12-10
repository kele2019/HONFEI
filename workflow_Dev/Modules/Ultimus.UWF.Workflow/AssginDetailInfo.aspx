<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AssginDetailInfo.aspx.cs" Inherits="Ultimus.UWF.Workflow.AssginDetailInfo" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<script type="text/javascript" src="/Assets/js/common.js"></script>
 <script src="/Assets/Layer/layer.js" type="text/javascript"></script>
    <link href="/Assets/Layer/skin/default/layer.css" rel="stylesheet" type="text/css" />
    <title>Assgin Detail Info</title>

    <script type="text/javascript">
        function ClosePage() {
//            alert(layer.index);
            //            layer.close(layer.index);
            //layer.closeAll();
            var index = parent.layer.getFrameIndex(window.name);
            parent.layer.close(index);  
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    <div id="myDiv" class="container">
        <div class="row" style="font-size:14px" >
        <h2 style="text-align:center">
        Authorization Letter<br /> 委托书
        </h2>
       <br /> 
       I   
           <asp:Label runat="server" ID="lbFromName" Font-Bold="true"></asp:Label>
        <%--<asp:Image runat="server" ID="lbFromName"  Width="80px"  />--%>
          hereby authorize
        <%-- <asp:Image runat="server" ID="lbToUser" Width="80px" />--%>
         <asp:Label runat="server" ID="lbToUser"  Font-Bold="true"></asp:Label>
       to handle with <asp:Label runat="server" ID="lbComments"></asp:Label> during my absence from <asp:Label runat="server" ID="lbStartDare"></asp:Label> to <asp:Label runat="server" ID="lbEndDare"></asp:Label> .  
        <br /><br /><br /><br />
        本人 
          <asp:Label runat="server" ID="lbFromNameC"  Font-Bold="true"></asp:Label> 
          <%--<asp:Image runat="server" ID="lbFromNameC"  Width="80px" />--%>     于    <asp:Label runat="server" ID="lbStartDareC"></asp:Label> 
        到 <asp:Label runat="server" ID="lbEndDareC"></asp:Label>           外出期间，授权  
        <%-- <asp:Image runat="server" ID="lbToUserC"  Width="80px"  />  --%>
           <asp:Label runat="server" ID="lbToUserC"  Font-Bold="true"></asp:Label>
         办理   <asp:Label runat="server" ID="lbCommentsC"></asp:Label>相关事宜。
        <br />
       

         <br /><br /> 
        
        I acknowledge all the relevant documents signed by the authorized person within the scope of authority, and will bear all the responsivities that may cause.
        <br />被委托人在权限范围内签署的一切有关文件，我均承认。由此所造成的一切责任均由本人承担。

       
      

        <br /><br /><br />
            <div class="row">
        Authorizer委托人:   <asp:Image runat="server" ID="lbTaskUser"  Width="80px" />  
        <br />
        Date 日期:<asp:Label runat="server" ID="lbCreateDate"></asp:Label>
      </div>
     <div style="text-align:center"> <input type="button" value="Close" class="btn" onclick="ClosePage()" /></div>
        </div>
         </div>
    </div>
    </form>
</body>
</html>
