using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MyLib;
using System.Text;
using System.Data;
using Ultimus.OC;

namespace Ultimus.UWF.OrgChart
{
    public partial class SelectUserNew : System.Web.UI.Page
    {
        private StringBuilder StrSQL;
        public string SelectType = "1";
        private List<string> DepartmentsList;
        private DataTable SelectedTable = new DataTable();
        string _domain = "";
        string _rootDept = "";
        //TODO:重构，把方法都提取到接口中，并把此页面替换为html+ajax
        protected void Page_Load(object sender, EventArgs e)
        {
            _domain = ConfigurationManager.AppSettings["DomainList"].Split(',')[0];
            _rootDept = ConfigurationManager.AppSettings["RootDepartment"];
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
                    SelectedList.Visible = true;
                    break;
                case "1": //单选人
                    bindGroups = false;
                    break;
                case "2": //多选人
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
                case "5": //多选岗位
                    SelectedList.Visible = true;
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

        private void BindDepartments()
        {
            Ultimus.OC.OrgChart oc = new OC.OrgChart();
            if (!string.IsNullOrEmpty(_domain))
            {
                oc = new OC.OrgChart(_domain);
            }
            Department[] depts = null;
            oc.GetTopLevelDepartments(out depts); //IOrg
            for (int j = 0; j < depts.Length; j++)
            {
                Department dept = depts[j];
                TreeNode node = new TreeNode();
                if (!string.IsNullOrEmpty(_rootDept) && dept.strDepartmentName != _rootDept)
                {
                    continue;
                }
                node.Text = dept.strDepartmentName;
                node.Value = dept.strDepartmentID;

                UserTreeView.Nodes.Add(node);

                if (j == 0)
                {
                    BingFirstNodes(node);
                }
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
                    //if (tn.Value.Split(new char[] { '|' }).Length > 1)
                    //{
                    rtnJson += "{'Name':'" + tn.Text + "',";
                    rtnJson += "'ID':'" + tn.Value + "',";
                    rtnJson += "'LoginName':'" + tn.Value + "',";
                    rtnJson += "'Type':'dept'},";
                    //}
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

            Department dept = new Department();
            Ultimus.OC.OrgChart oc = new OC.OrgChart();
            if (!string.IsNullOrEmpty(_domain))
            {
                oc = new OC.OrgChart(_domain);
            }
            oc.FindDepartment("", nn.Value, out dept);
            Department[] depts = null;
            dept.GetSubDepartments(out depts);
            foreach (Department dept1 in depts)
            {

                TreeNode node = new TreeNode();
                node.Text = dept1.strDepartmentName;
                node.Value = dept1.strDepartmentID;
                nn.ChildNodes.Add(node);
            }
            nn.Expand();
        }

        private void BingChildNodes()
        {
            string nodeValue = UserTreeView.SelectedNode.Value;
            if (UserTreeView.SelectedNode.ChildNodes.Count > 0)
            {
                return;
            }


            Department dept = new Department();
            Ultimus.OC.OrgChart oc = new OC.OrgChart();
            if (!string.IsNullOrEmpty(_domain))
            {
                oc = new OC.OrgChart(_domain);
            }
            oc.FindDepartment("", nodeValue, out dept);
            Department[] depts = null;
            dept.GetSubDepartments(out depts);
            if (UserTreeView.SelectedNode.ChildNodes.Count > 0)
            {
                UserTreeView.SelectedNode.ChildNodes.Clear();
            }
            foreach (Department dept1 in depts)
            {

                TreeNode node = new TreeNode();
                node.Text = dept1.strDepartmentName;
                node.Value = dept1.strDepartmentID;
                UserTreeView.SelectedNode.ChildNodes.Add(node);
            }
            UserTreeView.SelectedNode.Expand();
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

            Department dept = new Department();
            Ultimus.OC.OrgChart oc = new OC.OrgChart();
            if (!string.IsNullOrEmpty(_domain))
            {
                oc = new OC.OrgChart(_domain);
            }
            oc.FindDepartment("", nodeValue, out dept);
            User[] users = null;
            dept.GetDepartmentMembers(out users);
            DataTable dt = new DataTable();
            dt.Columns.Add("EmployeeID");
            dt.Columns.Add("Title");
            dt.Columns.Add("ShortName");
            dt.Columns.Add("FirstName");
            dt.Columns.Add("email");
            dt.Columns.Add("DepartmentName");
            foreach (User user in users)
            {
                DataRow row = dt.NewRow();
                row["EmployeeID"] = user.strUserName;
                row["Title"] = user.strJobFunction;
                row["ShortName"] = user.strUserName;
                row["FirstName"] = user.strUserFullName;
                row["email"] = "";
                row["DepartmentName"] = user.strDepartmentName;
                dt.Rows.Add(row);
            }

            //            string id = "";
            //            if (nodeValue.IndexOf('|') > 0)
            //            {
            //                id = nodeValue.Split('|')[0].ToString().Trim();
            //            }
            //            StrSQL = new StringBuilder();

            //            StrSQL.AppendFormat(@"select ve.USERID as EmployeeID,oj.JOBFUNCTION as Title,ve.LOGINNAME as ShortName,ve.USERNAME as FirstName,ve.email  as email from V_ORG_USERDEPARTMENT ved 
            //            inner join V_ORG_USER ve on ved.USERID=ve.USERID
            //            left join ORG_JOB oj on ve.USERID = oj.USERID where ved.DepartmentID='{0}'", id); 
            //DataTable dt = DataAccess.Instance("BizDB").ExecuteDataTable(StrSQL.ToString());
            Repeater1.DataSource = dt;
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
                        dr["Title"] = "";// (item.FindControl("Label2") as Label).Text;
                        dr["ShortName"] = (item.FindControl("UserAccount") as HiddenField).Value;
                        dr["FirstName"] = (item.FindControl("Label1") as Label).Text;
                        dr["email"] = "";
                        dr["EmployeeID"] = (item.FindControl("UserAccount") as HiddenField).Value.Trim();
                        dr["DepartmentName"] = (item.FindControl("Label3") as Label).Text;
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
            this.SelectedTable.Columns.Add("DepartmentName");
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
                case "5":
                    (e.Item.FindControl("CheckBox1") as CheckBox).Visible = true;
                    break;
            }
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

        public virtual DataTable GetUserInfoBySearchText(string searchText)
        {
            string domain = ConfigurationManager.AppSettings["DomainList"].Split(',')[0];
            //Ultimus.OC.OrgChart occ = new OC.OrgChart(domain);// Ultimus
            //OC.User user = null;
            searchText = "%" + searchText + "%";
            //occ.FindUser(searchText, "", "", out user);
            //if (user == null)
            //{
            //    occ.FindUser("", searchText, "", out user);
            //}
            string sql = @"SELECT [User].UserName,[User].DisplayName,[Organization].Name
              FROM [dbo].[User] (nolock) left join [dbo].Organization (nolock) on
              [Organization].[Id]=[User].[OrganizationId] where ([User].[IsEnabled]=1 and [User].[IsHidden]=0 and [User].[IsDeleted]=0) 
              and ([User].AccountName like '" + searchText + "' or [User].DisplayName like '" + searchText + "' or [Organization].Name like '" + searchText + "')";
            DataTable dt2 = DataAccess.Instance("ResDB").ExecuteDataTable(sql);
            DataTable dt = new DataTable();
            dt.Columns.Add("EMPLOYEEID");
            dt.Columns.Add("FIRSTNAME");
            dt.Columns.Add("LASTNAME");
            dt.Columns.Add("SHORTNAME");
            dt.Columns.Add("TITLE");
            dt.Columns.Add("EMAIL");
            dt.Columns.Add("SUPERVISORID");
            dt.Columns.Add("DEPARTMENTID");
            dt.Columns.Add("DepartmentName");
            for (int i = 0; i < dt2.Rows.Count; i++)
            {
                DataRow dr = dt.NewRow();
                dr["EMPLOYEEID"] = domain + "/" + dt2.Rows[i]["UserName"].ToString();
                dr["FIRSTNAME"] = dt2.Rows[i]["DisplayName"].ToString();
                dr["SHORTNAME"] = domain + "/" + dt2.Rows[i]["UserName"].ToString();
                dr["DepartmentName"] = dt2.Rows[i]["Name"].ToString();
                dr["TITLE"] = "";
                dt.Rows.Add(dr);
            }
            return dt;
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtSearch.Text))
            {
                //StrSQL = new StringBuilder();

                //StrSQL.AppendFormat("select * from (select ID as EMPLOYEEID ,FULLNAME as FIRSTNAME,' ' as LASTNAME,NAME as SHORTNAME,JOBFUNCTION as TITLE, '' as EMAIL,SUPERVISORID as SUPERVISORID,DEPTID as DEPARTMENTID FROM JOBS) ve where title like '%{0}%' or firstname like '%{0}%' or shortname like '%{0}%' or email like '%{0}%'", txtSearch.Text.Trim());
                DataTable dt = GetUserInfoBySearchText(txtSearch.Text);
                Repeater1.DataSource = dt.DefaultView;
                Repeater1.DataBind();
            }
        }

        protected void UserTreeView_TreeNodeCheckChanged(object sender, TreeNodeEventArgs e)
        {

        }
    }
}