using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;
using MyLib;
using System.Data;
namespace Ultimus.UWF.Home2.Code
{
    public class UIHelper
    {
        public static void InitFlowDropdownList(DropDownList ddl, bool addEmpty)
        {
            //ddl.Items.Clear();
            //if (addEmpty)
            //{
            //    ddl.Items.Add(new ListItem(LanguageHelper.Get("DropdownListItemEmpty"), ""));
            //}
           DataTable dtProcessName=DataAccess.Instance("UltDB").ExecuteDataTable("select distinct PROCESSNAME from PROCESSES");
           ddl.DataSource = dtProcessName;
           ddl.DataTextField = "PROCESSNAME";
           ddl.DataValueField = "PROCESSNAME";
           ddl.DataBind();
           ddl.Items.Insert(0, new ListItem("--Please Select--", ""));
            //ddl.Items.Add(new ListItem(LanguageHelper.Get(string.Format("FlowName_{0}", "CCSQ")), "出差申请流程"));//出差申请
            //ddl.Items.Add(new ListItem(LanguageHelper.Get(string.Format("FlowName_{0}", "CCBX")), "出差报销流程"));//出差报销
            //ddl.Items.Add(new ListItem(LanguageHelper.Get(string.Format("FlowName_{0}", "GRBX")), "流程标示3"));//个人报销
            //ddl.Items.Add(new ListItem(LanguageHelper.Get(string.Format("FlowName_{0}", "XJYZ")), "流程标示4"));//现金预支
            //ddl.Items.Add(new ListItem(LanguageHelper.Get(string.Format("FlowName_{0}", "GRBT")), "流程标示5"));//个人补贴申请
        }
        public static string ChangeProcessName(string ProcessName)
        {
            string ReturnValue = "";
            switch (ProcessName)
            {
                case "OT Application":
                    ReturnValue = "Time Record Application Process";
                    break;
                default:
                    ReturnValue = ProcessName;
                    break;
            }
            return ReturnValue;
        }
    }
}