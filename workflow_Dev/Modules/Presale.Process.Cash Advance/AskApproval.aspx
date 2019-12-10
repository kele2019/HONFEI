<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AskApproval.aspx.cs" Inherits="Presale.Process.Cash_Advance.AskApproval" %>

<%@ Register Src="/Modules/Ultimus.UWF.Form.ProcessControl/UserInfo.ascx" TagName="UserInfo" TagPrefix="ui" %>
<%@ Register Src="/Modules/Ultimus.UWF.Form.ProcessControl/ApprovalHistory.ascx" TagName="ApprovalHistory" TagPrefix="ah" %>
<%@ Register Src="/Modules/Ultimus.UWF.Form.ProcessControl/Attachments.ascx" TagName="Attachments" TagPrefix="attach" %>
<%@ Register Src="/Modules/Ultimus.UWF.Form.ProcessControl/ButtonList.ascx" TagName="ButtonList" TagPrefix="btn" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
        <meta http-equiv="X-UA-Compatible"  content="IE=EmulateIE8" />

    <title>借款申请 Cash Advance</title>
    <script type="text/javascript" src="/Assets/js/common.js"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            $("#ButtonList1_btnAsk").hide();
            $("#ButtonList1_var_AskForAccount").val("No");
            if (request("Type") == "myapproval") {
                $("#btnDiv").hide();
            }
            $("#ButtonList1_btnBack").hide();
        });
        function submitPageReview(obj) {
            $("#ButtonList1_btnSubmit").val("确定提交/Confirm Submit？");
            $("#ApprovalHistory1_txtSpecialAction").val("Inquire");
            $("#ButtonList1_btnSubmit").click();
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
     <div id="myDiv" class="container">
            <div class="row">
                <ui:userinfo id="UserInfo1" processtitle="借款申请(Cash Advance Request)" processprefix="CA" tablename="PROC_CashAdvance"
                    runat="server"  ></ui:userinfo>
            </div>
            <div class="row">
                <table class="table table-condensed table-bordered">
                    <tr>
                        <td class="banner" colspan="6">详细信息 Cash Detail
                        </td>
                    </tr>
                    <tr>
                        <td class="td-label" style="width:100px">
                          借款原因
                            <br /> Cash Advance Reason： 
                        </td>
                        <td class="td-content" colspan="3">
                        <asp:Label runat="server" ID="read_BorrowReson"  ></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="td-label"> 
                            借款金额 <br />
                           Cash Advance Applied (RMB)： 
                        </td>
                        <td class="td-content" >
                            <asp:Label runat="server" ID="read_BorrowAmount" ></asp:Label>
                        </td>
                        <td class="td-label"> 
                            币种 <br />
                           Currency：  
                        </td>
                        <td class="td-content" >
                            <asp:Label runat="server" ID="read_Currency"   ></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="td-label"> 
                            银行卡号 <br />
                           Bank Account： 
                        </td>
                        <td class="td-content" >
                            <asp:Label runat="server" ID="read_BankNo"  ></asp:Label>
                        </td>
                        <td class="td-label"> 
                            开户银行 <br />
                           Bank Name：  
                        </td>
                        <td class="td-content" >
                            <asp:Label runat="server" ID="read_BankName"  ></asp:Label>
                        </td>
                    </tr>
                </table>
            </div>
            <div  style="display:none" >
                <div class="row" style="width: 120%">

                    <table class="table table-condensed table-bordered tablerequired" style="width:83%" id="tbDetail">
                        <tr>
                            <td class="banner" colspan="5">借款明细 Detail of Cash Advance
                            </td>
                        </tr>
                        <tr>
                            <th > 序号 
                               No.
                            </th>
                            <th >金额Amount
                                 <span class="red">*</span>
                            </th>
                            <th >项目Item
                            </th>
                            <th >备注 Comments
                            </th>
                            
                        </tr>
                         <tbody id="tabletbodyDetail">
                         
                          </tbody>
                    </table>
                </div>

            </div>
            <div class="row">
                <attach:attachments id="Attachments1" ReadOnly="false" runat="server"></attach:attachments>
            </div>
            <div class="row">
             <ah:approvalhistory id="ApprovalHistory1" showaction="true" runat="server"></ah:approvalhistory>
            </div>
        </div>
        <div id="btnDiv" runat="server"   >
        <table style="width: 100%;" >
        <tr  width="500">
        <td align="center"  >
            <table>
                <tr>
                    <td> 
                    <input type="button"  class="btn" value="回答\Answer" onclick="submitPageReview('0')" />
                    </td>
                    </tr>
                    </table>
                    </td>
                    </tr>
                    </table>
        </div>
        <div style="display:none">
        <btn:buttonlist id="ButtonList1" runat="server"></btn:buttonlist>
        <asp:HiddenField runat="server" ID="hdIncident" />
        <asp:HiddenField runat="server" ID="hdPrint" />
        </div>
    </form>
</body>
</html>
