<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="NewRequest.aspx.cs" Inherits="Presale.Process.TravelExpenseReport.NewRequest" %>

<%@ Register Src="/Modules/Ultimus.UWF.Form.ProcessControl/UserInfo.ascx" TagName="UserInfo" TagPrefix="ui" %>
<%@ Register Src="/Modules/Ultimus.UWF.Form.ProcessControl/ApprovalHistory.ascx" TagName="ApprovalHistory" TagPrefix="ah" %>
<%@ Register Src="/Modules/Ultimus.UWF.Form.ProcessControl/Attachments.ascx" TagName="Attachments" TagPrefix="attach" %>
<%@ Register Src="/Modules/Ultimus.UWF.Form.ProcessControl/ButtonList.ascx" TagName="ButtonList" TagPrefix="btn" %>
<%@ Import Namespace="Ultimus.UWF.Common.Logic" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta http-equiv="X-UA-Compatible"  content="IE=EmulateIE8" />
    <title>Travel Expense Request Process</title>
    <script type="text/javascript" src="/Assets/js/common.js"></script>
    <script type="text/javascript">
        function beforeSubmit() {
            
            var totalUSD = $("#fld_TotalUSD").val() - 0;
            var totalRMB = $("#fld_TotalRMB").val() - 0;
            var rate = $("#fld_Rate").val() - 0;
            $("#fld_USD").val((totalUSD + totalRMB / rate).toFixed(2));
            var GMCODE = $("#fld_GMCODE").val().toUpperCase();
            var EMPLOYEECODE = $("#fld_Employee").val().toUpperCase();
            if (EMPLOYEECODE !=$.trim(GMCODE)) {
                if ($("#fld_TravelRequestNo").val() == "") {
                    alert("You have't fill in Travel Request NO.");
                    return false;
                }
            }
            var OthersFlag = "";
            var BMEFlag = "";
           // $("#ExpenseDetail").find('input[Title="Others"]').each(function (i, Etr) {
            $("#ExpenseDetail").find("tr").each(function (i, Etr) {
                //console.log($(this).val());
                var Others = $(this).find('input[Title="Others"]');
                var BME = $(this).find('input[Title="BME"]');
                //console.log($(Others).val());
                //if ($(this).find('input[Title="Others"]').val() != "") {
                if ($(Others).val() != undefined) {
                if(($(Others).val()-0)>0){
                    OthersFlag = "1";
                    }
            }
            if ($(BME).val() != undefined) {
                if (($(BME).val() - 0) > 0) {
                    BMEFlag = "1";
                }
            } 
          });
            if (OthersFlag != ""&&$("#fld_Remark").val()=="") {
                alert("Please input Remark");
                return false;
            }
            if (BMEFlag != "" && $("#ExpenseDetail1").find("tr").length==0) {
                alert("Please Input ENTERTAINMENT");
                return false;
            }


            var summary = "Travel Expense Request Process";
            $("#UserInfo1_fld_PROCESSSUMMARY").val(summary);
            $("#Attachments1_txtMust").val("1");
           
            return true;
        }
        function beforeSave() {
            var summary = "Travel Expense Request Process";
            $("#UserInfo1_fld_PROCESSSUMMARY").val(summary);
            $("#fld_TRSummary").val(summary);
            return true;
        }
        $(document).ready(function () {

            if ($("#hdUrgeTask").val() == "Yes") {
                $("#ReturnBackTask").show();
            }
            $("#costcenter").find("option:selected").text($("#fld_CostCenterSubject").val());
            $("#detail").find("tbody").each(function (i, Etr) {
                var currencyMeal = $(Etr).find("tr").eq(1).find("td").eq(1).children().last().val();
                var paidByMeal = $(Etr).find("tr").eq(2).find("td").eq(1).children().last().val();
                
               // $(Etr).find("tr").eq(1).find("td").eq(1).children().find("option:selected").val(currencyMeal);
                $(Etr).find("tr").eq(1).find("td").eq(1).children().val(currencyMeal);
                if (paidByMeal == "Paid By Company") {
                    $(Etr).find("tr").eq(2).find("td").eq(1).children().find("input").attr("checked", true);
                }
                var currencyBME = $(Etr).find("tr").eq(1).find("td").eq(2).children().last().val();
                $(Etr).find("tr").eq(1).find("td").eq(2).children().val(currencyBME)//.find("option:selected").text(currencyBME);
                var paidByBME = $(Etr).find("tr").eq(2).find("td").eq(2).children().last().val();
                if (paidByBME == "Paid By Company") {
                    $(Etr).find("tr").eq(2).find("td").eq(2).children().find("input").attr("checked", true);
                }
                var currencyAir = $(Etr).find("tr").eq(1).find("td").eq(3).children().last().val();
                $(Etr).find("tr").eq(1).find("td").eq(3).children().val(currencyAir); //.find("option:selected").text(currencyAir);
                var paidByAir = $(Etr).find("tr").eq(2).find("td").eq(3).children().last().val();
                if (paidByAir == "Paid By Company") {
                    $(Etr).find("tr").eq(2).find("td").eq(3).children().find("input").attr("checked", true);
                }
                else {
                    $(Etr).find("tr").eq(2).find("td").eq(3).children().find("input").attr("checked", false);
                }
                var currencyHotel = $(Etr).find("tr").eq(1).find("td").eq(4).children().last().val();
                $(Etr).find("tr").eq(1).find("td").eq(4).children().val(currencyHotel); //.find("option:selected").text(currencyHotel);
                var paidByHotel = $(Etr).find("tr").eq(2).find("td").eq(4).children().last().val();
                if (paidByHotel == "Paid By Company") {
                    $(Etr).find("tr").eq(2).find("td").eq(4).children().find("input").attr("checked", true);
                }
                var currencyTB = $(Etr).find("tr").eq(1).find("td").eq(5).children().last().val();
                $(Etr).find("tr").eq(1).find("td").eq(5).children().val(currencyTB); //.find("option:selected").text(currencyTB);
                var paidByTB = $(Etr).find("tr").eq(2).find("td").eq(5).children().last().val();
                if (paidByTB == "Paid By Company") {
                    $(Etr).find("tr").eq(2).find("td").eq(5).children().find("input").attr("checked", true);
                }
                var currencyLau = $(Etr).find("tr").eq(1).find("td").eq(6).children().last().val();
                $(Etr).find("tr").eq(1).find("td").eq(6).children().val(currencyLau); //.find("option:selected").text(currencyLau);
                var paidByLau = $(Etr).find("tr").eq(2).find("td").eq(6).children().last().val();
                if (paidByLau == "Paid By Company") {
                    $(Etr).find("tr").eq(2).find("td").eq(6).children().find("input").attr("checked", true);
                }
                var currencyTT = $(Etr).find("tr").eq(1).find("td").eq(7).children().last().val();
                $(Etr).find("tr").eq(1).find("td").eq(7).children().val(currencyTT); //.find("option:selected").text(currencyTT);
                var paidByTT = $(Etr).find("tr").eq(2).find("td").eq(7).children().last().val();
                if (paidByTT == "Paid By Company") {
                    $(Etr).find("tr").eq(2).find("td").eq(7).children().find("input").attr("checked", true);
                }
                var currencyOthers = $(Etr).find("tr").eq(1).find("td").eq(8).children().last().val();
                $(Etr).find("tr").eq(1).find("td").eq(8).children().val(currencyOthers); //.find("option:selected").text(currencyOthers);
                var paidByOthers = $(Etr).find("tr").eq(2).find("td").eq(8).children().last().val();
                if (paidByOthers == "Paid By Company") {
                    $(Etr).find("tr").eq(2).find("td").eq(8).children().find("input").attr("checked", true);
                }
            });
            if ($("#hdIncident").val() != "") {
                $("#ButtonList1_btnSubmit").val("Submit");
                $("#ButtonList1_btnBack").hide();
                $("#ButtonList1_btnReject").show();
            }
            getItem();
        });
        function item_onclick() {
            getItem();
        }
        function getButtonCheck(obj) {
            if ($(obj).attr("checked")) {
                $(obj).parent().next().val("Paid By Company");
            }
            else {
                $(obj).parent().next().val("Paid By Employee");
            }
            getItem();
        }
        function getItem() {
            var paidByCompany = 0;
            var paidByEmployee = 0;
            var rate = $("#fld_Rate").val();
            var TotalMeal = 0; var TotalMealPE = 0;
            var TotalBME = 0; var TotalBMEPE = 0;
            var TotalAirfare = 0; var TotalAirfarePE = 0;
            var TotalHotel = 0; var TotalHotelPE = 0;
            var TotalTB = 0; var TotalTBPE = 0;
            var TotalLaundry = 0; var TotalLaundryPE = 0;
            var TotalTT = 0; var TotalTTPE = 0;
            var TotalOthers = 0; var TotalOthersPE = 0;

            $("#detail").find("tbody").each(function (i, Etr) {
                var meal = $(Etr).find("tr").eq(0).find("td").eq(2).children().val();
                var currencyMeal = $(Etr).find("tr").eq(1).find("td").eq(1).children().last().val();
                var paidByMeal = $(Etr).find("tr").eq(2).find("td").eq(1).children().last().val();
                var BME = $(Etr).find("tr").eq(0).find("td").eq(3).children().val();
                var currencyBME = $(Etr).find("tr").eq(1).find("td").eq(2).children().last().val();
                var paidByBME = $(Etr).find("tr").eq(2).find("td").eq(2).children().last().val();
                var Air = $(Etr).find("tr").eq(0).find("td").eq(4).children().val();
                var currencyAir = $(Etr).find("tr").eq(1).find("td").eq(3).children().last().val();
                var paidByAir = $(Etr).find("tr").eq(2).find("td").eq(3).children().last().val();
                var Hotel = $(Etr).find("tr").eq(0).find("td").eq(5).children().val();
                var currencyHotel = $(Etr).find("tr").eq(1).find("td").eq(4).children().last().val();
                var paidByHotel = $(Etr).find("tr").eq(2).find("td").eq(4).children().last().val();
                var TB = $(Etr).find("tr").eq(0).find("td").eq(6).children().val();
                var currencyTB = $(Etr).find("tr").eq(1).find("td").eq(5).children().last().val();
                var paidByTB = $(Etr).find("tr").eq(2).find("td").eq(5).children().last().val();
                var Lau = $(Etr).find("tr").eq(0).find("td").eq(7).children().val();
                var currencyLau = $(Etr).find("tr").eq(1).find("td").eq(6).children().last().val();
                var paidByLau = $(Etr).find("tr").eq(2).find("td").eq(6).children().last().val();
                var TT = $(Etr).find("tr").eq(0).find("td").eq(8).children().val();
                var currencyTT = $(Etr).find("tr").eq(1).find("td").eq(7).children().last().val();
                var paidByTT = $(Etr).find("tr").eq(2).find("td").eq(7).children().last().val();
                var Others = $(Etr).find("tr").eq(0).find("td").eq(9).children().val();
                var currencyOthers = $(Etr).find("tr").eq(1).find("td").eq(8).children().last().val();
                var paidByOthers = $(Etr).find("tr").eq(2).find("td").eq(8).children().last().val();
                var itemDetailArray = new Array(); //生成二位数组
                itemDetailArray[0] = new Array(meal, currencyMeal, paidByMeal);
                itemDetailArray[1] = new Array(BME, currencyBME, paidByBME);
                itemDetailArray[2] = new Array(Air, currencyAir, paidByAir);
                itemDetailArray[3] = new Array(Hotel, currencyHotel, paidByHotel);
                itemDetailArray[4] = new Array(TB, currencyTB, paidByTB);
                itemDetailArray[5] = new Array(Lau, currencyLau, paidByLau);
                itemDetailArray[6] = new Array(TT, currencyTT, paidByTT);
                itemDetailArray[7] = new Array(Others, currencyOthers, paidByOthers);
                var SubRMB = 0;
                var SubUSD = 0;
                for (var i = 0; i < itemDetailArray.length; i++) {
                    var item = itemDetailArray[i];
                    var itemValue = (item[0] == "" ? "0" : item[0]);
                    if ($.trim(item[1]) == "RMB") {
                        SubRMB += (itemValue - 0);
                    }
                    if ($.trim(item[1]) == "USD") {
                        SubUSD += (itemValue - 0);
                    }
                    if (item[2] == "Paid By Employee") {
                        var value = getPaidValue(itemValue, item[1], rate);
                        paidByEmployee += (value - 0);
                    }
                    if (item[2] == "Paid By Company") {
                        var value = getPaidValue(itemValue, item[1], rate);
                        paidByCompany += (value - 0);
                        //alert(paidByCompany);
                    }

                    if (i == 0) { //餐费
                        TotalMeal += getPaidValue(itemValue, item[1], rate) - 0;
                        if (item[2] == "Paid By Employee") {
                            TotalMealPE += getPaidValue(itemValue, item[1], rate) - 0;
                        }
                    }
                    if (i == 1) { //BM/E
                        TotalBME += getPaidValue(itemValue, item[1], rate) - 0;
                        if (item[2] == "Paid By Employee") {
                            TotalBMEPE += getPaidValue(itemValue, item[1], rate) - 0;
                        }
                    }
                    if (i == 2) { //Airfare
                        TotalAirfare += getPaidValue(itemValue, item[1], rate) - 0;
                        if (item[2] == "Paid By Employee") {
                            TotalAirfarePE += getPaidValue(itemValue, item[1], rate) - 0;
                        }
                    }
                    if (i == 3) { //Hotel
                         TotalHotel += getPaidValue(itemValue, item[1], rate) - 0;
                        if (item[2] == "Paid By Employee") {
                            TotalHotelPE += getPaidValue(itemValue, item[1], rate) - 0;
                        }
                    }
                    if (i == 4) { //TB
                        TotalTB += getPaidValue(itemValue, item[1], rate) - 0;
                        if (item[2] == "Paid By Employee") {
                            TotalTBPE += getPaidValue(itemValue, item[1], rate) - 0;
                        }
                    }
                    if (i == 5) { //Laundry
                        TotalLaundry += getPaidValue(itemValue, item[1], rate) - 0;
                        if (item[2] == "Paid By Employee") {
                            TotalLaundryPE += getPaidValue(itemValue, item[1], rate) - 0;
                        }
                    }
                    if (i == 6) { //TT
                        TotalTT += getPaidValue(itemValue, item[1], rate) - 0;
                        if (item[2] == "Paid By Employee") {
                            TotalTTPE += getPaidValue(itemValue, item[1], rate) - 0;
                        }
                    }
                    if (i == 7) { //Others
                        TotalOthers += getPaidValue(itemValue, item[1], rate) - 0;
                        if (item[2] == "Paid By Employee") {
                            TotalOthersPE += getPaidValue(itemValue, item[1], rate) - 0;
                        }
                    }
                    
                }


                $(Etr).find("tr").eq(0).find("td").eq(10).children().eq(0).text(formatNumber(SubUSD.toFixed(2), 2, 1));
                $(Etr).find("tr").eq(0).find("td").eq(10).children().eq(1).val(SubUSD.toFixed(2));
                $(Etr).find("tr").eq(0).find("td").eq(11).children().eq(0).text(formatNumber(SubRMB.toFixed(2), 2, 1));
                $(Etr).find("tr").eq(0).find("td").eq(11).children().eq(1).val(SubRMB.toFixed(2));
            });
            getTotalMoney(rate);

          
//            $("#spanTotalMeal").text(formatNumber(TotalMeal.toFixed(2), 2, 1));
//            $("#fld_TotalMeal").val(TotalMeal.toFixed(2));
//            $("#fld_TotalMealPE").val(TotalMealPE.toFixed(2));
            GetPaidSubTotal($("#spanTotalMeal"), $("#fld_TotalMeal"), $("#fld_TotalMealPE"), TotalMeal, TotalMealPE);
            GetPaidSubTotal($("#spanTotalBME"), $("#fld_TotalBME"), $("#fld_TotalBMEPE"), TotalBME, TotalBMEPE);
            GetPaidSubTotal($("#spanTotalAirfare"), $("#fld_TotalAirfare"), $("#fld_TotalAirfarePE"), TotalAirfare, TotalAirfarePE);
            GetPaidSubTotal($("#spanTotalHotel"), $("#fld_TotalHotel"), $("#fld_TotalHotelPE"), TotalHotel, TotalHotelPE);
            GetPaidSubTotal($("#spanTotalTB"), $("#fld_TotalTB"), $("#fld_TotalTBPE"), TotalTB, TotalTBPE);
            GetPaidSubTotal($("#spanTotalLaundry"), $("#fld_TotalLaundry"), $("#fld_TotalLaundryPE"), TotalLaundry, TotalLaundryPE);
            GetPaidSubTotal($("#spanTotalTT"), $("#fld_TotalTT"), $("#fld_TotalTTPE"), TotalTT, TotalTTPE);
            GetPaidSubTotal($("#spanTotalOthers"), $("#fld_TotalOthers"), $("#fld_TotalOthersPE"), TotalOthers, TotalOthersPE);


            $("#spanPaidByCompanyRMB").text(formatNumber(paidByCompany.toFixed(2), 2, 1));
            $("#fld_PaidByCompanyRMB").val(paidByCompany.toFixed(2));

            $("#spanPaidByEmployeeRMB").text(formatNumber(paidByEmployee.toFixed(2), 2, 1));
            $("#fld_PaidByEmployeeRMB").val(paidByEmployee.toFixed(2));
        }
        function GetPaidSubTotal(SpanTotal,TotalValue,TotalValuePE,TotalAmount,TotalAmountPE) {
            $(SpanTotal).text(formatNumber(TotalAmount.toFixed(2), 2, 1));
            $(TotalValue).val(TotalAmount.toFixed(2));
            $(TotalValuePE).val(TotalAmountPE.toFixed(2));
        }


        function getPaidValue(value, currency, rate) {//获取税率转换后的值
            if ($.trim(currency)== "USD") {
               
                value = (value - 0) * (rate - 0);
            }
            return value;
        }
        function getTotalMoney(rate) {//获取RMB，USD的total总和
            var value = 0;
            var totalRMB = 0;
            var totalUSD = 0;
            $("#detail").find("tbody").each(function (i, Etr) {
                value = $(Etr).find("tr").eq(0).find("td").eq(11).children().eq(1).val() - 0;
                totalRMB += value;
            });
            value = 0;
            $("#detail").find("tbody").each(function (i, Etr) {
                value = $(Etr).find("tr").eq(0).find("td").eq(10).children().eq(1).val() - 0;
                totalUSD += value;
            });
            $("#spanTotalRMB").text(formatNumber(totalRMB.toFixed(2), 2, 1));
           
            $("#fld_TotalRMB").val((totalRMB == "NaN" ? "0" : totalRMB).toFixed(2));

            $("#sapnTotalUSD").text(formatNumber(totalUSD.toFixed(2), 2, 1));
            $("#fld_TotalUSD").val((totalUSD == "NaN" ? "0" : totalUSD).toFixed(2));

            var RMB = (($("#fld_TotalRMB").val() - 0) + ($("#fld_TotalUSD").val() - 0) * (rate - 0)).toFixed(2);
            $("#spanRMB").text(formatNumber(RMB, 2, 1));
            $("#fld_RMB").val(RMB);
        }
        function ShowTime(obj, index) {
            var mintime = $(obj).parent().prev().find("input").val();
            var maxtime = $(obj).parent().next().find("input").val();
            var StartDate = "";
            var EndDate = "";
            if (index == 1 && maxtime != undefined) {
                EndDate = maxtime
            }
            if (mintime != undefined && index == 2) {
                StartDate = mintime;
            }
            if (index == 2) {
                WdatePicker({ dateFmt: 'yyyy-MM-dd' });
            }
            else {
                WdatePicker({ dateFmt: 'yyyy-MM-dd' });
            }
        }
        function costcenter_onchange(obj) {
            $("#fld_CostCenterSubject").val($(obj).find("option:selected").text());
        }
        function employee_onclick(obj) {
            var digStr = "dialogHeight:600px;dialogWidth:850px;";
            var ReturnValue = window.showModalDialog("./Employee.aspx", "", digStr);
            if (ReturnValue != null) {
                var Employeedetail = eval("(" + ReturnValue + ")");
                var Employee = Employeedetail[0].EmployeeCode;
                var EmployeeName = Employeedetail[1].EmployeeName;
                $(obj).val(Employee + "-" + EmployeeName);
                $(obj).next().val(Employee);
                $(obj).next().next().val(EmployeeName);
            }
        }
        function currency_onchange(obj){
            $(obj).next().val($(obj).find("option:selected").text());
            getItem();
        }
        function TravelRequestNo_onclick(obj) {
            var digStr = "dialogHeight:500px;dialogWidth:850px;"
            var ReturnValue = window.showModalDialog("./TravelRequestList.aspx", null, digStr);
            if (ReturnValue != null) {
                var TravelRequest = eval("(" + ReturnValue + ")");
                value = TravelRequest[0].TravelRequstNo;
                $("#fld_TravelRequestNo").val(value);
            }
        }
        function CostCenter_onclick(obj) {
            var digStr = "dialogHeight:500px;dialogWidth:850px;";
            var ReturnValue = window.showModalDialog("./CostCenter.aspx", "", digStr);
            if (ReturnValue != null) {
                var cosetcenterDetail = eval("(" + ReturnValue + ")");
                var CostCenter = cosetcenterDetail[0].cosetcenter;
                var Description = cosetcenterDetail[1].Description;
                $(obj).val(CostCenter + "-" + Description);
                $(obj).next().val(CostCenter);
            }
            $("#fld_Project").val("");
            if ($("#fld_CostCenterValue").val() == "50806200") {
                $("#fld_Project").val("EC919RD010");
            }
        }
        
        function project_onclick(obj) {
            var digStr = "dialogHeight:500px;dialogWidth:850px;";
            var ReturnValue = window.showModalDialog("./Project.aspx", "", digStr);
            if (ReturnValue != null) {
                var Project = eval("(" + ReturnValue + ")");
                var ProjectValue = Project[0].ProjectValue;
                var ProjectDisplayValue = Project[1].Description;
                $(obj).val(ProjectValue + "-" + ProjectDisplayValue);
                $(obj).next().val(ProjectValue);
            }
            
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div id="myDiv" class="container">
        <div class="row" dir="ltr">
            <ui:userinfo id="UserInfo1" processtitle="Travel Expense Request Process" processprefix="FINTE" tablename="PROC_TravelExpense"
                tablenamedetail="PROC_TravelExpenseDetails_DT,PROC_TravelExpenseThird_DT,PROC_TravelExpenseForth_DT" runat="server"></ui:userinfo>
                <asp:TextBox runat="server" ID="fld_DGM" style="display:none;"></asp:TextBox>
                 <asp:TextBox runat="server" ID="fld_GM" style="display:none;"></asp:TextBox>
                 <asp:TextBox runat="server" ID="fld_GMCODE" style="display:none;"></asp:TextBox>
                 <asp:TextBox runat="server" ID="fld_DGMLogin" style="display:none;"></asp:TextBox>
                 <asp:TextBox runat="server" ID="fld_GMLogin" style="display:none;"></asp:TextBox>
                 <asp:TextBox runat="server" ID="fld_FinLogin" style="display:none;"></asp:TextBox>
                 <asp:TextBox runat="server" ID="fld_deptLogin" style="display:none;"></asp:TextBox>
                 <asp:TextBox runat="server" ID="fld_CompareValue" value="1500.00" style="display:none;"></asp:TextBox>
                 <asp:TextBox runat="server" ID="fld_USD" style="display:none;"></asp:TextBox>
                 <asp:TextBox runat="server" ID="status" style="display:none;"></asp:TextBox>
            <p style="font-weight:bold">Travel Detail（"<span style=" background:red;">&nbsp;</span>" must write）(BM/E=Bussiness Meals/Entert,T/B=Taxi/Bus,TT=Train Ticket)</p>
            <table class="table table-condensed table-bordered" id="detail" >
                <thead>
                <tr>
                    <th colspan="3" style="vertical-align:middle;width:27%"><span style=" background:red;float:left">&nbsp;</span><p style="text-align:center">BusIness Purpose of Trip</p></th>
                    <td colspan="9">
                        <asp:TextBox runat="server" ID="fld_PurposeOfTrip" CssClass="validate[required]" TextMode="MultiLine" MaxLength="100" Rows="3" style="margin-bottom:0px;" width="98%"></asp:TextBox>
                    </td>
                    <th style="vertical-align:middle;"><p style="text-align:center">Rate</p></th>
                    <td><asp:TextBox runat="server" ID="fld_Rate" Rows="3" TextMode="MultiLine" style="width:74%;"></asp:TextBox></td>
                </tr>
                <tr>
                        <td class="td-label" colspan="3" >
                            <span style=" background:red;float:left">&nbsp;</span>
                            <p style="text-align:center">Employee</p>
                        </td>
                        <td class="td-content" colspan="3">
                             <asp:TextBox runat="server" ID="fld_EmployeeDisplay" onclick="employee_onclick(this)" Width="92%"></asp:TextBox>
                            <asp:TextBox runat="server" ID="fld_Employee" style="display:none;"></asp:TextBox>
                            <asp:TextBox runat="server" ID="fld_EmployeeName" style="display:none;"></asp:TextBox>
                        </td>
                        <td class="td-label" colspan="2"><span style=" background:red;float:left">&nbsp;</span><p style="text-align:center">Cost Center</p></td>
                        <td class="td-content" colspan="3">
                            <asp:TextBox runat="server" ID="fld_CostCenterDisplay" CssClass="validate[required]" onclick="CostCenter_onclick(this)" Width="88%"></asp:TextBox>
                            <asp:TextBox runat="server" ID="fld_CostCenterValue" style="display:none"></asp:TextBox>
                        </td>
                        <td class="td-label"><p style="text-align:center">Project</p></td>
                        <td class="td-content" colspan="2">
                            <asp:TextBox runat="server" ID="fld_Project" onfocus="this.blur()" Width="88%"></asp:TextBox>
                        </td>
                    </tr>
                <tr>
                    <th width="1%"><p style="text-align:center">NO.</p></th>
                    <th width="8.3%"><p style="text-align:center">Location</p></th>
                    <th width="8.3%"><p style="text-align:center">Date</p></th>
                    <th width="6.8%"><p style="text-align:center">Meal</p></th>
                    <th width="6.8%"><p style="text-align:center">BM/E</p></th>
                    <th width="6.8%"><p style="text-align:center">Airfare</p></th>
                    <th width="6.8%"><p style="text-align:center">Hotel</p></th>
                    <th width="6.8%"><p style="text-align:center">T/B</p></th>
                    <th width="6.8%"><p style="text-align:center">Laundry</p></th>
                    <th width="6.8%"><p style="text-align:center">TT</p></th>
                    <th width="6.8%"><p style="text-align:center">Others</p></th>
                    <th width="10%"><p style="text-align:center">T.USD</p></th>
                    <th width="10%"><p style="text-align:center">T.RMB</p></th>
                   
                    <th width="7.2%"></th>
                </tr>
                </thead>
                <asp:Repeater runat="server" ID="fld_detail_PROC_TravelExpenseDetails_DT" OnItemCommand="fld_detail_PROC_TravalExpenseDetails_DT_ItemCommand" OnItemDataBound="fld_detail_PROC_TravalExpenseDetails_DT_ItemDataBound">
                    <ItemTemplate>
                        <tbody id="ExpenseDetail">
                        <tr>
                            <th style="text-align:center">
                                <span style=" background:red;float:left">&nbsp;</span> 
                                <asp:TextBox ID="fld_FORMID" Text='<%#Eval("FORMID") %>' runat="server" Style="display: none"></asp:TextBox>
                                <asp:Label ID="fld_ROWID" Text='<%# Container.ItemIndex+1%>' runat="server"></asp:Label>
                            </th>
                            <td>
                                <asp:TextBox ID="fld_TravelFrom" Text='<%#Eval("TravelFrom") %>' MaxLength="12" runat="server" Width="78%"></asp:TextBox>
                            </td>
                            <td>
                                <asp:TextBox ID="fld_ItemValue" runat="server" onclick="WdatePicker({readOnly:false,dateFmt: 'yyyy-MM-dd'})" Text='<%# String.IsNullOrEmpty(Eval("ItemValue").ToString())? "":DateTime.Parse(Eval("ItemValue").ToString()).ToString("yyyy-MM-dd") %>'  CssClass="validate[required]" Width="84%"></asp:TextBox>
                            </td>
                            <td>
                                <asp:TextBox ID="fld_Meal" Text='<%#Eval("Meal") %>' runat="server" onchange="item_onclick()" Width="78%"></asp:TextBox>
                            </td>
                            <td>
                                <asp:TextBox ID="fld_BME" Text='<%#Eval("BME") %>' runat="server" Title="BME" onchange="item_onclick()" Width="74%"></asp:TextBox>
                            </td>
                            <td>
                                <asp:TextBox ID="fld_Air" Text='<%#Eval("Air") %>' runat="server" onchange="item_onclick()" Width="74%"></asp:TextBox>
                            </td>
                            <td>
                                <asp:TextBox ID="fld_Hotel" Text='<%#Eval("Hotel") %>' runat="server" onchange="item_onclick()" Width="74%"></asp:TextBox>
                            </td>
                            <td>
                                <asp:TextBox ID="fld_TB" Text='<%#Eval("TB") %>' runat="server" onchange="item_onclick()" Width="74%"></asp:TextBox>
                            </td>
                            <td>
                                <asp:TextBox ID="fld_Lau" Text='<%#Eval("Lau") %>' runat="server" onchange="item_onclick()" Width="74%"></asp:TextBox>
                            </td>
                             <td>
                                <asp:TextBox ID="fld_TT" Text='<%#Eval("TT") %>' runat="server" onchange="item_onclick()" Width="74%"></asp:TextBox>
                            </td>
                             <td>
                                <asp:TextBox ID="fld_Others" Text='<%#Eval("Others") %>' Title="Others" onchange="item_onclick()" runat="server" Width="74%"></asp:TextBox>
                            </td>
                            <td>
                            <span></span>
                                <asp:TextBox ID="fld_USD" Text='<%#Eval("USD") %>' style="display:none"  value="0.00" runat="server" CssClass="validate[required]" Width="84%"></asp:TextBox>
                            </td>
                             <td>
                             <span></span>
                                <asp:TextBox ID="fld_RMB" Text='<%#Eval("RMB") %>'  style="display:none" onfocus="this.blur()" value="0.00" runat="server" CssClass="validate[required]" Width="84%"></asp:TextBox>
                            </td>
                            <td>
                                <asp:Button ID="btnDelete" runat="server" Text="Del" Width="88%" CssClass="btn" CommandName="del" ClientIDMode="Static" OnClientClick="return confirm('Confirm Del？')" />
                            </td>
                        </tr>
                        <tr>
                            <th></th>
                            <td colspan="2"><p style="text-align:right">Currency</p></td>
                            <td>
                                <asp:DropDownList runat="server" onchange="currency_onchange(this)" Width="90%">
                                    <asp:ListItem Selected="True">RMB</asp:ListItem>
                                    <asp:ListItem>USD</asp:ListItem>
                                </asp:DropDownList>
                                <asp:TextBox runat="server" ID="fld_CurrencyMeal" value="RMB" Text='<%#Eval("CurrencyMeal") %>' style="display:none;"></asp:TextBox>
                            </td>
                            <td>
                                <asp:DropDownList ID="DropDownList1" runat="server" onchange="currency_onchange(this)" Width="90%">
                                    <asp:ListItem Selected="True">RMB</asp:ListItem>
                                    <asp:ListItem>USD</asp:ListItem>
                                </asp:DropDownList>
                                <asp:TextBox runat="server" ID="fld_CurrencyBME" value="RMB" Text='<%#Eval("CurrencyBME") %>' style="display:none;"></asp:TextBox>
                            </td>
                            <td>
                                <asp:DropDownList ID="DropDownList2" runat="server" onchange="currency_onchange(this)" Width="90%">
                                    <asp:ListItem Selected="True">RMB</asp:ListItem>
                                    <asp:ListItem>USD</asp:ListItem>
                                </asp:DropDownList>
                                <asp:TextBox runat="server" ID="fld_CurrencyAir" value="RMB" Text='<%#Eval("CurrencyAir") %>' style="display:none;"></asp:TextBox>
                            </td>
                            <td>
                                <asp:DropDownList ID="DropDownList3" runat="server" onchange="currency_onchange(this)" Width="90%">
                                    <asp:ListItem Selected="True">RMB</asp:ListItem>
                                    <asp:ListItem>USD</asp:ListItem>
                                </asp:DropDownList>
                                <asp:TextBox runat="server" ID="fld_CurrencyHotel" value="RMB" Text='<%#Eval("CurrencyHotel") %>' style="display:none;"></asp:TextBox>
                            </td>
                            <td>
                                <asp:DropDownList ID="DropDownList4" runat="server" onchange="currency_onchange(this)" Width="90%">
                                    <asp:ListItem Selected="True">RMB</asp:ListItem>
                                    <asp:ListItem>USD</asp:ListItem>
                                </asp:DropDownList>
                                <asp:TextBox runat="server" ID="fld_CurrencyTB" value="RMB" Text='<%#Eval("CurrencyTB") %>' style="display:none;"></asp:TextBox>
                            </td>
                            <td>
                                <asp:DropDownList ID="DropDownList5" runat="server" onchange="currency_onchange(this)" Width="90%">
                                    <asp:ListItem Selected="True">RMB</asp:ListItem>
                                    <asp:ListItem>USD</asp:ListItem>
                                </asp:DropDownList>
                                <asp:TextBox runat="server" ID="fld_CurrencyLau" value="RMB" Text='<%#Eval("CurrencyLau") %>' style="display:none;"></asp:TextBox>
                            </td>
                            <td>
                                <asp:DropDownList ID="DropDownList6" runat="server" onchange="currency_onchange(this)" Width="90%">
                                    <asp:ListItem Selected="True">RMB</asp:ListItem>
                                    <asp:ListItem>USD</asp:ListItem>
                                </asp:DropDownList>
                                <asp:TextBox runat="server" ID="fld_CurrencyTT" value="RMB" Text='<%#Eval("CurrencyTT") %>' style="display:none;"></asp:TextBox>
                            </td>
                            <td>
                                <asp:DropDownList ID="DropDownList7" runat="server" onchange="currency_onchange(this)" Width="90%">
                                    <asp:ListItem Selected="True">RMB</asp:ListItem>
                                    <asp:ListItem>USD</asp:ListItem>
                                </asp:DropDownList>
                                <asp:TextBox runat="server" ID="fld_CurrencyOther" value="RMB" Text='<%#Eval("CurrencyOther") %>' style="display:none;"></asp:TextBox>
                            </td>
                            <td colspan="3"></td>
                        </tr>
                        <tr>
                            <th><span style="float:left">&nbsp;</span> </th>
                            <td colspan="2"><p style="float:right">Paid By Company</p></td>
                            <td style="text-align:center">
                                <asp:CheckBox class="CheckBox1" runat="server" Text="" onclick="getButtonCheck(this)"></asp:CheckBox>
                                <asp:TextBox ID="fld_payMeal" Text='<%#Eval("payMeal") %>' runat="server" value="Paid By Employee" style="display:none;"></asp:TextBox>
                            </td>
                            <td style="text-align:center">
                               <asp:CheckBox class="CheckBox2" runat="server" Text="" onclick="getButtonCheck(this)"></asp:CheckBox>
                                <asp:TextBox ID="fld_payBME" Text='<%#Eval("payBME") %>' runat="server" value="Paid By Employee" style="display:none;"></asp:TextBox>
                            </td>
                            <td style="text-align:center">
                               <asp:CheckBox class="CheckBox3" runat="server" Checked="true" Text="" onclick="getButtonCheck(this)"></asp:CheckBox>
                                <asp:TextBox ID="fld_payAir" Text='<%#Eval("payAir") %>' runat="server" value="Paid By Company" style="display:none;"></asp:TextBox>
                            </td>
                            <td style="text-align:center">
                                <asp:CheckBox class="CheckBox4" runat="server" Text="" onclick="getButtonCheck(this)"></asp:CheckBox>
                                <asp:TextBox ID="fld_payHotel" Text='<%#Eval("payHotel") %>' runat="server" value="Paid By Employee" style="display:none;"></asp:TextBox>
                            </td>
                            <td style="text-align:center">
                                <asp:CheckBox class="CheckBox5" runat="server" Text="" onclick="getButtonCheck(this)"></asp:CheckBox>
                                <asp:TextBox ID="fld_payTB" Text='<%#Eval("payTB") %>' runat="server" value="Paid By Employee" style="display:none;"></asp:TextBox>
                            </td>
                            <td style="text-align:center">
                                <asp:CheckBox class="CheckBox6" runat="server" Text="" onclick="getButtonCheck(this)"></asp:CheckBox>
                                <asp:TextBox ID="fld_payLau" Text='<%#Eval("payLau") %>' runat="server" value="Paid By Employee" style="display:none;"></asp:TextBox>
                            </td>
                            <td style="text-align:center">
                                <asp:CheckBox class="CheckBox8" runat="server" Text="" onclick="getButtonCheck(this)"></asp:CheckBox>
                                <asp:TextBox ID="fld_payTT" Text='<%#Eval("payTT") %>' runat="server" value="Paid By Employee" style="display:none;"></asp:TextBox>
                            </td>
                             <td style="text-align:center">
                               <asp:CheckBox class="CheckBox9" runat="server" Text="" onclick="getButtonCheck(this)"></asp:CheckBox>
                                <asp:TextBox ID="fld_payOthers" Text='<%#Eval("payOthers") %>' runat="server" value="Paid By Employee" style="display:none;"></asp:TextBox>
                            </td>
                            <td colspan="3"></td>
                        </tr>
                        </tbody>
                    </ItemTemplate>
                </asp:Repeater>
                <tfoot>
                <tr>
                    <th></th>
                    <td>
                        <asp:Button ID="btnAddDetail" runat="server" Text="add" CssClass="btn" CausesValidation="false" OnClick="btnAddDetail_Click"   />
                    </td>
                     <td>
                     <p style="text-align:right;">Total</p>
                    </td>
                     <td >
                      <span id="spanTotalMeal"></span>
                    <asp:TextBox ID="fld_TotalMeal" value="0.00" runat="server" width="84%" style="display:none"></asp:TextBox>
                    <asp:TextBox ID="fld_TotalMealPE" value="0.00" runat="server" width="84%" style="display:none"></asp:TextBox>
                    </td>
                      <td >
                       <span id="spanTotalBME"></span>
                    <asp:TextBox ID="fld_TotalBME" value="0.00" runat="server" width="84%" style="display:none"></asp:TextBox>
                    <asp:TextBox ID="fld_TotalBMEPE" value="0.00" runat="server" width="84%" style="display:none"></asp:TextBox>
                    </td>
                      <td >
                       <span id="spanTotalAirfare"></span>
                    <asp:TextBox ID="fld_TotalAirfare" value="0.00" runat="server" width="84%" style="display:none"></asp:TextBox>
                    <asp:TextBox ID="fld_TotalAirfarePE" value="0.00" runat="server" width="84%" style="display:none"></asp:TextBox>
                    </td>
                      <td >
                       <span id="spanTotalHotel"></span>
                    <asp:TextBox ID="fld_TotalHotel" value="0.00" runat="server" width="84%" style="display:none"></asp:TextBox>
                    <asp:TextBox ID="fld_TotalHotelPE" value="0.00" runat="server" width="84%" style="display:none"></asp:TextBox>
                    
                    </td>
                      <td >
                       <span id="spanTotalTB"></span>
                    <asp:TextBox ID="fld_TotalTB" value="0.00" runat="server" width="84%" style="display:none"></asp:TextBox>
                    <asp:TextBox ID="fld_TotalTBPE" value="0.00" runat="server" width="84%" style="display:none"></asp:TextBox>
                    </td>
                      <td >
                       <span id="spanTotalLaundry"></span>
                    <asp:TextBox ID="fld_TotalLaundry" value="0.00" runat="server" width="84%" style="display:none"></asp:TextBox>
                    <asp:TextBox ID="fld_TotalLaundryPE" value="0.00" runat="server" width="84%" style="display:none"></asp:TextBox>
                    </td>
                      <td >
                      <span id="spanTotalTT"></span>
                    <asp:TextBox ID="fld_TotalTT" value="0.00" runat="server" width="84%" style="display:none"></asp:TextBox>
                    <asp:TextBox ID="fld_TotalTTPE" value="0.00" runat="server" width="84%" style="display:none"></asp:TextBox>
                    
                    </td>
                    <td >
                        <span id="spanTotalOthers"></span>
                    <asp:TextBox ID="fld_TotalOthers" value="0.00" runat="server" width="84%" style="display:none"></asp:TextBox>
                    <asp:TextBox ID="fld_TotalOthersPE" value="0.00" runat="server" width="84%" style="display:none"></asp:TextBox>
                    
                    </td>
                   
                    <td>
                    <span id="sapnTotalUSD"></span>
                        <asp:TextBox ID="fld_TotalUSD" value="0.00" runat="server" width="84%" style="display:none"></asp:TextBox>
                    </td>
                    <td>
                    <span id="spanTotalRMB"></span>
                        <asp:TextBox ID="fld_TotalRMB" value="0.00" runat="server" width="84%" style="display:none"></asp:TextBox>
                    </td>
                    <td></td>
                </tr>
                <tr>
                    <th></th>
                    <td colspan="11"><p style="float:right">Total(RMB)</p></td>
                     <td>
                     <span id="spanRMB"></span>
                        <asp:TextBox ID="fld_RMB" runat="server" value="0.00" width="84%" style="display:none"></asp:TextBox>
                    </td>
                    
                    <td></td>
                </tr>
                <tr>
                    <th></th>
                    <td colspan="11"><p style="float:right">Paid By Company(RMB)</p></td>
                    <%--<td> <asp:TextBox ID="fld_PaidByCompanyUSD" runat="server" value="0.00" width="84%"></asp:TextBox></td>--%>
                     <td>
                     <span id="spanPaidByCompanyRMB"> </span>
                        <asp:TextBox ID="fld_PaidByCompanyRMB" runat="server" value="0.00" width="84%" style="display:none"></asp:TextBox>
                    </td>
                    
                    <td></td>
                </tr>
                <tr>
                    <th></th>
                    <td colspan="11"><p style="float:right">Paid By Employee(RMB)</p></td>
                    <%--<td> <asp:TextBox ID="fld_PaidByEmployeeUSD" runat="server" value="0.00" width="84%"></asp:TextBox></td>--%>
                      <td>
                      <span id="spanPaidByEmployeeRMB"></span>
                                <asp:TextBox ID="fld_PaidByEmployeeRMB" runat="server" value="0.00" width="84%" style="display:none"></asp:TextBox>
                            </td>
                    <td></td>
                </tr>
                </tfoot>
            </table>
            </div>
            <div class="row">
                <%--<p style="font-weight:bold">phone calls made(list all overseas cal made)（"<span style=" background:red;">&nbsp;</span>" must write）</p>--%>
                <table class="table table-condensed table-bordered">
                    <tr>
                        <td class="td-label">
                        <span style="height:30px; float:left;">&nbsp;</span>
                            <p style="text-align:center">Travel Request NO.</p>
                        </td>
                        <td class="td-content" colspan="7">
                            <asp:TextBox runat="server" ID="fld_TravelRequestNo" Width="90%" style="background-color:White;" onfocus="this.blur()"></asp:TextBox>
                            <input type="button" value="..." class="btn" onclick="return TravelRequestNo_onclick(this)" />

                        </td>
                    </tr>
                </table>
                <table class="table table-condensed table-bordered">
                <tr>
                    <td class="td-label" style="vertical-align:middle"><p style="text-align:center">Remark</p></td>
                    <td class="td-content" colspan="3">
                        <asp:TextBox runat="server" ID="fld_Remark" width="98%" TextMode="MultiLine" Rows="3"></asp:TextBox>
                    </td>
                </tr>
                  
                </table>
            </div>
            <div class="row">
                <p style="font-weight:bold;">ENTERTAINMENT (if space is insufficient,please attach a separate entertainment report)</p>
                <table class="table table-condensed table-bordered">
                    <tr>
                        <th width="10%"><p style="text-align:center">Date</p></th>
                        <th width="23%"><p style="text-align:center">Amount</p></th>
                        <th width="36%"><p style="text-align:center">Guest Name & Company</p></th>
                        <th width="23%"><p style="text-align:center">Business Purpose</p></th>
                        <th width="10%"></th>
                    </tr>
                    <tbody id="ExpenseDetail1">
                    <asp:Repeater runat="server" ID="fld_detail_PROC_TravelExpenseThird_DT" OnItemCommand="fld_detail_PROC_TravelExpenseThird_DT_ItemCommand" OnItemDataBound="fld_detail_PROC_TravelExpenseThird_DT_ItemDataBound">
                        <ItemTemplate>
                        <tr>
                            <td>
                                <asp:TextBox runat="server" ID="fld_Date" onfocus="ShowTime(this,'1');" Text='<%# String.IsNullOrEmpty(Eval("Date").ToString())? "":DateTime.Parse(Eval("Date").ToString()).ToString("yyyy-MM-dd") %>' Width="84%" ></asp:TextBox>
                            </td>
                            <td>
                                <asp:TextBox runat="server" ID="fld_Amount" Text='<%#Eval("Amount") %>' Width="92%" ></asp:TextBox>
                            </td>
                            <td>
                                <asp:TextBox runat="server" ID="fld_GuestNameCompany" Text='<%#Eval("GuestNameCompany") %>' Width="96%" ></asp:TextBox>
                            </td>
                            <td>
                                <asp:TextBox runat="server" ID="fld_BusinessPurpose" Text='<%#Eval("BusinessPurpose") %>' Width="92%" ></asp:TextBox>
                            </td>
                            <td>
                                <asp:Button ID="btnDelete" runat="server" Text="delete" CssClass="btn" CommandName="del" ClientIDMode="Static" OnClientClick="return confirm('Confirm Del？')" />
                            </td>
                        </tr>
                        </ItemTemplate>
                     </asp:Repeater>
                     </tbody>
                     <tr>
                        <td colspan="9">
                            <asp:Button ID="btnAddThird" runat="server" Text="add" CssClass="btn" CausesValidation="false" OnClick="btnAddThird_Click"   />
                        </td>
                     </tr>
                </table>
                <table class="table table-condensed table-bordered">
                     <tr>
                        <th colspan="5"><p style="float:left;font-weight:bold;">Special Case:</p></th>
                     </tr>
                     <tr>
                        <th width="23%"><p style="text-align:center">Item</p></th>
                        <th width="23%"><p style="text-align:center">Date</p></th>
                        <th width="23%"><p style="text-align:center">Amount</p></th>
                        <th width="23%"><p style="text-align:center">Reason</p></th>
                        <th width="10%"></th>
                    </tr>
                     <asp:Repeater runat="server" ID="fld_detail_PROC_TravelExpenseForth_DT" OnItemCommand="fld_detail_PROC_TravelExpenseForth_DT_ItemCommand" OnItemDataBound="fld_detail_PROC_TravelExpenseForth_DT_ItemDataBound">
                        <ItemTemplate>
                        <tr>
                            <td>
                                <asp:TextBox runat="server" ID="fld_Itme" Text='<%#Eval("Itme") %>' width="92%"></asp:TextBox>
                            </td>
                            <td>
                                <asp:TextBox runat="server" ID="fld_Date" onfocus="ShowTime(this,'1');" Text='<%# String.IsNullOrEmpty(Eval("Date").ToString())? "":DateTime.Parse(Eval("Date").ToString()).ToString("yyyy-MM-dd") %>' width="92%"></asp:TextBox>
                            </td>
                            <td>
                                <asp:TextBox runat="server" ID="fld_Amount" Text='<%#Eval("Amount") %>' width="92%"></asp:TextBox>
                            </td>
                            <td>
                                <asp:TextBox runat="server" ID="fld_Reason" Text='<%#Eval("Reason") %>' width="92%"></asp:TextBox>
                            </td>
                            <td>
                                <asp:Button ID="btnDelete" runat="server" Text="delete" CssClass="btn" CommandName="del" ClientIDMode="Static" OnClientClick="return confirm('Confirm Del？')" />
                            </td>
                        </tr>
                        </ItemTemplate>
                     </asp:Repeater>
                     <tr>
                        <td colspan="9">
                            <asp:Button ID="btnAddForth" runat="server" Text="add" CssClass="btn" CausesValidation="false" OnClick="btnAddForth_Click"   />
                        </td>
                    </tr>
                </table>
            </div>
            <div class="row">
                <attach:attachments id="Attachments1" runat="server"></attach:attachments>
            </div>
            <div class="row">
                <ah:approvalhistory id="ApprovalHistory1" showaction="true" runat="server"></ah:approvalhistory>
            </div>
           <div id="ReturnBackTask" style="display:none; text-align:center">
        <asp:Button runat="server" ID="btnRevoke"  Visible="false" CssClass="btn" Text="Revoke" 
                onclick="btnRevoke_Click" />
        </div>
        </div>
        <input type="button" value="Print" id="btnPrint" style="display:none" />
        <asp:TextBox runat="server" ID="fld_USERPOST" style="display:none;"></asp:TextBox>
        <btn:buttonlist id="ButtonList1" runat="server"></btn:buttonlist>
        <div style="display: none;">
            <asp:TextBox runat="server" ID="fld_TRSummary"></asp:TextBox>
            <asp:HiddenField runat="server" ID="hdIncident" />
         <asp:HiddenField runat="server" ID="hdUrgeTask" />
            <asp:HiddenField runat="server" ID="hdPrint"  Value="0" />
        </div>
    </form>
</body>
</html>
