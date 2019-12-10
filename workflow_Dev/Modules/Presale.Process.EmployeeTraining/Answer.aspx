<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Answer.aspx.cs" Inherits="Presale.Process.EmployeeTraining.Answer" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
     <base target="_self"></base>
    <meta http-equiv="Expires" content="-1" />
    <meta http-equiv="PRAGMA" content="NO-CACHE" />
    <meta http-equiv="Cache-Control" content="no-cache" />
    <title>Papers</title>
    <script type="text/javascript" src="/Assets/js/common.js"></script>
    <script type="text/javascript">
        function beforeSubmit() {
            var amount = 0;
            var amountOrg = 0;
            $("#practisediv").find("table").each(function (i, Etr) {
                var value = $(Etr).find("tr").eq(0).find("td").children().last().val();
                var valueOrg = $(Etr).find("tr").eq(1).find("td").find(".answer").text();
                amountOrg++;
                if (value == valueOrg) {
                    amount++;
                }
            });
            if (amount / amountOrg * 100 < 80) {
                alert("You failed in this exam.Please try it again");
                return false;
            }
            $("#successOrNot").val("success");
            var returnJson = "[{'throughAnswer':'success'}]";
            window.returnValue = returnJson;
            window.close();
        }
        function CloseForm() {
            $("#successOrNot").val("failed");
            var returnJson = "[{'throughAnswer':'failed'}]";
            window.returnValue = returnJson;
            window.close();
        }
        function getButtonCheck(obj, index) {
            var value = "";
            $(obj).parent().parent().find(".checkbox").each(function (i, Etr) {
                if ($(Etr).children().attr("checked")) {
                    value += $(Etr).children().text() + ",";
                }
            });
            value = value.split(",").sort().join("");
            $(obj).parent().parent().parent().prev().find("td").children().last().val(value);
        }
        $(document).ready(function () {
            $("#practisediv").find("table").each(function (i, Etr) {
                if ($(Etr).find("tr").eq(1).find(".answerTextA").text() == "") {
                    $(Etr).find("tr").eq(1).find(".answerTextA").hide();
                    $(Etr).find("tr").eq(1).find(".answerTextA").prev().hide();
                }
                if ($(Etr).find("tr").eq(1).find(".answerTextB").text() == "") {
                    $(Etr).find("tr").eq(1).find(".answerTextB").hide();
                    $(Etr).find("tr").eq(1).find(".answerTextB").prev().hide();
                }
                if ($(Etr).find("tr").eq(1).find(".answerTextC").text() == "") {
                    $(Etr).find("tr").eq(1).find(".answerTextC").hide();
                    $(Etr).find("tr").eq(1).find(".answerTextC").prev().hide();
                }
                if ($(Etr).find("tr").eq(1).find(".answerTextD").text() == "") {
                    $(Etr).find("tr").eq(1).find(".answerTextD").hide();
                    $(Etr).find("tr").eq(1).find(".answerTextD").prev().hide();
                }
                if ($(Etr).find("tr").eq(1).find(".answerTextE").text() == "") {
                    $(Etr).find("tr").eq(1).find(".answerTextE").hide();
                    $(Etr).find("tr").eq(1).find(".answerTextE").prev().hide();
                }
                if ($(Etr).find("tr").eq(1).find(".answerTextF").text() == "") {
                    $(Etr).find("tr").eq(1).find(".answerTextF").hide();
                    $(Etr).find("tr").eq(1).find(".answerTextF").prev().hide();
                }
            });
        });
    </script>
</head>
<body>
    <form id="form1" runat="server" style="margin-top:5%;margin-bottom:5%;">
      <div id="myDiv" class="container" style="width:90%"> 
            <div class="row">
                <p style="font-weight:bold;">Practise</p>
                <div id="practisediv">
                <asp:Repeater  ID="Repeater1" runat="server" >
                     <ItemTemplate> 
                        <table class="table table-condensed table-bordered" id="Question1">
                            <tr>
                                <td class="td-content">
                                    <%# Container.ItemIndex + 1 %>,<asp:Label runat="server" ID="Question" Text='<%# Eval("Question") %>' ></asp:Label>
                                    <asp:HiddenField runat="server" ID="hdRowIndex" Value='<%#Eval("RowIndex") %>' />
                                    <%--<asp:TextBox runat="server" ID="showUserAnswer" Value='<%# Eval("showUserAnswer").ToString()==""?"":Eval("showUserAnswer") %>' style="float:right;width:5%;background-color:White;" ReadOnly="true"></asp:TextBox>--%>
                                    <asp:TextBox runat="server" ID="showUserAnswer" onfocus="this.blur()" style="float:right;width:7%;background-color:White;" ></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td class="td-content" >
                                    <asp:CheckBox runat="server" ID="answerCheckA" Class="checkbox" Text="A," style="float:left" onclick="getButtonCheck(this,'A')"/>
                                    <asp:Label runat="server" ID="answerA" Class="answerTextA" Text='<%# Eval("answerA") %>' style="float:left;width:91%;"></asp:Label>
                                    <br />
                                    <br />
                                    <asp:CheckBox runat="server" ID="answerCheckB" Class="checkbox" Text="B," style="float:left" onclick="getButtonCheck(this,'B')"/>
                                    <asp:Label runat="server" ID="answerB" Class="answerTextB" Text='<%# Eval("answerB") %>' style="float:left;width:91%;"></asp:Label>
                                    <br />
                                    <br />
                                    <asp:CheckBox runat="server" ID="answerCheckC" Class="checkbox" Text="C," style="float:left" onclick="getButtonCheck(this,'C')"/>
                                    <asp:Label runat="server" ID="answerC" Class="answerTextC" Text='<%# Eval("answerC") %>' style="float:left;width:91%;"></asp:Label>
                                    <br />
                                    <br />
                                    <asp:CheckBox runat="server" ID="answerCheckD" Class="checkbox" Text="D," style="float:left" onclick="getButtonCheck(this,'D')"/>
                                    <asp:Label runat="server" ID="answerD" Class="answerTextD" Text='<%# Eval("answerD") %>' style="float:left;width:91%;"></asp:Label>
                                    <br />
                                    <br />
                                    <asp:CheckBox runat="server" ID="answerCheckE" Class="checkbox" Text="E," style="float:left" onclick="getButtonCheck(this,'E')"/>
                                    <asp:Label runat="server" ID="answerE" Class="answerTextE" Text='<%# Eval("answerE") %>' style="float:left;width:91%;"></asp:Label>
                                    <br />
                                    <br />
                                    <asp:CheckBox runat="server" ID="answerCheckF" Class="checkbox" Text="F," style="float:left" onclick="getButtonCheck(this,'F')"/>
                                    <asp:Label runat="server" ID="answerF" Class="answerTextF" Text='<%# Eval("answerF") %>' style="float:left;width:91%;"></asp:Label>
                                    <asp:Label runat="server" ID="answer" Class="answer" Text='<%# Eval("answer") %>' style="display:none;"></asp:Label>
                                </td>
                            </tr>
                        </table>
                     </ItemTemplate>
                 </asp:Repeater>
                 </div>
            </div>
        </div>
        <asp:TextBox runat="server" ID="formId" style="display:none;"></asp:TextBox>
        <asp:TextBox runat="server" ID="successOrNot" style="display:none;"></asp:TextBox>
        <div class="center">
             <asp:Button ID="btnSave" runat="server" Text="OK" onclick="btnSave_Clcik"  CssClass="btn btn-primary " />
             <input type="button" value="Cancle" class="btn" onclick="CloseForm()"   />
        </div>
    </form>
</body>
</html>



