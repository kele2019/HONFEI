<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="HRGApproval.aspx.cs" Inherits="Presale.Process.EmployeeTermination.HRGApproval" %>
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
        function getButtonCheck(obj, index) {
            if (index == "1") {
                if ($(obj).attr("checked")) {
                    $("#fld_HRGEICC").val("CollectedCancelled");
                }
                else {
                    $("#fld_HRGEICC").val("");
                }
            }
            if (index == "2") {
                if ($(obj).attr("checked")) {
                    $("#fld_HRGEINA").val("NA");
                }
                else {
                    $("#fld_HRGEINA").val("");
                }
            }
            if (index == "3") {
                if ($(obj).attr("checked")) {
                    $("#fld_HRGWNCACC").val("CollectedCancelled");
                }
                else {
                    $("#fld_HRGWNCACC").val("");
                }
            }
            if (index == "4") {
                if ($(obj).attr("checked")) {
                    $("#fld_HRGWNCANA").val("NA");
                }
                else {
                    $("#fld_HRGWNCANA").val("");
                }
            }
            if (index == "5") {
                if ($(obj).attr("checked")) {
                    $("#fld_HRGNCPCC").val("CollectedCancelled");
                }
                else {
                    $("#fld_HRGNCPCC").val("");
                }
            }
            if (index == "6") {
                if ($(obj).attr("checked")) {
                    $("#fld_HRGNCPNA").val("NA");
                }
                else {
                    $("#fld_HRGNCPNA").val("");
                }
            }
        }



        $(document).ready(function () {
            var Companyplace = $("#read_Companyplace").val();
            if (Companyplace != "")
                $("input[name^='Companyplace']").eq((Companyplace - 5) * (-1)).attr("checked", true);

            var Companypolicies = $("#read_Companypolicies").val();
            if (Companypolicies != "")
                $("input[name^='Companypolicies']").eq((Companypolicies - 5) * (-1)).attr("checked", true);

            var Companyphysical = $("#read_Companyphysical").val();
            if (Companyphysical != "")
                $("input[name^='Companyphysical']").eq((Companyphysical - 5) * (-1)).attr("checked", true);

            var Companybenefits = $("#read_Companybenefits").val();
            if (Companybenefits != "")
                $("input[name^='Companybenefits']").eq((Companybenefits - 5) * (-1)).attr("checked", true);

            var Companyprovide = $("#read_Companyprovide").val();
            if (Companyprovide != "")
                $("input[name^='Companyprovide']").eq((Companyprovide - 5) * (-1)).attr("checked", true);

            var Companytraining = $("#read_Companytraining").val();
            if (Companytraining != "")
                $("input[name^='Companytraining']").eq((Companytraining - 5) * (-1)).attr("checked", true);

            var Companyenough = $("#read_Companyenough").val();
            if (Companyenough != "")
                $("input[name^='Companyenough']").eq((Companyenough - 5) * (-1)).attr("checked", true);

            var Companyimprove = $("#read_Companyimprove").val();
            if (Companyimprove != "")
                $("input[name^='Companyimprove']").eq((Companyimprove - 5) * (-1)).attr("checked", true);

        });
    </script>
</head>
<body>
    <form id="form1" runat="server">
     <div id="myDiv" class="container">
            <div class="row">
                <ui:userinfo id="UserInfo1" processtitle="Employee Termination-Check Out List" processprefix="ETCO" tablename="PROC_EmployeeTerminationCheckOut"
                    runat="server"  ></ui:userinfo>
            </div>
            <div class="row">
                <p style="font-weight:bold;">Termination Employee（"<span style=" background:red;">&nbsp;</span>" must write） </p>
                <table class="table table-condensed table-bordered">
                    <tr>
                        <td class="td-label">
                         <span style=" background:red;  height:30px; float:left;">&nbsp;</span>
                        <p style="text-align:center">Termination Employee</p>
                        </td>
                        <td class="td-content" colspan="7" >
                             
                            <asp:Label runat="server" ID="read_TerminationEmployee" style="display:block;"></asp:Label>
                            <asp:TextBox runat="server" ID="read_TerminationEmployeeValue" style="display:none;"></asp:TextBox>
                        </td>
                    </tr>
                </table>
            </div>

            <div class="row">
                <p style="font-weight:bold;">HRG Responsibilities（"<span style=" background:red;">&nbsp;</span>" must write） </p>
                <table class="table table-condensed table-bordered">
                    <tr>
                        <td class="td-label">
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
                        <%--<td class="td-label">
                         <span style="height:30px; float:left;">&nbsp;</span>
                        <p style="text-align:center">Serial/Card</p>
                        </td>--%>
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
                            <span style="background:red;margin-top:5px;  height:30px; float:left;">&nbsp;</span>
                            <asp:CheckBox runat="server"  ID="HRGEICC" onclick="getButtonCheck(this,1)" style="margin-left:50%;"></asp:CheckBox>
                            <asp:TextBox runat="server" ID="fld_HRGEICC" style="display:none;"></asp:TextBox>
                        </td>
                        <td class="td-content" style="vertical-align:middle">
                            <span style="background:red;margin-top:5px;  height:30px; float:left;">&nbsp;</span>
                            <asp:CheckBox runat="server"  ID="HRGEINA" onclick="getButtonCheck(this,2)" style="margin-left:50%;"></asp:CheckBox>
                            <asp:TextBox runat="server" ID="fld_HRGEINA" style="display:none;"></asp:TextBox>
                        </td>
                        <%--<td class="td-content">
                            <span style="background:red;margin-top:5px;  height:30px; float:left;">&nbsp;</span>
                            <asp:TextBox runat="server" ID="fld_HRGEISC" CssClass="validate[required]"></asp:TextBox>
                        </td>--%>
                        <td class="td-content" style="vertical-align:middle">
                            <span style="background:red;margin-top:5px;  height:30px; float:left;">&nbsp;</span>
                            <asp:TextBox runat="server" ID="fld_HRGEIRPS" CssClass="validate[required]"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="td-label" style="vertical-align:middle">
                         <span style="height:30px; float:left;">&nbsp;</span>
                        <p style="text-align:center">Waive Non Compete Agreement</p>
                        </td>
                        <td class="td-content" style="vertical-align:middle">
                            <span style="background:red;margin-top:5px;  height:30px; float:left;">&nbsp;</span>
                            <asp:CheckBox runat="server"  ID="HRGWNCACC" onclick="getButtonCheck(this,3)" style="margin-left:50%;"></asp:CheckBox>
                            <asp:TextBox runat="server" ID="fld_HRGWNCACC" style="display:none;"></asp:TextBox>
                        </td>
                        <td class="td-content" style="vertical-align:middle">
                            <span style="background:red;margin-top:5px;  height:30px; float:left;">&nbsp;</span>
                            <asp:CheckBox runat="server"  ID="HRGWNCANA" onclick="getButtonCheck(this,4)" style="margin-left:50%;"></asp:CheckBox>
                            <asp:TextBox runat="server" ID="fld_HRGWNCANA" style="display:none;"></asp:TextBox>
                        </td>
                        <%--<td class="td-content">
                            <span style="background:red;margin-top:5px;  height:30px; float:left;">&nbsp;</span>
                            <asp:TextBox runat="server" ID="fld_HRGWNCASC" CssClass="validate[required]"></asp:TextBox>
                        </td>--%>
                        <td class="td-content" style="vertical-align:middle">
                            <span style="background:red;margin-top:5px;  height:30px; float:left;">&nbsp;</span>
                            <asp:TextBox runat="server" ID="fld_HRGWNCARPS" CssClass="validate[required]"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="td-label" style="vertical-align:middle">
                         <span style="height:30px; float:left;">&nbsp;</span>
                        <p style="text-align:center">Non Compete payment</p>
                        </td>
                        <td class="td-content" style="vertical-align:middle">
                            <span style="background:red;margin-top:5px;  height:30px; float:left;">&nbsp;</span>
                            <asp:CheckBox runat="server"  ID="HRGNCPCC" onclick="getButtonCheck(this,5)" style="margin-left:50%;"></asp:CheckBox>
                            <asp:TextBox runat="server" ID="fld_HRGNCPCC" style="display:none;"></asp:TextBox>
                        </td>
                        <td class="td-content" style="vertical-align:middle">
                            <span style="background:red;margin-top:5px;  height:30px; float:left;">&nbsp;</span>
                            <asp:CheckBox runat="server"  ID="HRGNCPNA" onclick="getButtonCheck(this,6)" style="margin-left:50%;"></asp:CheckBox>
                            <asp:TextBox runat="server" ID="fld_HRGNCPNA" style="display:none;"></asp:TextBox>
                        </td>
                        <%--<td class="td-content">
                            <span style="background:red;margin-top:5px;  height:30px; float:left;">&nbsp;</span>
                            <asp:TextBox runat="server" ID="fld_HRGNCPSC"  CssClass="validate[required]"></asp:TextBox>
                        </td>--%>
                        <td class="td-content" style="vertical-align:middle">
                            <span style="background:red;margin-top:5px;  height:30px; float:left;">&nbsp;</span>
                            <asp:TextBox runat="server" ID="fld_HRGNCPRPS"  CssClass="validate[required]"></asp:TextBox>
                        </td>
                    </tr>
                </table>
            </div>



             <div class="row">

     
      
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
      <input type="radio" disabled="disabled" onclick="CheckedValue('Companyplace','4')" name="Companyplace"   />4&nbsp;
      <input type="radio" disabled="disabled" onclick="CheckedValue('Companyplace','3')" name="Companyplace"  />3&nbsp;
      <input type="radio" disabled="disabled" onclick="CheckedValue('Companyplace','2')" name="Companyplace"  />2&nbsp;
      <input type="radio" disabled="disabled" onclick="CheckedValue('Companyplace','1')" name="Companyplace"  />1&nbsp;
      <asp:TextBox runat="server" ID="read_Companyplace" style="display:none"></asp:TextBox>
      </td>
       </tr>
        <tr>
      <td>What is your overall view of the company’s policies and procedures?<%--您对公司政策及规定的总体看法？--%>
      <br />
       <input type="radio" disabled="disabled" onclick="CheckedValue('Companypolicies','5')" name="Companypolicies" />5&nbsp;
      <input type="radio"  disabled="disabled" onclick="CheckedValue('Companypolicies','4')" name="Companypolicies"  />4&nbsp;
      <input type="radio"  disabled="disabled" onclick="CheckedValue('Companypolicies','3')" name="Companypolicies"  />3&nbsp;
      <input type="radio"  disabled="disabled" onclick="CheckedValue('Companypolicies','2')" name="Companypolicies"  />2&nbsp;
      <input type="radio"  disabled="disabled" onclick="CheckedValue('Companypolicies','1')" name="Companypolicies"  />1&nbsp;
      <asp:TextBox runat="server" ID="read_Companypolicies" style="display:none"></asp:TextBox>
      
      </td>
       </tr>
        <tr>
      <td>  How do you rate the working conditions and physical facilities?<%--您认为公司工作条件及硬件设施如何？--%>
      <br />
       <input type="radio" disabled="disabled" onclick="CheckedValue('Companyphysical','5')" name="Companyphysical" />5&nbsp;
      <input type="radio"  disabled="disabled" onclick="CheckedValue('Companyphysical','4')" name="Companyphysical"  />4&nbsp;
      <input type="radio"  disabled="disabled" onclick="CheckedValue('Companyphysical','3')" name="Companyphysical"  />3&nbsp;
      <input type="radio"  disabled="disabled" onclick="CheckedValue('Companyphysical','2')" name="Companyphysical"  />2&nbsp;
      <input type="radio"  disabled="disabled" onclick="CheckedValue('Companyphysical','1')" name="Companyphysical"  />1&nbsp;
      <asp:TextBox runat="server" ID="read_Companyphysical" style="display:none"></asp:TextBox>
      </td>
       </tr>

       <tr>
       <th>Compensation & Benefit <%--薪酬及福利--%></th>
       </tr>
       <tr>
       <td>
       Compared with other companies, how do you rate our benefits package?<%--与其他公司比较，您认为公司的福利条件如何？--%>
      <br /> <input type="radio" disabled="disabled" onclick="CheckedValue('Companybenefits','5')" name="Companybenefits" />5&nbsp;
      <input type="radio" disabled="disabled" onclick="CheckedValue('Companybenefits','4')" name="Companybenefits"  />4&nbsp;
      <input type="radio" disabled="disabled" onclick="CheckedValue('Companybenefits','3')" name="Companybenefits"  />3&nbsp;
      <input type="radio" disabled="disabled" onclick="CheckedValue('Companybenefits','2')" name="Companybenefits"  />2&nbsp;
      <input type="radio" disabled="disabled" onclick="CheckedValue('Companybenefits','1')" name="Companybenefits"  />1&nbsp;
      <asp:TextBox runat="server" ID="read_Companybenefits"  style="display:none"></asp:TextBox>
       </td>
       </tr>
       


       <tr>
       <td>
       How do you feel about pay provide by the Company?<%--您认为公司的工资待遇如何？--%>
     <br /> <input type="radio"disabled="disabled" onclick="CheckedValue('Companyprovide','5')" name="Companyprovide" />5&nbsp;
      <input type="radio" disabled="disabled" onclick="CheckedValue('Companyprovide','4')" name="Companyprovide"  />4&nbsp;
      <input type="radio" disabled="disabled" onclick="CheckedValue('Companyprovide','3')" name="Companyprovide"  />3&nbsp;
      <input type="radio" disabled="disabled" onclick="CheckedValue('Companyprovide','2')" name="Companyprovide"  />2&nbsp;
      <input type="radio" disabled="disabled" onclick="CheckedValue('Companyprovide','1')" name="Companyprovide"  />1&nbsp;
      <asp:TextBox runat="server" ID="read_Companyprovide"  style="display:none"></asp:TextBox>
       </td>
       </tr>
         
      
      <tr>
      <th>Training Opportunities <%--培训机会--%></th>
      </tr>
       
       <tr>
       <td>
      How would you rate the training you received?<%--您对在公司接受的相关培训给予如何评价？--%> 
     <br /> <input type="radio" disabled="disabled" onclick="CheckedValue('Companytraining','5')" name="Companytraining" />5&nbsp;
      <input type="radio" disabled="disabled" onclick="CheckedValue('Companytraining','4')" name="Companytraining"  />4&nbsp;
      <input type="radio" disabled="disabled" onclick="CheckedValue('Companytraining','3')" name="Companytraining"  />3&nbsp;
      <input type="radio" disabled="disabled" onclick="CheckedValue('Companytraining','2')" name="Companytraining"  />2&nbsp;
      <input type="radio" disabled="disabled" onclick="CheckedValue('Companytraining','1')" name="Companytraining"  />1&nbsp;
      <asp:TextBox runat="server" ID="read_Companytraining"  style="display:none"></asp:TextBox>
      </td>
       </tr>

       <tr>
       <td>
       Were you given enough information, in the early stages of your employment, about the Company, such as benefits, practices and policies, organization, orientation, etc?
       <%-- 在您加入公司初期，您是否有被提供有关公司各方面充足的信息？比如：福利、惯例、 政策、组织架构及工作方向等？--%>
     <br /> <input type="radio" disabled="disabled" onclick="CheckedValue('Companyenough','5')" name="Companyenough" />5&nbsp;
      <input type="radio" disabled="disabled" onclick="CheckedValue('Companyenough','4')" name="Companyenough"  />4&nbsp;
      <input type="radio" disabled="disabled" onclick="CheckedValue('Companyenough','3')" name="Companyenough"  />3&nbsp;
      <input type="radio" disabled="disabled" onclick="CheckedValue('Companyenough','2')" name="Companyenough"  />2&nbsp;
      <input type="radio" disabled="disabled" onclick="CheckedValue('Companyenough','1')" name="Companyenough"  />1&nbsp;
      <asp:TextBox runat="server" ID="read_Companyenough"  style="display:none"></asp:TextBox>
       </td>
       </tr>
         
       <tr>
       <td>
       How do you rate subsequent training opportunities to improve your skills and opportunities?<%--您如何评价：在职期间得到的有关技能和发展提升的培训机会？--%>
      <br /><input type="radio" disabled="disabled" onclick="CheckedValue('Companyimprove','5')" name="Companyimprove" />5&nbsp;
      <input type="radio" disabled="disabled" onclick="CheckedValue('Companyimprove','4')" name="Companyimprove"  />4&nbsp;
      <input type="radio" disabled="disabled" onclick="CheckedValue('Companyimprove','3')" name="Companyimprove"  />3&nbsp;
      <input type="radio" disabled="disabled" onclick="CheckedValue('Companyimprove','2')" name="Companyimprove"  />2&nbsp;
      <input type="radio" disabled="disabled" onclick="CheckedValue('Companyimprove','1')" name="Companyimprove"  />1&nbsp;
      <asp:TextBox runat="server" ID="read_Companyimprove"  style="display:none"></asp:TextBox>
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
<asp:Label runat="server" ID="read_Personalsuggestion"  ></asp:Label>
</td>
 </tr>
 <tr>
 <th colspan="4">
   Please indicate the reason why you are leaving the Company?<%--请简要说明您离开公司的原因？--%> 
 </th>
 </tr>
  <tr>
 <td colspan="4">
  <asp:Label runat="server" ID="read_Personalreason"  ></asp:Label>
 </td>
 </tr> 


 <tr>
 <th colspan="4" style="text-align:left">
   If reason for leaving is another job, please provide:
<%--如果您离职的理由是因为找到了其他工作，方便的话请提供以下信息：--%> 
 </th>
 </tr>
       <tr>
       <th style="width:17%">Company Name<%--公司名称--%>:</th>
       <td style="width:33%"><asp:Label runat="server" ID="read_NextCompanyName"  ></asp:Label></td>
       
       <th style="width:17%">Job Title<%--职位名称:--%>:</th>
       <td style="width:33%">
       <asp:Label runat="server" ID="read_NextJobtitle" ></asp:Label> </td>
       </tr>


       <tr>
       <th>Interview By<%--面试负责人:--%>:</th>
       <td><asp:Label runat="server" ID="read_NextInterview" ></asp:Label></td>
       
       <th>Date<%--日期--%>:</th>
       <td>
       <asp:Label runat="server" ID="rea_NextJoinDD"  ></asp:Label> </td>
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
        <btn:buttonlist id="ButtonList1" runat="server"></btn:buttonlist>
        <div style="display:none">
        <asp:TextBox runat="server" ID="fld_TRSummary"></asp:TextBox>
        <asp:HiddenField runat="server" ID="hdIncident" />
        <asp:HiddenField runat="server" ID="hdPrint" />
        </div>
    </form>
</body>
</html>






