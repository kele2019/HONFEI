<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="NewRequest.aspx.cs" Inherits="Presale.Process.DayOffRecord.NewRequest" %>

<%@ Register Src="/Modules/Ultimus.UWF.Form.ProcessControl/UserInfo.ascx" TagName="UserInfo" TagPrefix="ui" %>
<%@ Register Src="/Modules/Ultimus.UWF.Form.ProcessControl/ApprovalHistory.ascx" TagName="ApprovalHistory" TagPrefix="ah" %>
<%@ Register Src="/Modules/Ultimus.UWF.Form.ProcessControl/Attachments.ascx" TagName="Attachments" TagPrefix="attach" %>
<%@ Register Src="/Modules/Ultimus.UWF.Form.ProcessControl/ButtonList.ascx" TagName="ButtonList" TagPrefix="btn" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta http-equiv="X-UA-Compatible"  content="IE=EmulateIE8" />
    <title>Day-Off Record Application Form</title>
    <script type="text/javascript" src="/Assets/js/common.js"></script>
     <script type="text/javascript">
         function beforeSubmit() {
             var date = new Date($("#fld_StartDate").val().replace(/-/g,"/"));
             $("#fld_DayOffYear").val(date.getFullYear());
             $("#fld_DayOffMonth").val(date.getMonth() + 1);

             var CheckFlag = changeTime();
             if (!CheckFlag&&CheckFlag!=undefined) {
                 return false;
             }
             var SumHour = $("#fld_SumHour").val() - 0;
             var Msg = "";
//             if (SumHour % 2 != 0) {
//                 Msg = "Your Applying time is wrong.Please input again!";

//             }
             if (Msg == "") {
                 var summary = "Day-Off Record Application Process";
                 $("#UserInfo1_fld_PROCESSSUMMARY").val(summary);
                 return true;
             }
             else {
                 alert(Msg);
                 return false;
             }
            
         }
         function showTime(obj) {
             var time = new Date(obj.replace(/-/g, "/"));
             var year = time.getFullYear();
             var month = time.getMonth() + 1;
             var date = time.getDate();
             return (year + "-" + month + "-" + date) == "NaN-NaN-NaN" ? "" : (year + "-" + month + "-" + date);
         }
         $(document).ready(function () {

             $("#starthours").val($("#fld_StartHours").val());
             $("#startminutes").val($("#fld_StartMinutes").val());
             $("#endhours").val($("#fld_EndHours").val());
             $("#endmiutes").val($("#fld_EndMinutes").val());
             $("#fld_StartDate").val(showTime($("#fld_StartDate").val()));
             $("#fld_EndDate").val(showTime($("#fld_EndDate").val()));
             if ($("#hdIncident").val() != "") {
                 $("#ButtonList1_btnSubmit").val("Submit");
                 $("#ButtonList1_btnBack").hide();
                 $("#ButtonList1_btnReject").show();
             }
             if ($("#hdUrgeTask").val() == "Yes") {
                 $("#ReturnBackTask").show();
             }

             Calculateclass('0');
              
         });
         function beforeSave() {
             var summary = "Day-Off Record Application Process";
             $("#UserInfo1_fld_PROCESSSUMMARY").val(summary);
             $("#fld_TRSummary").val(summary);
             return true;
         }
         function getHours(startDate, startHours, startMinutes, endDate, endHours, endMinutes) {
             var date1 = new Date(startDate.replace(/-/g, "/"));
             date1.setHours(parseFloat(startHours));
             date1.setMinutes(parseFloat(startMinutes));
             var date2 = new Date(endDate.replace(/-/g, "/"));
             date2.setHours(parseFloat(endHours));
             date2.setMinutes(parseFloat(endMinutes));
             var time1 = date1.getTime();
             var time2 = date2.getTime();
             return ((time2 - time1) / (1000 * 60 * 60)).toFixed(2);
         }
         function changeOption(obj) {
             var times = $(obj).val();
             $(obj).next().val(times);
             changeTime(obj);
         }
         function changeTime() {
             var startdate = $("#fld_StartDate").val();
             var starthours = $("#fld_StartHours").val();
             var startminutes = $("#fld_StartMinutes").val();
             var enddate = $("#fld_EndDate").val();
             var endhours = $("#fld_EndHours").val();
             var endminutes = $("#fld_EndMinutes").val();
             var hours = getHours(startdate, starthours, startminutes, enddate, endhours, endminutes) == "NaN" ? "" : getHours(startdate, starthours, startminutes, enddate, endhours, endminutes);
                 hours=$("#fld_SumHour").val();

             if (hours < 0) {
                 alert("You enter the time less than zero.Please input again");
                 // $("#fld_SumHour").val("");
                 return false;
             }
             else {
                 if (($("#fld_sumOTHourCount").val()-0) -(hours-0) < 0) {
                     alert("You can be paid leave time is less than the input time.Please input again");
                     // $("#fld_SumHour").val("");
                     return false;
                 }
             }

                var myDate = new Date;

                var EDate = enddate.replace(/-/g, "/");
                var SDate = startdate.replace(/-/g, "/");
                var EEDate = new Date(EDate);
                var SSDate = new Date(SDate);
                var Smonth = SSDate.getMonth() + 1;
                var Emonth = EEDate.getMonth() + 1;
                if (startdate != "" && enddate!="") {
                    if (Smonth == 2 && Emonth==3) {
                        alert("Date invalid,Please re-select");
                        $("#fld_StartDate").val("");
                        $("#fld_EndDate").val("");
                        $("#fld_SumHour").val("");
                        return false;
                    }
                }

            }

            function Calculateclass(TFlag) {
                var TotalSumHours = 0;
                var MinDate ="";
                var MaxDate = "";

                var DetailCount = $("#leavedetails").find("tr").length;
                if (DetailCount > 0) {
                  $(".OldDate").hide();
                }
                else {
                    $("#ListTableDetail").hide();
                }

                $("#leavedetails").find("tr").each(function () {
                    //console.log($(this).find(".startDate").val()); //开始日期
                    //console.log($(this).find(".StartHours").val()); //开始小时
                    //console.log($(this).find(".dropStartHours").val()); //开始小时
                    //console.log($(this).find(".StartMinutes").val()); //开始分钟
                    //console.log($(this).find(".dropStartMinutes").val()); //开始分钟

                    //console.log($(this).find(".endDate").val()); //结束日期
                    //console.log($(this).find(".EndHours").val()); //结束小时
                    //console.log($(this).find(".dropEndHours").val()); //结束小时
                    //console.log($(this).find(".EndMinutes").val()); //结束分钟
                    //console.log($(this).find(".dropEndMinutes").val()); //结束分钟
                    //console.log($(this).find(".SumHour").val());
                    var StartDate = $(this).find(".startDate").val();

                    var StartHours = $(this).find(".StartHours").val() - 0;
                    var DropStartHours = $(this).find(".dropStartHours").val() - 0;

                    var StartMinutes = $(this).find(".StartMinutes").val() - 0;
                    var DropStartMinutes = $(this).find(".dropStartMinutes").val() - 0;

                    var EndDate = $(this).find(".endDate").val();
                    var DropEndHours = $(this).find(".dropEndHours").val() - 0;
                    var EndHours = $(this).find(".EndHours").val() - 0;

                    var DropEndMinutes = $(this).find(".dropEndMinutes").val() - 0;
                    var EndMinutes = $(this).find(".EndMinutes").val() - 0;

                    if (TFlag == "1") {
                        $(this).find(".StartHours").val(DropStartHours);
                        $(this).find(".StartMinutes").val(DropStartMinutes);
                        $(this).find(".EndHours").val(DropEndHours);
                        $(this).find(".EndMinutes").val(DropEndMinutes);
                    }
                    else {
                        // console.log("赋值下拉框：" + StartHours);
                        $(this).find(".dropStartHours").val(StartHours);
                        $(this).find(".dropStartMinutes").val(StartMinutes);
                        $(this).find(".dropEndHours").val(EndHours);
                        $(this).find(".dropEndMinutes").val(EndMinutes);
                    }

                    StartDate = $(this).find(".startDate").val();
                    StartHours = $(this).find(".StartHours").val() - 0;
                    DropStartHours = $(this).find(".dropStartHours").val() - 0;
                    StartMinutes = $(this).find(".StartMinutes").val() - 0;
                    DropStartMinutes = $(this).find(".dropStartMinutes").val() - 0;
                    EndDate = $(this).find(".endDate").val();
                    DropEndHours = $(this).find(".dropEndHours").val() - 0;
                    EndHours = $(this).find(".EndHours").val() - 0;
                    DropEndMinutes = $(this).find(".dropEndMinutes").val() - 0;
                    EndMinutes = $(this).find(".EndMinutes").val() - 0;

                    var SumHour = $(this).find(".SumHour").val();
                    if (TFlag != "") {
                        if (StartDate != "" && EndDate != "") {

                            var TemStarDate = new Date(StartDate.replace(/-/g, "/"));
                            var TemEndDate = new Date(EndDate.replace(/-/g, "/"));

                            if (MinDate == "") {
                                MinDate = new Date(StartDate.replace(/-/g, "/"));
                                MaxDate = new Date(EndDate.replace(/-/g, "/"));
                            }

                            if (TemStarDate.getTime() <= MinDate.getTime()) {
                                MinDate = TemStarDate;
                                $("#fld_StartDate").val(StartDate);
                                $("#fld_StartHours").val(ChangeData(DropStartHours.toString()));
                               // console.log("Length："+$("#fld_StartHours").val());
                                $("#fld_StartMinutes").val(ChangeData(DropStartMinutes.toString()));
                            }
                            if (TemEndDate.getTime() >= MaxDate.getTime()) {
                                MaxDate = TemEndDate;
                                $("#fld_EndDate").val(EndDate);
                                $("#fld_EndHours").val(ChangeData(DropEndHours.toString()));
                                $("#fld_EndMinutes").val(ChangeData(DropEndMinutes.toString()));
                            }

                            var SubSumHour = 0;
                            if (StartDate == EndDate) {
                                SubSumHour = EndHours - StartHours;
                                if (StartHours < 12 && EndHours > 12) {
                                    SubSumHour = SubSumHour - 1;
                                    if (SubSumHour > 8)
                                        SubSumHour = 8;
                                }
                                TotalSumHours += SubSumHour;
                                $(this).find(".SumHour").val(SubSumHour);
                            }
                            else {
                                alert("Invalid date");
                                $(this).find(".startDate").val("");
                                $(this).find(".endDate").val("");
                            }
                        }
                        $("#fld_SumHour").val(TotalSumHours);
                    }
                });
               $("#SPSH").text($("#fld_SumHour").val());
            }
            function ChangeData(objvalue) {
                if (objvalue.length == 1) {
                    return "0" + objvalue;
                }
                else
                    return objvalue;
            }

    </script>
</head>
<body>
    <form id="form1" runat="server">
     <div id="myDiv" class="container">
            <div class="row">
                <ui:userinfo id="UserInfo1" processtitle="Day-Off Record Application Process" processprefix="DOR" tablename="PROC_DayOffRecord" runat="server" tablenamedetail="PROC_DayOffRecord_DT"  ></ui:userinfo>
                <asp:TextBox runat="server" ID="fld_ApplicantUser" style="display:none;"></asp:TextBox>
                <asp:TextBox runat="server" ID="fld_ApplierLogin" style="display:none;" ></asp:TextBox>
                <asp:TextBox runat="server" ID="fld_sumOTHourCount" style="display:none;"></asp:TextBox>
                <asp:TextBox runat="server" ID="fld_DayOffYear" style="display:none;"></asp:TextBox>
                <asp:TextBox runat="server" ID="fld_DayOffMonth" style="display:none;" ></asp:TextBox>
                <asp:TextBox runat="server" ID="fld_HR" style="display:none;"></asp:TextBox>
                <asp:TextBox runat="server" ID="fld_HRLogin" style="display:none;"></asp:TextBox>
            </div>
            <div class="row">
                <p style="font-weight:bold;">Request require（"<span style=" background:red;">&nbsp;</span>" must write） </p>
                <table class="table table-condensed table-bordered">
                    <tr>
                        <td class="td-label"  style="vertical-align:middle">
                         <span style=" background:red;  height:30px; float:left;">&nbsp;</span>
                        <p style="text-align:center">Day-Off Reason</p>
                        </td>
                        <td class="td-content" colspan="7" >
                        <asp:TextBox runat="server" ID="fld_Reason" Width="98%" TextMode="MultiLine" Rows="3" CssClass="validate[required]"></asp:TextBox>
                        </td>
                    </tr>
                    <tr class="OldDate"  >
                    <td class="td-label"> 
                     <span style=" background:red; margin-top:5px;  height:30px; float:left;">&nbsp;</span>
                        <p style="text-align:center">From</p>
                    </td>
                    <td class="td-content"  colspan="3" >
                       <span><asp:TextBox runat="server"  ID="fld_StartDate" CssClass="validate[required] DateFormat" onchange="changeTime(this)" Width="45%" onclick="WdatePicker({readOnly:false,dateFmt: 'yyyy-MM-dd',maxDate:'#F{$dp.$D(\'fld_EndDate\')}'})"></asp:TextBox></span>
                            <asp:DropDownList runat="server" Width="20%" ID="starthours" onchange="changeOption(this)">
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
                            <asp:TextBox runat="server" ID="fld_StartHours"  value="00" style="display:none;"></asp:TextBox>
                            <asp:DropDownList runat="server" Width="20%" ID="startminutes" onchange="changeOption(this)">
                                <asp:ListItem>00</asp:ListItem>
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
                            <asp:TextBox runat="server" ID="fld_StartMinutes"  value="00" style="display:none;"></asp:TextBox>
                      </td>
                    <td class="td-label"> 
                       <span style=" background:red; margin-top:5px;  height:30px; float:left;">&nbsp;</span>
                        <p style="text-align:center">To</p>
                        </td>
                    <td class="td-content"  colspan="3" >
                            <span><asp:TextBox runat="server"  ID="fld_EndDate" CssClass="validate[required]" Width="45%" onchange="changeTime(this)" onclick="WdatePicker({readOnly:false,dateFmt: 'yyyy-MM-dd',minDate:'#F{$dp.$D(\'fld_StartDate\')}'})"></asp:TextBox></span>
                            <asp:DropDownList runat="server" Width="20%" ID="endhours" onchange="changeOption(this)">
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
                            <asp:TextBox runat="server" ID="fld_EndHours" value="00" style="display:none;"></asp:TextBox>
                            <asp:DropDownList runat="server" Width="20%" ID="endmiutes" onchange="changeOption(this)">
                                <asp:ListItem>00</asp:ListItem>
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
                            <asp:TextBox runat="server" ID="fld_EndMinutes" value="00" style="display:none;"></asp:TextBox>
                        </td>
                    </tr>
                     
                    <tr class="OldDate"   >
                        <td class="td-label"  style="width:100px">
                         <span style=" background:red;  height:30px; float:left;">&nbsp;</span>
                        <p style="text-align:center">Sum</p>
                        </td>
                        <td class="td-content" colspan="7" >
                        <span><asp:TextBox runat="server" ID="fld_SumHour" onfocus="this.blur()" style="background-color:White;" Width="37%" CssClass="validate[required]"></asp:TextBox></span>
                        hours
                        </td>
                    </tr>
                </table>

               
                   <div class="row" id="ListTableDetail" style="display:block">

                    <table class="table table-condensed table-bordered">
                    <tr>
                        <th width="33%"> 
                         <span style=" background:red; margin-top:5px;  height:30px; float:left;">&nbsp;</span>
                        <p style="text-align:center">
                        From</p>
                        </th>
                        <th width="33%">
                        <p style="text-align:center">To</p>
                        </th>
                        <th width="21%">
                        <p style="text-align:center">Sub sumhours</p>
                        </th>
                        <th width="12%"></th>
                        </tr>

                          <tbody id="leavedetails">
                    <asp:Repeater runat="server" ID="fld_detail_PROC_DayOffRecord_DT" OnItemCommand="fld_detail_PROC_Leave_DT_ItemCommand"  >
                        <ItemTemplate>
                            <tr>
                                <td style="display:none">
                                    <span style=" background:red;">&nbsp;</span>
                                    <asp:TextBox ID="fld_FORMID" Text='<%#Eval("FORMID") %>' runat="server" Style="display: none"></asp:TextBox>
                                    <asp:Label ID="fld_ROWID" Text='<%# Container.ItemIndex+1%>' runat="server"></asp:Label>
                                </td>
                                 <td style="text-align:center">
                                 <span>  <asp:TextBox runat="server" ID="fld_StartDate"  onchange="Calculateclass('1')" Text='<%#String.IsNullOrEmpty(Eval("StartDate").ToString())? "":DateTime.Parse(Eval("StartDate").ToString()).ToString("yyyy-MM-dd") %>' CssClass="validate[required] startDate"  onclick="WdatePicker({readOnly:false,dateFmt: 'yyyy-MM-dd'})" Width="35%"></asp:TextBox>
                                 </span>
                                <asp:DropDownList runat="server" Width="20%" ID="dropStartHours" CssClass="dropStartHours" onchange="Calculateclass('1')">
                                <asp:ListItem Selected="True" Value='0'>00</asp:ListItem>
                                <asp:ListItem Value="1">01</asp:ListItem>
                                <asp:ListItem Value="2">02</asp:ListItem>
                                <asp:ListItem Value="3">03</asp:ListItem>
                                <asp:ListItem Value="4">04</asp:ListItem>
                                <asp:ListItem Value="5">05</asp:ListItem>
                                <asp:ListItem Value="6">06</asp:ListItem>
                                <asp:ListItem Value="7">07</asp:ListItem>
                                <asp:ListItem Value="8">08</asp:ListItem>
                                <asp:ListItem Value="9">09</asp:ListItem>
                                <asp:ListItem Value="10">10</asp:ListItem>
                                <asp:ListItem Value="11">11</asp:ListItem>
                                <asp:ListItem Value="12">12</asp:ListItem>
                                <asp:ListItem Value="13">13</asp:ListItem>
                                <asp:ListItem Value="14">14</asp:ListItem>
                                <asp:ListItem Value="15">15</asp:ListItem>
                                <asp:ListItem Value="16">16</asp:ListItem>
                                <asp:ListItem Value="17">17</asp:ListItem>
                                <asp:ListItem Value="18">18</asp:ListItem>
                                <asp:ListItem Value="19">19</asp:ListItem>
                                <asp:ListItem Value="20">20</asp:ListItem>
                                <asp:ListItem Value="21">21</asp:ListItem>
                                <asp:ListItem Value="22">22</asp:ListItem>
                                <asp:ListItem Value="23">23</asp:ListItem>
                            </asp:DropDownList>
                            <asp:TextBox runat="server" ID="fld_StartHours"  CssClass="StartHours" Text='<%#Eval("StartHours") %>' style="display:none" ></asp:TextBox>

                                <asp:DropDownList runat="server" Width="20%" ID="dropStartMinutes" CssClass="dropStartMinutes" onchange="Calculateclass('1')">
                                    <asp:ListItem>00</asp:ListItem>
                                    <asp:ListItem>15</asp:ListItem>
                                    <asp:ListItem>30</asp:ListItem>
                                    <asp:ListItem>45</asp:ListItem>
                                </asp:DropDownList>
                            <asp:TextBox runat="server" ID="fld_StartMinutes"  CssClass="StartMinutes" Text='<%#Eval("StartMinutes") %>' style="display:none" ></asp:TextBox>

                                   </td>
                                   <td style="text-align:center">
                                   <span> <asp:TextBox runat="server"  ID="fld_EndDate" CssClass="validate[required] endDate" Width="45%" onchange="Calculateclass('1')" Text='<%#String.IsNullOrEmpty(Eval("EndDate").ToString())? "":DateTime.Parse(Eval("EndDate").ToString()).ToString("yyyy-MM-dd") %>' onclick="WdatePicker({readOnly:false,dateFmt: 'yyyy-MM-dd'})"></asp:TextBox>
                                   </span>
                                   <asp:DropDownList runat="server" Width="20%" ID="dropEndHours" CssClass="dropEndHours"  onchange="Calculateclass('1')">
                                    <asp:ListItem Selected="True">00</asp:ListItem>
                                     <asp:ListItem Value="1">01</asp:ListItem>
                                     <asp:ListItem Value="2">02</asp:ListItem>
                                     <asp:ListItem Value="3">03</asp:ListItem>
                                     <asp:ListItem Value="4">04</asp:ListItem>
                                     <asp:ListItem Value="5">05</asp:ListItem>
                                     <asp:ListItem Value="6">06</asp:ListItem>
                                     <asp:ListItem Value="7">07</asp:ListItem>
                                     <asp:ListItem Value="8">08</asp:ListItem>
                                     <asp:ListItem Value="9">09</asp:ListItem>
                                     <asp:ListItem Value="10">10</asp:ListItem>
                                     <asp:ListItem Value="11">11</asp:ListItem>
                                     <asp:ListItem Value="12">12</asp:ListItem>
                                     <asp:ListItem Value="13">13</asp:ListItem>
                                     <asp:ListItem Value="14">14</asp:ListItem>
                                     <asp:ListItem Value="15">15</asp:ListItem>
                                     <asp:ListItem Value="16">16</asp:ListItem>
                                     <asp:ListItem Value="17">17</asp:ListItem>
                                     <asp:ListItem Value="18">18</asp:ListItem>
                                     <asp:ListItem Value="19">19</asp:ListItem>
                                     <asp:ListItem Value="20">20</asp:ListItem>
                                     <asp:ListItem Value="21">21</asp:ListItem>
                                     <asp:ListItem Value="22">22</asp:ListItem>
                                     <asp:ListItem Value="23">23</asp:ListItem>
                                    </asp:DropDownList>
                                    <asp:TextBox runat="server" ID="fld_EndHours"  CssClass="EndHours" Text='<%#Eval("EndHours") %>' style="display:none" ></asp:TextBox>
                                   <asp:DropDownList runat="server" Width="20%" ID="dropEndMinutes" CssClass="dropEndMinutes" onchange="Calculateclass('1')">
                                       <asp:ListItem>00</asp:ListItem>
                                       <asp:ListItem>15</asp:ListItem>
                                       <asp:ListItem>30</asp:ListItem>
                                       <asp:ListItem>45</asp:ListItem>
                                   </asp:DropDownList>
                                   <asp:TextBox runat="server" ID="fld_EndMinutes"  CssClass="EndMinutes" Text='<%#Eval("EndMinutes") %>' style="display:none" ></asp:TextBox>
                                   </td>
                                   <td style="text-align:center">
                                   <asp:TextBox runat="server" ID="fld_SumHour" onfocus="this.blur()" Text='<%#Eval("SumHour") %>' style="background-color:White;" Width="37%" CssClass="validate[required] SumHour"></asp:TextBox>
                                   </td>
                                   <td>
                                   <asp:Button ID="btnDelete" runat="server" Text="delete" CssClass="btn" CommandName="del" ClientIDMode="Static" OnClientClick="return confirm('Confirm Del？')" />
                                   </td>

                                </tr>
                                </ItemTemplate>
                                </asp:Repeater>
                                </tbody>

                    <tr>
                    <td colspan="2">
                        <asp:Button runat="server" ID="btnAdd" Text="add" CssClass="btn"  CausesValidation="false" OnClick="btnAdd_Click"/>
                    </td>
                    <td>
                    Sum Hours: <span id="SPSH"></span>
                    </td>
                    <td></td>
                    </tr>

                        </table>



                 </div>


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


