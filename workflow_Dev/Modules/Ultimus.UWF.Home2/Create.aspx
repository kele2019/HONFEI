<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Create.aspx.cs" Inherits="Ultimus.UWF.Home2.Create" %>
<%@ Import Namespace="Ultimus.UWF.Home2.Code" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta http-equiv="X-UA-Compatible"  content="IE=EmulateIE8" />
    <title>Create Page</title>
    <script type="text/javascript" src="/Assets/js/common.js"></script>
    <link href="css/workflow.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript">
        function openForm(taskId, type, ele) {
            var sheight = screen.height - 150;
            var swidth = screen.width - 10;
            var winoption = "left=0,top=0,height=" + sheight + ",width=" + swidth + ",toolbar=yes,menubar=yes,location=yes,status=yes,scrollbars=yes,resizable=yes";
            s = window.open('../Ultimus.UWF.workflow/OpenForm.aspx?TaskId=' + taskId + '&Type=' + type + '', '', winoption);
            s.focus();
        }
        function openDeptProcess(dept, ele) {
            switch (dept) {
                case "Administration":
                    $("#administrationDept").show;
                    break;
                case "Purchase":
                    $("#purchaseDept").show;
                    break;
                case "Personnel":
                    $("#personalDept").show;
                    break;
                case "Finance":
                    $("#financeDept").show;
                    break;
                case "Quality":
                    $("#qualityDept").show;
                    break;
                case "IT":
                    $("#ITDept").show;
                    break;
            }
            return true;
        }
        $(document).ready(function () {
            $("#administrator").click();
            $(".HR_6").html("Time Record Application");
        });
        function selectPic(obj) {
            $(".dept").find("a").each(function () {
                $(this).attr("style", "background-image:url(hFimg/Department_bg_icon.png)");
            });
            $(obj).attr("style", "background-image:url(hFimg/Department_bg_icon_select.png)");
            var ProcessCategory = $(obj).next().html();
            $("#AdministrationDept").find("div").hide();
            
            $("#" + ProcessCategory + "").show();
        }
        function iframeAutoFit() {

        }
    </script>
     <style type="text/css">
     .depa{float:left; background-image:url(hFimg/Department_bg_icon.png); color:#fff; text-align:center; width:64px; height:50px; padding-top:15px; font-size:20px; margin-right:10px; margin-bottom:10px; line-height:20px;}
     .depaselect{float:left; background-image:url(hFimg/Department_bg_icon_select.png); color:#fff; text-align:center; width:64px; height:50px; padding-top:15px; font-size:20px; margin-right:10px; margin-bottom:10px; line-height:20px;}
     </style>
</head>
<body>
<form runat="server" id="form1" >
    <div style="display:none;">
    </div>
     <div class="contatn_right textpage_contant" style="width:880px">
    <div class="dept" >
    <span style="font-weight:bold;text-align:left;margin-bottom:0.5px">Department</span>
    <table>
    <tr>
    <td  align="center">
   <a id="administrator" href="javascript:void(0)" onclick="selectPic(this)">
   <img src="hFimg/Administration_icon.png" alt="" />
   </a>
    <p>Administrator</p>
    </td>
  <td align="center">
   <a href="javascript:void(0)" onclick="selectPic(this)">
   <img src="hFimg/Purchase_icon.png" alt="" />
   </a>
    <p>Purchase</p>
     </td>
      <td  align="center">
   <a href="javascript:void(0)" onclick="selectPic(this)">
   <img src="hFimg/Personel_icon.png" alt="" />
   </a>
    <p>HR</p>
     </td>
      <td >
   <a href="javascript:void(0)" onclick="selectPic(this)">
   <img src="hFimg/Personnel-_icon.png" alt="" />
   </a>
    <p>Finance</p>
     </td>
      <td >
   <a href="javascript:void(0)" onclick="selectPic(this)">
   <img src="hFimg/Quality_icon.png" alt="" />
   </a>
    <p>Quality</p>
     </td>
      <td >
   <a href="javascript:void(0)" onclick="selectPic(this)">
   <img src="hFimg/it_icon.png" alt="" />
   </a>
    <p>IT</p>
     </td>
    </tr>
    </table>
    <h2 style="text-align:left;font-weight:bold;">procedure</h2>
    <hr width="100%" style="margin-left:0;margin-right:0;padding-left:0;padding-right:0;"/>
    </div>
     <div class="app_box">
      <div class="box2" id="AdministrationDept" style="">
        <asp:Repeater ID="rpAdministrationProcess" runat="server" OnItemDataBound="rpAllProcess_ItemDataBound">
            <ItemTemplate>
           <div id='<%#Eval("CATEGORYNAME") %>'>
                    <asp:Repeater ID="rpProcess" runat="server">
                        <ItemTemplate>
                        <a href="javascript:void(0)" class='<%#Eval("ICON")%>' onclick="javascript:openForm('<%#Eval("TASKID") %>','NEWREQUEST',this);" ><%#Eval("PROCESSNAME")%></a>
                                   </ItemTemplate>
                    </asp:Repeater>
                    </div>
            </ItemTemplate>
        </asp:Repeater>
      </div>
      <div class="box2" id="PurchaseDept" style="">
        <asp:Repeater ID="rpPurchaseProcess" runat="server" OnItemDataBound="rpAllProcess_ItemDataBound">
            <ItemTemplate>
           <div id='<%#Eval("CATEGORYNAME") %>'>
                    <asp:Repeater ID="rpProcess" runat="server">
                        <ItemTemplate>
                        <a href="javascript:void(0)" class='<%#GetImgChange(Eval("PROCESSNAME").ToString()) %>' onclick="javascript:openForm('<%#Eval("TASKID") %>','NEWREQUEST',this);" ><%#Eval("PROCESSNAME")%></a>
                                   </ItemTemplate>
                    </asp:Repeater>
                    </div>
            </ItemTemplate>
        </asp:Repeater>
      </div>
      <div class="box2" id="PersonnelDept" style="">
        <asp:Repeater ID="rpPersonnelProcess" runat="server" OnItemDataBound="rpAllProcess_ItemDataBound">
            <ItemTemplate>
           <div id='<%#Eval("CATEGORYNAME") %>'>
                    <asp:Repeater ID="rpProcess" runat="server">
                        <ItemTemplate>
                        <a href="javascript:void(0)" class='<%#GetImgChange(Eval("PROCESSNAME").ToString()) %>' onclick="javascript:openForm('<%#Eval("TASKID") %>','NEWREQUEST',this);" ><%#UIHelper.ChangeProcessName(Eval("PROCESSNAME").ToString())%></a>
                                   </ItemTemplate>
                    </asp:Repeater>
                    </div>
            </ItemTemplate>
        </asp:Repeater>
      </div>
      <div class="box2" id="FinanceDept" style="">
        <asp:Repeater ID="rpFinanceProcess" runat="server" OnItemDataBound="rpAllProcess_ItemDataBound">
            <ItemTemplate>
           <div id='<%#Eval("CATEGORYNAME") %>'>
                    <asp:Repeater ID="rpProcess" runat="server">
                        <ItemTemplate>
                        <a href="javascript:void(0)" class='<%#GetImgChange(Eval("PROCESSNAME").ToString()) %>' onclick="javascript:openForm('<%#Eval("TASKID") %>','NEWREQUEST',this);" ><%#Eval("PROCESSNAME")%></a>
                        </ItemTemplate>
                    </asp:Repeater>
                    </div>
            </ItemTemplate>
        </asp:Repeater>
      </div>
      <div class="box2" id="QualityDept" style="">
        <asp:Repeater ID="rpQualityProcess" runat="server" OnItemDataBound="rpAllProcess_ItemDataBound">
            <ItemTemplate>
           <div id='<%#Eval("CATEGORYNAME") %>'>
                    <asp:Repeater ID="rpProcess" runat="server">
                        <ItemTemplate>
                        <a href="javascript:void(0)" class='<%#GetImgChange(Eval("PROCESSNAME").ToString()) %>' onclick="javascript:openForm('<%#Eval("TASKID") %>','NEWREQUEST',this);" ><%#Eval("PROCESSNAME")%></a>
                                   </ItemTemplate>
                    </asp:Repeater>
                    </div>
            </ItemTemplate>
        </asp:Repeater>
      </div>
      <div class="box2" id="ITDept" style="">
        <asp:Repeater ID="rpITProcess" runat="server" OnItemDataBound="rpAllProcess_ItemDataBound">
            <ItemTemplate>
           <div id='<%#Eval("CATEGORYNAME") %>'>
                    <asp:Repeater ID="rpProcess" runat="server">
                        <ItemTemplate>
                        <a href="javascript:void(0)" class='<%#GetImgChange(Eval("PROCESSNAME").ToString()) %>' onclick="javascript:openForm('<%#Eval("TASKID") %>','NEWREQUEST',this);" ><%#Eval("PROCESSNAME")%></a>
                                   </ItemTemplate>
                    </asp:Repeater>
                    </div>
            </ItemTemplate>
        </asp:Repeater>
      </div>
    </div>
    </div>

    <div style="display: none">
        <asp:TextBox ID="txtCurrentCategory" runat="server"></asp:TextBox>
        <asp:TextBox ID="txtType" runat="server"></asp:TextBox>
    </div>
    <script type="text/javascript">
        var currentResourceId = "";

        $(document).ready(function () {
            ShowProcess($("#txtCurrentCategory").val());
            var DBRWStr = $(window.parent.$("#td_DBRW"));
            var StrIndex = $(DBRWStr).html().indexOf('(') - 0;
            var Comstr = "";
            if (StrIndex > 0) {
                Comstr = $(DBRWStr).html().substr(0, StrIndex);
            }
            else {
                Comstr = $(DBRWStr).html();
            }
            var trCount = $("#txtType").val();
            if (trCount != "0") {
                $(DBRWStr).html(Comstr + "(" + trCount + ")");
            }
        });

        function ShowProcess(resourceID) {
            if (currentResourceId == resourceID) {
                return;
            }
            $(".daiban dl").attr("class", "fqi");
            $(".tanchu").hide();
            $("#dlProcess" + resourceID).attr("class", "fqi fqicurrently");
            $("#ulProcess" + resourceID).show();
            currentResourceId = resourceID;
        }

        function changeFav(ele, resourceID) {
            
        }
   
    </script>
    </form>
</body>
</html>
