using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MyLib;
using System.Text;
using System.Data;
using Ultimus.UWF.OrgChart.Entity;
using Ultimus.UWF.OrgChart.Interface;

namespace Ultimus.UWF.OrgChart
{
    public partial class SelectUser : System.Web.UI.Page
    {
        private StringBuilder StrSQL;
        public string SelectType = "1";
        private List<string> DepartmentsList;
        private DataTable SelectedTable = new DataTable();
        //TODO:重构，把方法都提取到接口中，并把此页面替换为html+ajax
        protected void Page_Load(object sender, EventArgs e)
        {
            UserTreeView.ShowExpandCollapse = true;
            UserTreeView.ShowLines = true;
            if (Request.QueryString["Type"] != null)
            {
                SelectType = Request.QueryString["Type"].ToString().Trim();
            }
            bool bindGroups = true;
            switch (SelectType)
            {
                case "0": //所有
                    UserTreeView.ShowCheckBoxes = TreeNodeTypes.All;

                    //Button1.Visible = true;
                    //Button2.Visible = true;
                    SelectedList.Visible = true;
                    break;
                case "1": //单选人
                    bindGroups = false;
                    break;
                case "2": //多选人
                    //Button1.Visible = true;
                    //Button2.Visible = true;
                    SelectedList.Visible = true;
                    bindGroups = false;
                    break;
                case "4": //多选部门
                    Button1.Visible = false;
                    Button2.Visible = false;
                    UserTreeView.ShowCheckBoxes = TreeNodeTypes.All;
                    Repeater1.Visible = false;
                    bindGroups = false;
                    break;
                default:
                    Button1.Visible = false;
                    Button2.Visible = false;
                    break;
            }

            if (hidSelectType.Value.StartsWith("3"))
            {
                UserTreeView.ShowCheckBoxes = TreeNodeTypes.Leaf | TreeNodeTypes.Parent;
            }
            if (!IsPostBack)
            {
                hidSelectType.Value = this.SelectType;
                InitTable();
                BindDepartments();
                if (bindGroups)
                {
                    BindGroups();
                }
            }

        }

        IOrg _org = ServiceContainer.Instance().GetService<IOrg>();
        private void BindDepartments()
        {
            //StrSQL = new StringBuilder();
            //string sql =  "select departmentid,departmentname,parentid  parentdept from V_ORG_DEPARTMENT  vt where parentid=0";
            ////sql = "select ID as DEPARTMENTID ,  NAME as DEPARTMENTNAME,  PARENTDEPT as PARENTID FROM DEPARTMENTS  a    where PARENTDEPT=0 ";
            //StrSQL.AppendLine(sql);
            //DataTable dt = DataAccess.Instance("BizDB").ExecuteDataTable(StrSQL.ToString());
            //int i = 0;
            //foreach (DataRow row in dt.Rows)
            //{
            //    TreeNode node = new TreeNode();
            //    node.Text = getDeptName(row["departmentname"].ToString().Trim());
            //    node.Value = row["departmentid"].ToString().Trim() + "|DEPT";

            //    UserTreeView.Nodes.Add(node);
            //    if (i == 0)
            //    {
            //        BingFirstNodes(node);
            //    }
            //    i++;
            //}

            UserTreeView.Nodes.Clear();
            List<DepartmentEntity> list = _org.GetDepartmentList();

            //加第一层节点
            List<DepartmentEntity> rootlist = list.FindAll(p => p.PARENTID == 0);
            foreach (DepartmentEntity ety in rootlist)
            {
                TreeNode tn = new TreeNode();
                tn.Text = ety.DEPARTMENTNAME;

                tn.Value = ety.DEPARTMENTID.ToString() + "|DEPT";
                UserTreeView.Nodes.Add(tn);

                //递归插入子节点
                AddChildNodes(tn, ety.DEPARTMENTID, list);
                if (!flag)
                {
                    tn.Expand();
                    flag = true;
                }
            }
            UserTreeView.ExpandDepth = 0;
        }

        bool flag = false;
        void AddChildNodes(TreeNode parent, int id, List<DepartmentEntity> list)
        {
            List<DepartmentEntity> childlist = list.FindAll(p => p.PARENTID == id);
            foreach (DepartmentEntity ety in childlist)
            {
                TreeNode tn = new TreeNode();
                tn.Text = ety.DEPARTMENTNAME;

                tn.Value = ety.DEPARTMENTID.ToString() + "|DEPT";
                parent.ChildNodes.Add(tn);

                AddChildNodes(tn, ety.DEPARTMENTID, list);


            }
        }

        private void BindGroups()
        {
            TreeNode tn = new TreeNode();
            tn.Text = "Groups组";
            UserTreeView.Nodes.Add(tn);
            StrSQL = new StringBuilder();
            StrSQL.AppendLine("SELECT      GROUPID as ID,    GROUPNAME FROM        V_ORG_GROUP");
            DataTable dt = DataAccess.Instance("BizDB").ExecuteDataTable(StrSQL.ToString());
            int i = 0;
            foreach (DataRow row in dt.Rows)
            {
                TreeNode node = new TreeNode();
                node.Text = getDeptName(row["GROUPNAME"].ToString().Trim());
                node.Value = row["ID"].ToString().Trim() + "|Group";

                tn.ChildNodes.Add(node);

                i++;
            }
        }

        string rtnJson = "";
        protected void btnOK_Click(object sender, EventArgs e)
        {
            IterateTreeView(UserTreeView.Nodes);
            Page.ClientScript.RegisterStartupScript(this.GetType(), "RtnVal", "<script>Confirm(\"" + rtnJson + "\");</script>");
        }

        public void IterateTreeView(TreeNodeCollection tnc)
        {
            foreach (TreeNode tn in tnc)
            {
                if (tn.Checked)
                {
                    if (tn.Value.Split(new char[] { '|' }).Length > 1)
                    {
                        rtnJson += "{'Name':'" + tn.Text + "[" + tn.Value.Split(new char[] { '|' })[1] + "]',";
                        rtnJson += "'ID':'" + tn.Value.Split(new char[] { '|' })[0] + "',";
                        rtnJson += "'Type':'" + tn.Value.Split(new char[] { '|' })[1] + "'},";
                    }
                }
                IterateTreeView(tn.ChildNodes);
            }
        }

        protected void UserTreeView_SelectedNodeChanged(object sender, EventArgs e)
        {
            BingChildNodes();
            BingUserList();
        }

        private void BingFirstNodes(TreeNode nn)
        {
            string nodeValue = nn.Value;
            string id = "";
            if (nodeValue.IndexOf("|") > 0)
            {
                id = nodeValue.Split('|')[0].ToString().Trim();
            }
            StrSQL = new StringBuilder();
            StrSQL.AppendFormat("select  DEPARTMENTID ,  DEPARTMENTNAME,    PARENTID FROM V_ORG_DEPARTMENT  a    where PARENTID='{0}'", id);
            DataTable dt = DataAccess.Instance("BizDB").ExecuteDataTable(StrSQL.ToString());
            if (dt.Rows.Count > 0)
            {
                if (nn.ChildNodes.Count > 0)
                {
                    nn.ChildNodes.Clear();
                }
                foreach (DataRow row in dt.Rows)
                {
                    TreeNode node = new TreeNode();
                    node.Text = getDeptName(row["departmentname"].ToString().Trim());
                    node.Value = row["departmentid"].ToString().Trim() + "|DEPT";
                    nn.ChildNodes.Add(node);
                }
                nn.Expand();
            }
        }

        private void BingChildNodes()
        {
            string nodeValue = UserTreeView.SelectedNode.Value;
            if (UserTreeView.SelectedNode.ChildNodes.Count > 0)
            {
                return;
            }
            string id = "";
            string type = "";
            if (nodeValue.IndexOf("|") > 0)
            {
                id = nodeValue.Split('|')[0].ToString().Trim();
                type = nodeValue.Split('|')[1].ToString().Trim();
            }
            if (type == "DEPT")
            {
                StrSQL = new StringBuilder();
                StrSQL.AppendFormat("select   DEPARTMENTID ,  DEPARTMENTNAME,   PARENTID FROM V_ORG_DEPARTMENT  a    where PARENTID='{0}'", id);
                DataTable dt = DataAccess.Instance("BizDB").ExecuteDataTable(StrSQL.ToString());
                if (dt.Rows.Count > 0)
                {
                    if (UserTreeView.SelectedNode.ChildNodes.Count > 0)
                    {
                        UserTreeView.SelectedNode.ChildNodes.Clear();
                    }
                    foreach (DataRow row in dt.Rows)
                    {
                        TreeNode node = new TreeNode();
                        node.Text = getDeptName(row["departmentname"].ToString().Trim());
                        node.Value = row["departmentid"].ToString().Trim() + "|DEPT";
                        UserTreeView.SelectedNode.ChildNodes.Add(node);
                    }
                    UserTreeView.SelectedNode.Expand();
                }
            }
        }

        private string getDeptName(string name)
        {
            if (!String.IsNullOrEmpty(name))
            {
                int index = name.IndexOf('[');
                if (index > 0)
                {
                    return name.Substring(0, index);
                }
                else
                {
                    return name;
                }
            }
            else
            {
                return "";
            }
        }

        private void BingUserList()
        {
            string nodeValue = UserTreeView.SelectedNode.Value;
            string id = "";
            if (nodeValue.IndexOf('|') > 0)
            {
                id = nodeValue.Split('|')[0].ToString().Trim();
            }
            StrSQL = new StringBuilder();

            StrSQL.AppendFormat(@"select ve.USERID as EmployeeID,oj.JOBFUNCTION as Title,ve.LOGINNAME as ShortName,ve.USERNAME as FirstName,ve.email  as email from V_ORG_USERDEPARTMENT ved 
            inner join V_ORG_USER ve on ved.USERID=ve.USERID
            left join ORG_JOB oj on ve.USERID = oj.USERID where ved.DepartmentID='{0}'", id); 
            DataTable dt = DataAccess.Instance("BizDB").ExecuteDataTable(StrSQL.ToString());
            Repeater1.DataSource = dt.DefaultView;
            Repeater1.DataBind();
        }

        private decimal GetUsers(string DepartmentID)
        {
            DepartmentsList = new List<string>();
            GetDepartments(DepartmentID);
            decimal UserCount = 0;
            StrSQL = new StringBuilder();
            if (DepartmentsList.Count > 0)
            {
                StrSQL.AppendLine("select count(*) from (select ID as EMPLOYEEID ,  DEPTID as DEPARTMENTID,	 JOBFUNCTION ,ISPRIMARY,ID as JOBID,SUPERVISORID FROM JOBS ) ved inner join (select ID as EMPLOYEEID ,FULLNAME as FIRSTNAME,' ' as LASTNAME,NAME as SHORTNAME,JOBFUNCTION as TITLE, '' as EMAIL,SUPERVISORID as SUPERVISORID,DEPTID as DEPARTMENTID FROM JOBS) ve on ved.employeeid=ve.EmployeeID where ved.DepartmentID in (");
                foreach (string str in DepartmentsList)
                {
                    StrSQL.AppendLine("'" + str + "',");
                }
                StrSQL = new StringBuilder(StrSQL.ToString().Substring(0, StrSQL.ToString().LastIndexOf(',')));
                StrSQL.AppendLine(")");
            }
            else
            {
                StrSQL.AppendLine("select count(*) from (select ID as EMPLOYEEID ,  DEPTID as DEPARTMENTID,	 JOBFUNCTION ,ISPRIMARY,ID as JOBID,SUPERVISORID FROM JOBS ) ved inner join (select ID as EMPLOYEEID ,FULLNAME as FIRSTNAME,' ' as LASTNAME,NAME as SHORTNAME,JOBFUNCTION as TITLE, '' as EMAIL,SUPERVISORID as SUPERVISORID,DEPTID as DEPARTMENTID FROM JOBS) ve on ved.employeeid=ve.EmployeeID where ved.DepartmentID='" + DepartmentID + "'");
            }
            UserCount = ConvertUtil.ToDecimal(DataAccess.Instance("BizDB").ExecuteScalar(StrSQL.ToString()));


            return UserCount;
        }

        private void GetDepartments(string DepartmentID)
        {
            StrSQL = new StringBuilder();
            StrSQL.AppendLine("select ID as DEPARTMENTID ,  DEPARTMENTNAME,  PARENTDEPT as PARENTID FROM DEPARTMENTS  a    where PARENTDEPT= '" + DepartmentID + "'");
            DataTable dt = DataAccess.Instance("UltDB").ExecuteDataTable(StrSQL.ToString());
            foreach (DataRow row in dt.Rows)
            {
                DepartmentsList.Add(row["departmentid"].ToString());
                GetDepartments(row["departmentid"].ToString());
            }
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            DataTable dt = ViewState["table"] as DataTable;
            foreach (RepeaterItem item in Repeater1.Items)
            {
                CheckBox cb = item.FindControl("CheckBox1") as CheckBox;
                RadioButton rb = item.FindControl("RadioButton1") as RadioButton;
                if (cb.Checked || rb.Checked)
                {
                    //cb.Enabled = false;
                    string str = (item.FindControl("UserID") as HiddenField).Value;
                    if (dt.Select("EmployeeID='" + str.Trim() + "'").Length == 0)
                    {
                        DataRow dr = dt.NewRow();
                        dr["Title"] = (item.FindControl("Label2") as Label).Text;
                        dr["ShortName"] = (item.FindControl("UserAccount") as Label).Text;
                        dr["FirstName"] = (item.FindControl("Label1") as Label).Text;
                        dr["email"] = (item.FindControl("Label3") as Label).Text;
                        dr["EmployeeID"] = (item.FindControl("UserID") as HiddenField).Value.Trim();
                        dt.Rows.Add(dr);

                    }
                }
            }
            Repeater2.DataSource = dt;
            Repeater2.DataBind();
            ViewState["table"] = dt;

            Button2.Visible = true;
        }

        private void InitTable()
        {
            this.SelectedTable.Columns.Add("EmployeeID");
            this.SelectedTable.Columns.Add("Title");
            this.SelectedTable.Columns.Add("ShortName");
            this.SelectedTable.Columns.Add("FirstName");
            this.SelectedTable.Columns.Add("email");
            ViewState["table"] = this.SelectedTable;
        }

        protected void Repeater1_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            (e.Item.FindControl("CheckBox1") as CheckBox).Visible = false;
            (e.Item.FindControl("RadioButton1") as RadioButton).Visible = false;
            switch (this.SelectType)
            {
                case "0":
                    (e.Item.FindControl("CheckBox1") as CheckBox).Visible = true;
                    break;
                case "1":
                    (e.Item.FindControl("RadioButton1") as RadioButton).Visible = true;
                    break;
                case "2":
                    (e.Item.FindControl("CheckBox1") as CheckBox).Visible = true;
                    break;
            }
            //if (this.SelectType == "1")
            //{
            //    (e.Item.FindControl("CheckBox1") as CheckBox).Visible = false;
            //}
            //else if (this.SelectType == "2")
            //{
            //    (e.Item.FindControl("RadioButton1") as RadioButton).Visible = false;
            //    DataTable dt = ViewState["table"] as DataTable;
            //    foreach (DataRow row in dt.Rows)
            //    {
            //        if (row["EmployeeID"].ToString() == (e.Item.FindControl("UserID") as HiddenField).Value)
            //        {
            //            (e.Item.FindControl("CheckBox1") as CheckBox).Checked = true;
            //            (e.Item.FindControl("CheckBox1") as CheckBox).Enabled = false;
            //        }
            //    }
            //}
        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            foreach (RepeaterItem item in Repeater2.Items)
            {
                if ((item.FindControl("CheckBox2") as CheckBox).Checked)
                {
                    string id = (item.FindControl("UserID") as HiddenField).Value;
                    DataTable dt = ViewState["table"] as DataTable;
                    foreach (DataRow row in dt.Rows)
                    {
                        if (row["EmployeeID"].ToString() == id)
                        {
                            row.Delete();
                            break;
                        }
                    }
                }
            }

            DataTable table = ViewState["table"] as DataTable;
            Repeater2.DataSource = table;
            Repeater2.DataBind();

        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtSearch.Text))
            {
                StrSQL = new StringBuilder();

                StrSQL.AppendFormat("select * from (select ID as EMPLOYEEID ,FULLNAME as FIRSTNAME,' ' as LASTNAME,NAME as SHORTNAME,JOBFUNCTION as TITLE, '' as EMAIL,SUPERVISORID as SUPERVISORID,DEPTID as DEPARTMENTID FROM JOBS) ve where title like '%{0}%' or firstname like '%{0}%' or shortname like '%{0}%' or email like '%{0}%'", txtSearch.Text.Trim());
                DataTable dt = DataAccess.Instance("UltDB").ExecuteDataTable(StrSQL.ToString());
                Repeater1.DataSource = dt.DefaultView;
                Repeater1.DataBind();
            }
        }

        protected void UserTreeView_TreeNodeCheckChanged(object sender, TreeNodeEventArgs e)
        {

        }
    }
}