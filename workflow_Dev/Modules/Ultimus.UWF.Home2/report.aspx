<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="report.aspx.cs" Inherits="Ultimus.UWF.Home2.report" %>
<%@ Import Namespace="Ultimus.UWF.Home2.Code" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <script type="text/javascript" src="<%=Ultimus.UWF.Common.Logic.WebUtil.GetRootPath() %>/Assets/js/common.js"></script>
    <link href="css/workflow.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript">
        function openForm(reportUrl) {
            var sheight = screen.height-150;
            var swidth = screen.width-10;
            var winoption = "left=0,top=0,height=" + sheight + ",width=" + swidth + ",toolbar=yes,menubar=yes,location=yes,status=yes,scrollbars=yes,resizable=yes";
            s = window.open(reportUrl, '', winoption);
            s.focus();
        }
    </script>
</head>
<body>
<form runat="server" id="form1" >
    <div style="display:none;">
    </div>
     <div class="contatn_right textpage_contant" style="width:880px">
     <div class="app_box">
      <div class="box1">
      </div>
      <div class="box2">
                <%--<a href="javascript:void(0)" class="app HR_6" onclick="javascript:openForm('<%#Eval("REPORTURL") %>');" ><%#Eval("REPORTNAME")%></a>--%>
                <a href="../Ultimus.UWF.Report/OTAndDayoffReport.aspx" target="_blank" class="app HR_6"  >
                OT-Dayoff Report</a>
                <a href="../Ultimus.UWF.Report/Leavetest.aspx" target="_blank" class="app HR_8"  >
                Leave Report</a>
           
                <a href="../Ultimus.UWF.Report/DocumentReiwBallotingList.aspx" target="_blank" class="app Quality_1"  >
                Document Review and Approve Form</a>

                  <a href="../Presale.Process.EmployeeTraining/EmployyeTrainingReports.aspx" target="_blank" class="app Quality_1"  >
                  Training Statistics Reports</a>
                <a href="../Presale.Process.ProcessPerformance/KPIReports.aspx" target="_blank" class="app Quality_1"  >
                KPI Reports</a>
                  <a href="../Presale.Process.EmployeeTraining/TrainingListReport.aspx" target="_blank" class="app Quality_1"  >
                Training List Reports</a>
        <asp:Repeater runat="server" ID="RpList">
      <ItemTemplate>
        <a href='<%#Eval("URL") %>' target="_blank" class='<%#Eval("ICON") %>'  >
               <%#Eval("MENUNAME")%></a>
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
//            ShowProcess($("#txtCurrentCategory").val());
//            var DBRWStr = $(window.parent.$("#td_DBRW"));
//            var StrIndex = $(DBRWStr).html().indexOf('(') - 0;
//            var Comstr = "";
//            if (StrIndex > 0) {
//                Comstr = $(DBRWStr).html().substr(0, StrIndex);
//            }
//            else {
//                Comstr = $(DBRWStr).html();
//            }
//            var trCount = $("#txtType").val();
//            if (trCount != "0") {
//                $(DBRWStr).html(Comstr + "(" + trCount + ")");
//            }
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
            //            $.post("AddFavHandler.ashx",
            //            { resourceID: resourceID },
            //            function (result) {
            //                if (result == "0") {
            //                    $(ele).attr("src", "images/fav.jpg");
            //                }
            //                else {
            //                    $(ele).attr("src", "images/fav1.jpg");
            //                }
            //            }
            //         );
        }
   
    </script>
    </form>
</body>
</html>
