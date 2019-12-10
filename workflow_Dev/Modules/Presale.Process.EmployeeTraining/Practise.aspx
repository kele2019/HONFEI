<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Practise.aspx.cs" Inherits="Presale.Process.EmployeeTraining.Practise" %>
<%@ Register Src="/Modules/Ultimus.UWF.Form.ProcessControl/UserInfo.ascx" TagName="UserInfo" TagPrefix="ui" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <base target="_self"/>
    <meta http-equiv="Expires" content="-1" />
    <meta http-equiv="PRAGMA" content="NO-CACHE" />
    <meta http-equiv="Cache-Control" content="no-cache" />
    <title>Practise</title>
    <script type="text/javascript" src="/Assets/js/common.js"></script>
    <script type="text/javascript">
        function SinglePersonConfirm() {
//            $("#prastisetable").find("table").each(function (i, Etr) {
//                var value = "";
//                $(Etr).find(".checkbox").each(function (j, Item) {
//                    if ($(Item).children().eq(0).attr("checked")) {
//                        value += $(Item).children().eq(1).text();
//                    }
//                });
//                value = value.split(",").join("");
//                $(Etr).find(".answer").val(value);
//            });

            var amount = $("#test").find("[checked]").val();
            if (amount == "0" || amount == "1") {
                $("#havePapers").val("have");
                var returnJson = "[{'havePapers':'have'}]";
                window.returnValue = returnJson;
                window.close();
            }
            else {
                return false;
            }
        }
        function CloseForm() {
            $("#havePapers").val("haveNothing");
            var returnJson = "[{'havePapers':'haveNothing'}]";
            window.returnValue = returnJson;
            window.close();
        }
        $(document).ready(function () {
//            $("#prastisetable").find("table").each(function (i, Etr) {
//                $(Etr).find(".answerText").each(function (j, Item) {
//                    $(item).val(" ");
//                });
//            });
        });
    </script>
</head>
<body>
    <form id="Form1" runat="server" style="width:90%;margin-left:5%; margin-top:5%;margin-bottom:5%;">
     <div>
            <asp:TextBox runat="server" ID="FormID" style="display:none;" ></asp:TextBox>
            <asp:TextBox runat="server" ID="havePapers" style="display:none;"></asp:TextBox>
            <div class="row">
                 <p style="font-weight:bold;display:block;">Question amount</p>
                 <table class="table table-condensed table-bordered">
                    <tr>
                        <td class="td-label" >
                         <span style="height:30px; float:left;">&nbsp;</span>
                        <p style="text-align:center">Question amount</p>
                        </td>
                        <td class="td-content" colspan="3" >
                            <asp:RadioButtonList ID="test" runat="server" AutoPostBack="True" onselectedindexchanged="SelectedIndexChanged" RepeatDirection="Horizontal" RepeatLayout="Flow">
                            <asp:ListItem Value="0">5&nbsp;&nbsp;&nbsp;&nbsp;</asp:ListItem>
                            <asp:ListItem Value="1">10</asp:ListItem>
                            </asp:RadioButtonList>
                        </td>
                    </tr>
                </table>
                <p style="font-weight:bold;display:block;" id="Practise">Practise</p>
                <div id="prastisetable">
                <asp:Repeater  ID="PROC_EmployeeTraining_DT" runat="server">
                        <ItemTemplate> 
                        <table class="table table-bordered" id="Question1">
                            <tr>
                                <td class="td-label"  style=" width:auto; vertical-align:middle;">
                                 <span style="height:30px; float:left;">&nbsp;</span>
                                <p style="text-align:center">Question <%# Container.ItemIndex + 1 %></p>
                                </td>
                                <td class="td-content" colspan="3" >
                                <asp:TextBox runat="server" ID="Question" Text='<%#Eval("Question")%>' Width="98%" TextMode="MultiLine" Rows="5"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td class="td-label" style="vertical-align:middle;"> 
                               <span style="height:30px; float:left;">&nbsp;</span>
                                <p style="text-align:center">Answer</p>
                                </td>
                                <td class="td-content"  colspan="3" >
                                    <asp:CheckBox runat="server" ID="answerCheckA" Class="checkbox" Text="A," style="float:left" />
                                    <asp:TextBox runat="server" ID="answerA" Class="answerText" Text='<%#Eval("answerA").ToString()==""?"":Eval("answerA") %>' style="float:left;width:91%;"></asp:TextBox>
                                    <br />
                                    <br />
                                    <asp:CheckBox runat="server" ID="answerCheckB" Class="checkbox" Text="B," style="float:left"/>
                                    <asp:TextBox runat="server" ID="answerB" Class="answerText" Text='<%#Eval("answerB").ToString()==""?"":Eval("answerB") %>' style="float:left;width:91%;"></asp:TextBox>
                                    <br />
                                    <br />
                                    <asp:CheckBox runat="server" ID="answerCheckC" Class="checkbox" Text="C," style="float:left"/>
                                    <asp:TextBox runat="server" ID="answerC" Class="answerText" Text='<%#Eval("answerC").ToString()==""?"":Eval("answerC") %>' style="float:left;width:91%;"></asp:TextBox>
                                    <br />
                                    <br />
                                    <asp:CheckBox runat="server" ID="answerCheckD" Class="checkbox" Text="D," style="float:left"/>
                                    <asp:TextBox runat="server" ID="answerD" Class="answerText" Text='<%#Eval("answerD").ToString()==""?"":Eval("answerD") %>' style="float:left;width:91%;"></asp:TextBox>
                                    <br />
                                    <br />
                                    <asp:CheckBox runat="server" ID="answerCheckE" Class="checkbox" Text="E," style="float:left"/>
                                    <asp:TextBox runat="server" ID="answerE" Class="answerText" Text='<%#Eval("answerE").ToString()==""?"":Eval("answerE") %>' style="float:left;width:91%;"></asp:TextBox>
                                    <br />
                                    <br />
                                    <asp:CheckBox runat="server" ID="answerCheckF" Class="checkbox" Text="F," style="float:left"/>
                                    <asp:TextBox runat="server" ID="answerF" Class="answerText" Text='<%#Eval("answerF").ToString()==""?"":Eval("answerF") %>' style="float:left;width:91%;"></asp:TextBox>
                                    <asp:TextBox runat="server" ID="answer" Class="answer"  Text='<%#Eval("answer")%>' style="display:none;"></asp:TextBox>
                                </td>
                            </tr>
                        </table>
                        </ItemTemplate>
                    </asp:Repeater>
                    </div>
            </div>
           <%-- onclientclick="return SinglePersonConfirm()" --%>
             <div class="center">
               <asp:Button ID="btnSave" runat="server" Text="OK" onclick="btnSave_Clcik"   CssClass="btn btn-primary " />
             <input type="button" value="Close" class="btn" onclick="CloseForm()"   />
             </div>
        </div>
    </form>
</body>
</html>



