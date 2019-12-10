using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Ultimus.UWF.Home2.Code.Rep;
using Ultimus.UWF.Home2.Code.DAO;
using Newtonsoft.Json;

namespace Ultimus.UWF.Home2
{
    public partial class AjaxOrgUser : System.Web.UI.Page
    {
        private string Query_Method { get { return (Request["Method"] + string.Empty).ToLower(); } }
        private int Query_ID { get { int i = 0; int.TryParse(Request.QueryString["ID"], out i); return i; } }
        private int Query_ParentID { get { int i = 0; int.TryParse(Request.QueryString["ParentID"], out i); return i; } }
        private string Query_Name { get { return Request["Name"]; } }
        private string Query_EngName { get { return Request["EngName"]; } }
        private string Query_LoginName { get { return Request["LoginName"]; } }
        private string Query_UserCode { get { return Request["UserCode"]; } }


        protected void Page_Load(object sender, EventArgs e)
        {
            RepResult result = new RepResult();
            try
            {
                switch (Query_Method)
                {
                    case "delorg":
                        DelOrg();
                        break;
                    case "deluser":
                        DelUser();
                        break;
                    case "validateorgname":
                        ValidateOrgName();
                        break;
                    case "validateusername":
                        ValidateUserName();
                        break;
                    case "validateusercode":
                        ValidateUserCode();
                        break;
                    default:
                        throw new Exception("没有对应的方法！");
                }
            }
            catch (Exception err)
            {
                result.Code = -1;
                result.ErrorMsg = err.Message;
            }

            string json = JsonConvert.SerializeObject(result);
            Response.Write(json);
            Response.End();
        }

        private void DelOrg()
        {
            if (Query_ID <= 0)
                throw new Exception("参数错误！");

            int totalPageCount = 0;
            OrgMgmt.Instance.Query(null, null, Query_ID, 0, 1, out totalPageCount);
            if (totalPageCount > 0)
                throw new Exception("请先删除子部门后在执行次操作！");

            UserMgmt.Instance.Query(null, Query_ID, true, null, 0, 1, out totalPageCount);
            if (totalPageCount > 0)
                throw new Exception("无法删除次，因为该部门下有用户！");

            OrgMgmt.Instance.Delete(Query_ID);
        }

        private void ValidateOrgName()
        {
            bool contains = OrgMgmt.Instance.ContainsName(Query_Name, Query_EngName, Query_ParentID, Query_ID);
            if (contains)
                throw new Exception("存在同名！");
        }

        private void ValidateUserName()
        {
            bool contains = UserMgmt.Instance.ContainsName(Query_LoginName, Query_ID);
            if (contains)
                throw new Exception("存在同名！");
        }

        private void ValidateUserCode()
        {
            bool contains = UserMgmt.Instance.ContainsUserCode(Query_UserCode, Query_ID);
            if (contains)
                throw new Exception("员工编号重复！");
        }

        private void DelUser()
        {
            if (Query_ID <= 0)
                throw new Exception("参数错误！");
            UserMgmt.Instance.Delete(Query_ID);
        }
    }
}