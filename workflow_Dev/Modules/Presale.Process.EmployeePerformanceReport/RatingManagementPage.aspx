<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="RatingManagementPage.aspx.cs" Inherits="Presale.Process.EmployeePerformanceReport.RatingManagementPage" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>RatingManagerment</title>
      <script type="text/javascript" src="/Assets/js/common.js"></script>
       <script src="/Assets/Layer/layer.js" type="text/javascript"></script>
    <link href="/Assets/Layer/skin/default/layer.css" rel="stylesheet" type="text/css" />

     <script type="text/javascript">
         $(document).ready(function () {

             $(".savebtn").bind("click", function () {
                 if (confirm("Confirm Save Data ?")) {
                     $("#btnSave").click();
                                  }
//                 layer.confirm('Confirm Save Data!', {
//                     btn: ['Confirm', 'Cancel']
//                 }, function (index, layero) {
//                     $("#btnSave").click();
//                 }
             //  );

             });
         });
         function CompleteData(obj) {
           
                 if (obj == "1") {
                  layer.msg('Save Sucess');
                    //   alert("Save Sucess");
                    // ClosePage();
                 }
                 else {
                     layer.msg(obj);
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
            <h3> 绩效等级管理表 Rating Management</h3>
        </td>
        <td align="right" style="padding-left:20;" width="250">
        </td>
    </tr>
</table>

 <table class="table table-condensed table-bordered">
       <tr>
       <td  class="td-label">
      Year
       </td>
        <td  class="td-content"  style="width:85%">
      <asp:DropDownList runat="server" Width="35%" ID="dropYear">
     </asp:DropDownList>
       <asp:Button runat="server" ID="btnSearch"  style="margin-left:40px;" CssClass="btn" 
                Text="Search" onclick="btnSearch_Click"  />
                <input type="button" value="Save"  id="savebtn2" class="savebtn btn btn-primary" style="float:right;" />
       </td>
       </tr>
        </table>


         <div class="row">
         <table class="table table-condensed table-bordered">
          <tr>
         <td class="banner"  colspan="3">Rating List</td>
         </tr>
         <tr>
         <th>No</th>
         <th>UserName</th>
         <th>Rating Value</th>
         </tr>

         <asp:Repeater runat="server" ID="RPList">
         <ItemTemplate>
         <tr>
          <td><%# Container.ItemIndex+1%>
          <asp:HiddenField runat="server" ID="hdUserLoginName" Value='<%#Eval("LOGINNAME") %>' />
          </td>
         <td><%#Eval("UserFullName")%></td>
         <td><asp:TextBox runat="server" ID="txtRating" Text='<%#Eval("RatingValue")%>' ></asp:TextBox></td>

         </tr>
         </ItemTemplate>
         </asp:Repeater>

        
        <tr>
        <td colspan="3" style="text-align:center">
         <input type="button" value="Save"  id="savebtn1" class="savebtn btn btn-primary"   />
         <asp:Button runat="server"  ID="btnSave" Text="Save" CssClass="btn btn-primary"  style="display:none" onclick="btnSave_Click"/>
        </td>
        </tr>
          </table>
       </div>

     </div>
    
    </div>
    </form>
</body>
</html>
