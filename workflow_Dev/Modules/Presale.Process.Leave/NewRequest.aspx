<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="NewRequest.aspx.cs" Inherits="Presale.Process.Leave.NewRequest" %>
<%@ Register Src="/Modules/Ultimus.UWF.Form.ProcessControl/UserInfo.ascx" TagName="UserInfo" TagPrefix="ui" %>
<%@ Register Src="/Modules/Ultimus.UWF.Form.ProcessControl/ApprovalHistory.ascx" TagName="ApprovalHistory" TagPrefix="ah" %>
<%@ Register Src="/Modules/Ultimus.UWF.Form.ProcessControl/Attachments.ascx" TagName="Attachments" TagPrefix="attach" %>
<%@ Register Src="/Modules/Ultimus.UWF.Form.ProcessControl/ButtonList.ascx" TagName="ButtonList" TagPrefix="btn" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta http-equiv="X-UA-Compatible"  content="IE=EmulateIE8" />
    <title>Leave Application Process</title>
    <script type="text/javascript" src="/Assets/js/common.js"></script>
    <script type="text/javascript">
        function beforeSubmit() {
            var Msg = "";
            $("#leavedetails").find("tr").each(function (i, Etr) {
                $(Etr).find(".StartHours").next().val($(Etr).find(".StartHours option:selected").text());
                $(Etr).find(".StartMinutes").next().val($(Etr).find(".StartMinutes option:selected").text());
                $(Etr).find(".EndHours").next().val($(Etr).find(".EndHours option:selected").text());
                $(Etr).find(".EndMinutes").next().val($(Etr).find(".EndMinutes option:selected").text());
            });
            if ($("#text_FuallpaySick").val() - $("#text_everySickday").val() < 0) {
                //  alert("Your remaining Full Pay Sick leave is less than the input of the holiday.Please input again");
                Msg = "Your remaining Full Pay Sick leave is less than the input of the holiday.Please input again";
                //return false;
            }
            var leavedays = 0.00;
            $("#leavedetails").find("tr").each(function (i, Etr) {
                leavedays = $(Etr).find("td").eq(4).children().val() - 0;
                //                if (!((leavedays * 100) % 25)) {
                if (!((leavedays) % 0.125)) {

                }

                else {
                    //alert("Your Applying time is wrong.Please input again!");
                    Msg = "Your Applying time is wrong.Please input again!";
                    $(Etr).find("td").eq(4).children().val("");
                }
                if ($(Etr).find("td").eq(1).find(".DropApplying").find("option:selected").text() == "Annual Leave") {
                    if (leavedays <= 0) {
                        Msg = "Your Applying time is wrong.Please input again!";
                    }
                }

            });
            $("#leavedetails").find("tr").each(function (i, Etr) {
                var EvySickday = 0.00;
                var FullPaySickday = $("#text_FuallpaySick").val() - 0;
                if ($(Etr).find("td").eq(1).find(".DropApplying").find("option:selected").text() == "Annual Leave") {
                    $("#fld_nowAnnualLeave").val($(Etr).find("td").eq(4).children().val());
                }
                if ($(Etr).find("td").eq(1).find(".DropApplying").find("option:selected").text() == "Full Pay Sick Leave") {
                    EvySickday += $(Etr).find("td").eq(4).children().val() - 0;
                    if (EvySickday > FullPaySickday) {
                        // alert("Your remaining Full Pay Sick leave is less than the input of the holiday.Please input again");
                        Msg = "Your remaining Full Pay Sick leave is less than the input of the holiday.Please input again";
                        $(Etr).find("td").eq(4).children().val("");
                       // return false;
                    }
                }

            });
            var hours =0;
            $("#leavedetails").find("tr").each(function (i, Etr) {
                hours += (($(Etr).find("td").eq(4).children().val() == "" ? 0 : $(Etr).find("td").eq(4).children().val()) - 0);
            });
            if (Msg == "") {
                $("#fld_sumLeave").val(hours);
                var summary = "Leave Application";
                $("#UserInfo1_fld_PROCESSSUMMARY").val(summary);
                $("body").find(".nodays").removeAttr("disabled");
                return true;
            }
            else {
                alert(Msg);
                return false;
            }
        }
        
        function beforeSave() {
            var summary = "Leave Application";
            $("#UserInfo1_fld_PROCESSSUMMARY").val(summary);
            $("#fld_TRSummary").val(summary);
            $("body").find(".nodays").removeAttr("disabled");
            return true;
        }
        $(document).ready(function () {
            $("#fld_nowAnnualLeave").val(0);
            $("#leavedetails").find("tr").each(function (i, Etr) {
                $(Etr).find("td").eq(1).find(".DropApplying").val($(Etr).find("td").eq(1).find(".DropApplying").next().val());
                $(Etr).find("td").eq(2).find(".StartHours").val($(Etr).find("td").eq(2).find(".StartHours").next().val());
                $(Etr).find("td").eq(2).find(".StartMinutes").val($(Etr).find("td").eq(2).find(".StartMinutes").next().val());
                $(Etr).find("td").eq(3).find(".EndHours").val($(Etr).find("td").eq(3).find(".EndHours").next().val());
                $(Etr).find("td").eq(3).find(".EndMinutes").val($(Etr).find("td").eq(3).find(".EndMinutes").next().val());

                if ($(Etr).find("td").eq(1).find(".DropApplying").next().val() == "Annual Leave") {
                    $(Etr).find(".txthours").show();
                    var nodasy = $(Etr).find(".nodays").val();
                    $(Etr).find(".txthours:eq(0)").val((nodasy * 8).toFixed(2));

                    $(Etr).find(".txthours:eq(1)").next().attr("disabled", true);
                }
                else {
                    $(Etr).find(".txthours").hide();
                    $(Etr).find(".txthours:eq(1)").next().removeAttr("disabled");
                }

            });
            if ($("#hdIncident").val() != "") {
                $("#ButtonList1_btnSubmit").val("Submit");
                $("#ButtonList1_btnBack").hide();
                $("#ButtonList1_btnReject").show();
            }
            if ($("#hdUrgeTask").val() == "Yes") {
                $("#ReturnBackTask").show();
            }
        });
        function clearedisabled() {
           $("body").find(".nodays").removeAttr("disabled");
       }
       function deleteclearedisabled() {
           if (confirm("Confirm Del ?")) {
               $("body").find(".nodays").removeAttr("disabled");
               return true;
           }
           else
           { 
           return false;
       }
       }
        function getItemChecked(obj) {
            $(obj).next().val($(obj).find("option:selected").text());
        }
        function changeOption(obj) {
            var times = $(obj).val();
            $(obj).next().val(times);
            cacluateDate(obj);
        }
        function getHours(startDate, startHours, startMinutes, endDate, endHours, endMinutes) {
            var date1 = new Date(startDate.replace(/-/g,"/"));
            date1.setHours(parseInt(startHours));
            date1.setMinutes(parseInt(startMinutes));
            var date2 = new Date(endDate.replace(/-/g, "/"));
            date2.setHours(parseInt(endHours));
            date2.setMinutes(parseInt(endMinutes));
            var time1 = date1.getTime();
            var time2 = date2.getTime();
            return ((time2 - time1) / (1000 * 60 * 60)).toFixed(2);
        }
        function changestart(obj) {
            var startdate = $(obj).parent().parent().find("td:eq(2)").find("input").val();
            
            var starthours = $(obj).parent().find(".StartHours").val();
            var startminutes = $(obj).parent().find(".StartMinutes").val();
            var enddate = $(obj).parent().parent().find("td:eq(3)").find("input").val();
            var endhours = $(obj).parent().next().find(".EndHours").val();
            var endminutes = $(obj).parent().next().find(".EndMinutes").val();
            var hours = getHours(startdate, starthours, startminutes, enddate, endhours, endminutes) == "NaN" ? "" : getHours(startdate, starthours, startminutes, enddate, endhours, endminutes);
            $(obj).parent().next().next().children().val(hours);
            if (hours < 0) {
                alert("The holiday you entered is less than zero.Please input again");
                return false;
            }
            if ($(obj).parent().parent().find("td:eq(1)").find(".DropApplying").val() == "Annual Leave") {
                $("#fld_nowAnnualLeave").val(hours);
                if (parseFloat($("#fld_sumAnnualLeave").val()) - parseFloat(hourse) < 0) {
                    alert("Your remaining annual leave is less than the input of the holiday.Please input again");
                    return false;
                }
            }
            if ($(obj).parent().parent().find("td:eq(1)").find(".DropApplying").val() == "Full Pay Sick Leave") {
                $("#text_everySickday").val(hours);
                if (parseFloat($("#fld_FuallpaySick").val()) - parseFloat(hourse) < 0) {
                    alert("Your remaining full pay sick leave is less than the input of the holiday.Please input again");
                    return false;
                }
            }
        }
        function changeend(obj) {
            var startdate = $(obj).parent().parent().find("td:eq(2)").find("input").val();
            var starthours = $(obj).parent().prev().find(".StartHours").val();
            var startminutes = $(obj).parent().prev().find(".StartMinutes").val();
            var enddate = $(obj).parent().parent().find("td:eq(3)").find("input").val();
            var endhours = $(obj).parent().find(".EndHours").val();
            var endminutes = $(obj).parent().find(".EndMinutes").val();
            var hours = getHours(startdate, starthours, startminutes, enddate, endhours, endminutes) == "NaN" ? "" : getHours(startdate, starthours, startminutes, enddate, endhours, endminutes);
            $(obj).parent().next().children().val(hours);
            if (hours < 0) {
                alert("The holiday you entered is less than zero.Please input again");
                return false;
            }
            if ($(obj).parent().parent().find("td:eq(1)").find(".DropApplying").val() == "Annual Leave") {
                $("#fld_nowAnnualLeave").val(hours);
                if (parseFloat($("#fld_sumAnnualLeave").val()) - parseFloat(hourse) < 0) {
                    alert("Your remaining annual leave is less than the input of the holiday.Please input again");
                    $(obj).parent().next().children().val("");
                    return false;
                }
            }
        }
        function checktype(obj) {//判断年假 事假 病假
            var ALlastday = $("#fld_nowAnnualLeave").val() - 0;
            var FullSick = $("#text_FuallpaySick").val() - 0;
            $("#leavedetails").find("tr").each(function (i, Etr) {
                var style = $(Etr).find("td:eq(1)").children().val();
                if (style == "Personal Leave") {
                    if (ALlastday != "0") {
                        alert("Your annual leave is not zero!Please input again");
                        $(Etr).find("td:eq(3)").children().val("");
                        return false;
                    }
                }
                if (style == "Sick Leave") {
                    if (FullSick != "0") {
                        if (ALlastday != "0") {
                            alert("Your Full Pay Sick leave is not zero!Please input again");
                            $(Etr).find("td:eq(3)").children().val("");
                            return false;
                        }
                    }
                }
            });

            var LeaveType = $(obj).parent().parent().find(".DropApplying").find("option:selected").val();
            
            if (LeaveType == "Annual Leave") {
              
                $(obj).parent().parent().find(".txthours").show();
                $(obj).parent().parent().find(".txthours:eq(1)").next().attr("disabled", true);
                CheckEndDate(obj);
            }
            else {
                $(obj).parent().parent().find(".txthours").hide();
                $(obj).parent().parent().find(".txthours:eq(1)").next().removeAttr("disabled");
            }
        }
        function NoODays_onblur(obj) {

            if ($(obj).parent().parent().find("td:eq(1)").find(".DropApplying").val() == "Annual Leave") {

                $("#fld_nowAnnualLeave").val($(obj).val());
                var time = $("#fld_sumAnnualLeave").val;
                if (parseFloat($("#fld_sumAnnualLeave").val()) - parseFloat($(obj).val()) < 0) {
                    var str = "Your remaining annual leave is less than the input of the holiday.Please input again";
                    alert(str);
                    $(obj).val("");
                    return false;
                }
            }
        }
        function checkleavetype(obj) {
            if ($(obj).parent().parent().find("td:eq(1)").find(".DropApplying").val() == "Annual Leave") {
                $(obj).blur();
            }        
        }

        function CheckEndDate(obj) {
//            var myDate = new Date;
//            var Cyear = myDate.getFullYear(); //获取当前年
            var EndDate = $(obj).parent().parent().parent().find('.endDate').val();
            var StartDate = $(obj).parent().parent().parent().find('.startDate').val();
           // var myEndDate = new Date(EndDate);
       
            if (EndDate != "" && StartDate != "" && StartDate!=undefined&&EndDate!=undefined) {
                var reg = new RegExp('-', "g")
                var EndDateValue = EndDate.replace(reg, '/');
                var StartDateValue = StartDate.replace(reg, '/');
                var EDate = new Date(EndDateValue);
                var SDate = new Date(StartDateValue);
                var Emonth = EDate.getMonth()+1;
                var Smonth = SDate.getMonth() + 1;
//                console.log("结束月份" + Emonth);
//                console.log("开始月份" + Smonth);
                var LeaveType = $(obj).parent().parent().parent().find(".DropApplying").find("option:selected").val();
                  
                if (LeaveType == "Annual Leave" && (Emonth == 4 && Smonth == 3)) {
                    alert("Date invalid,Please re-select");
                    $(obj).parent().parent().parent().find('.endDate').val("");
                    $(obj).parent().parent().parent().find('.startDate').val("");
                }
                else {
                    if (LeaveType == "Annual Leave") {
                        cacluateDate(obj);
                    }
                }
            }


        }

        function cacluateDate(obj) {
               if($(obj).html()=="") {
               obj=$(obj).parent();
               }
            var LeaveType = $(obj).parent().parent().find(".DropApplying").find("option:selected").val();
           
            if (LeaveType == "Annual Leave") {
                var StartDate = $(obj).parent().parent().find(".startDate").val();
                var EndDate = $(obj).parent().parent().find(".endDate").val();
                var Starthour = $(obj).parent().parent().find(".StartHours").val();
                var Endhour = $(obj).parent().parent().find(".EndHours").val();
                var Startminutes = $(obj).parent().parent().find(".StartMinutes").val();
                var Endtminutes = $(obj).parent().parent().find(".EndMinutes").val();
                if (StartDate != "" && EndDate != "") {
                    $.ajax({ url: 'checkSumAnnualLeave.ashx',
                        data: { "StartDate": StartDate, "EndDate": EndDate, "Starthour": Starthour, "Endhour": Endhour, "Startminutes": Startminutes, "Endtminutes": Endtminutes },
                        type: 'POST',
                        success: function (value) {
                        value=-1;
                            $("#fld_nowAnnualLeave").val(value);
                            if (parseFloat($("#fld_sumAnnualLeave").val()) - parseFloat(value) < 0) {
                                var str = "Your remaining annual leave is less than the input of the holiday.Please input again";
                                alert(str);
                                $(obj).val("");
                            }
                            else {
                                $(obj).parent().parent().find(".nodays").val(value);
                                $(obj).parent().parent().find(".txthours:eq(0)").val((value * 8).toFixed(2));
                            }
                        }
                    });
                }
                else {
                    $(obj).parent().parent().find(".nodays").val(0);
                    $(obj).parent().parent().find(".txthours:eq(0)").val(0);
                }

            }
        }

    </script>
</head>
<body>
    <form id="form1" runat="server">
     <div id="myDiv" class="container">
            <div class="row">
                <ui:userinfo id="UserInfo1" processtitle="Leave Application Process" processprefix="HRLE" tablename="PROC_Leave" tablenamedetail="PROC_Leave_DT"
                    runat="server"  ></ui:userinfo>
               <asp:TextBox runat="server" ID="fld_sumAnnualLeave" style="display:none;"></asp:TextBox>
                <asp:TextBox runat="server" ID="fld_nowAnnualLeave" style="display:none;"></asp:TextBox>
                <asp:TextBox runat="server" ID="fld_applierLogin" style="display:none;" ></asp:TextBox>
                <asp:TextBox runat="server" ID="fld_sumLeave" style="display:none;"></asp:TextBox>
                <asp:TextBox runat="server" ID="text_FuallpaySick" style="display:none" ></asp:TextBox>
                <asp:TextBox runat="server" ID="text_everySickday" style="display:none" ></asp:TextBox>
                <asp:TextBox runat="server" ID="fld_DGMLogin" style="display:none;"></asp:TextBox>
                <asp:TextBox runat="server" ID="fld_HRLogin" style="display:none;"></asp:TextBox>
                <asp:TextBox runat="server" ID="fld_DGM" style="display:none;"></asp:TextBox>
                <asp:TextBox runat="server" ID="fld_HR" style="display:none;"></asp:TextBox>
            </div>
            <div class="row">
                <p style="font-weight:bold;">Request require（"<span style=" background:red;">&nbsp;</span>" must write） </p>
                <p style="color:red;">
                “The minimum unit of annual leave consumed is 1 hour, 1 day annual leave=8 hours 年休假的最小计算单位为1小时。（一  天年假换算为8小时）”
                </p>
                
                <table class="table table-condensed table-bordered">
                    <tr>
                        <th width="22%"> 
                        <p style="text-align:center">Applying for</p>
                        </th>
                        <th width="22%">
                        <p style="text-align:center">From</p>
                        </th>
                        <th width="22%">
                        <p style="text-align:center">To</p>
                        </th>
                        <th width="22%">
                        <p style="text-align:center">Number of absence</p>
                        </th>
                        <th width="12%"></th>
                    </tr>
                    <tbody id="leavedetails">
                    <asp:Repeater runat="server" ID="fld_detail_PROC_Leave_DT" OnItemCommand="fld_detail_PROC_Leave_DT_ItemCommand" OnItemDataBound="fld_detail_PROC_Leave_DT_ItemDataBound" >
                        <ItemTemplate>
                            <tr>
                                <td style="display:none">
                                    <span style=" background:red;">&nbsp;</span>
                                    <asp:TextBox ID="fld_FORMID" Text='<%#Eval("FORMID") %>' runat="server" Style="display: none"></asp:TextBox>
                                    <asp:Label ID="fld_ROWID" Text='<%# Container.ItemIndex+1%>' runat="server"></asp:Label>
                                    <asp:TextBox runat="server" ID="fld_Usethisyear" Text='<%# Eval("Usethisyear")%>' style="display:none" ></asp:TextBox>
                                    <asp:TextBox runat="server" ID="fld_Uselastyear" Text='<%# Eval("Uselastyear") %>' style="display:none" ></asp:TextBox>
                                </td>
                                <td>
                                    <asp:DropDownList ID="dropApplying" runat="server" class="DropApplying" onchange="checktype(this)" onclick="getItemChecked(this);"></asp:DropDownList>
                                    <asp:TextBox runat="server" ID="fld_Applying" Text='<%#Eval("Applying") %>' style="display:none;"></asp:TextBox>
                                </td>
                                <td>
                                    <span><asp:TextBox runat="server" ID="fld_StartDate" Text='<%# String.IsNullOrEmpty(Eval("StartDate").ToString())? "":DateTime.Parse(Eval("StartDate").ToString()).ToString("yyyy-MM-dd") %>' CssClass="validate[required] startDate" onclick="WdatePicker({readOnly:false,dateFmt: 'yyyy-MM-dd'})" onchange="CheckEndDate(this)" Width="35%"></asp:TextBox></span>
                                    <asp:DropDownList runat="server" class="StartHours" Width="25%" ID="starthours" onchange="changeOption(this)">
                                        <asp:ListItem Selected="True">00</asp:ListItem>
                                        <asp:ListItem>01</asp:ListItem>
                                        <asp:ListItem>02</asp:ListItem>
                                        <asp:ListItem>03</asp:ListItem>
                                        <asp:ListItem>04</asp:ListItem>
                                        <asp:ListItem>05</asp:ListItem>
                                        <asp:ListItem>06</asp:ListItem>
                                        <asp:ListItem>07</asp:ListItem>
                                        <asp:ListItem>08</asp:ListItem>
                                        <asp:ListItem>09</asp:ListItem>
                                        <asp:ListItem>10</asp:ListItem>
                                        <asp:ListItem>11</asp:ListItem>
                                        <asp:ListItem>12</asp:ListItem>
                                        <asp:ListItem>13</asp:ListItem>
                                        <asp:ListItem>14</asp:ListItem>
                                        <asp:ListItem>15</asp:ListItem>
                                        <asp:ListItem>16</asp:ListItem>
                                        <asp:ListItem>17</asp:ListItem>
                                        <asp:ListItem>18</asp:ListItem>
                                        <asp:ListItem>19</asp:ListItem>
                                        <asp:ListItem>20</asp:ListItem>
                                        <asp:ListItem>21</asp:ListItem>
                                        <asp:ListItem>22</asp:ListItem>
                                        <asp:ListItem>23</asp:ListItem>
                                    </asp:DropDownList>:
                                    <asp:TextBox runat="server" ID="fld_StartHours" Text='<%# String.IsNullOrEmpty(Eval("StartHours").ToString())? "":Eval("StartHours")%>' style="display:none;"></asp:TextBox>
                                    <asp:DropDownList runat="server" class="StartMinutes" Width="25%" ID="startminutes" onchange="changeOption(this)">
                                        <asp:ListItem Selected="True">00</asp:ListItem> 
                                        <%--<asp:ListItem>01</asp:ListItem>
                                        <asp:ListItem>02</asp:ListItem>
                                        <asp:ListItem>03</asp:ListItem>
                                        <asp:ListItem>04</asp:ListItem>
                                        <asp:ListItem>05</asp:ListItem>
                                        <asp:ListItem>06</asp:ListItem>
                                        <asp:ListItem>07</asp:ListItem>
                                        <asp:ListItem>08</asp:ListItem>
                                        <asp:ListItem>09</asp:ListItem>
                                        <asp:ListItem>10</asp:ListItem>
                                        <asp:ListItem>11</asp:ListItem>
                                        <asp:ListItem>12</asp:ListItem>
                                        <asp:ListItem>13</asp:ListItem>
                                        <asp:ListItem>14</asp:ListItem>--%>
                                        <asp:ListItem>15</asp:ListItem>
                                        <%--<asp:ListItem>16</asp:ListItem>
                                        <asp:ListItem>17</asp:ListItem>
                                        <asp:ListItem>18</asp:ListItem>
                                        <asp:ListItem>19</asp:ListItem>
                                        <asp:ListItem>20</asp:ListItem>
                                        <asp:ListItem>21</asp:ListItem>
                                        <asp:ListItem>22</asp:ListItem>
                                        <asp:ListItem>23</asp:ListItem>
                                        <asp:ListItem>24</asp:ListItem>
                                        <asp:ListItem>25</asp:ListItem>
                                        <asp:ListItem>26</asp:ListItem>
                                        <asp:ListItem>27</asp:ListItem>
                                        <asp:ListItem>28</asp:ListItem>
                                        <asp:ListItem>29</asp:ListItem>--%>
                                        <asp:ListItem>30</asp:ListItem>
                                        <%--<asp:ListItem>31</asp:ListItem>
                                        <asp:ListItem>32</asp:ListItem>
                                        <asp:ListItem>33</asp:ListItem>
                                        <asp:ListItem>34</asp:ListItem>
                                        <asp:ListItem>35</asp:ListItem>
                                        <asp:ListItem>36</asp:ListItem>
                                        <asp:ListItem>37</asp:ListItem>
                                        <asp:ListItem>38</asp:ListItem>
                                        <asp:ListItem>39</asp:ListItem>
                                        <asp:ListItem>40</asp:ListItem>
                                        <asp:ListItem>41</asp:ListItem>
                                        <asp:ListItem>42</asp:ListItem>
                                        <asp:ListItem>43</asp:ListItem>
                                        <asp:ListItem>44</asp:ListItem>--%>
                                        <asp:ListItem>45</asp:ListItem>
                                        <%--<asp:ListItem>46</asp:ListItem>
                                        <asp:ListItem>47</asp:ListItem>
                                        <asp:ListItem>48</asp:ListItem>
                                        <asp:ListItem>49</asp:ListItem>
                                        <asp:ListItem>50</asp:ListItem>
                                        <asp:ListItem>51</asp:ListItem>
                                        <asp:ListItem>52</asp:ListItem>
                                        <asp:ListItem>53</asp:ListItem>
                                        <asp:ListItem>54</asp:ListItem>
                                        <asp:ListItem>55</asp:ListItem>
                                        <asp:ListItem>56</asp:ListItem>
                                        <asp:ListItem>57</asp:ListItem>
                                        <asp:ListItem>58</asp:ListItem>
                                        <asp:ListItem>59</asp:ListItem>--%>
                                    </asp:DropDownList>
                                    <asp:TextBox runat="server" ID="fld_StartMinutes" Text='<%# String.IsNullOrEmpty(Eval("StartMinutes").ToString())? "":Eval("StartMinutes")%>' style="display:none;"></asp:TextBox>
                                </td>
                                <td >
                                    <span><asp:TextBox runat="server" ID="fld_EndDate"  Text='<%# String.IsNullOrEmpty(Eval("EndDate").ToString())? "":DateTime.Parse(Eval("EndDate").ToString()).ToString("yyyy-MM-dd") %>' CssClass="validate[required] endDate" onclick="WdatePicker({readOnly:false,dateFmt: 'yyyy-MM-dd'})" onchange="CheckEndDate(this)" Width="35%"></asp:TextBox></span>
                                    <asp:DropDownList runat="server" class="EndHours" Width="25%" ID="endhours" onchange="changeOption(this)">
                                        <asp:ListItem Selected="True">00</asp:ListItem>
                                        <asp:ListItem>01</asp:ListItem>
                                        <asp:ListItem>02</asp:ListItem>
                                        <asp:ListItem>03</asp:ListItem>
                                        <asp:ListItem>04</asp:ListItem>
                                        <asp:ListItem>05</asp:ListItem>
                                        <asp:ListItem>06</asp:ListItem>
                                        <asp:ListItem>07</asp:ListItem>
                                        <asp:ListItem>08</asp:ListItem>
                                        <asp:ListItem>09</asp:ListItem>
                                        <asp:ListItem>10</asp:ListItem>
                                        <asp:ListItem>11</asp:ListItem>
                                        <asp:ListItem>12</asp:ListItem>
                                        <asp:ListItem>13</asp:ListItem>
                                        <asp:ListItem>14</asp:ListItem>
                                        <asp:ListItem>15</asp:ListItem>
                                        <asp:ListItem>16</asp:ListItem>
                                        <asp:ListItem>17</asp:ListItem>
                                        <asp:ListItem>18</asp:ListItem>
                                        <asp:ListItem>19</asp:ListItem>
                                        <asp:ListItem>20</asp:ListItem>
                                        <asp:ListItem>21</asp:ListItem>
                                        <asp:ListItem>22</asp:ListItem>
                                        <asp:ListItem>23</asp:ListItem>
                                    </asp:DropDownList>:
                                    <asp:TextBox runat="server" ID="fld_EndHours" Text='<%# String.IsNullOrEmpty(Eval("EndHours").ToString())? "":Eval("EndHours")%>' style="display:none;"></asp:TextBox>
                                    <asp:DropDownList runat="server" class="EndMinutes" Width="25%" ID="endminutes" onchange="changeOption(this)">
                                        <asp:ListItem Selected="True">00</asp:ListItem>
                                        <%--<asp:ListItem>01</asp:ListItem>
                                        <asp:ListItem>02</asp:ListItem>
                                        <asp:ListItem>03</asp:ListItem>
                                        <asp:ListItem>04</asp:ListItem>
                                        <asp:ListItem>05</asp:ListItem>
                                        <asp:ListItem>06</asp:ListItem>
                                        <asp:ListItem>07</asp:ListItem>
                                        <asp:ListItem>08</asp:ListItem>
                                        <asp:ListItem>09</asp:ListItem>
                                        <asp:ListItem>10</asp:ListItem>
                                        <asp:ListItem>11</asp:ListItem>
                                        <asp:ListItem>12</asp:ListItem>
                                        <asp:ListItem>13</asp:ListItem>
                                        <asp:ListItem>14</asp:ListItem>--%>
                                        <asp:ListItem>15</asp:ListItem>
                                        <%--<asp:ListItem>16</asp:ListItem>
                                        <asp:ListItem>17</asp:ListItem>
                                        <asp:ListItem>18</asp:ListItem>
                                        <asp:ListItem>19</asp:ListItem>
                                        <asp:ListItem>20</asp:ListItem>
                                        <asp:ListItem>21</asp:ListItem>
                                        <asp:ListItem>22</asp:ListItem>
                                        <asp:ListItem>23</asp:ListItem>
                                        <asp:ListItem>24</asp:ListItem>
                                        <asp:ListItem>25</asp:ListItem>
                                        <asp:ListItem>26</asp:ListItem>
                                        <asp:ListItem>27</asp:ListItem>
                                        <asp:ListItem>28</asp:ListItem>
                                        <asp:ListItem>29</asp:ListItem>--%>
                                        <asp:ListItem>30</asp:ListItem>
                                        <%--<asp:ListItem>31</asp:ListItem>
                                        <asp:ListItem>32</asp:ListItem>
                                        <asp:ListItem>33</asp:ListItem>
                                        <asp:ListItem>34</asp:ListItem>
                                        <asp:ListItem>35</asp:ListItem>
                                        <asp:ListItem>36</asp:ListItem>
                                        <asp:ListItem>37</asp:ListItem>
                                        <asp:ListItem>38</asp:ListItem>
                                        <asp:ListItem>39</asp:ListItem>
                                        <asp:ListItem>40</asp:ListItem>
                                        <asp:ListItem>41</asp:ListItem>
                                        <asp:ListItem>42</asp:ListItem>
                                        <asp:ListItem>43</asp:ListItem>
                                        <asp:ListItem>44</asp:ListItem>--%>
                                        <asp:ListItem>45</asp:ListItem>
                                        <%--<asp:ListItem>46</asp:ListItem>
                                        <asp:ListItem>47</asp:ListItem>
                                        <asp:ListItem>48</asp:ListItem>
                                        <asp:ListItem>49</asp:ListItem>
                                        <asp:ListItem>50</asp:ListItem>
                                        <asp:ListItem>51</asp:ListItem>
                                        <asp:ListItem>52</asp:ListItem>
                                        <asp:ListItem>53</asp:ListItem>
                                        <asp:ListItem>54</asp:ListItem>
                                        <asp:ListItem>55</asp:ListItem>
                                        <asp:ListItem>56</asp:ListItem>
                                        <asp:ListItem>57</asp:ListItem>
                                        <asp:ListItem>58</asp:ListItem>
                                        <asp:ListItem>59</asp:ListItem>--%>
                                    </asp:DropDownList>
                                    <asp:TextBox runat="server" ID="fld_EndMinutes" Text='<%# String.IsNullOrEmpty(Eval("EndMinutes").ToString())? "":Eval("EndMinutes")%>' style="display:none;"></asp:TextBox>
                                </td>
                                <td>
                                    <input type="text" value="" class="txthours" style="display:none;width:50%;" disabled="disabled" />
                                    <span class="txthours" style="display:none">Hours</span>
                                    <asp:TextBox runat="server" ID="fld_NoODays" Text='<%#Eval("NoODays") %>' onfocus="checkleavetype(this)" onblur="NoODays_onblur(this)" CssClass="validate[required] nodays" Width="50%"></asp:TextBox>
                                    <span class="txthours" style="display:none">Days</span>

                                    <%--<p style="float:right">&nbsp;&nbsp;&nbsp;&nbsp;</p>--%>
                                    <%--<input type="checkbox" runat="server"  id="cb_SelectValue"  value='<%# Container.ItemIndex+1%>' style="float:right" />
                                    <asp:TextBox ID="fld_FORMID" Text='<%#Eval("FORMID") %>' runat="server" Style="display: none"></asp:TextBox>
                                    <asp:Label ID="fld_ROWID" Text='<%# Container.ItemIndex+1%>' runat="server" style="display:none;"></asp:Label>--%>
                                </td>
                                 <td style="text-align:center">
                                    <asp:Button ID="btnDelete" runat="server" Text="delete" CssClass="btn" CommandName="del" ClientIDMode="Static" OnClientClick="return deleteclearedisabled()" />
                                </td>
                            </tr>
                        </ItemTemplate>
                    </asp:Repeater>
                    </tbody>
                    <tr>
                        <td colspan="5">
                            <asp:Button runat="server" ID="btnAdd" Text="add" CssClass="btn"  CausesValidation="false" OnClientClick="clearedisabled()" OnClick="btnAdd_Click"/>
                        </td>
                    </tr>
                </table>
                   <%-- <asp:Button runat="server" ID="btnDel" Text="delete" CssClass="btn" CausesValidation="false" OnClick="btnDel_Click" />--%>
                <table class="table table-condensed table-bordered">
                    <tr>
                        <td class="td-label" style="vertical-align:middle">
                            <p style="text-align:center">Remark</p>
                        </td>
                        <td class="td-content" colspan="4">
                            <asp:TextBox runat="server" Rows="3" ID="fld_remark" TextMode="MultiLine" Width="98%"></asp:TextBox>
                        </td>
                    </tr>
                </table>
            </div>
            <div class="row" style="display:none;">
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
        <btn:buttonlist id="ButtonList1" runat="server"></btn:buttonlist>
        <div style="display:none">
        <asp:TextBox runat="server" ID="fld_TRSummary"></asp:TextBox>
        <asp:HiddenField runat="server" ID="hdIncident" />
        <asp:HiddenField runat="server" ID="hdPrint" />
        <asp:HiddenField runat="server" ID="hdUrgeTask" />
        </div>
    </form>
</body>
</html>


