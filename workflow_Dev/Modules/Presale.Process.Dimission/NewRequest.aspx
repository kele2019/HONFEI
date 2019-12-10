<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="NewRequest.aspx.cs" Inherits="Presale.Process.Dimission.NewRequest" %>
<%@ Register Src="/Modules/Ultimus.UWF.Form.ProcessControl/UserInfo.ascx" TagName="UserInfo" TagPrefix="ui" %>
<%@ Register Src="/Modules/Ultimus.UWF.Form.ProcessControl/ApprovalHistory.ascx" TagName="ApprovalHistory" TagPrefix="ah" %>
<%@ Register Src="/Modules/Ultimus.UWF.Form.ProcessControl/Attachments.ascx" TagName="Attachments" TagPrefix="attach" %>
<%@ Register Src="/Modules/Ultimus.UWF.Form.ProcessControl/ButtonList.ascx" TagName="ButtonList" TagPrefix="btn" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Exit Interview Process</title>
     <meta http-equiv="X-UA-Compatible"  content="IE=EmulateIE8" />
    <script type="text/javascript" src="/Assets/js/common.js"></script>
    <script type="text/javascript">
    
     function CheckedValue(flag, vindex) {
            $('#fld_' + flag).val(vindex);
        }
        $(document).ready(function () {
            var EvalueatinValue = $("#fld_EvalueatinValue").val();
            if (EvalueatinValue != "")
                $("input[name^='EvalueatinValue']").eq((EvalueatinValue - 5) * (-1)).attr("checked", true);
});
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div id="myDiv" class="container">
     <div class="row">
                <ui:userinfo id="UserInfo1" processtitle="Exit Interview" processprefix="ED" tablename="PROC_Dimission"
                    runat="server"  ></ui:userinfo>
     </div> 
            
     <div class="row">

      
      
      <p>The purpose of this questionnaire is to help the company determine reasons for employee turnover. The company appreciates your efforts in answering these questions frankly and your answer will not be used as part of any decision regarding future re-employment. Also, nothing in this form will be revealed to any other company.</p> 
      <p> There are two parts to this form, the first, simple questions, the second, give suggestion for changes of improvement. Please rate according to the appropriate tables.</p>
      <p>Part One:<%--第一部分--%> </p>
      <p>These questions will help us address areas that need improvement in our company. Please rate your reactions according to the following point system.
      <%--这些问题将帮助我们致力于公司管理的不断完善。请您依照下列评分标准评分：--%>
      </p>

       <table class="table table-condensed table-bordered">
       <tr>
       <th>
       TABLE FOR EVALUATIN OF ELEMENTS
         </th>
       </tr>
       <tr><td><input type="radio" onclick="CheckedValue('EvalueatinValue','5')" name="EvalueatinValue" /> Excellent <%--很好--%> </td> </tr>
       <tr><td><input type="radio" onclick="CheckedValue('EvalueatinValue','4')" name="EvalueatinValue"/> Good  <%--良好--%></td> </tr>
       <tr><td><input type="radio" onclick="CheckedValue('EvalueatinValue','3')" name="EvalueatinValue"/> Satisfactory  <%--满意--%></td> </tr>
       <tr><td><input type="radio" onclick="CheckedValue('EvalueatinValue','2')" name="EvalueatinValue"/> Fair  <%--一般--%></td> </tr>
       <tr><td><input type="radio" onclick="CheckedValue('EvalueatinValue','1')" name="EvalueatinValue"/> Poor   <%--差--%>
       <asp:TextBox runat="server" ID="fld_EvalueatinValue" style="display:none"></asp:TextBox>
       </td> </tr>

       <tr>
       <th> General Environment <%--总体环境--%></th>
       </tr>
       <tr>
      <th> How would you rate this company as a place to work?<%--您如何评价公司作为您就业的一个选择单位？--%></th>
       </tr>
       <tr>
       <td>
       <asp:TextBox runat="server" ID="fld_Companyplace" TextMode="MultiLine" Rows="5" style="width:98%"></asp:TextBox>
       </td>
       </tr>


        <tr>
      <th>What is your overall view of the company’s policies and procedures?<%--您对公司政策及规定的总体看法？--%></th>
       </tr>
       <tr>
       <td>
       <asp:TextBox runat="server" ID="fld_Companypolicies" TextMode="MultiLine" Rows="5" style="width:98%"></asp:TextBox>
       </td>
       </tr>


       
        <tr>
      <th>  How do you rate the working conditions and physical facilities?<%--您认为公司工作条件及硬件设施如何？--%></th>
       </tr>
       <tr>
       <td>
       <asp:TextBox runat="server" ID="fld_Companyphysical" TextMode="MultiLine" Rows="5" style="width:98%"></asp:TextBox>
       </td>
       </tr>

       <tr>
       <th>Compensation & Benefit <%--薪酬及福利--%></th>
       </tr>
       <tr>
       <th>
       Compared with other companies, how do you rate our benefits package?<%--与其他公司比较，您认为公司的福利条件如何？--%>
       </th>
       </tr>
        <tr>
       <td>
       <asp:TextBox runat="server" ID="fld_Companybenefits" TextMode="MultiLine" Rows="5" style="width:98%"></asp:TextBox>
       </td>
       </tr>


       <tr>
       <th>
       How do you feel about pay provide by the Company?<%--您认为公司的工资待遇如何？--%>
       </th>
       </tr>
        <tr>
       <td>
       <asp:TextBox runat="server" ID="fld_Companyprovide" TextMode="MultiLine" Rows="5" style="width:98%"></asp:TextBox>
       </td>
       </tr>
      
      <tr>
      <th>Training Opportunities <%--培训机会--%></th>
      </tr>


       <tr>
       <th>
      How would you rate the training you received?<%--您对在公司接受的相关培训给予如何评价？--%> </th>
       </tr>
        <tr>
       <td>
       <asp:TextBox runat="server" ID="fld_Companytraining" TextMode="MultiLine" Rows="5" style="width:98%"></asp:TextBox>
       </td>
       </tr>

       <tr>
       <th>
       Were you given enough information, in the early stages of your employment, about the Company, such as benefits, practices and policies, organization, orientation, etc?
       <%-- 在您加入公司初期，您是否有被提供有关公司各方面充足的信息？比如：福利、惯例、 政策、组织架构及工作方向等？--%>
       </th>
       </tr>
        <tr>
       <td>
       <asp:TextBox runat="server" ID="fld_Companyenough" TextMode="MultiLine" Rows="5" style="width:98%"></asp:TextBox>
       </td>
       </tr>


       <tr>
       <th>
       How do you rate subsequent training opportunities to improve your skills and opportunities?<%--您如何评价：在职期间得到的有关技能和发展提升的培训机会？--%>
       </th>
       </tr>
        <tr>
       <td>
       <asp:TextBox runat="server" ID="fld_Companyimprove" TextMode="MultiLine" Rows="5" style="width:98%"></asp:TextBox>
       </td>
       </tr>
       </table>

      <p> Part Two:<%--第二部分--%></p>
      <p>
      Please give suggestion/recommendation on what area(s) the company need to improve and please state your opinion in detail.
<%--请对你认为公司需要改善的方面给出详细的建议、意见。--%></p>
       <asp:TextBox runat="server" ID="fld_Personalsuggestion" TextMode="MultiLine" Rows="5" style="width:98%"></asp:TextBox>
      <p>
      Please indicate the reason why you are leaving the Company?<%--请简要说明您离开公司的原因？--%></p>
       <asp:TextBox runat="server" ID="fld_Personalreason" TextMode="MultiLine" Rows="5" style="width:98%"></asp:TextBox>


       <p>If reason for leaving is another job, please provide:
<%--如果您离职的理由是因为找到了其他工作，方便的话请提供以下信息：--%></p>
 <table class="table table-condensed table-bordered">
       <tr>
       <th>Company Name<%--公司名称--%>:</th>
       <td><asp:TextBox runat="server" ID="fld_NextCompanyName" MaxLength="100"  Width="95%"></asp:TextBox></td>
       
       <th>Job Title<%--职位名称:--%>:</th>
       <td>
       <asp:TextBox runat="server" ID="fld_NextJobtitle" MaxLength="100" Width="95%"></asp:TextBox> </td>
       </tr>


       <tr>
       <th>Interview By<%--面试负责人:--%>:</th>
       <td><asp:TextBox runat="server" ID="fld_NextInterview" MaxLength="100" Width="95%"></asp:TextBox></td>
       
       <th>Date<%--日期--%>:</th>
       <td>
       <asp:TextBox runat="server" ID="fld_NextJoinDD" MaxLength="100" Width="95%"></asp:TextBox> </td>
       </tr>

</table>


     </div> 
    </div>
    </form>
</body>
</html>
