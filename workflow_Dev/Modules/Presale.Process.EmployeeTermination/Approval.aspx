<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Approval.aspx.cs" Inherits="Presale.Process.EmployeeTermination.Approval" %>
<%@ Register Src="/Modules/Ultimus.UWF.Form.ProcessControl/UserInfo.ascx" TagName="UserInfo" TagPrefix="ui" %>
<%@ Register Src="/Modules/Ultimus.UWF.Form.ProcessControl/ApprovalHistory.ascx" TagName="ApprovalHistory" TagPrefix="ah" %>
<%@ Register Src="/Modules/Ultimus.UWF.Form.ProcessControl/Attachments.ascx" TagName="Attachments" TagPrefix="attach" %>
<%@ Register Src="/Modules/Ultimus.UWF.Form.ProcessControl/ButtonList.ascx" TagName="ButtonList" TagPrefix="btn" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta http-equiv="X-UA-Compatible"  content="IE=EmulateIE8" />
    <title>Employee Termination-Check Out List</title>
    <script type="text/javascript" src="/Assets/js/common.js"></script>
    <script type="text/javascript">
        function submitPageReview(obj) {
            $("#ButtonList1_btnSubmit").click();
        }
        function getButtonCheck(obj, index) {
            if (index == "1") {
                if ($(obj).attr("checked")) {
                    $("#fld_HRSMCCC").val("CollectedCancelled");
                    $("#HRSMCNA").attr("checked", false);
                }
                else {
                    $("#fld_HRSMCCC").val("");
                }
            }
            if (index == "2") {
                if ($(obj).attr("checked")) {
                    $("#fld_HRSMCNA").val("NA");
                    $("#HRSMCCC").attr("checked", false);
                }
                else {
                    $("#fld_HRSMCNA").val("");
                }
            }
            if (index == "3") {
                if ($(obj).attr("checked")) {
                    $("#fld_HRSWPTCC").val("CollectedCancelled");
                    $("#HRSWPTNA").attr("checked", false);
                }
                else {
                    $("#fld_HRSWPTCC").val("");
                }
            }
            if (index == "4") {
                if ($(obj).attr("checked")) {
                    $("#fld_HRSWPTNA").val("NA");
                    $("#HRSWPTCC").attr("checked", false);
                }
                else {
                    $("#fld_HRSWPTNA").val("");
                }
            }
            if (index == "5") {
                if ($(obj).attr("checked")) {
                    $("#fld_HRSGTLTCC").val("CollectedCancelled");
                    $("#HRSGTLTNA").attr("checked", false);
                }
                else {
                    $("#fld_HRSGTLTCC").val("");
                }
            }
            if (index == "6") {
                if ($(obj).attr("checked")) {
                    $("#fld_HRSGTLTNA").val("NA");
                    $("#HRSGTLTCC").attr("checked", false);
                }
                else {
                    $("#fld_HRSGTLTNA").val("");
                }
            }
            if (index == "7") {
                if ($(obj).attr("checked")) {
                    $("#fld_HRSVPHRCC").val("CollectedCancelled");
                    $("#HRSVPHRNA").attr("checked", false);
                }
                else {
                    $("#fld_HRSVPHRCC").val("");
                }
            }
            if (index == "8") {
                if ($(obj).attr("checked")) {
                    $("#fld_HRSVPHRNA").val("NA");
                    $("#HRSVPHRCC").attr("checked", false);
                }
                else {
                    $("#fld_HRSVPHRNA").val("");
                }
            }
            if (index == "9") {
                if ($(obj).attr("checked")) {
                    $("#fld_HRSVCACC").val("CollectedCancelled");
                    $("#HRSVCANA").attr("checked", false);
                }
                else {
                    $("#fld_HRSVCACC").val("");
                }
            }
            if (index == "10") {
                if ($(obj).attr("checked")) {
                    $("#fld_HRSVCANA").val("NA");
                    $("#HRSVCACC").attr("checked", false);
                }
                else {
                    $("#fld_HRSVCANA").val("");
                }
            }
            if (index == "11") {
                if ($(obj).attr("checked")) {
                    $("#fld_HRSVEAPRCC").val("CollectedCancelled");
                    $("#HRSVEAPRNA").attr("checked", false);
                }
                else {
                    $("#fld_HRSVEAPRCC").val("");
                }
            }
            if (index == "12") {
                if ($(obj).attr("checked")) {
                    $("#fld_HRSVEAPRNA").val("NA");
                    $("#HRSVEAPRCC").attr("checked", false);
                }
                else {
                    $("#fld_HRSVEAPRNA").val("");
                }
            }
        }
        $(document).ready(function () {
            if ($("#read_ITCSTACC").text() == "CollectedCancelled") {
                $("#ITCSTACC").attr("checked", true);
            }
            if ($("#read_ITCSTANA").text() == "NA") {
                $("#ITCSTANA").attr("checked", true);
            }
            if ($("#read_ITDesktopCC").text() == "CollectedCancelled") {
                $("#ITDesktopCC").attr("checked", true);
            }
            if ($("#read_ITDesktopNA").text() == "NA") {
                $("#ITDesktopNA").attr("checked", true);
            }
            if ($("#read_ITNSCC").text() == "CollectedCancelled") {
                $("#ITNSCC").attr("checked", true);
            }
            if ($("#read_ITNSNA").text() == "NA") {
                $("#ITNSNA").attr("checked", true);
            }
            if ($("#read_ITSLCC").text() == "CollectedCancelled") {
                $("#ITSLCC").attr("checked", true);
            }
            if ($("#read_ITSLNA").text() == "NA") {
                $("#ITSLNA").attr("checked", true);
            }
            if ($("#read_ITEATCC").text() == "CollectedCancelled") {
                $("#ITEATCC").attr("checked", true);
            }
            if ($("#read_ITEATNA").text() == "NA") {
                $("#ITEATNA").attr("checked", true);
            }
            if ($("#read_ITOtherCC").text() == "CollectedCancelled") {
                $("#ITOtherCC").attr("checked", true);
            }
            if ($("#read_ITOtherNA").text() == "NA") {
                $("#ITOtherNA").attr("checked", true);
            }
            if ($("#read_FinanceRFSCC").text() == "CollectedCancelled") {
                $("#FinanceRFSCC").attr("checked", true);
            }
            if ($("#read_FinanceRFSNA").text() == "NA") {
                $("#FinanceRFSNA").attr("checked", true);
            }
            if ($("#read_FinanceLAACSCC").text() == "CollectedCancelled") {
                $("#FinanceLAACSCC").attr("checked", true);
            }
            if ($("#read_FinanceLAACSNA").text() == "NA") {
                $("#FinanceLAACSNA").attr("checked", true);
            }
            if ($("#read_FinanceOtherCC").text() == "CollectedCancelled") {
                $("#FinanceOtherCC").attr("checked", true);
            }
            if ($("#read_FinanceOtherNA").text() == "NA") {
                $("#FinanceOtherNA").attr("checked", true);
            }
            if ($("#read_HRGEICC").text() == "CollectedCancelled") {
                $("#HRGEICC").attr("checked", true);
            }
            if ($("#read_HRGEINA").text() == "NA") {
                $("#HRGEINA").attr("checked", true);
            }
            if ($("#read_HRGWNCACC").text() == "CollectedCancelled") {
                $("#HRGWNCACC").attr("checked", true);
            }
            if ($("#read_HRGWNCANA").text() == "NA") {
                $("#HRGWNCANA").attr("checked", true);
            }
            if ($("#read_HRGNCPCC").text() == "CollectedCancelled") {
                $("#HRGNCPCC").attr("checked", true);
            }
            if ($("#read_HRGNCPNA").text() == "NA") {
                $("#HRGNCPNA").attr("checked", true);
            }
            if ($("#read_AdminIHBCC").text() == "CollectedCancelled") {
                $("#AdminIHBCC").attr("checked", true);
            }
            if ($("#read_AdminIHBNA").text() == "NA") {
                $("#AdminIHBNA").attr("checked", true);
            }
            if ($("#read_AdminCKCC").text() == "CollectedCancelled") {
                $("#AdminCKCC").attr("checked", true);
            }
            if ($("#read_AdminCKNA").text() == "NA") {
                $("#AdminCKNA").attr("checked", true);
            }
            if ($("#read_AdminCBCCC").text() == "CollectedCancelled") {
                $("#AdminCBCCC").attr("checked", true);
            }
            if ($("#read_AdminCBCNA").text() == "NA") {
                $("#AdminCBCNA").attr("checked", true);
            }
            if ($("#read_AdminCSCC").text() == "CollectedCancelled") {
                $("#AdminCSCC").attr("checked", true);
            }
            if ($("#read_AdminCSNA").text() == "NA") {
                $("#AdminCSNA").attr("checked", true);
            }
            if ($("#fld_HRSMCCC").val() == "CollectedCancelled") {
                $("#HRSMCCC").attr("checked", true);
            }
            if ($("#fld_HRSMCNA").val() == "NA") {
                $("#HRSMCNA").attr("checked", true);
            }
            if ($("#fld_HRSGTLTCC").val() == "CollectedCancelled") {
                $("#HRSWPTCC").attr("checked", true);
            }
            if ($("#fld_HRSWPTNA").val() == "NA") {
                $("#HRSWPTNA").attr("checked", true);
            }
            if ($("#fld_HRSGTLTCC").val() == "CollectedCancelled") {
                $("#HRSGTLTCC").attr("checked", true);
            }
            if ($("#fld_HRSGTLTNA").val() == "NA") {
                $("#HRSGTLTNA").attr("checked", true);
            }
            if ($("#fld_HRSVPHRCC").val() == "CollectedCancelled") {
                $("#HRSVPHRCC").attr("checked", true);
            }
            if ($("#fld_HRSVPHRNA").val() == "NA") {
                $("#HRSVPHRNA").attr("checked", true);
            }
            if ($("#fld_HRSVCACC").val() == "CollectedCancelled") {
                $("#HRSVCACC").attr("checked", true);
            }
            if ($("#fld_HRSVCANA").val() == "NA") {
                $("#HRSVCANA").attr("checked", true);
            }
            if ($("#fld_HRSVEAPRCC").val() == "CollectedCancelled") {
                $("#HRSVEAPRCC").attr("checked", true);
            }
            if ($("#fld_HRSVEAPRNA").val() == "NA") {
                $("#HRSVEAPRNA").attr("checked", true);
            }

            /*=====================HSEF START=====================*/
            if ($("#fld_HSEIHBCC").val() == "CollectedCancelled") { $("#HSEFIHBCC").attr("checked", true); }
            if ($("#fld_HSEIHBNA").val() == "NA") { $("#HSEFIHBNA").attr("checked", true); }
            if ($("#fld_HSECKCC").val() == "CollectedCancelled") { $("#HSEFNA").attr("checked", true); }
            if ($("#fld_HSECKNA").val() == "NA") { $("#HSEFCC").attr("checked", true); }
            /*=====================HSEF END=====================*/
            func2();
            var TaskType = request('Type');

            if (TaskType == 'mytask') {
                func1('1');
            }
            else {
                func1('0');
            }
        });

        function func1(flag) {
            $("#formdiv").each(function (i) {
                $(this).find("table").each(function () {
                    if (flag == "1") {
                        if ($(this).attr("ID") == undefined) {
                            $(this).find('input[type="checkbox"]').attr("disabled", "disabled");
                            $(this).find('input[type="radio"]').attr("disabled", "disabled");
                        }
                    }
                    else {
                        $(this).find('input[type="checkbox"]').attr("disabled", "disabled");
                        $(this).find('input[type="text"]').attr("disabled", "disabled");
                    }
                });
            });
        }
        function func2() {
            var Companyplace = $("#fld_Companyplace").val();
            if (Companyplace != "")
                $("input[name^='Companyplace']").eq((Companyplace - 5) * (-1)).attr("checked", true);

            var Companypolicies = $("#fld_Companypolicies").val();
            if (Companypolicies != "")
                $("input[name^='Companypolicies']").eq((Companypolicies - 5) * (-1)).attr("checked", true);

            var Companyphysical = $("#fld_Companyphysical").val();
            if (Companyphysical != "")
                $("input[name^='Companyphysical']").eq((Companyphysical - 5) * (-1)).attr("checked", true);

            var Companybenefits = $("#fld_Companybenefits").val();
            if (Companybenefits != "")
                $("input[name^='Companybenefits']").eq((Companybenefits - 5) * (-1)).attr("checked", true);

            var Companyprovide = $("#fld_Companyprovide").val();
            if (Companyprovide != "")
                $("input[name^='Companyprovide']").eq((Companyprovide - 5) * (-1)).attr("checked", true);

            var Companytraining = $("#fld_Companytraining").val();
            if (Companytraining != "")
                $("input[name^='Companytraining']").eq((Companytraining - 5) * (-1)).attr("checked", true);

            var Companyenough = $("#fld_Companyenough").val();
            if (Companyenough != "")
                $("input[name^='Companyenough']").eq((Companyenough - 5) * (-1)).attr("checked", true);

            var Companyimprove = $("#fld_Companyimprove").val();
            if (Companyimprove != "")
                $("input[name^='Companyimprove']").eq((Companyimprove - 5) * (-1)).attr("checked", true);

            var CompanyManagement1 = $("#fld_CompanyManagement1").val();
            if (CompanyManagement1 != "")
                $("input[name^='CompanyManagement1']").eq((CompanyManagement1 - 5) * (-1)).attr("checked", true);
            var CompanyManagement2 = $("#fld_CompanyManagement2").val();
            if (CompanyManagement2 != "")
                $("input[name^='CompanyManagement2']").eq((CompanyManagement2 - 5) * (-1)).attr("checked", true);

            var CompanyManagement3 = $("#fld_CompanyManagement3").val();
            if (CompanyManagement3 != "")
                $("input[name^='CompanyManagement3']").eq((CompanyManagement3 - 5) * (-1)).attr("checked", true);

            var CompanyManagement4 = $("#fld_CompanyManagement4").val();
            if (CompanyManagement4 != "")
                $("input[name^='CompanyManagement4']").eq((CompanyManagement4 - 5) * (-1)).attr("checked", true);

            var CompanyManagement5 = $("#fld_CompanyManagement5").val();
            if (CompanyManagement5 != "")
                $("input[name^='CompanyManagement5']").eq((CompanyManagement5 - 5) * (-1)).attr("checked", true);

            var CompanyManagement6 = $("#fld_CompanyManagement6").val();
            if (CompanyManagement6 != "")
                $("input[name^='CompanyManagement6']").eq((CompanyManagement6 - 5) * (-1)).attr("checked", true);


            var CompanyManagement7 = $("#fld_CompanyManagement7").val();
            if (CompanyManagement7 != "")
                $("input[name^='CompanyManagement7']").eq((CompanyManagement7 - 5) * (-1)).attr("checked", true);

            var CompanyManagement8 = $("#fld_CompanyManagement8").val();
            if (CompanyManagement8 != "")
                $("input[name^='CompanyManagement8']").eq((CompanyManagement8 - 5) * (-1)).attr("checked", true);


        }


        function beforeSubmit() {
            var msg = "";
            if ($("#fld_HRSMCCC").val() == "" && $("#fld_HRSMCNA").val() == "")
                msg = "Pls select HRS Responsibilities\n";
            if ($("#fld_HRSWPTCC").val() == "" && $("#fld_HRSWPTNA").val() == "")
                msg = "Pls select HRS Responsibilities\n";

            if ($("#fld_HRSGTLTCC").val() == "" && $("#fld_HRSGTLTNA").val() == "")
                msg = "Pls select HRS Responsibilities\n";

            if ($("#fld_HRSVPHRCC").val() == "" && $("#fld_HRSVPHRNA").val() == "")
                msg = "Pls select HRS Responsibilities\n";

            if ($("#fld_HRSVCACC").val() == "" && $("#fld_HRSVCANA").val() == "")
                msg = "Pls select HRS Responsibilities\n";

            if ($("#fld_HRSVEAPRCC").val() == "" && $("#fld_HRSVEAPRNA").val() == "")
                msg = "Pls select HRS Responsibilities\n";
            if (msg != "") {
                alert(msg);
                return false;
            }
            else
                return true;
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
     <div id="myDiv" class="container">
            <div class="row">
                <ui:userinfo id="UserInfo1" processtitle="Employee Termination-Check Out List" processprefix="ETCO" tablename="PROC_EmployeeTerminationCheckOut"
                    runat="server"  ></ui:userinfo>
            </div>
            <div class="row" id="formdiv">
                <p style="font-weight:bold;">Termination Employee</p>
                <table class="table table-condensed table-bordered">
                    <tr>
                        <td class="td-label">
                         <span style="height:30px; float:left;">&nbsp;</span>
                        <p style="text-align:center">Termination Employee</p>
                        </td>
                        <td class="td-content" colspan="3" >
                        <asp:Label runat="server" ID="read_TerminationEmployee" ></asp:Label>
                        <asp:Label runat="server" ID="read_TerminationEmployeeValue" style="display:none;"></asp:Label>
                        </td>
                    </tr>
                </table>

      <p>The purpose of this questionnaire is to help the company determine reasons for employee turnover. The company appreciates your efforts in answering these questions frankly and your answer will not be used as part of any decision regarding future re-employment. Also, nothing in this form will be revealed to any other company.</p> 
      <p> There are two parts to this form, the first, simple questions, the second, give suggestion for changes of improvement. Please rate according to the appropriate tables.</p>
      
       <table class="table table-condensed table-bordered">
       <tr>
       <th style=" text-align:left; font-weight:bold;">  Part One:<%--第一部分--%>  </th>
       </tr>
       <tr>
       <th>
      These questions will help us address areas that need improvement in our company. Please rate your reactions according to the following point system.
      <%--这些问题将帮助我们致力于公司管理的不断完善。请您依照下列评分标准评分：--%>
         </th>
       </tr>
       <tr><td>
        TABLE FOR EVALUATIN OF ELEMENTS：
       (5)Excellent <%--很好--%>  (4) Good  <%--良好--%> (3) Satisfactory  <%--满意--%> (2) Fair  <%--一般--%> (1) Poor   <%--差--%></td> </tr>
       <tr>
       <th> General Environment <%--总体环境--%></th>
       </tr>
       <tr>
      <td> How would you rate this company as a place to work?<%--您如何评价公司作为您就业的一个选择单位？--%>
     <br /> <input type="radio" onclick="CheckedValue('Companyplace','5')" name="Companyplace" />5&nbsp;
      <input type="radio" onclick="CheckedValue('Companyplace','4')" name="Companyplace"  />4&nbsp;
      <input type="radio" onclick="CheckedValue('Companyplace','3')" name="Companyplace"  />3&nbsp;
      <input type="radio" onclick="CheckedValue('Companyplace','2')" name="Companyplace"  />2&nbsp;
      <input type="radio" onclick="CheckedValue('Companyplace','1')" name="Companyplace"  />1&nbsp;
      <asp:TextBox runat="server" ID="fld_Companyplace" style="display:none"></asp:TextBox>
      </td>
       </tr>
        <tr>
      <td>What is your overall view of the company’s policies and procedures?<%--您对公司政策及规定的总体看法？--%>
      <br />
       <input type="radio" onclick="CheckedValue('Companypolicies','5')" name="Companypolicies" />5&nbsp;
      <input type="radio" onclick="CheckedValue('Companypolicies','4')" name="Companypolicies"  />4&nbsp;
      <input type="radio" onclick="CheckedValue('Companypolicies','3')" name="Companypolicies"  />3&nbsp;
      <input type="radio" onclick="CheckedValue('Companypolicies','2')" name="Companypolicies"  />2&nbsp;
      <input type="radio" onclick="CheckedValue('Companypolicies','1')" name="Companypolicies"  />1&nbsp;
      <asp:TextBox runat="server" ID="fld_Companypolicies" style="display:none"></asp:TextBox>
      
      </td>
       </tr>
        <tr>
      <td>  How do you rate the working conditions and physical facilities?<%--您认为公司工作条件及硬件设施如何？--%>
      <br />
       <input type="radio" onclick="CheckedValue('Companyphysical','5')" name="Companyphysical" />5&nbsp;
      <input type="radio" onclick="CheckedValue('Companyphysical','4')" name="Companyphysical"  />4&nbsp;
      <input type="radio" onclick="CheckedValue('Companyphysical','3')" name="Companyphysical"  />3&nbsp;
      <input type="radio" onclick="CheckedValue('Companyphysical','2')" name="Companyphysical"  />2&nbsp;
      <input type="radio" onclick="CheckedValue('Companyphysical','1')" name="Companyphysical"  />1&nbsp;
      <asp:TextBox runat="server" ID="fld_Companyphysical" style="display:none"></asp:TextBox>
      </td>
       </tr>

       <tr>
       <th>Compensation & Benefit <%--薪酬及福利--%></th>
       </tr>
       <tr>
       <td>
       Compared with other companies, how do you rate our benefits package?<%--与其他公司比较，您认为公司的福利条件如何？--%>
      <br /> <input type="radio" onclick="CheckedValue('Companybenefits','5')" name="Companybenefits" />5&nbsp;
      <input type="radio" onclick="CheckedValue('Companybenefits','4')" name="Companybenefits"  />4&nbsp;
      <input type="radio" onclick="CheckedValue('Companybenefits','3')" name="Companybenefits"  />3&nbsp;
      <input type="radio" onclick="CheckedValue('Companybenefits','2')" name="Companybenefits"  />2&nbsp;
      <input type="radio" onclick="CheckedValue('Companybenefits','1')" name="Companybenefits"  />1&nbsp;
      <asp:TextBox runat="server" ID="fld_Companybenefits"  style="display:none"></asp:TextBox>
       </td>
       </tr>
       


       <tr>
       <td>
       How do you feel about pay provide by the Company?<%--您认为公司的工资待遇如何？--%>
     <br /> <input type="radio" onclick="CheckedValue('Companyprovide','5')" name="Companyprovide" />5&nbsp;
      <input type="radio" onclick="CheckedValue('Companyprovide','4')" name="Companyprovide"  />4&nbsp;
      <input type="radio" onclick="CheckedValue('Companyprovide','3')" name="Companyprovide"  />3&nbsp;
      <input type="radio" onclick="CheckedValue('Companyprovide','2')" name="Companyprovide"  />2&nbsp;
      <input type="radio" onclick="CheckedValue('Companyprovide','1')" name="Companyprovide"  />1&nbsp;
      <asp:TextBox runat="server" ID="fld_Companyprovide"  style="display:none"></asp:TextBox>
       </td>
       </tr>
         
      
      <tr>
      <th>Training Opportunities <%--培训机会--%></th>
      </tr>
       
       <tr>
       <td>
      How would you rate the training you received?<%--您对在公司接受的相关培训给予如何评价？--%> 
     <br /> <input type="radio" onclick="CheckedValue('Companytraining','5')" name="Companytraining" />5&nbsp;
      <input type="radio" onclick="CheckedValue('Companytraining','4')" name="Companytraining"  />4&nbsp;
      <input type="radio" onclick="CheckedValue('Companytraining','3')" name="Companytraining"  />3&nbsp;
      <input type="radio" onclick="CheckedValue('Companytraining','2')" name="Companytraining"  />2&nbsp;
      <input type="radio" onclick="CheckedValue('Companytraining','1')" name="Companytraining"  />1&nbsp;
      <asp:TextBox runat="server" ID="fld_Companytraining"  style="display:none"></asp:TextBox>
      </td>
       </tr>

       <tr>
       <td>
       Were you given enough information, in the early stages of your employment, about the Company, such as benefits, practices and policies, organization, orientation, etc?
       <%-- 在您加入公司初期，您是否有被提供有关公司各方面充足的信息？比如：福利、惯例、 政策、组织架构及工作方向等？--%>
     <br /> <input type="radio" onclick="CheckedValue('Companyenough','5')" name="Companyenough" />5&nbsp;
      <input type="radio" onclick="CheckedValue('Companyenough','4')" name="Companyenough"  />4&nbsp;
      <input type="radio" onclick="CheckedValue('Companyenough','3')" name="Companyenough"  />3&nbsp;
      <input type="radio" onclick="CheckedValue('Companyenough','2')" name="Companyenough"  />2&nbsp;
      <input type="radio" onclick="CheckedValue('Companyenough','1')" name="Companyenough"  />1&nbsp;
      <asp:TextBox runat="server" ID="fld_Companyenough"  style="display:none"></asp:TextBox>
       </td>
       </tr>
         
       <tr>
       <td>
       How do you rate subsequent training opportunities to improve your skills and opportunities?<%--您如何评价：在职期间得到的有关技能和发展提升的培训机会？--%>
      <br /><input type="radio" onclick="CheckedValue('Companyimprove','5')" name="Companyimprove" />5&nbsp;
      <input type="radio" onclick="CheckedValue('Companyimprove','4')" name="Companyimprove"  />4&nbsp;
      <input type="radio" onclick="CheckedValue('Companyimprove','3')" name="Companyimprove"  />3&nbsp;
      <input type="radio" onclick="CheckedValue('Companyimprove','2')" name="Companyimprove"  />2&nbsp;
      <input type="radio" onclick="CheckedValue('Companyimprove','1')" name="Companyimprove"  />1&nbsp;
      <asp:TextBox runat="server" ID="fld_Companyimprove"  style="display:none"></asp:TextBox>
       </td>
       </tr>
       
        <tr>
      <th>Management Style <%--管理风格--%></th>
      </tr>
       
        <tr>
       <td>
      How well did you understand the performance standards you were expected to meet?<%--您如何评价：在职期间得到的有关技能和发展提升的培训机会？--%>
      <br /><input type="radio" onclick="CheckedValue('CompanyManagement1','5')" name="CompanyManagement1" />5&nbsp;
      <input type="radio" onclick="CheckedValue('CompanyManagement1','4')" name="CompanyManagement1"  />4&nbsp;
      <input type="radio" onclick="CheckedValue('CompanyManagement1','3')" name="CompanyManagement1"  />3&nbsp;
      <input type="radio" onclick="CheckedValue('CompanyManagement1','2')" name="CompanyManagement1"  />2&nbsp;
      <input type="radio" onclick="CheckedValue('CompanyManagement1','1')" name="CompanyManagement1"  />1&nbsp;
      <asp:TextBox runat="server" ID="fld_CompanyManagement1"  style="display:none"></asp:TextBox>
       </td>
       </tr>


        <tr>
       <td>
      How well were you informed about changes that affected your work?<%--您如何评价：在职期间得到的有关技能和发展提升的培训机会？--%>
      <br /><input type="radio" onclick="CheckedValue('CompanyManagement2','5')" name="CompanyManagement2" />5&nbsp;
      <input type="radio" onclick="CheckedValue('CompanyManagement2','4')" name="CompanyManagement2"  />4&nbsp;
      <input type="radio" onclick="CheckedValue('CompanyManagement2','3')" name="CompanyManagement2"  />3&nbsp;
      <input type="radio" onclick="CheckedValue('CompanyManagement2','2')" name="CompanyManagement2"  />2&nbsp;
      <input type="radio" onclick="CheckedValue('CompanyManagement2','1')" name="CompanyManagement2"  />1&nbsp;
      <asp:TextBox runat="server" ID="fld_CompanyManagement2"  style="display:none"></asp:TextBox>
       </td>
       </tr>

        <tr>
       <td>
     How much of a chance do you feel you had to develop your full potential?<%--您如何评价：在职期间得到的有关技能和发展提升的培训机会？--%>
      <br /><input type="radio" onclick="CheckedValue('CompanyManagement3','5')" name="CompanyManagement3" />5&nbsp;
      <input type="radio" onclick="CheckedValue('CompanyManagement3','4')" name="CompanyManagement3"  />4&nbsp;
      <input type="radio" onclick="CheckedValue('CompanyManagement3','3')" name="CompanyManagement3"  />3&nbsp;
      <input type="radio" onclick="CheckedValue('CompanyManagement3','2')" name="CompanyManagement3"  />2&nbsp;
      <input type="radio" onclick="CheckedValue('CompanyManagement3','1')" name="CompanyManagement3"  />1&nbsp;
      <asp:TextBox runat="server" ID="fld_CompanyManagement3"  style="display:none"></asp:TextBox>
       </td>
       </tr>

       <tr>
       <td>
     How do you feel about management’s willingness to hear complaints and make changes?<%--您如何评价：在职期间得到的有关技能和发展提升的培训机会？--%>
      <br /><input type="radio" onclick="CheckedValue('CompanyManagement4','5')" name="CompanyManagement4" />5&nbsp;
      <input type="radio" onclick="CheckedValue('CompanyManagement4','4')" name="CompanyManagement4"  />4&nbsp;
      <input type="radio" onclick="CheckedValue('CompanyManagement4','3')" name="CompanyManagement4"  />3&nbsp;
      <input type="radio" onclick="CheckedValue('CompanyManagement4','2')" name="CompanyManagement4"  />2&nbsp;
      <input type="radio" onclick="CheckedValue('CompanyManagement4','1')" name="CompanyManagement4"  />1&nbsp;
      <asp:TextBox runat="server" ID="fld_CompanyManagement4"  style="display:none"></asp:TextBox>
       </td>
       </tr>

        <tr>
       <td>
     How do you rate your supervisor willingness to answer questions and help solve problems?<%--您如何评价：在职期间得到的有关技能和发展提升的培训机会？--%>
      <br /><input type="radio" onclick="CheckedValue('CompanyManagement5','5')" name="CompanyManagement5" />5&nbsp;
      <input type="radio" onclick="CheckedValue('CompanyManagement5','4')" name="CompanyManagement5"  />4&nbsp;
      <input type="radio" onclick="CheckedValue('CompanyManagement5','3')" name="CompanyManagement5"  />3&nbsp;
      <input type="radio" onclick="CheckedValue('CompanyManagement5','2')" name="CompanyManagement5"  />2&nbsp;
      <input type="radio" onclick="CheckedValue('CompanyManagement5','1')" name="CompanyManagement5"  />1&nbsp;
      <asp:TextBox runat="server" ID="fld_CompanyManagement5"  style="display:none"></asp:TextBox>
       </td>
       </tr>

        <tr>
       <td>
       To what extent were you encouraged to offer suggestions and improvements?<%--您如何评价：在职期间得到的有关技能和发展提升的培训机会？--%>
      <br /><input type="radio" onclick="CheckedValue('CompanyManagement6','5')" name="CompanyManagement6" />5&nbsp;
      <input type="radio" onclick="CheckedValue('CompanyManagement6','4')" name="CompanyManagement6"  />4&nbsp;
      <input type="radio" onclick="CheckedValue('CompanyManagement6','3')" name="CompanyManagement6"  />3&nbsp;
      <input type="radio" onclick="CheckedValue('CompanyManagement6','2')" name="CompanyManagement6"  />2&nbsp;
      <input type="radio" onclick="CheckedValue('CompanyManagement6','1')" name="CompanyManagement6"  />1&nbsp;
      <asp:TextBox runat="server" ID="fld_CompanyManagement6"  style="display:none"></asp:TextBox>
       </td>
       </tr>

        <tr>
       <td>
      What level of respect did you hold for your superior?<%--您如何评价：在职期间得到的有关技能和发展提升的培训机会？--%>
      <br /><input type="radio" onclick="CheckedValue('CompanyManagement7','5')" name="CompanyManagement7" />5&nbsp;
      <input type="radio" onclick="CheckedValue('CompanyManagement7','4')" name="CompanyManagement7"  />4&nbsp;
      <input type="radio" onclick="CheckedValue('CompanyManagement7','3')" name="CompanyManagement7"  />3&nbsp;
      <input type="radio" onclick="CheckedValue('CompanyManagement7','2')" name="CompanyManagement7"  />2&nbsp;
      <input type="radio" onclick="CheckedValue('CompanyManagement7','1')" name="CompanyManagement7"  />1&nbsp;
      <asp:TextBox runat="server" ID="fld_CompanyManagement7"  style="display:none"></asp:TextBox>
       </td>
       </tr>

        <tr>
       <td>
    How would you rate the spirit of cooperation and teamwork among the employees in your department?<%--您如何评价：在职期间得到的有关技能和发展提升的培训机会？--%>
      <br /><input type="radio" onclick="CheckedValue('CompanyManagement8','5')" name="CompanyManagement8" />5&nbsp;
      <input type="radio" onclick="CheckedValue('CompanyManagement8','4')" name="CompanyManagement8"  />4&nbsp;
      <input type="radio" onclick="CheckedValue('CompanyManagement8','3')" name="CompanyManagement8"  />3&nbsp;
      <input type="radio" onclick="CheckedValue('CompanyManagement8','2')" name="CompanyManagement8"  />2&nbsp;
      <input type="radio" onclick="CheckedValue('CompanyManagement8','1')" name="CompanyManagement8"  />1&nbsp;
      <asp:TextBox runat="server" ID="fld_CompanyManagement8"  style="display:none"></asp:TextBox>
       </td>
       </tr>
       </table>

 <table class="table table-condensed table-bordered">
 <tr>
 <th colspan="4" style="text-align:left; font-weight:bold;">
  Part Two:<%--第二部分--%>
  </th>
 </tr>
 <tr>
 <th style="text-align:left" colspan="4">
      Please give suggestion/recommendation on what area(s) the company need to improve and please state your opinion in detail.
<%--请对你认为公司需要改善的方面给出详细的建议、意见。--%> 
 </th>
 </tr>
 <tr>
<td colspan="4">
<asp:Label runat="server"   ID="read_Personalsuggestion"></asp:Label>
</td>
 </tr>
 <tr>
 <th colspan="4">
   Please indicate the reason why you are leaving the Company?<%--请简要说明您离开公司的原因？--%> 
 </th>
 </tr>
  <tr>
 <td colspan="4">
  <asp:Label runat="server"   ID="read_Personalreason"></asp:Label>
 </td>
 </tr> 


 <tr>
 <th colspan="4" style="text-align:left">
   If reason for leaving is another job, please provide:
<%--如果您离职的理由是因为找到了其他工作，方便的话请提供以下信息：--%> 
 </th>
 </tr>
       <tr>
       <th>Company Name<%--公司名称--%>:</th>
       <td><asp:Label runat="server" ID="read_NextCompanyName"></asp:Label></td>
       
       <th>Job Title<%--职位名称:--%>:</th>
       <td>
       <asp:Label runat="server" ID="read_NextJobtitle"></asp:Label> </td>
       </tr>


       <tr>
       <th>Interview By<%--面试负责人:--%>:</th>
       <td><asp:Label runat="server" ID="read_NextInterview"></asp:Label></td>
       
       <th>Date<%--日期--%>:</th>
       <td>
       <asp:Label runat="server" ID="read_NextJoinDD" ></asp:Label> </td>
       </tr>

</table>

     


                <p style="font-weight:bold;">IT Responsibilities</p>
                <table class="table table-condensed table-bordered">
                    <tr>
                        <th width="30%">
                         <span style="height:30px; float:left;">&nbsp;</span>
                        <p style="text-align:center">item</p>
                        </th>
                        <th width="15%">
                         <span style="height:30px; float:left;">&nbsp;</span>
                        <p style="text-align:center">Collected/Cancelled</p>
                        </th>
                        <th width="15%">
                         <span style="height:30px; float:left;">&nbsp;</span>
                        <p style="text-align:center">N/A</p>
                        </th>
                        <%--<th>
                         <span style="height:30px; float:left;">&nbsp;</span>
                        <p style="text-align:center">Serial/Card</p>
                        </th>--%>
                        <th width="40%">
                         <span style="height:30px; float:left;">&nbsp;</span>
                        <p style="text-align:center">Responsible Person's Signature</p>
                        </th>
                    </tr>
                    <tr style="display:none">
                        <th style="vertical-align:middle">
                         <span style="height:30px; float:left;">&nbsp;</span>
                        <p style="text-align:center">Conferencing Service Termination-2011-AP<br /> Voice team->Genesys/AT&T calling card</p>
                        </th>
                        <td style="vertical-align:middle;text-align:center">
                            <span style="height:30px; float:left;">&nbsp;</span>
                            <asp:CheckBox runat="server"  ID="ITCSTACC" disabled="true"></asp:CheckBox>
                            <asp:Label runat="server" ID="read_ITCSTACC" style="display:none;"></asp:Label>
                        </td>
                        <td style="vertical-align:middle;text-align:center">
                            <span style="height:30px; float:left;">&nbsp;</span>
                            <asp:CheckBox runat="server"  ID="ITCSTANA" disabled="true"></asp:CheckBox>
                            <asp:Label runat="server" ID="read_ITCSTANA" style="display:none;"></asp:Label>
                        </td>
                        <td style="vertical-align:middle;text-align:center">
                            <span style="height:30px; float:left;">&nbsp;</span>
                            <asp:Label runat="server" ID="read_ITCSTARPS"></asp:Label>
                        </td>
                    </tr>
                     <tr>
                        <th style="vertical-align:middle;text-align:center">
                         <span style="height:30px; float:left;">&nbsp;</span>
                        <p style="text-align:center">Desktop</p>
                        </th>
                        <td style="vertical-align:middle;text-align:center">
                            <span style="height:30px; float:left;">&nbsp;</span>
                            <asp:CheckBox runat="server"  ID="ITDesktopCC" disabled="true"></asp:CheckBox>
                            <asp:Label runat="server" ID="read_ITDesktopCC" style="display:none;"></asp:Label>
                        </td>
                        <td style="vertical-align:middle;text-align:center">
                            <span style="height:30px; float:left;">&nbsp;</span>
                            <asp:CheckBox runat="server"  ID="ITDesktopNA" disabled="true"></asp:CheckBox>
                            <asp:Label runat="server" ID="read_ITDesktopNA" style="display:none;"></asp:Label>
                        </td>
                       <%-- <td style="vertical-align:middle;text-align:center">
                            <span style="height:30px; float:left;">&nbsp;</span>
                            <asp:Label runat="server" ID="read_ITDesktopSC"></asp:Label>
                        </td>--%>
                        <td style="vertical-align:middle;text-align:center">
                            <span style="height:30px; float:left;">&nbsp;</span>
                            <asp:Label runat="server" ID="read_ITDesktopRPS"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <th style="vertical-align:middle">
                         <span style="height:30px; float:left;">&nbsp;</span>
                        <p style="text-align:center">Network Service</p>
                        </th>
                        <td style="vertical-align:middle;text-align:center">
                            <span style="height:30px; float:left;">&nbsp;</span>
                            <asp:CheckBox runat="server"  ID="ITNSCC" disabled="true"></asp:CheckBox>
                            <asp:Label runat="server" ID="read_ITNSCC" style="display:none;"></asp:Label>
                        </td>
                        <td style="vertical-align:middle;text-align:center">
                            <span style="height:30px; float:left;">&nbsp;</span>
                            <asp:CheckBox runat="server"  ID="ITNSNA" disabled="true"></asp:CheckBox>
                            <asp:Label runat="server" ID="read_ITNSNA" style="display:none;"></asp:Label>
                        </td>
                       <%-- <td style="vertical-align:middle;text-align:center">
                            <span style="height:30px; float:left;">&nbsp;</span>
                            <asp:Label runat="server" ID="read_ITNSSC"></asp:Label>
                        </td>--%>
                        <td style="vertical-align:middle;text-align:center">
                            <span style="height:30px; float:left;">&nbsp;</span>
                            <asp:Label runat="server" ID="read_ITNSRPS"></asp:Label>
                        </td>
                    </tr>
                     <tr>
                        <th style="vertical-align:middle">
                         <span style="height:30px; float:left;">&nbsp;</span>
                        <p style="text-align:center">Software Licenses</p>
                        </th>
                        <td style="vertical-align:middle;text-align:center">
                            <span style="height:30px; float:left;">&nbsp;</span>
                            <asp:CheckBox runat="server"  ID="ITSLCC" disabled="true"></asp:CheckBox>
                            <asp:Label runat="server" ID="read_ITSLCC" style="display:none;"></asp:Label>
                        </td>
                        <td style="vertical-align:middle;text-align:center">
                            <span style="height:30px; float:left;">&nbsp;</span>
                            <asp:CheckBox runat="server"  ID="ITSLNA" disabled="true"></asp:CheckBox>
                            <asp:Label runat="server" ID="read_ITSLNA" style="display:none;"></asp:Label>
                        </td>
                        <%--<td style="vertical-align:middle;text-align:center">
                            <span style="height:30px; float:left;">&nbsp;</span>
                            <asp:Label runat="server" ID="read_ITSLSC"></asp:Label>
                        </td>--%>
                        <td style="vertical-align:middle;text-align:center">
                            <span style="height:30px; float:left;">&nbsp;</span>
                            <asp:Label runat="server" ID="read_ITSLRPS"></asp:Label>
                        </td>
                    </tr>
                     <tr>
                        <th style="vertical-align:middle">
                         <span style="height:30px; float:left;">&nbsp;</span>
                        <p style="text-align:center">Email Account Termination</p>
                        </th>
                        <td style="vertical-align:middle;text-align:center">
                            <span style="height:30px; float:left;">&nbsp;</span>
                            <asp:CheckBox runat="server"  ID="ITEATCC" disabled="true"></asp:CheckBox>
                            <asp:Label runat="server" ID="read_ITEATCC" style="display:none;"></asp:Label>
                        </td>
                        <td style="vertical-align:middle;text-align:center">
                            <span style="margin-top:5px;  height:30px; float:left;">&nbsp;</span>
                            <asp:CheckBox runat="server"  ID="ITEATNA" disabled="true"></asp:CheckBox>
                            <asp:Label runat="server" ID="read_ITEATNA" style="display:none;"></asp:Label>
                        </td>
                        <%--<td style="vertical-align:middle;text-align:center">
                            <span style="height:30px; float:left;">&nbsp;</span>
                            <asp:Label runat="server" ID="read_ITEATSC"></asp:Label>
                        </td>--%>
                        <td style="vertical-align:middle;text-align:center">
                            <span style="height:30px; float:left;">&nbsp;</span>
                            <asp:Label runat="server" ID="read_ITEATRPS"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <th style="vertical-align:middle">
                         <span style="height:30px; float:left;">&nbsp;</span>
                        <p style="text-align:center">Others</p>
                        </th>
                        <td style="vertical-align:middle;text-align:center">
                            <span style="height:30px; float:left;">&nbsp;</span>
                            <asp:CheckBox runat="server"  ID="ITOtherCC" disabled="true"></asp:CheckBox>
                            <asp:Label runat="server" ID="read_ITOtherCC" style="display:none;"></asp:Label>
                        </td>
                        <td style="vertical-align:middle;text-align:center">
                            <span style="height:30px; float:left;">&nbsp;</span>
                            <asp:CheckBox runat="server"  ID="ITOtherNA" disabled="true"></asp:CheckBox>
                            <asp:Label runat="server" ID="read_ITOtherNA" style="display:none;"></asp:Label>
                        </td>
                       <%-- <td style="vertical-align:middle;text-align:center">
                            <span style="height:30px; float:left;">&nbsp;</span>
                            <asp:Label runat="server" ID="read_ITOtherSC"></asp:Label>
                        </td>--%>
                        <td style="vertical-align:middle;text-align:center">
                            <span style="height:30px; float:left;">&nbsp;</span>
                            <asp:Label runat="server" ID="read_ITOtherRPS"></asp:Label>
                        </td>
                    </tr>
                </table>
                <p style="font-weight:bold;">Finance Responsibilities</p>
                <table class="table table-condensed table-bordered">
                    <tr>
                        <th width="30%">
                         <span style="height:30px; float:left;">&nbsp;</span>
                        <p style="text-align:center">item</p>
                        </th>
                        <th width="15%">
                         <span style="height:30px; float:left;">&nbsp;</span>
                        <p style="text-align:center">Collected/Cancelled</p>
                        </th>
                        <th width="15%">
                         <span style="height:30px; float:left;">&nbsp;</span>
                        <p style="text-align:center">N/A</p>
                        </th>
                        <%--<th>
                         <span style="height:30px; float:left;">&nbsp;</span>
                        <p style="text-align:center">Serial/Card</p>
                        </th>--%>
                        <th width="40%">
                         <span style="height:30px; float:left;">&nbsp;</span>
                        <p style="text-align:center">Responsible Person's Signature</p>
                        </th>
                    </tr>
                     <tr>
                        <th style="vertical-align:middle;text-align:center">
                         <span style="height:30px; float:left;">&nbsp;</span>
                        <p style="text-align:center">Reserved fund settled</p>
                        </th>
                        <td style="vertical-align:middle;text-align:center">
                            <span style="height:30px; float:left;">&nbsp;</span>
                            <asp:CheckBox runat="server"  ID="FinanceRFSCC" disabled="true"></asp:CheckBox>
                            <asp:Label runat="server" ID="read_FinanceRFSCC" style="display:none;"></asp:Label>
                        </td>
                        <td style="vertical-align:middle;text-align:center">
                            <span style="height:30px; float:left;">&nbsp;</span>
                            <asp:CheckBox runat="server"  ID="FinanceRFSNA" disabled="true"></asp:CheckBox>
                            <asp:Label runat="server" ID="read_FinanceRFSNA" style="display:none;"></asp:Label>
                        </td>
                        <%--<td style="vertical-align:middle;text-align:center">
                            <span style="height:30px; float:left;">&nbsp;</span>
                            <asp:Label runat="server" ID="read_FinanceRFSSC"></asp:Label>
                        </td>--%>
                        <td style="vertical-align:middle;text-align:center">
                            <span style="height:30px; float:left;">&nbsp;</span>
                            <asp:Label runat="server" ID="read_FinanceRFSRPS"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <th style="vertical-align:middle">
                         <span style="height:30px; float:left;">&nbsp;</span>
                        <p style="text-align:center">Loan and advanced cash settled</p>
                        </th>
                        <td style="vertical-align:middle;text-align:center">
                            <span style="height:30px; float:left;">&nbsp;</span>
                            <asp:CheckBox runat="server"  ID="FinanceLAACSCC" disabled="true"></asp:CheckBox>
                            <asp:Label runat="server" ID="read_FinanceLAACSCC" style="display:none;"></asp:Label>
                        </td>
                        <td style="vertical-align:middle;text-align:center">
                            <span style="margin-top:5px;  height:30px; float:left;">&nbsp;</span>
                            <asp:CheckBox runat="server"  ID="FinanceLAACSNA" disabled="true"></asp:CheckBox>
                            <asp:Label runat="server" ID="read_FinanceLAACSNA" style="display:none;"></asp:Label>
                        </td>
                        <%--<td style="vertical-align:middle">
                            <span style="height:30px; float:left;">&nbsp;</span>
                            <asp:Label runat="server" ID="read_FinanceLAACSSC" ></asp:Label>
                        </td>--%>
                        <td style="vertical-align:middle;text-align:center">
                            <span style="height:30px; float:left;">&nbsp;</span>
                            <asp:Label runat="server" ID="read_FinanceLAACSRPS"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <th style="vertical-align:middle">
                         <span style="height:30px; float:left;">&nbsp;</span>
                        <p style="text-align:center">Other</p>
                        </th>
                        <td style="vertical-align:middle;text-align:center">
                            <span style="height:30px; float:left;">&nbsp;</span>
                            <asp:CheckBox runat="server"  ID="FinanceOtherCC" disabled="true"></asp:CheckBox>
                            <asp:Label runat="server" ID="read_FinanceOtherCC" style="display:none;"></asp:Label>
                        </td>
                        <td style="vertical-align:middle;text-align:center">
                            <span style="height:30px; float:left;">&nbsp;</span>
                            <asp:CheckBox runat="server"  ID="FinanceOtherNA" disabled="true"></asp:CheckBox>
                            <asp:Label runat="server" ID="read_FinanceOtherNA" style="display:none;"></asp:Label>
                        </td>
                        <%--<td style="vertical-align:middle;text-align:center">
                            <span style="height:30px; float:left;">&nbsp;</span>
                            <asp:Label runat="server" ID="read_FinanceOtherSC" ></asp:Label>
                        </td>--%>
                        <td style="vertical-align:middle;text-align:center">
                            <span style="height:30px; float:left;">&nbsp;</span>
                            <asp:Label runat="server" ID="read_FinanceOtherRPS"></asp:Label>
                        </td>
                    </tr>
                </table>
                <%--<p style="font-weight:bold;">HRG Responsibilities</p>
                <table class="table table-condensed table-bordered">
                    <tr>
                        <th>
                         <span style="height:30px; float:left;">&nbsp;</span>
                        <p style="text-align:center">item</p>
                        </td>
                        <td class="td-label">
                         <span style="height:30px; float:left;">&nbsp;</span>
                        <p style="text-align:center">Collected/Cancelled</p>
                        </td>
                        <td class="td-label">
                         <span style="height:30px; float:left;">&nbsp;</span>
                        <p style="text-align:center">N/A</p>
                        </td>
                        <td class="td-label">
                         <span style="height:30px; float:left;">&nbsp;</span>
                        <p style="text-align:center">serial/card</p>
                        </td>
                        <td class="td-label">
                         <span style="height:30px; float:left;">&nbsp;</span>
                        <p style="text-align:center">Responsible Person's Signature</p>
                        </td>
                    </tr>
                     <tr>
                        <td class="td-label" style="vertical-align:middle">
                         <span style="height:30px; float:left;">&nbsp;</span>
                        <p style="text-align:center">Exit Interview</p>
                        </td>
                        <td class="td-content" style="vertical-align:middle">
                            <span style="margin-top:5px;  height:30px; float:left;">&nbsp;</span>
                            <asp:CheckBox runat="server"  ID="HRGEICC" style="margin-left:50%;" disabled="true"></asp:CheckBox>
                            <asp:Label runat="server" ID="read_HRGEICC" style="display:none;"></asp:Label>
                        </td>
                        <td class="td-content" style="vertical-align:middle">
                            <span style="margin-top:5px;  height:30px; float:left;">&nbsp;</span>
                            <asp:CheckBox runat="server"  ID="HRGEINA" style="margin-left:50%;" disabled="true"></asp:CheckBox>
                            <asp:Label runat="server" ID="read_HRGEINA" style="display:none;"></asp:Label>
                        </td>
                        <td class="td-content">
                            <span style="margin-top:5px;  height:30px; float:left;">&nbsp;</span>
                            <asp:Label runat="server" ID="read_HRGEISC" CssClass="validate[required]"></asp:Label>
                        </td>
                        <td class="td-content" style="vertical-align:middle">
                            <span style="margin-top:5px;  height:30px; float:left;">&nbsp;</span>
                            <asp:Label runat="server" ID="read_HRGEIRPS"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="td-label" style="vertical-align:middle">
                         <span style="height:30px; float:left;">&nbsp;</span>
                        <p style="text-align:center">Waive Non Compete Agreement</p>
                        </td>
                        <td class="td-content" style="vertical-align:middle">
                            <span style="margin-top:5px;  height:30px; float:left;">&nbsp;</span>
                            <asp:CheckBox runat="server"  ID="HRGWNCACC" style="margin-left:50%;" disabled="true"></asp:CheckBox>
                            <asp:Label runat="server" ID="read_HRGWNCACC" style="display:none;"></asp:Label>
                        </td>
                        <td class="td-content" style="vertical-align:middle">
                            <span style="margin-top:5px;  height:30px; float:left;">&nbsp;</span>
                            <asp:CheckBox runat="server"  ID="HRGWNCANA" style="margin-left:50%;" disabled="true"></asp:CheckBox>
                            <asp:Label runat="server" ID="read_HRGWNCANA" style="display:none;"></asp:Label>
                        </td>
                        <td class="td-content">
                            <span style="background:red;margin-top:5px;  height:30px; float:left;">&nbsp;</span>
                            <asp:Label runat="server" ID="read_HRGWNCASC"></asp:Label>
                        </td>
                        <td class="td-content" style="vertical-align:middle">
                            <span style="margin-top:5px;  height:30px; float:left;">&nbsp;</span>
                            <asp:Label runat="server" ID="read_HRGWNCARPS"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="td-label" style="vertical-align:middle">
                         <span style="height:30px; float:left;">&nbsp;</span>
                        <p style="text-align:center">Non Compete payment</p>
                        </td>
                        <td class="td-content" style="vertical-align:middle">
                            <span style="margin-top:5px;  height:30px; float:left;">&nbsp;</span>
                            <asp:CheckBox runat="server"  ID="HRGNCPCC" style="margin-left:50%;" disabled="true"></asp:CheckBox>
                            <asp:Label runat="server" ID="read_HRGNCPCC" style="display:none;"></asp:Label>
                        </td>
                        <td class="td-content" style="vertical-align:middle">
                            <span style="margin-top:5px;  height:30px; float:left;">&nbsp;</span>
                            <asp:CheckBox runat="server"  ID="HRGNCPNA" style="margin-left:50%;" disabled="true"></asp:CheckBox>
                            <asp:Label runat="server" ID="read_HRGNCPNA" style="display:none;"></asp:Label>
                        </td>
                        <td class="td-content">
                            <span style="margin-top:5px;  height:30px; float:left;">&nbsp;</span>
                            <asp:Label runat="server" ID="read_HRGNCPSC" ></asp:Label>
                        </td>
                        <td class="td-content" style="vertical-align:middle">
                            <span style="margin-top:5px;  height:30px; float:left;">&nbsp;</span>
                            <asp:Label runat="server" ID="read_HRGNCPRPS" ></asp:Label>
                        </td>
                    </tr>
                </table>--%>
                <p style="font-weight:bold;">Administration Responsibilities</p>
                <table class="table table-condensed table-bordered">
                    <tr>
                        <th width="30%">
                         <span style="height:30px; float:left;">&nbsp;</span>
                        <p style="text-align:center">item</p>
                        </th>
                        <th width="15%">
                         <span style="height:30px; float:left;">&nbsp;</span>
                        <p style="text-align:center">Collected/Cancelled</p>
                        </th>
                        <th width="15%">
                         <span style="height:30px; float:left;">&nbsp;</span>
                        <p style="text-align:center">N/A</p>
                        </th>
                        <%--<th>
                         <span style="height:30px; float:left;">&nbsp;</span>
                        <p style="text-align:center">Serial/Card</p>
                        </th>--%>
                        <th width="40%">
                         <span style="height:30px; float:left;">&nbsp;</span>
                        <p style="text-align:center">Responsible Person's Signature</p>
                        </th>
                    </tr>
                     <tr>
                        <th style="vertical-align:middle">
                         <span style="height:30px; float:left;">&nbsp;</span>
                        <p style="text-align:center">ID/HonFei Badge</p>
                        </th>
                        <td style="vertical-align:middle;text-align:center">
                            <span style="height:30px; float:left;">&nbsp;</span>
                            <asp:CheckBox runat="server"  ID="AdminIHBCC" disabled="true"></asp:CheckBox>
                            <asp:Label runat="server" ID="read_AdminIHBCC" style="display:none;"></asp:Label>
                        </td>
                        <td style="vertical-align:middle;text-align:center">
                            <span style="height:30px; float:left;">&nbsp;</span>
                            <asp:CheckBox runat="server"  ID="AdminIHBNA" disabled="true"></asp:CheckBox>
                            <asp:Label runat="server" ID="read_AdminIHBNA" style="display:none;"></asp:Label>
                        </td>
                        <%--<td style="vertical-align:middle;text-align:center">
                            <span style="height:30px; float:left;">&nbsp;</span>
                            <asp:Label runat="server" ID="read_AdminIHBSC"></asp:Label>
                        </td>--%>
                        <td style="vertical-align:middle;text-align:center"> 
                            <span style="height:30px; float:left;">&nbsp;</span>
                            <asp:Label runat="server" ID="read_AdminIHBRPS" ></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <th style="vertical-align:middle">
                         <span style="height:30px; float:left;">&nbsp;</span>
                        <p style="text-align:center">Cabinet keys</p>
                        </th>
                        <td style="vertical-align:middle;text-align:center">
                            <span style="height:30px; float:left;">&nbsp;</span>
                            <asp:CheckBox runat="server"  ID="AdminCKCC" disabled="true"></asp:CheckBox>
                            <asp:Label runat="server" ID="read_AdminCKCC" style="display:none;"></asp:Label>
                        </td>
                        <td style="vertical-align:middle;text-align:center">
                            <span style="height:30px; float:left;">&nbsp;</span>
                            <asp:CheckBox runat="server"  ID="AdminCKNA" disabled="true"></asp:CheckBox>
                            <asp:Label runat="server" ID="read_AdminCKNA" style="display:none;"></asp:Label>
                        </td>
                        <%--<td style="vertical-align:middle;text-align:center">
                            <span style="height:30px; float:left;">&nbsp;</span>
                            <asp:Label runat="server" ID="read_AdminCKSC"></asp:Label>
                        </td>--%>
                        <td style="vertical-align:middle;text-align:center">
                            <span style="height:30px; float:left;">&nbsp;</span>
                            <asp:Label runat="server" ID="read_AdminCKRPS" ></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <th style="vertical-align:middle">
                         <span style="height:30px; float:left;">&nbsp;</span>
                        <p style="text-align:center">Company Business Cards</p>
                        </th>
                        <td style="vertical-align:middle;text-align:center">
                            <span style="height:30px; float:left;">&nbsp;</span>
                            <asp:CheckBox runat="server"  ID="AdminCBCCC" disabled="true"></asp:CheckBox>
                            <asp:Label runat="server" ID="read_AdminCBCCC" style="display:none;"></asp:Label>
                        </td>
                        <td style="vertical-align:middle;text-align:center">
                            <span style="height:30px; float:left;">&nbsp;</span>
                            <asp:CheckBox runat="server"  ID="AdminCBCNA" disabled="true"></asp:CheckBox>
                            <asp:Label runat="server" ID="read_AdminCBCNA" style="display:none;"></asp:Label>
                        </td>
                        <%--<td style="vertical-align:middle;text-align:center">
                            <span style="margin-top:5px;  height:30px; float:left;">&nbsp;</span>
                            <asp:Label runat="server" ID="read_AdminCBCSC" ></asp:Label>
                        </td>--%>
                        <td style="vertical-align:middle;text-align:center">
                            <span style="height:30px; float:left;">&nbsp;</span>
                            <asp:Label runat="server" ID="read_AdminCBCRPS" ></asp:Label>
                        </td>
                    </tr>
                     <tr>
                        <th style="vertical-align:middle">
                         <span style="height:30px; float:left;">&nbsp;</span>
                        <p style="text-align:center">Collect Stationary</p>
                        </th>
                        <td style="vertical-align:middle;text-align:center">
                            <span style="margin-top:5px;  height:30px; float:left;">&nbsp;</span>
                            <asp:CheckBox runat="server"  ID="AdminCSCC" disabled="true"></asp:CheckBox>
                            <asp:Label runat="server" ID="read_AdminCSCC" style="display:none;"></asp:Label>
                        </td>
                        <td style="vertical-align:middle;text-align:center">
                            <span style="height:30px; float:left;">&nbsp;</span>
                            <asp:CheckBox runat="server"  ID="AdminCSNA" disabled="true"></asp:CheckBox>
                            <asp:Label runat="server" ID="read_AdminCSNA" style="display:none;"></asp:Label>
                        </td>
                        <%--<td style="vertical-align:middle;text-align:center">
                            <span style="margin-top:5px;  height:30px; float:left;">&nbsp;</span>
                            <asp:Label runat="server" ID="read_AdminCSSC" ></asp:Label>
                        </td>--%>
                        <td style="vertical-align:middle;text-align:center">
                            <span style="height:30px; float:left;">&nbsp;</span>
                            <asp:Label runat="server" ID="read_AdminCSRPS"></asp:Label>
                        </td>
                    </tr>
                </table>


                 <p style="font-weight:bold;">HSEF Responsibilities</p>
                <table class="table table-condensed table-bordered">
                       <tr>
                        <th width="30%">
                         <span style="height:30px; float:left;">&nbsp;</span>
                        <p style="text-align:center">item</p>
                        </th>
                        <th width="15%">
                         <span style="height:30px; float:left;">&nbsp;</span>
                        <p style="text-align:center">Collected/Cancelled</p>
                        </th>
                        <th width="15%">
                         <span style="height:30px; float:left;">&nbsp;</span>
                        <p style="text-align:center">N/A</p>
                        </th>
                        <th width="40%">
                         <span style="height:30px; float:left;">&nbsp;</span>
                        <p style="text-align:center">Comments</p>
                        </th>
                    </tr>
                     <tr>
                        <th style="vertical-align:middle">
                         <span style="height:30px; float:left;">&nbsp;</span>
                        <p style="text-align:center">PPE Return</p>
                        </th>
                        <td style="vertical-align:middle;text-align:center">
                            <span style="height:30px; float:left;">&nbsp;</span>
                            <asp:CheckBox runat="server"  ID="HSEFIHBCC" onclick="getButtonCheck(this,1)"></asp:CheckBox>
                            <asp:TextBox runat="server" ID="fld_HSEIHBCC" style="display:none;"></asp:TextBox>
                        </td>
                        <td style="vertical-align:middle;text-align:center">
                            <span style="height:30px; float:left;">&nbsp;</span>
                            <asp:CheckBox runat="server"  ID="HSEFIHBNA" onclick="getButtonCheck(this,2)"></asp:CheckBox>
                            <asp:TextBox runat="server" ID="fld_HSEIHBNA" style="display:none;"></asp:TextBox>
                        </td>
                        <td style="vertical-align:middle;text-align:center">
                            <span style="height:30px; float:left;">&nbsp;</span>
                              <asp:Label runat="server" ID="read_HSEIHBRPS" ></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <th style="vertical-align:middle">
                         <span style="height:30px; float:left;">&nbsp;</span>
                        <p style="text-align:center">Other</p>
                        </th>
                        <td style="vertical-align:middle;text-align:center">
                            <span style="height:30px; float:left;">&nbsp;</span>
                            <asp:CheckBox runat="server"  ID="HSEFNA" onclick="getButtonCheck(this,3)"></asp:CheckBox>
                            <asp:TextBox runat="server" ID="fld_HSECKCC" style="display:none;"></asp:TextBox>
                        </td>
                        <td style="vertical-align:middle;text-align:center">
                            <span style="height:30px; float:left;">&nbsp;</span>
                            <asp:CheckBox runat="server"  ID="HSEFCC" onclick="getButtonCheck(this,4)"></asp:CheckBox>
                            <asp:TextBox runat="server" ID="fld_HSECKNA" style="display:none;"></asp:TextBox>
                        </td>
                        <td class="td-content" style="vertical-align:middle">
                            <span style="height:30px; float:left;">&nbsp;</span>
                            <asp:Label runat="server" ID="read_HSECKRPS" ></asp:Label>
                        </td>
                    </tr>
                        </table>


                <p style="font-weight:bold;">HRS Responsibilities</p>
                <table id="HRSTABLE" class="table table-condensed table-bordered">
                    <tr>
                        <th width="30%">
                         <span style="height:30px; float:left;">&nbsp;</span>
                        <p style="text-align:center">item</p>
                        </th>
                        <th width="15%">
                         <span style="height:30px; float:left;">&nbsp;</span>
                        <p style="text-align:center">Completion</p>
                        </th>
                        <th width="15%">
                         <span style="height:30px; float:left;">&nbsp;</span>
                        <p style="text-align:center">N/A</p>
                        </th>
                        <th width="40%">
                            <span style="height:30px; float:left;">&nbsp;</span>
                        <p style="text-align:center">Comments</p>
                        </th>
                    </tr>
                    <tr>
                        <th style="vertical-align:middle">
                         <span style="height:30px; float:left;">&nbsp;</span>
                        <p style="text-align:center">Medical Card</p>
                        </th>
                        <td style="vertical-align:middle;text-align:center">
                            <span style="height:30px; float:left;">&nbsp;</span>
                            <asp:CheckBox runat="server"  ID="HRSMCCC" onclick="getButtonCheck(this,1)"></asp:CheckBox>
                            <asp:TextBox runat="server" ID="fld_HRSMCCC" style="display:none;"></asp:TextBox>
                        </td>
                        <td style="vertical-align:middle;text-align:center">
                            <span style="height:30px; float:left;">&nbsp;</span>
                            <asp:CheckBox runat="server"  ID="HRSMCNA" onclick="getButtonCheck(this,2)"></asp:CheckBox>
                            <asp:TextBox runat="server" ID="fld_HRSMCNA" style="display:none;"></asp:TextBox>
                        </td>
                        <td style="vertical-align:middle">
                            <span style="height:30px; float:left;">&nbsp;</span>
                            <asp:TextBox runat="server" ID="fld_HRSMCComments" Width="94%"></asp:TextBox>
                        </td>
                         <%--<td class="td-content" style="vertical-align:middle">
                            <span style="margin-top:5px;  height:30px; float:left;">&nbsp;</span>
                            <asp:Label runat="server" ID="read_HRSMCSC" ></asp:Label>
                        </td>
                        <td class="td-content" style="vertical-align:middle">
                            <span style="margin-top:5px;  height:30px; float:left;">&nbsp;</span>
                            <asp:Label runat="server" ID="read_HRSMCRPS"></asp:Label>
                        </td>--%>
                    </tr>
                     <tr>
                        <th style="vertical-align:middle;text-align:center">
                         <span style="height:30px; float:left;">&nbsp;</span>
                        <p style="text-align:center">Work permit terminated</p>
                        </th>
                        <td style="vertical-align:middle;text-align:center">
                            <span style="height:30px; float:left;">&nbsp;</span>
                            <asp:CheckBox runat="server"  ID="HRSWPTCC" onclick="getButtonCheck(this,3)"></asp:CheckBox>
                            <asp:TextBox runat="server" ID="fld_HRSWPTCC" style="display:none;"></asp:TextBox>
                        </td>
                        <td style="vertical-align:middle;text-align:center">
                            <span style="height:30px; float:left;">&nbsp;</span>
                            <asp:CheckBox runat="server"  ID="HRSWPTNA" onclick="getButtonCheck(this,4)"></asp:CheckBox>
                            <asp:TextBox runat="server" ID="fld_HRSWPTNA" style="display:none;"></asp:TextBox>
                        </td>
                        <td style="vertical-align:middle">
                            <span style="height:30px; float:left;">&nbsp;</span>
                            <asp:TextBox runat="server" ID="fld_HRSWPTComments" Width="94%"></asp:TextBox>
                        </td>
                       <%-- <td class="td-content" style="vertical-align:middle">
                            <span style="margin-top:5px;  height:30px; float:left;">&nbsp;</span>
                            <asp:Label runat="server" ID="read_HRSWPTSC" ></asp:Label>
                        </td>
                        <td class="td-content" style="vertical-align:middle">
                            <span style="margin-top:5px;  height:30px; float:left;">&nbsp;</span>
                            <asp:Label runat="server" ID="read_HRSWPTRPS"></asp:Label>
                        </td>--%>
                    </tr>
                     <tr>
                        <th style="vertical-align:middle">
                         <span style="height:30px; float:left;">&nbsp;</span>
                        <p style="text-align:center">Give termination letter to employee once check out process are done,SI/PHF Account transition and Verify and clearance of personal state file admin fee</p>
                        </th>
                        <td style="vertical-align:middle;text-align:center">
                            <span style="height:30px; float:left;">&nbsp;</span>
                            <asp:CheckBox runat="server"  ID="HRSGTLTCC" onclick="getButtonCheck(this,5)"></asp:CheckBox>
                            <asp:TextBox runat="server" ID="fld_HRSGTLTCC" style="display:none;"></asp:TextBox>
                        </td>
                        <td style="vertical-align:middle;text-align:center">
                            <span style="height:30px; float:left;">&nbsp;</span>
                            <asp:CheckBox runat="server"  ID="HRSGTLTNA" onclick="getButtonCheck(this,6)"></asp:CheckBox>
                            <asp:TextBox runat="server" ID="fld_HRSGTLTNA" style="display:none;"></asp:TextBox>
                        </td>
                        <td style="vertical-align:middle">
                            <span style="height:30px; float:left;">&nbsp;</span>
                            <asp:TextBox runat="server" ID="fld_HRSGTLTComments" Width="94%"></asp:TextBox>
                        </td>
                        <%--<td class="td-content" style="vertical-align:middle">
                            <span style="margin-top:5px;  height:30px; float:left;">&nbsp;</span>
                            <asp:Label runat="server" ID="read_HRSGTLTSC" ></asp:Label>
                        </td>
                        <td class="td-content" style="vertical-align:middle">
                            <span style="margin-top:5px;  height:30px; float:left;">&nbsp;</span>
                            <asp:Label runat="server" ID="read_HRSGTLTRPS"></asp:Label>
                        </td>--%>
                    </tr>
                     <tr>
                        <th style="vertical-align:middle">
                         <span style="height:30px; float:left;">&nbsp;</span>
                        <p style="text-align:center">Verify payroll have received required termination details and ECN for processing final pay</p>
                        </th>
                        <td style="vertical-align:middle;text-align:center">
                            <span style="height:30px; float:left;">&nbsp;</span>
                            <asp:CheckBox runat="server"  ID="HRSVPHRCC" onclick="getButtonCheck(this,7)"></asp:CheckBox>
                            <asp:TextBox runat="server" ID="fld_HRSVPHRCC" style="display:none;"></asp:TextBox>
                        </td>
                        <td style="vertical-align:middle;text-align:center">
                            <span style="height:30px; float:left;">&nbsp;</span>
                            <asp:CheckBox runat="server"  ID="HRSVPHRNA" onclick="getButtonCheck(this,8)"></asp:CheckBox>
                            <asp:TextBox runat="server" ID="fld_HRSVPHRNA" style="display:none;"></asp:TextBox>
                        </td>
                        <td style="vertical-align:middle">
                            <span style="height:30px; float:left;">&nbsp;</span>
                            <asp:TextBox runat="server" ID="fld_HRSVPHRComments" Width="94%"></asp:TextBox>
                        </td>
                        <%--<td class="td-content" style="vertical-align:middle">
                            <span style="margin-top:5px;  height:30px; float:left;">&nbsp;</span>
                            <asp:Label runat="server" ID="read_HRSVPHRSC" ></asp:Label>
                        </td>
                        <td class="td-content" style="vertical-align:middle">
                            <span style="margin-top:5px;  height:30px; float:left;">&nbsp;</span>
                            <asp:Label runat="server" ID="read_HRSVPHRRPS"></asp:Label>
                        </td>--%>
                    </tr>
                    <tr>
                        <th style="vertical-align:middle">
                         <span style="height:30px; float:left;">&nbsp;</span>
                        <p style="text-align:center">Verify confidentiality agreement</p>
                        </th>
                        <td style="vertical-align:middle;text-align:center">
                            <span style="height:30px; float:left;">&nbsp;</span>
                            <asp:CheckBox runat="server"  ID="HRSVCACC" onclick="getButtonCheck(this,9)"></asp:CheckBox>
                            <asp:TextBox runat="server" ID="fld_HRSVCACC" style="display:none;"></asp:TextBox>
                        </td>
                        <td style="vertical-align:middle;text-align:center">
                            <span style="height:30px; float:left;">&nbsp;</span>
                            <asp:CheckBox runat="server"  ID="HRSVCANA" onclick="getButtonCheck(this,10)"></asp:CheckBox>
                            <asp:TextBox runat="server" ID="fld_HRSVCANA" style="display:none;"></asp:TextBox>
                        </td>
                        <td style="vertical-align:middle">
                            <span style="height:30px; float:left;">&nbsp;</span>
                            <asp:TextBox runat="server" ID="fld_HRSVCAComments" Width="94%"></asp:TextBox>
                        </td>
                       <%-- <td class="td-content" style="vertical-align:middle">
                            <span style="margin-top:5px;  height:30px; float:left;">&nbsp;</span>
                            <asp:Label runat="server" ID="read_HRSVCASC" ></asp:Label>
                        </td>
                        <td class="td-content" style="vertical-align:middle">
                            <span style="margin-top:5px;  height:30px; float:left;">&nbsp;</span>
                            <asp:Label runat="server" ID="read_HRSVCARPS"></asp:Label>
                        </td>--%>
                    </tr>
                    <tr>
                        <th style="vertical-align:middle">
                         <span style="height:30px; float:left;">&nbsp;</span>
                        <p style="text-align:center">Verify Educiational Assistance payback requirement</p>
                        </th>
                        <td style="vertical-align:middle;text-align:center">
                            <span style="height:30px; float:left;">&nbsp;</span>
                            <asp:CheckBox runat="server"  ID="HRSVEAPRCC" onclick="getButtonCheck(this,11)"></asp:CheckBox>
                            <asp:TextBox runat="server" ID="fld_HRSVEAPRCC" style="display:none;"></asp:TextBox>
                        </td>
                        <td style="vertical-align:middle;text-align:center">
                            <span style="height:30px; float:left;">&nbsp;</span>
                            <asp:CheckBox runat="server"  ID="HRSVEAPRNA" onclick="getButtonCheck(this,12)"></asp:CheckBox>
                            <asp:TextBox runat="server" ID="fld_HRSVEAPRNA" style="display:none;"></asp:TextBox>
                        </td>
                        <td style="vertical-align:middle">
                            <span style="height:30px; float:left;">&nbsp;</span>
                            <asp:TextBox runat="server" ID="fld_HRSVEAPRComments" Width="94%"></asp:TextBox>
                        </td>
                        <%--<td class="td-content" style="vertical-align:middle">
                            <span style="margin-top:5px;  height:30px; float:left;">&nbsp;</span>
                            <asp:Label runat="server" ID="read_HRSVEAPRSC" ></asp:Label>
                        </td>
                        <td class="td-content" style="vertical-align:middle"style="vertical-align:middle">
                            <span style="margin-top:5px;  height:30px; float:left;">&nbsp;</span>
                            <asp:Label runat="server" ID="read_HRSVEAPRRPS"></asp:Label>
                        </td>--%>
                    </tr>
                </table>
            </div>
            <div class="row" style="display:none;">
                <attach:attachments id="Attachments1" runat="server"></attach:attachments>
            </div>
            <div class="row">
             <ah:approvalhistory id="ApprovalHistory1" showaction="true" runat="server"></ah:approvalhistory>
            </div>
        </div>
        <%--<div id="btnDiv" runat="server"   >
            <table style="width: 100%;" >
                <tr>
                    <td align="center"  >
                        <table>
                            <tr>
                                <td> 
                                <input type="button"  class="btn" value="Complete" onclick="submitPageReview('0')" />
                                </td>
                            </tr>
                       </table>
                    </td>
                 </tr>
            </table>
        </div>
        <div style="display:none;">--%>
        <btn:buttonlist id="ButtonList1" runat="server"></btn:buttonlist>
        <div style="display:none">
        <asp:TextBox runat="server" ID="fld_TRSummary"></asp:TextBox>
        <asp:HiddenField runat="server" ID="hdIncident" />
        <asp:HiddenField runat="server" ID="hdPrint" />
        </div>
    </form>
</body>
</html>


